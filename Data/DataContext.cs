using Microsoft.EntityFrameworkCore;
using OOAD.Models;

namespace OOAD.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Equipments> Equipments { get; set; }
        public DbSet<EquipmentsReserve> EquipmentsReserve { get; set; }
        public DbSet<EquipmentsRent> EquipmentsRent { get; set; }
    }
}