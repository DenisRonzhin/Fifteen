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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fifteen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] arrayGame = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, {13, 14, 15, 0}};
       
        private struct buttonPosition
        {
            public Button buttonNumber;
            public int row;
            public int col;
        }

        List<buttonPosition> buttonList = new List<buttonPosition> { };

        //Инициализация игрового поля
        //Запомним положения кнопок на поле (фиксанем их координаты столбец, строка)
        private void InitialGame()
        {
            //1 строка поля
            Addbtn(0, 0, Button1);
            Addbtn(0, 1, Button2);
            Addbtn(0, 2, Button3);
            Addbtn(0, 3, Button4);

            //2-я строка поля
            Addbtn(1, 0, Button5);
            Addbtn(1, 1, Button6);
            Addbtn(1, 2, Button7);
            Addbtn(1, 3, Button8);

            //3-я строка поля
            Addbtn(2, 0, Button9);
            Addbtn(2, 1, Button10);
            Addbtn(2, 2, Button11);
            Addbtn(2, 3, Button12);

            //4-я строка поля
            Addbtn(3, 0, Button13);
            Addbtn(3, 1, Button14);
            Addbtn(3, 2, Button15);
            Addbtn(3, 3, Button16);
      
        }


        private void Check()
        {
            bool i = false;

            foreach (var item in buttonList)
            {
                if (item.buttonNumber.Name != "Button16" && "Button" + item.buttonNumber.Content.ToString() == item.buttonNumber.Name) i = true;
                else if (item.buttonNumber.Name != "Button16")
                {

                   i = false;
                   break;

                }   
            }
            if (i)
            {
                MessageBox.Show("У тебя получилось !!!");
                ShuffleArray();
                ShowButtonTitle();
            }
        }


        private void Addbtn(int row, int col, Button btn)
        {
            buttonPosition btnPosition = new buttonPosition();
            btnPosition.buttonNumber = btn;
            btnPosition.row = row;
            btnPosition.col = col;
            buttonList.Add(btnPosition);
        }


        //Перемешаем массив
        private void ShuffleArray()
        {
            Random rnd = new Random();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int tmpCol = rnd.Next(col + 1);
                    int tmpRow = rnd.Next(row + 1);
                    var tmp = arrayGame[row, col];
                    arrayGame[row, col] = arrayGame[tmpRow, tmpCol];
                    arrayGame[tmpRow, tmpCol] = tmp;
                }
            }
        }

        //Отоборазим заголовки кнопок, заголовки берем из массива arrayGame
        private void ShowButtonTitle()
        {
            int buttonIndex = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    buttonIndex++;
                    buttonList[buttonIndex-1].buttonNumber.Content = arrayGame[row, col].ToString();
                    buttonList[buttonIndex - 1].buttonNumber.Background = Brushes.LightGray;

                    if ("Button" + buttonList[buttonIndex - 1].buttonNumber.Content.ToString() == buttonList[buttonIndex - 1].buttonNumber.Name)
                    {
                        buttonList[buttonIndex - 1].buttonNumber.Background = Brushes.Coral;
                    }

                    if (arrayGame[row, col] == 0) 
                    {
                        buttonList[buttonIndex - 1].buttonNumber.Background = Brushes.DarkGray;
                        buttonList[buttonIndex - 1].buttonNumber.Content = "";
                    }

                }
            }
        }

        //Событие нажатие на кнопку
        private void Button_click(object sender, EventArgs e)
        {

            var r = ((Button)sender).Content;

            var cell = buttonList.Find(x => x.buttonNumber == ((Button)sender));

            int currentRow = cell.row;
            int currentCol = cell.col;

            if (currentCol - 1 >= 0 && arrayGame[currentRow,currentCol - 1] == 0) 
            {
                arrayGame[currentRow, currentCol - 1]= arrayGame[currentRow, currentCol];
                arrayGame[currentRow, currentCol] = 0;
                ShowButtonTitle();
                Check();
            } 

            if (currentCol + 1 <= 3 && arrayGame[currentRow, currentCol + 1] == 0) 
            {
                arrayGame[currentRow, currentCol + 1] = arrayGame[currentRow, currentCol];
                arrayGame[currentRow, currentCol] = 0;
                ShowButtonTitle();
                Check();
            } 

            if (currentRow - 1 >= 0 && arrayGame[currentRow - 1, currentCol] == 0)
            {
                arrayGame[currentRow - 1, currentCol] = arrayGame[currentRow, currentCol];
                arrayGame[currentRow, currentCol] = 0;
                ShowButtonTitle();
                Check();
            } 

            if (currentRow + 1 <= 3 && arrayGame[currentRow + 1, currentCol ] == 0)
            {
                arrayGame[currentRow + 1,currentCol] = arrayGame[currentRow,currentCol];
                arrayGame[currentRow,currentCol] = 0;
                ShowButtonTitle();
                Check();
            }

            //MessageBox.Show($"Строка{cell.row.ToString()} Столбец{cell.col.ToString()} Значение{cell.buttonNumber.Content.ToString()}");

        }


        public MainWindow()
        {
            InitializeComponent();
            InitialGame();
            ShuffleArray();
            ShowButtonTitle();
        }
    }
}
