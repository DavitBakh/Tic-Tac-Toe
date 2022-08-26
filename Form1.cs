namespace Tic_Tac_Toe.UI
{
    public partial class Form1 : Form
    {
        char userChar = 'X';
        char computerChar = 'O';
        char emptyChar = '_';

        bool User = false;
        bool AI = true;


        char[,] board;
        bool canMove = true;

        Button[,] buttons;

        public Form1()
        {
            InitializeComponent();

            board = new char[3, 3]
            {
                { emptyChar, emptyChar, emptyChar },
                { emptyChar, emptyChar, emptyChar },
                {emptyChar,emptyChar, emptyChar }
            };

            buttons = new Button[3, 3]
             {
                { btn1, btn2, btn3 },
                { btn4, btn5, btn6 },
                { btn7, btn8, btn9 }
             };

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ISComputerFirst(false);
            UpdateField();
        }
        public void ISComputerFirst(bool b)
        {
            if (b)
            {
                canMove = false;
                ComputerMove(board);
            }

        }


        public void UpdateField()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == emptyChar)
                        continue;
                    if (board[i, j] == 'X')
                        buttons[i, j].Text = "X";
                    else
                        buttons[i, j].Text = "O";
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button? button = sender as Button;

            if (button == null)
                return;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (canMove && button == buttons[i, j])
                    {
                        if (board[i, j] != emptyChar)
                            return;

                        board[i, j] = userChar;
                        UpdateField();
                        canMove = false;

                        if (isWin(userChar, board))
                        {
                            MessageBox.Show("Win Player " + userChar);
                            Close();
                        }

                        ComputerMove(board);
                        if (isWin(computerChar, board))
                        {
                            MessageBox.Show("Win Player " + computerChar);
                            Close();
                        }
                        if (isDraw(board))
                        {
                            MessageBox.Show("Draw");
                            Close();
                        }

                        return;
                    }
                }
            }
        }

        public void ComputerMove(char[,] board)
        {
            int bestScore = int.MinValue;
            char[,] copBoard = (char[,])board.Clone();

            int y = -1;
            int x = -1;
            for (int i = 0; i < copBoard.GetLength(0); i++)
            {
                for (int j = 0; j < copBoard.GetLength(1); j++)
                {
                    if (copBoard[i, j] == emptyChar)
                    {
                        copBoard[i, j] = computerChar;
                        int score = MiniMax(copBoard, 0, User);
                        copBoard[i, j] = emptyChar;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            y = i;
                            x = j;

                        }

                    }
                }
            }
            if (y != -1 && x != -1)
            {
                board[y, x] = computerChar;
                canMove = true;
                UpdateField();
            }

        }



        public int MiniMax(char[,] board, int depth, bool minMax)
        { 
            if (isWin(userChar, board))
                return -100 - depth;
            if (isWin(computerChar, board))
                return 100 - depth;
            if (isDraw(board))
                return 0 - depth ;


            if (minMax)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == emptyChar)
                        {
                            board[i, j] = computerChar;
                            int score = MiniMax(board, depth + 1, User);
                            board[i, j] = emptyChar;

                            if (score == (100 - (depth + 1)))
                                return score;

                            bestScore = Math.Max(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == emptyChar)
                        {
                            board[i, j] = userChar;
                            int score = MiniMax(board, depth + 1, AI);

                            board[i, j] = emptyChar;

                            if (score == (-100 - (depth + 1)))
                                return score;

                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }

        }


        public bool isWin(char ch, char[,] board)
        {
            for (int y = 0; y < board.GetLength(0); y++)
            {
                bool CheckX = true;
                bool CheckY = true;
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    if (board[y, x] != ch)
                        CheckY = false;

                    if (board[x, y] != ch)
                        CheckX = false;
                }
                if (CheckX || CheckY)
                    return true;
            }

            if (board[0, 0] == ch && board[1, 1] == ch && board[2, 2] == ch)
                return true;

            if (board[0, 2] == ch && board[1, 1] == ch && board[2, 0] == ch)
                return true;

            return false;
        }

        public bool isDraw(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == emptyChar)
                        return false;
                }
            }
            return true;
        }
    }
}