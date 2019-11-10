using System;
using System.Collections.Generic;

namespace FF.Task8
{
    public class RPN
    {
        public static int Evaluate(string expression)
        {
            var operators = "+-*/";
            var stack = new Stack<string>();
            var tokens = expression.Split(" ");
            foreach(var t in tokens){
                if(!operators.Contains(t)){
                    stack.Push(t);
                }else{
                    int.TryParse(stack.Pop(),out var a);
                    int.TryParse(stack.Pop(),out var b);
                    var index = operators.IndexOf(t, StringComparison.Ordinal);
                    switch (index)
                    {
                        case 0:
                            stack.Push((a + b).ToString());
                            break;
                        case 1:
                            stack.Push((b - a).ToString());
                            break;
                        case 2:
                            stack.Push((a * b).ToString());
                            break;
                        case 3:
                            stack.Push((b / a).ToString());
                            break;
                    }
                }
            }
 
            int.TryParse(stack.Pop(),out var returnValue);
 
            return returnValue;
        }
    }
}