﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Login
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
