using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuanTriQuanNuoc.Entites
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        public DateTime? BirthDay { set; get; }

        public bool Status { get; set; }

        [MaxLength(255)]
        public string Avatar { get; set; }
    }
}