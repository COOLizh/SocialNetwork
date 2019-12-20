using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Collections.Generic;

namespace SocialNetwork.Models
{
    public class UsersContext : IdentityDbContext<User>
    {
        public DbSet<FriendRequests> FriendsRequests {get; set;}
        //public DbSet<Messages> Messages {get; set;}
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

        public void SendFriendsRequest(string from, string to){
            FriendRequests[] frnd = FriendsRequests.Where(i => i.From == from && i.To == to).ToArray();
            if (frnd.Length == 0){
                FriendRequests req = new FriendRequests {
                    From = from,
                    To = to,
                    isConfirmed = false,
                };
                FriendsRequests.Add(req);
                SaveChanges();
            }
        }

        public void AcceptFriendsRequest(string from, string to){
            FriendRequests[] frnd = FriendsRequests.Where(i => i.From == from && i.To == to).ToArray();
            if (frnd.Length != 0 && !frnd[0].isConfirmed){
                frnd[0].isConfirmed = true;
                FriendsRequests.Update(frnd[0]);
                SaveChanges();
            }
        }

        public List<FriendRequests> GetFriends(string client){
            List<FriendRequests> frnd = FriendsRequests.Where(i => (i.From == client && i.isConfirmed == true) 
            || (i.To == client && i.isConfirmed == true)).ToList();
            return frnd;
        }

        public List<FriendRequests> GetRequests(string client){
            List<FriendRequests> frnd = FriendsRequests.Where(i => (i.From == client && i.isConfirmed == false) 
            || (i.To == client && i.isConfirmed == false)).ToList();
            return frnd;
        }
    }
}