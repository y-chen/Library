#load "logger.csx"

public class Util
{
    public static string CommandLineArgument(IList<string> args, int position)
    {
        if (args.Count() >= position + 1)
        {
            return args[position];
        }

        return string.Empty;
    }

    public static string GetCommitMessage(string commitMessageFilePath)
    {
        return File.ReadAllLines(commitMessageFilePath)[0];
    }
}
