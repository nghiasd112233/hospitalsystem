using HospitalSystem.Models;
     using HospitalSystem.Repositories;
     using System;
     using System.Text.RegularExpressions;
     using System.Linq;

     namespace HospitalSystem.BUS
     {
         /// <summary>
         /// Business logic for managing Patient entities.
         /// </summary>
         public class PatientBUS
         {
             private readonly PatientRepository _patientRepository;

             public PatientBUS(PatientRepository repository)
             {
                 _patientRepository = repository ?? throw new ArgumentNullException(nameof(repository));
             }

             public void Add(Patient patient)
             {
                 if (patient == null)
                     throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Bệnh nhân"));

                 if (string.IsNullOrWhiteSpace(patient.FullName) || patient.FullName.Length < 3 || patient.FullName.Length > 50)
                     throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                 if (!Regex.IsMatch(patient.FullName, @"^[\p{L}\s]+$"))
                     throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                 if (patient.BirthDate < new DateTime(1900, 1, 1) || patient.BirthDate > DateTime.Now)
                     throw new ArgumentException(ErrorMessages.InvalidBirthDate);

                 if (string.IsNullOrWhiteSpace(patient.Gender) || !new[] { "Nam", "Nữ" }.Contains(patient.Gender))
                     throw new ArgumentException(ErrorMessages.InvalidGender);

                 _patientRepository.Add(patient);
             }

             public void Update(Patient patient)
             {
                 if (patient == null)
                     throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Bệnh nhân"));

                 if (!_patientRepository.Exists(patient.Id))
                     throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Bệnh nhân", patient.Id));

                 if (string.IsNullOrWhiteSpace(patient.FullName) || patient.FullName.Length < 3 || patient.FullName.Length > 50)
                     throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                 if (!Regex.IsMatch(patient.FullName, @"^[\p{L}\s]+$"))
                     throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                 if (patient.BirthDate < new DateTime(1900, 1, 1) || patient.BirthDate > DateTime.Now)
                     throw new ArgumentException(ErrorMessages.InvalidBirthDate);

                 if (string.IsNullOrWhiteSpace(patient.Gender) || !new[] { "Nam", "Nữ" }.Contains(patient.Gender))
                     throw new ArgumentException(ErrorMessages.InvalidGender);

                 _patientRepository.Update(patient);
             }

             public void Delete(int id)
             {
                 if (!_patientRepository.Exists(id))
                     throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Bệnh nhân", id));

                 _patientRepository.Delete(id);
             }

             public System.Collections.Generic.List<Patient> GetAll()
             {
                 return _patientRepository.GetAll();
             }
         }
     }