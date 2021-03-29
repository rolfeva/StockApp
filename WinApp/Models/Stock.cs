using System;
using System.Collections.Generic;
using System.Text;

namespace WinApp.Models
{
    public class StockBase
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string exchange { get; set; }
    }

    public class StockData
	{
        public string date { get; set; }
        public double open { get; set; }
        public double low { get; set; }
        public double high { get; set; }
        public double close { get; set; }
        public int volume { get; set; }

        public DateTime dt { get { return DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); } }
    }


    public class Stock
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double changesPercentage { get; set; }
        public double change { get; set; }
        public double dayLow { get; set; }
        public double dayHigh { get; set; }
        public double yearHigh { get; set; }
        public double yearLow { get; set; }
        public double marketCap { get; set; }
        public double priceAvg50 { get; set; }
        public double priceAvg200 { get; set; }
        public int volume { get; set; }
        public int avgVolume { get; set; }
        public string exchange { get; set; }
        public double open { get; set; }
        public double previousClose { get; set; }
        public double eps { get; set; }
        public double pe { get; set; }
        public DateTime earningsAnnouncement { get; set; }
        public long sharesOutstanding { get; set; }
        public int timestamp { get; set; }
    }
}
