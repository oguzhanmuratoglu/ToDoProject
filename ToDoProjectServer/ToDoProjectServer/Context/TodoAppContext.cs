using Microsoft.EntityFrameworkCore;
using ToDoProjectServer.Model;

namespace ToDoProjectServer.Context;

public class TodoAppContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=MONSTER\SQLEXPRESS;Initial Catalog=TodoDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Todo>().HasKey(p => p.Id);
        modelBuilder.Entity<Todo>()
            .HasIndex(u => u.Work)
            .IsUnique();

        modelBuilder.Entity<Todo>().HasData(
        new Todo { Id = 1, Work = "Get to work", IsCompleted = false },
        new Todo { Id = 2, Work = "Pick up groceries", IsCompleted = false },
        new Todo { Id = 3, Work = "Go home", IsCompleted = false },
        new Todo { Id = 4, Work = "Fall asleep", IsCompleted = false },
        new Todo { Id = 5, Work = "Get up", IsCompleted = true },
        new Todo { Id = 6, Work = "Brush teeth", IsCompleted = true },
        new Todo { Id = 7, Work = "Take a shower", IsCompleted = true },
        new Todo { Id = 8, Work = "Check e-mail", IsCompleted = true },
        new Todo { Id = 9, Work = "Walk dog", IsCompleted = true });
    }
}


