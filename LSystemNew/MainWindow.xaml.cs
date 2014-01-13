using System;
using System.Collections.Immutable;
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

namespace LSystemNew {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        LSystem[] lsystems;
        int selectedLsys = 0;

        WriteableBitmap image;
        const int imgX = 809;
        const int imgY = 506;

        public MainWindow() {
            InitializeComponent();
            this.lsystems = new LSystem[] { LSystem.DragonCurve, LSystem.Plant, LSystem.HilbertCurve };

            this.image = BitmapFactory.New(imgX, imgY);
            this.lsImage.Source = image;

            var names = lsystems.Select((l) => l.Name);
            foreach (var name in names) {
                comboBox.Items.Add(name);
            }
            comboBox.SelectedIndex = selectedLsys;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            using (image.GetBitmapContext()) {
                image.Clear();
                lsystems[selectedLsys].Draw(image);
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            selectedLsys = comboBox.SelectedIndex;
        }
    }
}
