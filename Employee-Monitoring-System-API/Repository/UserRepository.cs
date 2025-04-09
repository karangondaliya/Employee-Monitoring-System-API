using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Employee_Monitoring_System_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users
                .Include(u => u.UserTasks).ThenInclude(ut => ut.Task)
                .Include(u => u.ProjectMemberships).ThenInclude(pm => pm.Project)
                .ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users
            .Include(u => u.UserTasks).ThenInclude(ut => ut.Task)
            .Include(u => u.ProjectMemberships).ThenInclude(pm => pm.Project)
            .FirstOrDefault(u => u.Id == id);
        }

        public User Update(User userChanges)
        {
            var user = _context.Users.Attach(userChanges);
            user.State = EntityState.Modified;
            _context.SaveChanges();
            return userChanges;
        }

        public User ValidateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        public User FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}