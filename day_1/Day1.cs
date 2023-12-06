/*
    Advent of Code 2023 - Day 1
    Matthew Wrobel
    2023-12-03
*/

namespace AdventOfCode.Day1
{
    class Day1{
        static void Main(string[] args){
            
            //Part 1
            //Read in the file
            List<string> listOfInputs = File.ReadAllLines(Environment.CurrentDirectory + "\\input.txt").ToList();

            int inputSum = 0;
            foreach (string input in listOfInputs){
                //Get list of gitis and add up the first/last combined
                var digits = input.Where(chr => Char.IsDigit(chr)).ToArray(); 
                inputSum += Int32.Parse(digits[0].ToString() + digits[digits.Length - 1].ToString());
            }
            Console.WriteLine("Part 1 Sum = " + inputSum);

            //Part 2
            string digit1, digit2;
            int newInputSum = 0;
            foreach (string input in listOfInputs){
                digit1 = DetermineDigit(input);
                digit2 = DetermineDigit(new string(input.Reverse().ToArray()), true);
                newInputSum += Int32.Parse(digit1 + digit2);
            }
            Console.WriteLine("Part 2 Sum = " + newInputSum);
            
        }

        static string DetermineDigit(string input, bool reverse = false){
            var numbers = new Dictionary<string, int>(){
                {"one", 1}, {"two", 2}, {"three", 3},
                {"four", 4}, {"five", 5}, {"six", 6},
                {"seven", 7}, {"eight", 8}, {"nine", 9},
                {"zero", 0}
            };

            if(reverse){    //if reverse, flip the strings so we can work backwards
                numbers = numbers.ToDictionary(x => new string(x.Key.Reverse().ToArray()), x => x.Value);
            }
            
            int index = 0, digit = 0;
            foreach(var character in input){
                //Get input as span
                var subInput = input.AsSpan(index++);
                foreach(var n in numbers){
                    //Look for key in span
                    if(subInput.StartsWith(n.Key)){
                        digit = n.Value;
                        return digit.ToString();
                    }
                }
                if((int)character >= 48 && (int)character <= 57){
                    digit = ((int)character) - 48;
                    break;
                }
            }
            return digit.ToString();
        }
    }
}