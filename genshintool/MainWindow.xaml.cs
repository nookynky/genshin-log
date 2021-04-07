using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using System.Timers;
using System.Windows.Forms;

namespace genshintool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int curResin;
        int lastResin;
        DateTime dtnow = DateTime.Now;
        DateTime lastDate;
        private static System.Timers.Timer aTimer;
        public MainWindow()
        {
            InitializeComponent();
            
            datetest.Content = dtnow;

            ResinEditButton.Click += editResin_Click;

            resinProgressbar.Minimum = 0;
            resinProgressbar.Maximum = 160;

            SetTimer();

        }
        public void SetTimer()
        {
            aTimer = new System.Timers.Timer(5000);
            aTimer.Elapsed += ResinAdd;
            aTimer.Elapsed += new ElapsedEventHandler(resinDisplay);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
 
        }

        public void ResinAdd(Object source, ElapsedEventArgs e)
        {
            curResin++;
        }

        public void resinDisplay(object source, ElapsedEventArgs e)
        {
            resinDetail.Content = "You currently have " + curResin + "/160";
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            TimeSpan diff = dtnow - lastDate;
            timeSpanTest.Content = diff.Seconds;
            curResin = lastResin;//

            resinDetail.Content = "You currently have " + curResin + "/160";
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            lastDate = dtnow;
            Properties.Settings.Default.lastDate = lastDate;
            lastResin = curResin;
            Properties.Settings.Default.lastResin = lastResin;
        }

        public void editResin_Click(object sender, RoutedEventArgs e)
        {
            string InputResin = Interaction.InputBox("Input your current resin below.", "", "0", -1, -1);
            curResin = int.Parse(InputResin);
            resinProgressbar.Value = curResin;
            resinDetail.Content = "You currently have " + curResin + "/160";

            TimeSpan diff = dtnow - lastDate;
            timeSpanTest.Content = diff.Seconds;
        }

    }
}
