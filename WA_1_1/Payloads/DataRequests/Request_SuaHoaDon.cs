namespace WA_1_1.Payloads.DataRequests
{
	public class Request_SuaHoaDon
	{
		public int HoaDonId { get; set; }
		public string TenHoaDon { get; set; }
		public string GhiChu { get; set; }
		public List<Request_SuaChiTietHoaDon> suaChiTietHoaDons { get; set; }
	}
}
