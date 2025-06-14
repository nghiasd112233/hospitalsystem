using System;
  using HospitalSystem.BUS;
  using HospitalSystem.Models;

  namespace HospitalSystem.Menus
  {
      public static class ReceptionistsMenu
      {
          public static void Show(ReceptionistBUS bus)
          {
              while (true)
              {
                  Console.WriteLine("\n=== Quản Lý Lễ Tân ===");
                  Console.WriteLine("1. Thêm lễ tân");
                  Console.WriteLine("2. Xem danh sách lễ tân");
                  Console.WriteLine("3. Cập nhật thông tin lễ tân");
                  Console.WriteLine("4. Xóa lễ tân");
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
                      default: Console.WriteLine("❌ Lựa chọn không hợp lệ!"); break;
                  }
              }
          }

          private static void Add(ReceptionistBUS bus)
          {
              string? fullName = null;
              while (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
              {
                  Console.Write("Tên lễ tân: ");
                  fullName = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
                      Console.WriteLine("❌ Tên phải từ 3 đến 50 ký tự và chỉ chứa chữ cái!");
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
              while (string.IsNullOrWhiteSpace(email))
              {
                  Console.Write("Email: ");
                  email = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(email))
                      Console.WriteLine("❌ Email không được để trống!");
              }

              var receptionist = new Receptionist
              {
                  FullName = fullName,
                  Phone = phone,
                  Email = email
              };

              bus.Add(receptionist);
              Console.WriteLine("✅ Thêm lễ tân thành công!");
          }

          private static void View(ReceptionistBUS bus)
          {
              var list = bus.GetAll();
              if (list.Count == 0)
              {
                  Console.WriteLine("📭 Không có lễ tân nào.");
                  return;
              }

              foreach (var r in list)
                  Console.WriteLine($"ID: {r.Id}, Tên: {r.FullName}, SĐT: {r.Phone}, Email: {r.Email}");
          }

          private static void Update(ReceptionistBUS bus)
          {
              int id;
              while (true)
              {
                  Console.Write("ID cần cập nhật: ");
                  if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                  Console.WriteLine("❌ ID không hợp lệ!");
              }

              string? fullName = null;
              while (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
              {
                  Console.Write("Tên mới: ");
                  fullName = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
                      Console.WriteLine("❌ Tên phải từ 3 đến 50 ký tự và chỉ chứa chữ cái!");
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
              while (string.IsNullOrWhiteSpace(email))
              {
                  Console.Write("Email mới: ");
                  email = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(email))
                      Console.WriteLine("❌ Email không được để trống!");
              }

              var receptionist = new Receptionist
              {
                  Id = id,
                  FullName = fullName,
                  Phone = phone,
                  Email = email
              };

              bus.Update(receptionist);
              Console.WriteLine("✅ Cập nhật thành công!");
          }

          private static void Delete(ReceptionistBUS bus)
          {
              int id;
              while (true)
              {
                  Console.Write("ID cần xóa: ");
                  if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                  Console.WriteLine("❌ ID không hợp lệ!");
              }

              bus.Delete(id);
              Console.WriteLine("✅ Xóa lễ tân thành công!");
          }
      }
  }