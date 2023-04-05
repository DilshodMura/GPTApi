
namespace Domain.Entities
{
    public interface IHistory
    {
        /// <summary>
        /// Gets or sets history id.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets or sets the user's id as ref.
        /// </summary>
        public long UserId { get; }
        public IUser User { get; }

        /// <summary>
        /// Gets or sets time of the sent message.
        /// </summary>
        public DateTime MsgTime { get; }
    }
}
