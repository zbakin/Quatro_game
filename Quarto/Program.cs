using System;
using System.Collections.Generic;

namespace Quarto
{
    class Program
    {
        static int pieceNum = 0; // piece number
        static int locationNum = 0; // location on the board
 
        // dictionary with location number and piece number
        static Dictionary<int, int> dict = new Dictionary<int, int>();
        
        // Create array of objects of class Piece
        static Piece[] pieces = new Piece[17];
       

        static void Main(string[] args)
        {
            //VARIABLES
            bool gameIsOn = false;

            // create 16 peices with different characteristics
            pieces[1] = new Piece("black", "tall", "square", "withHole");
            pieces[2] = new Piece("black", "tall", "square", "withoutHole");
            pieces[3] = new Piece("black", "tall", "circle", "withHole");
            pieces[4] = new Piece("black", "tall", "circle", "withoutHole");
            pieces[5] = new Piece("black", "small", "square", "withHole");
            pieces[6] = new Piece("black", "small", "square", "withoutHole");
            pieces[7] = new Piece("black", "small", "circle", "withHole");
            pieces[8] = new Piece("black", "small", "circle", "withoutHole");
            pieces[9] = new Piece("white", "tall", "square", "withHole");
            pieces[10] = new Piece("white", "tall", "square", "withoutHole");
            pieces[11] = new Piece("white", "tall", "circle", "withHole");
            pieces[12] = new Piece("white", "tall", "circle", "withoutHole");
            pieces[13] = new Piece("white", "small", "square", "withHole");
            pieces[14] = new Piece("white", "small", "square", "withoutHole");
            pieces[15] = new Piece("white", "small", "circle", "withHole");
            pieces[16] = new Piece("white", "small", "circle", "withoutHole");

            //Create player class objects
            Player p1 = new Player();
            Player p2 = new Player();
            p1.turn = true;
            p2.turn = false;

            // fill in the dict with keys from 1 to 16 and 0 values
            for(int i = 1;i <= 16; i++)
            {
                dict.Add(i,0); // 0 value means the board is empty
            }

            gameIsOn = true;
            while (gameIsOn)
            {
                if (p1.turn)
                {
                    Console.WriteLine("***** Player 1 Turn *******");

                    // get data from the user
                    pieceNum = requestPiece();
                    locationNum = requestLocation();

                    dict[locationNum] = pieceNum; // add piece to the board

                    if (isGameOver())
                    {
                        gameIsOn = false;
                        Console.WriteLine();
                        Console.WriteLine("Player 1 Wins! THE GAME IS OVER");
                    }
                    else
                    {
                        // change the players turn
                        p1.turn = false;
                        p2.turn = true;
                    }
                }
                else if (p2.turn)
                {
                    Console.WriteLine("***** Player 2 Turn *******");

                    // get data from the user
                    pieceNum = requestPiece();
                    locationNum = requestLocation();

                    dict[locationNum] = pieceNum; // add piece to the board

                    if (isGameOver())
                    {
                        gameIsOn = false;
                        Console.WriteLine();
                        Console.WriteLine("Player 2 Wins! THE GAME IS OVER");
                    }
                    else
                    {
                        // change the players turn
                        p1.turn = true; 
                        p2.turn = false;
                    }
                }
            }
        }

