using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace DemoProject.AppCode
{
    public class FilterExpressionMatcher<T>
    {
        const char CHR_NOT = '!';
        const char CHR_LPAREN = '(';
        const char CHR_RPAREN = ')';
        const char CHR_AND = '&';
        const char CHR_OR = '|';
        const char CHR_STAR = '*';

        private ParameterExpression ExpressionParam;
        public string TheString { get; set; } = default!;

        private int _ix = 0;
        public bool HasMore = true;
        public int StartAt {
            get => _ix;
            set {
                _ix = value;
                if(_ix >= TheString.Length) {
                    HasMore = false;
                }
            }
        }

        public FilterExpressionMatcher(string theString)
        {
            TheString = theString;
            ExpressionParam = Expression.Parameter(typeof(T), "p");
        }

        #region [ Tools ]

        public void SkipWs(){
            while(HasMore && char.IsWhiteSpace(TheString[StartAt])){
                StartAt++;
            }
        }

        public bool canMatch(char c){
            if(HasMore && TheString[StartAt] == c){
                StartAt++;
                return true;
            }
            
            return false;
        }
        
        public void match(char c){
            if(canMatch(c)) return;

            throw new Exception($@"'{c}' expected at index {StartAt}.");
        }

        public bool canMatch(string str){
            if(!HasMore) return false;

            if(HasMore && TheString.Substring(StartAt, str.Length) == str){
                StartAt += str.Length;
                return true;
            }

            return false;
        }
        
        public void mustMatch(string str){
            if(canMatch(str)) return;

            throw new Exception($@"'{str}' expected at index {StartAt}.");
        }

        #endregion [ Tools ]

        private Regex rxIdName = new Regex($@"[_a-zA-Z][_a-zA-Z0-9]*", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Multiline);
        public string match_ID_NAME(){
            //ID_NAME = [_a-zA-Z][_a-zA-Z0-9]*
            var m = rxIdName.Match(TheString, StartAt);
            if( !m.Success) throw new Exception($@"ID_NAME expected at index {StartAt}");

            StartAt += m.Length;

            return m.Value;
        }

        private Regex rxNumericLiteral = new Regex($@"(0|[1-9][0-9]*)(\.[0-9]+)?|\.[0-9]+", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Multiline);
        public double match_NUMERIC_LITERAL(){
            //LITERAL_NUMBER = (0|[1-9][0-9]*)(\.[0-9]+)?|\.[0-9]+;
            var m = rxNumericLiteral.Match(TheString, StartAt);
            StartAt += m.Length;
            
            return double.Parse(m.Value);
        }

        private Regex rxStringLiteral = new Regex($@"([^""\\]|\\.)*", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Multiline);
        private Regex rxEscapedChar = new Regex($@"\\(.)", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Multiline);
        public string match_STRING_LITERAL(){
            //quotes were read outside !!!
            //LITERAL_STRING = ([^"\\]|\\.)*;
            var m = rxStringLiteral.Match(TheString, StartAt);
            StartAt += m.Length;
            return rxEscapedChar.Replace(m.Value, "$1");
        }

        public Expression match_VALUE(){
            // VALUE = "LITERAL_STRING" | LITERAL_NUMBER | ID_NAME;
            if(canMatch('"')){
                var Val = Expression.Constant(match_STRING_LITERAL());
                match('"');

                return Val;
            }
            else if(HasMore && char.IsDigit(TheString[StartAt])){
                return Expression.Constant(match_NUMERIC_LITERAL());
            }
            else {
                return Expression.PropertyOrField(ExpressionParam, match_ID_NAME());
            }
        }

        private Regex rxOp = new Regex($@"<=?|==|!=|\*=\*?|=\*|>=?|(not\s+)?in", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Multiline);
        private Regex rxWs = new Regex($@"\s+", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Multiline);
        public string match_OPERATOR(){
            //OPERATOR = <=?|==|!=|\*=\*?|=\*|>=?|(not\s+)?in;
            var m = rxOp.Match(TheString, StartAt);

            if(!m.Success) throw new Exception($@"OPERATOR expected at index {StartAt}");

            StartAt += m.Length;
            return m.Value;
        }

        public List<Expression> match_VALUE_LIST(){
            //VALUE_LIST = VALUE + (WS + COMMA + WS + VALUE)*;
            List<Expression> values = new List<Expression>();

            SkipWs();
            values.Add(match_VALUE());
            SkipWs();

            while(canMatch(',')){
                SkipWs();
                values.Add(match_VALUE());
                SkipWs();
            }


            return values;
        }

        public Expression matchTEST(){
            //TEST = VALUE + WS + OPERATOR + WS + (VALUE | PAREN_OPEN + WS + VALUE_LIST + WS + PAREN_CLOSE)

            SkipWs();

            var LVal = match_VALUE();
            SkipWs();
            string op = match_OPERATOR();
            SkipWs();

            if(canMatch(CHR_LPAREN)){
                if (op != "in" && op != "not in") throw new Exception($@"Expected VALUE, not VALUE_LIST at index {StartAt}.");
                SkipWs();
                var valueList = Expression.NewArrayInit(LVal.Type, match_VALUE_LIST());
                SkipWs();
                match(CHR_RPAREN);

                var e = Expression.Call(valueList, nameof(Enumerable.Contains), null, LVal);

                return op == "not in" ? Expression.Not(e) : e;
            }
            else {
                if (op == "in" || op == "not in") throw new Exception($@"Expected VALUE_LIST, not VALUE at index {StartAt}.");
                
                SkipWs();
                var RVal = match_VALUE();
                if(RVal.Type != LVal.Type) {
                    RVal = Expression.Convert(RVal, LVal.Type);
                }
                SkipWs();
                
                switch (op)
                {
                    case "<":
                        return Expression.LessThan(LVal, RVal);
                    case "<=":
                        return Expression.LessThanOrEqual(LVal, RVal);
                    case "==":
                        return Expression.Equal(LVal, RVal);
                    case "!=":
                        return Expression.NotEqual(LVal, RVal);
                    case ">=":
                        return Expression.GreaterThan(LVal, RVal);
                    case ">":
                        return Expression.GreaterThanOrEqual(LVal, RVal);
                    case "=*":
                        return Expression.Call(LVal, nameof(string.StartsWith), null, RVal);
                    case "*=*":
                        return Expression.Call(LVal, nameof(string.Contains), null, RVal);
                    case "*=":
                        return Expression.Call(LVal, nameof(string.EndsWith), null, RVal);
                    default:
                        throw new Exception($@"Unknown operator '{op}'.");
                }
            }
        }

        public Expression matchAtom(){
            //atom = ( ("!" + WS)? + PAREN_OPEN + expression + PAREN_CLOSE ) | TEST;
            SkipWs();
            if(canMatch(CHR_NOT)){
                SkipWs();
                match(CHR_LPAREN);
                SkipWs();

                var e = matchExpression();
                
                SkipWs();
                match(CHR_RPAREN);

                return Expression.Not(e);
            }
            else if(canMatch(CHR_LPAREN)) {
                SkipWs();

                var e = matchExpression();
                
                SkipWs();
                match(CHR_RPAREN);

                return e;
            }
            else {
                return matchTEST();
            }
        }

        public Expression matchAndList(){
            //andList = fullAtom + (WS + "&&" + WS + fullAtom)*;
            var e = matchAtom();

            SkipWs();
            while(canMatch(CHR_AND)){
                match(CHR_AND);//the second & sign in && 
                SkipWs();
                
                e = Expression.AndAlso(e, matchAtom());
            }

            return e.CanReduce ? e.Reduce() : e;
        }

        public Expression matchOrList(){
            //orList = andList + (WS + "||" + WS + andList)*;
            var e = matchAndList();

            SkipWs();
            while(canMatch(CHR_OR)){
                match(CHR_OR); //the second | sign in ||
                SkipWs();

                e = Expression.OrElse(e, matchAndList());
            }

            return e.CanReduce ? e.Reduce() : e;
        }

        public Expression matchExpression(){
            //expression = WS + orList + WS; //RECURSIVE!
            return matchOrList();
        }

        public Expression<Func<T, bool>> GetPredicateExpression()
        {
            SkipWs();
            var e = matchExpression();
            SkipWs();
            if(StartAt < TheString.Length) throw new Exception($@"Invalid character at index {StartAt}");

            return Expression.Lambda<Func<T, bool>>(e, ExpressionParam);
        }
    }
}