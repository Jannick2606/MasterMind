using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace MasterMind
{
    class GameManager
    {
        Ball ball = new Ball();
        Code code = new Code();
        CodeChecker checker = new CodeChecker();
        public GameManager()
        {

        }
        public Brush GetColor()
        {
            return ball.Color;
        }
        public void SetColor(Brush color)
        {
            ball.Color = color;
        }
        public void GenerateCode()
        {
            code.GenerateCode();
        }
        public void CheckCode(Brush[] colors)
        {
            checker.CheckCode(colors);
        }
        public Brush[] GetCode()
        {
            return Code.CodeColors;
        }
        public int GetReds()
        {
            return checker.Reds;
        }
        public int GetWhites()
        {
            return checker.Whites;
        }
    }
}
