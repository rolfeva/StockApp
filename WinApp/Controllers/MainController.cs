using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WinApp.API;
using WinApp.Models;
using WinApp.Views.ViewModels;

namespace WinApp.Controllers
{
	public class MainController
	{
		public List<StockBaseViewModel> GetSymbols()
		{
			StockAPI api = new StockAPI();
			var stockQuery = from stock in api.GetSymbolList() select
							 new StockBaseViewModel
							 {
								 Name = stock.name,
								 Symbol = stock.symbol,
								 Price = stock.price.ToString(),
								 Exchange = stock.exchange
							 };
			List<StockBaseViewModel> list = stockQuery.ToList();
			return list;
		}

		public List<StockViewModel> GetStocks(string symbols)
		{
			StockAPI api = new StockAPI();
			var stockQuery = from stock in api.GetStockData(symbols) select
							new StockViewModel
							{
							Name = stock.name,
							Symbol = stock.symbol,
							Price = stock.price.ToString(),
							Exchange = stock.exchange,
							Volume = stock.volume,
							AvgVolume = stock.avgVolume
							};
			List<StockViewModel> list = stockQuery.ToList();
			return list;
		}

		public List<StockData> GetHistoricalStockData(string symbol)
		{
			StockAPI api = new StockAPI();
			//DateTime requestedDate = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

			//var stockData = api.GetHistoricalStockData(symbol);
			//var dailyData = new List<StockData>();
			//foreach (var data in stockData)
			//{
			//	DateTime d = DateTime.ParseExact(data.date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
			//	if (d.Date.CompareTo(requestedDate) == 0) dailyData.Add(data); 
			//}
			//return dailyData;

			return api.GetHistoricalStockData(symbol);
		}

		public StockData GetLatestStockData(string symbol)
		{
			StockAPI api = new StockAPI();
			var stockQuery = from stock in api.GetStockData(symbol)
							 select
							new StockData
							{
								date = DateTime.Now.ToString(),
								open = stock.open,
								low = stock.dayLow,
								high = stock.dayHigh,
								close = stock.price,
								volume = stock.volume
							};
			
			return stockQuery.First();
		}

		public List<StockNews> GetLatestStockNews()
		{
			StockAPI api = new StockAPI();
			var stockQuery = from stock in api.GetStockNews()
							 select
							new StockNews
							{
								symbol = stock.symbol,
								publishedDate = stock.publishedDate,
								title = stock.title,
								image = stock.image,
								site = stock.site,
								text = stock.text,
								url = stock.url
							};

			return stockQuery.ToList();
		}

		//public async void TestConnect()
		//{
		//	StockAPI stockAPI = new StockAPI();
		//	await stockAPI.SubscribeTo();
		//}
		
	}
}
