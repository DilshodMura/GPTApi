
namespace Domain.Entities
{
    public interface ITopic
    {
        /// <summary>
        /// Gets topic id.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets topic name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets topic history id.
        /// </summary>
        public long HistoryId { get; }
        public ICollection<IMessage> Messages { get; }
    }
}
