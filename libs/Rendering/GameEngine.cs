using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;

namespace libs;

using System.Security.Cryptography;
using Newtonsoft.Json;

public sealed class GameEngine
{
    private static GameEngine? _instance;
    public IGameObjectFactory gameObjectFactory;
    public int AmountOfBoxes = 0;

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

<<<<<<< HEAD
    public bool endGame(){
        if(AmountOfBoxes == 0){
            return false;
        }else{
            return true;
        }
    }

    public void Setup(){
=======
    public void Setup()
    {
>>>>>>> 1de581a6a56574c1718e8649aa3b64e514db7d13

        //Added for proper display of game characters
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic gameData = FileHandler.ReadJson();

        map.MapWidth = gameData.map.width;
        map.MapHeight = gameData.map.height;

        foreach (var gameObject in gameData.First.gameObjects)
        {
            AddGameObject(CreateGameObject(gameObject));
        }

<<<<<<< HEAD
        // AmountOfBoxes = gameObjectFactory.GetAmountOfBoxes;
        
=======
>>>>>>> 1de581a6a56574c1718e8649aa3b64e514db7d13
        _focusedObject = gameObjects.OfType<Player>().First();

    }

    public void Render()
    {

        //Clean the map
        Console.Clear();

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
    }

    // Method to create GameObject using the factory from clients
    public GameObject CreateGameObject(dynamic obj)
    {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject)
    {
<<<<<<< HEAD
        // int currentAmountOfBoxes = gameObjectFactory.AmountOfBoxes;
        // currentAmountOfBoxes++;
        // ((GameObjectFactory)gameObjectFactory).SetAmountOfBoxes(currentAmountOfBoxes);
        AmountOfBoxes++;
=======
        if (gameObject.Type == GameObjectType.Box)
        {
            int currentAmountOfBoxes = gameObjectFactory.AmountOfBoxes;
            currentAmountOfBoxes++;
            ((GameObjectFactory)gameObjectFactory).SetAmountOfBoxes(currentAmountOfBoxes);
        }
        gameObjects.Add(gameObject);
>>>>>>> 1de581a6a56574c1718e8649aa3b64e514db7d13
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
}