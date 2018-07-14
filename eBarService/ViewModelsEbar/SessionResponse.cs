namespace ViewModels
{
    public class SessionResponse
    {
        public string SessionKey { get; set; }
        public UserModel UserDetails { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public bool ResultFlag { get; set; }
    }
}