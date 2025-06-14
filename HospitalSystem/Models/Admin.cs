namespace HospitalSystem.Models
{
    /// <summary>
    /// Represents an Admin in the hospital system.
    /// </summary>
    public class Admin
    {
        public int Id { get; set; } // Unique identifier

        public required string FullName { get; set; } // Full name, 3-50 characters

        public required string Phone { get; set; } // Phone number, 10-11 digits

        public required string Email { get; set; } // ✅ Thêm Email để tương thích với menu
    }
}

