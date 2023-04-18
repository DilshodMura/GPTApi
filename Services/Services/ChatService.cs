using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using OpenAI.API;
using Services.ServiceModels;

namespace Services.Services
{
    public sealed class ChatService: IChatService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatService(
            IHistoryRepository historyRepository,
            ITopicRepository topicRepository,
            IMessageRepository messageRepository)
        {
            _historyRepository = historyRepository;
            _topicRepository = topicRepository;
            _messageRepository = messageRepository;
        }
        private async Task<string> CallOpenApi(string messageText)
        {
            // Set up the OpenAI API client
            var openai = new OpenAIAPI("<API_KEY>");

            // Set up the request parameters
            var prompt = $"User: {messageText}\nAI:";
            var temperature = 0.5;
            var maxTokens = 50;

            // Call the OpenAI API to generate a response
            var response = await openai.Completions.CreateCompletionAsync(
                model: "text-davinci-002",
                prompt: prompt,
                temperature: temperature,
                max_tokens: maxTokens
            );

            // Get the generated text from the API response
            var generatedText = response.Completions[0].Text;

            return generatedText;
        }
        public async Task<string> GetResponse(long userId, string messageText)
        {
            // Get the user's chat history or create a new one if none exists
            var history = await _historyRepository.CreateIfNotExistsAsync(userId);

            // Get all the topics in the user's chat history
            var topics = await _historyRepository.GetAllTopicsAsync(history.Id);

            // Get the latest topic in the user's chat history, or create a new one if none exists
            var latestTopic = topics.LastOrDefault() as Topic;
            if (latestTopic == null)
            {
                latestTopic = new Topic
                {
                    HistoryId = history.Id,
                    Messages = new List<IMessage>()
                };

                await _topicRepository.AddAsync(latestTopic);
                topics.Add(latestTopic);

                await _historyRepository.UpdateAsync(history);
            }

            // Create a new message object with the user's message
            var userMessage = new Message
            {
                MessageText = messageText,
                MessageTime = DateTime.UtcNow,
                IsUser = true
            };

            // Add the user message to the latest topic

            latestTopic.Messages.Add(userMessage);

            // Call the OpenAI API to get a response
            var openAiResponse = await CallOpenApi(messageText);

            // Create a new message object with the OpenAI response
            var aiMessage = new Message
            {
                MessageText = openAiResponse,
                MessageTime = DateTime.UtcNow,
                IsUser = false,
            };

            // Add the AI message to the latest topic
            latestTopic.Messages.Add(aiMessage);

            // Save the new messages and topic to the database
            await _topicRepository.UpdateAsync(latestTopic);
            await _messageRepository.AddAsync(userMessage);
            await _messageRepository.AddAsync(aiMessage);

            // Update the history
            await _historyRepository.UpdateAsync(history);

            // Return the OpenAI response
            return openAiResponse;
        }
        public async Task<List<ITopic>> GetTopics(long userId)
        {
            var topicList = await _historyRepository.GetAllTopicsAsync(userId);
            return topicList;
        }
        public async Task ClearTopics(long userId)
        {
            // Get the user's chat history or create a new one if none exists
            var history = await _historyRepository.GetByUserIdAsync(userId);
            Console.WriteLine(history);

            // Get all the topics in the user's chat history
            var topics = await _historyRepository.GetAllTopicsAsync(history.Id);

            // Remove all topics from the history
            foreach (var topic in topics)
            {
                await _topicRepository.DeleteAsync(topic.Id);
            }

            // Update the history in the database
            await _historyRepository.UpdateAsync(history);
        }
    }
}
