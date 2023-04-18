using Domain.Entities;

namespace Services.ServiceModels
{
    public sealed class User: IUser
    {
        /// <summary>
        /// Gets or sets the user's id.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; internal set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; internal set; }
    }
}
