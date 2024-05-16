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
    geckScript.Add("float fNameDefault");
    geckScript.Add("Set fNameDefault To (GetINIFloat \"Options:fNameDefault\" \"ImmersiveWastelandArsenal.ini\")");
    geckScript.Add(String.Empty);

    foreach (Weapon weapon in weapons)
    {
        if (weapon.EditorID == null) continue;

        geckScript.Add($";{weapon?.EditorID}");

        geckScript.Add($"If fNameDefault == 0");
        geckScript.Add($"\t;{weapon?.Vanilla}");

        geckScript.Add($"ElseIf fNameDefault == 1");
        if (!string.IsNullOrEmpty(weapon?.Immersive))
            geckScript.Add($"\tSetName (\"{weapon.Immersive.Replace("\"","%q")}\") {weapon.EditorID}");
        else
            geckScript.Add($"\t;{weapon?.Vanilla}");

        geckScript.Add($"ElseIf fNameDefault == 2");
        if (!string.IsNullOrEmpty(weapon?.Descriptive))
            geckScript.Add($"\tSetName (\"{weapon.Descriptive.Replace("\"", "%q")}\") {weapon.EditorID}");
        else
            geckScript.Add($"\t;{weapon?.Vanilla}");

        geckScript.Add($"EndIf");
        geckScript.Add(String.Empty);
    }

    filePath = Path.Combine(Directory.GetCurrentDirectory(), "gr_ImmersiveWastelandArsenal.txt");
    File.WriteAllText(filePath, String.Join('\n', geckScript));
}
catch (IOException e)
{
    Console.WriteLine($"Error: {e.Message}");
}