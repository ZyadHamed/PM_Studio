//This Class was Created Using the iText7 Nuget Package, link: https://www.nuget.org/packages/itext7/7.0.3-netstandard
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        public void GeneratePDFFile(string filePath, string fileName, PDFExportArgs args)
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

            //If the ExportAlgorithms argument was true
            if (args.ExportAlgorithms == true)
            {
                //Create a List of string that contains the FilePaths of all pmalg files
                List<string> algorithmFilePaths = FileManger.GetAllFilesByExtension(@"E:\", ".pmalg");

                //Create a List of Algorithms that will store the Algorithms inside the pmalg files
                List<Algorithm> algorithms = new List<Algorithm>();

                //Loop on all the file pathes
                foreach (string filePath2 in algorithmFilePaths)
                {
                    //Add the algorithm inside the current file into the List of algorithms
                    algorithms.Add(SaveLoadSystem.LoadData<Algorithm>(filePath2));
                }

                //Loop Inside all the Algorithms in the List
                foreach (Algorithm algorithm in algorithms)
                {
                    //Get the File Name of the Algorithm
                    string algorithmFileName = algorithm.algorithmFileName;

                    //Create a Paragraph that Contains the Name of the Algorithm as a Header for the Algorithm Steps
                    //(The remove method is placed to remove the extension of the File from the FileName)
                    Paragraph AlgorithmsTitle = new Paragraph(algorithmFileName.Remove(algorithmFileName.LastIndexOf("."))).SetFontSize(25);

                    //Create an Empty Pargraph that will store the Text of the Algorithm
                    Paragraph AlgorithmsText = new Paragraph("").SetFontSize(15);

                    //Create a Match collection for the Pattern of the Step Indicators for formatting the step indicators
                    MatchCollection matches = Regex.Matches(algorithm.algorithm, @"(\[\d*\])");

                    //Create a string array that holds the lines inside the algorithm
                    string[] Lines = algorithm.algorithm.Split('\n');

                    //Loop inside each match in the Match Collection
                    for (int i = 0; i < matches.Count; i++)
                    {
                        //Create a Text object that holds the step indicator of that line and give it Blue color
                        Text StepIndecator = new Text(matches[i].Value).SetFontColor(ColorConstants.BLUE);

                        //Create another Text object that holds the rest of the Text on the right of the step indicator
                        Text LineText = new Text(Lines[i].Substring(matches[i].Length));

                        //Add the step indicator , the LineText Text objects to the paragraph and add a new Line in the End of each Line
                        AlgorithmsText.Add(StepIndecator);
                        AlgorithmsText.Add(LineText);
                        AlgorithmsText.Add("\n");
                    }

                    //Add both Paragraphs into the document
                    document.Add(AlgorithmsTitle);
                    document.Add(AlgorithmsText);
                }
            }

            //document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            //Close the Document
            document.Close();


        }
    }
}
