using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository;

public interface IBlockRepository
{
    Task CreateAsync(string key);
    Task<IList<IBlock>> GetAsync(string key);
    Task Update(string key, Guid sectionId, IBlock block);
    Task RemoveSection(string key, Guid sectionId);
}
