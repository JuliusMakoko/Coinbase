using Coinbase.Context;
using Coinbase.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coinbase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public  class ExchangeRatesController : ControllerBase
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private readonly CRUDContext _CRUDContext;

        public IConfiguration Configuration { get; }

       

        public ExchangeRatesController(CRUDContext CRUDContext,  IConfiguration Config)
        {
            _CRUDContext = CRUDContext;
            Configuration = Config;
        }

        // GET: api/<ExchangeRatesController>
        [HttpGet]
        public IEnumerable<RequestData> Get()
        {
            return _CRUDContext.requests;
        }

        // GET api/<ExchangeRatesController>/5
       // [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
        [HttpGet("{cryptoId}")]
        public async void Get(string cryptoId)
        {
            if(cryptoId.ToUpper().Equals("BTC") || cryptoId.ToUpper().Equals("ETH"))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Cryptos Currencies Exchange Rates");

                    //var stringTask = httpClient.GetStringAsync(string.Format("https://api.coinbase.com/v2/exchange-rates?currency={0}", cryptoId));
                    //var json = await stringTask;

                    var streamTask = httpClient.GetStreamAsync(string.Format("https://api.coinbase.com/v2/exchange-rates?currency={0}", cryptoId));
                    var results = await JsonSerializer.DeserializeAsync<ExchangeRatesData>(await streamTask);

                    foreach (var exchange in results.exchangeRates.rates)
                    {
                        try
                        {
                            DynamicParameters param1 = new DynamicParameters();
                            param1.Add("ExchangeRate", cryptoId.ToUpper());
                            param1.Add("Key", exchange.Key);
                            param1.Add("Value", exchange.Value);

                            var ConnString = Configuration.GetConnectionString("CoinBaseDB");

                            SqlConnection conn = new SqlConnection(ConnString);

                            List<RequestData> list1 = conn.Query<RequestData>("SaveCoinBaseExchangeRates", param1, commandType: CommandType.StoredProcedure).ToList<RequestData>();


                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message.ToString();
                        }

                    }

                }
                catch (Exception ex)
                {
                    //catch any errors here
                    string msg = ex.Message.ToString();
                }

            }
            else
            {
                //return error
            }
          
        }

        // POST api/<ExchangeRatesController>
        [HttpPost]
        public void Post([FromBody] RequestData requestData)  
        {
            var val = _CRUDContext.requests.FirstOrDefault(x => x.value == requestData.value);

            if(val == null)
            {
                _CRUDContext.requests.Add(requestData);
                _CRUDContext.SaveChanges();

            }
            else
            {
                //value exists - do not add
                int d = 8;
            }
     

        }

        // PUT api/<ExchangeRatesController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] RequestData requestData) 
        {
            _CRUDContext.requests.Update(requestData);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/<ExchangeRatesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.requests.FirstOrDefault(x => x.id == id);

            if(item != null)
            {
                _CRUDContext.requests.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
