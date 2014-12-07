using System;
using MatrixRotator.Web.Models;
using MatrixRotator.Web.Models.MatrixRotator.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixRotator.Web.Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Matrix_WithNullMatrixCtorParameter_ThrowsArgumentNullException()
        {
            // arrange
            int[,] array = null;

            // act
            var matrix = new Matrix<int>(array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Matrix_NonSquareMatrixCtorParameter_ThrowsArgumentException()
        {
            // arrange
            int[,] array = { { 1, 2 }, { 3, 4 }, { 5, 6 } };

            // act
            var matrix = new Matrix<int>(array);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Matrix_IfIndexOutORange_ThrowsIndexOutOfRangeException()
        {
            // arrange
            int[,] array = { { 1, 2 }, { 3, 4 } };

            // act
            var element = new Matrix<int>(array)[2, 1];
        }

        [TestMethod]
        public void Matrix_SizeReturnsArraySize_Success()
        {
            // arrange
            int[,] array = { { 1, 2 }, { 3, 4 } };

            // act
            var actual = new Matrix<int>(array).Size;
            var expected = 2;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Matrix_IndexerReturnsElement_Success()
        {
            // arrange
            int[,] array = { { 1, 2 }, { 3, 4 } };

            // act
            var actual = new Matrix<int>(array)[1, 1];
            var expected = 4;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Matrix_Equals_Success()
        {
            // arrange
            int[,] array = { { 1, 2 }, { 3, 4 } };
            var matrix1 = new Matrix<int>(array);
            var matrix2 = new Matrix<int>(array);

            // act
            var equals = matrix1.Equals(matrix2);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_Equals_Fail()
        {
            // arrange
            int[,] array1 = { { 1, 2 }, { 3, 4 } };
            int[,] array2 = { { 1, 1 }, { 3, 4 } };
            var matrix1 = new Matrix<int>(array1);
            var matrix2 = new Matrix<int>(array2);

            // act
            var equals = matrix1.Equals(matrix2);

            // assert
            Assert.IsFalse(equals);
        }

        [TestMethod]
        public void Matrix_CloneReturnsTheSameMatrixCopy_Fail()
        {
            // arrange
            int[,] array = { { 1, 2 }, { 3, 4 } };
            var matrix1 = new Matrix<int>(array);

            // act
            var matrix2 = matrix1.Clone();
            var equals = matrix1.Equals(matrix2);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_RotateMatrix1x1_Success()
        {
            // arrange
            int[,] array =
            {
                {1}
            };
            int[,] arrayTr =
            {
                {1}
            };
            var matrix = new Matrix<int>(array);
            var matrixTr = new Matrix<int>(arrayTr);

            // act
            matrix.Rotate();
            var equals = matrix.Equals(matrixTr);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_RotateMatrix2x2_Success()
        {
            // arrange
            int[,] array =
            {
                {1, 2},
                {4, 5}
            };
            int[,] arrayTr =
            {
                {2, 5},
                {1, 4}
            };
            var matrix = new Matrix<int>(array);
            var matrixTr = new Matrix<int>(arrayTr);

            // act
            matrix.Rotate();
            var equals = matrix.Equals(matrixTr);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_RotateMatrix3x3_Success()
        {
            // arrange
            int[,] array =
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            int[,] arrayTr =
            {
                {3, 6, 9},
                {2, 5, 8},
                {1, 4, 7}
            };
            var matrix = new Matrix<int>(array);
            var matrixTr = new Matrix<int>(arrayTr);

            // act
            matrix.Rotate();
            var equals = matrix.Equals(matrixTr);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_RotateMatrix4x4_Success()
        {
            // arrange
            int[,] array =
            {
                {1, 2, 3, 100},
                {4, 3, 6, 19},
                {7, 8, 9, 1917},
                {4, 88, 9, 1917}
            };
            int[,] arrayTr =
            {
                {100, 19, 1917, 1917},
                {3, 6, 9, 9},
                {2, 3, 8, 88},
                {1, 4, 7, 4}
            };
            var matrix = new Matrix<int>(array);
            var matrixTr = new Matrix<int>(arrayTr);

            // act
            matrix.Rotate();
            var equals = matrix.Equals(matrixTr);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_RotateMatrix5x5_Success()
        {
            // arrange
            int[,] array =
            {
                {1, 2, 3, 4, 8}, 
                {5, 6, 7, 8, 76}, 
                {9, 10, 99, 11, 12},
                {13, 14, 15, 16, 3},
                {13, 4, 55, 16, 93}
            };
            int[,] arrayTr =
            {
                {8, 76, 12, 3, 93},
                {4, 8, 11, 16, 16},
                {3, 7, 99, 15, 55},
                {2, 6, 10, 14, 4},
                {1, 5, 9, 13, 13}
            };
            var matrix = new Matrix<int>(array);
            var matrixTr = new Matrix<int>(arrayTr);

            // act
            matrix.Rotate();
            var equals = matrix.Equals(matrixTr);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_DoubleRotate_Success()
        {
            // arrange
            int[,] array =
            {
                {1, 2, 3, 100},
                {4, 3, 6, 19},
                {7, 8, 9, 1917},
                {4, 88, 9, 1917}
            };
            int[,] arrayTr =
            {
                {1917, 9, 88, 4},
                {1917, 9, 8, 7},
                {19, 6, 3, 4},
                {100, 3, 2, 1}
            };
            var matrix = new Matrix<int>(array);
            var matrixTr = new Matrix<int>(arrayTr);

            // act
            matrix.Rotate();
            matrix.Rotate();
            var equals = matrix.Equals(matrixTr);

            // assert
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Matrix_FourRotateGivesTheSameMatrix_Success()
        {
            // arrange
            int[,] array =
            {
                {1, 2, 3, 100},
                {4, 3, 6, 19},
                {7, 8, 9, 1917},
                {4, 88, 9, 1917}
            };
            int[,] arrayTr =
            {
                {1, 2, 3, 100},
                {4, 3, 6, 19},
                {7, 8, 9, 1917},
                {4, 88, 9, 1917}
            };
            var matrix = new Matrix<int>(array);
            var matrixTr = new Matrix<int>(arrayTr);

            // act
            matrix.Rotate();
            matrix.Rotate();
            matrix.Rotate();
            matrix.Rotate();
            var equals = matrix.Equals(matrixTr);

            // assert
            Assert.IsTrue(equals);
        }
    }
}
