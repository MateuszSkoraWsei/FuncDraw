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
        int GetPriority(string token)
        {
            if ("*/".Contains(token)) return 2;
            
            else if ("+-".Contains(token)) return 1;
            
                
           
             return 0;
            
        }
        public void FindEquasion(List<string> tokens)
        {
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
                        MaxPriority = 0;
                        Counter = 0;
                        break;
                    }
                    Counter++;
                }
            }
            MessageBox.Show(tokens[0]);
        }
    }
}
