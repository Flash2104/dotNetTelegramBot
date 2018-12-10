using System;
using System.Collections.Generic;
using System.Linq;
//using ASITelBot.Commands;
//using Comindware.Platform.Contracts;
//using Comindware.Platform.WebApi.Client;
using TelegramBotNet.Models;

namespace ASITelBot
{
    public class PlatformClient
    {
        //private IRecordService recordService;

        private readonly string _solutionAlias;
        private readonly string _templateAlias;
        private readonly string _uri;

        public PlatformClient(ConfigurationModel configuration)
        {
            _uri = configuration.ServerConfiguration.Uri;
            _solutionAlias = configuration.ServerConfiguration.SolutionAlias;
            _templateAlias = configuration.ServerConfiguration.RecordTypeAlias;
            //var instanceParameters = new InterfaceInstanceParameters
            //{
            //    AuthenticationType = AuthenticationType.BasicAuth,
            //    Login = configuration.ActiveDirectoryCredentials.Login,
            //    Password = configuration.ActiveDirectoryCredentials.Password,
            //    Uri = _uri
            //};
            //this.recordService = ServiceInterfaceFactory.GetInterface<IRecordService>(instanceParameters);
        }

        public string CreateTripRecord(TripModel tripInfo)
        {
            string recordId = string.Empty;
            var properties = new Dictionary<string, object>()
            {
                {
                    "Mestokomndirovki", tripInfo.Destination
                },
                {
                    "Organizatsiya", tripInfo.Organization
                },
                {
                    "Datypoezdki", tripInfo.StartDate
                },
                {
                    "Okonchaniepoezdki", tripInfo.EndDate
                }
            };
           // var recordId = recordService.Create(_solutionAlias, _templateAlias, properties);
            return recordId;
        }
    }
}