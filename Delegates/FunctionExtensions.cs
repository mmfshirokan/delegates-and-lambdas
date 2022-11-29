﻿using System;
using System.Collections.Generic;

namespace Delegates
{
    /// <summary>
    /// Demonstrates the delegate type and its capabilities.
    /// </summary>
    public static class FunctionExtensions
    {
        /// <summary>
        /// Generates a sequence of the elements of type T using the following recurrent formula:
        /// x_(n+1) = f(x_(n)), n = 1, 2, ...,  where x_1 - initial value.
        /// The count of requested elements is defined by the given number.
        /// For example, arithmetic <see>
        ///     <cref>https://www.wikiwand.com/en/Arithmetic_progression</cref>
        /// </see>
        /// or geometric <see>
        ///     <cref>https://www.wikiwand.com/en/Geometric_progression</cref>
        /// </see>
        /// progressions.
        /// </summary>
        /// <param name="first">First element of the sequence.</param>
        /// <param name="formula">Recurrent formula.</param>
        /// <param name="count">Required count of the elements.</param>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <returns>A sequence of the elements of type T.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throw when <paramref name="count"/> is less than or equal to 0.</exception>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="formula"/> is null.</exception>        
        public static IEnumerable<T> GenerateProgression<T>(T first, Func<T, T>? formula, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (formula is null)
            {
                throw new ArgumentNullException(nameof(formula));
            }

            return GeneratorCore();

            IEnumerable<T> GeneratorCore()
            {
                yield return first;
                T currentElement = first;

                for (int i = 1; i < count; i++)
                {
                    currentElement = formula(currentElement);
                    yield return currentElement;
                    
                }
            }
        }

        /// <summary>
        /// Generates a sequence of the elements of type T using the following recurrent formula:
        /// x_(n+1) = f(x_(n)), n = 1, 2, ...,  where x_1 - initial value.
        /// The count of requested elements is defined by the condition.
        /// For example, arithmetic <see>
        ///     <cref>https://www.wikiwand.com/en/Arithmetic_progression</cref>
        /// </see>
        /// or geometric <see>
        ///     <cref>https://www.wikiwand.com/en/Geometric_progression</cref>
        /// </see>
        /// progressions.
        /// </summary>
        /// <param name="first">First element of the sequence.</param>
        /// <param name="formula">Recurrent formula.</param>
        /// <param name="finished">Presents the condition under which the sequence generation process ends.</param>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <returns>A sequence of the elements of type T.</returns>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="formula"/> is null.</exception> 
        /// <exception cref="ArgumentNullException">Throw when <paramref name="finished"/> is null.</exception>        
        public static IEnumerable<T> GenerateProgression<T>(T first, Func<T, T>? formula, Predicate<T>? finished)
        {
            if (formula is null)
            {
                throw new ArgumentNullException(nameof(formula));
            }

            if (finished is null)
            {
                throw new ArgumentNullException(nameof(finished));
            }

            return GeneratorCore();

            IEnumerable<T> GeneratorCore()
            {
                T currentElement = first;

                while(!finished(currentElement))
                {
                    yield return currentElement;
                    currentElement = formula(currentElement);
                }
            }
        }

        /// <summary>
        /// Generates a `n`s element of the sequence using the following recurrent formula:
        /// x_(n+1) = f(x_(n)), n = 1, 1, ...,  where x_1 - initial value.
        /// For example, arithmetic <see>
        ///     <cref>https://www.wikiwand.com/en/Arithmetic_progression</cref>
        /// </see>
        /// or geometric <see>
        ///     <cref>https://www.wikiwand.com/en/Geometric_progression</cref>
        /// </see>
        /// progressions.
        /// </summary>
        /// <param name="first">First element of the sequence.</param>
        /// <param name="formula">Recurrent formula.</param>
        /// <param name="number">Number of the element of the sequence which is returned.</param>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <returns>An elements of the sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throw when <paramref name="number"/> is less than or equal to 0.</exception>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="formula"/> is null.</exception>        
        public static T GetElement<T>(T first, Func<T, T>? formula, int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if (formula is null)
            {
                throw new ArgumentNullException(nameof(formula));
            }

            T currentElement = first;

            for (int i = 1; i < number; i++)
            {
                currentElement = formula(currentElement);
            }

            return currentElement;
        }

