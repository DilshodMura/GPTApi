using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Authenticate user by checking email and password.
        /// </summary>
        public async Task<IUser> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null || !VerifyPasswordHash(password, user.Password))
            {
                // Authentication failed
                return null;
            }

            // Authentication succeeded, return the user object
            return user;
        }

        /// <summary>
        /// Checking password.
        /// </summary>
        private static bool VerifyPasswordHash(string password, string passwordHash)
        {
            // TODO: Implement password hashing and verification logic
            // For simplicity, this implementation simply compares the strings
            return password == passwordHash;
        }
    }
}
