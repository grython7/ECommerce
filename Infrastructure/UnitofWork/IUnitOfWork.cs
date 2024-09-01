using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitofWork
{
    public interface IUnitOfWork
    {
        public Task<int> SaveAsync();
    }
}
