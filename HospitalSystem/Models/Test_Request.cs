using System;

       namespace HospitalSystem.Models
       {
           public class TestRequest
           {
               public int Id { get; set; }

               public int PatientId { get; set; }

               public int DoctorId { get; set; }

               public required string TestName { get; set; } // tên xét nghiệm

               public required string Status { get; set; } // ví dụ: Đang xử lý, Đã hoàn thành, Đã hủy

               public required DateTime RequestDate { get; set; } // ngày yêu cầu
           }
       }