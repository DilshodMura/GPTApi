
namespace Database.Entities
{
    public sealed class History
    {
        /// <summary>
        /// Gets or sets history id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user's id as ref.
        /// </summary>
        public long UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Gets or sets time of the sent message.
        /// </summary>
        public DateTime MsgTime { get; set; }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        public ICollection<Topic> Topics { get; set; }
    }
}
