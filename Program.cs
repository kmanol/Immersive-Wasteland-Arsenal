using ImmersiveWastelandArsenal;
using ImmersiveWastelandArsenal.Generator;

try
{
    #region Options
    Console.WriteLine("Enter Output Type:");
    Console.WriteLine("0: GECK Script (esp)");
    Console.WriteLine("1: Static Text Script (espless - scriptrunner)");
    Console.WriteLine("2: Dynamic Text Script (espless - scriptrunner)");

    string? sUserInput = Console.ReadLine();
    Int32.TryParse(sUserInput, out int iUserInput);

    Options options = new((OutputFlags)iUserInput);
    #endregion

    string scriptFilePath;

    switch (options.Output) {
        case OutputFlags.GECK:
            scriptFilePath = ScriptGenerator.GenerateGECKScript();
            break;
        case OutputFlags.TextStatic:
            scriptFilePath = ScriptGenerator.GenerateStaticTextScript();
            break;
        case OutputFlags.TextDynamic:
        default:
            scriptFilePath = ScriptGenerator.GenerateDynamicTextScript();
            break;
    }

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