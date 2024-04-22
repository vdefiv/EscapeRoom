namespace libs;

public sealed class InputHandler
{

    private static InputHandler? _instance;
    private GameEngine engine;

    public static InputHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InputHandler();
            }
            return _instance;
        }
    }

    private InputHandler()
    {
        //INIT PROPS HERE IF NEEDED
        engine = GameEngine.Instance;
    }

    public void Handle(ConsoleKeyInfo keyInfo)
    {
        GameObject focusedObject = engine.GetFocusedObject();

        if (focusedObject != null)
        {
            // Handle keyboard input to move the player
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (focusedObject.Rotate == 1)
                    {
                        focusedObject.Move(0, -1);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (focusedObject.Rotate == 2)
                    {
                        focusedObject.Move(0, 1);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (focusedObject.Rotate == 3)
                    {
                        focusedObject.Move(-1, 0);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (focusedObject.Rotate == 4)
                    {
                        focusedObject.Move(1, 0);
                    }
                    break;
                // Key for undoing one step
                case ConsoleKey.Z:
                    engine.Undo();
                    break;
                // Key for loading next level if it exists
                case ConsoleKey.Enter:
                    engine.TryLoadNextLevel();
                    break;

                case ConsoleKey.S:
                    engine.SaveMap();
                    break;

                case ConsoleKey.L:
                    engine.loadSavedGame();
                    break;

                default:
                    break;
            }
        }
    }
}