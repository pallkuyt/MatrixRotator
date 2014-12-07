using System;
using System.Collections.Generic;
using System.Linq;
using MatrixRotator.Web.Helpers;
using MatrixRotator.Web.Models;
using NHibernate.Linq;

namespace MatrixRotator.Web.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        public IEnumerable<Statistic> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<Statistic>().ToList();
            }
        }

        public void Add(Statistic item)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(item);
                    transaction.Commit();
                }
            }
        }
    }
}