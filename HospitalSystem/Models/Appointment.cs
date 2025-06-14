using System;

namespace HospitalSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; } // ✅ Tên thuộc tính chính xác

        public required string Status { get; set; } // Scheduled, Completed, Canceled
    }
}

