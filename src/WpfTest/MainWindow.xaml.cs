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

using Corsis.Xhtml;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string html = "<html><head><TITLE>title</TITLE></head><body>I♥NY☢<p>b<br>c: <img src=2 nonsense=x></a></body></html>";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = Html2Xhtml.RunAsFilter(stdin => stdin.Write(html)).ReadToXDocument().ToString();
        }
    }
}
