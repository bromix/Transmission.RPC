namespace Transmission.RPC;

public static class Environment
{
    public static void Load()
    {
        var pathOfTest = AppDomain.CurrentDomain.BaseDirectory;
        var envFileName = Path.Combine(pathOfTest, ".env");
        if (!File.Exists(envFileName)) return;
        foreach (string line in System.IO.File.ReadLines(envFileName))
        {
            var index = line.IndexOf("=");
            if (index > 0)
            {
                var key = line.Substring(0, index);
                var value = line.Substring(index + 1);
                System.Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}