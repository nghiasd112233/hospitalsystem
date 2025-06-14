using System;

namespace HospitalSystem.Models
{
    /// <summary>
    /// Represents a Medical Record in the hospital system.
    /// </summary>
    public class MedicalRecord
    {
        public int Id { get; set; } // Unique identifier

        public int PatientId { get; set; } // Foreign key to Patient

        public int DoctorId { get; set; } // Foreign key to Doctor

        public required string Diagnosis { get; set; } // Diagnosis, 3-500 characters

        public required string Treatment { get; set; } // Treatment plan, required

        public DateTime RecordDate { get; set; } // Date of record
    }
}
