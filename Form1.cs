using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        public TicTacToe()
        {
            InitializeComponent();
        }
        public Shape shp;
        public Game game;
        
        public class Game : TicTacToe
        {
            
            public PlaceHolder[,] grids = new PlaceHolder[3,3];
            //private Rectangle[,] coords = new Rectangle[3,3];
            public const int X = 0;
            public const int O = 1;
            public const int B = 2;
            public int pMoves = 0;
            public int pTurn = O;
            public int xWins = 0;
            public int oWins = 0;

            //Checking for a tie
            bool checkForTie(int turns)
            {
                bool tie = false;
                for(int row = 0; row < 3; row++)
                {
                    for(int col = 0; col < 3; col++)
                    {
                        if(turns == 9)
                        {
                            return true;
                        }
                    }
                }
                return tie;
            }
            public int getPTurn()
            {
                return pTurn;
            }
            public int getXWins()
            {
                return xWins;
            }
            public int getOWins()
            {
                return oWins;
            }
            public void Board()
            {
                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        //coords[i, j] = new Rectangle(i * 167, j * 167, 167, 167);
                        grids[i, j] = new PlaceHolder();
                        grids[i, j].setVal(B);
                        grids[i, j].setLoc(new Point(i, j));
                    }
                }
            }
            //Keeping track of player mouse clicks
            public void playerClick(Point p)
            {
                int x = 0;
                int y = 0;

                if(p.X < 167)
                {
                    x = 0;
                }
                else if(p.X > 167 && p.X < 334)
                {
                    x = 1;
                }
                else if(p.X > 334)
                {
                    x = 2;
                }


                if (p.Y < 167)
                {
                    y = 0;
                }
                else if (p.Y > 167 && p.Y < 334)
                {
                    y = 1;
                }
                else if (p.Y > 334)
                {
                    y = 2;
                }
                pMoves++;

                if (pMoves % 2 == 0)
                {
                    gameLbl.Text = "X's Turn!";
                    Shape.DrawX(new Point(x, y));
                    grids[x, y].setVal(X);
                    if (Win())
                    {
                        MessageBox.Show("X's have won!"); 
                        resetGame();
                        Shape.Draw();
                        xWins++;
                        pMoves = 0;
                    }
                   else if (checkForTie(pMoves))
                    {
                        MessageBox.Show("It was a tie!");
                        resetGame();
                        Shape.Draw();
                        pMoves = 0;
                    }
                    pTurn = O;
                }
                else
                {
                    gameLbl.Text = "O's Turn!";
                    Shape.DrawO(new Point(x, y));
                    grids[x, y].setVal(O);
                    if (Win())
                    {
                        MessageBox.Show("O's have won!");
                        resetGame();
                        Shape.Draw();
                        oWins++;
                        pMoves = 0;
                    }
                    else if (checkForTie(pMoves))
                    {
                        MessageBox.Show("It was a tie!");
                        resetGame();
                        Shape.Draw();
                        pMoves = 0;
                    }
                    pTurn = X;
                }
            
            }
            //Checking for winner
            public bool Win()
            {
                bool Won = false;

                for(int x = 0; x < 3; x++)
                {
                    if(grids[x, 0].getVal() == X && grids[x, 1].getVal() == X && grids[x, 2].getVal() == X)
                    {
                        return true;
                    }
                    if (grids[x, 0].getVal() == O && grids[x, 1].getVal() == O && grids[x, 2].getVal() == O)
                    {
                        return true;
                    }
                    switch (x)
                    {
                        case 0:
                            if (grids[x, 0].getVal() == X && grids[x + 1, 1].getVal() == X && grids[x + 2, 2].getVal() == X)
                            {
                                return true;
                            }
                            if (grids[x, 0].getVal() == O && grids[x + 1, 1].getVal() == O && grids[x + 2, 2].getVal() == O)
                            {
                                return true;
                            }
                                break;
                        case 2:
                            if (grids[x, 0].getVal() == X && grids[x - 1, 1].getVal() == X && grids[x - 2, 2].getVal() == X)
                            {
                                return true;
                            }
                            if (grids[x, 0].getVal() == O && grids[x - 1, 1].getVal() == O && grids[x - 2, 2].getVal() == O)
                            {
                                return true;
                            }
                                break;
                    }
                }
                for(int y = 0; y < 3; y++)
                {
                    if (grids[0, y].getVal() == X && grids[1, y].getVal() == X && grids[2, y].getVal() == X)
                    {
                        return true;
                    }
                    if (grids[0, y].getVal() == O && grids[1, y].getVal() == O && grids[2, y].getVal() == O)
                    {
                        return true;
                    }
                }
                return Won;
            }
            //Resetting the game board
            public void resetGame()
            {
                grids = new PlaceHolder[3, 3];
                Board();
            }
        }
        

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //Shape.graphics = CreateGraphics();
        }
      public class Shape
        {
            private static Graphics graphics;
            public Shape(Graphics g)
            {
                graphics = g;
                Draw();
            }
            //Drawing the game display
            public static void Draw()
            {
                //Drawing the playing field
                Brush backG = new SolidBrush(Color.White);
                Pen line = new Pen(Color.Black, 5);
                graphics.FillRectangle(backG, new Rectangle(0, 0, 500, 600));
                graphics.DrawLine(line, new Point(167, 0), new Point(167, 600));
                graphics.DrawLine(line, new Point(334, 0), new Point(334, 600));
                graphics.DrawLine(line, new Point(0, 167), new Point(500, 167));
                graphics.DrawLine(line, new Point(0, 334), new Point(500, 334));
            }
            
            public static void DrawX(Point p)
            {
                //Creating the X marker
                Pen X = new Pen(Color.Red, 5);
                int XP = p.X * 167;
                int YP = p.Y * 167;
                //if(Game.grids[,] )
                graphics.DrawLine(X, XP+10, YP+10, XP + 140, YP + 140);
                graphics.DrawLine(X, XP + 140, YP+10, XP+10, YP + 140);
            }
            public static void DrawO(Point p)
            {
                //Creating the O marker
                Pen O = new Pen(Color.Blue, 5);
                int XP = p.X * 167;
                int YP = p.Y * 167;

                graphics.DrawEllipse(O, XP+8, YP+8, 140, 140);
            }
        }

        public class PlaceHolder
        {
            private Point loc;
            private int val = Game.B;
            public void setLoc(Point point)
            {
                loc = point;
            }
            public Point getLoc()
            {
                return loc;
            }
            public void setVal(int x)
            {
                val = x;
            }
            public int getVal()
            {
                return val;
            }
        }
        

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        { 
   
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics pass = panel1.CreateGraphics();
            shp = new Shape(pass);
            game = new Game();
            game.Board();

            changeLabel();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Point mouse = Cursor.Position;
            mouse = panel1.PointToClient(mouse);
            game.playerClick(mouse);
            changeLabel();
        }
        //closing game menu strip button
        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //reset game menu strip button
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.resetGame();
            Shape.Draw();
        }
        
        //Displaying player's turn and amount of wins per player
        public void changeLabel()
        {
            string lText = "";
            if(game.getPTurn() == Game.X)
            {
                lText = "X's Turn!\n";
            }
            else
            {
                lText = "O's Turn!\n";
            }
            lText += "X has won " + game.getXWins() + " times. \nO has won " + game.getOWins() + " times.";
            gameLbl.Text = lText;
        }
    }
    
}
