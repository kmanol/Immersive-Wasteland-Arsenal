using System.IO;
using ImmersiveWastelandArsenal;
using Newtonsoft.Json;

try
{
    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "ImmersiveWastelandArsenal.json");
    string jsonString = File.ReadAllText(filePath);
    if (string.IsNullOrEmpty(jsonString)) return;

    List<Weapon> weapons = JsonConvert.DeserializeObject<List<Weapon>>(jsonString) ?? new List<Weapon>();
    if (weapons.Count == 0) return;

    List<string> geckScript = new List<string>();
    geckScript.Add("ScriptName NVMOD1ImmersiveWastelandArsenalScript");
    geckScript.Add("Begin GameMode");
    geckScript.Add("\tIf GetGameRestarted");
    geckScript.Add(String.Empty);
    geckScript.Add("\t\tfloat fNameDefault");
    geckScript.Add("\t\tSet fNameDefault To GetINIFloat \"Options:fNameDefault\"");
    geckScript.Add(String.Empty);

    foreach (Weapon weapon in weapons)
    {
        if (weapon.EditorID == null) continue;

        geckScript.Add($"\t\t;{weapon?.EditorID}");

        geckScript.Add($"\t\tIf fNameDefault == 0");
        geckScript.Add($"\t\t\t;{weapon?.Vanilla}");

        geckScript.Add($"\t\tElseIf fNameDefault == 1");
        if (!string.IsNullOrEmpty(weapon?.Immersive))
            geckScript.Add($"\t\t\tSetName \"{weapon.Immersive.Replace("\"","%q")}\" {weapon.EditorID}");
        else
            geckScript.Add($"\t\t\t;{weapon?.Vanilla}");

        geckScript.Add($"\t\tElseIf fNameDefault == 2");
        if (!string.IsNullOrEmpty(weapon?.Descriptive))
            geckScript.Add($"\t\t\tSetName \"{weapon.Descriptive.Replace("\"", "%q")}\" {weapon.EditorID}");
        else
            geckScript.Add($"\t\t\t;{weapon?.Vanilla}");

        geckScript.Add($"\t\tEndIf");
        geckScript.Add(String.Empty);
    }

    geckScript.Add("\tEndIf");
    geckScript.Add("End");

    filePath = Path.Combine(Directory.GetCurrentDirectory(), "ImmersiveWastelandArsenal.txt");
    File.WriteAllText(filePath, String.Join('\n', geckScript));
}
catch (IOException e)
{
    Console.WriteLine($"Error: {e.Message}");
}