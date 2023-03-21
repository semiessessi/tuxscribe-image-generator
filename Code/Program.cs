using System.CommandLine;

namespace TIG
{
    class Program
    {
        private static Option<string> FontOption = new Option<string>(
            name: "--font",
            description: "The font to use",
            getDefaultValue: () => "Segoe UI Historic");
        private static Option<string> OutputOption = new Option<string>(
            name: "--output",
            description: "The path to write the images to",
            getDefaultValue: () => ".\\out\\");
        private static Option<int> SizeOption = new Option<int>(
            name: "--size",
            description: "Target height in pixels for a full sized glyph",
            getDefaultValue: () => 512);

        static int Main(string[] args)
        {
            RootCommand rootCommand = new RootCommand("Sample app for System.CommandLine");
            rootCommand.AddOption(FontOption);
            rootCommand.AddOption(OutputOption);
            rootCommand.AddOption(SizeOption);

            rootCommand.SetHandler(
                GenerateImages.Run,
                FontOption, OutputOption, SizeOption);

            return rootCommand.Invoke(args);
        }
    }
}
