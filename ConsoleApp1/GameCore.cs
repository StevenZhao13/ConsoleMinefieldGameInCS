using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Square
    {

        public int proximityCount;
        public bool isBomb;
        public bool isVisible;

        public Square(bool isBomb)
        {
            this.proximityCount = 0;
            this.isBomb = isBomb;
            this.isVisible = false;
        }
    }


    class GameCore
    {
        private Square[][] minefield;

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
        }


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


        /*
         */
        public void generateProximityMark()
        {
            for ( int i = 0; i < this.minefield.Length; i++ ) {
                for ( int j = 0; j < this.minefield.Length; j++ ) {
                    if ( this.getSpecifiedSquare(j, i).isBomb == true ) {
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

                    } else { }
                }
            }
        }



        /*
         * "Graphic" This function simplys prints out the board.
         */
        public void printBoard()
        {

            for ( int i = 0; i < this.minefield.Length; i++ ) {

                for ( int j = 0; j < this.minefield.Length; j++ ) {

                    if ( this.minefield[i][j].isVisible ) {
                        if ( this.minefield[i][j].isBomb ) {
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
    }
}
