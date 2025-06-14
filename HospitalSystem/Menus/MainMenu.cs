using System;
using HospitalSystem.BUS;

namespace HospitalSystem.Menus
{
    public static class MainMenu
    {
        public static void Show(
            AdminBUS adminBUS,
            DoctorBUS doctorBUS,
            UserBUS userBUS,
            PatientBUS patientBUS,
            ReceptionistBUS receptionistBUS,
            AppointmentBUS appointmentBUS,
            PrescriptionBUS prescriptionBUS,
            MedicalRecordBUS medicalRecordBUS,
            PaymentBUS paymentBUS,
            TestRequestBUS testRequestBUS
        )
        {
            while (true)
            {
                Console.WriteLine("\n======= MENU CHÍNH =======");
                Console.WriteLine("1. Quản lý Quản trị viên");
                Console.WriteLine("2. Quản lý Bác sĩ");
                Console.WriteLine("3. Quản lý Y tá");
                Console.WriteLine("4. Quản lý Bệnh nhân");
                Console.WriteLine("5. Quản lý Lễ tân");
                Console.WriteLine("6. Quản lý Lịch hẹn");
                Console.WriteLine("7. Quản lý Đơn thuốc");
                Console.WriteLine("8. Quản lý Hồ sơ bệnh án");
                Console.WriteLine("9. Quản lý Thanh toán");
                Console.WriteLine("10. Quản lý Yêu cầu xét nghiệm");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AdminsMenu.Show(adminBUS); break;
                    case "2": DoctorsMenu.Show(doctorBUS); break;
                    case "3": NursesMenu.Show(userBUS); break;
                    case "4": PatientsMenu.Show(patientBUS); break;
                    case "5": ReceptionistsMenu.Show(receptionistBUS); break;
                    case "6": AppointmentsMenu.Show(appointmentBUS); break;
                    case "7": PrescriptionsMenu.Show(prescriptionBUS); break;
                    case "8": MedicalRecordsMenu.Show(medicalRecordBUS); break;
                    case "9": PaymentsMenu.Show(paymentBUS); break;
                    case "10": TestRequestsMenu.Show(testRequestBUS); break;
                    case "0":
                        Console.WriteLine("👋 Tạm biệt!");
                        return;
                    default:
                        Console.WriteLine("❌ Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }
    }
}