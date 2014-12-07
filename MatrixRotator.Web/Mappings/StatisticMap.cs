using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using MatrixRotator.Web.Models;

namespace MatrixRotator.Web.Mappings
{
    public class StatisticMap : ClassMap<Statistic>
    {
        
        //Constructor
        public StatisticMap()
        {
            Id(x => x.Id).GeneratedBy.Identity().UnsavedValue(0);

            Map(x => x.MatrixSize);

            Map(x => x.Operations);

            Map(x => x.ElapsedMiliseconds);

            Map(x => x.RotationDate);

            Table("Statistic");
        }
    }
}