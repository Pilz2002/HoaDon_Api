using Microsoft.AspNetCore.Mvc;
using WA_1_1.Services.Interfaces;
using WA_1_1.Payloads.DataRequests;

namespace WA_1_1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HoaDonController : ControllerBase
	{
		private readonly IHoaDonServices _iHoaDonServices;

		public HoaDonController(IHoaDonServices iHoaDonServices)
		{
			_iHoaDonServices = iHoaDonServices;
		}

		[HttpPost("ThemHoaDon")]
		public IActionResult ThemHoaDon(Request_ThemHoaDon request)
		{
			return Ok(_iHoaDonServices.ThemHoaDon(request));
		}

		[HttpPut("SuaHoaDon")]
		public IActionResult SuaHoaDon(Request_SuaHoaDon request)
		{
			return Ok(_iHoaDonServices.SuaHoaDon(request));
		}

		[HttpDelete("XoaHoaDon")]
		public IActionResult XoaHoaDon(Request_XoaHoaDon request)
		{
			return Ok(_iHoaDonServices.XoaHoaDon(request));
		}
	}
}
