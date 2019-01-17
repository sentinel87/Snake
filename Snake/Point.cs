using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public abstract class Point     //Single point on PictureBox
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Point(int pX, int pY)
        {
            x = pX; y = pY;
        }
        public abstract void PointParametersChange(Graphics D, Pen Brush, int pX, int pY);
    }
    class Piece: Point  //Single square
    {
        public override void PointParametersChange(Graphics D, Pen Brush, int pX, int pY)    //Draw single Square
        {
            X += pX;
            Y += pY;
            D.DrawRectangle(Brush, X, Y, 5, 5);
        }
        public Piece(int pX, int pY):base(pX,pY){}
    }

    class Food: Piece   //Food Square
    {
        public void GenerateFood()  //Appears randomly on the PictureBox 
        {
            Random Rnd = new Random();
            X = 3+((Rnd.Next(1, 39))*10);
            Y = 3+((Rnd.Next(1,44))*10);
        }
        public Food(int pX, int pY):base(pX,pY){ }
    }

    class SnakeBody //Collection of Snake Squares
    {
        public List<Piece>Body = new List<Piece>();
        public int Lenght=0;

        Pen blackPen = new Pen(Color.Black, 4);
        Pen greenPen = new Pen(Color.GreenYellow, 4);
        Pen redPen = new Pen(Color.Red, 4);

        public void AddSegment(Piece Object)    //Additional Square
        {
            Lenght++;
            Body.Add(Object);
        }

        public void Move(Graphics Target, int pX, int pY)   //Moving squares by changing positions of the elements
        {
            Target.DrawRectangle(greenPen, Body[0].X, Body[0].Y, 5, 5);
            for(int i=0;i<Lenght;i++)
            {
                if (i < (Lenght-1)) { Body[i].X= Body[i + 1].X; Body[i].Y = Body[i + 1].Y; }
                else
                {
                    Body[i].X += pX; Body[i].Y += pY;
                }
            }
            Target.DrawRectangle(blackPen, Body[Lenght-1].X, Body[Lenght-1].Y, 5, 5);
        }

        public Boolean Eat(ref Food Object, Graphics Target,int pX,int pY)  //If First element of the Snake meets food element
        {
            if (Body[Lenght-1].X == Object.X && Body[Lenght-1].Y == Object.Y)
            {
                Piece Another = new Piece(Body[Lenght - 1].X+pX, Body[Lenght - 1].Y+pY);
                AddSegment(Another);
                Another.PointParametersChange(Target, blackPen, 0, 0);
                Object.GenerateFood();
                Object.PointParametersChange(Target, redPen, 0, 0);
                return true;
            }
            return false;
        }

        public Boolean Collision()  //If Snake first element meets tail
        {
            for (int i = 0; i < Lenght-1; i++)
            {
                if(Body[i].X==Body[Lenght-1].X && Body[i].Y==Body[Lenght-1].Y)
                {
                    return true;
                }
            }
            return false;
        }

        public void ClearBody(Graphics Target)  //Erase all Snake elements 
        {
            for (int i = 0; i < Lenght; i++)
            {
                Body[i].PointParametersChange(Target, greenPen, 0, 0);
            }
            Body.Clear();
            Lenght = 0;
        }
    }
}
