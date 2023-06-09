﻿
namespace Domain.Entities
{
    public interface IUser
    {
        /// <summary>
        /// Gets or sets the user's id.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; }
    }
}