        public static int requestPiece()
        {
            int piece = 0;
            bool correctInput = false;
            while (!correctInput) // ask until the valid number is entered
            {
                Console.WriteLine();
                Console.WriteLine("Opponent has to choose the piece(number between 1 and 16): ");
                piece = int.Parse(Console.ReadLine()); //read and convert to int
                if (piece >= 1 && piece <= 16 && isPieceAvailable(piece))
                {
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("The number is out of range or the piece has already been used! Try again.");
                }
            }
            return piece;
        }
        public static int requestLocation()
        { 
            int location = 0;
            bool correctInput = false;
            Console.WriteLine("***** Board ******");
            Console.WriteLine("1  -  2 -  3 -  4");
            Console.WriteLine("5  -  6 -  7 -  8");
            Console.WriteLine("9  - 10 - 11 - 12");
            Console.WriteLine("13 - 14 - 15 - 16");
            while (!correctInput) // ask until the valid number is entered
            {
                Console.WriteLine();
                Console.WriteLine("Choose location of the board(number between 1 and 16): ");
                location = int.Parse(Console.ReadLine()); //read and convert to int
                if (location >= 1 && location <= 16 && isLocationAvailable(location))
                {
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("The number is out of range or this location is not available! Try again.");
                }
            }
            return location;
        }
        public static bool isPieceAvailable(int num)
        {
            if (dict.ContainsValue(num))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool isLocationAvailable(int loc)
        {
            if(dict[loc] == 0) // if the value of the key 'loc' is 0
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // CHECK FOR A WINNING COMBINATION
        public static bool isGameOver()
        {
            //first raw
            if(dict[1] != 0 && dict[2] != 0 && //check if there are 4 pieces on the board on that raw
               dict[3] != 0 && dict[4] != 0)
            {
                if (pieces[dict[1]].color == pieces[dict[2]].color &&
                    pieces[dict[2]].color == pieces[dict[3]].color &&
                    pieces[dict[3]].color == pieces[dict[4]].color)
                {
                    return true;
                }
                if (pieces[dict[1]].size == pieces[dict[2]].size &&
                    pieces[dict[2]].size == pieces[dict[3]].size &&
                    pieces[dict[3]].size == pieces[dict[4]].size)
                {
                    return true;
                }
                if (pieces[dict[1]].shape == pieces[dict[2]].shape &&
                    pieces[dict[2]].shape == pieces[dict[3]].shape &&
                    pieces[dict[3]].shape == pieces[dict[4]].shape)
                {
                    return true;
                }
                if (pieces[dict[1]].hole == pieces[dict[2]].hole &&
                    pieces[dict[2]].hole == pieces[dict[3]].hole &&
                    pieces[dict[3]].hole == pieces[dict[4]].hole)
                {
                    return true;
                }
            }
            //second raw
            if (dict[5] != 0 && dict[6] != 0 &&
              dict[7] != 0 && dict[8] != 0)
            {
                if(pieces[dict[5]].color == pieces[dict[6]].color &&
                    pieces[dict[6]].color == pieces[dict[7]].color &&
                    pieces[dict[7]].color == pieces[dict[8]].color)
                {
                    return true;
                }
                if (pieces[dict[5]].size == pieces[dict[6]].size &&
                    pieces[dict[6]].size == pieces[dict[7]].size &&
                    pieces[dict[7]].size == pieces[dict[8]].size)
                {
                    return true;
                }
                if (pieces[dict[5]].shape == pieces[dict[6]].shape &&
                    pieces[dict[6]].shape == pieces[dict[7]].shape &&
                    pieces[dict[7]].shape == pieces[dict[8]].shape)
                {
                    return true;
                }
                if (pieces[dict[5]].hole == pieces[dict[6]].hole &&
                    pieces[dict[6]].hole == pieces[dict[7]].hole &&
                    pieces[dict[7]].hole == pieces[dict[8]].hole)
                {
                    return true;
                }
            }
            //third raw
            if (dict[9] != 0 && dict[10] != 0 &&
              dict[11] != 0 && dict[12] != 0)
            {
                if (pieces[dict[9]].color == pieces[dict[10]].color &&
                    pieces[dict[10]].color == pieces[dict[11]].color &&
                    pieces[dict[11]].color == pieces[dict[12]].color)
                {
                    return true;
                }
                if (pieces[dict[9]].size == pieces[dict[10]].size &&
                    pieces[dict[10]].size == pieces[dict[11]].size &&
                    pieces[dict[11]].size == pieces[dict[12]].size)
                {
                    return true;
                }
                if (pieces[dict[9]].shape == pieces[dict[10]].shape &&
                    pieces[dict[10]].shape == pieces[dict[11]].shape &&
                    pieces[dict[11]].shape == pieces[dict[12]].shape)
                {
                    return true;
                }
                if (pieces[dict[9]].hole == pieces[dict[10]].hole &&
                    pieces[dict[10]].hole == pieces[dict[11]].hole &&
                    pieces[dict[11]].hole == pieces[dict[12]].hole)
                {
                    return true;
                }
            }
            //fourth raw
            if (dict[13] != 0 && dict[14] != 0 &&
              dict[15] != 0 && dict[16] != 0)
            {
                if (pieces[dict[13]].color == pieces[dict[14]].color &&
                    pieces[dict[14]].color == pieces[dict[15]].color &&
                    pieces[dict[15]].color == pieces[dict[16]].color)
                {
                    return true;
                }
                if (pieces[dict[13]].size == pieces[dict[14]].size &&
                    pieces[dict[14]].size == pieces[dict[15]].size &&
                    pieces[dict[15]].size == pieces[dict[16]].size)
                {
                    return true;
                }
                if (pieces[dict[13]].shape == pieces[dict[14]].shape &&
                    pieces[dict[14]].shape == pieces[dict[15]].shape &&
                    pieces[dict[15]].shape == pieces[dict[16]].shape)
                {
                    return true;
                }
                if (pieces[dict[13]].hole == pieces[dict[14]].hole &&
                    pieces[dict[14]].hole == pieces[dict[15]].hole &&
                    pieces[dict[15]].hole == pieces[dict[16]].hole)
                {
                    return true;
                }
            }
            //first column
            if (dict[1] != 0 && dict[5] != 0 &&
              dict[9] != 0 && dict[13] != 0)
            {
                if (pieces[dict[1]].color == pieces[dict[5]].color &&
                    pieces[dict[5]].color == pieces[dict[9]].color &&
                    pieces[dict[9]].color == pieces[dict[13]].color)
                {
                    return true;
                }
                if (pieces[dict[1]].shape == pieces[dict[5]].shape &&
                    pieces[dict[5]].shape == pieces[dict[9]].shape &&
                    pieces[dict[9]].shape == pieces[dict[13]].shape)
                {
                    return true;
                }
                if (pieces[dict[1]].size == pieces[dict[5]].size &&
                    pieces[dict[5]].size == pieces[dict[9]].size &&
                    pieces[dict[9]].size == pieces[dict[13]].size)
                {
                    return true;
                }
                if (pieces[dict[1]].hole == pieces[dict[5]].hole &&
                    pieces[dict[5]].hole == pieces[dict[9]].hole &&
                    pieces[dict[9]].hole == pieces[dict[13]].hole)
                {
                    return true;
                }
            }
            //second column
            if (dict[2] != 0 && dict[6] != 0 &&
              dict[10] != 0 && dict[14] != 0)
            {
                if (pieces[dict[2]].color == pieces[dict[6]].color &&
                    pieces[dict[6]].color == pieces[dict[10]].color &&
                    pieces[dict[10]].color == pieces[dict[14]].color)
                {
                    return true;
                }
                if (pieces[dict[2]].shape == pieces[dict[6]].shape &&
                    pieces[dict[6]].shape == pieces[dict[10]].shape &&
                    pieces[dict[10]].shape == pieces[dict[14]].shape)
                {
                    return true;
                }
                if (pieces[dict[2]].size == pieces[dict[6]].size &&
                    pieces[dict[6]].size == pieces[dict[10]].size &&
                    pieces[dict[10]].size == pieces[dict[14]].size)
                {
                    return true;
                }
                if (pieces[dict[2]].hole == pieces[dict[6]].hole &&
                    pieces[dict[6]].hole == pieces[dict[10]].hole &&
                    pieces[dict[10]].hole == pieces[dict[14]].hole)
                {
                    return true;
                }
            }
            //third column
            if (dict[3] != 0 && dict[7] != 0 &&
              dict[11] != 0 && dict[15] != 0)
            {
                if (pieces[dict[3]].color == pieces[dict[7]].color &&
                    pieces[dict[7]].color == pieces[dict[11]].color &&
                    pieces[dict[11]].color == pieces[dict[15]].color)
                {
                    return true;
                }
                if (pieces[dict[3]].shape == pieces[dict[7]].shape &&
                    pieces[dict[7]].shape == pieces[dict[11]].shape &&
                    pieces[dict[11]].shape == pieces[dict[15]].shape)
                {
                    return true;
                }
                if (pieces[dict[3]].size == pieces[dict[7]].size &&
                    pieces[dict[7]].size == pieces[dict[11]].size &&
                    pieces[dict[11]].size == pieces[dict[15]].size)
                {
                    return true;
                }
                if (pieces[dict[3]].hole == pieces[dict[7]].hole &&
                    pieces[dict[7]].hole == pieces[dict[11]].hole &&
                    pieces[dict[11]].hole == pieces[dict[15]].hole)
                {
                    return true;
                }
            }
            //fourth column
            if (dict[4] != 0 && dict[8] != 0 &&
              dict[12] != 0 && dict[16] != 0)
            {
                if (pieces[dict[4]].color == pieces[dict[8]].color &&
                    pieces[dict[8]].color == pieces[dict[12]].color &&
                    pieces[dict[12]].color == pieces[dict[16]].color)
                {
                    return true;
                }
                if (pieces[dict[4]].shape == pieces[dict[8]].shape &&
                    pieces[dict[8]].shape == pieces[dict[12]].shape &&
                    pieces[dict[12]].shape == pieces[dict[16]].shape)
                {
                    return true;
                }
                if (pieces[dict[4]].size == pieces[dict[8]].size &&
                    pieces[dict[8]].size == pieces[dict[12]].size &&
                    pieces[dict[12]].size == pieces[dict[16]].size)
                {
                    return true;
                }
                if (pieces[dict[4]].hole == pieces[dict[8]].hole &&
                    pieces[dict[8]].hole == pieces[dict[12]].hole &&
                    pieces[dict[12]].hole == pieces[dict[16]].hole)
                {
                    return true;
                }
            }
            //first diagonal 
            if (dict[1] != 0 && dict[6] != 0 &&
              dict[11] != 0 && dict[16] != 0)
            {
                if (pieces[dict[1]].color == pieces[dict[6]].color &&
                    pieces[dict[6]].color == pieces[dict[11]].color &&
                    pieces[dict[11]].color == pieces[dict[16]].color)
                {
                    return true;
                }
                if (pieces[dict[1]].shape == pieces[dict[6]].shape &&
                    pieces[dict[6]].shape == pieces[dict[11]].shape &&
                    pieces[dict[11]].shape == pieces[dict[16]].shape)
                {
                    return true;
                }
                if (pieces[dict[1]].size == pieces[dict[6]].size &&
                    pieces[dict[6]].size == pieces[dict[11]].size &&
                    pieces[dict[11]].size == pieces[dict[16]].size)
                {
                    return true;
                }
                if (pieces[dict[1]].hole == pieces[dict[6]].hole &&
                     pieces[dict[6]].hole == pieces[dict[11]].hole &&
                     pieces[dict[11]].hole == pieces[dict[16]].hole)
                {
                    return true;
                }
            }
            //second diagonal 
            if (dict[4] != 0 && dict[7] != 0 &&
              dict[10] != 0 && dict[13] != 0)
            {
                if (pieces[dict[4]].color == pieces[dict[7]].color &&
                    pieces[dict[7]].color == pieces[dict[10]].color &&
                    pieces[dict[10]].color == pieces[dict[13]].color)
                {
                    return true;
                }
                if (pieces[dict[4]].shape == pieces[dict[7]].shape &&
                    pieces[dict[7]].shape == pieces[dict[10]].shape &&
                    pieces[dict[10]].shape == pieces[dict[13]].shape)
                {
                    return true;
                }
                if (pieces[dict[4]].size == pieces[dict[7]].size &&
                    pieces[dict[7]].size == pieces[dict[10]].size &&
                    pieces[dict[10]].size == pieces[dict[13]].size)
                {
                    return true;
                }
                if (pieces[dict[4]].hole == pieces[dict[7]].hole &&
                    pieces[dict[7]].hole == pieces[dict[10]].hole &&
                    pieces[dict[10]].hole == pieces[dict[13]].hole)
                {
                    return true;
                }
            }
            return false; // return false if there is no winning combination found
        }
    }

    class Piece
    {
        //CLASS VARIABLES
        public string color;   // black or white
        public string size; // tall or small
        public string shape; // square or circle
        public string hole; // with hole or without hole

        //CLASS CONSTRUCTOR
        public Piece(string color, string height, string shape, string hole)
        {
            this.color = color;
            this.size = height;
            this.shape = shape;
            this.hole = hole;
        }
    }

    class Player
    {
        // CLASS VARIABLES
        public bool turn;
    }
}
