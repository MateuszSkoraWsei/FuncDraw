using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDraw
{
    public class Tokenizer
    {
       
        
        /// <summary>
        /// Tokenizes the expression into a list of strings
        /// </summary>
        /// <returns>return list of tokens</returns>
        /// <exception cref="ArgumentException">if catches wrong character , difrent from (XYxy+-*/)</exception>
        public static List<string> Tokenize(string expression)
        {
            
            List<string> tokens = new List<string>();
            StringBuilder currentToken = new StringBuilder();
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }
                if (c == '(' )
                {
                    do
                    {

                        c = expression[i];
                        if (i >= expression.Length)
                        {
                            throw new ArgumentException("Unmatched parentheses in expression.");
                        }
                        if (c == '(')
                        {
                            currentToken.Append(c);
                        }
                        else if (c == ')')
                        {
                            currentToken.Append(c);

                        }
                        else currentToken.Append(c);
                            i++;
                    } while (c != ')');


                }
                else
                {
                    if (char.IsDigit(c) || "xyXY".Contains(c))
                    {
                        currentToken.Append(c);
                    }
                    else
                    {
                        if (currentToken.Length > 0)
                        {
                            tokens.Add(currentToken.ToString());
                            currentToken.Clear();
                        }
                        if ("+-*/^".Contains(c))
                        {
                            tokens.Add(c.ToString());
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid character '{c}' in expression.");
                        }
                    }
                }
                
            }
            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken.ToString());
            }
            return tokens;
        }
    }
}
