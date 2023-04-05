
namespace Domain.Entities
{
    public interface ITopic
    {
        public long Id { get; }
        public string Name { get; }
        public long HistoryId { get; }
        public IHistory History { get; }
    }
}
