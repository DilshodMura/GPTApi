
namespace Database.Entities
{
    public sealed class RefreshTokenDb
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
