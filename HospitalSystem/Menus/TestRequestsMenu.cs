using System;
  using HospitalSystem.BUS;
  using HospitalSystem.Models;

  namespace HospitalSystem.Menus
  {
      public static class TestRequestsMenu
      {
          public static void Show(TestRequestBUS bus)
          {
              while (true)
              {
                  Console.WriteLine("\n=== Quản Lý Yêu Cầu Xét Nghiệm ===");
                  Console.WriteLine("1. Thêm yêu cầu");
                  Console.WriteLine("2. Xem danh sách yêu cầu");
                  Console.WriteLine("3. Cập nhật yêu cầu");
                  Console.WriteLine("4. Xóa yêu cầu");
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

          private static void Add(TestRequestBUS bus)
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
                  Console.Write("ID bác sĩ yêu cầu: ");
                  if (int.TryParse(Console.ReadLine(), out doctorId) && doctorId > 0) break;
                  Console.WriteLine("❌ ID bác sĩ không hợp lệ!");
              }

              string? testName = null;
              while (string.IsNullOrWhiteSpace(testName) || testName.Length < 3 || testName.Length > 100)
              {
                  Console.Write("Tên xét nghiệm: ");
                  testName = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(testName) || testName.Length < 3 || testName.Length > 100)
                      Console.WriteLine("❌ Tên xét nghiệm phải từ 3 đến 100 ký tự!");
              }

              string? status = null;
              while (string.IsNullOrWhiteSpace(status) || Array.IndexOf(new[] { "Pending", "Completed", "Canceled" }, status) == -1)
              {
                  Console.Write("Trạng thái (Pending/Completed/Canceled): ");
                  status = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(status) || Array.IndexOf(new[] { "Pending", "Completed", "Canceled" }, status) == -1)
                      Console.WriteLine("❌ Trạng thái phải là Pending, Completed hoặc Canceled!");
              }

              DateTime requestDate;
              while (true)
              {
                  Console.Write("Ngày yêu cầu (yyyy-MM-dd): ");
                  if (DateTime.TryParse(Console.ReadLine(), out requestDate)) break;
                  Console.WriteLine("❌ Ngày không hợp lệ!");
              }

              var request = new TestRequest
              {
                  PatientId = patientId,
                  DoctorId = doctorId,
                  TestName = testName,
                  Status = status,
                  RequestDate = requestDate
              };

              bus.Add(request);
              Console.WriteLine("✅ Thêm yêu cầu thành công!");
          }

          private static void View(TestRequestBUS bus)
          {
              var list = bus.GetAll();
              if (list.Count == 0)
              {
                  Console.WriteLine("📭 Không có yêu cầu nào.");
                  return;
              }

              foreach (var r in list)
              {
                  Console.WriteLine($"ID: {r.Id}, Bệnh nhân ID: {r.PatientId}, Bác sĩ ID: {r.DoctorId}, Tên xét nghiệm: {r.TestName}, Trạng thái: {r.Status}, Ngày: {r.RequestDate:yyyy-MM-dd}");
              }
          }

          private static void Update(TestRequestBUS bus)
          {
              int id;
              while (true)
              {
                  Console.Write("ID cần cập nhật: ");
                  if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                  Console.WriteLine("❌ ID không hợp lệ!");
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

              string? testName = null;
              while (string.IsNullOrWhiteSpace(testName) || testName.Length < 3 || testName.Length > 100)
              {
                  Console.Write("Tên xét nghiệm mới: ");
                  testName = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(testName) || testName.Length < 3 || testName.Length > 100)
                      Console.WriteLine("❌ Tên xét nghiệm phải từ 3 đến 100 ký tự!");
              }

              string? status = null;
              while (string.IsNullOrWhiteSpace(status) || Array.IndexOf(new[] { "Pending", "Completed", "Canceled" }, status) == -1)
              {
                  Console.Write("Trạng thái mới (Pending/Completed/Canceled): ");
                  status = Console.ReadLine()?.Trim();
                  if (string.IsNullOrWhiteSpace(status) || Array.IndexOf(new[] { "Pending", "Completed", "Canceled" }, status) == -1)
                      Console.WriteLine("❌ Trạng thái phải là Pending, Completed hoặc Canceled!");
              }

              DateTime requestDate;
              while (true)
              {
                  Console.Write("Ngày yêu cầu mới (yyyy-MM-dd): ");
                  if (DateTime.TryParse(Console.ReadLine(), out requestDate)) break;
                  Console.WriteLine("❌ Ngày không hợp lệ!");
              }

              var request = new TestRequest
              {
                  Id = id,
                  PatientId = patientId,
                  DoctorId = doctorId,
                  TestName = testName,
                  Status = status,
                  RequestDate = requestDate
              };

              bus.Update(request);
              Console.WriteLine("✅ Cập nhật yêu cầu thành công!");
          }

          private static void Delete(TestRequestBUS bus)
          {
              int id;
              while (true)
              {
                  Console.Write("ID cần xóa: ");
                  if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
                  Console.WriteLine("❌ ID không hợp lệ!");
              }

              bus.Delete(id);
              Console.WriteLine("✅ Xóa yêu cầu thành công!");
          }
      }
  } 