using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using TelegramBotNet.Models;

namespace TelegramBotNet
{
    public class ProcessServiceClient
    {
        private readonly RestClient _restClient;

        public ProcessServiceClient(ConfigurationModel configuration)
        {
            var serverUri = new Uri(configuration.ServerConfiguration.Uri);
            var serviceUri = new Uri(serverUri, "api/public/system/Process/ProcessObjectService");
            _restClient = new RestClient(serviceUri);
            _restClient.Authenticator = new HttpBasicAuthenticator(configuration.ActiveDirectoryCredentials.Login, configuration.ActiveDirectoryCredentials.Password);
        }

        public async Task<Response> CreateWithObjectId(string processAppId, string recordId)
        {
            var restRequest = new RestRequest("CreateWithObjectId", Method.POST);
            restRequest.AddJsonBody(new
            {
                processAppId,
                objectName = "Created from bot",
                objectId = recordId
            });
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "application/json");
            Response result = new Response();
            HttpStatusCode responceCode = HttpStatusCode.Unused;

            await Task.Run(() =>
            {
                var restResp = _restClient.Execute(restRequest);
                result.Content = restResp.Content;
                responceCode = restResp.StatusCode;
            });
            if (responceCode == HttpStatusCode.OK)
            {
                result.Success = true;
            }
            return result;
        }
    }

    public class Response
    {
        public string Content { get; set; }

        public bool Success { get; set; }
    }
}