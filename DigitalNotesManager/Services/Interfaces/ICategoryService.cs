using DigitalNotesManager.Domain.DTOs;
using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<Response<CategoryDto>> AddCategory(Category category);
        public Task<Response<List<CategoryDto>>> GetCategoriesByUserId(int userId);
        public Task<Response<string>> DeleteCategory(int categoryId, int userId);


        //public Task<CategoryDto> UpdateCategory(int categoryId);
    }
}
