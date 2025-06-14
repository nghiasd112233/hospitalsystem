using HospitalSystem.Models;
using HospitalSystem.Repositories;
using System;
using System.Collections.Generic;

namespace HospitalSystem.BUS
{
    /// <summary>
    /// Business logic for managing MedicalRecord entities.
    /// </summary>
    public class MedicalRecordBUS
    {
        private readonly MedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordBUS(MedicalRecordRepository repository)
        {
            _medicalRecordRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(MedicalRecord medicalRecord)
        {
            if (medicalRecord == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Hồ sơ y tế"));

            if (!_medicalRecordRepository.PatientExists(medicalRecord.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (!_medicalRecordRepository.DoctorExists(medicalRecord.DoctorId))
                throw new ArgumentException(ErrorMessages.InvalidDoctorId);

            if (string.IsNullOrWhiteSpace(medicalRecord.Diagnosis) || medicalRecord.Diagnosis.Length < 3 || medicalRecord.Diagnosis.Length > 500)
                throw new ArgumentException(ErrorMessages.InvalidDiagnosisLength);

            if (string.IsNullOrWhiteSpace(medicalRecord.Treatment) || medicalRecord.Treatment.Length < 3 || medicalRecord.Treatment.Length > 500)
                throw new ArgumentException("Phác đồ điều trị phải từ 3 đến 500 ký tự.");

            if (medicalRecord.RecordDate < new DateTime(1900, 1, 1) || medicalRecord.RecordDate > DateTime.Now)
                throw new ArgumentException(ErrorMessages.InvalidRecordDate);

            _medicalRecordRepository.Add(medicalRecord);
        }

        public void Update(MedicalRecord medicalRecord)
        {
            if (medicalRecord == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Hồ sơ y tế"));

            if (!_medicalRecordRepository.Exists(medicalRecord.Id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Hồ sơ y tế", medicalRecord.Id));

            if (!_medicalRecordRepository.PatientExists(medicalRecord.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (!_medicalRecordRepository.DoctorExists(medicalRecord.DoctorId))
                throw new ArgumentException(ErrorMessages.InvalidDoctorId);

            if (string.IsNullOrWhiteSpace(medicalRecord.Diagnosis) || medicalRecord.Diagnosis.Length < 3 || medicalRecord.Diagnosis.Length > 500)
                throw new ArgumentException(ErrorMessages.InvalidDiagnosisLength);

            if (string.IsNullOrWhiteSpace(medicalRecord.Treatment) || medicalRecord.Treatment.Length < 3 || medicalRecord.Treatment.Length > 500)
                throw new ArgumentException("Phác đồ điều trị phải từ 3 đến 500 ký tự.");

            if (medicalRecord.RecordDate < new DateTime(1900, 1, 1) || medicalRecord.RecordDate > DateTime.Now)
                throw new ArgumentException(ErrorMessages.InvalidRecordDate);

            _medicalRecordRepository.Update(medicalRecord);
        }

        public void Delete(int id)
        {
            if (!_medicalRecordRepository.Exists(id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Hồ sơ y tế", id));

            _medicalRecordRepository.Delete(id);
        }

        public List<MedicalRecord> GetAll()
        {
            return _medicalRecordRepository.GetAll();
        }
    }
}
