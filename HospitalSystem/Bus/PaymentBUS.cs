using HospitalSystem.Models;
using HospitalSystem.Repositories;
using System;
using System.Linq;

namespace HospitalSystem.BUS
{
    /// <summary>
    /// Business logic for managing Payment entities.
    /// </summary>
    public class PaymentBUS
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentBUS(PaymentRepository repository)
        {
            _paymentRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(Payment payment)
        {
            if (payment == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Thanh toán"));

            if (!_paymentRepository.PatientExists(payment.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (payment.Amount <= 0)
                throw new ArgumentException(ErrorMessages.InvalidAmount);

            if (payment.PaymentDate < new DateTime(1900, 1, 1) || payment.PaymentDate > DateTime.Now)
                throw new ArgumentException(ErrorMessages.InvalidPaymentDate);

            if (string.IsNullOrWhiteSpace(payment.PaymentMethod) || !new[] { "Tiền mặt", "Thẻ tín dụng", "Chuyển khoản" }.Contains(payment.PaymentMethod))
                throw new ArgumentException(ErrorMessages.InvalidPaymentMethod);

            _paymentRepository.Add(payment);
        }

        public void Update(Payment payment)
        {
            if (payment == null)
                throw new ArgumentException(string.Format(ErrorMessages.NullEntity, "Thanh toán"));

            if (!_paymentRepository.Exists(payment.Id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Thanh toán", payment.Id));

            if (!_paymentRepository.PatientExists(payment.PatientId))
                throw new ArgumentException(ErrorMessages.InvalidPatientId);

            if (payment.Amount <= 0)
                throw new ArgumentException(ErrorMessages.InvalidAmount);

            if (payment.PaymentDate < new DateTime(1900, 1, 1) || payment.PaymentDate > DateTime.Now)
                throw new ArgumentException(ErrorMessages.InvalidPaymentDate);

            if (string.IsNullOrWhiteSpace(payment.PaymentMethod) || !new[] { "Tiền mặt", "Thẻ tín dụng", "Chuyển khoản" }.Contains(payment.PaymentMethod))
                throw new ArgumentException(ErrorMessages.InvalidPaymentMethod);

            _paymentRepository.Update(payment);
        }

        public void Delete(int id)
        {
            if (!_paymentRepository.Exists(id))
                throw new ArgumentException(string.Format(ErrorMessages.NotFound, "Thanh toán", id));

            _paymentRepository.Delete(id);
        }

        public System.Collections.Generic.List<Payment> GetAll()
        {
            return _paymentRepository.GetAll();
        }
    }
}