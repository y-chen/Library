#load "logger.csx"

public class CommandLine
{
    public static string Execute(string command)
    {
        command = command.Replace("\"", "\"\"");

        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = "-c \"" + command + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        process.Start();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            Logger.LogError(process.StandardOutput.ReadToEnd());

            return process.ExitCode.ToString();
        }

        return process.StandardOutput.ReadToEnd();
    }
}
