using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WinApp.Views.ViewModels
{
	public class StockBaseViewModel : BaseViewModel
	{
		string name, symbol, exchange;
		string price;

		public string Name
		{
			get { return name; }
			set { name = value; NotifyPropertyChange("Name"); }
		}
		public string Symbol
		{
			get { return symbol; }
			set { symbol = value; NotifyPropertyChange("Symbol"); }
		}
		public string Price
		{
			get { return price + " USD"; }
			set { price = value; NotifyPropertyChange("Price"); }
		}
		public string Exchange
		{
			get { return exchange; }
			set { exchange = value; NotifyPropertyChange("Exchange"); }
		}
	}
}
