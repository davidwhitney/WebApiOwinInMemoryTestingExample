using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Api.Test.Acceptance.ValueApi
{
    [TestFixture]
    public class WhenCallingValueApi : InMemoryTest
    {
        [Test]
        public void GetValues_WithKnownValue_ReturnsCorrectBody()
        {
            var response = HttpClient.GetAsync("/values/5").Result;
            var body = response.Content.ReadAsStringAsync().Result;

            Assert.That(body, Is.StringContaining("5"));
        }
    }
}
