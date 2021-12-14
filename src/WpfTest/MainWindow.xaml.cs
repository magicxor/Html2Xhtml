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
using System.Xml.Linq;
using System.IO;

using Corsis.Xhtml;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var xhtml = Html2Xhtml.RunAsFilter(
                    i => i.Write(htmlBox.Text),
                    outputDocType: (DocType)Enum.Parse(typeof(DocType), s.doc.SelectedValue.ToString()),
                    e: s.e.IsChecked.Value,
                    inputEncodingName: s.inputEncoding.Text,
                    outputEncodingName: s.outputEncoding.Text,
                    lineLength: int.Parse(s.lineLength.Text),
                    tabLength: int.Parse(s.tabLength.Text),
                    preserveWhitespaceInComments: s.spaceComment.IsChecked.Value,
                    noProtectCData: !s.cdata.IsChecked.Value,
                    compactBlockElements: s.block.IsChecked.Value,
                    emptyElementTagsAlways: s.emptyAlways.IsChecked.Value,
                    compactEmptyElementTags: s.empty.IsChecked.Value,
                    dosEndOfLine: s.dos.IsChecked.Value
                    ).ReadToEnd();

                xhtmlBox.Text = xhtml;
                xmlBox.Text = xhtml.ToXDocument().ToString(SaveOptions.DisableFormatting);
            }
            catch (Exception ex)
            {
                xhtmlBox.Text = ex.Message;
            }
        }

        private void getButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(uriBox.Text);
            try
            {
                using (var r = uri.OpenDownload())
                {
                    htmlBox.Text = new StreamReader(r).ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                htmlBox.Text = ex.Message;
            }
        }
    }
}
