using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;

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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            PDFExportArgs args = new PDFExportArgs() 
            {
                ExportAlgorithms = cbxExportAlgorithms.IsChecked == true,
                ExportIdeas = cbxExportIdeas.IsChecked == true,
                ExportStoryConcepts = cbxExportStoryconcepts.IsChecked == true,
                ExportNotes = cbxExportNotes.IsChecked == true
            };

            if(saveFileDialog.ShowDialog() == true)
            {
                exporter.GeneratePDFFile(saveFileDialog.FileName + ".pdf", args);
            }
            
            
        }
    }
}
