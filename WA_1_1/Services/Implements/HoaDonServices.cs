using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using WA_1_1.AppDb;
using WA_1_1.Entities;
using WA_1_1.Payloads.Converters;
using WA_1_1.Payloads.DataRequests;
using WA_1_1.Payloads.DataResponses;
using WA_1_1.Payloads.Responses;
using WA_1_1.Services.Interfaces;

namespace WA_1_1.Services.Implements
{
	public class HoaDonServices : IHoaDonServices
	{
		private readonly AppDbContext _context;
		private readonly ResponsesObject<Responses_HoaDon> _responsesObject;
		private readonly HoaDonConverter _converter;

		public HoaDonServices(ResponsesObject<Responses_HoaDon> responsesObject, HoaDonConverter converter)
		{
			_context = new AppDbContext();
			_responsesObject = responsesObject;
			_converter = converter;
		}

		public ResponsesObject<Responses_HoaDon> SuaHoaDon(Request_SuaHoaDon request)
		{
			HoaDon hoaDonUpdate = _context.HoaDon.SingleOrDefault(x => x.HoaDonId == request.HoaDonId);
			if (hoaDonUpdate == null)
			{
				return _responsesObject.ResponesError(StatusCodes.Status404NotFound, "Khong tim thay hoa don Id", null);
			}
			hoaDonUpdate.TenHoaDon = request.TenHoaDon;
			hoaDonUpdate.GhiChu = request.GhiChu;
			hoaDonUpdate.ThoiGianCapNhat = DateTime.Now;
			_context.Update(hoaDonUpdate);
			_context.SaveChanges();
			hoaDonUpdate.DsChiTietHoaDon = SuaListChiTietHoaDon(hoaDonUpdate.HoaDonId, request.suaChiTietHoaDons);
			_context.HoaDon.Update(hoaDonUpdate);
			_context.SaveChanges();
			double? tongTien = 0;
			foreach (var item in hoaDonUpdate.DsChiTietHoaDon)
			{
				tongTien += item.ThanhTien;
			}
			hoaDonUpdate.TongTien = tongTien;
			_context.HoaDon.Update(hoaDonUpdate);
			_context.SaveChanges();
			return _responsesObject.ResponesSuccess("Sua hoa don thanh cong", _converter.EntityToDTO(hoaDonUpdate));
		}

		private List<ChiTietHoaDon> SuaListChiTietHoaDon(int hoaDonId, List<Request_SuaChiTietHoaDon> requests)
		{
			var hoaDon = _context.HoaDon.SingleOrDefault(x => x.HoaDonId == hoaDonId);
			if (hoaDon is null)
			{
				return null;
			}
			List<ChiTietHoaDon> list = new List<ChiTietHoaDon>();
			list = _context.ChiTietHoaDon.ToList().FindAll(x => x.HoaDonId == hoaDonId);

			int tmp = 0;
			foreach (var request in requests)
			{
				for (int i = tmp; i < list.Count; i++)
				{
					var sanPham = _context.SanPham.SingleOrDefault(x => x.SanPhamId == request.SanPhamId);
					if (sanPham is null)
					{
						throw new Exception("San pham khong ton tai");
					}
					list[i].SanPhamId = request.SanPhamId;
					list[i].DVT = request.DVT;
					list[i].SoLuong = request.SoLuong;
					list[i].ThanhTien = sanPham.GiaThanh * request.SoLuong;
					_context.ChiTietHoaDon.Update(list[i]);
					_context.SaveChanges();
					break;
				}
				tmp++;
			}
			return list;
		}

		public ResponsesObject<Responses_HoaDon> ThemHoaDon(Request_ThemHoaDon request)
		{
			var khachHang = _context.KhachHang.SingleOrDefault(x => x.KhachHangId == request.KhachHangId);
			if (khachHang is null)
			{
				return _responsesObject.ResponesError(StatusCodes.Status404NotFound, "Khong tim thay khach hang", null);
			}
			HoaDon hoaDon = new HoaDon();
			hoaDon.TenHoaDon = request.TenHoaDon;
			hoaDon.KhachHangId = request.KhachHangId;
			hoaDon.MaGiaoDich = TaoMaGiaoDich();
			hoaDon.ThoiGianTao = DateTime.Now;
			hoaDon.ThoiGianCapNhat = DateTime.Now;
			hoaDon.GhiChu = request.GhiChu;
			hoaDon.TongTien = 0;
			hoaDon.DsChiTietHoaDon = null;
			_context.HoaDon.Add(hoaDon);
			_context.SaveChanges();
			hoaDon.DsChiTietHoaDon = ThemListChiTietHoaDon(hoaDon.HoaDonId, request.themChiTietHoaDons);
			_context.HoaDon.Update(hoaDon);
			_context.SaveChanges();
			double? tongTien = 0;
			foreach (var item in hoaDon.DsChiTietHoaDon)
			{
				tongTien += item.ThanhTien;
			}
			hoaDon.TongTien = tongTien;
			_context.HoaDon.Update(hoaDon);
			_context.SaveChanges();
			return _responsesObject.ResponesSuccess("Them hoa don thanh cong", _converter.EntityToDTO(hoaDon));
		}

