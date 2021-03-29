using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using WinApp.Models;

namespace WinApp.API
{
	public class StockAPI : Api
	{
		public StockAPI()
		{
		}
		public List<StockBase> GetSymbolList()
		{
			string path = "/v3/stock/list?";
			var response = GET(GetURI(path));
			string content = response.Content.ReadAsStringAsync().Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<List<StockBase>>(content);
			}
			else
			{
				Console.WriteLine("Error code: " + response.StatusCode);
				return null;
			}
		}

		public List<Stock> GetStockData(string symbols)
		{
			string path = "/v3/quote/" + symbols + "?";
			var response = GET(GetURI(path));
			string content = response.Content.ReadAsStringAsync().Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				//Jsonconverter is set to ignores null values
				return JsonConvert.DeserializeObject<List<Stock>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			}
			else
			{
				Console.WriteLine("Error code: " + response.StatusCode);
				return null;
			}
		}

		public List<StockData> GetHistoricalStockData(string symbol)
		{
			string path = "/v3/historical-chart/5min/" + symbol + "?";
			var response = GET(GetURI(path));
			string content = response.Content.ReadAsStringAsync().Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				//Jsonconverter is set to ignores null values
				return JsonConvert.DeserializeObject<List<StockData>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			}
			else
			{
				Console.WriteLine("Error code: " + response.StatusCode);
				return null;
			}
		}

		public List<StockNews> GetStockNews()
		{
			string path = "/v3/stock_news?limit=50";
			var response = GET(GetURI(path));
			string content = response.Content.ReadAsStringAsync().Result;

			if(response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				//Jsonconverter is set to ignore null values
				return JsonConvert.DeserializeObject<List<StockNews>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			}
			else
			{
				Console.WriteLine("Error code: " + response.StatusCode);
				return null;
			}
		}

		//public async Task<string> SubscribeTo()
		//{
		//	CancellationTokenSource source = new CancellationTokenSource();
		//	CancellationToken token = source.Token;

		//	Uri subscribeURI = new Uri("wss://ws.twelvedata.com/v1/quotes/price");

		//	using (ClientWebSocket client = new ClientWebSocket())
		//	{
		//		client.Options.SetRequestHeader("X-TD-APIKEY", Key);

		//		try
		//		{
		//			await client.ConnectAsync(subscribeURI, token);
		//		}
		//		catch (Exception ex)
		//		{
		//			Console.WriteLine(ex);
		//			throw;
		//		}

		//		ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[8192]);
		//		WebSocketReceiveResult result = null;

		//		while (client.State == WebSocketState.Open)
		//		{
		//			Trace.WriteLine("Connection to API was successful");

		//			result = await client.ReceiveAsync(bytesReceived, CancellationToken.None);

		//			var response = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
		//			Trace.WriteLine("Response: " + response);

		//			//ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes("'action': 'subscribe', 'params': {'symbols': 'AAPL'}"));
		//			//await client.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);

		//			return null;

		//		}
		//	}

		//	return null;
		//}
	}
}

//https://fmpcloud.io/documentation#realtimeQuote