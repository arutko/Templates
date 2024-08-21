using Microsoft.EntityFrameworkCore;
using Template.Api.Controllers.Models;
using Template.Api.Controllers.Models.Base;
using Template.Api.Controllers.Services.Contracts.Identity;

namespace Template.Api.Controllers.DatabaseContext;

public class AppDbContext : DbContext
{
    private readonly IUserService _userService;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IUserService userService) : base(options)
    {
        _userService = userService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<Entity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.UtcNow;
            entry.Entity.ModifiedBy = _userService.UserId;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
                entry.Entity.CreatedBy = _userService.UserId;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Person> Persons => Set<Person>();
}
