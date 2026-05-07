using SnapShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Specifications
{
    internal class BaseSpecifications<T> : ISpecifications<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecifications()
        {

        }
        public BaseSpecifications(Expression<Func<T, bool>> criateriaExpression )
        {
            Criteria = criateriaExpression;
        }
    }
}
