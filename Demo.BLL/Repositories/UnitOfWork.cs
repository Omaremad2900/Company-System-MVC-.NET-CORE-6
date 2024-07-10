using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext dbContext) 
        {
            EmployeeRepository =new EmployeeRepository(dbContext);
            DepartmentRepository =new DepartmentRepository(dbContext);
            _appDbContext = dbContext;
        
        }

        public async Task<int> Complete()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
