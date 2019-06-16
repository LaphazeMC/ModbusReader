using ModbusReader.Services;
using ModbusReader.Services.Implementation;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace ModbusReader
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> ListSources { get; set; } = new List<string>(new string[] { "Serial port", "TCP/IP" });
        public List<string> ListPorts { get; set; }
        public List<string> ListReadFunctions { get; set; } = new List<string>(new string[] { "0x01 - Read Coil", "0x02 - Read Input", "0x03 - Read Holding Register - 0x04 - Read Input Register" });
        public string SelectedSource { get; set; }
        public string SelectedPort { get; set; }
        public string IpAddressValue { get; set; }
        public int PortValue { get; set; } = 502;
        public string StartAddressValue { get; set; }
        public int QuantityToReadValue { get; set; } = 1;
        public int TimeRefreshValue { get; set; } = 100;
        public string SelectedReadFunction { get; set; }
        public IModbusService modbusService;
        public Notifier Notifier { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.modbusService = new ModbusService();
            this.Notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            this.ListPorts = SerialPort.GetPortNames().ToList();
        }

        private void SourceSelectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.SelectedSource == this.ListSources.First()) // Serial mode
            {
                this.ComPortSelectList.IsEnabled = true;
                this.IpAddressInput.IsEnabled = false;
                this.PortInput.IsEnabled = false;
            }
            else // TCP IP
            {
                this.ComPortSelectList.IsEnabled = false;
                this.IpAddressInput.IsEnabled = true;
                this.PortInput.IsEnabled = true;
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            var isActionSuccess = false;
            if(this.SelectedSource == this.ListSources.First()) // Serial mode
            {
                isActionSuccess = modbusService.ToggleConnection(this.SelectedPort, true);
            }
            else // TCP/IP
            {
                if (Utils.IpAddress.IsIpV4Valid(this.IpAddressValue))
                {
                    isActionSuccess = modbusService.ToggleConnection(this.IpAddressValue, this.PortValue, true);
                }
                else
                {
                    this.Notifier.ShowError("Incorrect IP Address !");
                    return;
                }
            }
            if (isActionSuccess)
            {
                // change connection state button
            }
            else
            {
                this.Notifier.ShowError("Connection refused ! Nothing is listening on the IP:Port or this is blocked by firewall");
            }
        }
    }
}
