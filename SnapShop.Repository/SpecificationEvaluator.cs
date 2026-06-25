using Microsoft.EntityFrameworkCore;
using SnapShop.Core.Models;
using SnapShop.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseModel
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecifications<T> specs)
        {
            var query = inputQuery;
            if (specs.Criteria is not null) { 
                query= query.Where(specs.Criteria);
            }

            if (specs.SortAscending is not null) { 
            
                query = query.OrderBy(specs.SortAscending);
            }
            if (specs.SortDescending is not null)
            {

                query = query.OrderByDescending(specs.SortDescending);
            }

            query = specs.Includes.Aggregate(query, (currentQuery, includeExp) 
                => currentQuery.Include(includeExp));
            return query;
        }
    }
}
