using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Infrastructure.Data;
using DigitalNotesManager.Infrastructure.Repos.Interfaces;

namespace DigitalNotesManager.Infrastructure.Repos.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly DataContext _context;

        public CategoryRepository()
        {
            _context = new DataContext();
        }


        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

        }


        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Category> GetCategoriesByUserId(int userId)
        {
            return _context.Categories.Where(c => c.UserId == userId);
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }
    }
}
