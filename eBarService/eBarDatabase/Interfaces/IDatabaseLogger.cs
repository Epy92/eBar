namespace eBarDatabase
{
    public interface IDatabaseLogger
    {
        void Log(string type, string logPhrase);
        void Start();
    }
}