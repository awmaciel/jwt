using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AWM.JWT.Models
{
    public partial class ApiUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPasswd { get; set; }
        public string UserRole { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}