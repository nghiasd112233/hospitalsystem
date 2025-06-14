namespace HospitalSystem.Models
{
    /// <summary>
    /// Represents a Prescription in the hospital system.
    /// </summary>
    public class Prescription
    {
        public int Id { get; set; } // Unique identifier

        public int AppointmentId { get; set; } // Foreign key to Appointment

        public required string Medication { get; set; } // Medication name, 3-100 characters

        public required string Dosage { get; set; } // Dosage, 3-50 characters

        public string Details { get; set; } = string.Empty; // Optional additional instructions
    }
}