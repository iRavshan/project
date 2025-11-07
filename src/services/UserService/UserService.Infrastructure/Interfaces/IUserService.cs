using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task Register(string username, string email, string password);
        Task<string> Login(string username, string password);
    }
}
