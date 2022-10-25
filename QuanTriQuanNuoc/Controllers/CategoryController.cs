using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanTriQuanNuoc.Data;
using QuanTriQuanNuoc.Entites;
using QuanTriQuanNuoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanTriQuanNuoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryCreateRequest request)
        {
            var dbCategory = await _context.Categories.FindAsync(request.Id);
            if (dbCategory != null)
                return BadRequest($"Category with id {request.Id} is existed.");

            var category = new Category()
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId,
                SortOrder = request.SortOrder,
                SeoAlias = request.SeoAlias,
                SeoDescription = request.SeoDescription
            };
            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, request);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryCreateRequest request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            category.Name = request.Name;
            category.ParentId = request.ParentId;
            category.SortOrder = request.SortOrder;
            category.SeoAlias = request.SeoAlias;
            category.SeoDescription = request.SeoDescription;

            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var categoryVm = new CategoryVm()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId,
                    SortOrder = category.SortOrder,
                    SeoAlias = category.SeoAlias,
                    SeoDescription = category.SeoDescription
                };
                return Ok(categoryVm);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = _context.Categories;

            var categoryvms = await categories.Select(u => new CategoryVm()
            {
                Id = u.Id,
                Name = u.Name,
                SortOrder = u.SortOrder,
                ParentId = u.ParentId,
                SeoAlias = u.SeoAlias,
                SeoDescription = u.SeoDescription
            }).ToListAsync();

            return Ok(categoryvms);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetCategoyPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                int temp = 0;
                if (int.TryParse(filter, out temp))
                {
                    query = query.Where(x => x.Id == temp || x.Name.Contains(filter));
                }
                else
                {
                    query = query.Where(x => x.Name.Contains(filter));
                }
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1 * pageSize))
                .Take(pageSize)
                .Select(u => new CategoryVm()
                {
                    Id = u.Id,
                    Name = u.Name,
                    SortOrder = u.SortOrder,
                    ParentId = u.ParentId,
                    SeoAlias = u.SeoAlias,
                    SeoDescription = u.SeoDescription
                })
                .ToListAsync();

            var pagination = new Pagination<CategoryVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            var categoryVm = new CategoryVm()
            {
                Id = category.Id,
                Name = category.Name,
                SeoAlias = category.SeoAlias,
                SeoDescription = category.SeoDescription,
                SortOrder = category.SortOrder,
                ParentId = category.ParentId
            };
            return Ok(categoryVm);
        }
    }
}