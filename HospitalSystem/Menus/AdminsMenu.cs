using System;
using System.Linq;
using HospitalSystem.BUS;
using HospitalSystem.Models;

namespace HospitalSystem.Menus
{
    public static class AdminsMenu
    {
        public static void Show(AdminBUS bus)
        {
            while (true)
            {
                Console.WriteLine("\n=== Quản Lý Quản Trị Viên ===");
                Console.WriteLine("1. Thêm quản trị viên");
                Console.WriteLine("2. Xem danh sách quản trị viên");
                Console.WriteLine("3. Cập nhật quản trị viên");
                Console.WriteLine("4. Xóa quản trị viên");
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

        private static void Add(AdminBUS bus)
        {
            string? fullName = null;
            while (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
            {
                Console.Write("Tên quản trị viên: ");
                fullName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
                    Console.WriteLine("❌ Tên phải từ 3 đến 50 ký tự và chỉ chứa chữ cái!");
            }

            string? email = null;
            while (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.Write("Email: ");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    Console.WriteLine("❌ Email không hợp lệ!");
            }

            string? phone = null;
            while (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
            {
                Console.Write("Số điện thoại: ");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
                    Console.WriteLine("❌ Số điện thoại phải là 10 hoặc 11 số!");
            }

            var admin = new Admin
            {
                FullName = fullName,
                Email = email,
                Phone = phone ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Add(admin);
                Console.WriteLine("✅ Thêm quản trị viên thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void View(AdminBUS bus)
        {
            var list = bus.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("📭 Không có quản trị viên.");
                return;
            }

            foreach (var a in list)
                Console.WriteLine($"ID: {a.Id}, Tên: {a.FullName}, Email: {a.Email}, SĐT: {a.Phone}");
        }

        private static void Update(AdminBUS bus)
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

            string? email = null;
            while (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.Write("Email mới: ");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    Console.WriteLine("❌ Email không hợp lệ!");
            }

            string? phone = null;
            while (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
            {
                Console.Write("Số điện thoại mới: ");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
                    Console.WriteLine("❌ Số điện thoại phải là 10 hoặc 11 số!");
            }

            var admin = new Admin
            {
                Id = id,
                FullName = fullName,
                Email = email,
                Phone = phone ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Update(admin);
                Console.WriteLine("✅ Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void Delete(AdminBUS bus)
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
                Console.WriteLine("✅ Đã xóa quản trị viên!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}