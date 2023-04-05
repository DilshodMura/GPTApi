
namespace Domain.Entities
{
    public interface IMessage
    {
        /// <summary>
        /// Gets or sets the id for message.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets or sets the topic id for messages.
        /// </summary>
        public long TopicId { get; }
        public ITopic Topic { get; }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        public string MessageText { get; }

        /// <summary>
        /// Gets or sets the message time.
        /// </summary>
        public DateTime MessageTime { get; }
    }
}
