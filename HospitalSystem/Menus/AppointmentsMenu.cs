using System;
using System.Linq;
using HospitalSystem.BUS;
using HospitalSystem.Models;

namespace HospitalSystem.Menus
{
    public static class AppointmentsMenu
    {
        public static void Show(AppointmentBUS bus)
        {
            while (true)
            {
                Console.WriteLine("\n=== Quản Lý Lịch Hẹn ===");
                Console.WriteLine("1. Thêm lịch hẹn");
                Console.WriteLine("2. Xem danh sách lịch hẹn");
                Console.WriteLine("3. Cập nhật lịch hẹn");
                Console.WriteLine("4. Xóa lịch hẹn");
                Console.WriteLine("5. Quay lại");
                Console.Write("Chọn: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Add(bus); break;
                    case "2": View(bus); break;
                    case "3": Update(bus); break;
                    case "4": Delete(bus); break;
                    case "5": return;
                    default: Console.WriteLine("❌ Lựa chọn không hợp lệ."); break;
                }
            }
        }

        private static void Add(AppointmentBUS bus)
        {
            int patientId;
            while (true)
            {
                Console.Write("ID bệnh nhân: ");
                if (int.TryParse(Console.ReadLine(), out patientId) && patientId > 0) break;
                Console.WriteLine("❌ ID bệnh nhân không hợp lệ!");
            }

            int doctorId;
            while (true)
            {
                Console.Write("ID bác sĩ: ");
                if (int.TryParse(Console.ReadLine(), out doctorId) && doctorId > 0) break;
                Console.WriteLine("❌ ID bác sĩ không hợp lệ!");
            }

            DateTime appointmentDate;
            while (true)
            {
                Console.Write("Ngày hẹn (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out appointmentDate) && appointmentDate >= DateTime.Now) break;
                Console.WriteLine("❌ Ngày hẹn phải từ hôm nay trở đi!");
            }

            string? status = null;
            while (string.IsNullOrWhiteSpace(status) || !new[] { "Scheduled", "Completed", "Canceled" }.Contains(status))
            {
                Console.Write("Trạng thái (Scheduled/Completed/Canceled): ");
                status = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(status) || !new[] { "Scheduled", "Completed", "Canceled" }.Contains(status))
                    Console.WriteLine("❌ Trạng thái phải là Scheduled, Completed hoặc Canceled!");
            }

            var appointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                Status = status
            };

            try
            {
                bus.Add(appointment);
                Console.WriteLine("✅ Thêm lịch hẹn thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void View(AppointmentBUS bus)
        {
            var list = bus.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("📭 Không có lịch hẹn.");
                return;
            }

            foreach (var a in list)
                Console.WriteLine($"ID: {a.Id}, Bệnh nhân ID: {a.PatientId}, Bác sĩ ID: {a.DoctorId}, Ngày: {a.AppointmentDate:yyyy-MM-dd HH:mm}, Trạng thái: {a.Status}");
        }

        private static void Update(AppointmentBUS bus)
        {
            int id;
            while (true)
            {
                Console.Write("ID cần cập nhật: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                Console.WriteLine("❌ ID không hợp lệ.");
            }

            int patientId;
            while (true)
            {
                Console.Write("ID bệnh nhân mới: ");
                if (int.TryParse(Console.ReadLine(), out patientId) && patientId > 0) break;
                Console.WriteLine("❌ ID bệnh nhân không hợp lệ!");
            }

            int doctorId;
            while (true)
            {
                Console.Write("ID bác sĩ mới: ");
                if (int.TryParse(Console.ReadLine(), out doctorId) && doctorId > 0) break;
                Console.WriteLine("❌ ID bác sĩ không hợp lệ!");
            }

            DateTime appointmentDate;
            while (true)
            {
                Console.Write("Ngày hẹn mới (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out appointmentDate) && appointmentDate >= DateTime.Now) break;
                Console.WriteLine("❌ Ngày hẹn phải từ hôm nay trở đi!");
            }

            string? status = null;
            while (string.IsNullOrWhiteSpace(status) || !new[] { "Scheduled", "Completed", "Canceled" }.Contains(status))
            {
                Console.Write("Trạng thái mới (Scheduled/Completed/Canceled): ");
                status = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(status) || !new[] { "Scheduled", "Completed", "Canceled" }.Contains(status))
                    Console.WriteLine("❌ Trạng thái phải là Scheduled, Completed hoặc Canceled!");
            }

            var appointment = new Appointment
            {
                Id = id,
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                Status = status
            };

            try
            {
                bus.Update(appointment);
                Console.WriteLine("✅ Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void Delete(AppointmentBUS bus)
        {
            int id;
            while (true)
            {
                Console.Write("ID cần xóa: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                Console.WriteLine("❌ ID không hợp lệ.");
            }

            try
            {
                bus.Delete(id);
                Console.WriteLine("✅ Đã xóa lịch hẹn!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}