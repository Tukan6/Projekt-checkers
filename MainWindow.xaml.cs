using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using Microsoft.Windows.Themes;
using System.Security.Policy;

namespace Dáma_projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    //vygeneruju si zakladni hraci desku, hrace, a mista kde nic neni
    //pri kliku na zeton se objevi kam muze jit, to se vypocita pomoci arraye, tam kde to bude moct jit, tam bude visible pruhledny krouzek

    public partial class MainWindow : Window
    {
        enum BoardType { None, Player1, Player2, CanGo };
        private int _rows = 8;
        private int _cols = 8;
        private BoardType[,] BoardArray = new BoardType[8, 8];
        private bool p1Turn = true;
        private bool isClicked = false;
        private bool canTakeLeft = false;
        private bool canTakeRight = false;
        bool cantake1 = false;
        bool cantake2 = false;
        bool cantake3 = false;
        bool cantake4 = false;
        bool pbehindp = false;

        private int players1left = 12;
        private int players2left = 12;
        public MainWindow()
        {
            InitializeComponent();

            SetupBoard();
        }

        private void SetupBoard()
        {
            Board.RowDefinitions.Clear();
            Board.ColumnDefinitions.Clear();
            Board.Children.Clear();



            bool odd = true;


            for (int i = 0; i < _rows; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _cols; i++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //for (int i = 0; i < 64; i++)
            //{
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (odd)
                    {
                        if (y % 2 == 1)
                        {
                            Player1 player1 = new Player1();
                            IsHitTestVisible = true;
                            //if(p1Turn)
                            player1.MouseLeftButtonDown += Player1_Click;


                            Grid.SetColumn(player1, y);
                            Grid.SetRow(player1, x);
                            Board.Children.Add(player1);
                            BoardArray[x, y] = BoardType.Player1;
                        }
                        else
                            BoardArray[x, y] = BoardType.None;
                    }
                    if (!odd)
                    {
                        if (y % 2 == 0)
                        {
                            Player1 player1 = new Player1();
                            IsHitTestVisible = true;
                            //if(p1Turn)
                            player1.MouseLeftButtonDown += Player1_Click;

                            Grid.SetColumn(player1, y);
                            Grid.SetRow(player1, x);
                            Board.Children.Add(player1);
                            BoardArray[x, y] = BoardType.Player1;
                        }
                        else
                            BoardArray[x, y] = BoardType.None;

                    }
                }
                odd = !odd;

            }
            odd = !odd;
            for (int z = 5; z < 8; z++)
            {
                for (int p = 0; p < 8; p++)
                {
                    if (odd)
                    {
                        if (p % 2 == 0)
                        {
                            Player2 player2 = new Player2();
                            IsHitTestVisible = true;
                            //if(!p1Turn)
                            player2.MouseLeftButtonDown += Player2_Click;

                            Grid.SetColumn(player2, p);
                            Grid.SetRow(player2, z);
                            Board.Children.Add(player2);
                            BoardArray[z, p] = BoardType.Player2;

                        }
                        else
                            BoardArray[z, p] = BoardType.None;
                    }
                    if (!odd)
                    {
                        if (p % 2 == 1)
                        {
                            Player2 player2 = new Player2();
                            IsHitTestVisible = true;
                            //if(!p1Turn)
                            player2.MouseLeftButtonDown += Player2_Click;

                            Grid.SetColumn(player2, p);
                            Grid.SetRow(player2, z);
                            Board.Children.Add(player2);
                            BoardArray[z, p] = BoardType.Player2;
                        }
                        else
                            BoardArray[z, p] = BoardType.None;


                    }

                }
                odd = !odd;
            }
            //}
            for (int i = 0; i < _rows; i++)
            {
                CanGoBoard.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _cols; i++)
            {
                CanGoBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }
            //for (int x = 0; x < 8; x++)
            //{
            //    for (int y = 0; y < 8; y++)
            //    {

            //        CanGo cango = new CanGo();
            //        Grid.SetColumn(cango, y);
            //        Grid.SetRow(cango, x);
            //        CanGoBoard.Children.Add(cango);
            //    }
            //}

        }
        private void Player1_Click(object sender, MouseButtonEventArgs e)
        {
            if (p1Turn)
            {

                Player1 player1 = sender as Player1;


                int row = Grid.GetRow(player1);
                int col = Grid.GetColumn(player1);

                if (isClicked == false)
                {
                    if (col == 0 || col == 7)
                    {

                        if (col == 0)
                        {
                            if (BoardArray[row + 1, col + 1] == BoardType.Player1)
                            {

                            }
                            else
                            {
                                isClicked = true;
                                CanGo cangoBorder1 = new CanGo();
                                cangoBorder1.MouseLeftButtonDown += (sender, e) => CanGo1_Click(sender, e, player1, cangoBorder1);
                                if (BoardArray[row + 1, col + 1] == BoardType.Player2)
                                {
                                    if (row < 6)
                                    {
                                        if (BoardArray[row + 2, col + 2] == BoardType.Player2)
                                        {
                                            pbehindp = true;
                                        }
                                        else
                                        {
                                            Grid.SetColumn(cangoBorder1, col + 2);
                                            Grid.SetRow(cangoBorder1, row + 2);
                                            CanGoBoard.Children.Add(cangoBorder1);
                                            BoardArray[row + 2, col + 2] = BoardType.CanGo;
                                            canTakeRight = true;
                                        }
                                    }


                                }
                                else if (BoardArray[row + 1, col + 1] != BoardType.Player2 || pbehindp == true)//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cangoBorder1, col + 1);
                                    Grid.SetRow(cangoBorder1, row + 1);
                                    CanGoBoard.Children.Add(cangoBorder1);
                                    BoardArray[row + 1, col + 1] = BoardType.CanGo;
                                    pbehindp = false;
                                }

                            }

                        }
                        else
                        {
                            if (BoardArray[row + 1, col - 1] == BoardType.Player1)
                            {

                            }
                            else
                            {
                                isClicked = true;
                                CanGo cangoBorder2 = new CanGo();
                                cangoBorder2.MouseLeftButtonDown += (sender, e) => CanGo1_Click(sender, e, player1, cangoBorder2);
                                if (BoardArray[row + 1, col - 1] == BoardType.Player2)
                                {
                                    if (row < 6)
                                    {
                                        if (BoardArray[row + 2, col - 2] == BoardType.Player2)
                                        {
                                            pbehindp = true;
                                        }
                                        else
                                        {
                                            Grid.SetColumn(cangoBorder2, col - 2);
                                            Grid.SetRow(cangoBorder2, row + 2);
                                            CanGoBoard.Children.Add(cangoBorder2);
                                            BoardArray[row + 2, col - 2] = BoardType.CanGo;
                                            canTakeLeft = true;
                                        }
                                    }


                                }
                                else if (BoardArray[row + 1, col - 1] != BoardType.Player2 || pbehindp == true)//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cangoBorder2, col - 1);
                                    Grid.SetRow(cangoBorder2, row + 1);
                                    CanGoBoard.Children.Add(cangoBorder2);
                                    BoardArray[row + 1, col - 1] = BoardType.CanGo;
                                    pbehindp = false;
                                }

                            }

                        }
                    }
                    //pokud je to stredovy objekt
                    else
                    {
                        if (BoardArray[row + 1, col + 1] == BoardType.Player1) //doprava dolu
                        {

                        }
                        else
                        {
                            isClicked = true;
                            CanGo cango1 = new CanGo();
                            cango1.MouseLeftButtonDown += (sender, e) => CanGo1_Click(sender, e, player1, cango1);
                            if (BoardArray[row + 1, col + 1] == BoardType.Player2) //pokud muze vzit
                            {
                                if (col < 6 && row < 6)
                                {
                                    if (BoardArray[row + 2, col + 2] == BoardType.Player2)
                                    {
                                        pbehindp = true;
                                    }

                                    else
                                    {
                                        Grid.SetColumn(cango1, col + 2);
                                        Grid.SetRow(cango1, row + 2);
                                        CanGoBoard.Children.Add(cango1);
                                        BoardArray[row + 2, col + 2] = BoardType.CanGo;
                                        canTakeRight = true;
                                    }
                                }
                                else
                                {
                                    pbehindp = true;
                                }
                            }
                            else if (BoardArray[row + 1, col - 1] != BoardType.Player2 && BoardArray[row + 1, col + 1] != BoardType.Player2 || pbehindp == true)//pokud nemuze vzit
                            {
                                Grid.SetColumn(cango1, col + 1);
                                Grid.SetRow(cango1, row + 1);
                                CanGoBoard.Children.Add(cango1);
                                BoardArray[row + 1, col + 1] = BoardType.CanGo;
                                pbehindp = false;
                            }

                        }
                        if (BoardArray[row + 1, col - 1] == BoardType.Player1) //doleva dolu
                        {

                        }
                        else
                        {
                            isClicked = true;
                            CanGo cango2 = new CanGo();
                            cango2.MouseLeftButtonDown += (sender, e) => CanGo1_Click(sender, e, player1, cango2);
                            if (BoardArray[row + 1, col - 1] == BoardType.Player2) //pokud muze vzit
                            {
                                if (col > 1 && row < 6)
                                {
                                    if (BoardArray[row + 2, col - 2] == BoardType.Player2)
                                    {
                                        pbehindp = true;
                                    }
                                    else
                                    {
                                        Grid.SetColumn(cango2, col - 2);
                                        Grid.SetRow(cango2, row + 2);
                                        CanGoBoard.Children.Add(cango2);
                                        BoardArray[row + 2, col - 2] = BoardType.CanGo;
                                        canTakeLeft = true;
                                    }
                                }
                                else
                                {
                                    pbehindp = true;
                                }
                            }
                            else if (BoardArray[row + 1, col - 1] != BoardType.Player2 && BoardArray[row + 1, col + 1] != BoardType.Player2 || pbehindp == true)//pokud nemuze vzit
                            {
                                Grid.SetColumn(cango2, col - 1);
                                Grid.SetRow(cango2, row + 1);
                                CanGoBoard.Children.Add(cango2);
                                BoardArray[row + 1, col - 1] = BoardType.CanGo;
                                pbehindp = false;
                            }


                        }
                    }
                }
                else if (isClicked == true)
                {
                    CanGoBoard.Children.Clear();
                    isClicked = false;
                    canTakeLeft = false;
                    canTakeRight = false;
                    Player1_Click(player1, e);

                }

            }
            else
            {
                return;
            }

        }
        private void Player1_kingClick(object sender, MouseButtonEventArgs e)
        {
            if (p1Turn)
            {
                Player1 player1 = sender as Player1;
                int row = Grid.GetRow(player1);
                int col = Grid.GetColumn(player1);
                bool _1 = true;
                bool _2 = true;
                bool _3 = true;
                bool _4 = true;

                if (isClicked == false)
                {

                    for (int i = 1; i < 8; i++)
                    {
                        if (col == 0 || row == 0)
                        {
                            _1 = false;
                        }
                        
                        if (_1 == true && row > 0 && col > 0 && row - i >= 0 && col - i >= 0) //prvni smer
                        {
                            if (BoardArray[row - i, col - i] == BoardType.Player1)
                            {
                                _1 = false;
                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango1 = new CanGo();
                                cango1.MouseLeftButtonDown += (s, args) => CanGo1_Click(s, args, player1, cango1);
                                if (BoardArray[row - i, col - i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col > 1 + i && row > 1 + i)
                                    {
                                        if (BoardArray[row - (i + 1), col - (i + 1)] == BoardType.Player2)
                                        {
                                            _1 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango1, col - (i + 1));
                                            Grid.SetRow(cango1, row - (i + 1));
                                            CanGoBoard.Children.Add(cango1);
                                            BoardArray[row - (i + 1), col - (i + 1)] = BoardType.CanGo;
                                            cantake1 = true;
                                        }
                                    }
                                    else
                                    {
                                        _1 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango1, col - i);
                                    Grid.SetRow(cango1, row - i);
                                    CanGoBoard.Children.Add(cango1);
                                    BoardArray[row - i, col - i] = BoardType.CanGo;
                                }
                            }
                        }
                        if (col == 7 || row == 0)
                        {
                            _2 = false;
                        }
                        if (_2 == true && row > 0 && col < 7 && row -i >= 0 && col + i <= 7) //druhy smer
                        {
                            if (_2 == false || BoardArray[row - i, col + i] == BoardType.Player1)
                            {
                                _2 = false;
                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango2 = new CanGo();
                                cango2.MouseLeftButtonDown += (s, args) => CanGo1_Click(s, args, player1, cango2);
                                if (BoardArray[row - i, col + i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col < 6 - i && row > 1 + i)
                                    {
                                        if (BoardArray[row - (i + 1), col + (i + 1)] == BoardType.Player2)
                                        {
                                            _2 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango2, col + (i + 1));
                                            Grid.SetRow(cango2, row - (i + 1));
                                            CanGoBoard.Children.Add(cango2);
                                            BoardArray[row - (i + 1), col + (i + 1)] = BoardType.CanGo;
                                            cantake2 = true;
                                        }
                                    }
                                    else
                                    {
                                        _2 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango2, col + i);
                                    Grid.SetRow(cango2, row - i);
                                    CanGoBoard.Children.Add(cango2);
                                    BoardArray[row - i, col + i] = BoardType.CanGo;
                                }
                            }

                        }
                        if (col == 0 || row == 7)
                        {
                            _3 = false;
                        }
                        
                        if (_3 == true && row < 7 && col > 0 && row + i <= 7 && col - i >= 0) //treti smer
                        {
                            if (BoardArray[row + i, col - i] == BoardType.Player1)
                            {
                                _3 = false;
                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango3 = new CanGo();
                                cango3.MouseLeftButtonDown += (s, args) => CanGo1_Click(s, args, player1, cango3);
                                if (BoardArray[row + i, col - i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col > 1 + i && row < 1 - i)
                                    {
                                        if (BoardArray[row + (i + 1), col - (i + 1)] == BoardType.Player2)
                                        {
                                            _3 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango3, col - (i + 1));
                                            Grid.SetRow(cango3, row + (i + 1));
                                            CanGoBoard.Children.Add(cango3);
                                            BoardArray[row + (i + 1), col - (i + 1)] = BoardType.CanGo;
                                            cantake3 = true;
                                        }
                                    }
                                    else
                                    {
                                        _3 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango3, col - i);
                                    Grid.SetRow(cango3, row + i);
                                    CanGoBoard.Children.Add(cango3);
                                    BoardArray[row + i, col - i] = BoardType.CanGo;
                                }
                            }
                        }
                        if (col == 7 || row == 7)
                        {
                            _4 = false;
                        }
                        
                        if (_4 == true && row < 7 && col < 7 && row + i <= 7 && col + i <= 7) //ctvrty smer
                        {
                            if (BoardArray[row + i, col + i] == BoardType.Player1)
                            {
                                _4 = false;
                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango4 = new CanGo();
                                cango4.MouseLeftButtonDown += (s, args) => CanGo1_Click(s, args, player1, cango4);
                                if (BoardArray[row + i, col + i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col > 1 - i && row < 1 - i)
                                    {
                                        if (BoardArray[row + (i + 1), col + (i + 1)] == BoardType.Player2)
                                        {
                                            _4 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango4, + (i + 1));
                                            Grid.SetRow(cango4, row + (i + 1));
                                            CanGoBoard.Children.Add(cango4);
                                            BoardArray[row + (i + 1), col + (i + 1)] = BoardType.CanGo;
                                            cantake4 = true;
                                        }
                                    }
                                    else
                                    {
                                        _4 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango4, col + i);
                                    Grid.SetRow(cango4, row + i);
                                    CanGoBoard.Children.Add(cango4);
                                    BoardArray[row + i, col + i] = BoardType.CanGo;
                                }
                            }
                        }
                    }
                }
                else if (isClicked == true)
                {
                    CanGoBoard.Children.Clear();
                    isClicked = false;
                    cantake1 = false;
                    cantake2 = false;
                    cantake3 = false;
                    cantake4 = false;
                    Player1_kingClick(player1, e);
                }
            }
        }


        private void Player2_Click(object sender, MouseButtonEventArgs e)
        {
            if(!p1Turn)
            {
                Player2 player2 = sender as Player2;
                

                
                int row = Grid.GetRow(player2);
                int col = Grid.GetColumn(player2);

                if (isClicked == false)
                {
                    if (col == 0 || col == 7)
                    {

                        if (col == 0)
                        {
                            if (BoardArray[row - 1, col + 1] == BoardType.Player2)
                            {
                               
                            }
                            else
                            {
                                isClicked = true;
                                CanGo cangoBorder1 = new CanGo();
                                cangoBorder1.MouseLeftButtonDown += (sender, e) => CanGo2_Click(sender, e, player2, cangoBorder1);
                                if (BoardArray[row - 1, col + 1] == BoardType.Player1)
                                {
                                    if (row > 1)
                                    {
                                        if (BoardArray[row - 2, col + 2] == BoardType.Player1)
                                        {
                                            pbehindp = true;
                                        }
                                        else
                                        {
                                            Grid.SetColumn(cangoBorder1, col + 2);
                                            Grid.SetRow(cangoBorder1, row - 2);
                                            CanGoBoard.Children.Add(cangoBorder1);
                                            BoardArray[row - 2, col + 2] = BoardType.CanGo;
                                            canTakeRight = true;
                                        }
                                    }
                                    

                                }
                                else if (BoardArray[row - 1, col + 1] != BoardType.Player1 || pbehindp == true)//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cangoBorder1, col + 1);
                                    Grid.SetRow(cangoBorder1, row - 1);
                                    CanGoBoard.Children.Add(cangoBorder1);
                                    BoardArray[row - 1, col + 1] = BoardType.CanGo;
                                    pbehindp = false;
                                }
                            }

                        }
                        else
                        {
                            if (BoardArray[row - 1, col - 1] == BoardType.Player2)
                            {

                            }
                            else
                            {
                                isClicked = true;
                                CanGo cangoBorder2 = new CanGo();
                                cangoBorder2.MouseLeftButtonDown += (sender, e) => CanGo2_Click(sender, e, player2, cangoBorder2);
                                if (BoardArray[row - 1, col - 1] == BoardType.Player1)
                                {
                                    if (row > 1)
                                    {
                                        if(BoardArray[row - 2, col - 2] == BoardType.Player1)
                                    {
                                            pbehindp = true;
                                        }
                                    else
                                        {
                                            Grid.SetColumn(cangoBorder2, col - 2);
                                            Grid.SetRow(cangoBorder2, row - 2);
                                            CanGoBoard.Children.Add(cangoBorder2);
                                            BoardArray[row - 2, col - 2] = BoardType.CanGo;
                                            canTakeLeft = true;
                                        }
                                    }
                                    

                                }
                                else if (BoardArray[row - 1, col - 1] != BoardType.Player1 || pbehindp == true)//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cangoBorder2, col - 1);
                                    Grid.SetRow(cangoBorder2, row - 1);
                                    CanGoBoard.Children.Add(cangoBorder2);
                                    BoardArray[row - 1, col - 1] = BoardType.CanGo;
                                    pbehindp = false;
                                }
                            }

                        }
                    }
                    //pokud je stredovy objekt
                    else
                    {
                        if (BoardArray[row - 1, col + 1] == BoardType.Player2) //doprava nahoru
                        {

                        }
                        else
                        {
                            isClicked = true;
                            CanGo cango1 = new CanGo();
                            cango1.MouseLeftButtonDown += (sender, e) => CanGo2_Click(sender, e, player2, cango1);
                            if (BoardArray[row - 1, col + 1] == BoardType.Player1) //pokud muze vzit
                            {
                                if (col < 6 && row > 1)
                                {
                                    if (BoardArray[row - 2, col + 2] == BoardType.Player1)
                                    {
                                        pbehindp = true;
                                    }
                                    else
                                    {
                                        Grid.SetColumn(cango1, col + 2);
                                        Grid.SetRow(cango1, row - 2);
                                        CanGoBoard.Children.Add(cango1);
                                        BoardArray[row - 2, col + 2] = BoardType.CanGo;
                                        canTakeRight = true;
                                    }
                                }
                                else
                                {
                                    pbehindp = true;
                                }
                            }
                            else if (BoardArray[row - 1, col + 1] != BoardType.Player1 && BoardArray[row - 1, col - 1] != BoardType.Player1 || pbehindp == true)//pokud nemuze vzit
                            {
                                Grid.SetColumn(cango1, col + 1);
                                Grid.SetRow(cango1, row - 1);
                                CanGoBoard.Children.Add(cango1);
                                BoardArray[row - 1, col + 1] = BoardType.CanGo;
                                pbehindp = false;
                            }

                        }
                        if (BoardArray[row - 1, col - 1] == BoardType.Player2) //doleva nahoru
                        {

                        }
                        else
                        {
                            isClicked = true;
                            CanGo cango2 = new CanGo();
                            cango2.MouseLeftButtonDown += (sender, e) => CanGo2_Click(sender, e, player2, cango2);
                            if (BoardArray[row - 1, col - 1] == BoardType.Player1) //pokud muze vzit
                            {
                                if (col > 1 && row > 1)
                                {
                                    if (BoardArray[row - 2, col - 2] == BoardType.Player1)
                                    {
                                        pbehindp = true;
                                    }
                                    else
                                    {
                                        Grid.SetColumn(cango2, col - 2);
                                        Grid.SetRow(cango2, row - 2);
                                        CanGoBoard.Children.Add(cango2);
                                        BoardArray[row - 2, col - 2] = BoardType.CanGo;
                                        canTakeLeft = true;
                                    }
                                }
                                else
                                {
                                    pbehindp = true;
                                }
                            }
                            else if (BoardArray[row - 1, col + 1] != BoardType.Player1 && BoardArray[row - 1, col - 1] != BoardType.Player1 || pbehindp == true)//pokud nemuze vzit
                            {
                                Grid.SetColumn(cango2, col - 1);
                                Grid.SetRow(cango2, row - 1);
                                CanGoBoard.Children.Add(cango2);
                                BoardArray[row - 1, col - 1] = BoardType.CanGo;
                                pbehindp = false;
                            }


                        }
                    }
                }
                else if (isClicked == true)
                {
                    CanGoBoard.Children.Clear();
                    isClicked = false;
                    canTakeLeft = false;
                    canTakeRight = false;
                    Player2_Click(player2, e);

                }
               
            }
            else
            {
                return;
            }
            
        }
        private void Player2_kingClick(object sender, MouseButtonEventArgs e)
        {
            if (!p1Turn)
            {
                Player2 player2 = sender as Player2;
                int row = Grid.GetRow(player2);
                int col = Grid.GetColumn(player2);
                bool _1 = true;
                bool _2 = true;
                bool _3 = true;
                bool _4 = true;

                if (isClicked == false)
                {

                    for (int i = 1; i < 8; i++)
                    {
                        if (_1 == true && row > 0 && col > 0 && row - i >= 0 && col - i >= 0) //prvni smer
                        {
                            if (BoardArray[row - i, col - i] == BoardType.Player1)
                            {

                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango1 = new CanGo();
                                cango1.MouseLeftButtonDown += (s, args) => CanGo2_Click(s, args, player2, cango1);
                                if (BoardArray[row - i, col - i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col > 1 + i && row > 1 + i)
                                    {
                                        if (BoardArray[row - (i + 1), col - (i + 1)] == BoardType.Player2)
                                        {
                                            _1 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango1, col - (i + 1));
                                            Grid.SetRow(cango1, row - (i + 1));
                                            CanGoBoard.Children.Add(cango1);
                                            BoardArray[row - (i + 1), col - (i + 1)] = BoardType.CanGo;
                                            cantake1 = true;
                                        }
                                    }
                                    else
                                    {
                                        _1 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango1, col - i);
                                    Grid.SetRow(cango1, row - i);
                                    CanGoBoard.Children.Add(cango1);
                                    BoardArray[row - i, col - i] = BoardType.CanGo;
                                }
                            }
                        }
                        if (_2 == true && row > 0 && col < 7 && row - i >= 0 && col + i <= 7) //druhy smer
                        {
                            if (BoardArray[row - i, col + i] == BoardType.Player1)
                            {

                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango2 = new CanGo();
                                cango2.MouseLeftButtonDown += (s, args) => CanGo2_Click(s, args, player2, cango2);
                                if (BoardArray[row - i, col + i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col < 6 - i && row > 1 + i)
                                    {
                                        if (BoardArray[row - (i + 1), col + (i + 1)] == BoardType.Player2)
                                        {
                                            _2 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango2, col + (i + 1));
                                            Grid.SetRow(cango2, row - (i + 1));
                                            CanGoBoard.Children.Add(cango2);
                                            BoardArray[row - (i + 1), col + (i + 1)] = BoardType.CanGo;
                                            cantake2 = true;
                                        }
                                    }
                                    else
                                    {
                                        _2 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango2, col + i);
                                    Grid.SetRow(cango2, row - i);
                                    CanGoBoard.Children.Add(cango2);
                                    BoardArray[row - i, col + i] = BoardType.CanGo;
                                }
                            }

                        }
                        if (_3 == true && row < 7 && col > 0 && row + i <= 7 && col - i >= 0) //treti smer
                        {
                            if (BoardArray[row + i, col - i] == BoardType.Player1)
                            {

                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango3 = new CanGo();
                                cango3.MouseLeftButtonDown += (s, args) => CanGo2_Click(s, args, player2, cango3);
                                if (BoardArray[row + i, col - i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col > 1 + i && row < 1 - i)
                                    {
                                        if (BoardArray[row + (i + 1), col - (i + 1)] == BoardType.Player2)
                                        {
                                            _3 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango3, col - (i + 1));
                                            Grid.SetRow(cango3, row + (i + 1));
                                            CanGoBoard.Children.Add(cango3);
                                            BoardArray[row + (i + 1), col - (i + 1)] = BoardType.CanGo;
                                            cantake3 = true;
                                        }
                                    }
                                    else
                                    {
                                        _3 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango3, col - i);
                                    Grid.SetRow(cango3, row + i);
                                    CanGoBoard.Children.Add(cango3);
                                    BoardArray[row + i, col - i] = BoardType.CanGo;
                                }
                            }
                        }
                        if (_4 == true && row < 7 && col < 7 && row + i <= 7 && col + i <= 7) //ctvrty smer
                        {
                            if (BoardArray[row + i, col + i] == BoardType.Player1)
                            {

                            }
                            else
                            {
                                isClicked = true;
                                CanGo cango4 = new CanGo();
                                cango4.MouseLeftButtonDown += (s, args) => CanGo2_Click(s, args, player2, cango4);
                                if (BoardArray[row + i, col + i] == BoardType.Player2) //pokud muze vzit
                                {
                                    if (col > 1 - i && row < 1 - i)
                                    {
                                        if (BoardArray[row + (i + 1), col + (i + 1)] == BoardType.Player2)
                                        {
                                            _4 = false;
                                        }

                                        else
                                        {
                                            Grid.SetColumn(cango4, +(i + 1));
                                            Grid.SetRow(cango4, row + (i + 1));
                                            CanGoBoard.Children.Add(cango4);
                                            BoardArray[row + (i + 1), col + (i + 1)] = BoardType.CanGo;
                                            cantake4 = true;
                                        }
                                    }
                                    else
                                    {
                                        _4 = false;
                                    }
                                }
                                else//pokud nemuze vzit
                                {
                                    Grid.SetColumn(cango4, col + i);
                                    Grid.SetRow(cango4, row + i);
                                    CanGoBoard.Children.Add(cango4);
                                    BoardArray[row + i, col + i] = BoardType.CanGo;
                                }
                            }
                        }
                    }
                }
                else if (isClicked == true)
                {
                    CanGoBoard.Children.Clear();
                    isClicked = false;
                    cantake1 = false;
                    cantake2 = false;
                    cantake3 = false;
                    cantake4 = false;
                    Player2_kingClick(player2, e);
                }
            }
        }
        private void CanGo1_Click(object sender, MouseButtonEventArgs e, Player1 player1, CanGo cango)
        {
            BoardArray[Grid.GetRow(player1), Grid.GetColumn(player1)] = BoardType.None;
            if (cantake1)
            {
                BoardArray[Grid.GetRow(player1) - 1, Grid.GetColumn(player1) - 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player1) && ely == Grid.GetColumn(player1))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players2left--;
                cantake1 = false;
            }
            if (cantake2)
            {
                BoardArray[Grid.GetRow(player1) - 1, Grid.GetColumn(player1) + 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) - 1;
                    if (elx == Grid.GetRow(player1) && ely == Grid.GetColumn(player1))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players2left--;
                cantake2 = false;
            }
            if (cantake3)
            {
                BoardArray[Grid.GetRow(player1) + 1, Grid.GetColumn(player1) - 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) - 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player1) && ely == Grid.GetColumn(player1))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players2left--;
                cantake3 = false;
            }
            if (cantake4)
            {
                BoardArray[Grid.GetRow(player1) + 1, Grid.GetColumn(player1) + 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player1) && ely == Grid.GetColumn(player1))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players2left--;
                cantake4 = false;
            }
            if (canTakeLeft)
            {
                BoardArray[Grid.GetRow(player1) + 1, Grid.GetColumn(player1) - 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) - 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player1) && ely  == Grid.GetColumn(player1))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players2left--;
                canTakeLeft = false;
                // Board.Visibility = Visibility.Hidden;
            }
            else if(canTakeRight)
            {
                BoardArray[Grid.GetRow(player1) + 1, Grid.GetColumn(player1) + 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) - 1;
                    int ely = Grid.GetColumn(uiel) - 1;
                    if (elx == Grid.GetRow(player1) && ely == Grid.GetColumn(player1))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players2left--;
                canTakeRight = false;
            }

            int row = Grid.GetRow(cango);
            int col = Grid.GetColumn(cango);
            BoardArray[row, col] = BoardType.Player1;
            Grid.SetRow(player1, row);
            Grid.SetColumn(player1, col);
            isClicked = false;
            p1Turn = !p1Turn;
            // Smazat všechny objekty CanGo z CanGoBoard
            CanGoBoard.Children.Clear();
            if (players2left == 0)
            {
                redwon.Visibility = Visibility.Visible;
            }
            if (row == 7)
            {
                player1.king.Visibility = Visibility.Visible;
                player1.MouseLeftButtonDown -= Player1_Click;
                player1.MouseLeftButtonDown += Player1_kingClick;
            }
        }
        private void CanGo2_Click(object sender, MouseButtonEventArgs e, Player2 player2, CanGo cango)
        {
            BoardArray[Grid.GetRow(player2), Grid.GetColumn(player2)] = BoardType.None;
            if (cantake1)
            {
                BoardArray[Grid.GetRow(player2) - 1, Grid.GetColumn(player2) - 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player2) && ely == Grid.GetColumn(player2))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players1left--;
                cantake1 = false;
            }
            if (cantake2)
            {
                BoardArray[Grid.GetRow(player2) - 1, Grid.GetColumn(player2) + 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) - 1;
                    if (elx == Grid.GetRow(player2) && ely == Grid.GetColumn(player2))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players1left--;
                cantake2 = false;
            }
            if (cantake3)
            {
                BoardArray[Grid.GetRow(player2) + 1, Grid.GetColumn(player2) - 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) - 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player2) && ely == Grid.GetColumn(player2))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players1left--;
                cantake3 = false;
            }
            if (cantake4)
            {
                BoardArray[Grid.GetRow(player2) + 1, Grid.GetColumn(player2) + 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player2) && ely == Grid.GetColumn(player2))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players1left--;
                cantake4 = false;
            }
            if (canTakeLeft)
            {
                BoardArray[Grid.GetRow(player2) - 1, Grid.GetColumn(player2) - 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) + 1;
                    if (elx == Grid.GetRow(player2) && ely == Grid.GetColumn(player2))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players1left--;
                canTakeLeft = false;
                // Board.Visibility = Visibility.Hidden;
            }
            else if (canTakeRight)
            {
                BoardArray[Grid.GetRow(player2) - 1, Grid.GetColumn(player2) + 1] = BoardType.None;
                foreach (var el in Board.Children)
                {
                    UIElement uiel = (UIElement)el;
                    int elx = Grid.GetRow(uiel) + 1;
                    int ely = Grid.GetColumn(uiel) - 1;
                    if (elx == Grid.GetRow(player2) && ely == Grid.GetColumn(player2))
                    {
                        Board.Children.Remove(uiel);
                        break;
                    }
                }
                players1left--;
                canTakeRight = false;
            }

            int row = Grid.GetRow(cango);
            int col = Grid.GetColumn(cango);
            BoardArray[row, col] = BoardType.Player2;
            Grid.SetRow(player2, row);
            Grid.SetColumn(player2, col);
            isClicked = false;
            p1Turn = !p1Turn;
            // Smazat všechny objekty CanGo z CanGoBoard
            CanGoBoard.Children.Clear();    
            if (players1left == 0)
            {
                bluewon.Visibility = Visibility.Visible;
            }
            if (row == 0)
            {
                player2.king.Visibility = Visibility.Visible;
                player2.MouseLeftButtonDown -= Player2_Click;
                player2.MouseLeftButtonDown += Player2_kingClick;
            }
        }

    }
}

