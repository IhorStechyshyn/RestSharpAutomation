using RestSharp;
using System.Net;

namespace RestSharpAutomation.Utilities
{
    public class ApiRequest<T> where T : new()
    {
        private readonly RestClient _client;
        private readonly RestRequest _request;
        private IRestResponse<T> _response;

        public ApiRequest(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            _request = new RestRequest();
        }

        public void SetEndpoint(string url)
        {
            _request.Resource = url;
        }

        public void SetMethod(Method method)
        {
            _request.Method = method;
        }

        public void Execute()
        {
            _response = _client.Execute<T>(_request);
        }

        public T GetResponse()
        {
            return _response.Data;
        }

        public HttpStatusCode GetStatusCode()
        {
            return _response.StatusCode;
        }

        public bool IsSuccessful()
        {
            return _response.IsSuccessful;
        }

        public void AddJsonBody(object body)
        {
            _request.AddJsonBody(body);
        }
    }
}
