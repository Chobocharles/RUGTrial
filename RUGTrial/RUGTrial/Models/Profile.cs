﻿using System.Text.Json.Serialization;

namespace RUGTrial.Models
{
    public class Profile
    {
        [JsonPropertyName("uuid")]
        public Guid Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }

        public string? Md5 { get; set; }

        public string? Sha1 { get; set; }

        public string? Sha256 { get; set; }
    }
}
