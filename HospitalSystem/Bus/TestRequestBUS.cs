using HospitalSystem.Models;
using HospitalSystem.Repositories;
using System;
using System.Linq;

namespace HospitalSystem.BUS
{
    /// <summary>
    /// Business logic for managing TestRequest entities.
    /// </summary>
    public class TestRequestBUS
    {
        private readonly TestRequestRepository _testRequestRepository;

        public TestRequestBUS(TestRequestRepository repository)
        {
            _testRequestRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(TestRequest testRequest)
        {
            if (testRequest == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Yêu cầu xét nghiệm"));

            if (!_testRequestRepository.PatientExists(testRequest.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (!_testRequestRepository.DoctorExists(testRequest.DoctorId))
                throw new ArgumentException(ErrorMessages.InvalidDoctorId);

            if (string.IsNullOrWhiteSpace(testRequest.TestName) || testRequest.TestName.Length < 3 || testRequest.TestName.Length > 100)
                throw new ArgumentException(ErrorMessages.InvalidTestNameLength);

            if (string.IsNullOrWhiteSpace(testRequest.Status) || !new[] { "Pending", "Completed", "Canceled" }.Contains(testRequest.Status))
                throw new ArgumentException(ErrorMessages.InvalidTestRequestStatus);

            if (testRequest.RequestDate < new DateTime(1900, 1, 1) || testRequest.RequestDate > DateTime.Now)
                throw new ArgumentException(ErrorMessages.InvalidRequestDate);

            _testRequestRepository.Add(testRequest);
        }

        public void Update(TestRequest testRequest)
        {
            if (testRequest == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Yêu cầu xét nghiệm"));

            if (!_testRequestRepository.Exists(testRequest.Id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Yêu cầu xét nghiệm", testRequest.Id));

            if (!_testRequestRepository.PatientExists(testRequest.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (!_testRequestRepository.DoctorExists(testRequest.DoctorId))
                throw new ArgumentException(ErrorMessages.InvalidDoctorId);

            if (string.IsNullOrWhiteSpace(testRequest.TestName) || testRequest.TestName.Length < 3 || testRequest.TestName.Length > 100)
                throw new ArgumentException(ErrorMessages.InvalidTestNameLength);

            if (string.IsNullOrWhiteSpace(testRequest.Status) || !new[] { "Pending", "Completed", "Canceled" }.Contains(testRequest.Status))
                throw new ArgumentException(ErrorMessages.InvalidTestRequestStatus);

            if (testRequest.RequestDate < new DateTime(1900, 1, 1) || testRequest.RequestDate > DateTime.Now)
                throw new ArgumentException(ErrorMessages.InvalidRequestDate);

            _testRequestRepository.Update(testRequest);
        }

        public void Delete(int id)
        {
            if (!_testRequestRepository.Exists(id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Yêu cầu xét nghiệm", id));

            _testRequestRepository.Delete(id);
        }

        public System.Collections.Generic.List<TestRequest> GetAll()
        {
            return _testRequestRepository.GetAll();
        }
    }
}