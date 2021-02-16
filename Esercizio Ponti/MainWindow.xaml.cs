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
        Thread t1;
        Thread t2;
        Random r;
        List<Image> lstImage;
        List<Image> filaA;
        List<Image> filaB;
        object locker = new object();
        public MainWindow()
        {
            InitializeComponent();
            r = new Random();
            lstImage = new List<Image>();
            filaA = new List<Image>();
            filaB = new List<Image>();
            RandomSpawnMacchine();
            t1 = new Thread(new ThreadStart(MuoviFilaA));
            t2 = new Thread(new ThreadStart(MuoviFilaB));

            t1.Start();
            t2.Start();
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

        public void MuoviFilaA()
        {
            if (filaA.Count != 0)
            {
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
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        marginTop += 30;
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


                            Thread.Sleep(TimeSpan.FromMilliseconds(100));

                            if (marginTop == 100)
                            {
                                queueImgEntrateNelPonte.Enqueue(img);
                            }
                            marginTop += 30;

                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                            }));


                            if (marginTop == 400)
                            {
                                queueImgEntrateNelPonte.Dequeue();
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
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        marginTop += 30;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                        }));

                        if (marginTop == 640 && i == filaA.Count - 1)
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


            }
        }
        public void MuoviFilaB()
        {

            if (filaB.Count != 0)
            {
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
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        marginTop -= 30;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                        }));

                        if (marginTop == 410)
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


                            Thread.Sleep(TimeSpan.FromMilliseconds(100));

                            if (marginTop == 410)
                            {
                                queueImgEntrateNelPonte.Enqueue(img);
                            }
                            marginTop -= 30;

                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                            }));


                            if (marginTop == 110)
                            {
                                queueImgEntrateNelPonte.Dequeue();
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
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        marginTop -= 30;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            img.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                        }));

                        if (marginTop == -70 && i == filaB.Count - 1)
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

            }
        }
    }
}
