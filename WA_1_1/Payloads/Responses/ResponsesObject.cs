namespace WA_1_1.Payloads.Responses
{
	public class ResponsesObject<T>
	{
		public int Status { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }

		public ResponsesObject()
		{
		}

		public ResponsesObject(int status, string message, T data)
		{
			Status = status;
			Message = message;
			Data = data;
		}

		public ResponsesObject<T> ResponesSuccess (string message, T data)
		{
			return new ResponsesObject<T>(StatusCodes.Status200OK, message, data);
		}

		public ResponsesObject<T> ResponesError(int status,string message, T data)
		{
			return new ResponsesObject<T>(status, message, data);
		}

	}
}
