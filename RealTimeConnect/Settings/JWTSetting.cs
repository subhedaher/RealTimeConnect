﻿namespace RealTimeConnect.Settings
{
    public class JWTSetting
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirePerHour { get; set; }
    }
}
