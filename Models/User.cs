using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        [Key]
        override public string Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [Column(TypeName = "varchar(6)")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Country")]
        [Column(TypeName = "varchar(30)")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "BirthDay")]
        [Column(TypeName = "varchar(10)")]
        public string BirthDay {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Column(TypeName = "varchar(30)")]
        override public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(32)")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}