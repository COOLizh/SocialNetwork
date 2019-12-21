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

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string BirthDay {get; set;}

        override public string Email { get; set; }

        public string Photo {get; set;}

        public double PhotoLon {get; set;}
        
        public double PhotoLat {get; set;}
    }
}