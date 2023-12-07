/*
    Advent of Code 2023 - Day 1
    Matthew Wrobel
    2023-12-06
*/

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
                    
                    
                }                
            } 
        }
    }
}