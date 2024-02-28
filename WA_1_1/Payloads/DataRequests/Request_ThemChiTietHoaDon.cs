using WA_1_1.Entities;

namespace WA_1_1.Payloads.DataRequests
{
	public class Request_ThemChiTietHoaDon
	{
		public int SanPhamId { get; set; }
		public int SoLuong { get; set; }
		public string DVT { get; set; }
	}
}
