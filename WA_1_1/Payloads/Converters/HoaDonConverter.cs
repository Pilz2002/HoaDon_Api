using WA_1_1.AppDb;
using WA_1_1.Entities;
using WA_1_1.Payloads.DataResponses;

namespace WA_1_1.Payloads.Converters
{
	public class HoaDonConverter
	{
		private readonly AppDbContext _context;
		private readonly ChiTietHoaDonConverter _converter;
		public HoaDonConverter()
		{
			_context = new AppDbContext();
			_converter = new ChiTietHoaDonConverter();
		}

		public Responses_HoaDon EntityToDTO(HoaDon hoaDon)
		{
			return new Responses_HoaDon()
			{
				TenKhachHang = _context.KhachHang.SingleOrDefault(x => x.KhachHangId == hoaDon.KhachHangId).HoTen,
				TenHoaDon = hoaDon.TenHoaDon,
				GhiChu = hoaDon.GhiChu,
				MaGiaoDich = hoaDon.MaGiaoDich,
				ThoiGianTao = hoaDon.ThoiGianTao,
				ThoiGianCapNhat = hoaDon.ThoiGianCapNhat,
				TongTien = hoaDon.TongTien,
				responses_ChiTietHoaDons = _context.ChiTietHoaDon.Where(x=>x.HoaDonId == hoaDon.HoaDonId).Select(x => _converter.EntityToDTO(x))
			};
		}
	}
}
