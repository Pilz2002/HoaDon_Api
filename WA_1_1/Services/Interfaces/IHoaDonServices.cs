using WA_1_1.Payloads.DataRequests;
using WA_1_1.Payloads.DataResponses;
using WA_1_1.Payloads.Responses;

namespace WA_1_1.Services.Interfaces
{
	public interface IHoaDonServices
	{
		ResponsesObject<Responses_HoaDon> ThemHoaDon(Request_ThemHoaDon request);
		ResponsesObject<Responses_HoaDon> SuaHoaDon(Request_SuaHoaDon request);
		string TaoMaGiaoDich();
		ResponsesObject<Responses_HoaDon> XoaHoaDon(Request_XoaHoaDon request);
	}
}
