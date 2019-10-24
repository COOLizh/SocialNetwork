using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar(30)")]
        protected string Name{ get; set;}

        [Required]
        [Display(Name = "Surname")]
        [Column(TypeName = "varchar(30)")]
        protected string Surname{ get; set;}
        protected bool Gender{ get; set;}

        [Required]
        [Display(Name = "Country")]
        [Column(TypeName = "varchar(30)")]
        protected string Country{ get; set;}

        [Required]
        [Display(Name = "Email")]
        [Column(TypeName = "varchar(100)")]
        protected string Email{ get; set;}

        [Required]
        [Display(Name = "Password")]
        [Column(TypeName = "varchar(100)")]
        protected string Password{ get; set;}

        public User(){}
        public void Construct(int id, string name, string surname, bool maleGender, string email, string password, string country){
            Id = id;
            Name = name;
            Surname = surname;
            Gender = (maleGender) ? true : false;
            Email = email;
            Password = password;
            Country = country;
        }
    }
}