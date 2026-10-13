using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZooCon.Entities;

namespace ZooCon
{
    public class ZooDbContext: DbContext
    {
        public ZooDbContext(DbContextOptions<ZooDbContext> options)
          : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Zoo> Zoos { get; set; }
        public DbSet<Caretaker> Caretakers { get; set; }
        public DbSet<AnimalCaretaker> AnimalCaretakers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AnimalCaretaker>().HasKey(dp => new { dp.AnimalId, dp.CaretakerId });
            modelBuilder.Entity<AnimalCaretaker>()
                .HasOne(dp => dp.Animal)
                .WithMany(d => d.AnimalCaretakers)
                .HasForeignKey(dp => dp.AnimalId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AnimalCaretaker>()
             .HasOne(dp => dp.Caretaker)
             .WithMany(d => d.AnimalCaretakers)
             .HasForeignKey(dp => dp.CaretakerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
