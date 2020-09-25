//This Class was Created Using the iText7 Nuget Package, link: https://www.nuget.org/packages/itext7/7.0.3-netstandard
using System;
using System.Collections.Generic;
using System.Text;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PM_Studio
{
   public class PDF_Exporter
    {
        /// <summary>
        /// Generates a PDF file Containing the Data of the Algorithms in the Project
        /// Code will be updated soon to include other types of files and include formatting for text
        /// </summary>
        /// <param name="filePath">The Location to save the file in</param>
        /// <param name="fileName">the Name of the Pdf file assosaied with an extesion of .pdf</param>
        /// <param name="algorithms">The Algorithms to put there data in the PDF file</param>
        public void GeneratePDFFile(string filePath, string fileName, List<Algorithm> algorithms)
        {
            //Create a PdfWriter for creating and writing data into a PDF file
            PdfWriter writer = new PdfWriter(filePath + fileName);

            //Create a PdfDocument to store all the Pages and Data in the PDF
            PdfDocument pdf = new PdfDocument(writer);

            //Create a Document to store the Data of the Files in the Project
            Document document = new Document(pdf);

            //Create a Header for the PDF document that contains the fileName of the PDF File
            Paragraph header = new Paragraph(fileName).SetTextAlignment(TextAlignment.CENTER).SetFontSize(30);

            //Add it to the document
            document.Add(header);

            //Loop Inside all the Algorithms in the List
            foreach (Algorithm algorithm in algorithms)
            {
                //Get the File Name of the Algorithm
                string algorithmFileName = algorithm.algorithmFileName;

                //Create a Paragraph that Contains the Name of the Algorithm as a Header for the Algorithm Steps
                //(The remove method is placed to remove the extension of the File from the FileName)
                Paragraph AlgorithmsTitle = new Paragraph(algorithmFileName.Remove(algorithmFileName.LastIndexOf("."))).SetFontSize(25);

                //Create another Pargraph that stores the Text of the Algorithm
                Paragraph AlgorithmsText = new Paragraph(algorithm.algorithm).SetFontSize(15);

                //Add both Paragraphs into the document
                document.Add(AlgorithmsTitle);
                document.Add(AlgorithmsText);
            }
            //Close the Document
            document.Close();

        }
    }
}
