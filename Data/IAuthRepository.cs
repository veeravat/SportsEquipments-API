using System.Threading.Tasks;
using OOAD.Models;

namespace OOAD.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user,string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
         Task<bool> UserIDExists(string userid);
    }
}