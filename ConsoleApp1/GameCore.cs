using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Square
    {

        public int proximityCount;
        public bool isNade;
        public bool isVisible;

        public Square(bool isBomb)
        {
            this.proximityCount = 0;
            this.isNade = isBomb;
            this.isVisible = false;
        }
    }


    class GameCore
    {
        private Square[][] minefield;

        public static int GAME_STATE = 0, LOST_STATE = 1, WIN_STATE = 2;
        public int state;

        public GameCore(int size, int percentage)
        {
            Random randomNumberGen = new Random();

            this.minefield = new Square[size][];

            for ( int i = 0; i < this.minefield.Length; i++ ) {
                Square[] tempColumn = new Square[size];
                this.minefield[i] = tempColumn;

                for ( int j = 0; j < this.minefield.Length; j++ ) {
                    Square curSquare;

                    int roll = randomNumberGen.Next(1,100);
                    if ( roll <= percentage ) curSquare = new Square(true);
                    else curSquare = new Square(false);

                    this.minefield[i][j] = curSquare;
                }
            }

            this.state = GameCore.GAME_STATE;
        }
        // End constructor func


        /*
         * 
         */
        public Square getSpecifiedSquare(int x, int y)
        {
            if ( 0 <= x || x < this.minefield[0].Length
                || 0 <= y || y < this.minefield.Length ) {
                return this.minefield[y][x];
            } else {
                return null;
            }
        }
        // End func


        /*
         */
        public void generateProximityMark()
        {
            for ( int i = 0; i < this.minefield.Length; i++ ) {
                for ( int j = 0; j < this.minefield.Length; j++ ) {
                    if ( this.getSpecifiedSquare(j, i).isNade == true ) {
                        Square tempPointer;
                        // Top left
                        tempPointer = this.getSpecifiedSquare(j - 1, i - 1);
                        if ( tempPointer != null ) tempPointer.proximityCount++;
                        // Top mid
                        tempPointer = this.getSpecifiedSquare(j, i - 1);
                        if ( tempPointer != null ) tempPointer.proximityCount++;
                        // Top right
                        tempPointer = this.getSpecifiedSquare(j + 1, i - 1);
                        if ( tempPointer != null ) tempPointer.proximityCount++;
                        // Mid left
                        tempPointer = this.getSpecifiedSquare(j - 1, i);
                        if ( tempPointer != null ) tempPointer.proximityCount++;
                        // Mid right
                        tempPointer = this.getSpecifiedSquare(j + 1, i);
                        if ( tempPointer != null ) tempPointer.proximityCount++;
                        // Bottom left
                        tempPointer = this.getSpecifiedSquare(j - 1, i + 1);
                        if ( tempPointer != null ) tempPointer.proximityCount++;
                        // Bottom mid
                        tempPointer = this.getSpecifiedSquare(j, i + 1);
                        if ( tempPointer != null ) tempPointer.proximityCount++;
                        // Bottom right
                        tempPointer = this.getSpecifiedSquare(j + 1, i + 1);
                        if ( tempPointer != null ) tempPointer.proximityCount++;

                    } else {
                    }
                }
            }
        }
        // End func


        /*
         * "Graphic" This function simplys prints out the board.
         */
        public void printBoard()
        {

            for ( int i = 0; i < this.minefield.Length; i++ ) {

                for ( int j = 0; j < this.minefield.Length; j++ ) {

                    if ( this.minefield[i][j].isVisible ) {
                        if ( this.minefield[i][j].isNade ) {
                            Console.Write("*");
                        } else {
                            Console.Write(this.minefield[i][j].proximityCount);
                        }

                    } else {
                        Console.Write("?");
                    }
                }
                Console.Write("\n");
            }
        }
        // End Func


        /*
         * RIP method.
         */
        public void NadeTripped()
        {
            this.state = GameCore.LOST_STATE;
            for ( int i = 0; i < this.minefield.Length; i++ ) {
                for ( int j = 0; j < this.minefield.Length; j++ ) {
                    this.minefield[i][j].isVisible = true;
                }
            }
        }
        // End Func


        /*
         */
        public void updateGameState()
        {
            bool isEnded = true;
            for ( int i = 0; i < this.minefield.Length && isEnded; i++ ) {
                for ( int j = 0; j < this.minefield.Length && isEnded; j++ ) {
                    if ( this.minefield[i][j].isNade == false
                        && this.minefield[i][j].isVisible == false ) {
                        isEnded = false;
                    } else {
                    }
                }
            }

        }
        // End Func


        /*
         * This is a self-contained function that read & parse player input from console
         * And further make the change to 
        */
        public void readPlayerInupt()
        {
            Console.WriteLine("Plz input your selection of place thru the following " +
                "format: x y");

            bool inputParsed = false;
            while ( !inputParsed ) {
                String inputLine = Console.ReadLine();
                String[] splitedInput = inputLine.Split();

                int x, y;
                if ( splitedInput.Length == 2
                    && int.TryParse(splitedInput[0], out x)
                    && int.TryParse(splitedInput[0], out y)
                    && this.getSpecifiedSquare(x - 1, y - 1) != null ) {

                    Square targetSq = this.getSpecifiedSquare(x, y);
                    if ( targetSq.isNade ) {
                        // RIP
                        this.NadeTripped();
                    } else {
                        targetSq.isVisible = true;
                    }

                    inputParsed = true;
                } else {
                }
            }
        }
        // End Func


        /*
         */
        public void mainProcess()
        {
            while ( this.state == GameCore.GAME_STATE ) {
                this.readPlayerInupt();
                this.updateGameState();
            }

            if ( this.state == GameCore.LOST_STATE ) {
                Console.WriteLine("Get rekt");
            } else if ( this.state == GameCore.WIN_STATE ) {
                Console.WriteLine("You are pretty good");
            } else {
                Console.WriteLine("Something went wrnog");
            }
        }
        // End Func


        /*
         * 
         * Main Func
         */ 
        public static void Main(String[] args)
        {
            GameCore inst = new GameCore(20, 20);

        }

    }



}
