namespace HospitalSystem.Models
{
    /// <summary>
    /// Represents a Doctor in the hospital system.
    /// </summary>
    public class Doctor
    {
        public int Id { get; set; }

        public required string FullName { get; set; }

        public required string Email { get; set; }

        public string Specialty { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
