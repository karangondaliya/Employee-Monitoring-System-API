using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _context;

        public BranchRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Branch branch)
        {
            _context.Branches.Add(branch);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Branch br = _context.Branches.Find(id);
            if (br != null)
            {
                _context.Branches.Remove(br);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Branch> GetAll()
        {
            return _context.Branches.ToList();
        }

        public Branch GetById(int id)
        {
            return _context.Branches.Find(id);
        }

        public void Update(Branch branchChanges)
        {
            var br = _context.Branches.Attach(branchChanges);
            br.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
