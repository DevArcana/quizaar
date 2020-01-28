using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
    }
}
