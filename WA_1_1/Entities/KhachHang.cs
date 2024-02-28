namespace WA_1_1.Entities
{
	public class KhachHang
	{
		public int KhachHangId { get; set; }
		public string HoTen { get; set; }
		public DateTime NgaySinh { get; set; }
		public string SDT { get; set; }
		public IEnumerable<HoaDon> DsHoaDon { get; set; }
	}
}
