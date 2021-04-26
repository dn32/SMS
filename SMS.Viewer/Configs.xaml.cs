using SMS.Library;
using System.Windows;

namespace SMS.Viewer
{
    public partial class Configs : Window
    {
        public NobreakInterface NobreakInterface { get; }

        public MainWindow MainWindow { get; }

        public Configs(NobreakInterface nobreakInterface, MainWindow mainWindow)
        {
            InitializeComponent();
            NobreakInterface = nobreakInterface;
            MainWindow = mainWindow;
            AtualizarTela();
        }

        public void AtualizarTela()
        {
            if (MainWindow.Package == null) return;
            BtnBeepOnOf.IsEnabled = !MainWindow.Package.Status.ByPass;

            BtnTestarBaterias.IsEnabled = !MainWindow.Package.Status.TesteAtivo;
            BtnParaTesteDebaterias.IsEnabled =  MainWindow.Package.Status.TesteAtivo;

            // Status não está funcionando
             //BtnBeepOnOf.Content = MainWindow.Package.Status.BeepLigado ? "Desligar beep" : "Ligar beep";
        }

        private void BtnBeepOnOf_Click(object sender, RoutedEventArgs e)
        {
            NobreakInterface.SetBeepOnOf();
            if (MainWindow.Package?.Status != null)
                MainWindow.Package.Status.BeepLigado = !MainWindow.Package.Status.BeepLigado;
            AtualizarTela();
        }

        private void BtnTestarBaterias_Click(object sender, RoutedEventArgs e)
        {
            NobreakInterface.SetTestOn();
            if (MainWindow.Package?.Status != null)
                MainWindow.Package.Status.TesteAtivo = true;
            AtualizarTela();
        }

        private void BtnParaTesteDebaterias_Click(object sender, RoutedEventArgs e)
        {
            NobreakInterface.SetTestOff();
            if (MainWindow.Package?.Status != null)
                MainWindow.Package.Status.TesteAtivo = false;
            AtualizarTela();
        }
    }
}
