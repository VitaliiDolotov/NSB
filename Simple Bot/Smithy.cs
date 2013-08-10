using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Simple_Bot
{
    public static class Smithy
    {
        public static IWebDriver driver { get; set; }

        public static double[,] meltingField = new double[6, 6];

        public static void FirstInit()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    meltingField[i, j] = -1;
        }

        public static void InitMeltingField()
        {
            //проинициализировали поле числами
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    //Если ячейка пустая или содержит процент
                    if (meltingField[i, j] < 12 && meltingField[i, j] > -1000)
                    {
                        meltingField[i, j] = CellNumberGeter(i, j);
                        LeftUpCell(i, j);
                        UpCell(i, j);
                        RightUpCell(i, j);
                        RightCell(i, j);
                        RightDownCell(i, j);
                        DownCell(i, j);
                        LeftDownCell(i, j);
                    }
                    Console.Write(meltingField[i, j].ToString() + " ");
                }
        }

        //вытягиваем число из ячейки
        private static int CellNumberGeter(int x, int y)
        {
            string selector = string.Format(".game_field  tr:nth-of-type({0}) td:nth-of-type({1})", x + 1, y + 1);
            IWebElement cell = driver.FindElement(By.CssSelector(selector));
            string classAtribute = cell.GetAttribute("class");
            //Если строка не пустая, то возвращаем число, лижащее в ячейке, если пустая, то возвращаем -1
            if (!string.IsNullOrEmpty(classAtribute))
            {
                return Convert.ToInt32(classAtribute.Remove(0, 4));
            }
            else return -1;
        }

        //выходы
        public static void Exit(int x, int y)
        {
            int emptyCellsCount = 0;

            if (IsEmptyLeftUpCell(x, y))
                emptyCellsCount++;
            if (IsEmptyUpCell(x, y))
                emptyCellsCount++;
            if (IsEmptyRightUpCell(x, y))
                emptyCellsCount++;
            if (IsEmptyRightCell(x, y))
                emptyCellsCount++;
            if (IsEmptyRightDownCell(x, y))
                emptyCellsCount++;
            if (IsEmptyDownCell(x, y))
                emptyCellsCount++;
            if (IsEmptyLeftDownCell(x, y))
                emptyCellsCount++;
            if (IsEmptyLeftCell(x, y))
                emptyCellsCount++;

            if (meltingField[x, y] == emptyCellsCount)
            {
                if (IsEmptyLeftUpCell(x, y))
                    meltingField[x - 1, y - 1] = 999;
                if (IsEmptyUpCell(x, y))
                    meltingField[x, y - 1] = 999;
                if (IsEmptyRightUpCell(x, y))
                    meltingField[x + 1, y - 1] = 999;
                if (IsEmptyRightCell(x, y))
                    meltingField[x + 1, y] = 999;
                if (IsEmptyRightDownCell(x, y))
                    meltingField[x + 1, y + 1] = 999;
                if (IsEmptyDownCell(x, y))
                    meltingField[x, y + 1] = 999;
                if (IsEmptyLeftDownCell(x, y))
                    meltingField[x - 1, y + 1] = 999;
                if (IsEmptyLeftCell(x, y))
                    meltingField[x - 1, y] = 999;
            }
        }

        //кликаем ячейки смежные с нулевыми
        public static void ClickEmptyCell()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    bool isAnyEmptyCell = IsNullLeftUpCell(i, j) & IsNullUpCell(i, j) & IsNullRightUpCell(i, j) & IsNullRightCell(i, j) &
                        IsNullRightDownCell(i, j) & IsNullDownCell(i, j) & IsNullLeftDownCell(i, j) & IsNullLeftCell(i, j);
                    if (meltingField[i, j] == -999)
                    {
                        string selector = string.Format(".game_field tr:nth-of-type({0}) td:nth-of-type({1})", i + 1, j + 1);
                        driver.FindElement(By.CssSelector(selector)).Click();
                    }
                }
        }

        //кликаем 12,5
        public static void Click12Cell()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (meltingField[i, j] == 12.5)
                    {
                        string selector = string.Format(".game_field tr:nth-of-type({0}) td:nth-of-type({1})", i + 1, j + 1);
                        driver.FindElement(By.CssSelector(selector)).Click();
                    }
                }
        }

        //кликаем 25
        public static void Click25Cell()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (meltingField[i, j] == 25)
                    {
                        string selector = string.Format(".game_field tr:nth-of-type({0}) td:nth-of-type({1})", i + 1, j + 1);
                        driver.FindElement(By.CssSelector(selector)).Click();
                    }
                }
        }

        //кликаем 37,5
        public static void Click37Cell()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (meltingField[i, j] == 37.5)
                    {
                        string selector = string.Format(".game_field tr:nth-of-type({0}) td:nth-of-type({1})", i + 1, j + 1);
                        driver.FindElement(By.CssSelector(selector)).Click();
                    }
                }
        }

        private static double CellPercentage(int x, int y)
        {
            double retv = 100 * meltingField[x, y] / 8;
            if (retv == 0)   
            {
                string selector = string.Format(".game_field tr:nth-of-type({0}) td:nth-of-type({1})", x + 1, y + 1);
                driver.FindElement(By.CssSelector(selector)).Click();
                retv = CellNumberGeter(x, y);
            }
            return retv;
        }

        #region SetCellValuePerentage

        private static void LeftUpCell(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x - 1, y - 1] > 10 || meltingField[x - 1, y - 1] < 0) && meltingField[x - 1, y - 1] < 999)
            {
                if (meltingField[x - 1, y - 1] == -1)
                    meltingField[x - 1, y - 1] = CellPercentage(x, y);
                else
                    meltingField[x - 1, y - 1] += CellPercentage(x, y);
            }
        }

        private static void UpCell(int x, int y)
        {
            if (y - 1 < 0)
                return;
            //если в ячейке число и в верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x, y - 1] > 10 || meltingField[x, y - 1] < 0) && meltingField[x, y - 1] < 999)
            {
                if (meltingField[x, y - 1] == -1)
                    meltingField[x, y - 1] = CellPercentage(x, y);
                else
                    meltingField[x, y - 1] += CellPercentage(x, y);
            }
        }

        private static void RightUpCell(int x, int y)
        {
            if (x + 1 > 6 || y - 1 < 0)
                return;
            //если в ячейке число и в верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x + 1, y - 1] > 10 || meltingField[x + 1, y - 1] < 0) && meltingField[x + 1, y - 1] < 999)
            {
                if (meltingField[x + 1, y - 1] == -1)
                    meltingField[x + 1, y - 1] = CellPercentage(x, y);
                else
                    meltingField[x + 1, y - 1] += CellPercentage(x, y);
            }
        }

        private static void RightCell(int x, int y)
        {
            if (x + 1 > 6)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x + 1, y] > 10 || meltingField[x + 1, y] < 0) && meltingField[x + 1, y] < 999)
            {
                if (meltingField[x + 1, y] == -1)
                    meltingField[x + 1, y] = CellPercentage(x, y);
                else
                    meltingField[x + 1, y] += CellPercentage(x, y);
            }
        }

        private static void RightDownCell(int x, int y)
        {
            if (x + 1 > 6 || y + 1 > 6)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x + 1, y + 1] > 10 || meltingField[x + 1, y + 1] < 0) && meltingField[x + 1, y + 1] < 999)
            {
                if (meltingField[x + 1, y + 1] == -1)
                    meltingField[x + 1, y + 1] = CellPercentage(x, y);
                else
                    meltingField[x + 1, y + 1] += CellPercentage(x, y);
            }
        }

        private static void DownCell(int x, int y)
        {
            if (y + 1 > 6)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x, y + 1] > 10 || meltingField[x, y + 1] < 0) && meltingField[x, y + 1] < 999)
            {
                if (meltingField[x, y + 1] == -1)
                    meltingField[x, y + 1] = CellPercentage(x, y);
                else
                    meltingField[x, y + 1] += CellPercentage(x, y);
            }
        }

        private static void LeftDownCell(int x, int y)
        {
            if (x - 1 < 0 || y + 1 > 6)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x - 1, y + 1] > 10 || meltingField[x - 1, y + 1] < 0) && meltingField[x - 1, y + 1] < 999)
            {
                if (meltingField[x - 1, y + 1] == -1)
                    meltingField[x - 1, y + 1] = CellPercentage(x, y);
                else
                    meltingField[x - 1, y + 1] += CellPercentage(x, y);
            }
        }

        private static void LeftCell(int x, int y)
        {
            if (x - 1 < -1)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && (meltingField[x - 1, y] > 10 || meltingField[x - 1, y] < 0) && meltingField[x - 1, y] < 999)
            {
                if (meltingField[x - 1, y] == -1)
                    meltingField[x - 1, y] = CellPercentage(x, y);
                else
                    meltingField[x - 1, y] += CellPercentage(x, y);
            }
        }

        #endregion

        #region IsCellEmpty

        private static bool IsEmptyLeftUpCell(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем false
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y - 1] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y - 1] < 12 && meltingField[x - 1, y - 1] > -1)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyUpCell(int x, int y)
        {
            if (y - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x, y - 1] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y - 1] < 12 && meltingField[x - 1, y - 1] > -1)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyRightUpCell(int x, int y)
        {
            if (x + 1 > 6 || y - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y - 1] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y - 1] < 12 && meltingField[x + 1, y - 1] > -1)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyRightCell(int x, int y)
        {
            if (x + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y] < 12 && meltingField[x + 1, y] > -1)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyRightDownCell(int x, int y)
        {
            if (x + 1 > 6 || y + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y + 1] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y + 1] < 12 && meltingField[x + 1, y + 1] > -1)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyDownCell(int x, int y)
        {
            if (y + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x, y + 1] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x, y + 1] < 12 && meltingField[x, y + 1] > -1)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyLeftDownCell(int x, int y)
        {
            if (x - 1 < 0 || y + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y + 1] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y + 1] < 12 && meltingField[x - 1, y + 1] > -1)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyLeftCell(int x, int y)
        {
            if (x - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y] == 999)
            {
                //мутируем ячейку на -1
                meltingField[x, y]--;
                return false;
            }

            //если в ячейке число и в левой верхней тоже число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y] < 12 && meltingField[x - 1, y] > -1)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region IsNullCell

        private static bool IsNullLeftUpCell(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  меньше 999  но и не число
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y - 1] < 999 && meltingField[x - 1, y - 1] > 10)
            {
                meltingField[x - 1, y - 1] = -999;
                return true;
            }

            return false;
        }

        private static bool IsNullUpCell(int x, int y)
        {
            if (y - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x, y - 1] < 999 && meltingField[x, y - 1] > 10)
            {
                meltingField[x, y - 1] = -999;
                return true;
            }

            return false;
        }

        private static bool IsNullRightUpCell(int x, int y)
        {
            if (x + 1 > 6 || y - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y - 1] < 999 && meltingField[x + 1, y - 1] > 10)
            {
                meltingField[x + 1, y - 1] = -999;
                return true;
            }

            return false;
        }

        private static bool IsNullRightCell(int x, int y)
        {
            if (x + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y] < 999 && meltingField[x + 1, y] > 10)
            {
                meltingField[x + 1, y - 1] = -999;
                return true;
            }

            return false;
        }

        private static bool IsNullRightDownCell(int x, int y)
        {
            if (x + 1 > 6 || y + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x + 1, y + 1] < 999 && meltingField[x + 1, y + 1] > 10)
            {
                meltingField[x + 1, y + 1] = -999;
                return true;
            }

            return false;
        }

        private static bool IsNullDownCell(int x, int y)
        {
            if (y + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x, y + 1] < 999 && meltingField[x, y + 1] > 10)
            {
                meltingField[x, y + 1] = -999;
                return true;
            }

            return false;
        }

        private static bool IsNullLeftDownCell(int x, int y)
        {
            if (x - 1 < 0 || y + 1 > 6)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y + 1] < 999 && meltingField[x - 1, y + 1] > 10)
            {
                meltingField[x - 1, y + 1] = -999;
                return true;
            }

            return false;
        }

        private static bool IsNullLeftCell(int x, int y)
        {
            if (x - 1 < 0)
                return false;
            //если в ячейке число и в левой верхней  999, то мутируем и выкидуем тру
            if (meltingField[x, y] < 12 && meltingField[x, y] > -1 && meltingField[x - 1, y] < 999 && meltingField[x - 1, y] > 10)
            {
                meltingField[x - 1, y] = -999;
                return true;
            }

            return false;
        }

        #endregion
    }

    public class Melting
    {
        int number;
        double percentage;
        int state;
    }
}
