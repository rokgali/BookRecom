using System;
using System.IO;
using backend.persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace migration
{
    class Program
    {
        public static void Main(string[] args) 
        {
            Console.WriteLine("Applying migrations");
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ConsoleStartup>()
                .Build();
            using (var context = (BookRecomDbContext) webHost.Services.GetService(typeof(BookRecomDbContext)))
            {
                context.Database.Migrate();
            }
            Console.WriteLine("Done");
        }
    }
}