using System.IO;

string filePath = @"path\to\your\file.csv"; // Replace with your actual file path

try
{
    using StreamReader reader = new(filePath);
    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        string[] parts = line.Split(','); // Split by comma (adjust if your delimiter is different)

        // Access each part of the row
        string data1 = parts[0];
        string data2 = parts[1];
        // ... and so on for other columns

        // Do something with the data from each row
        Console.WriteLine($"Data1: {data1}, Data2: {data2}");
    }
}
catch (IOException e)
{
    Console.WriteLine($"Error reading file: {e.Message}");
}