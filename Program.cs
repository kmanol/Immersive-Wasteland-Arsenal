using ImmersiveWastelandArsenal;

Options options = new(OutputFlags.TextDynamic);

string scriptContents;
string scriptName;

try
{
    switch (options.Output) {
        case OutputFlags.GECK:
            scriptName = "ImmersiveWastelandArsenal.txt";
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