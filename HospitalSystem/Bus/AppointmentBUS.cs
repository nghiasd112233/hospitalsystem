using HospitalSystem.Models;
using HospitalSystem.Repositories;
using System;
using System.Linq;

namespace HospitalSystem.BUS
{
    /// <summary>
    /// Business logic for managing Appointment entities.
    /// </summary>
    public class AppointmentBUS
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentBUS(AppointmentRepository repository)
        {
            _appointmentRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Cuộc hẹn"));

            if (!_appointmentRepository.PatientExists(appointment.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (!_appointmentRepository.DoctorExists(appointment.DoctorId))
                throw new ArgumentException(ErrorMessages.InvalidDoctorId);

            if (appointment.AppointmentDate < DateTime.Today)
                throw new ArgumentException(ErrorMessages.InvalidAppointmentDate);

            if (string.IsNullOrWhiteSpace(appointment.Status) || !new[] { "Scheduled", "Completed", "Canceled" }.Contains(appointment.Status))
                throw new ArgumentException(ErrorMessages.InvalidAppointmentStatus);

            _appointmentRepository.Add(appointment);
        }

        public void Update(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Cuộc hẹn"));

            if (!_appointmentRepository.Exists(appointment.Id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Cuộc hẹn", appointment.Id));

            if (!_appointmentRepository.PatientExists(appointment.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (!_appointmentRepository.DoctorExists(appointment.DoctorId))
                throw new ArgumentException(ErrorMessages.InvalidDoctorId);

            if (appointment.AppointmentDate < DateTime.Today)
                throw new ArgumentException(ErrorMessages.InvalidAppointmentDate);

            if (string.IsNullOrWhiteSpace(appointment.Status) || !new[] { "Scheduled", "Completed", "Canceled" }.Contains(appointment.Status))
                throw new ArgumentException(ErrorMessages.InvalidAppointmentStatus);

            _appointmentRepository.Update(appointment);
        }

        public void Delete(int id)
        {
            if (!_appointmentRepository.Exists(id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Cuộc hẹn", id));

            _appointmentRepository.Delete(id);
        }

        public System.Collections.Generic.List<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }
    }
}