using System.Web.Http;

namespace Api.Controllers
{
    public class ValuesController : ApiController
    {
        public string Get(int id)
        {
            return "value" + id;
        }
    }
}