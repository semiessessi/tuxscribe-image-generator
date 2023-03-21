using System.CommandLine;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TIG
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

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
            description: "Font size to use for the glyphs",
            getDefaultValue: () => 512);

        static int Main(string[] args)
        {
            // this creates a new console window :(
            //AllocConsole();

            RootCommand rootCommand = new RootCommand("Generate tuxscribe style hieroglyph images from fonts");
            rootCommand.AddOption(FontOption);
            rootCommand.AddOption(OutputOption);
            rootCommand.AddOption(SizeOption);

            rootCommand.SetHandler(
                GenerateImages.Run,
                FontOption, OutputOption, SizeOption);

            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
