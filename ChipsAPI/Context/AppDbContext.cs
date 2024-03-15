using ChipsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChipsAPI.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        //*** README ***
        //Una vez implementada la clase que define la tabla; aquí se pueden agregar indices y sus tipos (unique o repetible)
        //Así como el nombre que se utilizará en la base de datos.
        //Configuración del DbSet; IMPORTANTE esto es necesario previo a realizar la migración de la BD y posterior update.
        //Con esto se creará la migración x ( PM> Add-Migration x) y se podrá actualizar la misma BD (PM>Update-Database)
        public DbSet<Prestador> Prestadores { get; set;}
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            modelBuilder.Entity<Prestador>()
            .HasIndex(p => p.Email)
            .IsUnique();

        }

    }
}
