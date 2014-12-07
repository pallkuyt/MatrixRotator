using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MatrixRotator.Web.Helpers;
using MatrixRotator.Web.Models;
using MatrixRotator.Web.Repositories;
using NHibernate.Linq;

namespace MatrixRotator.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly MatrixUploader _uploader = new MatrixUploader();

        public HomeController()
            : this(new StatisticRepository())
        {
        }

        public HomeController(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public ActionResult Index()
        {
            return View(Matrix);
        }

        private IntegerMatrix Matrix
        {
            get { return Session["Matrix"] as IntegerMatrix; }
            set { Session["Matrix"] = value; }
        }

        public ActionResult Statistics()
        {
            var statistics = _statisticRepository.GetAll().OrderByDescending(s => s.RotationDate);
            return View(statistics);
        }

        public ActionResult Rotate()
        {
            if (Matrix != null)
            {
                var complexity = Matrix.Rotate();

                var statItem = new Statistic()
                {
                    MatrixSize = Matrix.Size,
                    Operations = complexity.Operations,
                    ElapsedMiliseconds = complexity.Elapsed.TotalMilliseconds,
                    RotationDate = DateTime.Now
                };

                Task.Factory.StartNew(() => _statisticRepository.Add(statItem));
            }

            return View("Index", Matrix);
        }

        [HttpPost]
        public ActionResult UploadMatrix(HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null && uploadFile.ContentLength > 0 && uploadFile.FileName.EndsWith(".csv"))
            {
                using (Stream inputStream = uploadFile.InputStream)
                {
                    Matrix = _uploader.UploadMatrix(inputStream);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                throw new Exception("Please provide csv file with square matrix");
            }
        }

        private class MatrixUploader
        {
            private IEnumerable<string> ReadLines(Func<Stream> streamProvider,
                                         Encoding encoding)
            {
                using (var stream = streamProvider())
                using (var reader = new StreamReader(stream, encoding))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }

            public IntegerMatrix UploadMatrix(Stream stream)
            {
                var lines = ReadLines(() => stream, Encoding.UTF8)
                    .Select(l => l.Split(','))
                    .ToArray();

                var matrixSize = lines.Length;
                var matrix = new int[matrixSize, matrixSize];

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines.Length != lines[i].Length)
                    {
                        throw new Exception(string.Format("File doesn't provide matrix of square structure: line {0}", i + 1));
                    }

                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        int element;
                        if (int.TryParse(lines[i][j], out element))
                        {
                            matrix[i, j] = element;
                        }
                        else
                        {
                            throw new Exception(string.Format("Invalid element in position {0};{1}", i + 1, j + 1));
                        }
                    }
                }

                return new IntegerMatrix(matrix);
            }
        }
    }
}