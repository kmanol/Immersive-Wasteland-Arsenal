using ImmersiveWastelandArsenal;

Console.WriteLine("Enter Output Type:");
Console.WriteLine("0: GECK Script (esp)");
Console.WriteLine("1: Static Text Script (espless - scriptrunner)");
Console.WriteLine("2: Dynamic Text Script (espless - scriptrunner)");

string? sUserInput = Console.ReadLine();
Int32.TryParse(sUserInput, out int iUserInput);

Options options = new((OutputFlags)iUserInput);

string scriptContents;
string scriptName;

try
{
    switch (options.Output) {
        case OutputFlags.GECK:
            scriptName = "NVMOD1ImmersiveWastelandArsenalScript.txt";
            scriptContents = ScriptGenerator.GenerateGECKScript();
            break;
        case OutputFlags.TextStatic:
            scriptName = "gr_ImmersiveWastelandArsenal.txt";
            scriptContents = ScriptGenerator.GenerateStaticTextScript();
            break;
        case OutputFlags.TextDynamic:
        default:
            scriptName = "gr_ImmersiveWastelandArsenal.txt";
            scriptContents = ScriptGenerator.GenerateDynamicTextScript();
            break;
    }

    string filePath = Path.Combine(Directory.GetCurrentDirectory(), scriptName);
    File.WriteAllText(filePath, String.Join('\n', scriptContents));
}
catch (IOException e)
{
    Console.WriteLine($"Error: {e.Message}");
}