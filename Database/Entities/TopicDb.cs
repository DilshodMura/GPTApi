
namespace Database.Entities
{
    public class TopicDb
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long HistoryId { get; set; }
        public HistoryDb History { get; set; }

        public ICollection<MessageDb> Messages { get; set; }
    }
}
