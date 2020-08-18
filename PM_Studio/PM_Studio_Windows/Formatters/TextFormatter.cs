using System;
using System.Collections.Generic;
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
                //Restore the old values of the selection and color of the text box
                rtxtAlgorithm.SelectionStart = originalIndex;
                rtxtAlgorithm.SelectionLength = originalLength;
                rtxtAlgorithm.SelectionColor = originalColor;

                //get back the focus to the textbox
                rtxtAlgorithm.Focus();
            }
        }
    }
}
