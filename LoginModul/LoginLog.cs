namespace LoginModul
{
    public class LoginLog
    {
        public long LoginLogID { get; set; }
        public System.DateTime LogTime { get; set; }
        public string Location { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
    }
}