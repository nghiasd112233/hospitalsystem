using System;
using System.Linq;
using HospitalSystem.BUS;
using HospitalSystem.Models;

namespace HospitalSystem.Menus
{
    public static class DoctorsMenu
    {
        public static void Show(DoctorBUS bus)
        {
            while (true)
            {
                Console.WriteLine("\n=== Quản Lý Bác Sĩ ===");
                Console.WriteLine("1. Thêm bác sĩ");
                Console.WriteLine("2. Xem danh sách bác sĩ");
                Console.WriteLine("3. Cập nhật bác sĩ");
                Console.WriteLine("4. Xóa bác sĩ");
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

        private static void Add(DoctorBUS bus)
        {
            string? fullName = null;
            while (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
            {
                Console.Write("Tên bác sĩ: ");
                fullName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
                    Console.WriteLine("❌ Tên phải từ 3 đến 50 ký tự và chỉ chứa chữ cái!");
            }

            string? specialty = null;
            while (specialty != null && specialty.Length > 100)
            {
                Console.Write("Chuyên môn (nhấn Enter để bỏ qua): ");
                specialty = Console.ReadLine()?.Trim();
                if (specialty != null && specialty.Length > 100)
                    Console.WriteLine("❌ Chuyên môn không được vượt quá 100 ký tự!");
            }

            string? phone = null;
            while (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
            {
                Console.Write("Số điện thoại: ");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
                    Console.WriteLine("❌ Số điện thoại phải là 10 hoặc 11 số!");
            }

            string? email = null;
            while (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.Write("Email: ");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    Console.WriteLine("❌ Email không hợp lệ!");
            }

            var doctor = new Doctor
            {
                FullName = fullName,
                Specialty = specialty ?? string.Empty, // Sử dụng giá trị mặc định nếu null
                Phone = phone ?? string.Empty,
                Email = email ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Add(doctor);
                Console.WriteLine("✅ Thêm bác sĩ thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void View(DoctorBUS bus)
        {
            var list = bus.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("📭 Không có bác sĩ.");
                return;
            }

            foreach (var d in list)
                Console.WriteLine($"ID: {d.Id}, Tên: {d.FullName}, Chuyên môn: {d.Specialty}, SĐT: {d.Phone}, Email: {d.Email}");
        }

        private static void Update(DoctorBUS bus)
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

            string? specialty = null;
            while (specialty != null && specialty.Length > 100)
            {
                Console.Write("Chuyên môn mới (nhấn Enter để bỏ qua): ");
                specialty = Console.ReadLine()?.Trim();
                if (specialty != null && specialty.Length > 100)
                    Console.WriteLine("❌ Chuyên môn không được vượt quá 100 ký tự!");
            }

            string? phone = null;
            while (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
            {
                Console.Write("Số điện thoại mới: ");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
                    Console.WriteLine("❌ Số điện thoại phải là 10 hoặc 11 số!");
            }

            string? email = null;
            while (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.Write("Email mới: ");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    Console.WriteLine("❌ Email không hợp lệ!");
            }

            var doctor = new Doctor
            {
                Id = id,
                FullName = fullName,
                Specialty = specialty ?? string.Empty, // Sử dụng giá trị mặc định nếu null
                Phone = phone ?? string.Empty,
                Email = email ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Update(doctor);
                Console.WriteLine("✅ Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void Delete(DoctorBUS bus)
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
                Console.WriteLine("✅ Đã xóa bác sĩ!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}