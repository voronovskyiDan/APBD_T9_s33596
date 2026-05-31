using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface INoteRepository
    {
        Task<List<Note>> GetByUserIdAsync(int userId);
        Task<Note?> GetByIdAsync(int id, int userId);
        Task AddAsync(Note note);
        Task DeleteAsync(Note note);
    }
}
