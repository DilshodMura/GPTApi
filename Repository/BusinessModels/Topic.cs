
using Domain.Entities;

namespace Repository.BusinessModels
{
    public class Topic : ITopic
    {
        /// <summary>
        /// Gets or internal sets topic id.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Gets or internal sets topic name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or internal sets topic hisory id.
        /// </summary>
        public long HistoryId { get; internal set; }
        public IHistory History { get; internal set; }

        /// <summary>
        /// Gets or internal sets topic messages.
        /// </summary>
        public ICollection<IMessage> Messages { get; internal set; }
    }
}
