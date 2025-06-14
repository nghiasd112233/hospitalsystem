using HospitalSystem.Models;
using HospitalSystem.Repositories;
using System;
using System.Collections.Generic;

namespace HospitalSystem.BUS
{
    /// <summary>
    /// Business logic for managing Prescription entities.
    /// </summary>
    public class PrescriptionBUS
    {
        private readonly PrescriptionRepository _prescriptionRepository;
        private readonly AppointmentRepository _appointmentRepository;

        public PrescriptionBUS(PrescriptionRepository prescriptionRepo, AppointmentRepository appointmentRepo)
        {
            _prescriptionRepository = prescriptionRepo ?? throw new ArgumentNullException(nameof(prescriptionRepo));
            _appointmentRepository = appointmentRepo ?? throw new ArgumentNullException(nameof(appointmentRepo));
        }

        public void Add(Prescription prescription)
        {
            if (prescription == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Đơn thuốc"));

            if (!_appointmentRepository.AppointmentExists(prescription.AppointmentId))
                throw new ArgumentException(ErrorMessages.InvalidAppointmentId);

            if (string.IsNullOrWhiteSpace(prescription.Medication) || prescription.Medication.Length < 3 || prescription.Medication.Length > 100)
                throw new ArgumentException(ErrorMessages.InvalidMedicationLength);

            if (string.IsNullOrWhiteSpace(prescription.Dosage) || prescription.Dosage.Length < 3 || prescription.Dosage.Length > 50)
                throw new ArgumentException(ErrorMessages.InvalidDosageLength);

            _prescriptionRepository.Add(prescription);
        }

        public void Update(Prescription prescription)
        {
            if (prescription == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Đơn thuốc"));

            if (!_prescriptionRepository.Exists(prescription.Id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Đơn thuốc", prescription.Id));

            if (!_appointmentRepository.AppointmentExists(prescription.AppointmentId))
                throw new ArgumentException(ErrorMessages.InvalidAppointmentId);

            if (string.IsNullOrWhiteSpace(prescription.Medication) || prescription.Medication.Length < 3 || prescription.Medication.Length > 100)
                throw new ArgumentException(ErrorMessages.InvalidMedicationLength);

            if (string.IsNullOrWhiteSpace(prescription.Dosage) || prescription.Dosage.Length < 3 || prescription.Dosage.Length > 50)
                throw new ArgumentException(ErrorMessages.InvalidDosageLength);

            _prescriptionRepository.Update(prescription);
        }

        public void Delete(int id)
        {
            if (!_prescriptionRepository.Exists(id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Đơn thuốc", id));

            _prescriptionRepository.Delete(id);
        }

        public List<Prescription> GetAll()
        {
            return _prescriptionRepository.GetAll();
        }
    }
}