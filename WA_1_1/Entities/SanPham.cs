namespace WA_1_1.Entities
{
	public class SanPham
	{
		public int SanPhamId { get; set; }
		public int LoaiSanPhamId { get; set; }
		public LoaiSanPham LoaiSanPham { get; set; }
		public string TenSanPham { get; set; }
		public double GiaThanh { get; set; }
		public string Mota { get; set; }
		public DateTime NgayHetHan { get; set; }
		public string KyHieuSanPham { get; set; }
		public IEnumerable<ChiTietHoaDon> DsChiTietHoaDon { get; set; }
	}
}
