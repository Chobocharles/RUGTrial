namespace RUGTrial.Models
{
    public class User
    {
        public string Gender { get; set; }

        public Name Name { get; set; }

        public Address Location { get; set; }

        public string Email { get; set; }

        public Profile Login { get; set; }

        public BirthDate BirthDate { get; set; }

        public Registration Registration { get; set; }

        public string Phone { get; set; }

        public string Cell { get; set; }

        public Identity Identity { get; set; }

        public Picture Picture { get; set; }

        public string Nat { get; set; } = "US";
    }
}
