using Domain.Entities;

namespace Services.ServiceModels
{
    public sealed class RefreshToken:IRefreshToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
