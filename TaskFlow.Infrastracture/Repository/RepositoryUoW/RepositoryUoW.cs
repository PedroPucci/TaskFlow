using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using TaskFlow.Infrastracture.Connections;
using TaskFlow.Infrastracture.Repository.Interfaces;
using TaskFlow.Infrastracture.Repository.Request;

namespace TaskFlow.Infrastracture.Repository.RepositoryUoW
{
    public class RepositoryUoW : IRepositoryUoW
    {
        private readonly DataContext _context;
        private bool _disposed = false;
        private IUserRepository? _userEntityRepository = null;
        private ITaskRepository? _taskEntityRepository = null;
        private ICategoryRepository? _categoryRepository = null;

        public RepositoryUoW(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userEntityRepository is null)
                {
                    _userEntityRepository = new UserRepository(_context);
                }
                return _userEntityRepository;
            }
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                if (_taskEntityRepository is null)
                {
                    _taskEntityRepository = new TaskRepository(_context);
                }
                return _taskEntityRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository is null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error($"Database connection failed: {ex.Message}");
                throw new ApplicationException("Database is not available. Please check the connection.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}