
namespace Domain.Entities
{
    public interface IRefreshToken
    {
        /// <summary>
        /// Gets or sets the refresh token id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the refresh token userid.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token date time exp.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
