using System;
using System.Collections.Generic;
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

namespace PM_Studio
{
    /// <summary>
    /// Interaction logic for PublishManger.xaml
    /// </summary>
    public partial class PublishManger : Page
    {
        public PublishManger()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            PDF_Exporter exporter = new PDF_Exporter();
            
            exporter.GeneratePDFFile(@"E:\", "Calculator2.pdf", new PDFExportArgs() { ExportAlgorithms = true });
        }
    }
}
