using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _dbContext;

        public NoteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Note note)
        {
            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Note note)
        {
            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Note?> GetByIdAsync(int id, int userId)
        {
            return await _dbContext.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        }

        public async Task<List<Note>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Notes
               .Where(n => n.UserId == userId)
               .OrderByDescending(n => n.CreatedAt)
               .ToListAsync();
        }
    }
}
