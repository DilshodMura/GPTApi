
namespace Database.Entities
{
    public sealed class MessageDb
    {
        /// <summary>
        /// Gets or sets the id for message.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the topic id for messages.
        /// </summary>
        public long TopicId { get; set; }

        public TopicDb Topic { get; set; }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Gets or sets the message time.
        /// </summary>
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// Gets or sets if message from user 
        /// </summary>
        public bool IsUser { get; set; }

    }
}
