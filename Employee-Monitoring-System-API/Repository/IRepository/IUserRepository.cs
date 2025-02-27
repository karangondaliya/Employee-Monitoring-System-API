using System.Collections.Generic;
using System.Threading.Tasks;
using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        User Add(User user);
        User Update(User userChanges);
        User Delete(int id);
        User GetByEmail(string email);
    }
}