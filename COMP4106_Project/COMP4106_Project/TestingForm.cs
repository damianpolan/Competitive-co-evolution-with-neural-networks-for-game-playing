using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using COMP4106_Project.Game;
using System.Threading;

namespace COMP4106_Project
{
    public partial class TestingForm : Form
    {
        Board board = new Board();

        TButton[,] buttons;



        class TButton : Button
        {
            public int x;
            public int y;

            public TButton(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        BoardLocation selectedLocation = null;



        private Direction getFromPos(int xDif, int yDif)
        {
            if (xDif > 0 && yDif == 0)
            {
                return Direction.Right;
            }
            else if (xDif < 0 && yDif == 0)
            {
                return Direction.Left;
            }
            else if (xDif == 0 && yDif < 0)
            {
                return Direction.Up;
            }
            else if (xDif == 0 && yDif > 0)
            {
                return Direction.Down;
            }

            return Direction.None;
        }


        public void buttonClick(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (selectedLocation != null && (selectedLocation.type.Equals("pawn") || selectedLocation.type.Equals("king")))
                {
                    BoardLocation clickedP = board.pieces[((TButton)sender).x, ((TButton)sender).y];
                    if (clickedP.type.Equals("none"))
                    {
                        Piece pp = (Piece)selectedLocation;
                        board.MakeMove(new Move[] { new Move(pp.id, MoveType.Move, getFromPos(clickedP.x - pp.x, clickedP.y - pp.y)) });
                    }
                    else if (clickedP.type.Equals("pawn") || clickedP.type.Equals("king"))
                    {
                        Piece clickedPP = (Piece)clickedP;
                        Piece pp = (Piece)selectedLocation;
                        board.MakeMove(new Move[] { new Move(pp.id, MoveType.Attack, getFromPos(clickedP.x - pp.x, clickedP.y - pp.y)) });
                    }
                }
                else
                {
                    selectedLocation = board.pieces[((TButton)sender).x, ((TButton)sender).y];
                    lblInfo1.Text = "SELECTED: " + selectedLocation.type + " [ " + selectedLocation.x + ", " + selectedLocation.y + " ] ";
                }
            }


            displayBoard();

        }

        public TestingForm()
        {
            InitializeComponent();
            //txtGame.Font = new Font(FontFamily.GenericMonospace, txtGame.Font.Size); ;

            //Board b = new Board();


            //b.MakeMove(0, new Move[] { 
            //    new Move(2, MoveType.Move, Direction.Right) 
            //});


            //txtGame.Text = b.ToString();



            pnlGame.RowCount = board.pieces.GetLength(1);
            pnlGame.ColumnCount = board.pieces.GetLength(0);
            pnlGame.SuspendLayout();
            buttons = new TButton[board.pieces.GetLength(0), board.pieces.GetLength(1)];
            for (int y = 0; y < 30; y++)
            {
                for (int x = 0; x < 30; x++)
                {
                    buttons[x, y] = new TButton(x, y) { Width = 15, Height = 15 };
                    buttons[x, y].Margin = new System.Windows.Forms.Padding(0);
                    buttons[x, y].FlatStyle = FlatStyle.Popup;
                    buttons[x, y].Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8);
                    buttons[x, y].MouseUp += new MouseEventHandler(buttonClick);

                    pnlGame.Controls.Add(buttons[x, y], x, y);
                }
            }
            pnlGame.ResumeLayout();

            displayBoard();

        }


        private void displayBoard()
        {
            for (int y = 0; y < board.pieces.GetLength(1); y++)
            {
                for (int x = 0; x < board.pieces.GetLength(0); x++)
                {
                    string s = board.stringOf(board.pieces[x, y]);

                    if (s.Equals("#"))
                        buttons[x, y].BackColor = Color.Black;
                    else if (s.Equals("-"))
                        buttons[x, y].BackColor = Color.White;
                    else if (s.Equals("x"))
                        buttons[x, y].BackColor = Color.Red;
                    else if (s.Equals("o"))
                        buttons[x, y].BackColor = Color.Blue;
                    else if (s.Equals("X"))
                        buttons[x, y].BackColor = Color.DarkRed;
                    else if (s.Equals("O"))
                        buttons[x, y].BackColor = Color.DarkBlue;
                    else
                        buttons[x, y].BackColor = Color.Gray;
                }

            }
        }



        private Direction selectedDir()
        {
            if (radUp.Checked)
                return Direction.Up;
            if (radDown.Checked)
                return Direction.Down;
            if (radLeft.Checked)
                return Direction.Left;
            if (radRight.Checked)
                return Direction.Right;

            return Direction.None;
        }


        List<Move> allMoves = new List<Move>();

        private void btnAttack_Click(object sender, EventArgs e)
        {
            //b.MakeMove(0, new Move[] { 
            //    new Move(2, MoveType.Move, Direction.Right) 
            //});

            if (selectedLocation.type.Equals("pawn") || selectedLocation.type.Equals("king"))
            {
                if (!alreadyMoved(((Piece)selectedLocation).id))
                {
                    allMoves.Add(new Move(((Piece)selectedLocation).id, MoveType.Attack, selectedDir()));
                    lstMoveQueue.Items.Add("ID=" + ((Piece)selectedLocation).id + " Attack " + selectedDir().ToString());
                }
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (selectedLocation.type.Equals("pawn") || selectedLocation.type.Equals("king"))
            {
                if (!alreadyMoved(((Piece)selectedLocation).id))
                {
                    allMoves.Add(new Move(((Piece)selectedLocation).id, MoveType.Move, selectedDir()));
                    lstMoveQueue.Items.Add("ID=" + ((Piece)selectedLocation).id + " Move " + selectedDir().ToString());
                }
            }
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            if (selectedLocation.type.Equals("pawn") || selectedLocation.type.Equals("king"))
            {
                if (!alreadyMoved(((Piece)selectedLocation).id))
                {
                    allMoves.Add(new Move(((Piece)selectedLocation).id, MoveType.Defend, selectedDir()));
                    lstMoveQueue.Items.Add("ID=" + ((Piece)selectedLocation).id + " Defend " + selectedDir().ToString());
                }
            }
        }

        private bool alreadyMoved(int pieceId)
        {
            for (int i = 0; i < allMoves.Count; i++)
            {
                if (allMoves[i].pieceId.Equals(pieceId))
                    return true;
            }
            return false;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            board.MakeMove(allMoves.ToArray());
            displayBoard();
            lstMoveQueue.Items.Clear();
            allMoves.Clear();
        }

    }
}
