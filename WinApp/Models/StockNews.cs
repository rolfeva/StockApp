using System;
using System.Collections.Generic;
using System.Text;

namespace WinApp.Models
{
    public class StockNews
    {
        public string symbol { get; set; }
        public string publishedDate { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string site { get; set; }
        public string text { get; set; }
        public string url { get; set; }
    }
}
