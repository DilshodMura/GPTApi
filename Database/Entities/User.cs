
namespace Database.Entities
{
    public sealed class User
    {
        /// <summary>
        /// Gets or sets the user's id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; }
    }
}
