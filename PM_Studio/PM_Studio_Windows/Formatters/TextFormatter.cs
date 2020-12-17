using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PM_Studio
{
    class TextFormatter
    {
        public void FormatAlgorithmTextBox(System.Windows.Forms.RichTextBox rtxtAlgorithm, params string[] Patterns)
        {
            //Get the values of the current selection and current color at the textbox
            int originalIndex = rtxtAlgorithm.SelectionStart;
            int originalLength = rtxtAlgorithm.SelectionLength;
            System.Drawing.Color originalColor = System.Drawing.Color.FromArgb(210, 210, 210);

            //Get the lines of the richtextbox without the step number indecators
            string Lines = Regex.Replace(rtxtAlgorithm.Text, @"(\[\d*\])", "");
            //Set the richtextbox text to those lines (to remove any steps indicators)
            rtxtAlgorithm.Text = Lines;

           
            for (int i = 0; i < rtxtAlgorithm.Lines.Length; i++)
            {
               
                rtxtAlgorithm.Text = rtxtAlgorithm.Text.Insert(rtxtAlgorithm.GetFirstCharIndexFromLine(i), $"[{i + 1}]");

            }

          
            //Get all the patterns passed in by the method
            foreach (string pattern in Patterns)
            {
                //Foreach pattern of those, create a list of regex matches based on that pattern
                MatchCollection matches = Regex.Matches(rtxtAlgorithm.Text, pattern);
                //Set the selection to all the text box, and give it the original color
                rtxtAlgorithm.SelectionStart = 0;
                rtxtAlgorithm.SelectionLength = rtxtAlgorithm.Text.Length;
                rtxtAlgorithm.SelectionColor = originalColor;

                //Loop inside each match inside the matches list
                foreach (Match m in matches)
                {
                    //Set the selection start to that match index
                    rtxtAlgorithm.SelectionStart = m.Index;
                    //Set the selection length to that match length
                    rtxtAlgorithm.SelectionLength = m.Length;
                    //give the selection(the word to be formatted) a blue color
                    rtxtAlgorithm.SelectionColor = System.Drawing.Color.Blue;
                }

                //Get The Index of the CurrentLine in the RichTextBox
                int CurrentLine = rtxtAlgorithm.GetLineFromCharIndex(originalIndex);

                //If the Current Line contains only a step indicator without any text, then it's a Blank Line
                //Then Set the selection start to the end of that Line

                if (CurrentLine > 0 && Regex.IsMatch(rtxtAlgorithm.Lines[CurrentLine], @"(\[\d*\])")) 
                {

                    //Set the Selection Start to the End of the blank Line
                    rtxtAlgorithm.SelectionStart = rtxtAlgorithm.GetFirstCharIndexFromLine(CurrentLine) + rtxtAlgorithm.Lines[CurrentLine].Length;

                    //Restore the old values of the selection length and color of the text box
                    rtxtAlgorithm.SelectionLength = originalLength;
                    rtxtAlgorithm.SelectionColor = originalColor;
                }

                //If the Line did not contain a step indicator only, then set the selection start to the original one
                else
                {
                    //Set the Selection Start to the original Selection start
                    rtxtAlgorithm.SelectionStart = originalIndex;

                    //Restore the old values of the selection length and color of the text box
                    rtxtAlgorithm.SelectionLength = originalLength;
                    rtxtAlgorithm.SelectionColor = originalColor;

                }

                //get back the focus to the textbox
                rtxtAlgorithm.Focus();
            }
        }
    }
}
