using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILoginService
    {
        //Auth method
        public Task<IUser> AuthenticateUserAsync(string email, string password);
    }
}
