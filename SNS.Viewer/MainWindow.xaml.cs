using SNS.Library;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SNS.Viewer
{
    public partial class MainWindow : Window
    {
        private readonly TimeSpan GET_DATA_FREQUENCY = TimeSpan.FromSeconds(1);

        public Point AnchorPoint { get; set; }

        public bool InDrag { get; set; }

        public NobreakInterface NobreakInterface { get; set; }

        public Package Package { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            MainWindow1.Left = screenWidth - MainWindow1.Width - 400;
            MainWindow1.Top = screenHeight - MainWindow1.Height - (screenHeight > screenWidth ? 82 : 40);
            NobreakInterface = new NobreakInterface(Callback);
            _ = Init();
        }

        private async Task Init()
        {
            while (true) // Todo - Usar token de cancelamento
            {
                await Connect();
                await GetStatus();
            }
        }

        private async Task Connect()
        {
            while (true)
            {
                try
                {
                    NobreakInterface.Connect();
                    Write("Conectado. Aguardando...");
                    break;
                }
                catch (Exception ex)
                {
                    Write(ex.Message);
                }

                await Task.Delay(GET_DATA_FREQUENCY);
            }
        }

        private async Task GetStatus()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        NobreakInterface.GetStatus();
                        await Task.Delay(GET_DATA_FREQUENCY);
                    }
                    catch (Exception ex)
                    {
                        Write(ex.Message);
                        break;
                    }
                }
            });
        }

        private void Write(string text)
        {
            Dispatcher.Invoke(() =>
            {
                TxtValor.Text = text;
            });
        }

        public Configs Configs { get; set; }

        private void MainWindow1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Configs = new Configs(NobreakInterface, this);
            Configs.Show();
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

        private void Callback(Package package)
        {
            Package = package;
            Write(package.ToString());
            if (Configs != null)
            {
                Dispatcher.Invoke(() =>
                {
                    Configs.AtualizarTela();
                });
            }
        }
    }
}
