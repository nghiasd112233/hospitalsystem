using System;
using System.Linq;
using HospitalSystem.BUS;
using HospitalSystem.Models;

namespace HospitalSystem.Menus
{
    public static class PatientsMenu
    {
        public static void Show(PatientBUS bus)
        {
            while (true)
            {
                Console.WriteLine("\n=== Quản Lý Bệnh Nhân ===");
                Console.WriteLine("1. Thêm bệnh nhân");
                Console.WriteLine("2. Xem danh sách bệnh nhân");
                Console.WriteLine("3. Cập nhật bệnh nhân");
                Console.WriteLine("4. Xóa bệnh nhân");
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

        private static void Add(PatientBUS bus)
        {
            string? fullName = null;
            while (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
            {
                Console.Write("Tên bệnh nhân: ");
                fullName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
                    Console.WriteLine("❌ Tên phải từ 3 đến 50 ký tự và chỉ chứa chữ cái!");
            }

            DateTime birthDate;
            while (true)
            {
                Console.Write("Ngày sinh (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate) && birthDate <= DateTime.Now && birthDate >= new DateTime(1900, 1, 1)) break;
                Console.WriteLine("❌ Ngày sinh phải từ năm 1900 đến hiện tại!");
            }

            string? gender = null;
            while (string.IsNullOrWhiteSpace(gender) || !new[] { "Nam", "Nữ" }.Contains(gender))
            {
                Console.Write("Giới tính (Nam/Nữ): ");
                gender = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(gender) || !new[] { "Nam", "Nữ" }.Contains(gender))
                    Console.WriteLine("❌ Giới tính phải là Nam hoặc Nữ!");
            }

            string? phone = null;
            while (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
            {
                Console.Write("Số điện thoại: ");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
                    Console.WriteLine("❌ Số điện thoại phải là 10 hoặc 11 số!");
            }

            var patient = new Patient
            {
                FullName = fullName,
                BirthDate = birthDate,
                Gender = gender,
                Phone = phone ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Add(patient);
                Console.WriteLine("✅ Thêm bệnh nhân thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void View(PatientBUS bus)
        {
            var list = bus.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("📭 Không có bệnh nhân.");
                return;
            }

            foreach (var p in list)
                Console.WriteLine($"ID: {p.Id}, Tên: {p.FullName}, Ngày sinh: {p.BirthDate:yyyy-MM-dd}, Giới tính: {p.Gender}, SĐT: {p.Phone}");
        }

        private static void Update(PatientBUS bus)
        {
            int id;
            while (true)
            {
                Console.Write("ID cần cập nhật: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                Console.WriteLine("❌ ID không hợp lệ.");
            }

            string? fullName = null;
            while (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
            {
                Console.Write("Tên mới: ");
                fullName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
                    Console.WriteLine("❌ Tên phải từ 3 đến 50 ký tự và chỉ chứa chữ cái!");
            }

            DateTime birthDate;
            while (true)
            {
                Console.Write("Ngày sinh mới (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate) && birthDate <= DateTime.Now && birthDate >= new DateTime(1900, 1, 1)) break;
                Console.WriteLine("❌ Ngày sinh phải từ năm 1900 đến hiện tại!");
            }

            string? gender = null;
            while (string.IsNullOrWhiteSpace(gender) || !new[] { "Nam", "Nữ" }.Contains(gender))
            {
                Console.Write("Giới tính mới (Nam/Nữ): ");
                gender = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(gender) || !new[] { "Nam", "Nữ" }.Contains(gender))
                    Console.WriteLine("❌ Giới tính phải là Nam hoặc Nữ!");
            }

            string? phone = null;
            while (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
            {
                Console.Write("Số điện thoại mới: ");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
                    Console.WriteLine("❌ Số điện thoại phải là 10 hoặc 11 số!");
            }

            var patient = new Patient
            {
                Id = id,
                FullName = fullName,
                BirthDate = birthDate,
                Gender = gender,
                Phone = phone ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Update(patient);
                Console.WriteLine("✅ Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void Delete(PatientBUS bus)
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
                Console.WriteLine("✅ Đã xóa bệnh nhân!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}