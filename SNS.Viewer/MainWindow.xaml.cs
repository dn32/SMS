using SNS.Library;
using System;
using System.Threading;
using System.Windows;

namespace SNS.Viewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            MainWindow1.Left = screenWidth - MainWindow1.Width - 400;
            MainWindow1.Top = screenHeight - MainWindow1.Height - 40;

            _ = new Startup().Run(Callback, TimeSpan.FromSeconds(2));
        }

        private void Callback(Package p)
        {
            this.Dispatcher.Invoke(() =>
            {
                var txt = $"In: {p.TensaoEntrada}V - Out: {p.TensaoSaida}V - Pwr: {p.PotenciaSaida}% - Temp: {p.Temperatura}º - Bat: {p.PorcentagemTensaoBateria}% ({(p.Status.ByPass ? "AC" : "Bateria")})";
                TxtValor.Text = txt;
            });
        }
    }
}
