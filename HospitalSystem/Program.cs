using System;
  using System.IO;
  using Microsoft.Extensions.Configuration;
  using MySql.Data.MySqlClient;
  using HospitalSystem;
  using HospitalSystem.Repositories;
  using HospitalSystem.BUS;
  using HospitalSystem.Menus;

  namespace HospitalSystem
  {
      /// <summary>
      /// Main entry point for the Hospital System application.
      /// </summary>
      class Program
      {
          static void Main(string[] args)
          {
              try
              {
                  Console.OutputEncoding = System.Text.Encoding.UTF8;
                  Console.InputEncoding = System.Text.Encoding.UTF8;

                  // Đọc cấu hình từ appsettings.json
                  var configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .Build();

                  // Khởi tạo kết nối cơ sở dữ liệu
                  var dbConnection = new DbConnection(configuration);
                  using var connection = dbConnection.GetConnection();

                  // Khởi tạo các repository
                  var userRepo = new UserRepository(connection);
                  var doctorRepo = new DoctorRepository(connection);
                  var adminRepo = new AdminRepository(connection);
                  var patientRepo = new PatientRepository(connection);
                  var appointmentRepo = new AppointmentRepository(connection);
                  var prescriptionRepo = new PrescriptionRepository(connection);
                  var medicalRecordRepo = new MedicalRecordRepository(connection);
                  var paymentRepo = new PaymentRepository(connection);
                  var testRequestRepo = new TestRequestRepository(connection);
                  var receptionistRepo = new ReceptionistRepository(connection);

                  // Khởi tạo các BUS
                  var userBUS = new UserBUS(userRepo);
                  var doctorBUS = new DoctorBUS(doctorRepo);
                  var adminBUS = new AdminBUS(adminRepo);
                  var patientBUS = new PatientBUS(patientRepo);
                  var appointmentBUS = new AppointmentBUS(appointmentRepo);
                  var prescriptionBUS = new PrescriptionBUS(prescriptionRepo, appointmentRepo); // Đúng tham số
                  var medicalRecordBUS = new MedicalRecordBUS(medicalRecordRepo);
                  var paymentBUS = new PaymentBUS(paymentRepo);
                  var testRequestBUS = new TestRequestBUS(testRequestRepo);
                  var receptionistBUS = new ReceptionistBUS(receptionistRepo);

                  // Gọi menu chính
                  MainMenu.Show(
                      adminBUS,
                      doctorBUS,
                      userBUS,
                      patientBUS,
                      receptionistBUS,
                      appointmentBUS,
                      prescriptionBUS,
                      medicalRecordBUS,
                      paymentBUS,
                      testRequestBUS
                  );
              }
              catch (MySqlException ex)
              {
                  Console.WriteLine($"❌ Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
              }
              catch (Exception ex)
              {
                  Console.WriteLine($"❌ Lỗi khởi động chương trình: {ex.Message}");
              }
          }
      }
  }