using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using WinApp.Controllers;
using WinApp.Models;
using WinApp.Views.ViewModels;

namespace WinApp
{
	public partial class MainWindow : Window
	{
		MainController controller;
		List<StockBaseViewModel> allStocks;
		List<StockBaseViewModel> currentExchangeStocks; //List of all the stocks from the currently selected exchange
		string selectedExchange;
		MainViewModel model;
		Timer stockGraphUpdater;

		public MainWindow()
		{
			controller = new MainController();
			InitializeComponent();

			allStocks = new List<StockBaseViewModel>();
			currentExchangeStocks = allStocks;

			model = new MainViewModel();
			this.DataContext = model;

			lvStocks.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));

			stockGraphUpdater = new Timer();
		}

		private void ListView_OnColumnClick(object sender, RoutedEventArgs e)
		{
			GridViewColumnHeader columnClicked = e.OriginalSource as GridViewColumnHeader;
			string header = (string)columnClicked.Column.Header;
			SortStocksBy(header);
		}

		private void ButtonSearchStockSymbol_Click(object sender, RoutedEventArgs e)
		{
			if (tb_Search.Text != null)
			{
				if (tb_Search.Text == "")
				{
					DisplayStocks(selectedExchange);
					return;
				}

				List<StockBaseViewModel> tempList = new List<StockBaseViewModel>();

				for (int i = currentExchangeStocks.Count - 1; i >= 0; i--)
				{
					var item = currentExchangeStocks[i];
					if (item.Symbol.ToLower().Contains(tb_Search.Text.ToLower()))
					{
						tempList.Add(item);
					}
				}

				lvStocks.ItemsSource = tempList;
			}
		}

		private void StockList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lvStocks.SelectedItem == null) return;

			var selectedStock = ((StockBaseViewModel)lvStocks.SelectedItem);
			string sym = selectedStock.Symbol;
			string name = selectedStock.Name;
			
			model.NewPlot(sym, name);
			model.AddStockData(controller.GetHistoricalStockData(sym));
			model.DisplayDailyPrice();

			if (!stockGraphUpdater.Enabled)
			{
				stockGraphUpdater.Elapsed += (sender, e) => UpdateStockData(sender, e, sym);
			}
			else
			{
				stockGraphUpdater.Dispose();
				stockGraphUpdater = new Timer();
				stockGraphUpdater.Elapsed += (sender, e) => UpdateStockData(sender, e, sym);
			}

			stockGraphUpdater.Interval = 5000; // 10 sec
			stockGraphUpdater.Enabled = true;
		}
		private void UpdateStockData(object source, ElapsedEventArgs e, string symbol)
		{
			//model.AddDataPoint(controller.GetLatestStockData(symbol));
		}

		private void cbExchange_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string exchange = (sender as ComboBox).SelectedItem as string;
			DisplayStocks(exchange);
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			//Get all stocks
			var stockList = controller.GetSymbols();
			if (stockList != null)
			{
				allStocks = stockList;
				UpdateExchanges();
				DisplayStocks("all");
			}
		}

		private void DisplayStocks(string exchange)
		{
			if(selectedExchange == exchange)
			{
				lvStocks.ItemsSource = currentExchangeStocks;
			}
			else if(exchange.ToLower() == "all")
			{
				lvStocks.ItemsSource = allStocks;
				currentExchangeStocks = allStocks;
				selectedExchange = "all";
			}
			else
			{
				//Remove all null values
				var tmpList = allStocks.FindAll(c => c.Exchange != null && !c.Exchange.Equals(string.Empty));
				//Find all stocks with matching exchange
				var sortedList = tmpList.FindAll(x => x.Exchange.Equals(exchange));
				currentExchangeStocks = sortedList;
				lvStocks.ItemsSource = currentExchangeStocks;
				selectedExchange = exchange;
			}
		}

		private void UpdateExchanges()
		{
			cbExchange.Items.Clear();
			var tmpExchangeList = allStocks.Select(x => x.Exchange).Distinct();
			cbExchange.Items.Add("All");
			foreach (var ex in tmpExchangeList)
			{
				if(ex != "") cbExchange.Items.Add(ex);
			}
		}

		private void SortStocksBy(string columnHeader)
		{
			if (lvStocks.Items.Count <= 1) return;

			lvStocks.UnselectAll();
			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvStocks.ItemsSource);

			if (view.SortDescriptions.Count <= 0)
			{
				view.SortDescriptions.Add(new SortDescription(columnHeader, ListSortDirection.Descending));
				return;
			}

			if (view.SortDescriptions[0].Direction == ListSortDirection.Ascending)
			{
				view.SortDescriptions.Clear();
				view.SortDescriptions.Add(new SortDescription(columnHeader, ListSortDirection.Descending));
			}
			else
			{
				view.SortDescriptions.Clear();
				view.SortDescriptions.Add(new SortDescription(columnHeader, ListSortDirection.Ascending));
			}
		}
	}


	public class MainViewModel
	{
		public MainViewModel()
		{
			
			var tmp = new PlotModel { Title = "Symbol", Subtitle = "Name" };

			//// Create two line series (markers are hidden by default)
			//var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
			//series1.Points.Add(new DataPoint(0, 0));
			//series1.Points.Add(new DataPoint(10, 18));
			//series1.Points.Add(new DataPoint(20, 12));
			//series1.Points.Add(new DataPoint(30, 8));
			//series1.Points.Add(new DataPoint(40, 15));

			//var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };
			//series2.Points.Add(new DataPoint(0, 4));
			//series2.Points.Add(new DataPoint(10, 12));
			//series2.Points.Add(new DataPoint(20, 16));
			//series2.Points.Add(new DataPoint(30, 25));
			//series2.Points.Add(new DataPoint(40, 5));


			//// Add the series to the plot model
			//tmp.Series.Add(series1);
			//tmp.Series.Add(series2);

			// Axes are created automatically if they are not defined
			//tmp.Axes.Add(new LinearAxis { Title = "Time", Position = AxisPosition.Bottom, Minimum = 0, Maximum = 200 });
			//tmp.Axes.Add(new LinearAxis { Title = "Price", Position = AxisPosition.Left, Minimum = -100, Maximum = 200 });

			// Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
			this.Model = tmp;
		}

		public PlotModel Model { get; private set; }
		private List<StockData> stockDataList;
		private List<StockData> todaysList;

		public void NewPlot(string title, string subtitle)
		{
			stockDataList = new List<StockData>();
			Model.Title = title;
			Model.Subtitle = subtitle;
			Model.Series.Clear();
			Model.InvalidatePlot(true);
		}

		public void AddStockData(List<StockData> list)
		{
			stockDataList = list;
		}

		public void AddDataPoint(StockData data)
		{
			stockDataList.Add(data);

			DateTime d = DateTime.ParseExact(data.date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

			if(Model.Series.Count > 0) (Model.Series[0] as LineSeries).Points.Insert(0, new DataPoint(DateTimeAxis.ToDouble(d), data.close));
			
			Model.InvalidatePlot(true);
		}

		public void DisplayDailyPrice()
		{
			if (stockDataList.Count <= 0) return; //Return if there is no data to display

			Model.Axes.Clear();

			//var list = new List<Tuple<DateTime, double>>();
			var candleList = new List<StockData>();
			double minPrice = stockDataList[0].close;
			double maxPrice = stockDataList[0].close;
			DateTime today = DateTime.Now.AddDays(0); //day to display
			DateTime lastDataPoint = today;// DateTime.ParseExact(stockDataList[0].date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

			foreach (var data in stockDataList)
			{
				DateTime d = DateTime.ParseExact(data.date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
				//Check if datapoint is the same day and add it to the list
				if (d.Date.Day == today.Date.Day && d.Date.Month == today.Date.Month)
				{
					if (d.CompareTo(lastDataPoint) > 0) lastDataPoint = d;

					if (data.close < minPrice) minPrice = data.close;
					if (data.close > maxPrice) maxPrice = data.close;

					//list.Add(new Tuple<DateTime, double>(d, data.close));
					candleList.Add(data);
				}
			}

			//Sets timeline to display between 9:30 and the last data collected (+1 hour)
			var startDate = today.Date.Add(new TimeSpan(9, 30, 0));
			var endDate = lastDataPoint.AddHours(1); //display one hour forward
			var minTime = DateTimeAxis.ToDouble(startDate);
			var maxTime = DateTimeAxis.ToDouble(endDate);

			Model.Axes.Add(new DateTimeAxis { Title = "Time", Position = AxisPosition.Bottom, StringFormat = "HH:mm",
											MinorIntervalType = DateTimeIntervalType.Auto,
											MajorGridlineStyle = LineStyle.Dot,
											MinorGridlineStyle = LineStyle.Dot,
											MajorGridlineColor = OxyColor.FromRgb(44, 44, 44),
											TicklineColor = OxyColor.FromRgb(82, 82, 82) });
			Model.Axes.Add(new LinearAxis { Title = "Price (USD)", Position = AxisPosition.Left,
											MajorGridlineStyle = LineStyle.Dot,
											MinorGridlineStyle = LineStyle.Dot,
											MajorGridlineColor = OxyColor.FromRgb(44, 44, 44),
											TicklineColor = OxyColor.FromRgb(82, 82, 82)});

			//Model.Axes.Add(new DateTimeAxis { Title = "Time (Today)", Position = AxisPosition.Bottom, Minimum = minTime, Maximum = maxTime, StringFormat = "HH:mm" });
			//Model.Axes.Add(new LinearAxis { Title = "Price (USD)", Position = AxisPosition.Left, Minimum = minPrice*0.999, Maximum = maxPrice*1.001});

			todaysList = candleList;

			//Sort list by date
			todaysList.Sort((x, y) => DateTime.Compare(x.dt, y.dt));

			AddCandleStickSeries("price", candleList);
			//AddLineSeries("price", list);
			Model.InvalidatePlot(true);
		}

		private void AddLineSeries(string title, List<Tuple<DateTime, double>> data)
		{
			var newSeries = new LineSeries { Title = title, MarkerType = MarkerType.Circle };
			foreach (var point in data)
			{
				newSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(point.Item1), point.Item2));
			}

			Model.Series.Add(newSeries);
		}

		private void AddCandleStickSeries(string title, List<StockData> list)
		{
			CandleStickSeries newSeries = new CandleStickSeries()
			{
				Color = OxyColors.Black,
				IncreasingColor = OxyColor.FromRgb(0, 197, 49),
				DecreasingColor = OxyColor.FromRgb(255, 95, 95),
				DataFieldX = "dt",
				DataFieldHigh = "high",
				DataFieldLow = "low",
				DataFieldClose = "close",
				DataFieldOpen = "open",
				TrackerFormatString = "Date: {2}\nOpen: {5:0.00000}\nHigh: {3:0.00000}\nLow: {4:0.00000}\nClose: {6:0.00000}",
				ItemsSource = todaysList
			};

			Model.Series.Add(newSeries);
		}

		//https://github.com/oxyplot/oxyplot
	}
}
