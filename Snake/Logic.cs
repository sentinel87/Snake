using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Logic     //All game logic
    {
        private Graphics _Target;
        private int _Score;
        private int _Speed;
        private int _Level=1;
        private int Direction = 3;
        private SnakeBody Body = new SnakeBody();
        private Food Food = new Food(3, 3);
        public Boolean GameOver = false;

        Pen blackPen = new Pen(Color.Black, 4);
        Pen greenPen = new Pen(Color.GreenYellow, 4);
        Pen redPen = new Pen(Color.Red, 4);

        public Graphics Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

        public int Score
        {
            get { return _Score; }
            set { _Score = value; }
        }

        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        public int Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        public void NewGame()   //Starting new game
        {
            GameOver = false;
            Score = 0;
            _Speed = 0;
            _Level = 1;
            Piece First = new Piece(13, 13);
            Piece Last = new Piece(23, 13);
            Body.AddSegment(First);
            Body.AddSegment(Last);
            Food.GenerateFood();
            Food.PointParametersChange(_Target, redPen, 0, 0);
        }

        public void GameEvent(int Width,int Height) //Event for one Timer Tick
        {
            int PositionX = 0;
            int PositionY = 0;

            bool Collision = false;
            if (Direction == 1)
            {
                PositionX = 10;
            }
            else if (Direction == 3)
            {
                PositionY = 10;
            }
            else if (Direction == 2)
            {
                PositionX = -10;
            }
            else if (Direction == 4)
            {
                PositionY = -10;
            }

            Body.Move(_Target, PositionX, PositionY);   //Determines where draw next square
            Collision = Body.Collision();   //Checks Collision terms
            if (Body.Eat(ref Food, Target, PositionX, PositionY) == true) { _Score += 100; IncreaseSpeed(); };  //If Snake will meet Food square
            GameEnd(Width, Height, Collision);  //Check Game Over conditions
        }

        public void IncreaseSpeed()
        {
            if (_Speed!=100)    //max Speed (max Level is 10)
            {
                if(Score>=_Level*1500)  //Increase Speed and Level Values
                {
                    _Level++;
                    _Speed += 10;
                }
            }
        }

        public void GameControls(int Command)   //Setting Direction value
        {
            if(Command==3 && Direction != 4) { Direction = 3; }
            else if (Command == 1 && Direction != 2) { Direction = 1; }
            else if (Command == 4 && Direction != 3) { Direction = 4; }
            else if (Command == 2 && Direction != 1) { Direction = 2; }
        }

        private void GameEnd(int Width,int Height,bool Collision)   //Cleans game logic and sets starting parameters
        {
            if (Body.Body[Body.Lenght - 1].X >= Width || Body.Body[Body.Lenght - 1].Y >= Height || Body.Body[Body.Lenght - 1].X <= 0 || Body.Body[Body.Lenght - 1].Y <= 0 || Collision == true)
            {
                Body.ClearBody(Target);
                Direction = 3;
                _Speed = 0;
                Food.PointParametersChange(Target, greenPen, 0, 0);
                GameOver = true;
            }
        }
    }
}
