using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Name{ get; set;}

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Surname{ get; set;}

        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Gender{ get; set;}

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Country{ get; set;}

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email{ get; set;}

        [Required]
        [Column(TypeName = "varchar(32)")]
        public string Password{ get; set;}

        public User(){}
        public void Construct(string name, string surname, string gender, string email, string password, string country){
            Name = name;
            Surname = surname;
            Gender = gender;
            Email = email;
            Password = password;
            Country = country;
        }
    }
}