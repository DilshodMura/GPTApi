
using Domain.Entities;

namespace Services.ServiceModels
{
    public class Topic : ITopic
    {
        public long Id { get; internal set; }
        private string _name;

        public string Name
        {
            get => _name ?? "Test";
            set => _name = value;
        }
        public long HistoryId { get; internal set; }

        public ICollection<IMessage> Messages { get; internal set; }
    }
}
