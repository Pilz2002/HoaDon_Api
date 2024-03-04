namespace WA_1_1.Payloads.DataRequests
{
	public class Request_LayHoaDon
	{
		public int? Year { get; set; }
		public int? Month { get; set; }
		public int? DayBegin { get; set; }
		public int? DayEnd { get; set; }
		public double? MoneyMin { get; set; }
		public double? MoneyMax { get; set; }
		public string? MaGiaoDichorTenHoaDon { get; set; }
		public int PageSize { get; set; } = -1;
		public int PageNumber { get; set; } = 1;
	}
}
