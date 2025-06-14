namespace HospitalSystem.Models
       {
           public class Receptionist
           {
               public int Id { get; set; }

               public required string FullName { get; set; } = string.Empty;

               public required string Phone { get; set; } = string.Empty;

               public required string Email { get; set; } = string.Empty;
           }
       }