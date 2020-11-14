using medic_api.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace medic_api.DAL
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<MedicalData> MedicalDataset { get; set; }
    }
}