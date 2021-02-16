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
        readonly Uri uriMacchinaReverse = new Uri("macchina1Revers.png", UriKind.Relative);
        readonly Uri uriMacchina = new Uri("macchina1.png", UriKind.Relative);
        Random r;
        Thread t1;
        Thread t2;
        List<Image> lstImage;
        List<Image> filaA;
        List<Image> filaB;
        object locker = new object();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void RandomSpawnMacchine()
        {
            lstImage.Clear();
            lstImage.Add(img1);
            lstImage.Add(img2);
            lstImage.Add(img3);
            lstImage.Add(img4);
            lstImage.Add(img5);
            lstImage.Add(img6);
            lstImage.Add(img7);
            lstImage.Add(img8);
            lstImage.Add(img9);
            lstImage.Add(img10);
            lstImage.Add(img11);
            lstImage.Add(img12);
            lstImage.Add(img13);
            lstImage.Add(img14);
            lstImage.Add(img15);
            lstImage.Add(img16);
            lstImage.Add(img17);
            lstImage.Add(img18);
            lstImage.Add(img19);
            lstImage.Add(img20);
            for (int i = 0; i < lstImage.Count; i++)
            {
                if (r.Next(0, 2) == 0)
                {
                    lstImage[i].Source = new BitmapImage(uriMacchinaReverse);
                    lstImage[i].Margin = new Thickness(181, (-50 - (60 * (filaA.Count))), 0, 0);
                    filaA.Add(lstImage[i]);
                    lstImage.Remove(lstImage[i]);
                    i--;
                    lblAutoFilaA.Content = "Auto in fila per la filaA: " + filaA.Count;
                }
                else
                {
                    lstImage[i].Source = new BitmapImage(uriMacchina);
                    lstImage[i].Margin = new Thickness(238, (569 + (60 * (filaB.Count))), 0, 0);
                    filaB.Add(lstImage[i]);
                    lstImage.Remove(lstImage[i]);
                    i--;
                    lblAutoFilaB.Content = "Auto in fila per la filaB: " + filaB.Count;
                }
            }

        }

        public void MuoviFilaA()
        {
            if (filaA.Count != 0)
            {
                int autoInFila = filaA.Count;
                while (true)
                {
                    bool ver = false;
                    for (int i = 0; i < filaA.Count; i++)
                    {
                        int marginLeft = int.MinValue;
                        int marginTop = int.MinValue;
                        int marginBottom = int.MinValue;
                        int marginRight = int.MinValue;
                        Image img = filaA[i];
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            marginLeft = (int)img.Margin.Left;
                            marginTop = (int)img.Margin.Top;
                            marginBottom = (int)img.Margin.Bottom;
                            marginRight = (int)img.Margin.Right;
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(0.5));
                        marginTop += 1;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                        }));

                        if (marginTop == 100)
                        {
                            ver = true;
                            break;
                        }
                    }
                    if (ver)
                    {
                        break;
                    }
                }

                lock (locker)
                {
                    Queue<Image> queueImgEntrateNelPonte = new Queue<Image>();
                    while (true)
                    {
                        bool ver = false;
                        for (int i = 0; i < filaA.Count; i++)
                        {
                            int marginLeft = int.MinValue;
                            int marginTop = int.MinValue;
                            int marginBottom = int.MinValue;
                            int marginRight = int.MinValue;
                            Image img = filaA[i];
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                marginLeft = (int)img.Margin.Left;
                                marginTop = (int)img.Margin.Top;
                                marginBottom = (int)img.Margin.Bottom;
                                marginRight = (int)img.Margin.Right;
                            }));


                            Thread.Sleep(TimeSpan.FromMilliseconds(0.5));

                            if (marginTop == 100)
                            {
                                queueImgEntrateNelPonte.Enqueue(img);

                            }
                            if (queueImgEntrateNelPonte.Count < 10)
                            {
                                marginTop += 1;
                            }


                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                            }));


                            if (marginTop == 400)
                            {
                                queueImgEntrateNelPonte.Dequeue();
                                autoInFila--;
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    lblAutoFilaA.Content = "Auto in fila per la filaA: " + autoInFila;
                                }));

                                if (queueImgEntrateNelPonte.Count == 0)
                                {
                                    ver = true;
                                    break;
                                }
                            }

                        }

                        if (ver)
                        {
                            break;
                        }
                    }
                }


                while (true)
                {
                    bool ver = false;
                    for (int i = 0; i < filaA.Count; i++)
                    {
                        int marginLeft = int.MinValue;
                        int marginTop = int.MinValue;
                        int marginBottom = int.MinValue;
                        int marginRight = int.MinValue;
                        Image img = filaA[i];
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            marginLeft = (int)img.Margin.Left;
                            marginTop = (int)img.Margin.Top;
                            marginBottom = (int)img.Margin.Bottom;
                            marginRight = (int)img.Margin.Right;
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(0.5));
                        marginTop += 1;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                        }));

                        if (marginTop >= 640 && i == filaA.Count - 1)
                        {
                            filaA.RemoveAt(i);
                            ver = true;
                            break;
                        }
                        else if (marginTop >= 640)
                        {
                            filaA.RemoveAt(i);
                        }

                    }
                    if (ver)
                    {
                        break;
                    }
                }

                if (filaA.Count == 0 && filaB.Count == 0)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        btnInizia.IsEnabled = true;
                    }));

                }



            }
        }
        public void MuoviFilaB()
        {

            if (filaB.Count != 0)
            {
                int autoInFila = filaB.Count;
                while (true)
                {
                    bool ver = false;
                    for (int i = 0; i < filaB.Count; i++)
                    {
                        int marginLeft = int.MaxValue;
                        int marginTop = int.MaxValue;
                        int marginBottom = int.MaxValue;
                        int marginRight = int.MaxValue;
                        Image img = filaB[i];
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            marginLeft = (int)img.Margin.Left;
                            marginTop = (int)img.Margin.Top;
                            marginBottom = (int)img.Margin.Bottom;
                            marginRight = (int)img.Margin.Right;
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(0.5));
                        marginTop -= 1;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                        }));

                        if (marginTop == 419)
                        {
                            ver = true;
                            break;
                        }
                    }
                    if (ver)
                    {
                        break;
                    }
                }

                lock (locker)
                {
                    Queue<Image> queueImgEntrateNelPonte = new Queue<Image>();
                    while (true)
                    {
                        bool ver = false;
                        for (int i = 0; i < filaB.Count; i++)
                        {
                            int marginLeft = int.MaxValue;
                            int marginTop = int.MaxValue;
                            int marginBottom = int.MaxValue;
                            int marginRight = int.MaxValue;
                            Image img = filaB[i];
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                marginLeft = (int)img.Margin.Left;
                                marginTop = (int)img.Margin.Top;
                                marginBottom = (int)img.Margin.Bottom;
                                marginRight = (int)img.Margin.Right;
                            }));


                            Thread.Sleep(TimeSpan.FromMilliseconds(0.5));

                            if (marginTop == 410)
                            {
                                queueImgEntrateNelPonte.Enqueue(img);
                            }
                            if (queueImgEntrateNelPonte.Count < 10)
                            {
                                marginTop -= 1;
                            }

                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                            }));


                            if (marginTop == 110)
                            {
                                queueImgEntrateNelPonte.Dequeue();
                                autoInFila--;
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    lblAutoFilaB.Content = "Auto in fila per la filaB: " + autoInFila;
                                }));

                                if (queueImgEntrateNelPonte.Count == 0)
                                {
                                    ver = true;
                                    break;
                                }
                            }

                        }

                        if (ver)
                        {
                            break;
                        }
                    }
                }

                while (true)
                {
                    bool ver = false;
                    for (int i = 0; i < filaB.Count; i++)
                    {
                        int marginLeft = int.MaxValue;
                        int marginTop = int.MaxValue;
                        int marginBottom = int.MaxValue;
                        int marginRight = int.MaxValue;
                        Image img = filaB[i];
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            marginLeft = (int)img.Margin.Left;
                            marginTop = (int)img.Margin.Top;
                            marginBottom = (int)img.Margin.Bottom;
                            marginRight = (int)img.Margin.Right;
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(0.5));
                        marginTop -= 1;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                        }));

                        if (marginTop <= -70 && i == filaB.Count - 1)
                        {
                            filaB.RemoveAt(i);
                            ver = true;
                            break;
                        }
                        else if (marginTop <= -70)
                        {
                            filaB.RemoveAt(i);
                        }
                    }
                    if (ver)
                    {
                        break;
                    }
                }

                if (filaA.Count == 0 && filaB.Count == 0)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        btnInizia.IsEnabled = true;
                    }));

                }



            }
        }

        public void Start()
        {
            t1 = new Thread(new ThreadStart(MuoviFilaA));
            t2 = new Thread(new ThreadStart(MuoviFilaB));

            t1.Start();
            t2.Start();
        }

        private void btnInizia_Click(object sender, RoutedEventArgs e)
        {
            btnInizia.IsEnabled = false;
            r = new Random();
            lstImage = new List<Image>();
            filaA = new List<Image>();
            filaB = new List<Image>();
            RandomSpawnMacchine();
            Start();
        }
    }
}
