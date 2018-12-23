using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataController;
using Data.Recievers;
using System.IO;
using UILib;

namespace Task7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UIController ParabolicSARController { get; set; }
        public UIController CandleUIController { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            IncomingDataController controller =
                new IncomingDataController(new ExcelDataReciever(new FileInfo(@"Source\prices.xlsx")));
            controller.StartReceive(1000);
            controller.DataReceived += Controller_DataReceived;

            ParabolicSARController = new ParabolicSARController(20);
            CandleUIController = new CandleUIController(20);
            DataContext = this;
        }

        public static readonly DependencyProperty ChartColorProperty =
            DependencyProperty.Register("ChartColor", typeof(Brush), typeof(MainWindow));

        public static readonly DependencyProperty LastTimeProperty =
            DependencyProperty.Register("LastTime", typeof(string), typeof(MainWindow));

        public static readonly DependencyProperty OpenProperty =
            DependencyProperty.Register("OpenValue", typeof(decimal), typeof(MainWindow));

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.Register("CloseValue", typeof(decimal), typeof(MainWindow));

        public static readonly DependencyProperty HighProperty =
            DependencyProperty.Register("HighValue", typeof(decimal), typeof(MainWindow));

        public static readonly DependencyProperty LowProperty =
            DependencyProperty.Register("LowValue", typeof(decimal), typeof(MainWindow));

        public Brush ChartColor
        {
            get => Dispatcher.Invoke(() => (Brush)GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => SetValue(ChartColorProperty, value));
        }

        public decimal OpenValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => SetValue(OpenProperty, value));
        }

        public decimal CloseValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(CloseProperty));
            set => Dispatcher.Invoke(() => SetValue(CloseProperty, value));
        }

        public decimal HighValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(HighProperty));
            set => Dispatcher.Invoke(() => SetValue(HighProperty, value));
        }

        public decimal LowValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(LowProperty));
            set => Dispatcher.Invoke(() => SetValue(LowProperty, value));
        }

        private void Controller_DataReceived(Data.Candle data)
        {
            if (data != null)
            {
                OpenValue = data.Open;
                CloseValue = data.Close;
                HighValue = data.High;
                LowValue = data.Low;
                LastTime = data.Time.ToString();
                ParabolicSARController.AddValueToLine(data, (x) => x.Close);
                CandleUIController.AddValueToLine(data, (x) => x.Close);
            }
        }

        public string LastTime
        {
            get => Dispatcher.Invoke(() => (string)GetValue(LastTimeProperty));
            set => Dispatcher.Invoke(() => this.SetValue(LastTimeProperty, value));
        }
    }
}
