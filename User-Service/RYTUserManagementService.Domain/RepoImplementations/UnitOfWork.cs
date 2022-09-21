using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Models;


namespace RYTUserManagementService.Domain.RepoImplementations
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly UserManagementDbContext _context;
        private IGenericRepository<Student> _students;
        private IGenericRepository<Teacher> _teachers;
        private IGenericRepository<School> _schools;


        public UnitOfWork(UserManagementDbContext context)
        {
            _context = context;

        }
        public IGenericRepository<Student> Students => _students ??= new GenericRepository<Student>(_context);
        public IGenericRepository<Teacher> Teachers => _teachers ??= new GenericRepository<Teacher>(_context);
        public IGenericRepository<School> Schools => _schools ??= new GenericRepository<School>(_context);
        
       

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
