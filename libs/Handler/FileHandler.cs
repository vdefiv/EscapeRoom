using System.Reflection.Metadata.Ecma335;

namespace libs;

using Newtonsoft.Json;

public static class FileHandler
{
    private static string filePath;
    private readonly static string envVar = "GAME_SETUP_PATH";
    private readonly static string envVar_Saved = "GAME_SAVE_PATH";

    private static bool loadSavedGameBool = false;
    public static bool LoadSavedGame
    {
        get { return loadSavedGameBool; }
        set { loadSavedGameBool = value; }
    }
    
    static FileHandler()
    {
        Initialize();
    }

    private static void Initialize()
    {
        
        if(loadSavedGameBool){
            if(Environment.GetEnvironmentVariable(envVar_Saved) != null){
            filePath = Environment.GetEnvironmentVariable(envVar_Saved);
            loadSavedGameBool = false;
            Console.WriteLine("Loading saved game");
            }
        }else{
            if(Environment.GetEnvironmentVariable(envVar) != null){
            filePath = Environment.GetEnvironmentVariable(envVar);
            Console.WriteLine("Loading new game");
            };   
        }
    }

    public static dynamic ReadJson()
    {
        Initialize();
        if (string.IsNullOrEmpty(filePath))
        {
            throw new InvalidOperationException("JSON file path not provided in environment variable");
        }

        try
        {
            string jsonContent = File.ReadAllText(filePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonContent);
            return jsonData;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException($"JSON file not found at path: {filePath}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading JSON file: {ex.Message}");
        }
    }



    public static void SaveJson(List<GameObject> gameObjects)
    {
        File.WriteAllText("SavedGame.json", string.Empty);
        var jsonData = new
        {
            map = new
            {
                width = 10,
                height = 8
            },
            First = new
            {
                gameObjects = gameObjects.OrderBy(obj => obj.Type == 0 ? 1 : 0).ToList()
            }
        };

        try
        {
            string json = JsonConvert.SerializeObject(jsonData);
            File.WriteAllText("SavedGame.json", json);
            Console.WriteLine("wrote into json file");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}
