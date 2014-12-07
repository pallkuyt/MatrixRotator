using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixRotator.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;

    namespace MatrixRotator.Web.Models
    {
        public struct Complexity
        {
            private readonly int _operations;
            private readonly TimeSpan _elapsed;

            public Complexity(int operations, TimeSpan elapsed)
            {
                _operations = operations;
                _elapsed = elapsed;
            }

            public int Operations
            {
                get { return _operations; }
            }

            public TimeSpan Elapsed
            {
                get { return _elapsed; }
            }
        }

        public class Matrix<T> : IEquatable<Matrix<T>>, ICloneable
        {
            private readonly T[,] _matrix;

            public Matrix(T[,] matrix)
            {
                if (matrix == null)
                {
                    throw new ArgumentNullException("matrix");
                }

                if (matrix.GetLength(0) != matrix.GetLength(1))
                {
                    throw new ArgumentException("Matrix should have the same number of rows and columns", "matrix");
                }

                _matrix = matrix;
            }

            public int Size
            {
                get { return _matrix.GetLength(0); }
            }

            public T this[int i, int j]
            {
                get
                {
                    try
                    {
                        return _matrix[i, j];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        throw new IndexOutOfRangeException("Invalid index", e);
                    }
                }
            }

            public Complexity Rotate()
            {
                var watch = Stopwatch.StartNew();

                int operations = 0;
                int halfSize = (int)Math.Ceiling(Size * 0.5);

                for (int i = 0; i < halfSize; i++)
                {
                    for (int j = i; j < Size - 1 - i; j++)
                    {
                        T temp = _matrix[i, j];
                        _matrix[i, j] = _matrix[j, Size - 1 - i];
                        _matrix[j, Size - 1 - i] = _matrix[Size - 1 - i, Size - 1 - j];
                        _matrix[Size - 1 - i, Size - 1 - j] = _matrix[Size - 1 - j, i];
                        _matrix[Size - 1 - j, i] = temp;
                        operations++;
                    }
                }
                watch.Stop();

                return new Complexity(operations, watch.Elapsed);
            }

            public Matrix<T> Clone()
            {
                return new Matrix<T>(_matrix.Clone() as T[,]);
            }

            #region System.Object members

            public override string ToString()
            {
                var builder = new StringBuilder();

                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        builder.Append(string.Format(CultureInfo.CurrentCulture, "{0} ", _matrix[i, j]));
                    }

                    builder.Append(Environment.NewLine);
                }

                return builder.ToString();
            }

            public override bool Equals(object obj)
            {
                var other = obj as Matrix<T>;
                return Equals(other);
            }

            public override int GetHashCode()
            {
                return _matrix.GetHashCode();
            }

            #endregion

            #region IEquatable<Matrix<T>> members
            public bool Equals(Matrix<T> other)
            {
                if (other == null)
                {
                    return false;
                }

                if (other.Size != Size)
                {
                    return false;
                }

                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (!EqualityComparer<T>.Default.Equals(_matrix[i, j], other[i, j]))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            #endregion

            #region ICloneable members

            object ICloneable.Clone()
            {
                return Clone();
            }

            #endregion
        }
    }
}