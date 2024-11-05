namespace ground_station.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ApiResponse()
        {
            Success = true; // Default success to true
            Data = default!;
            Message = string.Empty;
        }
    }
}