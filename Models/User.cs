using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Name{ get; set;}

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Surname{ get; set;}
        //protected bool Gender{ get; set;}

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Country{ get; set;}

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email{ get; set;}

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Password{ get; set;}

        public User(){}
        public void Construct(int id, string name, string surname, bool maleGender, string email, string password, string country){
            Id = id;
            Name = name;
            Surname = surname;
            //Gender = (maleGender) ? true : false;
            Email = email;
            Password = password;
            Country = country;
        }
    }
}