using HospitalManagement.Presenters.Doctor;
using HospitalManagement.Views.Interfaces.Doctor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_Prescription : UserControl, IPrescriptionView
    {
        private PrescriptionPresenter _presenter;
        private List<MedicineDto> _medicines = new List<MedicineDto>();
        private List<PrescriptionItemDto> _prescriptionItems = new List<PrescriptionItemDto>();
        private Action _onBack;

        public int RecordId { get; set; }
        public int ExaminationId { get; set; }

        public event EventHandler SaveRequested;
        public event EventHandler PrintRequested;
        public event EventHandler CancelRequested;

        public UC_Prescription()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        public void Initialize(int examinationId, Action onBack)
        {
            ExaminationId = examinationId;
            _onBack = onBack;
            
            _presenter = new PrescriptionPresenter(this, examinationId);
            _presenter.Initialize();
        }

        private void SetupDataGridView()
        {
            dgvPrescription.EnableHeadersVisualStyles = false;
            dgvPrescription.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvPrescription.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            dgvPrescription.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvPrescription.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvPrescription.ColumnHeadersHeight = 40;
            
            dgvPrescription.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPrescription.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            dgvPrescription.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvPrescription.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dgvPrescription.DefaultCellStyle.Padding = new Padding(5);
            dgvPrescription.RowTemplate.Height = 35;
            
            dgvPrescription.GridColor = Color.FromArgb(226, 232, 240);
        }

        #region IPrescriptionView Implementation

        public void LoadPatientInfo(PrescriptionPatientDto patient)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadPatientInfo(patient)));
                return;
            }

            lblPatientName.Text = patient.FullName;
            var ageText = patient.Age.HasValue ? $"{patient.Age} tuổi" : "N/A";
            lblPatientDetails.Text = $"{patient.Gender}, {ageText}";
            lblDiagnosis.Text = $"Chẩn đoán: {patient.Diagnosis ?? "Chưa có"}";
        }

        public void LoadMedicineList(IEnumerable<MedicineDto> medicines)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadMedicineList(medicines)));
                return;
            }

            _medicines = medicines.ToList();
            cmbMedicine.DataSource = null;
            cmbMedicine.DataSource = _medicines;
            cmbMedicine.DisplayMember = "MedicineName";
            cmbMedicine.ValueMember = "MedicineId";
        }

        public void AddPrescriptionItem(PrescriptionItemDto item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AddPrescriptionItem(item)));
                return;
            }

            // Check if already exists
            var existing = _prescriptionItems.FirstOrDefault(p => p.MedicineId == item.MedicineId);
            if (existing != null)
            {
                existing.Quantity = item.Quantity;
                existing.Dosage = item.Dosage;
                existing.Frequency = item.Frequency;
                existing.Duration = item.Duration;
                existing.Instructions = item.Instructions;
            }
            else
            {
                _prescriptionItems.Add(item);
            }

            RefreshPrescriptionGrid();
            UpdateTotal();
        }

        public void RemovePrescriptionItem(int medicineId)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => RemovePrescriptionItem(medicineId)));
                return;
            }

            var item = _prescriptionItems.FirstOrDefault(p => p.MedicineId == medicineId);
            if (item != null)
            {
                _prescriptionItems.Remove(item);
                RefreshPrescriptionGrid();
                UpdateTotal();
            }
        }

        public void ClearPrescriptionItems()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ClearPrescriptionItems()));
                return;
            }

            _prescriptionItems.Clear();
            RefreshPrescriptionGrid();
            UpdateTotal();
        }

        public IEnumerable<PrescriptionItemDto> GetPrescriptionItems()
        {
            return _prescriptionItems.ToList();
        }

        public void ShowLoading(bool isLoading)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowLoading(isLoading)));
                return;
            }

            panelLoading.Visible = isLoading;
            panelLoading.BringToFront();
        }

        public void ShowError(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowError(message)));
                return;
            }

            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowSuccess(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowSuccess(message)));
                return;
            }

            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Private Methods

        private void RefreshPrescriptionGrid()
        {
            dgvPrescription.Rows.Clear();
            foreach (var item in _prescriptionItems)
            {
                dgvPrescription.Rows.Add(
                    item.MedicineName,
                    item.Dosage,
                    item.Frequency,
                    item.Quantity,
                    item.Instructions,
                    "Xóa"
                );
                dgvPrescription.Rows[dgvPrescription.Rows.Count - 1].Tag = item.MedicineId;
            }
        }

        private void UpdateTotal()
        {
            var total = _prescriptionItems.Sum(p => p.TotalPrice);
            lblTotalAmount.Text = $"{total:N0} đ";
        }

        #endregion

        #region Event Handlers

        private void btnAddMedicine_Click(object sender, EventArgs e)
        {
            if (cmbMedicine.SelectedItem == null)
            {
                ShowError("Vui lòng chọn thuốc.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDosage.Text))
            {
                ShowError("Vui lòng nhập liều dùng.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFrequency.Text))
            {
                ShowError("Vui lòng nhập tần suất.");
                return;
            }

            var medicine = cmbMedicine.SelectedItem as MedicineDto;
            if (medicine == null) return;

            var item = new PrescriptionItemDto
            {
                MedicineId = medicine.MedicineId,
                MedicineName = medicine.MedicineName,
                Unit = medicine.Unit,
                Dosage = txtDosage.Text.Trim(),
                Frequency = txtFrequency.Text.Trim(),
                Duration = (int)numDuration.Value,
                Quantity = (int)numQuantity.Value,
                Instructions = txtInstructions.Text.Trim(),
                PricePerUnit = medicine.PricePerUnit
            };

            AddPrescriptionItem(item);

            // Reset form for next medicine
            cmbMedicine.SelectedIndex = -1;
            if (_medicines.Any()) cmbMedicine.SelectedIndex = 0;
        }

        private void dgvPrescription_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if clicked on Remove button column
            if (e.ColumnIndex == colRemove.Index)
            {
                var medicineId = (int)dgvPrescription.Rows[e.RowIndex].Tag;
                var result = MessageBox.Show(
                    "Bạn có chắc muốn xóa thuốc này khỏi đơn?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    RemovePrescriptionItem(medicineId);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn hủy đơn thuốc hiện tại?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                CancelRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _onBack?.Invoke();
        }

        #endregion
    }
}