        /// <summary>
        /// Calculates a value as a composition of sequentially executed binary operation
        /// on the elements of the sequence by the rule:
        /// value = operation(x_1, x_2), value = operation(value, x_3),  ... , value = operation(value, x_n)
        /// The elements of the sequence are generated by recurrent formula: 
        /// x_(n+1) = f(x_(n)), n = 1, 2, ...,  where x_1 - initial value.
        /// The count of requested elements for the calculation is defined by the given number.
        /// For example,
        /// x_1 + x_2 + x_3 + ... + x_n or x_1 * x_2 * x_3 * ... * x_n,
        /// where using  arithmetic or geometric progressions.
        /// </summary>
        /// <param name="first">First element of the sequence.</param>
        /// <param name="formula">Recurrent formula.</param>
        /// <param name="operation">Binary operation.</param>
        /// <param name="count">Count of the elements.</param>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <returns>A value as a composition of sequentially executed binary operation on the elements of the sequence.</returns>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="formula"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="operation"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw when <paramref name="count"/> is less than or equal to 0.</exception>
        public static T Calculate<T>(T first, Func<T, T>? formula, Func<T, T, T>? operation, int count)
        {
            if (formula is null)
            {
                throw new ArgumentNullException(nameof(formula));
            }

            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            T currentElement = formula(first);
            T result = operation(first, formula(first));

            for (int i = 2; i < count; i++)
            {
                currentElement = formula(currentElement);
                result = operation(result, currentElement);
            }

            return result;
        }

        /// <summary>
        /// Generates a sequence of the elements of type T using the following recurrent formula:
        /// x_(n+1) = f(x_(n-1), x_(n)), n = 1, 2, ..., where x_0 and x_1 - initial values.
        /// The count of requested elements is defined by the given number.
        /// </summary>
        /// <param name="first">First element in the sequence.</param>
        /// <param name="second">Second element in the sequence.</param>
        /// <param name="formula">Recurrent formula.</param>
        /// <param name="count">The number of requested elements.</param>
        /// <typeparam name="T">Type of sequence elements.</typeparam>
        /// <returns>A sequence of the elements of type T.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throw when <paramref name="count"/> is less than or equal to 0.</exception>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="formula"/> is null.</exception>
        public static IEnumerable<T> GenerateSequence<T>(T first, T second, Func<T, T, T>? formula, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (formula is null)
            {
                throw new ArgumentNullException(nameof(formula));
            }

            return GeneratorCore();

            IEnumerable<T> GeneratorCore()
            {
                yield return first;
                yield return second;

                T firstCurrentElement = first;
                T secondCurrentElement = second;

                for (int i = 2; i < count; i++)
                {
                    T temp = secondCurrentElement;
                    secondCurrentElement = formula(firstCurrentElement, secondCurrentElement);
                    firstCurrentElement = temp;
                    yield return secondCurrentElement;
                }
            }
        }

        /// <summary>
        /// Combines several predicates using logical AND operator.
        /// </summary>
        /// <param name="predicates">Array of the predicates.</param>
        /// <returns>
        /// A new predicate that combine the specified predicated using AND operator.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throw when predicates is null.</exception>
        /// <example>
        ///   var result = CombinePredicatesWithAnd(
        ///            x => !string.IsNullOrEmpty(x),
        ///            x => x.StartsWith("START"),
        ///            x => x.EndsWith("END"),
        ///            x => x.Contains("#"));
        ///   should return the predicate that identical to 
        ///   x=> (!string.IsNullOrEmpty(x)) && x.StartsWith("START") && x.EndsWith("END") && x.Contains("#").
        ///   The following example should create predicate that returns true if int value between -10 and 10:
        ///   var result = CombinePredicatesWithAnd(x => x > -10, x => x < 10);
        /// </example>
        public static Predicate<T> CombinePredicates<T>(params Predicate<T>[]? predicates)
        {
            if (predicates is null)
            {
                throw new ArgumentNullException(nameof(predicates));
            }

            return (T item) => 
            {
                foreach (var predicate in predicates)
                {
                    if (predicate is null)
                    {
                        continue;
                    }

                    if (!predicate(item))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
        
        /// <summary>
        /// Finds maximum from two elements according to comparer logic.
        /// If elements are equal returns <paramref name="lhs"/>.
        /// </summary>
        /// <param name="lhs">Left hand side operand.</param>
        /// <param name="rhs">Right hand side operand.</param>
        /// <param name="comparer">Comparer logic.</param>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <returns>Maximum from two elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null.</exception>
        public static T FindMax<T>(T lhs, T rhs, Comparison<T>? comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return comparer(lhs, rhs) >= 0 ? lhs : rhs;
        }
    }
}
