using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }        

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoItemType> TodoItemTypes { get; set; }
        public DbSet<TodoItemList> TodoItemLists { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TodoItemType>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TodoItemList>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
