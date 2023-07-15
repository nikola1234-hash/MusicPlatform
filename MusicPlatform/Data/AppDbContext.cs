using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data.Entities;
using MusicPlatform.Enums;
using MusicPlatform.Services;

namespace MusicPlatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FanBase> FanBases { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistImages> ArtistImages { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }


        public async Task<List<Song>> SearchSongs(string searchTerm)
        {
            return await Songs
                .FromSqlRaw("EXECUTE SearchSongs @p0", searchTerm)
                .ToListAsync();
        }
        public async Task<List<Song>> SearcSongsByLyrics(string value)
        {
            return await Songs
                .FromSqlRaw("EXECUTE SearchSongsByLyrics @p0", value)
                .ToListAsync();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Username)
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();

            var hashedPassword = PasswordEncrypt.HashPassword("123");

            modelBuilder.Entity<User>()
                        .HasData(new User
                        {
                            Id = Guid.NewGuid(),
                            Username = "admin",
                            Email = "admin@gmail.com",
                            Password = hashedPassword,
                            Role = Role.Admin
                        });


            modelBuilder.Entity<User>()
                     .HasData(new User
                     {
                         Id = Guid.NewGuid(),
                         Username = "user1",
                         Email = "u1@gmail.com",
                         Password = hashedPassword,
                         Role = Role.User
                     });


            modelBuilder.Entity<User>()
                     .HasData(new User
                     {
                         Id = Guid.NewGuid(),
                         Username = "user2",
                         Email = "u2@gmail.com",
                         Password = hashedPassword,
                         Role = Role.User
                     });

            modelBuilder.Entity<User>()
                     .HasData(new User
                     {
                         Id = Guid.NewGuid(),
                         Username = "user3",
                         Email = "u3@gmail.com",
                         Password = hashedPassword,
                         Role = Role.User
                     });




            modelBuilder.Entity<Song>()
                    .HasIndex(s => s.Name);

            modelBuilder.Entity<Comment>()
                        .HasIndex(c => c.UserId)
                        .IsUnique();
            modelBuilder.Entity<Song>()
                        .HasOne(s => s.Artist)
                        .WithMany(a => a.Songs)
                        .HasForeignKey(s => s.ArtistId)
                        .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity &&
                (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified
                )
            );

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).DateModified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).DateCreated = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {

            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is BaseEntity &&
                   (
                       e.State == EntityState.Added ||
                       e.State == EntityState.Modified
                   )
               );

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).DateModified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).DateCreated = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
