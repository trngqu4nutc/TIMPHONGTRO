namespace MODEL.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NTDbContext : DbContext
    {
        public NTDbContext()
            : base("name=NTDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Img> Imgs { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Provincial> Provincials { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Money)
                .HasPrecision(18, 0);

            modelBuilder.Entity<News>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);
        }
    }
}
