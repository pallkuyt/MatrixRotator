using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixRotator.Web.Models
{
    public class Statistic
    {
        public virtual int Id { get; set; }
        public virtual int MatrixSize { get; set; }
        public virtual double ElapsedMiliseconds { get; set; }
        public virtual int Operations { get; set; }
        public virtual DateTime RotationDate { get; set; }
    }
}