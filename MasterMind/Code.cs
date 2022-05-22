using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MasterMind
{
    //This class generates the code the user has to guess
    class Code
    {
        private Brush[] colors = new Brush[8]
            {   Brushes.Black,
                Brushes.Purple,
                Brushes.Red,
                Brushes.Brown,
                Brushes.Blue,
                Brushes.Yellow,
                Brushes.Green,
                Brushes.Orange
            };
        private static Brush[] codeColors = new Brush[4];
        private Random r = new Random();
        public void GenerateCode()
        {
            for (int i = 0; i < codeColors.Length; i++)
            {
                codeColors[i] = colors[r.Next(0, 8)];
                Debug.WriteLine("Code: " + codeColors[i]);
            }
        }

        public static Brush[] CodeColors
        {
            get { return codeColors; }
            private set { codeColors = value; }
        }

    }
}
