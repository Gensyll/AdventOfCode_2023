/*
    Advent of Code 2023 - Day 3
    Matthew Wrobel
    2023-12-06
*/

using System.Text.RegularExpressions;

namespace AdventOfCode.Day3
{
    class Day3{
        static void Main(string[] args){
            //Get and set up input
            List<string> rawInput = File.ReadAllLines($"{Environment.CurrentDirectory}\\input_sample.txt").ToList();
            char[,] inputMap = new char[rawInput.Count, rawInput[0].Length];
            foreach(string inputRow in rawInput){                
                int charIndex = 0;
                foreach(char inputChar in inputRow.ToCharArray()){
                    inputMap[rawInput.IndexOf(inputRow), charIndex] = inputChar;
                    ++charIndex;
                }                
            }

            //Map output preview
            for(int mapRow = 0; mapRow <= inputMap.GetLength(0) - 1; ++mapRow){
                for(int mapCol = 0; mapCol <= inputMap.GetLength(1) - 1; ++mapCol){
                    Console.Write(inputMap[mapRow, mapCol]);
                }
                Console.WriteLine();
            }

            //TODO: Determine part numbers
            for(int mapRow = 0; mapRow <= inputMap.GetLength(0) - 1; ++mapRow){
                for(int mapCol = 0; mapCol <= inputMap.GetLength(1) - 1; ++mapCol){
                    List<char> digitList = new List<char>();
                    //Only proceed if current value is a digit
                    if(Char.IsNumber(inputMap[mapRow, mapCol])){
                        int mapColOffset = 0;
                        //Get length of number for checking
                        while(Char.IsNumber(inputMap[mapRow, mapCol + mapColOffset])){
                            digitList.Add(inputMap[mapRow, mapCol + mapColOffset]);
                            ++mapColOffset;
                        }

                        //Check spaces surrounding current number
                        bool isAdjacent = false;
                        for(int x = 0; x < mapColOffset; ++x){
                            if(!char.IsLetterOrDigit(inputMap[mapRow, mapCol + x - 1]) && !inputMap[mapRow, mapCol + x - 1].Equals(".")) isAdjacent = true;
                            if(!char.IsLetterOrDigit(inputMap[mapRow, mapCol + x]) && !inputMap[mapRow, mapCol + x].Equals(".")) isAdjacent = true;
                            if(!char.IsLetterOrDigit(inputMap[mapRow, mapCol + x + 1]) && !inputMap[mapRow, mapCol + x + 1].Equals(".")) isAdjacent = true;
                        }

                        //Skip the digits we found
                        mapCol += mapColOffset;
                        foreach(char x in digitList){
                            Console.Write(x);
                        }
                        Console.WriteLine();
                    }
                    
                }                
            } 
        }
    }
}