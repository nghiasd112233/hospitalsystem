using System;
using System.Linq;
using HospitalSystem.BUS;
using HospitalSystem.Models;

namespace HospitalSystem.Menus
{
    public static class MedicalRecordsMenu
    {
        public static void Show(MedicalRecordBUS bus)
        {
            while (true)
            {
                Console.WriteLine("\n=== Quản Lý Hồ Sơ Bệnh Án ===");
                Console.WriteLine("1. Thêm hồ sơ bệnh án");
                Console.WriteLine("2. Xem danh sách hồ sơ bệnh án");
                Console.WriteLine("3. Cập nhật hồ sơ bệnh án");
                Console.WriteLine("4. Xóa hồ sơ bệnh án");
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

        private static void Add(MedicalRecordBUS bus)
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

            string? diagnosis = null;
            while (string.IsNullOrWhiteSpace(diagnosis) || diagnosis.Length < 3 || diagnosis.Length > 500)
            {
                Console.Write("Chẩn đoán: ");
                diagnosis = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(diagnosis) || diagnosis.Length < 3 || diagnosis.Length > 500)
                    Console.WriteLine("❌ Chẩn đoán phải từ 3 đến 500 ký tự!");
            }

            DateTime recordDate;
            while (true)
            {
                Console.Write("Ngày ghi nhận (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out recordDate) && recordDate >= new DateTime(1900, 1, 1) && recordDate <= DateTime.Now) break;
                Console.WriteLine("❌ Ngày ghi nhận phải từ năm 1900 đến hiện tại!");
            }

            string? treatment = null;
            while (string.IsNullOrWhiteSpace(treatment) || treatment.Length < 3 || treatment.Length > 500)
            {
                Console.Write("Phương pháp điều trị: ");
                treatment = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(treatment) || treatment.Length < 3 || treatment.Length > 500)
                    Console.WriteLine("❌ Phương pháp điều trị phải từ 3 đến 500 ký tự!");
            }

            var record = new MedicalRecord
            {
                PatientId = patientId,
                DoctorId = doctorId,
                Diagnosis = diagnosis,
                RecordDate = recordDate,
                Treatment = treatment // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Add(record);
                Console.WriteLine("✅ Thêm hồ sơ bệnh án thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void View(MedicalRecordBUS bus)
        {
            var list = bus.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("📭 Không có hồ sơ bệnh án.");
                return;
            }

            foreach (var r in list)
                Console.WriteLine($"ID: {r.Id}, Bệnh nhân ID: {r.PatientId}, Bác sĩ ID: {r.DoctorId}, Chẩn đoán: {r.Diagnosis}, Ngày: {r.RecordDate:yyyy-MM-dd}, Điều trị: {r.Treatment}");
        }

        private static void Update(MedicalRecordBUS bus)
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

            string? diagnosis = null;
            while (string.IsNullOrWhiteSpace(diagnosis) || diagnosis.Length < 3 || diagnosis.Length > 500)
            {
                Console.Write("Chẩn đoán mới: ");
                diagnosis = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(diagnosis) || diagnosis.Length < 3 || diagnosis.Length > 500)
                    Console.WriteLine("❌ Chẩn đoán phải từ 3 đến 500 ký tự!");
            }

            DateTime recordDate;
            while (true)
            {
                Console.Write("Ngày ghi nhận mới (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out recordDate) && recordDate >= new DateTime(1900, 1, 1) && recordDate <= DateTime.Now) break;
                Console.WriteLine("❌ Ngày ghi nhận phải từ năm 1900 đến hiện tại!");
            }

            string? treatment = null;
            while (string.IsNullOrWhiteSpace(treatment) || treatment.Length < 3 || treatment.Length > 500)
            {
                Console.Write("Phương pháp điều trị mới: ");
                treatment = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(treatment) || treatment.Length < 3 || treatment.Length > 500)
                    Console.WriteLine("❌ Phương pháp điều trị phải từ 3 đến 500 ký tự!");
            }

            var record = new MedicalRecord
            {
                Id = id,
                PatientId = patientId,
                DoctorId = doctorId,
                Diagnosis = diagnosis,
                RecordDate = recordDate,
                Treatment = treatment // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Update(record);
                Console.WriteLine("✅ Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void Delete(MedicalRecordBUS bus)
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
                Console.WriteLine("✅ Đã xóa hồ sơ bệnh án!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}