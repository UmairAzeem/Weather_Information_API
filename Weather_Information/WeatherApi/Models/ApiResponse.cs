namespace WeatherApi.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public ApiResponse()
        {
            Success = false;
            Message = "Something went wrong";
            Data = null;
        }
    }
}
