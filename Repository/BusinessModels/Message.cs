
namespace Repository.BusinessModels
{
    public sealed class Message
    {
        /// <summary>
        /// Gets or sets the id for message.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Gets or sets the topic id for messages.
        /// </summary>
        public long TopicId { get; internal set; }

        public Topic Topic { get; internal set; }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        public string MessageText { get; internal set; }

        /// <summary>
        /// Gets or sets the message time.
        /// </summary>
        public DateTime MessageTime { get; internal set; }

        /// <summary>
        /// Gets or sets if message from user 
        /// </summary>
        public bool IsUser { get; internal set; }

    }
}
