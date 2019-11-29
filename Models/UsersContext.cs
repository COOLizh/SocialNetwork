using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SocialNetwork.Models
{
    public class UsersContext : IdentityDbContext<User>
    {
        //public DbSet<User> SUsers { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var property in entityType.GetProperties())
                    if (property.ClrType == typeof(bool) || property.ClrType == typeof(Boolean))
                        property.SetValueConverter(new BoolToZeroOneConverter<Int16>());
                    else if (property.ClrType == typeof(Nullable<bool>) || property.ClrType == typeof(Nullable<Boolean>))
                        property.SetValueConverter(new BoolToZeroOneConverter<Nullable<Int16>>());
        }
    }
}