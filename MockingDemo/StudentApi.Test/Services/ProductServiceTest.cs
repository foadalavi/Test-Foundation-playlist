using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using StudentApi.Models.Config;
using StudentApi.Services;
using StudentApi.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApi.Test.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task OnGetProductAsync_WhanSuccess_ShouldReturnOneDummyProduct()
        {
            // Arrange
            var httpMessagehandlerMock = new Mock<HttpMessageHandler>();
            var httpresponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(DataFixture.GetDummyProduct()))
            };

            httpMessagehandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(httpresponse);

            var httpClient = new HttpClient(httpMessagehandlerMock.Object);
            var config = Options.Create(new ProductOptions()
            {
                EndPoint = "http://testEndpoint.com",
                MaxId = 15
            });
            var service = new ProductService(httpClient, config);

            // Act

            var response = await service.GetProductAsync(1);

            // Assert
            response.Should().BeEquivalentTo(DataFixture.GetDummyProduct());
        }
    }
}
