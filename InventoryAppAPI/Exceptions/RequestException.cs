namespace InventoryAppAPI.Exceptions
{
    public class RequestException : Exception
    {
        public int StatusCode { get; set; }

        public RequestException(int statusCode, string msg) : base(msg) 
        {
            StatusCode = statusCode;
        }
    
    }
}
