using System;
using System.Linq;
using HospitalSystem.BUS;
using HospitalSystem.Models;

namespace HospitalSystem.Menus
{
    public static class PrescriptionsMenu
    {
        public static void Show(PrescriptionBUS bus)
        {
            while (true)
            {
                Console.WriteLine("\n=== Quản Lý Đơn Thuốc ===");
                Console.WriteLine("1. Thêm đơn thuốc");
                Console.WriteLine("2. Xem danh sách đơn thuốc");
                Console.WriteLine("3. Cập nhật đơn thuốc");
                Console.WriteLine("4. Xóa đơn thuốc");
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

        private static void Add(PrescriptionBUS bus)
        {
            int appointmentId;
            while (true)
            {
                Console.Write("ID cuộc hẹn: ");
                if (int.TryParse(Console.ReadLine(), out appointmentId) && appointmentId > 0) break;
                Console.WriteLine("❌ ID cuộc hẹn không hợp lệ!");
            }

            string? medication = null;
            while (string.IsNullOrWhiteSpace(medication) || medication.Length < 3 || medication.Length > 100)
            {
                Console.Write("Tên thuốc: ");
                medication = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(medication) || medication.Length < 3 || medication.Length > 100)
                    Console.WriteLine("❌ Tên thuốc phải từ 3 đến 100 ký tự!");
            }

            string? dosage = null;
            while (string.IsNullOrWhiteSpace(dosage) || dosage.Length < 3 || dosage.Length > 50)
            {
                Console.Write("Liều lượng: ");
                dosage = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(dosage) || dosage.Length < 3 || dosage.Length > 50)
                    Console.WriteLine("❌ Liều lượng phải từ 3 đến 50 ký tự!");
            }

            string? details = null;
            while (details != null && details.Length > 255)
            {
                Console.Write("Chi tiết (nhấn Enter để bỏ qua): ");
                details = Console.ReadLine()?.Trim();
                if (details != null && details.Length > 255)
                    Console.WriteLine("❌ Chi tiết không được vượt quá 255 ký tự!");
            }

            var prescription = new Prescription
            {
                AppointmentId = appointmentId,
                Medication = medication,
                Dosage = dosage,
                Details = details ?? string.Empty // Sử dụng giá trị mặc định nếu null
            };

            try
            {
                bus.Add(prescription);
                Console.WriteLine("✅ Thêm đơn thuốc thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void View(PrescriptionBUS bus)
        {
            var list = bus.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("📭 Không có đơn thuốc.");
                return;
            }

            foreach (var p in list)
                Console.WriteLine($"ID: {p.Id}, ID cuộc hẹn: {p.AppointmentId}, Thuốc: {p.Medication}, Liều lượng: {p.Dosage}, Chi tiết: {p.Details}");
        }

        private static void Update(PrescriptionBUS bus)
        {
            int id;
            while (true)
            {
                Console.Write("ID cần cập nhật: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                Console.WriteLine("❌ ID không hợp lệ.");
            }

            int appointmentId;
            while (true)
            {
                Console.Write("ID cuộc hẹn mới: ");
                if (int.TryParse(Console.ReadLine(), out appointmentId) && appointmentId > 0) break;
                Console.WriteLine("❌ ID cuộc hẹn không hợp lệ!");
            }

            string? medication = null;
            while (string.IsNullOrWhiteSpace(medication) || medication.Length < 3 || medication.Length > 100)
            {
                Console.Write("Tên thuốc mới: ");
                medication = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(medication) || medication.Length < 3 || medication.Length > 100)
                    Console.WriteLine("❌ Tên thuốc phải từ 3 đến 100 ký tự!");
            }

            string? dosage = null;
            while (string.IsNullOrWhiteSpace(dosage) || dosage.Length < 3 || dosage.Length > 50)
            {
                Console.Write("Liều lượng mới: ");
                dosage = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(dosage) || dosage.Length < 3 || dosage.Length > 50)
                    Console.WriteLine("❌ Liều lượng phải từ 3 đến 50 ký tự!");
            }

            string? details = null;
            while (details != null && details.Length > 255)
            {
                Console.Write("Chi tiết mới (nhấn Enter để bỏ qua): ");
                details = Console.ReadLine()?.Trim();
                if (details != null && details.Length > 255)
                    Console.WriteLine("❌ Chi tiết không được vượt quá 255 ký tự!");
            }

            var prescription = new Prescription
            {
                Id = id,
                AppointmentId = appointmentId,
                Medication = medication,
                Dosage = dosage,
                Details = details ?? string.Empty // Sử dụng giá trị mặc định nếu null
            };

            try
            {
                bus.Update(prescription);
                Console.WriteLine("✅ Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void Delete(PrescriptionBUS bus)
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
                Console.WriteLine("✅ Đã xóa đơn thuốc!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}