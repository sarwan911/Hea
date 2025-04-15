using Hea.Models;

namespace Hea
{
    public interface IAuth
    {
        string Authentication(string username, string password);
        User GetUser(string userId);
    }
}
