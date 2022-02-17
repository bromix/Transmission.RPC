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
            var values = line.Split("=");
            if (values.Length != 2) continue;
            System.Environment.SetEnvironmentVariable(values[0], values[1]);
        }
    }
}