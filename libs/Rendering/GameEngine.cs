using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;

namespace libs;

using System.Security.Cryptography;
using Newtonsoft.Json;

public sealed class GameEngine
{
    private static GameEngine? _instance;
    public IGameObjectFactory gameObjectFactory;

    public int currentLevel = 3;

    public static GameEngine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEngine();
            }
            return _instance;
        }
    }

    private GameEngine()
    {
        //INIT PROPS HERE IF NEEDED
        gameObjectFactory = new GameObjectFactory();
    }

    private GameObject? _focusedObject;

    private Map map = new Map();

    private List<GameObject> gameObjects = new List<GameObject>();


    public Map GetMap()
    {
        return map;
    }

    public GameObject GetFocusedObject()
    {
        return _focusedObject;
    }

    public bool endGame()
    {
        var Targets = gameObjects.OfType<Target>();
        var Boxes = gameObjects.OfType<Box>();
        int Hits = 0;

        foreach (var Target in Targets)
        {
            foreach (var Box in Boxes)
            {
                if (Box.PosX == Target.PosX && Box.PosY == Target.PosY) {
                    Hits++;
                }   
            }
        }
        return (Hits != Targets.Count());
    }

    public void Setup()
    {
        // Added for proper display of game characters
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic gameData = FileHandler.ReadJson();

        var Level = gameData.First;

        switch (currentLevel)
        {
            case (1):
                Level = gameData.First;
                break;
            case (2):
                Level = gameData.Second;
                break;
            case (3):
                Level = gameData.Third;
                break;
            default: 
                return;
        }

        gameObjects.Clear();
        map.MapWidth = gameData.map.width;
        map.MapHeight = gameData.map.height;

        foreach (var gameObject in Level.gameObjects)
        {
            AddGameObject(CreateGameObject(gameObject));
        }

        _focusedObject = gameObjects.OfType<Player>().First();

    }

    public void Render()
    {
        //Clean the map
        Console.Clear();
        Console.WriteLine($"Level: {currentLevel}");
        map.Initialize();

        PlaceGameObjects();

        //Render the map
        for (int i = 0; i < map.MapHeight; i++)
        {
            for (int j = 0; j < map.MapWidth; j++)
            {
                DrawObject(map.Get(i, j));
            }
            Console.WriteLine();
        }

        if (endGame() == false && currentLevel != 3)
        {
            Console.WriteLine("You completed the level!");
            Console.WriteLine("Press Enter to get to the next level!");
        }
    }

    public void Undo()
    {
        // Only step back possible
        foreach (var gameObject in gameObjects)
        {
            gameObject.PosX = gameObject.GetPrevPosX();
            gameObject.PosY = gameObject.GetPrevPosY();
        }
    }

    // Method to create GameObject using the factory from clients
    public GameObject CreateGameObject(dynamic obj)
    {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject)
    {
        gameObjects.Add(gameObject);
    }

    private void PlaceGameObjects()
    {
        gameObjects.ForEach(delegate (GameObject obj)
        {
            map.Set(obj);
        });
    }

    private void DrawObject(GameObject gameObject)
    {
        Console.ResetColor();

        if (gameObject != null)
        {
            Console.ForegroundColor = gameObject.Color;
            Console.Write(gameObject.CharRepresentation);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(' ');
        }
    }

    // Increase currentLevel if Level is finished and next Level exists  
    public void TryLoadNextLevel()
    {
        if ((endGame() == false) && (currentLevel < 3))
        {
            currentLevel++;
            Setup();
        }
    }
}