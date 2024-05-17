using Newtonsoft.Json;

namespace ImmersiveWastelandArsenal.Generator
{
    public class ScriptGenerator
    {
        public static string GenerateGECKScript()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", $"{Globals.ModName}.json");
            string jsonString = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(jsonString)) return string.Empty;

            List<Weapon> weapons = JsonConvert.DeserializeObject<List<Weapon>>(jsonString) ?? new List<Weapon>();
            if (weapons.Count == 0) return string.Empty;

            List<string> geckScript = new List<string>();
            geckScript.Add($"ScriptName NVMOD1{Globals.ModName}Script");
            geckScript.Add("Begin GameMode");
            geckScript.Add("\tIf GetGameRestarted");
            geckScript.Add(string.Empty);
            geckScript.Add("\t\tfloat fNameDefault");
            geckScript.Add("\t\tSet fNameDefault To GetINIFloat \"Options:fNameDefault\"");
            geckScript.Add(string.Empty);

            foreach (Weapon weapon in weapons)
            {
                if (weapon.EditorID == null) continue;

                geckScript.Add($"\t\t;{weapon?.EditorID}");

                geckScript.Add($"\t\tIf fNameDefault == 0");
                geckScript.Add($"\t\t\t;{weapon?.Vanilla}");

                geckScript.Add($"\t\tElseIf fNameDefault == 1");
                if (!string.IsNullOrEmpty(weapon?.Immersive))
                    geckScript.Add($"\t\t\tSetName \"{weapon.Immersive.Replace("\"", "%q")}\" {weapon.EditorID}");
                else
                    geckScript.Add($"\t\t\t;{weapon?.Vanilla}");

                geckScript.Add($"\t\tElseIf fNameDefault == 2");
                if (!string.IsNullOrEmpty(weapon?.Descriptive))
                    geckScript.Add($"\t\t\tSetName \"{weapon.Descriptive.Replace("\"", "%q")}\" {weapon.EditorID}");
                else
                    geckScript.Add($"\t\t\t;{weapon?.Vanilla}");

                geckScript.Add($"\t\tEndIf");
                geckScript.Add(string.Empty);
            }

            geckScript.Add("\tEndIf");
            geckScript.Add("End");

            string scriptFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"NVMOD1{Globals.ModName}Script.txt");
            File.WriteAllText(scriptFilePath, string.Join('\n', geckScript));

            return scriptFilePath;
        }

        public static string GenerateStaticTextScript()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", $"{Globals.ModName}.json");
            string jsonString = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(jsonString)) return string.Empty;

            List<Weapon> weapons = JsonConvert.DeserializeObject<List<Weapon>>(jsonString) ?? new List<Weapon>();
            if (weapons.Count == 0) return string.Empty;

            List<string> textScript = new List<string>();
            textScript.Add("float fNameDefault");
            textScript.Add($"Set fNameDefault To (GetINIFloat \"Options:fNameDefault\" \"{Globals.ModName}.ini\")");
            textScript.Add(string.Empty);

            foreach (Weapon weapon in weapons)
            {
                if (weapon.EditorID == null) continue;

                textScript.Add($";{weapon?.EditorID}");

                textScript.Add($"If fNameDefault == 0");
                textScript.Add($"\t;{weapon?.Vanilla}");

                textScript.Add($"ElseIf fNameDefault == 1");
                if (!string.IsNullOrEmpty(weapon?.Immersive))
                    textScript.Add($"\tSetName (\"{weapon.Immersive.Replace("\"", "%q")}\") {weapon.EditorID}");
                else
                    textScript.Add($"\t;{weapon?.Vanilla}");

                textScript.Add($"ElseIf fNameDefault == 2");
                if (!string.IsNullOrEmpty(weapon?.Descriptive))
                    textScript.Add($"\tSetName (\"{weapon.Descriptive.Replace("\"", "%q")}\") {weapon.EditorID}");
                else
                    textScript.Add($"\t;{weapon?.Vanilla}");

                textScript.Add($"EndIf");
                textScript.Add(string.Empty);
            }

            string scriptFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"gr_{Globals.ModName}.txt");
            File.WriteAllText(scriptFilePath, string.Join('\n', textScript));

            return scriptFilePath;
        }

        public static string GenerateDynamicTextScript()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Script", "DynamicTextScript.txt");
            string textScript = File.ReadAllText(filePath);

            string scriptFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"gr_{Globals.ModName}.txt");
            File.WriteAllText(scriptFilePath, textScript);

            return scriptFilePath;
        }
    }
}
