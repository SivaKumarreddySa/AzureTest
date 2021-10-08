using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Partner.Comms.Common;
using Partner.Comms.PayLink.FuncApp.Controllers;
using Partner.Comms.Integration.Tests.Infrastructure;
using Partner.Comms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Partner.Comms.DTO;

namespace Partner.Comms.Integration.Tests
{
   

    public class CommunicationsControllerTest : BaseController<CommunicationsController>
    {
  
        private readonly ITestOutputHelper _output;
        public readonly CommunicationsController _controller;

        private Dictionary<string, int> _response { get; set; }
    

        public CommunicationsControllerTest(
                ITestOutputHelper output
            )
        {
            _controller = new CommunicationsController(Host.ServiceProvider.GetRequiredService<IErrorService>(),
                                                    Host.ServiceProvider.GetRequiredService<ICommsService>());
            _output = output;
            _response = new Dictionary<string, int>();
                    
         
        }

        //[Fact]
        //public void TC_4_ChannelIDNull_DontSendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_4_ChannelIDNull_DontSendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}


        //[Fact]
        //public void TC_5_EmailNull_RegexFail_DontSendToNextQueue()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_5_EmailNull_RegexFail_DontSendToNextQueue.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_6_EmailNotNull_RegexFail_SendMessageToQueue()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_6_EmailNotNull_RegexFail_SendMessageToQueue.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 1);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_7_EmailNull_RegexFail_DontSendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_7_EmailNull_RegexFail_DontSendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_8_EmailNull_RegexFail_DontSendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_8_EmailNull_RegexFail_DontSendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        [Fact]
        public void TC_9_EmailNotNull_RegexFail_SendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_9_EmailNotNull_RegexFail_SendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 1);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }

        [Fact]
        public void TC_10_EmailNull_RegexFail_DontSendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_10_EmailNull_RegexFail_DontSendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 0);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }


        [Fact]
        public void TC_11_EmailNull_RegexFail_DontSendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_11_EmailNull_RegexFail_DontSendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 0);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }

        [Fact]
        public void TC_12_EmailNotNull_RegexFail_SendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_12_EmailNotNull_RegexFail_SendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 1);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }


        [Fact]
        public void TC_13_ChannelNotActive_DontSendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_13_Channel-13NotActive_DontSendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 0);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }


        [Fact]
        public void TC_13_StatusNotA_ChannelNotActive_DontSendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_13_StatusNotA_Channel-13NotActive_DontSendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 0);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }


        [Fact]
        public void TC_15_StatusNotA_DontSendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_15_StatusNotA_DontSendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 0);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }


        [Fact]
        public void TC_16_DocBalAmtLessThan0_DontSendMessage()
        {
            // arrange
            var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_16_DocBalAmtLessThan0_DontSendMessage.json");
            var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
            // act
            var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
            // assert
            IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
            result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
            Assert.True(count == 0);
            _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        }


        //[Fact]
        //public void TC_17_DocketStatusA_DocketBalanceLessThan0_DontSendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_17_DocketStatusA_DocketBalanceLessThan0_DontSendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_18_PaylinkRules_Pass_SendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_18_PaylinkRules_Pass_SendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 1);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}


        //[Fact]
        //public void TC_19_DocStatusNotA_DontSendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_19_DocStatusNotA_DontSendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_20_21_ChannelSMSNotConfigured_DefaultPrimary_SendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_20_21_ChannelSMSNotConfigured_DefaultPrimary_SendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 1);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}


        //[Fact]
        //public void TC_22_24_Channel_SMS_Email_Configured_Primary_SendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_22_24_Channel_SMS_Email_Configured_Primary_SendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 1);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_23_25_Channel_SMS_Email_Configured_Delivery_SendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_23_25_Channel_SMS_Email_Configured_Delivery_SendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 1);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_27_DocketStatusE_PhoneAndEmailNull_DontSendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_27_DocketStatusE_PhoneAndEmailNull_DontSendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_28_DocketStatusA_PhoneAndEmailNull_DontSendMessage()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_28_DocketStatusA_PhoneAndEmailNull_DontSendMessage.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}


        //[Fact]
        //public void TC_18_Send_Message_To_Paylink_Queue()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_18_Send_Message_To_Paylink_Queue.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 1);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}

        //[Fact]
        //public void TC_4_ChannelID_Null()
        //{
        //    // arrange
        //    var payload = Helper.GetJsonPayload("Payload\\PaylinkRules\\", "TC_4_ChannelID_Null.json");
        //    var request = Helper.CreateHttpRequest(payload, HttpMethods.Post);
        //    // act
        //    var response = (OkObjectResult)_controller.HttpTriggerCreateLongURL(request, Guid.NewGuid().ToString(), Logger).Result;
        //    // assert
        //    IDictionary<string, int> result = Assert.IsAssignableFrom<IDictionary<string, int>>(response.Value);
        //    result.TryGetValue(typeof(CommunicationDTO).Name, out int count);
        //    Assert.True(count == 0);
        //    _output.WriteLine("Output:\n {0}", string.Join("\n ", result.Select(m => $"{m.Key}: {m.Value}")));
        //}



    }
}