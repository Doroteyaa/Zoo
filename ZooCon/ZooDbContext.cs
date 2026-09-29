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
    }
}
