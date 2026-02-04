using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Patient;
using System;
using System.Linq;

namespace HospitalManagement.Presenters.Patient
{
    public class PaymentPresenter
    {
        private readonly IPaymentView _view;
        private readonly IPaymentService _paymentService;
        private int _patientId;

        public PaymentPresenter(IPaymentView view, int patientId)
        {
            _view = view;
            _patientId = patientId;
            _paymentService = new PaymentService();
        }

        public void LoadInvoices(string statusFilter = "all", string typeFilter = "all")
        {
            try
            {
                _view.ShowLoading(true);

                var invoices = _paymentService.GetPatientInvoices(_patientId);

                if (statusFilter != "all")
                {
                    invoices = invoices.Where(i => i.InvoiceStatus == statusFilter);
                }

                if (typeFilter != "all")
                {
                    invoices = invoices.Where(i => i.PaymentType == typeFilter);
                }

                _view.LoadInvoices(invoices.ToList());
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải danh sách hóa đơn: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void PayInvoice(int invoiceId, string paymentMethod)
        {
            try
            {
                _view.ShowLoading(true);

                var success = _paymentService.PayInvoice(invoiceId, paymentMethod);

                if (success)
                {
                    _view.ShowSuccess("Thanh toán thành công!");
                    LoadInvoices(_view.SelectedStatusFilter);
                }
                else
                {
                    _view.ShowError("Không thể thanh toán. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi thanh toán: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }
    }
}
