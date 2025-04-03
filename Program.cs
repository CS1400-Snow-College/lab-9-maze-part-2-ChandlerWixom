// Chandler Wixom, 4/2/2025, lab 9 maze part 2

// Introduction
using System.ComponentModel;
using System.Net.Http.Headers;

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
(bool esc, bool moved) pass;
var badguyLocation = BadGuys();
Random random = new Random();
do

{
    
    pass = Movement(KeyRead());
    if (pass.moved)
        BadGuysMove();
}
while (!pass.esc);









// Movement methods ---------------------------------------------------------------


//Reads the key and returning a string of the key ---------------------------
string KeyRead()
{
    ConsoleKey key = Console.ReadKey(true).Key;
    string move = Convert.ToString(key);

     if (move == "Escape")
    {
        return "Escape";
    }
    else if (move == "W" || move == "UpArrow")
    {
    
        return "up";
        
    }
    else if (move == "S" || move == "DownArrow")
    {
        return "down";
    }
    else if (move == "A" || move == "LeftArrow")
    {
        return "left";
    }
    else if (move == "D" || move == "RightArrow")
    {
    
        return "right";
    }
    else
    {
        return "";
    }

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
(bool esc,bool moved) Movement(string move)
{
    if (move == "Escape")
    {
        return (true,false);
    }
    else if (move == "up")
    {
        if (CanMove(move))
        {
        Console.CursorTop--;
         return (false, true);
        }
        return (false, false);
        
    }
    else if (move == "down")
    {
        if (CanMove(move))
        {
        Console.CursorTop++;
         return (false, true);
        }
        return (false, false);

    }
    else if (move == "left")
    {
        if (CanMove(move))
        {
        Console.CursorLeft--;
            return (false, true);
        }
        return (false, false);

    }
    else if (move == "right")
    {
        if (CanMove(move))
        {
        Console.CursorLeft++;
            return (false, true);
        }
        return (false, false);

    }
    else
    {
        return (false,false);
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


// locate badguys
// look for % and remembers theri location
(int x, int y)[] BadGuys()
{
    (int x, int y)[] badGuys = new (int x, int y)[2];
int count =0;
for (int i = 0; i < mapRows.Length; i++)
{
    for (int j = 0; j < mapRows[i].Length; j++)
    {
        if (mapRows[i][j] == '%')
        {
            badGuys[count] = (j,i);
                count++;
        }
    }
}
return badGuys;
}

// bad guys move
void BadGuysMove()
{
    (int, int) temp = Console.GetCursorPosition();
    Console.ForegroundColor = ConsoleColor.DarkRed;
for (int i = 0; i < badguyLocation.Length; i++)
{
    
    string move = RandomMovement(); // temp
    Console.SetCursorPosition(badguyLocation[i].x,badguyLocation[i].y);
    

    if (CanMove(move))
    {
    Console.Write(" ");

    Movement(move);
            Console.CursorLeft--;
            badguyLocation[i] = Console.GetCursorPosition();
            Console.Write("%");
            
    }
    
}
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.SetCursorPosition(temp.Item1,temp.Item2);
}


string RandomMovement()
{
    int randind = random.Next(0,4);

    if (randind == 0)
    {
        return "up";
    }
    else if (randind == 1)
    {
        return "down";
    }
    else if (randind == 2) 
    {
        return "right";
    }
    else 
    {
        return "left";
    }
}