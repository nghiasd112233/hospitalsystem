using HospitalSystem.Models;
     using HospitalSystem.Repositories;
     using System;
     using System.Text.RegularExpressions;

     namespace HospitalSystem.BUS
     {
         /// <summary>
         /// Business logic for managing Admin entities.
         /// </summary>
         public class AdminBUS
         {
             private readonly AdminRepository _adminRepository;

             public AdminBUS(AdminRepository repository)
             {
                 _adminRepository = repository ?? throw new ArgumentNullException(nameof(repository));
             }

             public void Add(Admin admin)
             {
                 if (admin == null)
                     throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Quản trị viên"));

                 if (string.IsNullOrWhiteSpace(admin.FullName) || admin.FullName.Length < 3 || admin.FullName.Length > 50)
                     throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                 if (!Regex.IsMatch(admin.FullName, @"^[\p{L}\s]+$"))
                     throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                 if (string.IsNullOrWhiteSpace(admin.Phone) || !Regex.IsMatch(admin.Phone, @"^[0-9]{10,11}$"))
                     throw new ArgumentException(ErrorMessages.InvalidPhoneLength);

                 if (_adminRepository.ExistsByPhone(admin.Phone))
                     throw new ArgumentException(ErrorMessages.PhoneExists);

                 _adminRepository.Add(admin);
             }

             public void Update(Admin admin)
             {
                 if (admin == null)
                     throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Quản trị viên"));

                 if (!_adminRepository.Exists(admin.Id))
                     throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Quản trị viên", admin.Id));

                 if (string.IsNullOrWhiteSpace(admin.FullName) || admin.FullName.Length < 3 || admin.FullName.Length > 50)
                     throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                 if (!Regex.IsMatch(admin.FullName, @"^[\p{L}\s]+$"))
                     throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                 if (string.IsNullOrWhiteSpace(admin.Phone) || !Regex.IsMatch(admin.Phone, @"^[0-9]{10,11}$"))
                     throw new ArgumentException(ErrorMessages.InvalidPhoneLength);

                 if (_adminRepository.ExistsByPhoneForOther(admin.Phone, admin.Id))
                     throw new ArgumentException(string.Format(ErrorMessages.PhoneExistsForOther, "quản trị viên"));

                 _adminRepository.Update(admin);
             }

             public void Delete(int id)
             {
                 if (!_adminRepository.Exists(id))
                     throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Quản trị viên", id));

                 _adminRepository.Delete(id);
             }

             public System.Collections.Generic.List<Admin> GetAll()
             {
                 return _adminRepository.GetAll();
             }
         }
     }