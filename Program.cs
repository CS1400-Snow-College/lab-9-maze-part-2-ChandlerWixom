// Chandler Wixom, 4/2/2025, lab 9 maze part 2

// Introduction
Console.BackgroundColor = ConsoleColor.DarkGray;
Console.ForegroundColor = ConsoleColor.Gray;
Console.Clear();

Console.WriteLine(" Hello, This is a maze game. Your job is to reach the (#) while avoiding the (%)\n\n Use \tw, a, s, d   or   Up, Down, Left, Right Arrow keys to move.\n\n Press Any key to start \n\tEsc to exit");
Console.ReadKey(true);

// Map
string[] mapRows = File.ReadAllLines("../../../maze.txt");
Console.Clear();
WriteMap(mapRows);
Console.ForegroundColor = ConsoleColor.DarkGreen;

// Write Map
void WriteMap(string[] map)
{
    foreach (var row in map)
    {
        Console.WriteLine(row);
    }
}