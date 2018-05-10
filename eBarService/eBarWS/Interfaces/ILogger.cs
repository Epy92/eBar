namespace eBarWS.Interfaces
{
    public interface ILogger
    {
        void Log(string type, string logPhrase);
        void Start();
    }
}