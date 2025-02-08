using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entity;

namespace TaskFlow.Infrastracture.Connections
{
    public static class DataModelConfiguration
    {
        public static void ConfigureModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();

                entity.HasMany(u => u.Tasks)
                      .WithOne(t => t.User)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                      .IsRequired();

                entity.Property(t => t.Description)
                      .IsRequired(false);

                entity.Property(t => t.DueDate)
                      .IsRequired();

                entity.Property(t => t.Status)
                      .IsRequired();

                entity.HasOne(t => t.User)
                      .WithMany(u => u.Tasks)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                //entity.HasOne(t => t.Category)
                //      .WithMany(c => c.Tasks)
                //      .HasForeignKey(t => t.CategoryId)
                //      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
