namespace HospitalSystem.BUS
{
    /// <summary>
    /// Contains error message constants used across BUS classes.
    /// </summary>
    public static class ErrorMessages
    {
        // Common errors
        public const string NullEntity = "{0} không được để trống.";
        public const string InvalidId = "ID {0} không hợp lệ.";
        public const string NotFound = "{0} với ID {1} không tồn tại.";
        public const string AlreadyExists = "{0} đã tồn tại.";

        // Name errors
        public const string InvalidFullNameLength = "Tên phải từ 3 đến 50 ký tự.";
        public const string InvalidFullNameFormat = "Tên chỉ được chứa chữ cái và khoảng trắng.";

        // Phone errors
        public const string InvalidPhoneLength = "Số điện thoại phải là 10 hoặc 11 số.";
        public const string InvalidPhoneFormat = "Số điện thoại chỉ được chứa số.";
        public const string PhoneExists = "Số điện thoại đã được sử dụng.";
        public const string PhoneExistsForOther = "Số điện thoại đã được sử dụng bởi {0} khác.";

        // Email errors
        public const string InvalidEmailFormat = "Email không hợp lệ.";
        public const string EmailExists = "Email đã được sử dụng.";
        public const string EmailExistsForOther = "Email đã được sử dụng bởi {0} khác.";

        // Appointment errors
        public const string InvalidAppointmentDate = "Ngày hẹn phải từ hôm nay trở đi.";
        public const string InvalidAppointmentStatus = "Trạng thái phải là Scheduled, Completed hoặc Canceled.";
        public const string InvalidPatientId = "ID bệnh nhân không tồn tại.";
        public const string InvalidDoctorId = "ID bác sĩ không tồn tại.";

        // MedicalRecord errors
        public const string InvalidDiagnosisLength = "Chẩn đoán phải từ 3 đến 500 ký tự.";
        public const string InvalidRecordDate = "Ngày ghi nhận không hợp lệ, phải từ năm 1900 đến hiện tại.";

        // Patient errors
        public const string InvalidBirthDate = "Ngày sinh phải từ năm 1900 đến hiện tại.";
        public const string InvalidGender = "Giới tính phải là Nam hoặc Nữ.";

        // Payment errors
        public const string InvalidAmount = "Số tiền phải lớn hơn 0.";
        public const string InvalidPaymentDate = "Ngày thanh toán không hợp lệ, phải từ năm 1900 đến hiện tại.";
        public const string InvalidPaymentMethod = "Phương thức thanh toán phải là Tiền mặt, Thẻ tín dụng hoặc Chuyển khoản.";

        // Prescription errors
        public const string InvalidMedicationLength = "Tên thuốc phải từ 3 đến 100 ký tự.";
        public const string InvalidDosageLength = "Liều lượng phải từ 3 đến 50 ký tự.";
        public const string InvalidAppointmentId = "ID cuộc hẹn không tồn tại.";

        // TestRequest errors
        public const string InvalidTestNameLength = "Tên xét nghiệm phải từ 3 đến 100 ký tự.";
        public const string InvalidTestRequestStatus = "Trạng thái phải là Pending, Completed hoặc Canceled.";
        public const string InvalidRequestDate = "Ngày yêu cầu không hợp lệ, phải từ năm 1900 đến hiện tại.";

        // Doctor errors
        public const string InvalidSpecialtyLength = "Chuyên môn không được vượt quá 100 ký tự.";
    }
}