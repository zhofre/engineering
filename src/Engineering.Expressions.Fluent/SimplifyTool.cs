using System;

namespace Engineering.Expressions.Fluent
{
    internal static class SimplifyTool
    {
        public static Expression<T> Normal<T>(Expression<T> expression)
            where T : IExpressible
        {
            // drill down into the expression and simplify where possible
            var classifiedExpression = expression as IClassifiable<T>;
            


            
            throw new NotImplementedException();
        }
    }
}