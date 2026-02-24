using Microsoft.EntityFrameworkCore;
using ProvaPratica.Domain.Entities;

namespace ProvaPratica.Infrastructure.DataAccess
{
    public class ProvaPraticaDbContext : DbContext
    {
        public ProvaPraticaDbContext(DbContextOptions<ProvaPraticaDbContext> options) : base(options) { }
      
       public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Podemos fazer indexação e os relacionamentos através do Entity Framework por aqui.

            //Especificando o nome das tabelas sem que elas sejam acessadas e possam ser alteradas.
            //modelBuilder.Entity<Tag>().ToTable("Tags");
        }
    }
}
