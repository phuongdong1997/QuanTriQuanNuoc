using Microsoft.AspNetCore.Mvc;
using QuanTriQuanNuoc.Controllers;
using QuanTriQuanNuoc.Data;
using QuanTriQuanNuoc.Entites;
using QuanTriQuanNuoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuanTriQuanNuoc.Test
{
    public class CategoryControllerTest
    {
        private ApplicationDbContext _context;

        public CategoryControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext();
        }

        [Fact]
        public void Should_Create_Instance_Not_Null_Success()
        {
            var controller = new CategoryController(_context);
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task PostCatefory_ValidInput_Success()
        {
            var categoryController = new CategoryController(_context);
            var result = await categoryController.PostCategory(new CategoryCreateRequest()
            {
                Id = 1,
                ParentId = null,
                Name = "Category_ValidInput_Success",
                SortOrder = 5,
                SeoAlias = "Ca-phe-den",
                SeoDescription = "Ca-phe-den-thom-lung"
            });

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PostFunction_ValidInput_Failed()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    Id = 1,
                    ParentId = null,
                    Name = "Category_ValidInput_Success",
                    SortOrder = 5,
                    SeoAlias = "Ca-phe-den",
                    SeoDescription = "Ca-phe-den-thom-lung"
                }
            });
            await _context.SaveChangesAsync();
            var categoriesController = new CategoryController(_context);

            var result = await categoriesController.PostCategory(new CategoryCreateRequest()
            {
                Id = 1,
                ParentId = null,
                Name = "Category_ValidInput_Success",
                SortOrder = 5,
                SeoAlias = "Ca-phe-den",
                SeoDescription = "Ca-phe-den-thom-lung"
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetCategory_HasData_ReturnSuccess()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                     Id = 1,
                    ParentId = null,
                    Name = "Category_ValidInput_Success",
                    SortOrder = 5,
                    SeoAlias = "Ca-phe-den",
                    SeoDescription = "Ca-phe-den-thom-lung"
                }
            });
            await _context.SaveChangesAsync();
            var categoriesController = new CategoryController(_context);
            var result = await categoriesController.GetAllCategory();
            var okResult = result as OkObjectResult;
            var CategoryVms = okResult.Value as IEnumerable<CategoryVm>;
            Assert.True(CategoryVms.Count() > 0);
        }

        [Fact]
        public async Task GetCategoryPaging_NoFilter_ReturnSuccess()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    Id = 1,
                    ParentId = null,
                    Name = "GetCategoryPaging_NoFilter_ReturnSuccess1",
                    SortOrder =1,
                    SeoAlias = "Ca-phe-den",
                    SeoDescription = "Ca-phe-den-thom-lung"
                },
                 new Category(){
                    Id = 2,
                    ParentId = null,
                    Name = "GetCategoryPaging_NoFilter_ReturnSuccess2",
                    SortOrder =2,
                    SeoAlias = "Ca-phe-den-2",
                    SeoDescription = "Ca-phe-den-thom-lung-2"
                },
                  new Category(){
                    Id = 3,
                    ParentId = null,
                    Name = "GetCategoryPaging_NoFilter_ReturnSuccess3",
                    SortOrder =3,
                    SeoAlias = "Ca-phe-den-3",
                    SeoDescription = "Ca-phe-den-thom-lung-3"
                },
                   new Category(){
                    Id = 4,
                    ParentId = null,
                    Name = "GetCategoryPaging_NoFilter_ReturnSuccess4",
                    SortOrder =4,
                    SeoAlias = "Ca-phe-den-4",
                    SeoDescription = "Ca-phe-den-thom-lung-4"
                }
            });
            await _context.SaveChangesAsync();
            var categoriesController = new CategoryController(_context);
            var result = await categoriesController.GetCategoyPaging(null, 1, 2);
            var okResult = result as OkObjectResult;
            var categoriesVms = okResult.Value as Pagination<CategoryVm>;
            Assert.Equal(4, categoriesVms.TotalRecords);
            Assert.Equal(2, categoriesVms.Items.Count);
        }

        [Fact]
        public async Task GetCategoryPaging_HasFilter_ReturnSuccess()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    Id = 1,
                    ParentId = null,
                    Name = "GetCategoryPaging_HasFilter_ReturnSuccess",
                    SortOrder = 3,
                    SeoAlias = "Ca-phe-den",
                    SeoDescription = "Ca-phe-den-thom-lung"
                }
            });
            await _context.SaveChangesAsync();

            var cagororiesController = new CategoryController(_context);
            var result = await cagororiesController.GetCategoyPaging("GetCategoryPaging_HasFilter_ReturnSuccess", 1, 2);
            var okResult = result as OkObjectResult;
            var CategoryVms = okResult.Value as Pagination<CategoryVm>;
            Assert.Equal(1, CategoryVms.TotalRecords);
            Assert.Single(CategoryVms.Items);
        }

        [Fact]
        public async Task GetById_HasData_ReturnSuccess()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    Id = 1,
                    ParentId = null,
                    Name = "GetById_HasData_ReturnSuccess",
                    SortOrder =1,
                    SeoAlias = "Ca-phe-den",
                    SeoDescription = "Ca-phe-den-thom-lung"
                }
            });
            await _context.SaveChangesAsync();
            var categoryController = new CategoryController(_context);
            var result = await categoryController.GetById(1);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var categoryVm = okResult.Value as CategoryVm;
            Assert.Equal(1, categoryVm.Id);
        }

        [Fact]
        public async Task PutCategory_ValidInput_Success()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    Id = 1,
                    ParentId = null,
                    Name = "PutCategory_ValidInput_Success",
                    SortOrder =1,
                    SeoAlias = "Ca-phe-den",
                    SeoDescription = "Ca-phe-den-thom-lung"
                }
            });
            await _context.SaveChangesAsync();
            var categoryController = new CategoryController(_context);
            var result = await categoryController.PutCategory(1, new CategoryCreateRequest()
            {
                ParentId = null,
                Name = "PutCategory_ValidInput_Success updated",
                SortOrder = 6,
                SeoAlias = "Ca-phe-den-6",
                SeoDescription = "Ca-phe-den-thom-lung-6"
            });
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutCategory_ValidInput_Failed()
        {
            var categoryController = new CategoryController(_context);

            var result = await categoryController.PutCategory(1, new CategoryCreateRequest()
            {
                ParentId = null,
                Name = "PutUser_ValidInput_Failed update",
                SortOrder = 6,
                SeoAlias = "Ca-phe-den",
                SeoDescription = "Ca-phe-den-thom-lung"
            });
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ValidInput_Success()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    Id =1,
                    ParentId = null,
                    Name = "DeleteCategory_ValidInput_Success",
                    SortOrder =1,
                    SeoAlias = "Ca-phe-den",
                    SeoDescription = "Ca-phe-den-thom-lung"
                }
            });
            await _context.SaveChangesAsync();
            var categoryController = new CategoryController(_context);
            var result = await categoryController.DeleteCategory(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ValidInput_Failed()
        {
            var categoryController = new CategoryController(_context);
            var result = await categoryController.DeleteCategory(1);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}