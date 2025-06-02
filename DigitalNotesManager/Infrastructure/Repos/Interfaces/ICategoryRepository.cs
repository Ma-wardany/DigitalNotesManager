using DigitalNotesManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Infrastructure.Repos.Interfaces
{
    public interface ICategoryRepository
    {
        public Task AddCategoryAsync(Category category);

        public Task DeleteCategoryAsync(Category category);

        public IQueryable<Category> GetCategoriesByUserId(int userId);

        public Task<Category> GetCategoryByIdAsync(int categoryId);
    }
}
