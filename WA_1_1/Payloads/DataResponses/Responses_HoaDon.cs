using WA_1_1.Entities;

namespace WA_1_1.Payloads.DataResponses
{
	public class Responses_HoaDon
	{
		public string TenKhachHang { get; set; }
		public string TenHoaDon { get; set; }
		public string? MaGiaoDich { get; set; }
		public DateTime? ThoiGianTao { get; set; }
		public DateTime ThoiGianCapNhat { get; set; }
		public string GhiChu { get; set; }
		public double? TongTien { get; set; }
		public IQueryable<Responses_ChiTietHoaDon> responses_ChiTietHoaDons { get; set; }
	}
}