		private List<ChiTietHoaDon> ThemListChiTietHoaDon(int hoaDonId, List<Request_ThemChiTietHoaDon> requests)
		{
			var hoaDon = _context.HoaDon.SingleOrDefault(x => x.HoaDonId == hoaDonId);
			if (hoaDon is null)
			{
				return null;
			}
			List<ChiTietHoaDon> list = new List<ChiTietHoaDon>();
			foreach (var request in requests)
			{
				ChiTietHoaDon ct = new ChiTietHoaDon();
				var sanPham = _context.SanPham.SingleOrDefault(x => x.SanPhamId == request.SanPhamId);
				if (sanPham is null)
				{
					_context.HoaDon.Remove(hoaDon);
					_context.SaveChanges();
					throw new Exception("San pham khong ton tai");
				}
				ct.HoaDonId = hoaDonId;
				ct.SanPhamId = request.SanPhamId;
				ct.DVT = request.DVT;
				ct.SoLuong = request.SoLuong;
				ct.ThanhTien = sanPham.GiaThanh * request.SoLuong;
				list.Add(ct);
			}
			_context.ChiTietHoaDon.AddRange(list);
			_context.SaveChanges();
			return list;
		}

		public string TaoMaGiaoDich()
		{
			var currentTime = DateTime.Now.ToString("yyyyMMdd") + "_";
			var soHoaDonTrongNgay = _context.HoaDon.Count(x => x.ThoiGianTao.Date == DateTime.Now.Date) + 1;
			return currentTime + soHoaDonTrongNgay.ToString();
		}

		public ResponsesObject<Responses_HoaDon> XoaHoaDon(Request_XoaHoaDon request)
		{
			HoaDon hoaDon = _context.HoaDon.SingleOrDefault(x => x.HoaDonId == request.HoaDonId);
			if (hoaDon == null) return _responsesObject.ResponesError(StatusCodes.Status404NotFound, "Khong tim thay hoa don", null);
			_context.HoaDon.Remove(hoaDon);
			_context.SaveChanges();
			return _responsesObject.ResponesSuccess("Xoa thanh cong", _converter.EntityToDTO(hoaDon));
		}

		public IQueryable<Responses_HoaDon> LocHoaDon(Request_LayHoaDon request)
		{
			//LayHoaDonTheoNam
			if (request.Year != null && request.Month != null)
			{
				var hoaDons = _context.HoaDon.Where(x => x.ThoiGianCapNhat.Month == request.Month && x.ThoiGianCapNhat.Year == request.Year).ToList();
				if (hoaDons == null)
				{
					return null;
				}
				foreach (var hoaDon in hoaDons)
				{
					hoaDon.DsChiTietHoaDon = LayDSChiTietHoaDon(hoaDon.HoaDonId);
				}
				List<Responses_HoaDon> ret = new List<Responses_HoaDon>();
				foreach (var hoaDon in hoaDons)
				{
					ret.Add(_converter.EntityToDTO(hoaDon));
				}
				return PhanTrang(ret.AsQueryable(),request.PageSize,request.PageNumber);
			}
			//Lay hoa don theo ngay
			else if (request.DayBegin != null && request.DayEnd != null)
			{
				var hoaDons = _context.HoaDon.Where(x => x.ThoiGianCapNhat.Day >= request.DayBegin && x.ThoiGianCapNhat.Day <= request.DayEnd).ToList();
				if (hoaDons == null)
				{
					return null;
				}
				foreach (var item in hoaDons)
				{
					item.DsChiTietHoaDon = LayDSChiTietHoaDon(item.HoaDonId);
				}
				List<Responses_HoaDon> ret = new List<Responses_HoaDon>();
				foreach (var item in hoaDons)
				{
					ret.Add(_converter.EntityToDTO(item));
				}
				return PhanTrang(ret.AsQueryable(), request.PageSize, request.PageNumber);
			}
			//lay theo tong tien
			else if (request.MoneyMin != null && request.MoneyMax != null)
			{
				var hoaDons = _context.HoaDon.Where(x => x.TongTien >= request.MoneyMin && x.TongTien <= request.MoneyMax).ToList();
				if (hoaDons == null)
				{
					return null;
				}
				foreach (var item in hoaDons)
				{
					item.DsChiTietHoaDon = LayDSChiTietHoaDon(item.HoaDonId);
				}
				List<Responses_HoaDon> ret = new List<Responses_HoaDon>();
				foreach (var item in hoaDons)
				{
					ret.Add(_converter.EntityToDTO(item));
				}
				return PhanTrang(ret.AsQueryable(), request.PageSize, request.PageNumber);
			}
			//Lay hoa don theo ma hoac ten
			else if (request.MaGiaoDichorTenHoaDon != null)
			{
				var hoaDons = _context.HoaDon.Where(x => x.MaGiaoDich == request.MaGiaoDichorTenHoaDon).ToList();
				if (hoaDons == null)
				{
					return null;
				}
				List<Responses_HoaDon> ret = new List<Responses_HoaDon>();
				foreach (var item in hoaDons)
				{
					ret.Add(_converter.EntityToDTO(item));
				}
				return PhanTrang(ret.AsQueryable(), request.PageSize, request.PageNumber);
			}
			//Lay theo ngay tao moi nhat
			else
			{
				var hoaDons = _context.HoaDon.OrderByDescending(x => x.ThoiGianTao).ToList();
				List<Responses_HoaDon> ret = new List<Responses_HoaDon>();
				foreach (var hoaDon in hoaDons)
				{
					ret.Add(_converter.EntityToDTO(hoaDon));
				}
				return PhanTrang(ret.AsQueryable(), request.PageSize, request.PageNumber);
			}
		}

		private List<ChiTietHoaDon> LayDSChiTietHoaDon(int hoaDonId)
		{
			List<ChiTietHoaDon> list = new List<ChiTietHoaDon>();
			list = _context.ChiTietHoaDon.Where(x => x.HoaDonId == hoaDonId).ToList();
			return list;
		}

		public IQueryable<Responses_HoaDon> PhanTrang(IQueryable<Responses_HoaDon> input, int pageSize, int pageNumber)
		{
			return input.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
		}
	}
}
