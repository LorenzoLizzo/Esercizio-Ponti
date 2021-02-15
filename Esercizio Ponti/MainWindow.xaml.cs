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
using System.Threading;

namespace Esercizio_Ponti
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r;
        List<Image> lstImage;
        List<Image> filaA;
        List<Image> filaB;
        public MainWindow()
        {
            InitializeComponent();
            r = new Random();
            lstImage = new List<Image>();
            filaA = new List<Image>();
            filaB = new List<Image>();
            RandomSpawnMacchine();
        }

        public void RandomSpawnMacchine()
        {
            lstImage.Add(img1);
            lstImage.Add(img2);
            lstImage.Add(img3);
            lstImage.Add(img4);

            foreach (Image img in lstImage)
            {
                if (r.Next(0, 2) == 0)
                {
                    img.Margin = new Thickness(181, (-50 - (60 * (filaA.Count))), 0, 0);
                    filaA.Add(img);
                }
                else
                {
                    img.Margin = new Thickness(238, (590 + (60 * (filaB.Count))), 0, 0);
                    filaB.Add(img);
                }
            }

        }
    }
}
