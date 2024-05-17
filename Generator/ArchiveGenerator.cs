using System;
using System.IO;
using System.IO.Compression;

namespace ImmersiveWastelandArsenal.Generator
{
    internal class ArchiveGenerator
    {
        public static void GenerateArchive(string scriptSourceFilePath)
        {
            string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"{Globals.ModName}");

            #region Config
            string configFolderPath = Path.Combine(rootFolderPath, "Config");

            if (Directory.Exists(configFolderPath)) Directory.Delete(configFolderPath, recursive: true);
            Directory.CreateDirectory(configFolderPath);

            List<string> configFileExtensions = new List<string>() { "ini", "json" };
            foreach (string fileExtension in configFileExtensions)
            {
                string configSourceFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", $"{Globals.ModName}.{fileExtension}");
                string configDestinationFilePath = Path.Combine(configFolderPath, $"{Globals.ModName}.{fileExtension}");
                File.Copy(configSourceFilePath, configDestinationFilePath, true);
            }
            #endregion

            #region Scripts
            string nvseFolderPath = Path.Combine(rootFolderPath, "NVSE");
            string scriptsFolderPath = Path.Combine(nvseFolderPath, "Plugins", "Scripts");

            if (Directory.Exists(nvseFolderPath)) Directory.Delete(nvseFolderPath, recursive: true);
            Directory.CreateDirectory(scriptsFolderPath);

            string scriptsSourceFilePath = Path.Combine(scriptSourceFilePath);
            string scriptsDestinationFilePath = Path.Combine(scriptsFolderPath, Path.GetFileName(scriptSourceFilePath));
            File.Copy(scriptsSourceFilePath, scriptsDestinationFilePath, true);
            #endregion

            string zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{Globals.ModName}.zip");
            if (File.Exists(zipFilePath)) File.Delete(zipFilePath);
            ZipFile.CreateFromDirectory(rootFolderPath, zipFilePath, CompressionLevel.Fastest, includeBaseDirectory: false);

            File.Delete(scriptSourceFilePath);
            Directory.Delete(rootFolderPath, recursive: true);
        }
    }
}
