using Microsoft.AspNetCore.Mvc;
using SnapShop.Repository.Data;

namespace SnapShop.APIs.Controllers
{
    public class TestController : BaseController
    {
        public TestController(ShopContext context)
        {
            _Context = context;
        }

        public ShopContext _Context { get; }

        [HttpGet]
        public void ExceptionTest() {

            var dummy = _Context.Products.Find(100);
            var dum = dummy.ToString();

        }

    }
}
