using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.Models
{
    public class UsersContext : IdentityDbContext<User>
    {
        public DbSet<FriendRequests> FriendsRequests {get; set;}
        public DbSet<Message> Messages {get; set;}
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

        public string getDialogueId(string from, string to){
            string tmp = from + to;
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, tmp);
            }
        }

        public bool isDialogueExists(string dialogueId){
            Message[] msg = Messages.Where(i => i.DialogId == dialogueId).ToArray();
            if(msg.Length == 0){
                return false;
            }
            return true;
        }

        public List<Message> GetDialogue(string dialogueId)
        {
            return Messages.Where(i => i.DialogId == dialogueId).ToList();
        }

        public void SendMessage(string msg, string dialogueId, string from)
        {
            Message mes = new Message();
            mes.DialogId = dialogueId;
            mes.Text = msg;
            mes.From = from;
            Messages.Add(mes);
            SaveChanges(); 
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}