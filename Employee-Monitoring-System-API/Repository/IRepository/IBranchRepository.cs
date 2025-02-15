using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface IBranchRepository
    {
        IEnumerable<Branch> GetAll();
        Branch GetById(int id);
        void Add(Branch branch);
        void Update(Branch branch);
        void Delete(int id);

    }
}
