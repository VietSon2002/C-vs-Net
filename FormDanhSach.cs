using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20t1020242
{
    public partial class FormDanhSach : Form
    {
        
        public FormDanhSach()
        {
            InitializeComponent();
        }
        String fileName = "20t1020242.json";


        private void FormDanhSach_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTaoDS_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    // Đọc nội dung từ tệp JSON
                    string jsonContent = File.ReadAllText(fileName);

                    // Chuyển đổi nội dung JSON thành danh sách đối tượng
                    List<DanhSach> danhSachList = JsonConvert.DeserializeObject<List<DanhSach>>(jsonContent);

                    // Hiển thị dữ liệu trong DataGridView
                    dataGridView1.DataSource = danhSachList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tệp JSON không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}