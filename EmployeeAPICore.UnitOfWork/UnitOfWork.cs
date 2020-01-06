using EmployeeAPICore.Model;
using EmployeeAPICore.Repository;
using System;

namespace EmployeeAPICore.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly EmployeeAPICoreDbContext _context;
        public GenericRepository<Employee> Employees { get; private set; }
        public GenericRepository<Department> Departments { get; private set; }
        public UnitOfWork(EmployeeAPICoreDbContext context)
        {
            _context = context;
            Employees = new GenericRepository<Employee>(_context);
            Departments = new GenericRepository<Department>(_context);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
