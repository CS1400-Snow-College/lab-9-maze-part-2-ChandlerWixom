// Chandler Wixom, 4/2/2025, lab 9 maze part 2

// Introduction
using System.ComponentModel;

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

// Active Running Loop
Console.SetCursorPosition(0,0);
bool pass;
do
{
    pass = Movement(KeyRead());

}
while (!pass);









// Movement methods ---------------------------------------------------------------


//Reads the key and returning a string of the key ---------------------------
string KeyRead()
{
    ConsoleKey key = Console.ReadKey(true).Key;
    return Convert.ToString(key);
}

// Wall Check
bool WallCheck(int top, int left)
{
    char temp = mapRows[Console.CursorTop - top][Console.CursorLeft + left];
    if ( temp == '*' || temp == '|')
    {
        return true;
    }
    else 
    {
        return false;
    }
}



// movement check sees if player is allowed to move -------------------------------
bool CanMove(string move)
{
    if (move == "up")
    {
        if (Console.CursorTop < 1)
        {
            return false;
        }
        else 
        {
            if (WallCheck(1,0))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else if (move == "down")
    {
        if (Console.CursorTop >= mapRows.Length - 1)
        {
            return false;
        }
        else 
        {
             if (WallCheck(-1,0))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else if (move == "right")
    {
        if (Console.CursorLeft >= mapRows[1].Length - 1)
        {
            return false;
        }
        else 
        {
             if (WallCheck(0,1))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else if (move == "left")
    {
        if (Console.CursorLeft <= 0)
        {
            return false;
        }
        else 
        {
             if (WallCheck(0,-1))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else
    {
        return false;
    }
}

// runs a movement check and if true then player moves ------------------------------------
bool Movement(string move)
{
    if (move == "Escape")
    {
        return true;
    }
    else if (move == "W" || move == "UpArrow")
    {
        if (CanMove("up"))
        Console.CursorTop--;

        return false;
    }
    else if (move == "S" || move == "DownArrow")
    {
        if (CanMove("down"))
        Console.CursorTop++;

        return false;
    }
    else if (move == "A" || move == "LeftArrow")
    {
        if (CanMove("left"))
        Console.CursorLeft--;

        return false;
    }
    else if (move == "D" || move == "RightArrow")
    {
        if (CanMove("right"))
        Console.CursorLeft++;

        return false;
    }
    else
    {
        return false;
    }
}


// Loop Writes Map --------------------------------------------------------------
void WriteMap(string[] map)
{
    foreach (var row in map)
    {
        Console.WriteLine(row);
    }
}