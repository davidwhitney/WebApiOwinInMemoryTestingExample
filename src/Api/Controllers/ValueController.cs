using System.Collections.Generic;
using System.Web.Http;

namespace Api.Controllers
{
    public class ValuesController : ApiController
    {
        public Values Get(int id)
        {
            return new Values
            {
                "value" + 5
            };
        }

    }

    public class Values : List<string>
    {
    }
}