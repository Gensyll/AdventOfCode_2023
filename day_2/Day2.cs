/*
    Advent of Code 2023 - Day 2
    Matthew Wrobel
    2023-12-05
*/

using System.Text.RegularExpressions;

namespace AdventOfCode.Day2
{
    class Day2{
        const int TOT_R_CUBES = 12;
        const int TOT_G_CUBES = 13;
        const int TOT_B_CUBES = 14;     

        struct Game{
            public int id;
            public int powerFewestCubes;
            public bool isPossible;
        }   

        static void Main(string[] args){
            //Part 1
            int sumOfIds = 0, sumOfPowers = 0;
            List<string> listOfGames = File.ReadAllLines(Environment.CurrentDirectory + "\\input.txt").ToList();
            foreach(string game in listOfGames){
                Game processedGame = ProcessGameString(game);
                if(processedGame.isPossible){
                    sumOfIds += processedGame.id;                    
                }
                //part2
                sumOfPowers += processedGame.powerFewestCubes;
            }

            Console.WriteLine("Total sum of IDs = " + sumOfIds);
            //Part 2
            Console.WriteLine("Total sum of powers = " + sumOfPowers);
        }

        static Game ProcessGameString(string gameInput){   
            Game newGame = new Game();
            newGame.isPossible = true;
            newGame.id = Int32.Parse(gameInput.SkipWhile(c => !char.IsDigit(c)).TakeWhile(char.IsDigit).ToArray());

            //Extract only game results
            Match match = Regex.Match(gameInput, "(?<=Game.*:\\s+)(.*)");
            List<string> cubePulls = match.Value.Split(";").ToList();

            //part2 - determine fewest required cube count
            int fewBlue = 0, fewRed = 0, fewGreen = 0;
            
            //Go through and determine if each game is possible            
            foreach(string roll in cubePulls){      
                int blueCount = 0, redCount = 0, greenCount = 0;          

                Match blueMatch = Regex.Match(roll, "(\\d+)(?= blue)");
                Match redMatch = Regex.Match(roll, "(\\d+)(?= red)");
                Match greenMatch = Regex.Match(roll, "(\\d+)(?= green)");

                blueCount = blueMatch.Success ? Int32.Parse(blueMatch.Value) : 0;
                redCount = redMatch.Success ? Int32.Parse(redMatch.Value) : 0;
                greenCount = greenMatch.Success ? Int32.Parse(greenMatch.Value) : 0;
                Console.Write("B-" + blueCount + " R-" + redCount + " G-" + greenCount + " | ");

                //part2
                if(blueCount > fewBlue) fewBlue = blueCount;
                if(redCount > fewRed) fewRed = redCount;
                if(greenCount > fewGreen) fewGreen = greenCount;

                if(blueCount > TOT_B_CUBES || greenCount > TOT_G_CUBES || redCount > TOT_R_CUBES)
                    newGame.isPossible = false;                               
            }
            
            newGame.powerFewestCubes = fewBlue * fewRed * fewGreen;
            Console.WriteLine("Power - " + newGame.powerFewestCubes + " - Possible?=" + newGame.isPossible);

            return newGame;
        }
    }
}
