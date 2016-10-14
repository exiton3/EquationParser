using EquationSimplification.Expressions;
using EquationSimplification.Lex;

namespace EquationSimplification.Parsing
{
    //Grammar for boolean expressions
    //E  -> ReT
    //Re -> or T Re | e
    //T  -> RtF
    //Rt -> and F Rt | e
    //F ->  (E) | var | const


    /*
        Expr    ← Sum
        Sum     ← Product ('+' / '-') Product        
        Product ← Factor (*) Factor
        Factor   ← const | var | ( Expr )'
       
    */
    public sealed class Parser
    {
        private readonly ITokenizer tokenizer;
        private Token currentToken;

        public Parser(ITokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        public IExpression Parse(string input)
        {
            tokenizer.Tokenize(input);
            return Expression();
        }

        IExpression Expression()
        {
            return RestExpr(Term());
        }

        private IExpression RestExpr(IExpression exp)
        {
            var token = GetNextToken();
            if (token == null)
            {
                return exp;
            }

            if (token.Name == TokenKind.Plus)
            {
                return RestExpr(new SumExpression(exp, Term()));
            }
            if (token.Name == TokenKind.Minus)
            {
                return RestExpr(new MinusExpression(exp, Term()));
            }

            tokenizer.PushBack(token);
            return exp;
        }

        IExpression Term()
        {
            return RestTerm(Factor());
        }

        private IExpression RestTerm(IExpression exp)
        {
            var token = GetNextToken();
            if (token == null)
            {
                return exp;
            }

            if (token.Name == TokenKind.Product)
            {
                return RestTerm(new ProductExpression(exp, Factor()));
            }

            tokenizer.PushBack(token);
            return exp;
        }

        IExpression Factor()
        {
            IExpression expression = null;
            GetNextToken();
            switch (currentToken.Name)
            {
                case TokenKind.Constant:
                    expression = new ConstantExpression(double.Parse(currentToken.Value));
                    break;
                case TokenKind.Variable:
                    expression = new VariableExpression(currentToken.Value);
                    break;

                case TokenKind.Power:
                    expression = new PowerExpression(currentToken.Value);
                    break;

                case TokenKind.LParen:
                    expression = Expression();
                    GetNextToken();
                    break;
            }
            return expression;
        }

        private Token GetNextToken()
        {
            return currentToken = tokenizer.GetNextToken();
        }
    }
}