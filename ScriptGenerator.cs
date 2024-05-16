using Newtonsoft.Json;

namespace ImmersiveWastelandArsenal
{
    public class ScriptGenerator
    {
        public static string GenerateGECKScript()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "ImmersiveWastelandArsenal.json");
            string jsonString = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(jsonString)) return string.Empty;

            List<Weapon> weapons = JsonConvert.DeserializeObject<List<Weapon>>(jsonString) ?? new List<Weapon>();
            if (weapons.Count == 0) return string.Empty;

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
                    geckScript.Add($"\t\t\tSetName \"{weapon.Immersive.Replace("\"", "%q")}\" {weapon.EditorID}");
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

            return String.Join('\n', geckScript);
        }

        public static string GenerateStaticTextScript()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "ImmersiveWastelandArsenal.json");
            string jsonString = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(jsonString)) return string.Empty;

            List<Weapon> weapons = JsonConvert.DeserializeObject<List<Weapon>>(jsonString) ?? new List<Weapon>();
            if (weapons.Count == 0) return string.Empty;

            List<string> textScript = new List<string>();
            textScript.Add("float fNameDefault");
            textScript.Add("Set fNameDefault To (GetINIFloat \"Options:fNameDefault\" \"ImmersiveWastelandArsenal.ini\")");
            textScript.Add(String.Empty);

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
                textScript.Add(String.Empty);
            }

            return String.Join('\n', textScript);
        }

        public static string GenerateDynamicTextScript()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "DynamicTextScript.txt");
            string textScript = File.ReadAllText(filePath);
            return textScript;
        }
    }
}
