namespace G4.Domain.Entities {
    public class User {

        public Guid Identifier { get; set; }

        public required string Username { get; set; }
        public required string Password { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }
        public DateTime EmailConfirmationDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastLoginAt { get; set; }


    }
}
