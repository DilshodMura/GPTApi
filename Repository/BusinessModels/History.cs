using Domain.Entities;

namespace Repository.BusinessModels
{
    public sealed class History : IHistory
    {
        /// <summary>
        /// Gets or sets history id.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Gets or sets the user's id as ref.
        /// </summary>
        public long UserId { get; internal set; }
        public IUser User { get; internal set; }

        /// <summary>
        /// Gets or sets time of the sent message.
        /// </summary>
        public DateTime MsgTime { get; internal set; }
    }
}
