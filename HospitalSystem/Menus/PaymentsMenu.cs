using System;
using System.Linq;
using HospitalSystem.BUS;
using HospitalSystem.Models;

namespace HospitalSystem.Menus
{
    public static class PaymentsMenu
    {
        public static void Show(PaymentBUS bus)
        {
            while (true)
            {
                Console.WriteLine("\n=== Quản Lý Thanh Toán ===");
                Console.WriteLine("1. Thêm thanh toán");
                Console.WriteLine("2. Xem danh sách thanh toán");
                Console.WriteLine("3. Cập nhật thanh toán");
                Console.WriteLine("4. Xóa thanh toán");
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

        private static void Add(PaymentBUS bus)
        {
            int patientId;
            while (true)
            {
                Console.Write("ID bệnh nhân: ");
                if (int.TryParse(Console.ReadLine(), out patientId) && patientId > 0) break;
                Console.WriteLine("❌ ID bệnh nhân không hợp lệ!");
            }

            decimal amount;
            while (true)
            {
                Console.Write("Số tiền: ");
                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0) break;
                Console.WriteLine("❌ Số tiền phải lớn hơn 0!");
            }

            DateTime paymentDate;
            while (true)
            {
                Console.Write("Ngày thanh toán (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out paymentDate) && paymentDate >= new DateTime(1900, 1, 1) && paymentDate <= DateTime.Now) break;
                Console.WriteLine("❌ Ngày thanh toán phải từ năm 1900 đến hiện tại!");
            }

            string? paymentMethod = null;
            while (string.IsNullOrWhiteSpace(paymentMethod) || !new[] { "Tiền mặt", "Thẻ tín dụng", "Chuyển khoản" }.Contains(paymentMethod))
            {
                Console.Write("Phương thức (Tiền mặt/Thẻ tín dụng/Chuyển khoản): ");
                paymentMethod = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(paymentMethod) || !new[] { "Tiền mặt", "Thẻ tín dụng", "Chuyển khoản" }.Contains(paymentMethod))
                    Console.WriteLine("❌ Phương thức phải là Tiền mặt, Thẻ tín dụng hoặc Chuyển khoản!");
            }

            var payment = new Payment
            {
                PatientId = patientId,
                Amount = amount,
                PaymentDate = paymentDate,
                PaymentMethod = paymentMethod ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Add(payment);
                Console.WriteLine("✅ Thêm thanh toán thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void View(PaymentBUS bus)
        {
            var list = bus.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("📭 Không có thanh toán.");
                return;
            }

            foreach (var p in list)
                Console.WriteLine($"ID: {p.Id}, ID bệnh nhân: {p.PatientId}, Số tiền: {p.Amount}, Ngày: {p.PaymentDate:yyyy-MM-dd}, Phương thức: {p.PaymentMethod}");
        }

        private static void Update(PaymentBUS bus)
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

            decimal amount;
            while (true)
            {
                Console.Write("Số tiền mới: ");
                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0) break;
                Console.WriteLine("❌ Số tiền phải lớn hơn 0!");
            }

            DateTime paymentDate;
            while (true)
            {
                Console.Write("Ngày thanh toán mới (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out paymentDate) && paymentDate >= new DateTime(1900, 1, 1) && paymentDate <= DateTime.Now) break;
                Console.WriteLine("❌ Ngày thanh toán phải từ năm 1900 đến hiện tại!");
            }

            string? paymentMethod = null;
            while (string.IsNullOrWhiteSpace(paymentMethod) || !new[] { "Tiền mặt", "Thẻ tín dụng", "Chuyển khoản" }.Contains(paymentMethod))
            {
                Console.Write("Phương thức mới (Tiền mặt/Thẻ tín dụng/Chuyển khoản): ");
                paymentMethod = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(paymentMethod) || !new[] { "Tiền mặt", "Thẻ tín dụng", "Chuyển khoản" }.Contains(paymentMethod))
                    Console.WriteLine("❌ Phương thức phải là Tiền mặt, Thẻ tín dụng hoặc Chuyển khoản!");
            }

            var payment = new Payment
            {
                Id = id,
                PatientId = patientId,
                Amount = amount,
                PaymentDate = paymentDate,
                PaymentMethod = paymentMethod ?? string.Empty // Đảm bảo required property được khởi tạo
            };

            try
            {
                bus.Update(payment);
                Console.WriteLine("✅ Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }

        private static void Delete(PaymentBUS bus)
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
                Console.WriteLine("✅ Đã xóa thanh toán!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}