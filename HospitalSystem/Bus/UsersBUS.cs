using HospitalSystem.Models;
       using HospitalSystem.Repositories;
       using System;
       using System.Text.RegularExpressions;
       using System.Linq;

       namespace HospitalSystem.BUS
       {
           /// <summary>
           /// Business logic for managing User entities (e.g., Nurse).
           /// </summary>
           public class UserBUS
           {
               private readonly UserRepository _userRepository;

               public UserBUS(UserRepository repository)
               {
                   _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
               }

               public void Add(User user)
               {
                   if (user == null)
                       throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Người dùng"));

                   if (string.IsNullOrWhiteSpace(user.FullName) || user.FullName.Length < 3 || user.FullName.Length > 50)
                       throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                   if (!Regex.IsMatch(user.FullName, @"^[\p{L}\s]+$"))
                       throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                   if (string.IsNullOrWhiteSpace(user.Role) || !new[] { "Nurse" }.Contains(user.Role))
                       throw new ArgumentException("Vai trò phải là Nurse.");

                   if (string.IsNullOrWhiteSpace(user.Email) || !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                       throw new ArgumentException(ErrorMessages.InvalidEmailFormat);

                   if (_userRepository.ExistsByEmail(user.Email))
                       throw new ArgumentException(ErrorMessages.EmailExists);

                   _userRepository.Add(user);
               }

               public void Update(User user)
               {
                   if (user == null)
                       throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Người dùng"));

                   if (!_userRepository.Exists(user.Id))
                       throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Người dùng", user.Id));

                   if (string.IsNullOrWhiteSpace(user.FullName) || user.FullName.Length < 3 || user.FullName.Length > 50)
                       throw new ArgumentException(ErrorMessages.InvalidFullNameLength);
                   if (!Regex.IsMatch(user.FullName, @"^[\p{L}\s]+$"))
                       throw new ArgumentException(ErrorMessages.InvalidFullNameFormat);

                   if (string.IsNullOrWhiteSpace(user.Role) || !new[] { "Nurse" }.Contains(user.Role))
                       throw new ArgumentException("Vai trò phải là Nurse.");

                   if (string.IsNullOrWhiteSpace(user.Email) || !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                       throw new ArgumentException(ErrorMessages.InvalidEmailFormat);

                   if (_userRepository.ExistsByEmailForOther(user.Email, user.Id))
                       throw new ArgumentException(string.Format(ErrorMessages.EmailExistsForOther, "người dùng"));

                   _userRepository.Update(user);
               }

               public void Delete(int id)
               {
                   if (!_userRepository.Exists(id))
                       throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Người dùng", id));

                   _userRepository.Delete(id);
               }

               public System.Collections.Generic.List<User> GetAll()
               {
                   return _userRepository.GetAll();
               }
           }
       }