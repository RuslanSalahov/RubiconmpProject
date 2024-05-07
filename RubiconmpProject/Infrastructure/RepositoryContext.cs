using Microsoft.EntityFrameworkCore;
using RubiconmpProject.Models;
using System.Collections.Generic;

namespace RubiconmpProject.Infrastructure
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
         : base(options) { }
        public DbSet<Rectangle> Rectangles { get; set; }
    }
}
