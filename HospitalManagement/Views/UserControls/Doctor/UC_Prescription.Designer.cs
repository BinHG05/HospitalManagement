namespace HospitalManagement.Views.UserControls.Doctor
{
    partial class UC_Prescription
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            
            // Left panel - Patient info + Medicine selection
            this.panelPatientInfo = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblPatientDetails = new System.Windows.Forms.Label();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            
            this.panelMedicineSelect = new System.Windows.Forms.Panel();
            this.lblMedicineTitle = new System.Windows.Forms.Label();
            this.cmbMedicine = new System.Windows.Forms.ComboBox();
            this.lblDosage = new System.Windows.Forms.Label();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.btnAddMedicine = new System.Windows.Forms.Button();
            
            // Right panel - Prescription list
            this.panelPrescriptionList = new System.Windows.Forms.Panel();
            this.lblPrescriptionTitle = new System.Windows.Forms.Label();
            this.dgvPrescription = new System.Windows.Forms.DataGridView();
            this.colMedicineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFrequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInstructions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            
            this.panelTotal = new System.Windows.Forms.Panel();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            
            this.panelLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelPatientInfo.SuspendLayout();
            this.panelMedicineSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.panelPrescriptionList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescription)).BeginInit();
            this.panelTotal.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            
            // =============================================
            // panelHeader
            // =============================================
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.panelHeader.Controls.Add(this.btnBack);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(950, 55);
            this.panelHeader.TabIndex = 0;
            
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.Black;
            this.btnBack.Location = new System.Drawing.Point(10, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 35);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "←";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(60, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Kê đơn thuốc";
            this.lblTitle.UseCompatibleTextRendering = true;
            
            // =============================================
            // splitContainer
            // =============================================
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 55);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(950, 545);
            this.splitContainer.SplitterDistance = 400;
            this.splitContainer.TabIndex = 1;
            
            // =============================================
            // Left Panel - Patient Info + Medicine Selection
            // =============================================
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.splitContainer.Panel1.Controls.Add(this.panelMedicineSelect);
            this.splitContainer.Panel1.Controls.Add(this.panelPatientInfo);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(15);
            
            // panelPatientInfo
            this.panelPatientInfo.BackColor = System.Drawing.Color.White;
            this.panelPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPatientInfo.Location = new System.Drawing.Point(15, 15);
            this.panelPatientInfo.Name = "panelPatientInfo";
            this.panelPatientInfo.Padding = new System.Windows.Forms.Padding(20);
            this.panelPatientInfo.Size = new System.Drawing.Size(370, 130);
            this.panelPatientInfo.TabIndex = 0;
            this.panelPatientInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblPatientName.Location = new System.Drawing.Point(15, 15);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Text = "Nguyễn Văn A";
            
            this.lblPatientDetails.AutoSize = true;
            this.lblPatientDetails.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatientDetails.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPatientDetails.Location = new System.Drawing.Point(15, 45);
            this.lblPatientDetails.Name = "lblPatientDetails";
            this.lblPatientDetails.Text = "Nam, 35 tuổi";
            
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiagnosis.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.lblDiagnosis.Location = new System.Drawing.Point(15, 75);
            this.lblDiagnosis.MaximumSize = new System.Drawing.Size(340, 0);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Text = "Chẩn đoán: Viêm họng cấp";
            
            this.panelPatientInfo.Controls.Add(this.lblPatientName);
            this.panelPatientInfo.Controls.Add(this.lblPatientDetails);
            this.panelPatientInfo.Controls.Add(this.lblDiagnosis);
            
            // panelMedicineSelect
            this.panelMedicineSelect.BackColor = System.Drawing.Color.White;
            this.panelMedicineSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMedicineSelect.Location = new System.Drawing.Point(15, 135);
            this.panelMedicineSelect.Name = "panelMedicineSelect";
            this.panelMedicineSelect.Padding = new System.Windows.Forms.Padding(15);
            this.panelMedicineSelect.Size = new System.Drawing.Size(370, 395);
            this.panelMedicineSelect.TabIndex = 1;
            
            int yPos = 15;
            int labelWidth = 100;
            int inputX = 120;
            int rowHeight = 40;
            
            this.lblMedicineTitle.AutoSize = true;
            this.lblMedicineTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMedicineTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblMedicineTitle.Location = new System.Drawing.Point(15, yPos);
            this.lblMedicineTitle.Text = "Thêm thuốc";
            yPos += 35;
            
            this.lblDosage.Location = new System.Drawing.Point(15, yPos + 3);
            this.lblDosage.Text = "Chọn thuốc:";
            this.lblDosage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDosage.AutoSize = true;
            
            this.cmbMedicine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedicine.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMedicine.Location = new System.Drawing.Point(15, yPos + 25);
            this.cmbMedicine.Size = new System.Drawing.Size(340, 28);
            yPos += rowHeight + 15;
            
            // Dosage
            var lblDosageLabel = new System.Windows.Forms.Label();
            lblDosageLabel.Location = new System.Drawing.Point(15, yPos + 3);
            lblDosageLabel.Text = "Liều dùng:";
            lblDosageLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDosageLabel.AutoSize = true;
            
            this.txtDosage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDosage.Location = new System.Drawing.Point(inputX, yPos);
            this.txtDosage.Size = new System.Drawing.Size(230, 28);
            this.txtDosage.Text = "1 viên";
            yPos += rowHeight;
            
            // Frequency
            var lblFrequencyLabel = new System.Windows.Forms.Label();
            lblFrequencyLabel.Location = new System.Drawing.Point(15, yPos + 3);
            lblFrequencyLabel.Text = "Tần suất:";
            lblFrequencyLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblFrequencyLabel.AutoSize = true;
            
            this.txtFrequency.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFrequency.Location = new System.Drawing.Point(inputX, yPos);
            this.txtFrequency.Size = new System.Drawing.Size(230, 28);
            this.txtFrequency.Text = "3 lần/ngày";
            yPos += rowHeight;
            
            // Duration
            var lblDurationLabel = new System.Windows.Forms.Label();
            lblDurationLabel.Location = new System.Drawing.Point(15, yPos + 3);
            lblDurationLabel.Text = "Số ngày:";
            lblDurationLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDurationLabel.AutoSize = true;
            
            this.numDuration.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numDuration.Location = new System.Drawing.Point(inputX, yPos);
            this.numDuration.Size = new System.Drawing.Size(80, 28);
            this.numDuration.Minimum = 1;
            this.numDuration.Maximum = 365;
            this.numDuration.Value = 7;
            yPos += rowHeight;
            
            // Quantity
            var lblQuantityLabel = new System.Windows.Forms.Label();
            lblQuantityLabel.Location = new System.Drawing.Point(15, yPos + 3);
            lblQuantityLabel.Text = "Số lượng:";
            lblQuantityLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblQuantityLabel.AutoSize = true;
            
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numQuantity.Location = new System.Drawing.Point(inputX, yPos);
            this.numQuantity.Size = new System.Drawing.Size(80, 28);
            this.numQuantity.Minimum = 1;
            this.numQuantity.Maximum = 1000;
            this.numQuantity.Value = 21;
            yPos += rowHeight;
            
            // Instructions
            var lblInstructionsLabel = new System.Windows.Forms.Label();
            lblInstructionsLabel.Location = new System.Drawing.Point(15, yPos + 3);
            lblInstructionsLabel.Text = "Hướng dẫn:";
            lblInstructionsLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblInstructionsLabel.AutoSize = true;
            
            this.txtInstructions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtInstructions.Location = new System.Drawing.Point(inputX, yPos);
            this.txtInstructions.Size = new System.Drawing.Size(230, 28);
            this.txtInstructions.Text = "Uống sau ăn";
            yPos += rowHeight + 10;
            
            // Add button
            this.btnAddMedicine.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnAddMedicine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddMedicine.FlatAppearance.BorderSize = 0;
            this.btnAddMedicine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMedicine.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnAddMedicine.ForeColor = System.Drawing.Color.White;
            this.btnAddMedicine.Location = new System.Drawing.Point(15, yPos);
            this.btnAddMedicine.Size = new System.Drawing.Size(340, 45);
            this.btnAddMedicine.Text = "➕ Thêm vào đơn";
            this.btnAddMedicine.Click += new System.EventHandler(this.btnAddMedicine_Click);
            
            this.panelMedicineSelect.Controls.Add(this.lblMedicineTitle);
            this.panelMedicineSelect.Controls.Add(this.lblDosage);
            this.panelMedicineSelect.Controls.Add(this.cmbMedicine);
            this.panelMedicineSelect.Controls.Add(lblDosageLabel);
            this.panelMedicineSelect.Controls.Add(this.txtDosage);
            this.panelMedicineSelect.Controls.Add(lblFrequencyLabel);
            this.panelMedicineSelect.Controls.Add(this.txtFrequency);
            this.panelMedicineSelect.Controls.Add(lblDurationLabel);
            this.panelMedicineSelect.Controls.Add(this.numDuration);
            this.panelMedicineSelect.Controls.Add(lblQuantityLabel);
            this.panelMedicineSelect.Controls.Add(this.numQuantity);
            this.panelMedicineSelect.Controls.Add(lblInstructionsLabel);
            this.panelMedicineSelect.Controls.Add(this.txtInstructions);
            this.panelMedicineSelect.Controls.Add(this.btnAddMedicine);
            
            // =============================================
            // Right Panel - Prescription List
            // =============================================
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel2.Controls.Add(this.panelActions);
            this.splitContainer.Panel2.Controls.Add(this.panelTotal);
            this.splitContainer.Panel2.Controls.Add(this.panelPrescriptionList);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(15);
            
            // panelPrescriptionList
            this.panelPrescriptionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrescriptionList.Location = new System.Drawing.Point(15, 15);
            this.panelPrescriptionList.Name = "panelPrescriptionList";
            this.panelPrescriptionList.Size = new System.Drawing.Size(516, 400);
            this.panelPrescriptionList.TabIndex = 0;
            
            this.lblPrescriptionTitle.AutoSize = true;
            this.lblPrescriptionTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPrescriptionTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblPrescriptionTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPrescriptionTitle.Text = "📋 Đơn thuốc";
            
            this.dgvPrescription.AllowUserToAddRows = false;
            this.dgvPrescription.AllowUserToDeleteRows = false;
            this.dgvPrescription.BackgroundColor = System.Drawing.Color.White;
            this.dgvPrescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPrescription.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPrescription.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPrescription.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPrescription.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrescription.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colMedicineName,
                this.colDosage,
                this.colFrequency,
                this.colQuantity,
                this.colInstructions,
                this.colRemove
            });
            this.dgvPrescription.Location = new System.Drawing.Point(0, 30);
            this.dgvPrescription.Name = "dgvPrescription";
            this.dgvPrescription.ReadOnly = true;
            this.dgvPrescription.RowHeadersVisible = false;
            this.dgvPrescription.Size = new System.Drawing.Size(516, 360);
            this.dgvPrescription.TabIndex = 1;
            this.dgvPrescription.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrescription_CellClick);
            
            this.colMedicineName.HeaderText = "Tên thuốc";
            this.colMedicineName.Name = "colMedicineName";
            this.colMedicineName.ReadOnly = true;
            this.colMedicineName.Width = 150;
            
            this.colDosage.HeaderText = "Liều";
            this.colDosage.Name = "colDosage";
            this.colDosage.ReadOnly = true;
            this.colDosage.Width = 70;
            
            this.colFrequency.HeaderText = "Tần suất";
            this.colFrequency.Name = "colFrequency";
            this.colFrequency.ReadOnly = true;
            this.colFrequency.Width = 90;
            
            this.colQuantity.HeaderText = "SL";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.Width = 50;
            
            this.colInstructions.HeaderText = "Hướng dẫn";
            this.colInstructions.Name = "colInstructions";
            this.colInstructions.ReadOnly = true;
            this.colInstructions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.colRemove.HeaderText = "";
            this.colRemove.Name = "colRemove";
            this.colRemove.ReadOnly = true;
            this.colRemove.Text = "Xóa";
            this.colRemove.UseColumnTextForButtonValue = true;
            this.colRemove.Width = 50;
            
            this.panelPrescriptionList.Controls.Add(this.lblPrescriptionTitle);
            this.panelPrescriptionList.Controls.Add(this.dgvPrescription);
            
            // panelTotal
            this.panelTotal.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotal.Location = new System.Drawing.Point(15, 415);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(516, 50);
            this.panelTotal.TabIndex = 1;
            
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTotalLabel.Location = new System.Drawing.Point(10, 13);
            this.lblTotalLabel.Text = "Tổng tiền thuốc:";
            
            this.lblTotalAmount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblTotalAmount.Location = new System.Drawing.Point(350, 10);
            this.lblTotalAmount.Size = new System.Drawing.Size(150, 30);
            this.lblTotalAmount.Text = "0 đ";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.panelTotal.Controls.Add(this.lblTotalLabel);
            this.panelTotal.Controls.Add(this.lblTotalAmount);
            
            // panelActions
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(15, 465);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(516, 65);
            this.panelActions.TabIndex = 2;
            
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(0, 10);
            this.btnSave.Size = new System.Drawing.Size(150, 45);
            this.btnSave.Text = "💾 Lưu đơn";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(165, 10);
            this.btnPrint.Size = new System.Drawing.Size(150, 45);
            this.btnPrint.Text = "🖨️ In đơn";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(330, 10);
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            this.panelActions.Controls.Add(this.btnSave);
            this.panelActions.Controls.Add(this.btnPrint);
            this.panelActions.Controls.Add(this.btnCancel);
            
            // panelLoading
            this.panelLoading.BackColor = System.Drawing.Color.FromArgb(200, 255, 255, 255);
            this.panelLoading.Controls.Add(this.lblLoading);
            this.panelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLoading.Location = new System.Drawing.Point(0, 0);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(950, 600);
            this.panelLoading.TabIndex = 10;
            this.panelLoading.Visible = false;
            
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.lblLoading.Text = "⏳ Đang tải...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // =============================================
            // UC_Prescription
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_Prescription";
            this.Size = new System.Drawing.Size(950, 600);
            
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelPatientInfo.ResumeLayout(false);
            this.panelPatientInfo.PerformLayout();
            this.panelMedicineSelect.ResumeLayout(false);
            this.panelMedicineSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.panelPrescriptionList.ResumeLayout(false);
            this.panelPrescriptionList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescription)).EndInit();
            this.panelTotal.ResumeLayout(false);
            this.panelTotal.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelPatientInfo;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientDetails;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Panel panelMedicineSelect;
        private System.Windows.Forms.Label lblMedicineTitle;
        private System.Windows.Forms.ComboBox cmbMedicine;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Button btnAddMedicine;
        private System.Windows.Forms.Panel panelPrescriptionList;
        private System.Windows.Forms.Label lblPrescriptionTitle;
        private System.Windows.Forms.DataGridView dgvPrescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedicineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInstructions;
        private System.Windows.Forms.DataGridViewButtonColumn colRemove;
        private System.Windows.Forms.Panel panelTotal;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
