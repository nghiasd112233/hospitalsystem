namespace HospitalSystem.Models
{
    /// <summary>
    /// Represents a user in the system (used for nurses).
    /// </summary>
    public class User
    {
        public int Id { get; set; } // Unique identifier

        public required string FullName { get; set; } // Full name of the user

        public required string Email { get; set; } // Email of the user

        public required string Role { get; set; } // Example: "Nurse"
    }
}
