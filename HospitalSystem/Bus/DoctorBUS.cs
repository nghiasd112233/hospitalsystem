using HospitalSystem.Models;
     using HospitalSystem.Repositories;
     using System;
     using System.Text.RegularExpressions;

     namespace HospitalSystem.BUS
     {
         /// <summary>
         /// Business logic for managing Doctor entities.
         /// </summary>
         public class DoctorBUS
         {
             private readonly DoctorRepository _doctorRepository;

             public DoctorBUS(DoctorRepository repository)
             {
                 _doctorRepository = repository ?? throw new ArgumentNullException(nameof(repository));
             }

             public void Add(Doctor doctor)
             {
                 if (doctor == null)
                     throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Bác sĩ"));

                 if (string.IsNullOrWhiteSpace(doctor.FullName) || doctor.FullName.Length < 3 || doctor.FullName.Length > 50)
                     throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                 if (!Regex.IsMatch(doctor.FullName, @"^[\p{L}\s]+$"))
                     throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                 if (!string.IsNullOrEmpty(doctor.Specialty) && doctor.Specialty.Length > 100)
                     throw new ArgumentException(ErrorMessages.InvalidSpecialtyLength);

                 if (!string.IsNullOrEmpty(doctor.Phone) && !Regex.IsMatch(doctor.Phone, @"^[0-9]{10,11}$"))
                     throw new ArgumentException(ErrorMessages.InvalidPhoneLength);

                 if (!string.IsNullOrEmpty(doctor.Phone) && _doctorRepository.ExistsByPhone(doctor.Phone))
                     throw new ArgumentException(ErrorMessages.PhoneExists);

                 _doctorRepository.Add(doctor);
             }

             public void Update(Doctor doctor)
             {
                 if (doctor == null)
                     throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Bác sĩ"));

                 if (!_doctorRepository.Exists(doctor.Id))
                     throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Bác sĩ", doctor.Id));

                 if (string.IsNullOrWhiteSpace(doctor.FullName) || doctor.FullName.Length < 3 || doctor.FullName.Length > 50)
                     throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                 if (!Regex.IsMatch(doctor.FullName, @"^[\p{L}\s]+$"))
                     throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                 if (!string.IsNullOrEmpty(doctor.Specialty) && doctor.Specialty.Length > 100)
                     throw new ArgumentException(ErrorMessages.InvalidSpecialtyLength);

                 if (!string.IsNullOrEmpty(doctor.Phone) && !Regex.IsMatch(doctor.Phone, @"^[0-9]{10,11}$"))
                     throw new ArgumentException(ErrorMessages.InvalidPhoneLength);

                 if (!string.IsNullOrEmpty(doctor.Phone) && _doctorRepository.ExistsByPhoneForOther(doctor.Phone, doctor.Id))
                     throw new ArgumentException(string.Format(ErrorMessages.PhoneExistsForOther, "bác sĩ"));

                 _doctorRepository.Update(doctor);
             }

             public void Delete(int id)
             {
                 if (!_doctorRepository.Exists(id))
                     throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Bác sĩ", id));

                 _doctorRepository.Delete(id);
             }

             public System.Collections.Generic.List<Doctor> GetAll()
             {
                 return _doctorRepository.GetAll();
             }
         }
     }