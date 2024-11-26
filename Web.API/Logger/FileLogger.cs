namespace Web.API.Logger;
public class FileLogger
{
    private readonly string _filepath;
    public FileLogger(string filepath)
    {
        _filepath = filepath;
    }
    public void LogError(string message, Exception ex)
    {
        var logMessage = $"[{DateTime.Now}] Error: {message} - Exception: {ex.Message}\n" +
            $"StackTrace: {ex.StackTrace}\n";
        WriteToFile(logMessage);
    }

    public void WriteToFile(string message)
    {
        lock (this)
            File.AppendAllText(_filepath, message);
    }
}
