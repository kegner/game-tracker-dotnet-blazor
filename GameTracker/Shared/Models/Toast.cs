namespace GameTracker.Shared.Models
{
    public class Toast
    {
        public string Id { get; set; } = "";
        public bool IsError { get; set; } = false;
        public string Message { get; set; } = "";
    }
}
