using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaidCalculator
{
    public partial class MainWindow : Window
    {
        public List<int> count = new List<int>();
        
        public MainWindow()
        {
            InitializeComponent();
            sizeBox.Text = "";
            varBox.Text = "";
            genericButton.IsChecked = true;
            zeroButton.IsChecked = true;
            TypeCheck();
            LevelCheck();
            CountCheck();
        }

        /// <summary>
        /// Общая проверка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        public void Check(object sender, RoutedEventArgs routedEventArgs)
        {
            sizeBox.Text = "";
            varBox.Text = "";
            countBox.SelectedIndex = 0;
            TypeCheck();
            LevelCheck();
            CountCheck();
            PreCountCheck();
        }

        /// <summary>
        /// Изменение типа RAID
        /// </summary>
        public void TypeCheck()
        {
            if (genericButton.IsChecked == true)
            {
                if (oneButton.IsChecked == true || fiveZeroButton.IsChecked == true || sixZeroButton.IsChecked == true || sevenZeroButton.IsChecked == true)
                {
                    zeroButton.IsChecked = true;
                }
                eraPanel.IsVisible = false;
                varText.IsVisible = false;
                varBox.IsVisible = false;
            }
            else
            {
                eraPanel.IsVisible = true;
            }
        }

        /// <summary>
        /// Вывод текстбокса для размера группы дисков
        /// </summary>
        public void LevelCheck()
        {
            if (oneZeroButton.IsChecked == true || fiveZeroButton.IsChecked == true || sixZeroButton.IsChecked == true || sevenZeroButton.IsChecked == true || nPlusMButton.IsChecked == true)
            {
                varText.IsVisible = true;
                varBox.IsVisible = true;
                countBox.IsEnabled = false;
                if (nPlusMButton.IsChecked == true)
                {
                    varText.Text = "Дисков под контрольные суммы:";
                }
                else
                {
                    varText.Text = "Размер группы:";
                }
            }
            else
            {
                varText.IsVisible = false;
                varBox.IsVisible = false;
                countBox.IsEnabled = true;
            }
        }

        /// <summary>
        /// Проверка корректности введённого количества дисков
        /// </summary>
        public void CountCheck()
        {
            count.Clear();
            if (zeroButton.IsChecked == true)
            {
                for (int i = 2; i < 65; i++)
                {
                    count.Add(i);
                }
            }
            else if (oneButton.IsChecked == true)
            {
                for (int i = 2; i < 33; i++)
                {
                    count.Add(i);
                }
            }
            else if (fiveButton.IsChecked == true)
            {
                if (genericButton.IsChecked == true)
                {
                    for (int i = 5; i < 64; i++)
                    {
                        count.Add(i);
                    }
                }
                else if (eraButton.IsChecked == true)
                {
                    for (int i = 4; i < 64; i++)
                    {
                        count.Add(i);
                    }
                }
            }
            else if (sixButton.IsChecked == true)
            {
                if (genericButton.IsChecked == true)
                {
                    for (int i = 5; i < 63; i++)
                    {
                        count.Add(i);
                    }
                }
                else if (eraButton.IsChecked == true)
                {
                    for (int i = 4; i < 63; i++)
                    {
                        count.Add(i);
                    }
                }
            }
            else if (sevenThreeButton.IsChecked == true)
            {
                for (int i = 6; i < 62; i++)
                {
                    count.Add(i);
                }
            }
            else if (nPlusMButton.IsChecked == true)
            {
                if (varBox.Text != "")
                {
                    for (int i = 8; i < 65 - Convert.ToInt32(varBox.Text); i++)
                    {
                        count.Add(i);
                    }
                }
            }
            else if (oneZeroButton.IsChecked == true)
            {
                if (varBox.Text != "" && Convert.ToInt32(varBox.Text) >= 4)
                {
                    for (int i = Convert.ToInt32(varBox.Text); i < 33; i += 2)
                    {
                        count.Add(i);
                    }
                }
            }
            else if (fiveZeroButton.IsChecked == true)
            {
                if (varBox.Text != "" && Convert.ToInt32(varBox.Text) >= 8)
                {
                    for (int i = Convert.ToInt32(varBox.Text); i < 65 - Convert.ToInt32(varBox.Text); i += Convert.ToInt32(varBox.Text))
                    {
                        count.Add(i);
                    }
                }
            }
            else if (sixZeroButton.IsChecked == true)
            {
                if (varBox.Text != "" && Convert.ToInt32(varBox.Text) >= 8)
                {
                    for (int i = Convert.ToInt32(varBox.Text); i < 65 - (Convert.ToInt32(varBox.Text) * 2); i += Convert.ToInt32(varBox.Text))
                    {
                        count.Add(i);
                    }
                }
            }
            else if (sevenZeroButton.IsChecked == true)
            {
                if (varBox.Text != "" && Convert.ToInt32(varBox.Text) >= 8)
                {
                    for (int i = Convert.ToInt32(varBox.Text); i < 65 - (Convert.ToInt32(varBox.Text) * 3); i += Convert.ToInt32(varBox.Text))
                    {
                        count.Add(i);
                    }
                }
            }
            countBox.ItemsSource = count.ToList();
            countBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Ввод только цифр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string EnterCheck(string text)
        {
            if (text != "")
            {
                if (text.Substring(0, 1) == "0")
                {
                    text = text.Substring(1);
                }
                string enter = text;
                foreach (char e in text)
                {
                    if (Char.IsDigit(e) == false)
                    {
                        enter = enter.Substring(0, enter.IndexOf(e)) + enter.Substring(enter.IndexOf(e) + 1);
                    }
                }
                text = enter;
            }
            return text;
        }

        /// <summary>
        /// Ограничения на ввод и пересчёт результата при изменении объёма диска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SizeEnterCheck(object sender, KeyEventArgs keyEventArgs)
        {
            sizeBox.Text = EnterCheck(sizeBox.Text);
            PreCountCheck();
        }

        /// <summary>
        /// Ограничения на ввод и пересчёт результата при изменении размера группы / количества контрольных сумм
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="keyEventArgs"></param>
        public void VarEnterCheck(object sender, KeyEventArgs keyEventArgs)
        {
            varBox.Text = EnterCheck(varBox.Text);
            if (varBox.Text == "")
            {
                countBox.IsEnabled = false;
            }
            else
            {
                countBox.IsEnabled = true;
            }
            CountCheck();
            PreCountCheck();
        }

        /// <summary>
        /// Пересчёт результата при изменении количества дисков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectionChangedEventArgs"></param>
        public void SelectCheck(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            PreCountCheck();
        }

        /// <summary>
        /// Проверка всех полей на соответствие
        /// </summary>
        public void PreCountCheck()
        {
            answerBox.Text = "";
            answerBox.Foreground = Brush.Parse("Red");
            if (sizeBox.Text == "")
            {
                answerBox.Text = "Введите объём диска\n";
            }
            if (nPlusMButton.IsChecked == true || oneZeroButton.IsChecked == true || fiveZeroButton.IsChecked == true || sixZeroButton.IsChecked == true || sevenZeroButton.IsChecked == true)
            {
                if (varBox.Text == "")
                {
                    if (varText.Text == "Размер группы:")
                    {
                        answerBox.Text += "Введите размер группы";
                    }
                    else
                    {
                        answerBox.Text += "Введите кол-во дисков под контрольные суммы";
                    }
                }
                else
                {
                    if (oneZeroButton.IsChecked == true && Convert.ToInt32(varBox.Text) < 4)
                    {
                        countBox.IsEnabled = false;
                        answerBox.Text += "Минимальный размер группы: 4";
                    }
                    else if (fiveZeroButton.IsChecked == true && Convert.ToInt32(varBox.Text) < 8)
                    {
                        countBox.IsEnabled = false;
                        answerBox.Text += "Минимальный размер группы: 8";
                    }
                    else if (sixZeroButton.IsChecked == true && Convert.ToInt32(varBox.Text) < 8)
                    {
                        countBox.IsEnabled = false;
                        answerBox.Text += "Минимальный размер группы: 8";
                    }
                    else if (sevenZeroButton.IsChecked == true && Convert.ToInt32(varBox.Text) < 8)
                    {
                        countBox.IsEnabled = false;
                        answerBox.Text += "Минимальный размер группы: 8";
                    }
                }
            }
            if (answerBox.Text == "")
            {
                Count();
            }
        }

        /// <summary>
        /// Расчёт ёмкости с RAID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        public void Count()
        {
            answerBox.Foreground = Brush.Parse("DarkBlue");
            int DS = Convert.ToInt32(sizeBox.Text);
            int DC = count[countBox.SelectedIndex];
            if (nPlusMButton.IsChecked == true)
            {
                int CS = Convert.ToInt32(varBox.Text);
                answerBox.Text = Convert.ToString(DS * (DC + CS)) + " ТБ";
            }
            else if (oneZeroButton.IsChecked == true || fiveZeroButton.IsChecked == true || sixZeroButton.IsChecked == true || sevenZeroButton.IsChecked == true)
            {
                int GS = Convert.ToInt32(varBox.Text);
                if (oneZeroButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(((DC * 2 / GS) + DC) * DS);
                }
                else if (fiveZeroButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString((((DC + 1) / GS) + DC) * DS);
                }
                else if (sixZeroButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(((((DC + 2) / GS) * 2) + DC) * DS);
                }
                else if (sevenZeroButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(((((DC + 3) / GS) * 3) + DC) * DS);
                }
                answerBox.Text += " ТБ";
            }
            else
            {
                if (zeroButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(DS * DC);
                }
                else if (oneButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(DS * DC * 2);
                }
                else if (fiveButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(DS * (DC + 1));
                }
                else if (sixButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(DS * (DC + 2));
                }
                else if (sevenThreeButton.IsChecked == true)
                {
                    answerBox.Text = Convert.ToString(DS * (DC + 3));
                }
                answerBox.Text += " ТБ";
            }
        }
    }
}