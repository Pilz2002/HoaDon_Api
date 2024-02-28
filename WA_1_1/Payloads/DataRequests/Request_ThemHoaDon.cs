using WA_1_1.Entities;

namespace WA_1_1.Payloads.DataRequests
{
	public class Request_ThemHoaDon
	{
		public int KhachHangId { get; set; }
		public string TenHoaDon { get; set; }
		public string GhiChu { get; set; }
		public List<Request_ThemChiTietHoaDon> themChiTietHoaDons { get; set; }
	}
}
