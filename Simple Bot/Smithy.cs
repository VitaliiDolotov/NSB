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

        public static Melting[,] meltingPlayField = new Melting[6, 6];

        public static void FirstInit()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    meltingPlayField[i, j] = new Melting();
        }

        public static void InitMeltingField()
        {
            //проинициализировали поле числами
            for (int j = 0; j < 6; j++)
                for (int i = 0; i < 6; i++)
                {
                    int cellValue = CellNumberGeter(i, j);
                    if (cellValue == -1)
                        meltingPlayField[i, j].state = 0;
                    else
                    {
                        meltingPlayField[i, j].number = cellValue;
                        meltingPlayField[i, j].state = -2;
                    }
                }
        }

        public static void InitPercentages()
        {
            for (int j = 0; j < 6; j++)
                for (int i = 0; i < 6; i++)
                {
                    if (meltingPlayField[i, j].state == 0)
                    {
                        SetPercentageLeftUpCell(i, j);
                        SetPercentageUpCell(i, j);
                        SetPercentageRightUpCell(i, j);
                        SetPercentageRightCell(i, j);
                        SetPercentageRightDownCell(i, j);
                        SetPercentageDownCell(i, j);
                        SetPercentageLeftDownCell(i, j);
                        SetPercentageLeftCell(i, j);
                    }
                }
        }

        public static void InitNumbers()
        {
            for (int j = 0; j < 6; j++)
                for (int i = 0; i < 6; i++)
                {
                    if (meltingPlayField[i, j].state == -2)
                    {
                        // выход
                        if (meltingPlayField[i, j].exits == meltingPlayField[i, j].number)
                        {
                            if (meltingPlayField[i, j].Lu)
                                if (!(i - 1 < 0 || j - 1 < 0))
                                    meltingPlayField[i - 1, j - 1].state = 1;

                            if(meltingPlayField[i, j].U)
                                if(!(i - 1 < 0))
                                    meltingPlayField[i - 1, j].state = 1;

                            if(meltingPlayField[i, j].Ru)
                                if(!(i - 1 < 0 || j + 1 > 5))
                                    meltingPlayField[i - 1, j + 1].state = 1;

                            if(meltingPlayField[i, j].R)
                                if(!(j + 1 > 5))
                                    meltingPlayField[i, j + 1].state = 1;

                            if(meltingPlayField[i, j].Rd)
                                if(!(i + 1 > 5 || j + 1 > 5))
                                    meltingPlayField[i + 1, j + 1].state = 1;

                            if(meltingPlayField[i, j].D)
                                if(!(i + 1 > 5))
                                    meltingPlayField[i + 1, j].state = 1;

                            if(meltingPlayField[i, j].Ld)
                                if(!(i + 1 > 5 ||j - 1 < 0))
                                    meltingPlayField[i + 1, j - 1].state = 1;


                            if (meltingPlayField[i, j].L)
                                if (!(j - 1 < 0))
                                    meltingPlayField[i, j - 1].state = 1;
                        }

                        IsEmptyLeftUpCellN(i, j);
                        IsEmptyUpCellN(i, j);
                        IsEmptyRightUpCellN(i, j);
                        IsEmptyRightCellN(i, j);
                        IsEmptyRightDownCellN(i, j);
                        IsEmptyDownCellN(i, j);
                        IsEmptyLeftDownCellN(i, j);
                        IsEmptyLeftCellN(i, j);

                        //если после переинициализации число стало ноль то кликаем по всем рядошним ячейкам
                        if (meltingPlayField[i, j].number == 0)
                        {
                            if (meltingPlayField[i, j].Lu)
                                if (!(i - 1 < 0 || j - 1 < 0))
                                    ClickCell(i - 1, j - 1);

                            if (meltingPlayField[i, j].U)
                                if (!(i - 1 < 0))
                                    ClickCell(i - 1, j);

                            if (meltingPlayField[i, j].Ru)
                                if (!(i - 1 < 0 || j + 1 > 5))
                                    ClickCell(i - 1, j + 1);

                            if (meltingPlayField[i, j].R)
                                if (!(j + 1 > 5))
                                    ClickCell(i, j + 1);

                            if (meltingPlayField[i, j].Rd)
                                if (!(i + 1 > 5 || j + 1 > 5))
                                    ClickCell(i + 1, j + 1);

                            if (meltingPlayField[i, j].D)
                                if (!(i + 1 > 5))
                                    ClickCell(i + 1, j);

                            if (meltingPlayField[i, j].Ld)
                                if (!(i + 1 > 5 || j - 1 < 0))
                                    ClickCell(i + 1, j - 1);


                            if (meltingPlayField[i, j].L)
                                if (!(j - 1 < 0))
                                    ClickCell(i, j - 1);
                        }

                        InitMeltingField();
                    }
                }
        }

        #region GetValueForCell

        private static void SetPercentageLeftUpCell(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0)
                return;
            if (meltingPlayField[x - 1, y - 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x - 1, y - 1);

        }

        private static void SetPercentageUpCell(int x, int y)
        {
            if (x - 1 < 0)
                return;
            if (meltingPlayField[x - 1, y].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x - 1, y);
        }

        private static void SetPercentageRightUpCell(int x, int y)
        {
            if (x - 1 < 0 || y + 1 > 5)
                return;
            if (meltingPlayField[x - 1, y + 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x - 1, y + 1);

        }

        private static void SetPercentageRightCell(int x, int y)
        {
            if (y + 1 > 5)
                return;
            if (meltingPlayField[x, y + 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x, y + 1);
        }

        private static void SetPercentageRightDownCell(int x, int y)
        {
            if (x + 1 > 5 || y + 1 > 5)
                return;
            if (meltingPlayField[x + 1, y + 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x + 1, y + 1);
        }

        private static void SetPercentageDownCell(int x, int y)
        {
            if (x + 1 > 5)
                return;
            if (meltingPlayField[x + 1, y].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x + 1, y);
        }

        private static void SetPercentageLeftDownCell(int x, int y)
        {
            if (x + 1 > 5 || y - 1 < 0)
                return;
            if (meltingPlayField[x + 1, y - 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x + 1, y - 1);
        }

        private static void SetPercentageLeftCell(int x, int y)
        {
            if (y - 1 < 0)
                return;
            if (meltingPlayField[x, y - 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x, y - 1);
        }

        #endregion

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

        private static void ClickCell(int x, int y)
        {
            string selector = string.Format(".game_field  tr:nth-of-type({0}) td:nth-of-type({1})", x + 1, y + 1);
            driver.FindElement(By.CssSelector(selector)).Click();
        }

        //выходы
        public static void Exit(int x, int y)
        {

        }

        #region GetValueForCell

        private static void IsEmptyLeftUpCell(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0)
                return;
            if (meltingPlayField[x - 1, y - 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x - 1, y - 1);

        }

        private static void IsEmptyUpCell(int x, int y)
        {
            if (x - 1 < 0)
                return;
            if (meltingPlayField[x - 1, y].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x - 1, y);
        }

        private static void IsEmptyRightUpCell(int x, int y)
        {
            if (x - 1 < 0 || y + 1 > 5)
                return;
            if (meltingPlayField[x - 1, y + 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x - 1, y + 1);

        }

        private static void IsEmptyRightCell(int x, int y)
        {
            if (y + 1 > 5)
                return;
            if (meltingPlayField[x, y + 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x, y + 1);
        }

        private static void IsEmptyRightDownCell(int x, int y)
        {
            if (x + 1 > 5 || y + 1 > 5)
                return;
            if (meltingPlayField[x + 1, y + 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x + 1, y + 1);
        }

        private static void IsEmptyDownCell(int x, int y)
        {
            if (x + 1 > 5)
                return;
            if (meltingPlayField[x + 1, y].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x + 1, y);
        }

        private static void IsEmptyLeftDownCell(int x, int y)
        {
            if (x + 1 > 5 || y - 1 < 0)
                return;
            if (meltingPlayField[x + 1, y - 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x + 1, y - 1);
        }

        private static void IsEmptyLeftCell(int x, int y)
        {
            if (y - 1 < 0)
                return;
            if (meltingPlayField[x, y - 1].number > -1)
                meltingPlayField[x, y].percentage += NewCellPercentage(x, y - 1);
        }

        #endregion

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

        private static double NewCellPercentage(int x, int y)
        {
            double retv = 100 * meltingPlayField[x, y].number / 8;
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

        #region IsCellEmptyN

        private static void IsEmptyLeftUpCellN(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0)
                return;
            if (meltingPlayField[x - 1, y - 1].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x - 1, y - 1].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].Lu = true;
            }
        }

        private static void IsEmptyUpCellN(int x, int y)
        {
            if (x - 1 < 0)
                return;
            if (meltingPlayField[x - 1, y].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x - 1, y].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].U = true;
            }
        }

        private static void IsEmptyRightUpCellN(int x, int y)
        {
            if (x - 1 < 0 || y + 1 > 5)
                return;
            if (meltingPlayField[x - 1, y + 1].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x - 1, y + 1].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].Ru = true;
            }
        }

        private static void IsEmptyRightCellN(int x, int y)
        {
            if (y + 1 > 5)
                return;
            if (meltingPlayField[x, y + 1].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x, y + 1].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].R = true;
            }
        }

        private static void IsEmptyRightDownCellN(int x, int y)
        {
            if (x + 1 > 5 || y + 1 > 5)
                return;
            if (meltingPlayField[x + 1, y + 1].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x + 1, y + 1].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].Rd = true;
            }
        }

        private static void IsEmptyDownCellN(int x, int y)
        {
            if (x + 1 > 5)
                return;
            if (meltingPlayField[x + 1, y].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x + 1, y].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].D = true;
            }
        }

        private static void IsEmptyLeftDownCellN(int x, int y)
        {
            if (x + 1 > 5 || y - 1 < 0)
                return;
            if (meltingPlayField[x + 1, y - 1].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x + 1, y - 1].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].Ld = true;
            }
        }

        private static void IsEmptyLeftCellN(int x, int y)
        {
            if (y - 1 < 0)
                return;
            if (meltingPlayField[x, y - 1].state == 1)
                meltingPlayField[x, y].number--;
            if (meltingPlayField[x, y - 1].state == -2)
            {
                meltingPlayField[x, y].exits--;
                meltingPlayField[x, y].L = true;
            }
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
        public int number;
        public double percentage;
        public int state;
        public int exits;

        public bool Lu = false;
        public bool U = false;
        public bool Ru = false;
        public bool R = false;
        public bool Rd = false;
        public bool D = false;
        public bool Ld = false;
        public bool L = false;

        public Melting()
        {
            number = -200;
            percentage = 0;
            state = 0;
            exits = 8;
        }
    }
}
