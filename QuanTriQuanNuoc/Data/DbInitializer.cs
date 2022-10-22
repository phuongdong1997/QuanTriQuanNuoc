using Microsoft.AspNetCore.Identity;
using QuanTriQuanNuoc.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanTriQuanNuoc.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string AdminRoleName = "Admin";
        private readonly string UserRoleName = "Member";

        public DbInitializer(ApplicationDbContext context,
          UserManager<User> userManager,
          RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            #region Quyền

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = AdminRoleName,
                    Name = AdminRoleName,
                    NormalizedName = AdminRoleName.ToUpper(),
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = UserRoleName,
                    Name = UserRoleName,
                    NormalizedName = UserRoleName.ToUpper(),
                });
            }

            #endregion Quyền

            #region Người dùng

            if (!_userManager.Users.Any())
            {
                var result = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    FirstName = "Quản trị",
                    LastName = "1",
                    Email = "tedu.international@gmail.com",
                    LockoutEnabled = false
                }, "Admin@123");
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("admin");
                    await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
            }

            #endregion Người dùng

            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(new List<Category>
                {
                    new Category { Name = "Cà phê",SeoAlias="ca-phe" , SortOrder = 1},
                    new Category { Name = "Nước ngọt",SeoAlias="nuoc-ngot" , SortOrder = 2},
                    new Category { Name = "Nước mát",SeoAlias="nuoc-mat" , SortOrder = 3},
                    new Category { Name = "Nước trái cây",SeoAlias="nuoc-trai-cay" , SortOrder = 4},
                    new Category { Name = "Sinh tố",SeoAlias="sinh-to" , SortOrder = 5},
                    new Category { Name = "Trà sữa",SeoAlias="tra-sua" , SortOrder = 6}
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Products.Any())
            {
                _context.Products.AddRange(new List<Product>
                {
                    new Product { Name = "Cà phê đen",SeoAlias="ca-phe-den",Price=15000,Description="Cà phê hương chồn đậm đà thơm lừng",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=1 ,CreateDate=DateTime.Now, SortOrder = 1},
                    new Product { Name = "Cà phê sữa",SeoAlias="ca-phe-sua",Price=20000,Description="Cà phê hương chồn đậm đà thơm lừng + sữa ngôi sao đậm đà",CongThuc="2 chung sữa + 2 thìa cà phê",Unit="ly",Status=true,CategoryId=1 ,CreateDate=DateTime.Now, SortOrder = 2},
                    new Product { Name = "Cappuccino",SeoAlias="ca-phe-cappuccino",Price=30000,Description="Cà phê hương chồn đậm đà thơm lừng",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=1 ,CreateDate=DateTime.Now, SortOrder = 3},
                    new Product { Name = "Cà phê đen 4",SeoAlias="ca-phe-den-4",Price=15000,Description="Cà phê hương chồn đậm đà thơm lừng 4",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=1 ,CreateDate=DateTime.Now, SortOrder = 4},
                    new Product { Name = "Cà phê đen 5",SeoAlias="ca-phe-den-5",Price=25000,Description="Cà phê hương chồn đậm đà thơm lừng 5",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=1 ,CreateDate=DateTime.Now, SortOrder = 5},
                    new Product { Name = "Cà phê đen 6",SeoAlias="ca-phe-den-6",Price=35000,Description="Cà phê hương chồn đậm đà thơm lừng 7",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=1 ,CreateDate=DateTime.Now, SortOrder = 6},
                    new Product { Name = "Cà phê đen 7",SeoAlias="ca-phe-den-7",Price=45000,Description="Cà phê hương chồn đậm đà thơm lừng 7",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=2 ,CreateDate=DateTime.Now, SortOrder = 7},
                    new Product { Name = "Cà phê đen 8",SeoAlias="ca-phe-den-8",Price=55000,Description="Cà phê hương chồn đậm đà thơm lừng 8",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=2 ,CreateDate=DateTime.Now, SortOrder = 8},
                    new Product { Name = "Cà phê đen 9",SeoAlias="ca-phe-den-9",Price=65000,Description="Cà phê hương chồn đậm đà thơm lừng 9",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=2 ,CreateDate=DateTime.Now, SortOrder = 9},
                    new Product { Name = "Cà phê đen 10",SeoAlias="ca-phe-den-10",Price=75000,Description="Cà phê hương chồn đậm đà thơm lừng 10",CongThuc="2 muỗng đường + 3 thìa cà phê",Unit="ly",Status=true,CategoryId=2 ,CreateDate=DateTime.Now, SortOrder = 10},
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}