
namespace Database.Entities
{
    public class Topic
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long HistoryId { get; set; }
        public History History { get; set; }

        public ICollection<Message> Messages { get; set; } 
    }
}
