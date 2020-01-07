using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Models;
using API.Utility;

namespace API.Services
{
    public interface IInstanceService
    {
        Result<Instance> CreateInstance(long templateId, TimeSpan duration);
        Result<Instance> GetInstance(long instanceId, bool allowHistoric);

        IEnumerable<Instance> GetActiveInstances();
        IEnumerable<Instance> GetAllInstances();
    }

    public class InstanceService : IInstanceService
    {
        private readonly AppDbContext _context;

        public InstanceService(AppDbContext context)
        {
            _context = context;
        }

        public Result<Instance> CreateInstance(long templateId, TimeSpan duration)
        {
            throw new NotImplementedException();
        }

        public Result<Instance> GetInstance(long instanceId, bool allowHistoric)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instance> GetActiveInstances()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instance> GetAllInstances()
        {
            throw new NotImplementedException();
        }
    }
}
