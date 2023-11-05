using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace _20t1020242
{
    internal class DanhSach
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public DateTime NamXuatBan { get; set; }
        public string NhaXuatBan { get; set; }
        public string GiaNhap { get; set; }
        public string GiaBan { get; set; }
        public string SoLuong { get; set; }
        public string HinhSach { get; set; }

        // Hàm tạo ra danh sách sách từ tệp JSON
        public static List<DanhSach> FromJson(string json)
        {
            return JsonSerializer.Deserialize<List<DanhSach>>(json);
        }

        // Hàm lưu danh sách sách thành tệp JSON
        public static void SaveToJson(List<DanhSach> danhSach, string fileName)
        {
            string json = JsonSerializer.Serialize(danhSach);
            File.WriteAllText(fileName, json);
        }
    }
}