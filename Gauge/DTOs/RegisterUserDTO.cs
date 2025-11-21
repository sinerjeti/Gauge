using System;
using System.Collections.Generic;
using System.Text;

namespace Gauge.DTOs
{
    public class RegisterUserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Birthday { get; set; } = string.Empty;
    }
}
