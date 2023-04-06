
using Domain.Entities;

namespace Repository.BusinessModels
{
    public class Topic : ITopic
    {
        public long Id { get; internal set; }
        public string Name { get; internal set; }
        public long HistoryId { get; internal set; }
        public History History { get; internal set; }

        public ICollection<Message> Messages { get; internal set; }
    }
}
