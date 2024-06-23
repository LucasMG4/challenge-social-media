using Microsoft.EntityFrameworkCore;

namespace G4.Infraestructure.Context {
    public class AppDataContext : DbContext {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

        }
    }
}
