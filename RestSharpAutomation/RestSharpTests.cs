using NUnit.Framework;
using RestSharp;
using RestSharpAutomation.Utilities;
using System.Collections.Generic;
using System.Net;

namespace RestSharpAutomation
{
    [TestFixture]
    public class RestSharpTests : TestBase
    {
        private ApiRequest<List<TestResult>> _request;

        [SetUp]
        public void SetUp()
        {
            _request = new ApiRequest<List<TestResult>>(BaseUrl.AbsoluteUri);
            _request.SetEndpoint("/posts");
        }

        [Test, Order(0)]
        public void TestGet_ShouldReturnOk()
        {
            _request.SetMethod(Method.GET);
            _request.Execute();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(true, _request.IsSuccessful());
                Assert.AreNotEqual(0, _request.GetResponse().Count);
            });
        }

        [Test, Order(1)]
        public void TestPost_ShouldCreateNewResult()
        {
            var newPostRecord = new TestResult
            {
                UserId = 1,
                Id = 101,
                Body = "Some test body",
                Title = "Some test title"
            };

            _request.SetMethod(Method.POST);
            _request.AddJsonBody(newPostRecord);
            _request.Execute();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(true, _request.IsSuccessful());
                Assert.That(_request.GetResponse()[0].UserId, Is.EqualTo(newPostRecord.UserId));
                Assert.That(_request.GetResponse()[0].Id, Is.EqualTo(newPostRecord.Id));
                Assert.That(_request.GetResponse()[0].Body, Is.EqualTo(newPostRecord.Body));
                Assert.That(_request.GetResponse()[0].Title, Is.EqualTo(newPostRecord.Title));
            });
        }

        [Test, Order(2)]
        public void TestPut_ShouldUpdateExistingResult()
        {
            _request.SetEndpoint("posts/1");
            _request.SetMethod(Method.PUT);
            _request.AddJsonBody(new TestResult
            {
                Id = 1,
                UserId = 1,
                Body = "Eddited body",
                Title = "Eddited title"
            });
            _request.Execute();

            Assert.Multiple(() =>
            {
                Assert.That(_request.GetResponse()[0].Body, Is.EqualTo("Eddited body"));
                Assert.That(_request.GetResponse()[0].Title, Is.EqualTo("Eddited title"));
            });
        }

        [Test, Order(3)]
        public void TestDelete()
        {
            _request.SetEndpoint("posts/100");
            _request.SetMethod(Method.DELETE);
            _request.Execute();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(true, _request.IsSuccessful());
                Assert.AreEqual(HttpStatusCode.OK, _request.GetStatusCode());
            });
        }
    }
}
