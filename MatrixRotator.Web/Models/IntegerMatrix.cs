using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MatrixRotator.Web.Models.MatrixRotator.Web.Models;

namespace MatrixRotator.Web.Models
{
    public class IntegerMatrix : Matrix<int>
    {
        public IntegerMatrix(int[,] matrix)
            : base(matrix)
        {
        }
    }
}