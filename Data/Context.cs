using Microsoft.EntityFrameworkCore;
using ParkingApp.Models;

namespace ParkingApp.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}
        public DbSet<ParkingValues> Users {get; set;}
    }
}