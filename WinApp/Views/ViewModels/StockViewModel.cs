using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WinApp.Views.ViewModels
{
    public class StockViewModel : StockBaseViewModel
    {
        double changesPercentage, change, dayLow, dayHigh, yearHigh, yearLow, marketCap,
               priceAvg50, priceAvg200, open, previousClose, eps, pe;
        int volume, avgVolume, timestamp;
        DateTime earningsAnnouncement;
        long sharesOutstanding;

        public double ChangesPercentage { get { return changesPercentage; } set { changesPercentage = value; NotifyPropertyChange("ChangesPercentage"); } }
        public double Change { get { return change; } set { change = value; NotifyPropertyChange("Change"); } }
        public double DayLow { get { return dayLow; } set { dayLow = value; NotifyPropertyChange("DayLow"); } }
        public double DayHigh { get { return dayHigh; } set { dayHigh = value; NotifyPropertyChange("DayHigh"); } }
        public double YearHigh { get { return yearHigh; } set { yearHigh = value; NotifyPropertyChange("YearHigh"); } }
        public double YearLow { get { return yearLow; } set { yearLow = value; NotifyPropertyChange("YearLow"); } }
        public double MarketCap { get { return marketCap; } set { marketCap = value; NotifyPropertyChange("MarketCap"); } }
        public double PriceAvg50 { get { return priceAvg50; } set { priceAvg50 = value; NotifyPropertyChange("PriceAvg50"); } }
        public double PriceAvg200 { get { return priceAvg200; } set { priceAvg200 = value; NotifyPropertyChange("PriceAvg200"); } }
        public double Open { get { return open; } set { open = value; NotifyPropertyChange("Open"); } }
        public double PreviousClose { get { return previousClose; } set { previousClose = value; NotifyPropertyChange("PreviousClose"); } }
        public double Eps { get { return eps; } set { eps = value; NotifyPropertyChange("Eps"); } }
        public double Pe { get { return pe; } set { pe = value; NotifyPropertyChange("Pe"); } }
        public int Volume { get { return volume; } set { volume = value; NotifyPropertyChange("Volume"); } }
        public int AvgVolume { get { return avgVolume; } set { avgVolume = value; NotifyPropertyChange("AvgVolume"); } }
        public int Timestamp { get { return timestamp; } set { timestamp = value; NotifyPropertyChange("Timestamp"); } }
        public DateTime EarningsAnnouncement { get { return earningsAnnouncement; } set { earningsAnnouncement = value; NotifyPropertyChange("EarningsAnnouncement"); } }
        public long SharesOutstanding { get { return sharesOutstanding; } set { sharesOutstanding = value; NotifyPropertyChange("SharesOutstanding"); } }
    }
}
