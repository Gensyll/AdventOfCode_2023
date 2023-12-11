/*
    Advent of Code 2023 - Day 3
    Matthew Wrobel
    2023-12-06
*/

using System.Data;
using System.Diagnostics.SymbolStore;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day3
{
    class Day3{
        static void Main(string[] args){
            int sum = 0, gearSum = 0;

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

            //Map preview
            for(int mapRow = 0; mapRow <= inputMap.GetLength(0) - 1; ++mapRow){
                for(int mapCol = 0; mapCol <= inputMap.GetLength(1) - 1; ++mapCol){
                    Console.Write(inputMap[mapRow, mapCol]);
                }
                Console.WriteLine();
            }

            //part2
            List<((int, int), int)> gearMap = new List<((int, int), int)>();

            //TODO: Determine part numbers
            for(int mapRow = 0; mapRow <= inputMap.GetLength(0) - 1; ++mapRow){
                for(int mapCol = 0; mapCol <= inputMap.GetLength(1) - 1; ++mapCol){
                    List<char> digitList = new List<char>();
                    //Only proceed if current value is a digit
                    if(char.IsNumber(inputMap[mapRow, mapCol])){
                        int mapColOffset = 0;
                        //Get length of number and store for potential summing
                        while(mapCol + mapColOffset < inputMap.GetLength(1) && char.IsNumber(inputMap[mapRow, mapCol + mapColOffset])){
                            digitList.Add(inputMap[mapRow, mapCol + mapColOffset]);
                            ++mapColOffset;
                        }

                        int currNumber = int.Parse(digitList.ToArray());
                        Console.Write($"Checking {currNumber}.. ");

                        //Check spaces surrounding current number
                        //part2 -- check for * and track in gearMap 
                        bool isAdjacent = false;
                        for(int x = 0; x < mapColOffset; ++x){
                            if(x == 0 && mapCol + x - 1 >= 0){   //Check centre-left if valid leftmost digit
                                if(IsSymbol(inputMap[mapRow, mapCol - 1])) {
                                        isAdjacent = true;
                                        if(inputMap[mapRow, mapCol - 1].Equals('*')) gearMap.Add(((mapRow, mapCol - 1), currNumber));
                                    }
                            }                            
                            if(x == mapColOffset - 1 && mapCol + x + 1 < inputMap.GetLength(1)){    //Check centre-right if valid rightmost digit
                                if(IsSymbol(inputMap[mapRow, mapCol + x + 1])) {
                                    isAdjacent = true;      
                                    if(inputMap[mapRow, mapCol + x + 1].Equals('*')) gearMap.Add(((mapRow, mapCol + x + 1), currNumber));
                                }                           
                            }

                            if(mapRow - 1 >= 0){ //Check above
                                if(x == 0 && mapCol + x - 1 >= 0){   //Check top-left
                                    if(IsSymbol(inputMap[mapRow - 1, mapCol - 1])) {
                                        isAdjacent = true;
                                        if(inputMap[mapRow - 1, mapCol - 1].Equals('*')) gearMap.Add(((mapRow - 1, mapCol - 1), currNumber));
                                    }
                                }                            
                                if(x == mapColOffset - 1 && mapCol + x + 1 < inputMap.GetLength(1)){    //Check top-right
                                    if(IsSymbol(inputMap[mapRow - 1, mapCol + x + 1])) {
                                        isAdjacent = true;      
                                        if(inputMap[mapRow - 1, mapCol + x + 1].Equals('*')) gearMap.Add(((mapRow - 1, mapCol + x + 1), currNumber));                   
                                    }        
                                }
                                if(IsSymbol(inputMap[mapRow - 1, mapCol + x])) {
                                    isAdjacent = true;   //Check top
                                    if(inputMap[mapRow - 1, mapCol + x].Equals('*')) gearMap.Add(((mapRow - 1, mapCol + x), currNumber));
                                }
                            }  

                            if(mapRow + 1 < inputMap.GetLength(0)){ //Check below
                                if(x == 0 && mapCol + x - 1 >= 0){   //Check bottom-left if valid leftmost digit
                                    if(IsSymbol(inputMap[mapRow + 1, mapCol - 1])) {
                                        isAdjacent = true;
                                        if(inputMap[mapRow + 1, mapCol - 1].Equals('*')) gearMap.Add(((mapRow + 1, mapCol - 1), currNumber));
                                    }
                                }                            
                                if(x == mapColOffset - 1 && mapCol + x + 1 < inputMap.GetLength(1)){    //Check bottom-right if valid rightmost digit
                                    if(IsSymbol(inputMap[mapRow + 1, mapCol + x + 1])) {
                                        isAdjacent = true;              
                                        if(inputMap[mapRow + 1, mapCol + x + 1].Equals('*')) gearMap.Add(((mapRow + 1, mapCol + x + 1), currNumber));                   
                                    }
                                }
                                if(IsSymbol(inputMap[mapRow + 1, mapCol + x])) {
                                    isAdjacent = true;   //Check bottom
                                    if(inputMap[mapRow + 1, mapCol + x].Equals('*')) gearMap.Add(((mapRow + 1, mapCol + x), currNumber));
                                }
                            }
                        }

                        if(isAdjacent){
                            Console.Write($"Adjacent, adding {currNumber} to {sum}.");
                            sum += currNumber;                            
                        }

                        //Skip the digits we found
                        mapCol += mapColOffset;
                        Console.WriteLine();                     
                    }
                }                
            } 

            Console.Write($"Gears found: ");
            foreach(((int, int), int) kvp in gearMap)
            {
                foreach(((int, int), int) compkvp in gearMap)
                {
                    Console.Write($"Checking {{{kvp.Item1.Item1},{kvp.Item1.Item2}}}/{kvp.Item2} against {{{compkvp.Item1.Item1},{compkvp.Item1.Item2}}}/{compkvp.Item2}... ");
                    if(kvp.Item1.Item1 == compkvp.Item1.Item1 && kvp.Item1.Item2 == compkvp.Item1.Item2){
                        Console.WriteLine($"Match! Ratio is {kvp.Item2 * compkvp.Item2}");
                        gearSum += kvp.Item2 * compkvp.Item2;
                        gearMap.Remove(compkvp); gearMap.Remove(kvp);
                    }else{
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Total sum = {sum}");
            Console.WriteLine($"Total gear ratio = {gearSum}");
        }

        static bool IsSymbol(char input){            
            return !char.IsLetterOrDigit(input) && !input.Equals('.');
        }        
    }
}