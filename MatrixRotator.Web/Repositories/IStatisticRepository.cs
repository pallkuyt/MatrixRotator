using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixRotator.Web.Models;

namespace MatrixRotator.Web.Repositories
{
    public interface IStatisticRepository
    {
        IEnumerable<Statistic> GetAll();

        void Add(Statistic item);
    }
}
