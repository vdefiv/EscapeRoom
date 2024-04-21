using libs;

class Program
{
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;
        engine.Setup(false);

        // Main game loop
        while (true)
        {
            engine.Render();

            // Handle keyboard input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            inputHandler.Handle(keyInfo);
            // Checks if it's the last level
            if (engine.currentLevel == 3 && engine.endGame() == false)
            {
                engine.Render();
                Console.WriteLine("Game finished. All levels mastered!");
                break;
            }
        }
    }
}