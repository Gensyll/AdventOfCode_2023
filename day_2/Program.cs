using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day2
{
    class Program{
        const int TOT_R_CUBES = 12;
        const int TOT_G_CUBES = 13;
        const int TOT_B_CUBES = 14;     

        struct Game{
            public int id;
            public int powerFewestCubes;
        }   

        static void Main(string[] args){
            //Part 1
            int sumOfIds = 0, sumOfPowers = 0;
            List<string> listOfGames = File.ReadAllLines(Environment.CurrentDirectory + "\\input.txt").ToList();
            foreach(string game in listOfGames){
                Game? processedGame = ProcessGameString(game);
                if(processedGame.HasValue){
                    sumOfIds += processedGame.Value.id;
                    //part2
                    sumOfPowers += processedGame.Value.powerFewestCubes;
                }
            }

            Console.WriteLine("Total sum of IDs = " + sumOfIds);
            //Part 2
            Console.WriteLine("Total sum of powers = " + sumOfPowers);
        }

        static Game? ProcessGameString(string gameInput){            
            //Extract only game results
            Match match = Regex.Match(gameInput, "(?<=Game.*:\\s+)(.*)");
            List<string> cubePulls = match.Value.Split(";").ToList();

            //part2 - determine fewest cube count
            int fewBlue = -1, fewRed = -1, fewGreen = -1;
            
            //Go through and determine if each game is possible            
            foreach(string roll in cubePulls){      
                int blueCount = 0, redCount = 0, greenCount = 0;          

                Match blueMatch = Regex.Match(roll, "(\\d+)(?= blue)");
                Match redMatch = Regex.Match(roll, "(\\d+)(?= red)");
                Match greenMatch = Regex.Match(roll, "(\\d+)(?= green)");

                blueCount = blueMatch.Success ? Int32.Parse(blueMatch.Value) : 0;
                redCount = redMatch.Success ? Int32.Parse(redMatch.Value) : 0;
                greenCount = greenMatch.Success ? Int32.Parse(greenMatch.Value) : 0;
                
                if(blueCount > TOT_B_CUBES || greenCount > TOT_G_CUBES || redCount > TOT_R_CUBES)
                    return null;

                //part2
                if((fewBlue == -1 || blueCount < fewBlue) && blueCount > 0) fewBlue = blueCount;
                if((fewRed == -1 || redCount < fewRed) && redCount > 0) fewRed = redCount;
                if((fewGreen == -1 || greenCount < fewGreen) && greenCount > 0) fewGreen = greenCount;

            }

            Game newGame = new Game();
            newGame.id = Int32.Parse(gameInput.SkipWhile(c => !char.IsDigit(c)).TakeWhile(char.IsDigit).ToArray());
            newGame.powerFewestCubes = fewBlue * fewRed * fewGreen;

            return newGame;
        }
    }
}
