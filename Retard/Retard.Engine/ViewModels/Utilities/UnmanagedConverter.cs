using System;
using System.Linq.Expressions;

namespace Retard.Engine.ViewModels.Utilities
{
    /// <summary>
    /// Convertisseur générique
    /// </summary>
    /// <typeparam name="TDest">Le type de départ</typeparam>
    /// <typeparam name="TDest">Le type de destination</typeparam>
    public readonly struct UnmanagedConverter<TFrom, TDest> where TFrom : struct, IConvertible where TDest : struct, IConvertible
    {
        public static readonly Func<TFrom, TDest> Convert = UnmanagedConverter<TFrom, TDest>.GenerateConverter();

        /// <summary>
        /// Convertit la valeur de <typeparamref name="TFrom"/> vers <typeparamref name="TDest"/>
        /// </summary>
        /// <returns>La valeur convertie en <typeparamref name="TDest"/></returns>
        static Func<TFrom, TDest> GenerateConverter()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TFrom));
            Expression<Func<TFrom, TDest>> dynamicMethod =
                Expression.Lambda<Func<TFrom, TDest>>(Expression.Convert(parameter, typeof(TDest)), parameter);
            return dynamicMethod.Compile();
        }
    }
}
