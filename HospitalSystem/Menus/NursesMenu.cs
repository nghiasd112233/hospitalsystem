using System;
  using System.Linq; // Bắt buộc để dùng .Where
  using HospitalSystem.BUS;
  using HospitalSystem.Models;

  namespace HospitalSystem.Menus
  {
      public static class NursesMenu
      {
          public static void Show(UserBUS nurseBUS)
          {
              while (true)
              {
                  Console.WriteLine("\n=== Quản Lý Điều Dưỡng ===");
                  Console.WriteLine("1. Thêm điều dưỡng");
                  Console.WriteLine("2. Xem danh sách điều dưỡng");
                  Console.WriteLine("3. Cập nhật điều dưỡng");
                  Console.WriteLine("4. Xóa điều dưỡng");
                  Console.WriteLine("5. Quay lại");
                  Console.Write("Chọn: ");

                  var choice = Console.ReadLine();
                  switch (choice)
                  {
                      case "1": Add(nurseBUS); break;
                      case "2": View(nurseBUS); break;
                      case "3": Update(nurseBUS); break;
                      case "4": Delete(nurseBUS); break;
                      case "5": return;
                      default: Console.WriteLine("❌ Lựa chọn không hợp lệ."); break;
                  }
              }
          }

          private static void Add(UserBUS bus)
          {
              string? fullName = null;
              while (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
              {
                  Console.Write("Tên điều dưỡng: ");
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

              var nurse = new User
              {
                  FullName = fullName,
                  Email = email,
                  Role = "Nurse"
              };

              bus.Add(nurse);
              Console.WriteLine("✅ Thêm điều dưỡng thành công!");
          }

          private static void View(UserBUS bus)
          {
              var nurses = bus.GetAll().Where(u => u.Role == "Nurse").ToList();
              if (nurses.Count == 0)
              {
                  Console.WriteLine("📭 Không có điều dưỡng.");
                  return;
              }

              foreach (var n in nurses)
                  Console.WriteLine($"ID: {n.Id}, Tên: {n.FullName}, Email: {n.Email}");
          }

          private static void Update(UserBUS bus)
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

              var nurse = new User
              {
                  Id = id,
                  FullName = fullName,
                  Email = email,
                  Role = "Nurse"
              };

              bus.Update(nurse);
              Console.WriteLine("✅ Cập nhật thành công!");
          }

          private static void Delete(UserBUS bus)
          {
              int id;
              while (true)
              {
                  Console.Write("ID cần xóa: ");
                  if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                  Console.WriteLine("❌ ID không hợp lệ.");
              }

              bus.Delete(id);
              Console.WriteLine("✅ Đã xóa điều dưỡng!");
          }
      }
  }