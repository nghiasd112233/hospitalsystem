using HospitalSystem.Models;
       using HospitalSystem.Repositories;
       using System;
       using System.Text.RegularExpressions;

       namespace HospitalSystem.BUS
       {
           /// <summary>
           /// Business logic for managing Receptionist entities.
           /// </summary>
           public class ReceptionistBUS
           {
               private readonly ReceptionistRepository _receptionistRepository;

               public ReceptionistBUS(ReceptionistRepository receptionistRepository)
               {
                   _receptionistRepository = receptionistRepository ?? throw new ArgumentNullException(nameof(receptionistRepository));
               }

               public void Add(Receptionist receptionist)
               {
                   if (receptionist == null)
                       throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Lễ tân"));

                   if (string.IsNullOrWhiteSpace(receptionist.FullName) || receptionist.FullName.Length < 3 || receptionist.FullName.Length > 50)
                       throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                   if (!Regex.IsMatch(receptionist.FullName, @"^[\p{L}\s]+$"))
                       throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                   if (string.IsNullOrWhiteSpace(receptionist.Phone) || !Regex.IsMatch(receptionist.Phone, @"^[0-9]{10,11}$"))
                       throw new ArgumentException(ErrorMessages.InvalidPhoneLength);

                   if (_receptionistRepository.ExistsByPhone(receptionist.Phone))
                       throw new ArgumentException(ErrorMessages.PhoneExists);

                   _receptionistRepository.Add(receptionist);
               }

               public void Update(Receptionist receptionist)
               {
                   if (receptionist == null)
                       throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Lễ tân"));

                   if (!_receptionistRepository.Exists(receptionist.Id))
                       throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Lễ tân", receptionist.Id));

                   if (string.IsNullOrWhiteSpace(receptionist.FullName) || receptionist.FullName.Length < 3 || receptionist.FullName.Length > 50)
                       throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                   if (!Regex.IsMatch(receptionist.FullName, @"^[\p{L}\s]+$"))
                       throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                   if (string.IsNullOrWhiteSpace(receptionist.Phone) || !Regex.IsMatch(receptionist.Phone, @"^[0-9]{10,11}$"))
                       throw new ArgumentException(ErrorMessages.InvalidPhoneLength);

                   if (_receptionistRepository.ExistsByPhoneForOther(receptionist.Phone, receptionist.Id))
                       throw new ArgumentException(string.Format(ErrorMessages.PhoneExistsForOther, "lễ tân"));

                   _receptionistRepository.Update(receptionist);
               }

               public void Delete(int id)
               {
                   if (!_receptionistRepository.Exists(id))
                       throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Lễ tân", id));

                   _receptionistRepository.Delete(id);
               }

               public System.Collections.Generic.List<Receptionist> GetAll()
               {
                   return _receptionistRepository.GetAll();
               }
           }
       }