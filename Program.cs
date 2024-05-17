using ImmersiveWastelandArsenal;
using ImmersiveWastelandArsenal.Generator;

Console.WriteLine("Enter Output Type:");
Console.WriteLine("0: GECK Script (esp)");
Console.WriteLine("1: Static Text Script (espless - scriptrunner)");
Console.WriteLine("2: Dynamic Text Script (espless - scriptrunner)");

string? sUserInput = Console.ReadLine();
Int32.TryParse(sUserInput, out int iUserInput);

Options options = new((OutputFlags)iUserInput);

string scriptName;
string scriptContents;

try
{
    switch (options.Output) {
        case OutputFlags.GECK:
            scriptName = $"NVMOD1{Globals.ModName}Script.txt";
            scriptContents = ScriptGenerator.GenerateGECKScript();
            break;
        case OutputFlags.TextStatic:
            scriptName = $"gr_{Globals.ModName}.txt";
            scriptContents = ScriptGenerator.GenerateStaticTextScript();
            break;
        case OutputFlags.TextDynamic:
        default:
            scriptName = $"gr_{Globals.ModName}.txt";
            scriptContents = ScriptGenerator.GenerateDynamicTextScript();
            break;
    }

    string scriptFilePath = Path.Combine(Directory.GetCurrentDirectory(), scriptName);
    File.WriteAllText(scriptFilePath, String.Join('\n', scriptContents));

    switch (options.Output)
    {
        case OutputFlags.TextStatic:
        case OutputFlags.TextDynamic:
            ArchiveGenerator.GenerateArchive(scriptFilePath);
            break;
    }
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}