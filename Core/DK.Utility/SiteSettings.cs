﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Utility
{
    public class SiteSettings
    {
        public string ElmahPath { get; set; }
        public JwtSettings JwtSettings { get; set; }
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
        public string  RefreshTokenExpirationMinutes { get; set; }
        public string TokenPath { get; set; }


    }
}
