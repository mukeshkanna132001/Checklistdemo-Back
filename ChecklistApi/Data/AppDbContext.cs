using ChecklistApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChecklistApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ChecklistItem> ChecklistItems => Set<ChecklistItem>();
}
