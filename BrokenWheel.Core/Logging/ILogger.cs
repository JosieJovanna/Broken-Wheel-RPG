namespace BrokenWheel.Core.Logging
{
    public interface ILogger
    {
        void Log(string value);
        void LogWarning(string value);
        void LogError(string value);
        void LogGood(string value);

        void LogCategory(string category, string value);
        void LogCategoryWarning(string category, string value);
        void LogCategoryError(string category, string value);
        void LogCategoryGood(string category, string value);
    }
}
