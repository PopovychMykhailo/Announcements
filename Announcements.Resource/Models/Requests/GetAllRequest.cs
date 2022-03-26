namespace Announcements.Resource.Models.Requests
{
    public class GetAllRequest
    {
        public bool WithDetails { get; set; } = false;
        public int MaxSize { get; set; } = 0;           // 0 - for all
    }
}
