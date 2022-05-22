using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MasterMind
{
    class CodeChecker
    {
        private int reds = 0;
        private int whites = 0;
        bool[] correct = new bool[4];
        bool[] semiCorrect = new bool[4];
        private Brush[] code;
        private Brush[] userColors;
        public CodeChecker()
        {

        }
        private void GetCode()
        {
            code = Code.CodeColors;
        }
        //Calls the neccesary methods to get the code and check if it's correct
        public void CheckCode(Brush[] colors)
        {
            for (int i = 0; i < correct.Length; i++)
            {
                correct[i] = false;
                semiCorrect[i] = false;
            }
            reds = 0;
            whites = 0;
            userColors = colors;
            GetCode();
            CheckForReds();
            CheckForWhites();
        }
        //Checks if there are any colors that are in the code and also placed correctly
        private void CheckForReds()
        {
            for (int i = 0; i < userColors.Length; i++)
            {
                if (userColors[i] == code[i])
                {
                    reds++;
                    correct[i] = true;
                }
            }
        }
        //Checks if there are any colors that are in the code but not in the right spot
        private void CheckForWhites()
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (correct[i] == false)
                {
                    for (int j = 0; j < code.Length; j++)
                    {
                        if (correct[j] == false)
                        {
                            if (userColors[i] == code[j])
                            {
                                semiCorrect[i] = true;
                                break;
                            }
                        }
                    }
                }
            }
            CountWhites();
        }
        private void CountWhites()
        {
            for (int i = 0; i < semiCorrect.Length; i++)
            {
                if (semiCorrect[i] == true)
                {
                    whites++;
                }
            }
        }
        public int Reds
        {
            get { return reds; }
            private set { reds = value; }
        }
        public int Whites
        {
            get { return whites; }
            private set { whites = value; }
        }
    }
}
