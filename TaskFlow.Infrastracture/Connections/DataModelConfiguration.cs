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
                //entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
                //entity.Property(u => u.UpdatedAt).IsRequired(false);

                //entity.HasMany(u => u.Tasks)
                //      .WithOne(t => t.User)
                //      .HasForeignKey(t => t.UserId)
                //      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
