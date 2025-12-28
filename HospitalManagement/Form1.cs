using HospitalManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagement
{
    public partial class Form1 : Form
    {
        private readonly DatabaseTestService _testService;

        public Form1()
        {
            InitializeComponent();
            _testService = new DatabaseTestService();
        }

        private async void btnRunTest_Click(object sender, EventArgs e)
        {
            btnRunTest.Enabled = false;
            txtLog.Text = "Đang chạy bài test... Vui lòng đợi.\n";
            
            bool result = await _testService.RunFullTest();
            
            txtLog.Text = _testService.GetLog();
            
            if (result)
            {
                MessageBox.Show("Bài test hoàn tất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bài test thất bại. Vui lòng kiểm tra log.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            btnRunTest.Enabled = true;
        }
    }
}
