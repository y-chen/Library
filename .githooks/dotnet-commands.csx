#load "logger.csx"
#load "command-line.csx"

public class DotnetCommands
{
    public static int FormatCode() => ExecuteCommand("dotnet csharpier ./src");
    public static int BuildCode() => ExecuteCommand("dotnet build ./src");

    // public static int FormatTests() => ExecuteCommand("dotnet format ./tests");
    // public static int RunTests() => ExecuteCommand("dotnet test ./tests");

    private static int ExecuteCommand(string command)
    {
        string response = CommandLine.Execute(command);
        Int32.TryParse(response, out int exitCode);

        return exitCode;
    }
}
