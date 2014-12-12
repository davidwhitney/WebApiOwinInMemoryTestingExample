using Api.ApplicationServices;
using Moq;
using NUnit.Framework;

namespace Api.Test.Acceptance.ValueApi
{
    [TestFixture]
    public class WhenCallingValueApi : InMemoryTest
    {
        [Test]
        public void GetValues_WithKnownValue_ReturnsCorrectBody()
        {
            MockOut<IGetValues>().Setup(x => x.GetAValue(It.IsAny<int>())).Returns(5);

            var response = HttpClient.GetAsync("/values/5").Result;
            var body = response.Content.ReadAsStringAsync().Result;

            Assert.That(body, Is.StringContaining("5"));
        }

        [Test]
        public void GetValues_WithUnknownValue_Throws()
        {
            var response = HttpClient.GetAsync("/values/totally-not-a-number").Result;
            var body = response.Content.ReadAsStringAsync().Result;

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(body, Is.StringContaining("The request is invalid."));
        }
    }
}
