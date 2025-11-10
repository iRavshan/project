using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string username, string password);
        Task Register(string dtoUserName, string dtoEmail, string dtoPassword, 
            string? dtoFirstName = null, string? dtoLastName = null, string? dtoPhone = null, string role = "Student");
    }
}
