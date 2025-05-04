using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDraw
{
    internal class CalculatePosition
    {
        
        public int MaxPriority = 0;
        public int actualPriority = 0;
        public List<int> priorityTable = new List<int>(); // Fixed the error by initializing the list properly.  
        public int result = 0;
        private double x;
        private double y;
        private List<string> tokens;
        private string output;
        int GetPriority(string token)
        {
            if ("*/".Contains(token)) return 2;
            
            else if ("+-".Contains(token)) return 1;
            
                
           
             return 0;
            
        }
        public CalculatePosition(double X = 0 , double Y = 0 , List<string> Tokens = null , string Output = "y")
        {
            x = X;
            y = Y;
            tokens = Tokens ?? new List<string>();
            output = Output;
        }
        public string FindEquasion()
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Contains("x") || tokens[i].Contains("X"))
                {
                    if(output == "x" || output == "X")
                    {
                       
                        throw new ArgumentException("Invalid character 'x' in expression.");

                    }
                    else
                    {
                        if (tokens[i].Length == 1 )
                        {
                            tokens[i] = x.ToString();
                        }
                        else
                        {
                            int index = tokens[i].IndexOf("x");
                            int multiplaier = int.Parse(tokens[i].Substring(0, index));
                            double wynik = multiplaier * x;
                            tokens[i] = wynik.ToString();
                        }
                            
                    }
                        

                }
                else if(tokens[i].Contains("y") || tokens[i].Contains("Y"))
                {
                    if (output == "y" || output == "Y")
                    {
                        throw new ArgumentException("Invalid character 'y' in expression.");
                    }
                    else
                    {
                        if (tokens[i].Length == 1)
                        {
                            tokens[i] = y.ToString();
                        }
                        else
                        {
                            // Find the index of 'y' in the token
                            int index = tokens[i].IndexOf("y");
                            int multiplaier = int.Parse(tokens[i].Substring(0, index));
                            double wynik = multiplaier * y;
                            tokens[i] = wynik.ToString();
                        }
                    }

                }
            }
            while (tokens.Count > 1)
            {
                 int Counter = 0;
                foreach (var token in tokens)
                {
                    actualPriority = GetPriority(token);
                    priorityTable.Add(actualPriority);
                    if (actualPriority > MaxPriority)
                    {
                        MaxPriority = actualPriority;
                    }
                }
                if (tokens[0] == "-")
                {
                    tokens.RemoveAt(0);
                    priorityTable.RemoveAt(0);
                    MaxPriority = 0;
                    Counter = 0;
                    tokens[0] = "-" + tokens[0];
                    continue;
                }
                foreach (var priorityValue in priorityTable)
                {
                    
                    if (priorityValue == MaxPriority && priorityTable.Count >= 3 && tokens.Count >= 3 )
                    {
                        // Perform calculations and update tokens list.  
                        
                        
                        
                            if (tokens[Counter] == "*")
                            {
                                result = Convert.ToInt32(tokens[Counter - 1]) * Convert.ToInt32(tokens[Counter + 1]);
                            }
                            else if (tokens[Counter] == "/")
                            {
                                result = Convert.ToInt32(tokens[Counter - 1]) / Convert.ToInt32(tokens[Counter + 1]);
                            }
                            else if (tokens[Counter] == "-")
                            {
                                result = Convert.ToInt32(tokens[Counter - 1]) - Convert.ToInt32(tokens[Counter + 1]);
                            }
                            else if (tokens[Counter] == "+")
                            {
                                result = Convert.ToInt32(tokens[Counter - 1]) + Convert.ToInt32(tokens[Counter + 1]);
                            }
                        
                        
                        
                        tokens.RemoveAt(Counter - 1);
                        tokens.RemoveAt(Counter - 1);
                        tokens.RemoveAt(Counter -1);
                        tokens.Insert(Counter - 1, result.ToString());
                        priorityTable.RemoveAt(Counter - 1);
                        priorityTable.RemoveAt(Counter - 1);
                        priorityTable.RemoveAt(Counter - 1);
                        priorityTable.Insert(Counter - 1, 0);
                        MaxPriority = 0;
                        Counter = 0;
                        break;
                    }
                    Counter++;
                }
            }
            if(output == "x" || output == "X")
            {
                return $"{tokens[0]},{y}";
            }
            else if (output == "y" || output == "Y")
            {
                return $"{y},{tokens[0]}";
            }
            else
            {
                throw new ArgumentException("Invalid output variable. Expected 'x' or 'y'.");
            }
            
        }
    }
}
