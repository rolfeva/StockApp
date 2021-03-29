using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;

namespace WinApp.API
{
	public class Api
	{
		public string Key { get; set; }
		public Api()
		{
			Key = GetKey("API/Key.txt");
		}

		protected HttpResponseMessage GET(string URL)
		{
			using (HttpClient client = new HttpClient())
			{
				var result = client.GetAsync(URL);
				result.Wait();

				return result.Result;
			}
		}

		protected string GetURI(string path)
		{
			string s = "https://fmpcloud.io/api" + path + "&apikey=" + Key;
			return s;
		}
		public string GetKey(string path)
		{
			StreamReader sr = new StreamReader(path);
			return sr.ReadToEnd();
		}
	}
}
