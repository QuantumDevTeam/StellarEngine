namespace Stellar.Tools;

abstract class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Incorrect using. for help: 'stellar help'");
            return 1;
        }

        if (args[0] == "install-sdk")
            return InstallSdk();

        Console.WriteLine("Unknown command. for help: 'stellar help'");
        return 1;
    }

    static int InstallSdk()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var dotnetRoot = Path.Combine(home, ".dotnet");
        var sdkPath = Path.Combine(dotnetRoot, "sdk-advertising", "stellar.sdk");

        Directory.CreateDirectory(sdkPath);

        Console.WriteLine($"Installing Stellar SDK workload to {sdkPath}");

        // дальше позже:
        // - распаковка data/workload
        // - установка STELLAR_ENGINE_PATH
        // - проверка dotnet sdk version

        Console.WriteLine("DONE (MVP)");
        return 0;
    }
}