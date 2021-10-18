using System;
using System.Collections.Generic;

#nullable disable

namespace LoginApi.Models
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
