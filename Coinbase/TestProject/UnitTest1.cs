using Coinbase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class UnitTest1 : IDisposable
    {
       
        protected TestServer testServer;

        public UnitTest1()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<StartupTest>();
            testServer = new TestServer(webBuilder);
        }

        [Fact]
        public async Task GetAllRates()
        {
            var response = await testServer.CreateRequest("/api/ExchangeRatesController").SendAsync("GET");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);


        }
        [Fact]
        public async Task GetRatesByID()
        {
            var response = await testServer.CreateRequest("/api/ExchangeRatesController/BTC").SendAsync("GET");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            response = await testServer.CreateRequest("/api/ExchangeRatesController/ETH").SendAsync("GET");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SaveRate()
        {
            var _crypto = new RequestData()
            {
                id = 1,
                exchangerate = "BTC",
                key = "AES",
                value = "2340496.88"

            };
            var response = await testServer.CreateRequest("/api/ExchangeRatesController/"+_crypto).SendAsync("POST");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

          
        }
        [Fact]
        public async Task UpdateRateByID()
        {
            int id = 5;
            var response = await testServer.CreateRequest("/api/ExchangeRatesController/id="+id).SendAsync("PUT");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);


        }
        [Fact]
        public async Task DeleteRateByID()
        {
            int id = 5;
            var response = await testServer.CreateRequest("/api/ExchangeRatesController/id=" + id).SendAsync("DELETE");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);


        }
        public void Dispose()
        {
            testServer.Dispose();
        }
    }

      
    }

