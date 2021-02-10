using SNS.Library;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SNS.Viewer
{
    public partial class MainWindow : Window
    {
        private readonly TimeSpan GET_DATA_FREQUENCY = TimeSpan.FromSeconds(2);

        public Point AnchorPoint { get; set; }

        public bool InDrag { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            MainWindow1.Left = screenWidth - MainWindow1.Width - 400;
            MainWindow1.Top = screenHeight - MainWindow1.Height - 40;
            _ = Init();
        }

        private async Task Init()
        {
            while (new Startup().Run(Callback, GET_DATA_FREQUENCY) == false)
            {
                await Task.Delay(GET_DATA_FREQUENCY);
            }
        }

        private void GridOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnchorPoint = e.GetPosition(this);
            InDrag = true;
            CaptureMouse();
            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (InDrag)
            {
                Point currentPoint = e.GetPosition(this);
                var dif = currentPoint.X - AnchorPoint.X;

                var screenWidth = SystemParameters.PrimaryScreenWidth;
                if (Left + MainWindow1.Width >= screenWidth && dif > 0) return;
                if (Left <= 0 && dif < 0) return;

                Left += dif;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (InDrag)
            {
                ReleaseMouseCapture();
                InDrag = false;
                e.Handled = true;
            }
        }

        private void Callback(Package p)
        {
            this.Dispatcher.Invoke(() =>
            {
                var txt = $"{DateTime.Now.ToString("HH:mm ss")} - In: {p.TensaoEntrada}V - Out: {p.TensaoSaida}V - Pwr: {p.PotenciaSaida}% - Temp: {p.Temperatura}º - Bat: {p.PorcentagemTensaoBateria}% ({(p.Status.ByPass ? "AC" : "Bateria")})";
                TxtValor.Text = txt;
            });
        }
    }
}
