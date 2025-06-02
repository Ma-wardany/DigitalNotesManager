using DigitalNotesManager.Domain.DTOs;
using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Helpers;
using DigitalNotesManager.Infrastructure.Repos.Interfaces;
using DigitalNotesManager.Infrastructure.Repos.Repository;
using DigitalNotesManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalNotesManager.Services.ServiceImp
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
            _userRepository = new UserRepository();
        }

        public async Task<Response<CategoryDto>> AddCategory(Category category)
        {
            var user = _userRepository.getBbyIdAsync(category.UserId);
            if (user == null)
                return Response<CategoryDto>.Failure("User not found");


            var existingCategory = await _categoryRepository.GetCategoriesByUserId(category.UserId)
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == category.Name.ToLower());


            if (existingCategory != null)
                return Response<CategoryDto>.Failure("Category already exists for this user");

            try
            {
                await _categoryRepository.AddCategoryAsync(category);

                CategoryDto categoryDto = new CategoryDto
                {
                    CategoryId = category.Id,
                    Name       = category.Name,
                };
                
                return Response<CategoryDto>.Success(categoryDto, "Category added successfully");
            }
            catch (Exception ex)
            {
                return Response<CategoryDto>.Failure($"An error occurred while adding the category: {ex.Message}");
            }
        }


        public async Task<Response<List<CategoryDto>>> GetCategoriesByUserId(int userId)
        {
            var user = _userRepository.getBbyIdAsync(userId);

            if (user == null)
                return Response<List<CategoryDto>>.Failure("User not found");

            try
            {
                var categories = _categoryRepository.GetCategoriesByUserId(userId);

                var CategoryDtos = categories.Select(c => new CategoryDto
                {
                    CategoryId = c.Id,
                    Name       = c.Name,
                }).ToList();


                if (CategoryDtos.Count == 0)
                    return Response<List<CategoryDto>>.Failure("No categories found for this user");


                return Response<List<CategoryDto>>.Success(CategoryDtos, "Categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return Response<List<CategoryDto>>.Failure($"An error occurred while retrieving categories: {ex.Message}");
            }
            
        }

        public async Task<Response<string>> DeleteCategory(int categoryId, int userId)
        {
            var user = _userRepository.getBbyIdAsync(userId);

            if (user == null)
                return Response<string>.Failure("User not found");


            var category = await _categoryRepository.GetCategoriesByUserId(userId)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                return Response<string>.Failure("Category not found for this user");


            try
            {
                await _categoryRepository.DeleteCategoryAsync(category);
                return Response<string>.Success("Category deleted successfully");
            }
            catch (Exception ex)
            {
                return Response<string>.Failure($"An error occurred while deleting the category: {ex.Message}");
            }


        }

    }
}
