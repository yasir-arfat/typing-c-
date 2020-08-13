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
using System.Windows.Threading;
using System.Diagnostics;
using System.Drawing;
using typing;
namespace typing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       int errors = 0;
        int tc = 0;
        int w =0 ;
            
       double time3 = 0;
      
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        public MainWindow()
        {
            InitializeComponent();

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            
        }
        

        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                stopwatch.Text = currentTime;
                int time1 = ts.Minutes * 60;
               double time2 = time1 + ts.Seconds;
                 time3 = time2 / 60;
                 if (t5.Text ==Convert.ToString( ts.Minutes) ) { sto(); }
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            dt.Start(); result.Focus();

        }
//stop
        public  void stop_Click(object sender, RoutedEventArgs e)
        {
            sto();
            initTest();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            stopwatch.Text = "00:00:00";
            tc = 0;
            w = 0;
            time3 = 0;
            t1.Text = "";
            t2.Text = "";
           
             errors = 0;
             error.Text ="";
            result.Text = "";
        }

      
       
       
        public void sto() 
        {
            if (sw.IsRunning){    sw.Stop();}
            tc = result.Text.Length;
            t1.Text = "" + tc;
            t2.Text = "" + w;
            t3.Text += "\nCPM           " + Convert.ToString(tc / time3); t3.ScrollToEnd();
            error.Text =""+errors;
            his.Text += "\nWord/Minut   " + Convert.ToString(w / time3) ; his.ScrollToEnd();
        }


        void initTest()
        {


            var split = result.Text.Split(' ');
             errors = 0;
            foreach (var s in split)
            {
                var tempTb = new TextBox();
                tempTb.SpellCheck.IsEnabled = true;  // Added this line
                tempTb.Text = s;

                SpellingError e = tempTb.GetSpellingError(0); // no longer always null
                var a = tempTb.GetSpellingErrorLength(0);
                var b = tempTb.GetSpellingError(0);
                var c = tempTb.GetSpellingErrorStart(0);

                //if (tempTb.GetSpellingErrorLength(0) >= 0)  //doesn't appear to be correct 
                if (e != null)
                {
                    errors++; 
                    error.Text = Convert.ToString(errors);
                }
            }
        }

       

        private void result_KeyUp(object sender, KeyEventArgs e)
        { if (e.Key == Key.Space) { w++; }
        else if (e.Key == Key.Enter) { w++;  }

        }

       

    }
}
 