using libs;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
         Stopwatch gameTimer = new Stopwatch();
         TimeSpan gameDuration = TimeSpan.FromSeconds(20);
        var runGame = true;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;
        engine.Setup(false);
        // Timer
       
        // Main game loop
        while (runGame)
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
                Console.WriteLine("Game over! Time Ended;");
                runGame =false;
              
            }

            // Calculate the remaining time and display it
            TimeSpan remainingTime = gameDuration - gameTimer.Elapsed;


            engine.Render();
            Console.WriteLine($"Time remaining: {remainingTime.TotalSeconds} seconds");



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
                runGame = false;
            }
                // Add a delay of 1 second
            Thread.Sleep(10);

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