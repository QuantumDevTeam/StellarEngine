namespace Stellar.Tools;

public class Program
{
    private static void CopySdkTemplate(string[] args)
    {
        throw new NotImplementedException();
    }

    public static void Main(string[] args)
    {
        switch (args[0])
        {
            case "SDKTemplateCreate":
                CopySdkTemplate(args);
                break;
            default:
                Console.WriteLine("Tool used is incorrect");
                break;
        }
    }
}