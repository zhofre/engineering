using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    /// <summary>
    ///     Expressions
    /// </summary>
    public abstract class Expression<T> : IExpressible, IClassifiable<T>, IEquatable<Expression<T>>
        where T : IExpressible
    {
        internal abstract bool RequiresBrackets { get; }
        public abstract bool CanScale { get; }
        public abstract string Representation { get; }
        public abstract Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            where TOther : IExpressible;
        public abstract Expression<T> Transform(Func<Expression<T>, Expression<T>> f);

        public static implicit operator Expression<T>(T other)
        {
            var derOther = other as IDerived<T>;
            return derOther == null
                ? new ConstantExpression<T>(other)
                : derOther.Expression;
        }

        public static Expression<T> operator *(Prefix prefix, Expression<T> expression)
        {
            return new PrefixExpression<T>(prefix, expression);
        }

        internal string BracketedRepresentation
            => "(" + Representation + ")";

        internal string AutoBracketedRepresentation
            => RequiresBrackets ? BracketedRepresentation : Representation;

        double IClassifiable<T>.GetExponent() => GetExponentImpl();
        protected abstract double GetExponentImpl();
        double IClassifiable<T>.GetScale() => GetScaleImpl();
        protected abstract double GetScaleImpl();

        IEnumerable<Expression<T>> IClassifiable<T>.GetNumerator() => GetNumeratorImpl();
        protected abstract IEnumerable<Expression<T>> GetNumeratorImpl();

        IEnumerable<Expression<T>> IClassifiable<T>.GetDenominator() => GetDenominatorImpl();
        protected abstract IEnumerable<Expression<T>> GetDenominatorImpl();

        public abstract bool Equals(Expression<T> other);

        public sealed override bool Equals(object other)
            => Equals(other as Expression<T>);

        public sealed override int GetHashCode() => GetHashCodeImpl();
        protected abstract int GetHashCodeImpl();

        public bool Equals(IExpressible other)
            => Equals(other as Expression<T>);

    }
}