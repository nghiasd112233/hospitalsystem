using System;

       namespace HospitalSystem.Models
       {
           /// <summary>
           /// Represents a Payment in the hospital system.
           /// </summary>
           public class Payment
           {
               public int Id { get; set; } // Unique identifier

               public int PatientId { get; set; } // Foreign key to Patient

               public decimal Amount { get; set; } // Payment amount

               public required DateTime PaymentDate { get; set; } // Payment date

               public required string PaymentMethod { get; set; } // Payment method
           }
       }