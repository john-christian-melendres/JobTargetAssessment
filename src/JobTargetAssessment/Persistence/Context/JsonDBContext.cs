using Newtonsoft.Json;

namespace JobTargetAssessment.Persistence;


public class JsonDbContext
{
    private readonly string _filePath;

    public JsonDbContext(string fileName = "users.json")
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty", nameof(fileName));

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = Directory.GetParent(baseDirectory)!.Parent!.Parent!.Parent!.Parent!.Parent!.FullName;
        var dbFolder = Path.Combine(projectRoot, "db");

        Directory.CreateDirectory(dbFolder);
        _filePath = Path.Combine(dbFolder, fileName);

        Console.WriteLine($"Base Directory: {baseDirectory}");
        Console.WriteLine($"Project Root: {projectRoot}");
        Console.WriteLine($"DB Folder: {dbFolder}");
        Console.WriteLine($"Full File Path: {_filePath}");
        Console.WriteLine($"File Exists: {File.Exists(_filePath)}");
    }

    public async Task<IEnumerable<T>> ReadDataAsync<T>()
    {
        if (!File.Exists(_filePath)) return  Enumerable.Empty<T>();

        string json = await File.ReadAllTextAsync(_filePath);

        try
        {
            return JsonConvert.DeserializeObject<List<T>>(json) ??  Enumerable.Empty<T>();
        }
        catch (JsonException)
        {
            return Enumerable.Empty<T>();
        }
    }

    public async Task WriteDataAsync<T>(IEnumerable<T> data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        await File.WriteAllTextAsync(_filePath, json);
    }
}