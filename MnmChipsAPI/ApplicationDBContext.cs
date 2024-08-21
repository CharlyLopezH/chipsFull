using Microsoft.EntityFrameworkCore;
using MnmChipsAPI.Entidades;

namespace MnmChipsAPI
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Prestador>().Property(p => p.Foto).IsUnicode(true);
            modelBuilder.Entity<Prestador>().ToTable(t => t.HasTrigger("trg_AfterInsertPrestador"));
        }
        public DbSet<Prestador> Prestadores { get; set; }
    }
}