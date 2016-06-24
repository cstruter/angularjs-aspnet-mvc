using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSTruter.Models
{
    public class PersonViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required]
        public int Gender { get; set; }

        public int? Age { get; set; }
    }
}