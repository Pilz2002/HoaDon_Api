using WA_1_1.AppDb;
using WA_1_1.Entities;
using WA_1_1.Payloads.DataResponses;

namespace WA_1_1.Payloads.Converters
{
	public class ChiTietHoaDonConverter
	{
		private readonly AppDbContext _context;

		public ChiTietHoaDonConverter()
		{
			_context = new AppDbContext();
		}
		public Responses_ChiTietHoaDon EntityToDTO(ChiTietHoaDon chiTietHoaDon)
		{
			return new Responses_ChiTietHoaDon()
			{
				DVT = chiTietHoaDon.DVT,
				SoLuong = chiTietHoaDon.SoLuong,
				TenSanPham = _context.SanPham.SingleOrDefault(x => x.SanPhamId == chiTietHoaDon.SanPhamId).TenSanPham,
				ThanhTien = chiTietHoaDon.ThanhTien
			};
		}
	}
}
