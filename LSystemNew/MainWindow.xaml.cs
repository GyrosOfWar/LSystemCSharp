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

        double DegToRad(double deg) {
            return deg * (Math.PI / 180.0);
        }

        public MainWindow() {
            InitializeComponent();
            this.lsystems = new LSystem[3];

            var dragonCurveRules = ImmutableDictionary.Create<char, string>().Add('X', "X+YF+").Add('Y', "-FX-Y");
            lsystems[0] = new LSystem("FX", dragonCurveRules, 12, Math.PI / 2, "DragonCurve", 5, new Vector(400, 200));

            var hilbertRules = ImmutableDictionary.Create<char, string>().Add('A', "-BF+AFA+FB-").Add('B', "+AF-BFB-FA+");
            lsystems[1] = new LSystem("A", hilbertRules, 6, Math.PI / 2, "HilbertCurve", 6, new Vector(imgX / 2 - 150, imgY - 50));

            var plantRules = ImmutableDictionary.Create<char, string>().Add('F', "1FF-[2-F+F]+[3+F-F]").Add('X', "1FF+[2+F]+[3-F]");
            lsystems[2] = new LSystem("FX", plantRules, 6, DegToRad(25), "Plant", 5, new Vector(50, 300));

            this.image = BitmapFactory.New(imgX, imgY);
            this.lsImage.Source = image;

            var names = lsystems.Select((l) => l.Name);
            foreach (var name in names) {
                comboBox.Items.Add(name);
            }
            comboBox.SelectedIndex = 0;
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
