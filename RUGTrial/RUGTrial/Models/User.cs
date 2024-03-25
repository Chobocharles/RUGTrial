using System.Text.Json.Serialization;

namespace RUGTrial.Models
{
    public class User
    {
        public string? Gender { get; set; }

        public Name? Name { get; set; }

        public Address? Location { get; set; }

        public string? Email { get; set; }

        public Profile? Login { get; set; }

        [JsonPropertyName("dob")]
        public BirthDate? BirthDate { get; set; }

        [JsonPropertyName("registered")]
        public Registration? Registration { get; set; }

        public string? Phone { get; set; }

        public string? Cell { get; set; }

        [JsonPropertyName("id")]
        public Identity? Identity { get; set; }

        public Picture? Picture { get; set; }

        public string? Nat { get; set; } = "us";
    }
}
