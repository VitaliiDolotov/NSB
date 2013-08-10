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
        public static IWebDriver driver;

        public static double[,] meltingField = new double[6, 6];

        public Smithy(IWebDriver Driver)
        {
            driver = Driver;
        }

        public void InitMeltingField()
        {
            //проинициализировали поле числами
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    //Если ячейка пустая или содержит процент
                    if (meltingField[i, j] < 12)
                        meltingField[i, j] = CellNumberGeter(i, j);
                }
        }

        //вытягиваем число из ячейки
        private int CellNumberGeter(int x, int y)
        {
            string selector = string.Format(".game_field  tr:nth-of-type({0}) td:nth-of-type({1})", x, y);
            IWebElement cell = driver.FindElement(By.CssSelector(selector));
            string classAtribute = cell.GetAttribute("class");
            //Если строка не пустая, то возвращаем число, лижащее в ячейке, если пустая, то возвращаем -1
            if (string.IsNullOrEmpty(classAtribute))
            {
                return Convert.ToInt32(classAtribute.Remove(0, 4));
            }
            else return -1;
        }

        //записываем проценты
        private void SetPercentage()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (meltingField[i, j] == 0)
                    {

                    }
                }
        }

        private double CellPercentage(int x, int y)
        {
            return 8 / 100 * meltingField[x, y];
        }

        #region SetCellValue

        private void LeftUpCell(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] > 10 && meltingField[x, y] < 999 && meltingField[x - 1, y - 1] > 10 && meltingField[x - 1, y - 1] < 999)
                meltingField[x - 1, y - 1] += CellPercentage(x, y);
        }

        private void UpCell(int x, int y)
        {
            if (y - 1 < 0)
                return;
            //если в ячейке число и в верхней проценты но меньше 999
            if (meltingField[x, y] > 10 && meltingField[x, y] < 999 && meltingField[x, y - 1] > 10 && meltingField[x, y - 1] < 999)
                meltingField[x, y - 1] += CellPercentage(x, y);
        }

        private void RightUpCell(int x, int y)
        {
            if (x + 1 < 0 || y - 1 < 6)
                return;
            //если в ячейке число и в верхней проценты но меньше 999
            if (meltingField[x, y] > 10 && meltingField[x, y] < 999 && meltingField[x + 1, y - 1] > 10 && meltingField[x + 1, y - 1] < 999)
                meltingField[x + 1, y - 1] += CellPercentage(x, y);
        }

        private void LeftCell(int x, int y)
        {
            if (x + 1 < 0)
                return;
            //если в ячейке число и в левой верхней проценты но меньше 999
            if (meltingField[x, y] > 10 && meltingField[x, y] < 999 && meltingField[x - 1, y - 1] > 10 && meltingField[x - 1, y - 1] < 999)
                meltingField[x - 1, y - 1] += CellPercentage(x, y);
        }

        #endregion
    }
}
