using System;

       namespace HospitalSystem.Models
       {
           /// <summary>
           /// Represents a Patient in the hospital system.
           /// </summary>
           public class Patient
           {
               public int Id { get; set; } // Unique identifier

               public required string FullName { get; set; } // Full name, 3-50 characters

               public required DateTime BirthDate { get; set; } // Date of birth

               public required string Gender { get; set; } // Nam or Nữ

               public string? Phone { get; set; } // Optional phone number
           }
       }