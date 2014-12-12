using System.Web.Http;
using Api.ApplicationServices;

namespace Api.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IGetValues _dependency;

        public ValuesController(IGetValues dependency)
        {
            _dependency = dependency;
        }

        public Values Get(int id)
        {
            var value = _dependency.GetAValue(id);
            return new Values { "value" + value };
        }

    }
}