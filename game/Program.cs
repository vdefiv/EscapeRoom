using libs;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
         Stopwatch gameTimer;
         TimeSpan gameDuration;
        gameTimer = new Stopwatch();
        gameDuration = TimeSpan.FromSeconds(20);
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;
        engine.Setup(false);
        // Timer
       
        // Main game loop
        while (true)
        {
            Console.Clear();
            if (!gameTimer.IsRunning)
            {
                gameTimer.Start();
            }

            // Check if the game time has expired
            if (gameTimer.Elapsed >= gameDuration)
            {
                // Stop the game
                gameTimer.Stop();
                Console.WriteLine("Game over!");
                return;
            }

            // Calculate the remaining time and display it
            TimeSpan remainingTime = gameDuration - gameTimer.Elapsed;
            Console.WriteLine($"Time remaining: {remainingTime.TotalSeconds} seconds");


            engine.Render();


            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                inputHandler.Handle(keyInfo);
            }

            // Checks if it's the last level
            // if (engine.currentLevel == 3 && engine.endGame() == false)
            // {
            //     engine.Render();
            //     Console.WriteLine("Game finished. All levels mastered!");
            //     break;
            // }

            if (engine.endGame() == false)
            {
                engine.Render();
                Console.WriteLine("You escaped!");
                break;
            }
                // Add a delay of 1 second
    Thread.Sleep(50);

        }
        // Main game loop
        // while (true)
        // {
        //     engine.Render();

        //     // Handle keyboard input
        //     ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        //     inputHandler.Handle(keyInfo);

        //     // Checks if it's the last level
        //     // if (engine.currentLevel == 3 && engine.endGame() == false)
        //     // {
        //     //     engine.Render();
        //     //     Console.WriteLine("Game finished. All levels mastered!");
        //     //     break;
        //     // }

        //     if (engine.endGame() == false)
        //     {
        //         engine.Render();
        //         Console.WriteLine("You escaped!");
        //         break;
        //     }
        // }
    }
}