using RYTUserManagementService.Models;

namespace RYTUserManagementService.Domain.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
  
        IGenericRepository<Student> Students { get;}
        IGenericRepository<School> Schools { get;}
        IGenericRepository<Teacher> Teachers { get;}

        Task Save();
    }
}
