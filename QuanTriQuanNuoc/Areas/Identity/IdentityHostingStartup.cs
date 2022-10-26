using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanTriQuanNuoc.Data;
using QuanTriQuanNuoc.Entites;

[assembly: HostingStartup(typeof(QuanTriQuanNuoc.Areas.Identity.IdentityHostingStartup))]
namespace QuanTriQuanNuoc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}