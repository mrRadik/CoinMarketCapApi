﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BussinesFacade.Defenitions;
using BussinesFacade.Extensions;
using Newtonsoft.Json;

namespace BussinesFacade
{
    public class CoinMarketCapService
    {
        private readonly string _coinMarketCapUrl;
        private readonly string _currencyInfopUrl;
        private readonly string _apiKey;

        public CoinMarketCapService( )
        {
            _coinMarketCapUrl = ConfigurationManager.AppSettings["UrlCryptoCurrencyList"];
            _currencyInfopUrl = ConfigurationManager.AppSettings["UrlCryptoCurrencyInfo"];
            _apiKey = ConfigurationManager.AppSettings["APIKey"];
        }

        public List<Data> GetCurrencies(string symbol, int limit, string convertCurrency, string sortingFields,
            string sortDirrections, int startPosition = 1)
        {
            var url = new UriBuilder(_coinMarketCapUrl);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = startPosition.ToString();
            queryString["limit"] = limit.ToString();
            queryString["convert"] = convertCurrency;
            queryString["sort"] = sortingFields;
            queryString["sort_dir"] = sortDirrections;

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);
            client.Headers.Add("Accepts", "application/json");
            var json = client.DownloadString(url.ToString());

            var e = JsonConvert.DeserializeObject<CurrencyModel>(json).Data.Take(10).ToList();
            foreach (var item in e)
            {
                item.Logo = GetLogoUrl(item.Id);
            }

            return e;
        }

        public string GetLogoUrl(long id)
        {
            var url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/info");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["id"] = id.ToString();

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);
            client.Headers.Add("Accepts", "application/json");
            var json = client.DownloadString(url.ToString());
            json = json.Replace($"\"{id.ToString()}\":", "\"Item\":");
            return JsonConvert.DeserializeObject<CurrencyInfoModel>(json).Data.Item.Logo.ToString();
        }
    }
}