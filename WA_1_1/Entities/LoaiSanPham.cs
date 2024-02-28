namespace WA_1_1.Entities
{
	public class LoaiSanPham
	{
		public int LoaiSanPhamId { get; set; }
		public string TenLoaiSanPham { get; set; }
		public IEnumerable<SanPham> DsSanPham { get; set; }
	}
}
