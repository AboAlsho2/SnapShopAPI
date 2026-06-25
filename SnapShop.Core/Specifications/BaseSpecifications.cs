using SnapShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = 
            new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> SortAscending { get ; set ; }
        public Expression<Func<T, object>> SortDescending { get ; set ; }

        public BaseSpecifications()
        {

        }
        public BaseSpecifications(Expression<Func<T, bool>> criateriaExpression )
        {
            Criteria = criateriaExpression;
        }

        public void OrderAscending(Expression<Func<T, object>> sortAsc)
        {
            SortAscending = sortAsc;
        }

        public void OrderDescending(Expression<Func<T, object>> sortDes)
        {
            SortDescending = sortDes;
        }

    }
}
