using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Media;
using System.Diagnostics;               // For prcesss related information
using System.Runtime.InteropServices;   // For DLL importing 

namespace Simple_Bot
{
    public class BotvaClass
    {
        private const int SW_HIDE = 0;

        private const int SW_RESTORE = 9;

        private int hWnd;

        string oldBotvaWindow;
        string newBotvaWindow;

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        string CurrentGold = "0";
        Int64 maxStatCost = 0;


        int Delay1 = 0;
        int Delay2 = 0;
        int Delay3 = 0;
        int Delay4 = 0;

        int TimeOutValue = 1;

        bool CanMakeCryDust = false;
        bool CanMakeSoap = false;

        string SettingsFile = "settings.bin";
        string LoggFile = "Log.txt";

        static string power = "0", block = "0", endurance = "0", dexterity = "0", charisma = "0";

        List<DateTime> PendingTimes = new List<DateTime>();

        static string PageSource = "";

        static int PandaCount = 0;

        //ADV
        bool AdvIsOpened = false;

        static DateTime Timer_MineWork = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_CryDustMaking = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Fishing = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_PotionBoil = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_CryStiring = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_TanksMaking = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Underground = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_WorkPending = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        static DateTime TP_Underground = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        static DateTime Timer_FightMonster = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_FightZorro = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_FightCommon = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_SoapMaking = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_FightImmunOgl = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_FightImmunAnti = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Relogin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_MassAbil = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_VillageManager = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Shop = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_DayliGifts = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_DrinkOborotka = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_ForestFarmer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_BiggestPotion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Arena = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_MassFight = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);

        static DateTime Timer_Grif = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Mont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Drac = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Peg = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static IList<DateTime> Timer_Fly = new DateTime[4] { Timer_Grif, Timer_Mont, Timer_Drac, Timer_Peg };

        static DateTime Timer_PandaEffect1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_PandaEffect2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_PandaEffect3 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_PandaEffect4 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_TFEvery = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_TFDuring = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);

        static DateTime Timer_FarCountrys = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);

        static DateTime Timer_NestReminder = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);

        static DateTime Timer_AdvTimer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_OpenMySite = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);



        IWebDriver driver;

        Random rnd = new Random();

        private void Logger(string logerData)
        {
            string text = string.Empty;
            if (File.Exists(LoggFile))
            {
                var reader = new StreamReader(File.OpenRead(LoggFile));
                text = reader.ReadToEnd();
                reader.Close();
            }

            //удаялем старый файл и записуем старое значение
            File.Delete(LoggFile);
            var writer = new StreamWriter(File.OpenWrite(LoggFile));
            writer.WriteLine(text + DateTime.Now + " " + logerData);
            writer.Close();
        }

        public void Hide()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[15]) == true)
            {
                Process[] processRunning = Process.GetProcesses();
                foreach (Process pr in processRunning)
                {
                    if (pr.ProcessName.Contains("chrome") && pr.MainWindowTitle.Contains("Ботва") && oldBotvaWindow != pr.MainWindowHandle.ToString())
                    {
                        newBotvaWindow = pr.MainWindowHandle.ToString();
                        hWnd = pr.MainWindowHandle.ToInt32();
                        ShowWindow(hWnd, SW_HIDE);
                    }
                }
            }
        }

        public void ReHideWindow()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[15]) == true)
            {
                Process[] processRunning = Process.GetProcesses();
                foreach (Process pr in processRunning)
                {
                    if (newBotvaWindow == pr.MainWindowHandle.ToString())
                    {
                        hWnd = pr.MainWindowHandle.ToInt32();
                        ShowWindow(hWnd, SW_HIDE);
                        break;
                    }
                }
            }
        }

        private void OldBotvaWindow()
        {
            try
            {
                Process[] processRunning = Process.GetProcesses();
                foreach (Process pr in processRunning)
                {
                    if (pr.ProcessName.Contains("chrome") && pr.MainWindowTitle.Contains("Ботва"))
                    {
                        oldBotvaWindow = pr.MainWindowHandle.ToString();
                    }
                }
            }
            catch { }
        }

        public void EnvironmentSetUp()
        {
            MineSetUp();
            MineInventorybySetUp();
            CryDustMakingSetUp();
            SoapMakingSetUp();
            BigSmallFieldsSetUp();
            FishingSetUp();
            PotionMakingSetUp();
            StutsUpSetUp();
            FlySetUp();
            UndergroundSetUp();
            FightEffectsSetUp();
            NegativeEffectsSetUp();
            PandaEffectsSetUp();
            FarCountrysSetUp();
            MassAbilitysSetUp();
            VillageSetUp();
            BySlavesSetUp();
            BiggestPotionSetUp();
            PageSource = driver.PageSource;
        }

        private void BiggestPotionSetUp()
        {
            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[29]))
                {
                    AddingCategory("Счётчики");
                    AddingItemToTheCategory_Timers("Счётчики", "Ваши зверушки увеличены в размере и не могут быть проглочены Китушей", "Зелье огромности");
                    Timer_BiggestPotion = ToDateTime(GetResourceValue("Ваши зверушки увеличены в размере и не могут быть проглочены Китушей")[0]);
                }
            }
            catch { }
        }

        private void VillageSetUp()
        {
            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[12]) == true)
                {
                    AddingCategory("Ресурсы");
                    AddingItemToTheCategory_Timers("Счётчики", "Время работы заведующего на ферме", "Заведующий фермы");
                    Timer_VillageManager = ToDateTime(GetResourceValue("Время работы заведующего на ферме")[0]);
                }
            }
            catch { }
        }

        private void MineSetUp()
        {
            if (ReadFromFile(SettingsFile, "MineBox")[1] == "True")
            {
                //вытягиваем панель быстрого старта
                PanelForFastAccess();
                //вытягиваем иконку шахты
                IconsAdding("f43");
            }
        }

        private void BySlavesSetUp()
        {
            try
            {
                int slavesCount = Convert.ToInt32(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[8]);
                AddingItemToTheCategory_Resource("Ресурсы", "i70", "Количество занятых мест на плантации");
            }
            catch { }
        }

        private void MineInventorybySetUp()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MineBox")[10]) == true)
            {
                AddingCategory("Ресурсы");
                //ByCommon
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MineBox")[8]) == true)
                {
                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MineBox")[5]) == true)
                    {
                        AddingItemToTheCategory_Resource("Ресурсы", "i16", "Кирка");
                    }
                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MineBox")[6]) == true)
                    {
                        AddingItemToTheCategory_Resource("Ресурсы", "i18", "Каска");
                    }
                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MineBox")[7]) == true)
                    {
                        AddingItemToTheCategory_Resource("Ресурсы", "i17", "Очки");
                    }
                }
            }
        }

        private void UndergroundSetUp()
        {
            //если маркнут чекбокс для подзема
            if (ReadFromFile(SettingsFile, "UndergroundBox")[1] == "True")
            {
                AddingCategory("Счётчики");
                AddingCategory("Ресурсы");
                AddingItemToTheCategory_Resource("Ресурсы", "i35", "Ключ от ворот царства Манаглота");
                AddingItemToTheCategory_Timers("Счётчики", "Время до похода в подземелье", "До похода в подземелье");
                AddingItemToTheCategory_Resource("Ресурсы", "i36", "Ящик Пандоры");
                Timer_Underground = ToDateTime(GetResourceValue("Время до похода в подземелье")[0]);
                PandaCount = Convert.ToInt32(GetResourceValue("title='Ящик Пандоры'")[0]);
            }
        }

        private void CryDustMakingSetUp()
        {
            if (ReadFromFile(SettingsFile, "AdditionalSettingsBox")[1] == "True")
            {
                AddingCategory("Счётчики");
                AddingCategory("Ресурсы");
                AddingItemToTheCategory_Timers("Счётчики", "Перемолка Кристальной Пыли.", "Кристальная Пыль");
                AddingItemToTheCategory_Resource("Ресурсы", "i56", "Кристальная пыль");
                CanMakeCryDust = IconsAdding("f58");
            }
        }

        private void SoapMakingSetUp()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[4]) == true)
            {
                AddingCategory("Счётчики");
                AddingCategory("Ресурсы");
                AddingItemToTheCategory_Timers("Счётчики", "Добыча Мыльного Камня.", "Мыльный Камень");
                AddingItemToTheCategory_Resource("Ресурсы", "i57", "Мыльный камень");
                CanMakeSoap = IconsAdding("f57");
            }
        }

        private void BigSmallFieldsSetUp()
        {
            if (Convert.ToInt32(ReadFromFile(SettingsFile, "MineBox")[3]) != 101 || Convert.ToInt32(ReadFromFile(SettingsFile, "MineBox")[4]) != 101)
            {
                AddingCategory("Ресурсы");
                AddingItemToTheCategory_Resource("Ресурсы", "i34", "Билетов на большую поляну");
                AddingItemToTheCategory_Resource("Ресурсы", "i33", "Билетов на маленькую поляну");
            }

        }

        private void FishingSetUp()
        {
            if (ReadFromFile(SettingsFile, "AdditionalSettingsBox")[2] == "True")
            {
                //вытягиваем панель быстрого старта
                PanelForFastAccess();
                //вытягиваем иконку причала
                IconsAdding("f35");
                AddingCategory("Счётчики");
                AddingCategory("Ресурсы");
                AddingItemToTheCategory_Resource("Ресурсы", "i39", "Количество походов за пирашками на сегодня");
                AddingItemToTheCategory_Timers("Счётчики", "Время до возвращения судна с пирашками", "Судно с пирашками");
            }
        }

        private void FlySetUp()
        {
            if (ReadFromFile(SettingsFile, "AdditionalSettingsBox")[3] == "True")
            {
                //вытягиваем секцию летунов
                AddingCategory("Инкубатор");
            }
        }

        private void PotionMakingSetUp()
        {
            if (ReadFromFile(SettingsFile, "PotionMakingBox")[1] == "True")
            {
                //вытягиваем панель быстрого старта
                PanelForFastAccess();
                //вытягиваем иконку простейших зелий
                IconsAdding("f60");
                AddingItemToTheCategory_Timers("Счётчики", "Следующее помешивание зелья.", "Помешивание");
                AddingItemToTheCategory_Timers("Счётчики", "Варка зелья.", "Варка зелья");
            }
            if (ReadFromFile(SettingsFile, "PotionMakingBox")[2] == "True")
            {
                //вытягиваем иконку лаборатории
                IconsAdding("f59");
                AddingItemToTheCategory_Resource("Ресурсы", "i58", "Раскаленное стекло");
                AddingItemToTheCategory_Resource("Ресурсы", "i59", "Стеклянная тара");
            }
        }

        private void StutsUpSetUp()
        {
            if (ReadFromFile(SettingsFile, "StutsUpBox")[1] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[2] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[3] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[4] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[5] == "True")
            {
                //вытягиваем панель быстрого старта
                PanelForFastAccess();
                //вытягиваем иконку личного тренера
                System.Threading.Thread.Sleep(1000);
                IconsAdding("f38");
            }
        }

        private void FightEffectsSetUp()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[10]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[11]) == true)
            {
                // Счётчики
                AddingCategory("Счётчики");
                //вытягиваем иммуны Антикрута и оглушки
                if (PageSource.Contains("Антикрут") == false)
                {
                    AddingItemToTheCategory_Timers("Счётчики", "Иммунитет «Антикрут»", "Иммунитет «Антикрут»");
                }
                System.Threading.Thread.Sleep(875);
                if (PageSource.Contains("Оглушка") == false)
                {
                    AddingItemToTheCategory_Timers("Счётчики", "Иммунитет «Оглушка»", "Иммунитет «Оглушка»");
                }
            }

        }

        private void NegativeEffectsSetUp()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "EffectsBox")[1]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "EffectsBox")[2]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "EffectsBox")[3]) == true)
            {
                //вытягиваем секцию эффектов
                AddingCategory("Эффекты");
            }
        }

        private void PandaEffectsSetUp()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[1]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[2]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[3]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[4]) == true)
            {
                AddingCategory("Эффекты");
            }
        }

        private void FarCountrysSetUp()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FarCountrBox")[1]) == true)
            {
                IconsAdding("f9");
                AddingCategory("Счётчики");
                AddingCategory("Ресурсы");
                AddingItemToTheCategory_Resource("Ресурсы", "i38", "Ртутный порошок");
                AddingItemToTheCategory_Timers("Счётчики", "Гильдийный корабль", "Гильдийный корабль");
                Timer_FarCountrys = ToDateTime(GetResourceValue("Время до возвращения корабля из дальних стран.")[0]);
            }
        }

        private void MassAbilitysSetUp()
        {
            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassAbilityBox")[9]) == true)
                {
                    AddingCategory("Счётчики");
                    AddingItemToTheCategory_Timers("Счётчики", "Время до окончания тренировки массового скилла.", "Тренировка");
                    Timer_MassAbil = ToDateTime(GetResourceValue("Время до окончания тренировки массового скилла.")[0]);
                }
            }
            catch { }
        }

        //private bool CheckElementExist(IWebElement element)
        //{
        //    try
        //    {
        //        Assert.IsNotNull(element);
        //        return true;
        //    }
        //    catch (AssertionException)
        //    {
        //        return false;
        //    }
        //}

        private void WaitForElementAndClick(IWebElement element, int waitInMiliSeconds)
        {
            int delay = 0;
            do
            {
                try
                {
                    element.Click();
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(300);
                    delay += 300;
                }
            }
            while (delay < waitInMiliSeconds);
        }

        //private void ClickWhilePresent(IWebElement element)
        //{
        //    do
        //    {
        //        try
        //        {
        //            element.Click();
        //        }
        //        catch { }
        //    }
        //    while (CheckElementExist(element));
        //}

        private DateTime ToDateTime(string CounterTime)
        {
            char Separator = ':';
            string[] SeparatedTime;
            SeparatedTime = CounterTime.Split(Separator);
            DateTime ReturnTime = DateTime.Now;
            TimeSpan TimeToAdd = new TimeSpan(0, Convert.ToInt32(SeparatedTime[0]), Convert.ToInt32(SeparatedTime[1]), Convert.ToInt32(SeparatedTime[2]));
            ReturnTime = DateTime.Now.Add(TimeToAdd);
            return ReturnTime;
        }

        private void CompareValuesInFile(string Boxname, string[] ValuesFromForm)
        {
            string[] valuesFromFile = ReadFromFile(SettingsFile, Boxname);
            //если в файле значение нул или пустое или файла вообще нет
            if ((valuesFromFile[0] == null || valuesFromFile[0] == "") || File.Exists(SettingsFile) == false)
            {
                WreateToFile(SettingsFile, Boxname, ValuesFromForm);
            }
            else
            {
                for (int i = 0; i < ValuesFromForm.Length; i++)
                {
                    if (ValuesFromForm[i] != valuesFromFile[i + 1])
                    {
                        WreateToFile(SettingsFile, Boxname, ValuesFromForm);
                        break;
                    }
                }
            }
        }

        private string[] ReadFromFile(string FileName, string RowName)
        {
            string[] RetRow = { "NULL" };
            if (File.Exists(FileName) == true)
            {
                var reader = new StreamReader(File.OpenRead(FileName));
                string[] Rows = reader.ReadLine().Split(';');
                for (int i = 0; i < Rows.Length; i++)
                {
                    RetRow = Rows[i].Split(',');
                    if (RetRow[0] == RowName)
                    {
                        break;
                    }
                }
                reader.Close();
            }
            return RetRow;
        }

        private void WreateToFile(string FileName, string RowName, string[] Values)
        {
            bool Flag = false;
            //если файл уже есть то читаем его содержимое и удаляем чтоб заменить новым
            if (File.Exists(FileName) == true)
            {
                var reader = new StreamReader(File.OpenRead(FileName));
                string[] Rows = reader.ReadLine().Split(';');
                reader.Close();
                string[,] temp_mass = new string[100, 100];
                for (int count1 = 0; count1 < Rows.Length; count1++)
                {
                    string[] temp_row = Rows[count1].Split(',');
                    for (int count2 = 0; count2 < temp_row.Length; count2++)
                    {

                        //если первый элемент массива равен названию бокса
                        if (temp_row[count2] == RowName)
                        {
                            for (int count3 = 0; count3 < Values.Length + 1; count3++)
                            {
                                if (count3 == 0)
                                {
                                    temp_mass[count1, count3] = RowName;
                                }
                                else
                                {
                                    temp_mass[count1, count3] = Values[count3 - 1];
                                }
                            }
                            Flag = true;
                            break;
                        }
                        temp_mass[count1, count2] = temp_row[count2];
                    }
                }
                //если пришел новый рядок и он начинается с null, то вливаем в него нужный нам массив + если флаг в false - типо не нашли нужный рядок и нужно вливать нвоый
                if (Flag == false)
                {
                    for (int count3 = 0; count3 < Values.Length + 1; count3++)
                    {
                        //первому элементу присваеваем значение строки
                        if (count3 == 0)
                        {
                            temp_mass[Rows.Length + 1, count3] = RowName;
                        }
                        else
                        {
                            temp_mass[Rows.Length + 1, count3] = Values[count3 - 1];
                        }
                    }
                }
                //удаялем старый файл и записуем темповский массив в новый файл
                File.Delete(FileName);
                var writer = new StreamWriter(File.OpenWrite(FileName));
                for (int i = 0; i < 100; i++)
                {
                    for (int y = 0; y < 100; y++)
                    {
                        try
                        {
                            if (temp_mass[i, y] != null)
                            {
                                writer.Write(temp_mass[i, y]);
                                //если след элемент пустой,а предыдущий имел значение, то ставим точку с запятой
                                if (temp_mass[i, y + 1] == null && temp_mass[i, y - 1] != null)
                                {
                                    writer.Write(';');
                                }
                                //еслинет, то просто запятую
                                else
                                {
                                    writer.Write(',');
                                }
                            }
                        }
                        catch { }
                    }
                }
                writer.Close();
            }
            else
            {
                var writer = new StreamWriter(File.OpenWrite(FileName));
                for (int i = 0; i < Values.Length + 1; i++)
                {
                    if (i == 0)
                    {
                        writer.Write(RowName);
                    }
                    else
                    {
                        writer.Write(Values[i - 1]);
                    }
                    if (i == Values.Length)
                    {
                        writer.Write(";");
                    }
                    else writer.Write(",");
                }
                writer.Close();
            }
        }

        public BotvaClass()
        {
            try
            {
                OldBotvaWindow();
                driver = Login(ReadFromFile(SettingsFile, "LoginBox")[1], ReadFromFile(SettingsFile, "LoginBox")[2], ReadFromFile(SettingsFile, "LoginBox")[3], ReadFromFile(SettingsFile, "LoginBox")[4], Convert.ToBoolean(ReadFromFile(SettingsFile, "LoginBox")[5]));
                Hide();
            }
            catch { }
        }

        public IWebDriver Login(string server, string Log, string Pas, string DriverType, bool MailRuLogin)
        {
            IWebDriver driver = DriverCreation(DriverType);

            try
            {

                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
                string ServerValueXPath;
                switch (server)
                {
                    case "Сервер Адын":
                        ServerValueXPath = ".//option[@value='1']";
                        break;
                    case "Сервер Дыдва":
                        ServerValueXPath = ".//option[@value='2']";
                        break;
                    case "Сервер Тытра":
                        ServerValueXPath = ".//option[@value='3']";
                        break;
                    case "Сервер Turbo":
                        ServerValueXPath = ".//option[@value='4']";
                        break;
                    default:
                        ServerValueXPath = ".//option[@value='1']";
                        break;
                }

                //LOGIN
                //Default login    
                try
                {
                    if (MailRuLogin == false)
                    {
                        driver.Navigate().GoToUrl("http://www.botva.ru/");
                        driver.FindElement(By.Id("server")).FindElement(By.XPath(ServerValueXPath)).Click();
                        driver.FindElement(By.Id("loginForm")).FindElement(By.XPath(".//input[2]")).SendKeys(Log);
                        driver.FindElement(By.Id("loginForm")).FindElement(By.XPath(".//input[3]")).SendKeys(Pas);
                        driver.FindElement(By.Id("loginForm")).FindElement(By.XPath(".//input[5]")).Click();
                    }
                    else
                    {
                        //mail.ru login
                        driver.Navigate().GoToUrl("http://botva.mail.ru/");
                        driver.FindElement(By.Id("server")).FindElement(By.XPath(ServerValueXPath)).Click();
                        driver.FindElement(By.Id("loginForm")).FindElement(By.XPath(".//table/tbody/tr/td[1]/input")).SendKeys(Log);
                        driver.FindElement(By.Id("loginForm")).FindElement(By.XPath(".//input[2]")).SendKeys(Pas);
                        driver.FindElement(By.Id("loginForm")).FindElement(By.XPath(".//input[4]")).Click();
                    }
                }
                catch { }

                PageSource = driver.PageSource;

                AdvTimerAssigne();

                Delay1 = 600;
                Delay2 = 1109;
                Delay3 = 300;
                Delay4 = 698;

                Timer_Relogin = ToDateTime("00:09:20");

                string TFEvery = ReadFromFile(SettingsFile, "AdditionalSettingsBox")[32];
                if (TFEvery.Length == 1)
                    TFEvery = "0" + TFEvery;
                Timer_TFEvery = ToDateTime(string.Format("00:{0}:15", TFEvery));

                //асайнем таймер сбытни
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "ShopBox")[1]) == true)
                {
                    string timer = string.Format("00:{0}:{1}", Convert.ToString(ReadFromFile(SettingsFile, "ShopBox")[7]), rnd.Next(10, 55));
                    Timer_Shop = ToDateTime(timer);//ToDateTime("00:00:02");
                }
            }
            catch { }

            return driver;
        }

        private void Delays()
        {
            System.Threading.Thread.Sleep(rnd.Next(Delay1, Delay2));
        }

        private void SmallDelays()
        {
            System.Threading.Thread.Sleep(rnd.Next(Delay3, Delay4));
        }

        private IWebDriver DriverCreation(string DrType)
        {
            if (DrType == "Chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-extensions");
                IWebDriver driver = new ChromeDriver(options);

                //Hide chromedriver Window
                Process[] processRunning = Process.GetProcesses();
                foreach (Process pr in processRunning)
                {
                    if (pr.ProcessName.Contains("chromedriver"))
                    {
                        hWnd = pr.MainWindowHandle.ToInt32();
                        ShowWindow(hWnd, SW_HIDE);
                        break;
                    }
                }
                Timer_OpenMySite = ToDateTime("00:53:30");
                return driver;
            }
            else
            {
                IWebDriver driver = new FirefoxDriver();
                Timer_OpenMySite = ToDateTime("00:53:30");
                return driver;
            }
        }

        public void ReLogIn()
        {
            if (Timer_Relogin.CompareTo(DateTime.Now) < 0)
            {
                Timer_Relogin = ToDateTime("00:09:23");
                try
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
                    driver.Navigate().Refresh();
                    try
                    {
                        System.Threading.Thread.Sleep(3000);
                        IWebElement test = driver.FindElement(By.XPath("//a[@id='news_link'][text()='Новости']"));
                    }
                    catch
                    {
                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "LoginBox")[5]) == false)
                        {
                            driver.Navigate().GoToUrl("http://www.botva.ru/");
                        }
                        else
                        {
                            driver.Navigate().GoToUrl("http://botva.mail.ru/");
                        }
                    }
                }
                catch { }

                try
                {
                    int iterator = 0;
                    //если нет нашего тайтла или линки в футере то релогинемся
                    while (driver.Title.Contains("Ботва Онлайн | Битва за реальную капусту!") == false || driver.FindElement(By.XPath("//a[@id='news_link'][text()='Новости']")).Displayed == false)
                    {
                        if (iterator != 0)
                        {
                            System.Threading.Thread.Sleep(540000);
                        }
                        iterator++;
                        this.driver.Quit();
                        this.driver = Login(ReadFromFile(SettingsFile, "LoginBox")[1], ReadFromFile(SettingsFile, "LoginBox")[2], ReadFromFile(SettingsFile, "LoginBox")[3], ReadFromFile(SettingsFile, "LoginBox")[4], Convert.ToBoolean(ReadFromFile(SettingsFile, "LoginBox")[5]));
                    }
                }
                catch
                {
                    this.driver.Quit();
                    this.driver = Login(ReadFromFile(SettingsFile, "LoginBox")[1], ReadFromFile(SettingsFile, "LoginBox")[2], ReadFromFile(SettingsFile, "LoginBox")[3], ReadFromFile(SettingsFile, "LoginBox")[4], Convert.ToBoolean(ReadFromFile(SettingsFile, "LoginBox")[5]));
                }
                ReHideWindow();
            }
        }

        public void MineGoWork()
        {
            try
            {
                //если чекбокс "Работать в шахте" равен тру, то можем работать в шахте
                if (ReadFromFile(SettingsFile, "MineBox")[1] == "True")
                {
                    bool CahrCurrentWork = CurrentWork("Работа в карьере");
                    //если персонаж не занят нечем другим но и если он уже сидит в карьере
                    if (Timer_MineWork.CompareTo(DateTime.Now) < 0)
                    {
                        if (CharacterIsFree() == true || CahrCurrentWork == true)
                        {
                            int ImmunInMitutes = Convert.ToInt32(ReadFromFile(SettingsFile, "MineBox")[2]);
                            FinishFieldsOpening();
                            if (CanWorkInMine() > 0 && ImmunTime() > ImmunInMitutes)
                            {
                                Random rnd = new Random();
                                try
                                {
                                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
                                }
                                catch { }


                                try
                                {
                                    //если в тайтле страници нет надписи Карьер, то переходим в карьер
                                    if (driver.Title.IndexOf("Карьер") < 0)
                                    {
                                        //go to the mine
                                        driver.FindElement(By.XPath("//a/div[contains(@class,'f43')]")).Click();
                                        System.Threading.Thread.Sleep(rnd.Next(899, 999));
                                    }
                                }
                                catch { }
                                //начинаем копку и ждем 5 минут
                                try
                                {
                                    //начинаем копку и ждем 5 сек если сработал радар
                                    while (true)
                                    {
                                        try
                                        {
                                            driver.FindElement(By.Id("mine_form")).FindElement(By.XPath(".//center/input[@value='РАБОТАТЬ']")).Click();
                                            Timer_MineWork = ToDateTime(driver.FindElement(By.ClassName("mine_manager")).FindElement(By.TagName("span")).Text);
                                            DateTime Timer_Temp = ToDateTime("00:00:10");
                                            //System.Threading.Thread.Sleep(rnd.Next(6895, 7123));
                                            //добываем
                                            if (Timer_Temp.CompareTo(Timer_MineWork) > 0)
                                            {
                                                driver.FindElement(By.LinkText("ДОБЫТЬ КРИСТАЛЛ")).Click();
                                                Delays();
                                            }
                                            else break;
                                        }
                                        catch
                                        {
                                            break;
                                        }
                                    }
                                    Timer_MineWork = ToDateTime(driver.FindElement(By.CssSelector("p.mine_manager span")).Text);
                                    FinishFieldsOpening();
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public void MineGetCry()
        {
            try
            {
                //если чекбокс "Работать в шахте" равен тру, то можем работать в шахте
                if (ReadFromFile(SettingsFile, "MineBox")[1] == "True")
                {
                    bool CahrCurrentWork = CurrentWork("Работа в карьере");
                    //если персонаж не занят нечем другим но и если он уже сидит в карьере
                    if (Timer_MineWork.CompareTo(DateTime.Now) < 0)
                    {
                        if (CahrCurrentWork == true)
                        {
                            FinishFieldsOpening();
                            if (CanWorkInMine() > 0)
                            {
                                Random rnd = new Random();
                                try
                                {
                                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
                                }
                                catch { }


                                try
                                {
                                    //если в тайтле страници нет надписи Карьер, то переходим в карьер
                                    if (driver.Title.IndexOf("Карьер") < 0)
                                    {
                                        //go to the mine
                                        driver.FindElement(By.XPath("//a/div[contains(@class,'f43')]")).Click();
                                        System.Threading.Thread.Sleep(rnd.Next(899, 999));
                                    }
                                }
                                catch { }
                                //выкупуем крисс
                                try
                                {
                                    if (CahrCurrentWork == true)
                                    {
                                        driver.FindElement(By.LinkText("ДОБЫТЬ КРИСТАЛЛ")).Click();
                                        System.Threading.Thread.Sleep(rnd.Next(899, 1099));
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private int CanWorkInMine()
        {
            int RetValue = -1;
            try
            {
                string temp = driver.FindElement(By.Id("rmenu1")).FindElement(By.ClassName("timers")).Text;
                int Tem = temp.IndexOf("Я свободен!");
                if (Tem > 0)
                {
                    RetValue = 1;
                }
                else
                {
                    Tem = temp.IndexOf("Работа в карьере");
                    if (Tem >= 0)
                    {
                        RetValue = 1;
                    }
                }
            }
            catch
            {
                RetValue = -1;
            }
            return RetValue;
        }

        private int ImmunTime()
        {
            int RetTime = 0;
            try
            {
                string CounterTime = driver.FindElement(By.Id("rmenu1")).FindElement(By.XPath("div[1]/span/span")).Text;
                //клацаем на линку "Персонаж" чтобы обновилась страница при нулевом иммуне
                if (CounterTime == "00:00:00")
                {
                    driver.FindElement(By.Id("m1")).FindElement(By.XPath(".//b")).Click();
                }
                char Separator = ':';
                string[] SeparatedTime;
                SeparatedTime = CounterTime.Split(Separator);
                RetTime = Convert.ToInt32(SeparatedTime[1]);
                //если есть еще часы то плюсуем сходу 60 минут
                if (Convert.ToInt32(SeparatedTime[0]) > 0)
                {
                    RetTime += 60;
                }
            }
            catch { }
            return RetTime;
        }

        public void BigFields(int BigGuru = -1)
        {
            try
            {
                if (CharacterIsFree() == true)
                {
                    FinishFieldsOpening();
                    int MaxF;
                    //если не для нужд большого гуру, то присваиваем значение из бота
                    MaxF = BigGuru >= 0 ? BigGuru : Convert.ToInt32(ReadFromFile(SettingsFile, "MineBox")[4]);
                    Random rnd = new Random();

                    //находим текущее число полянок
                    try
                    {
                        string[] TempString = GetResourceValue("i34");
                        //если число полян больше заданного или больше максимума -3
                        if (Convert.ToInt32(TempString[0]) > MaxF)
                        {
                            try
                            {
                                //шахта
                                driver.FindElement(By.Id("m6")).FindElement(By.XPath(".//b")).Click();
                                Delays();
                                //большая
                                WaitForElementAndClick(driver.FindElement(By.LinkText("БОЛЬШАЯ")), 4000);
                                Delays();
                            }
                            catch { }

                            //цикл по количесву полянок
                            for (int i = 0; i < Convert.ToInt32(TempString[0]); i++)
                            {
                                try
                                {
                                    //ВСЛЕПУЮ
                                    driver.FindElement(By.LinkText("ВСЛЕПУЮ")).Click();
                                }
                                catch { }
                                try
                                {
                                    //ЕЩЁ
                                    driver.FindElement(By.LinkText("ПОПРОБОВАТЬ ЕЩЁ")).Click();
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                    FinishFieldsOpening();
                }
            }
            catch { }
        }

        public void SmallFields()
        {
            try
            {
                if (CharacterIsFree() == true)
                {
                    FinishFieldsOpening();
                    int MaxF = Convert.ToInt32(ReadFromFile(SettingsFile, "MineBox")[3]);
                    Random rnd = new Random();

                    //находим текущее число полянок
                    try
                    {
                        string[] TempString = GetResourceValue("i33");
                        //если число малых полян больше заданного или больше максимума -3
                        if (Convert.ToInt32(TempString[0]) > MaxF)
                        {
                            try
                            {
                                //шахта
                                driver.FindElement(By.Id("m6")).FindElement(By.XPath(".//b")).Click();
                                //малая
                                WaitForElementAndClick(driver.FindElement(By.LinkText("МАЛЕНЬКАЯ")), 4000);
                                System.Threading.Thread.Sleep(rnd.Next(689, 899));
                            }
                            catch { }

                            //цикл по количесву полянок
                            for (int i = 0; i < Convert.ToInt32(TempString[0]); i++)
                            {
                                try
                                {
                                    //ВСЛЕПУЮ
                                    driver.FindElement(By.LinkText("ВСЛЕПУЮ")).Click();
                                }
                                catch { }
                                try
                                {
                                    //ЕЩЁ
                                    driver.FindElement(By.LinkText("ПОПРОБОВАТЬ ЕЩЁ")).Click();
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                    FinishFieldsOpening();
                }
            }
            catch { }
        }

        private void FinishFieldsOpening()
        {
            try
            {
                if (CurrentWork("Открытие поляны") == true)
                {
                    Random rnd = new Random();
                    try
                    {
                        //переход в бодалку, автоматически на поляну перекинет
                        driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(154, 394));
                        //ВСЛЕПУЮ
                        driver.FindElement(By.LinkText("ВСЛЕПУЮ")).Click();
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void PanelForFastAccess()
        {
            try
            {
                IWebElement temp = driver.FindElement(By.Id("fast"));
            }
            catch
            {
                try
                {
                    //открываем настройки
                    driver.FindElement(By.LinkText("Настройки")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //настройки дизайна
                    driver.FindElement(By.LinkText("НАСТРОЙКИ ДИЗАЙНА")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //чекбокс
                    driver.FindElement(By.XPath("//input[@name='fast_total']")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //лист кнопок СОХРАНИТЬ
                    IList<IWebElement> ListOfsaveButtons = driver.FindElements(By.XPath("//input[@value='СОХРАНИТЬ']"));
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //берем 3 кнопку и кликаем
                    ListOfsaveButtons[2].Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                }
                catch { }
            }
        }

        public bool IconsAdding(string IconName)
        {
            bool RetValue = false;
            string Xpath = "";
            try
            {
                Xpath = ".//div[contains(@class,'" + IconName + "')]";
                IWebElement temp = driver.FindElement(By.Id("fast")).FindElement(By.XPath(Xpath));

                RetValue = true;
            }
            catch
            {
                try
                {
                    //идем в настройки быстрой панели
                    driver.FindElement(By.XPath("//div[contains(@class,'flast')]")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //перетягиваем иконку шахты
                    new Actions(driver).DragAndDrop(driver.FindElement(By.XPath(Xpath)), driver.FindElement(By.ClassName("ui-sortable"))).Build().Perform();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //сохраняем
                    driver.FindElement(By.LinkText("СОХРАНИТЬ")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    RetValue = true;
                }
                catch { }
            }
            return RetValue;
        }

        private bool CheckCategoryExists(string CategoryName)
        {
            bool RetValue = false;
            try
            {
                string PageContent = PageSource;
                if (PageContent.Contains(CategoryName))
                {
                    RetValue = true;
                }

            }
            catch { }
            return RetValue;
        }

        private bool CheckCategoryItemExists(string CategoryName, string ItemName)
        {
            bool RetValue = false;
            try
            {
                string PageContent = PageSource;
                if (PageContent.Contains(ItemName))
                {
                    RetValue = true;
                }
            }
            catch { }
            return RetValue;
        }

        private bool CheckCategoryItemExistsById(string CategoryName, string ItemId)
        {
            bool RetValue = false;
            try
            {
                string PageContent = PageSource;
                if (PageContent.Contains(ItemId))
                {
                    RetValue = true;
                }
            }
            catch { }
            return RetValue;
        }

        public string[] GetResourceValue(string ResourceName)
        {
            string[] RetValues = { "NULL", "NULL" };

            try
            {
                //контент всей страници
                string PageContent = driver.PageSource;
                //контент страници режим на 2 части нужным нам ресурсом
                string[] ResourceNameSplit = Regex.Split(PageContent, ResourceName);
                //берем вторую часть и разрезаем ее спанами
                string[] SpanSplit = Regex.Split(ResourceNameSplit[1], "</span>");
                //берем первую часть и разрезаем ее скобочками
                string[] FinalSplit = Regex.Split(SpanSplit[0], ">");
                //берем полследний элемент массива и разрезаем слешем
                RetValues = FinalSplit[FinalSplit.Length - 1].Split('/');
            }
            catch { }
            return RetValues;
        }

        private void AddingCategory(string CategoryName)
        {
            try
            {
                if (CheckCategoryExists(CategoryName) == false)
                {
                    //открываем настройки
                    driver.FindElement(By.LinkText("Настройки")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //настройки дизайна
                    driver.FindElement(By.LinkText("НАСТРОЙКИ ДИЗАЙНА")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //перетягиваем нужную категорию
                    //лист всех элементов
                    IList<IWebElement> NotDisplayedCategoriesList = driver.FindElement(By.Id("listSortDump")).FindElements(By.TagName("li"));
                    foreach (IWebElement TempElemet in NotDisplayedCategoriesList)
                    {
                        if (TempElemet.Text == CategoryName)
                        {
                            new Actions(driver).DragAndDrop(TempElemet, driver.FindElement(By.Id("listSort"))).Build().Perform();
                            //СОХРАНИТЬ
                            driver.FindElement(By.Id("b_other_10")).FindElement(By.LinkText("СОХРАНИТЬ")).Click();
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        private void AddingItemToTheCategory_Timers(string CategoryName, string TimerName, string SecondTimerName)
        {
            try
            {
                if (CheckCategoryItemExists(CategoryName, TimerName) == false)
                {
                    //открываем настройки
                    driver.FindElement(By.LinkText("Настройки")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //настройки дизайна
                    driver.FindElement(By.LinkText("НАСТРОЙКИ ДИЗАЙНА")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //перетягиваем нужную категорию
                    //лист всех элементов
                    IList<IWebElement> NotDisplayedTimers = driver.FindElement(By.Id("listTimerDump")).FindElements(By.TagName("li"));
                    foreach (IWebElement TempElemet in NotDisplayedTimers)
                    {
                        if (TempElemet.Text == SecondTimerName)
                        {
                            new Actions(driver).DragAndDrop(TempElemet, driver.FindElement(By.Id("listTimer"))).Build().Perform();
                            //СОХРАНИТЬ
                            driver.FindElement(By.Id("b_other_13")).FindElement(By.LinkText("СОХРАНИТЬ")).Click();
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        private void AddingItemToTheCategory_Resource(string CategoryName, string ResourceId, string ResourceName)
        {
            try
            {
                if (CheckCategoryItemExistsById(CategoryName, ResourceId) == false)
                {
                    //открываем настройки
                    driver.FindElement(By.LinkText("Настройки")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //настройки дизайна
                    driver.FindElement(By.LinkText("НАСТРОЙКИ ДИЗАЙНА")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(459, 587));
                    //перетягиваем нужные ресурсы
                    //лист всех элементов
                    IList<IWebElement> NotDisplayedTimers = driver.FindElement(By.Id("listDump")).FindElements(By.TagName("li"));
                    foreach (IWebElement TempElemet in NotDisplayedTimers)
                    {
                        string test = TempElemet.GetAttribute("title");
                        if (TempElemet.GetAttribute("title").Contains(ResourceName))
                        {
                            new Actions(driver).DragAndDrop(TempElemet, driver.FindElement(By.Id("listL"))).Build().Perform();
                            //СОХРАНИТЬ
                            driver.FindElement(By.Id("b_other_12")).FindElement(By.LinkText("СОХРАНИТЬ")).Click();
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        public string[] GetResourceValueById(string ResourceId)
        {
            string[] RetValues = { "NULL", "NULL" };
            int CategoriesCounter = 0;

            try
            {
                //ищем нужную категорию и разваричиваем ее
                IList<IWebElement> ListOfCategories = driver.FindElement(By.Id("accordion")).FindElements(By.XPath(".//h3[contains(@class,'ui-accordion-header')]"));
                foreach (IWebElement TempCategory in ListOfCategories)
                {
                    CategoriesCounter++;
                    if (TempCategory.Text == "Ресурсы")
                    {
                        TempCategory.Click();
                        System.Threading.Thread.Sleep(1109);
                        //ищем в категории нужный айтим
                        try
                        {
                            string XPathForItem = ".//div[" + Convert.ToString(CategoriesCounter) + "]/div/ul/li[contains(@id,'" + ResourceId + "')]";
                            string ResourceValue = driver.FindElement(By.Id("accordion")).FindElement(By.XPath(XPathForItem)).FindElement(By.TagName("span")).Text;
                            RetValues = ResourceValue.Split('/');
                            break;
                        }
                        catch { }
                    }
                }
            }
            catch { }
            return RetValues;
        }

        private void CategoryExpanding(string CategoryName)
        {
            try
            {
                IList<IWebElement> ListOfCategories = driver.FindElement(By.Id("accordion")).FindElements(By.XPath(".//h3[contains(@class,'ui-accordion-header')]"));
                foreach (IWebElement CategoryItem in ListOfCategories)
                {
                    if (CategoryItem.Text.Contains(CategoryName))
                    {
                        CategoryItem.Click();
                    }
                }
            }
            catch { }
        }

        private string TimerReader(string TimerName)
        {
            string RetValue = null;
            int CategoriesCounter = 0;

            try
            {
                //ищем нужную категорию и разваричиваем ее
                IList<IWebElement> ListOfCategories = driver.FindElement(By.Id("accordion")).FindElements(By.XPath(".//h3[contains(@class,'ui-accordion-header')]"));
                foreach (IWebElement TempCategory in ListOfCategories)
                {
                    CategoriesCounter++;
                    if (TempCategory.Text == "Счётчики")
                    {
                        TempCategory.Click();
                        //ищем в категории нужный айтим
                        try
                        {
                            string XPathForItem = ".//div[" + Convert.ToString(CategoriesCounter) + "]/div/ul/li[@title='" + TimerName + "']";
                            RetValue = driver.FindElement(By.Id("accordion")).FindElement(By.XPath(XPathForItem)).FindElement(By.TagName("span")).Text;
                            break;
                        }
                        catch { }
                    }
                }
            }
            catch { }
            return RetValue;
        }

        public void CrystalDustMaking()
        {
            bool ShoulMakeCryDust = Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[1]);
            if (ShoulMakeCryDust == true)
            {
                try
                {
                    if (Timer_CryDustMaking.CompareTo(DateTime.Now) < 0)
                    {
                        if (CanMakeCryDust == true)
                        {
                            try
                            {
                                if (Convert.ToInt32(GetResourceValue("Кристальная пыль")[0].Replace(".", "")) < 1991)
                                {
                                    //Переходим в жерновую ico f58
                                    driver.FindElement(By.XPath("//a/div[contains(@class,'f58')]")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(899, 999));
                                    //молоть
                                    driver.FindElement(By.XPath("//input[@value='МОЛОТЬ']")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(899, 999));
                                }
                            }
                            catch { }
                            //обнавляем таймер 
                            Timer_CryDustMaking = ToDateTime(GetResourceValue("Перемолка Кристальной Пыли.")[0]);
                        }
                    }
                }
                catch { }
            }
        }

        public void SoapMaking()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[4]) == true)
            {
                try
                {
                    if (Timer_SoapMaking.CompareTo(DateTime.Now) < 0)
                    {
                        if (CanMakeSoap == true)
                        {
                            try
                            {
                                if (Convert.ToInt32(GetResourceValue("Мыльный камень")[0].Replace(".", "")) < 1991)
                                {
                                    //Переходим на плуг ico f57
                                    driver.FindElement(By.XPath("//a/div[contains(@class,'f57')]")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(899, 999));
                                    //добывать
                                    driver.FindElement(By.XPath("//input[@value='ДОБЫВАТЬ']")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(899, 999));
                                }
                            }
                            catch { }
                            //обнавляем таймер 
                            Timer_SoapMaking = ToDateTime(GetResourceValue("Добыча Мыльного Камня.")[0]);
                        }
                    }
                }
                catch { }
            }
        }

        public void Fishing()
        {
            if (ReadFromFile(SettingsFile, "AdditionalSettingsBox")[2] == "True")
            {
                if (Timer_Fishing.CompareTo(DateTime.Now) < 0)
                {
                    Random rnd = new Random();
                    try
                    {
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
                    }
                    catch { }

                    //проверяем таймер, если 0 то отправляем
                    try
                    {
                        //открываем ресы
                        string Resources = GetResourceValue("Количество походов за пирашками на сегодня")[0];
                        System.Threading.Thread.Sleep(rnd.Next(898, 1156));

                        //глядим время
                        System.Threading.Thread.Sleep(rnd.Next(1105, 1199));
                        Timer_Fishing = ToDateTime(GetResourceValue("Время до возвращения судна с пирашками")[0]);
                        System.Threading.Thread.Sleep(rnd.Next(898, 1156));

                        if (Timer_Fishing.CompareTo(DateTime.Now) < 0 && Resources != "0" && Resources != "")
                        {
                            try
                            {
                                //переход в дальние страны
                                driver.FindElement(By.XPath("//a/div[contains(@class,'f35')]")).Click();
                                System.Threading.Thread.Sleep(rnd.Next(898, 1156));
                            }
                            catch { }
                            try
                            {
                                //кликаем отправить
                                driver.FindElement(By.XPath("//input[@value='ОТПРАВИТЬ']")).Click();
                            }
                            catch { }
                        }

                    }
                    catch { }
                }
            }
        }

        public void PotionBoil()
        {
            if (ReadFromFile(SettingsFile, "PotionMakingBox")[1] == "True")
            {
                try
                {

                    Random rnd = new Random();

                    //если таймер помешиваний больше текущего времени
                    if (Timer_PotionBoil.CompareTo(DateTime.Now) < 1)
                    {
                        try
                        {
                            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
                        }
                        catch { }

                        //переход в простейшие зелья
                        try
                        {
                            driver.FindElement(By.XPath("//a/div[contains(@class,'f60')]")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 1299));
                        }
                        catch { }

                        //арендуем котел
                        BoilerBuy();

                        //если есть счетчик до помешивания до выгребаем его
                        try
                        {
                            if (driver.FindElement(By.Id("alchemy_small_window_text")).Text == "Следующее помешивание котла через:")
                            {
                                Timer_PotionBoil = ToDateTime(driver.FindElement(By.Id("alchemy_small_window_text2")).FindElement(By.TagName("span")).Text);
                            }
                        }
                        catch { }

                        //если есть сообщение помешивания, то мешаем
                        try
                        {
                            if (driver.FindElement(By.Id("alchemy_small_window_text3")).Text == "Вы должны помешать зелье в котле в течение:")
                            {
                                Stirring();
                            }
                        }
                        catch { }

                        //если есть окончание варки, то выгребаем время до окончания
                        try
                        {
                            if (driver.FindElement(By.Id("alchemy_small_window_text")).Text == "Завершение варки зелья через:")
                            {
                                Timer_PotionBoil = ToDateTime(driver.FindElement(By.Id("alchemy_small_window_text2")).FindElement(By.TagName("span")).Text);
                            }
                        }
                        catch { }
                        int botelsCount = Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Стеклянная тара" + '\u0022' + " style")[0]);
                        if (botelsCount != 0)
                        {
                            //начинаме варить зелье если ничего не происходит
                            try
                            {
                                if (driver.FindElement(By.XPath("//input[@value='ЗАВЕРШИТЬ']")).Displayed == true)
                                {
                                    StartPotionMaking();
                                }
                            }
                            catch { }

                            //если просто есть кнопка варить
                            try
                            {
                                if (driver.FindElement(By.XPath("//input[@value='ВАРИТЬ']")).Displayed == true)
                                {
                                    StartPotionMaking();
                                }
                            }
                            catch { }

                            //если задизейблена варить
                            try
                            {
                                if (driver.FindElement(By.Id("alch_main_right_start")).FindElement(By.TagName("b")).Displayed == true)
                                {
                                    StartPotionMaking();
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }
        }

        private void BoilerBuy()
        {
            if (ReadFromFile(SettingsFile, "PotionMakingBox")[3] == "True")
            {
                Random rnd = new Random();

                try
                {
                    //если есть надпись аренда котла то покупаем
                    if (driver.FindElement(By.LinkText("закупочную.")).Displayed == true)
                    {
                        //кликаем линку "закупочная"
                        driver.FindElement(By.LinkText("закупочную.")).Click();
                        //старая секция арнеды
                        try
                        {
                            //переходим в алхимию
                            /*
                            driver.FindElement(By.Id("top_menu")).FindElement(By.XPath(".//a[@title='Братство Алхимиков']")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(698, 1599));
                            //закупочная
                            driver.FindElement(By.Id("hover_guild_shop")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(698, 1599));*/
                            //радиобатон пирашкового котла
                            driver.FindElement(By.Id("form_alchemy_shop")).FindElement(By.XPath(".//table/tbody/tr[3]/td[1]/div/div[1]/input[@value='3']")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(698, 1599));
                            //арендуем
                            driver.FindElement(By.XPath("//input[@value='АРЕНДОВАТЬ']")).Click();
                        }
                        catch { }
                        //обратно в простейшие зелья
                        driver.FindElement(By.XPath("//a/div[contains(@class,'f60')]")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(499, 1299));
                    }
                }
                catch { }
            }
        }

        private void StartPotionMaking()
        {
            Random rnd = new Random();
            try
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
            }
            catch { }
            try
            {
                //кликаем завершить
                driver.FindElement(By.XPath("//input[@value='ЗАВЕРШИТЬ']")).Click();
                System.Threading.Thread.Sleep(rnd.Next(499, 1421));
            }
            catch { }

            try
            {
                //чистим котел пару раз
                driver.FindElement(By.Id("form_alchemy_boiler")).FindElement(By.XPath(".//input[@value='ПОЧИСТИТЬ']")).Click();
                System.Threading.Thread.Sleep(rnd.Next(499, 1400));
                driver.FindElement(By.Id("form_alchemy_boiler")).FindElement(By.XPath(".//input[@value='ПОЧИСТИТЬ']")).Click();
                System.Threading.Thread.Sleep(rnd.Next(499, 899));
            }
            catch { }

            try
            {
                //наваливаем чистюль
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PotionMakingBox")[4]) == true)
                {
                    driver.FindElement(By.Id("b_max_1")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(499, 789));
                }
            }
            catch { }

            try
            {
                try
                {
                    //еслки кнопка задизейблена, то скидуем на разбавленный диавол и покупаем пузыри
                    if (driver.FindElement(By.Id("alch_main_right_start")).FindElement(By.TagName("b")).Text == "ВАРИТЬ")
                    {
                        //диавол
                        driver.FindElement(By.XPath("//img[contains(@src,'Alchemy_Potion_2s')]")).Click();
                        System.Threading.Thread.Sleep(730);
                        ByPotions();
                    }
                }
                catch { }
                //кликаем варить
                driver.FindElement(By.Id("alch_main_right_start")).FindElement(By.XPath(".//input[@value='ВАРИТЬ']")).Click();
                System.Threading.Thread.Sleep(rnd.Next(499, 1324));
            }
            catch { }

            //Инициализируем таймер кристального помешивания сразу после начала варки
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PotionMakingBox")[1]) == true && Convert.ToBoolean(ReadFromFile(SettingsFile, "PotionMakingBox")[5]) == true)
            {
                string duration = Convert.ToString(ReadFromFile(SettingsFile, "PotionMakingBox")[6]);
                if (duration.Length == 1)
                {
                    duration = "0" + duration;
                }
                Timer_CryStiring = ToDateTime("00:" + duration + ":01");
            }
        }

        private void ByPotions()
        {
            try
            {
                //в деревню
                driver.FindElement(By.Id("m3")).FindElement(By.XPath(".//b")).Click();
                //лавка
                driver.FindElement(By.XPath(".//div[text()='Лавка']")).Click();
            }
            catch { }
            //зеленка
            ByPotionsIterator("shop_cmd_1");
            //Синька
            ByPotionsIterator("shop_cmd_2");
            //Бутылочка
            ByPotionsIterator("shop_cmd_130");
            //Пузырек
            ByPotionsIterator("shop_cmd_131");
        }

        private void ByPotionsIterator(string PotionID)
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    driver.FindElement(By.Id(PotionID)).Click();
                    System.Threading.Thread.Sleep(679);
                }
                catch { }
            }
        }

        private void Stirring()
        {
            Random rnd = new Random();
            try
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
            }
            catch { }
            int Result = 0;
            try
            {
                if (driver.FindElement(By.Id("alchemy_small_window_text3")).Text == "Вы должны помешать зелье в котле в течение:")
                {
                    try
                    {
                        //переход в простейшие зелья
                        driver.FindElement(By.XPath("//a/div[contains(@class,'f60')]")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(499, 1299));
                    }
                    catch { }

                    //добываем пару чисел 
                    try
                    {
                        string text;
                        int FirstNyumber = ElementScreenshot("FirstNum", "right_main", ".//div[4]/img");
                        text = string.Format("Уравниваем температуру в котле: первое число {0}", FirstNyumber);
                        Logger(text);
                        int SecondNumber = ElementScreenshot("SecNum", "right_main", ".//div[5]/img");
                        text = string.Format("Уравниваем температуру в котле: второе число {0}", SecondNumber);
                        Logger(text);
                        //вычисляем разницу температур
                        Result = SecondNumber - FirstNyumber;

                        //опускаем если вторая больше первой
                        if (Result < 0)
                        {
                            try
                            {
                                driver.FindElement(By.Id("alchemy_stir_action")).FindElement(By.XPath(".//p[2]/input[@value='2']")).Click();
                                System.Threading.Thread.Sleep(rnd.Next(499, 1329));
                                //если результат отрицательный то умножаем на -1 чтоб стал положительным
                                Result = Result * (-1);
                            }
                            catch { }
                        }
                    }
                    catch { }

                    //вкидуем число в квери
                    try
                    {
                        driver.FindElement(By.Id("change_temperature")).Clear();
                        System.Threading.Thread.Sleep(rnd.Next(599, 1989));
                        driver.FindElement(By.Id("change_temperature")).SendKeys(Convert.ToString(Result));
                        System.Threading.Thread.Sleep(rnd.Next(1099, 1489));
                    }
                    catch { }

                    //помешуем

                    try
                    {
                        driver.FindElement(By.XPath("//input[@value='ПОМЕШАТЬ']")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(698, 1399));
                    }
                    catch { }
                }
            }
            catch { }
        }

        private int ElementScreenshot(string ImageName, string ImageElementID, string PathToTheImage)
        {
            int result = 0;
            try
            {
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile("Screen.png", ImageFormat.Png);

                //координаты и поинтер искомого имеджа
                IWebElement TargetImage = driver.FindElement(By.Id(ImageElementID)).FindElement(By.XPath(PathToTheImage));

                //фокусимся на элемент с числом
                Actions action = new Actions(driver);
                action.MoveToElement(TargetImage).Build().Perform();
                int width = TargetImage.Size.Width;
                int height = TargetImage.Size.Height;
                Point ElemPoint = TargetImage.Location;


                //исходный имедж
                Bitmap sourceBitmap = new Bitmap("Screen.png");
                //скрин компонента
                Bitmap PartOfScrn = new Bitmap(width - 1, height);

                Color sourceColor = new Color();

                for (int i = 0; i < width - 1; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        sourceColor = sourceBitmap.GetPixel(i + ElemPoint.X, j + ElemPoint.Y);
                        PartOfScrn.SetPixel(i, j, sourceColor);
                    }
                }
                PartOfScrn.Save(ImageName + ".png");

                //var sFile = new FileStream(@"C:\" + PicName + ".png", FileMode.Open);
                var sFile = new FileStream(ImageName + ".png", FileMode.Open);
                byte[] PhotoBytes = new byte[sFile.Length];
                sFile.Read(PhotoBytes, 0, PhotoBytes.Length);
                Simple_Bot.ocr.ocrSoapClient webservice = new Simple_Bot.ocr.ocrSoapClient();
                //SimpleBotLibrary.ocr.ocrSoapClient webservice = new SimpleBotLibrary.ocr.ocrSoapClient();
                result = Convert.ToInt32(webservice.Analyze("aksis8@gmail.com", PhotoBytes));

                sFile.Dispose();
                PartOfScrn.Dispose();
                sourceBitmap.Dispose();
            }
            catch { }

            return result;
        }

        public void TanksMaking()
        {
            if (ReadFromFile(SettingsFile, "PotionMakingBox")[2] == "True" && Convert.ToBoolean(ReadFromFile(SettingsFile, "PotionMakingBox")[1]) == true)
            {
                if (Timer_TanksMaking.CompareTo(DateTime.Now) < 0)
                {
                    if ((Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Стеклянная тара" + '\u0022' + " style")[0]) < Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Стеклянная тара" + '\u0022' + " style")[1])) || (Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Раскаленное стекло" + '\u0022' + " style")[0]) < Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Раскаленное стекло" + '\u0022' + " style")[1])))
                    {
                        //Изготавливаем тару
                        try
                        {
                            //переход в лабараторию
                            driver.FindElement(By.XPath("//a/div[contains(@class,'f59')]")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 1277));
                        }
                        catch { }

                        try
                        {
                            //отжиг
                            driver.FindElement(By.XPath("//input[@value='ОТЖИГ']")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 1234));
                        }
                        catch { }

                        try
                        {
                            //охлаждение
                            driver.FindElement(By.XPath("//input[@value='ОХЛАЖДЕНИЕ']")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(898, 1359));
                        }
                        catch { }
                        Timer_TanksMaking = ToDateTime("00:16:00");
                    }
                }
            }
        }

        public void StatsUp()
        {
            try
            {
                if (ReadFromFile(SettingsFile, "StutsUpBox")[1] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[2] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[3] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[4] == "True" || ReadFromFile(SettingsFile, "StutsUpBox")[5] == "True")
                {
                    if (CharacterIsFree() == true)
                    {
                        //смотрим сколько бабла на руках
                        try
                        {
                            CurrentGold = driver.FindElement(By.Id("gold")).FindElement(By.TagName("b")).Text.Replace(".", "");
                        }
                        catch { }

                        if (Convert.ToInt64(CurrentGold) >= maxStatCost)
                        {
                            string text = string.Format("Текущее кол-во золота {0}, максимальная стоимость стата {1}", CurrentGold, maxStatCost);
                            Logger(text);
                            try
                            {
                                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOutValue));
                            }
                            catch { }

                            //идем к тренеру
                            try
                            {
                                driver.FindElement(By.Id("fast")).FindElement(By.XPath(".//a/div[@class='ico f38']")).Click();
                                Delays();
                            }
                            catch { }

                            //собираем значения статов
                            MaxStatValues();

                            if (Convert.ToInt64(CurrentGold) >= maxStatCost)
                            {

                                if (Convert.ToInt64(CurrentGold) > Convert.ToInt64(power) || Convert.ToInt64(CurrentGold) > Convert.ToInt64(charisma) || Convert.ToInt64(CurrentGold) > Convert.ToInt64(block) ||
                                    Convert.ToInt64(CurrentGold) > Convert.ToInt64(endurance) || Convert.ToInt64(CurrentGold) > Convert.ToInt64(dexterity))
                                {
                                    //идем к тренеру если в хедере нет Тренировка
                                    if (driver.Title.Contains("Тренировка") == false)
                                    {
                                        try
                                        {
                                            driver.FindElement(By.Id("fast")).FindElement(By.XPath(".//a/div[@class='ico f38']")).Click();
                                            System.Threading.Thread.Sleep(rnd.Next(2333, 2555));
                                        }
                                        catch { }
                                    }

                                    try
                                    {
                                        //ищим свободный стат по приоритету и если флаг в тру то качаем его
                                        if (ReadFromFile(SettingsFile, "StutsUpBox")[1] == "True" & Convert.ToInt64(CurrentGold) > Convert.ToInt64(power))
                                        {
                                            try
                                            {
                                                while (true)
                                                {
                                                    driver.FindElement(By.Id("training_power")).FindElement(By.XPath(".//input[@value='ПОВЫСИТЬ']")).Click();
                                                    System.Threading.Thread.Sleep(rnd.Next(989, 1099));
                                                }
                                            }
                                            catch { }
                                        }

                                        if (ReadFromFile(SettingsFile, "StutsUpBox")[5] == "True" & Convert.ToInt64(CurrentGold) > Convert.ToInt64(charisma))
                                        {
                                            //мастерство
                                            try
                                            {
                                                while (true)
                                                {
                                                    driver.FindElement(By.Id("training_charisma")).FindElement(By.XPath(".//input[@value='ПОВЫСИТЬ']")).Click();
                                                    System.Threading.Thread.Sleep(rnd.Next(989, 1099));
                                                }
                                            }
                                            catch { }
                                        }

                                        if (ReadFromFile(SettingsFile, "StutsUpBox")[2] == "True" & Convert.ToInt64(CurrentGold) > Convert.ToInt64(block))
                                        {
                                            //защита
                                            try
                                            {
                                                while (true)
                                                {
                                                    driver.FindElement(By.Id("training_block")).FindElement(By.XPath(".//input[@value='ПОВЫСИТЬ']")).Click();
                                                    System.Threading.Thread.Sleep(rnd.Next(989, 1099));
                                                }
                                            }
                                            catch { }
                                        }


                                        if (ReadFromFile(SettingsFile, "StutsUpBox")[3] == "True" & Convert.ToInt64(CurrentGold) > Convert.ToInt64(dexterity))
                                        {
                                            //ловка
                                            try
                                            {
                                                while (true)
                                                {
                                                    driver.FindElement(By.Id("training_dexterity")).FindElement(By.XPath(".//input[@value='ПОВЫСИТЬ']")).Click();
                                                    System.Threading.Thread.Sleep(rnd.Next(989, 1099));
                                                }
                                            }
                                            catch { }
                                        }

                                        if (ReadFromFile(SettingsFile, "StutsUpBox")[4] == "True" & Convert.ToInt64(CurrentGold) > Convert.ToInt64(endurance))
                                        {
                                            //масса
                                            try
                                            {
                                                while (true)
                                                {
                                                    driver.FindElement(By.Id("training_endurance")).FindElement(By.XPath(".//input[@value='ПОВЫСИТЬ']")).Click();
                                                    System.Threading.Thread.Sleep(rnd.Next(989, 1099));
                                                }
                                            }
                                            catch { }
                                        }

                                        //собираем значения статов
                                        MaxStatValues();
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void MaxStatValues()
        {
            try
            {
                if (ReadFromFile(SettingsFile, "StutsUpBox")[1] == "True")
                {
                    power = driver.FindElement(By.XPath("//div[3]/div[3]/table/tbody/tr[1]/td[4]/b[1]")).Text.Replace(".", "");
                    if (Convert.ToInt64(power) > maxStatCost)
                        maxStatCost = Convert.ToInt64(power);
                }
            }
            catch { }

            try
            {
                if (ReadFromFile(SettingsFile, "StutsUpBox")[2] == "True")
                {
                    block = driver.FindElement(By.XPath("//div[3]/div[3]/table/tbody/tr[3]/td[4]/b[1]")).Text.Replace(".", "");
                    if (Convert.ToInt64(block) > maxStatCost)
                        maxStatCost = Convert.ToInt64(block);
                }
            }
            catch { }

            try
            {
                if (ReadFromFile(SettingsFile, "StutsUpBox")[4] == "True")
                {
                    endurance = driver.FindElement(By.XPath("//div[3]/div[3]/table/tbody/tr[7]/td[4]/b[1]")).Text.Replace(".", "");
                    if (Convert.ToInt64(endurance) > maxStatCost)
                        maxStatCost = Convert.ToInt64(endurance);
                }
            }
            catch { }

            try
            {
                if (ReadFromFile(SettingsFile, "StutsUpBox")[3] == "True")
                {
                    dexterity = driver.FindElement(By.XPath("//div[3]/div[3]/table/tbody/tr[5]/td[4]/b[1]")).Text.Replace(".", "");
                    if (Convert.ToInt64(dexterity) > maxStatCost)
                        maxStatCost = Convert.ToInt64(dexterity);
                }
            }
            catch { }

            try
            {
                if (ReadFromFile(SettingsFile, "StutsUpBox")[5] == "True")
                {
                    charisma = driver.FindElement(By.XPath("//div[3]/div[3]/table/tbody/tr[9]/td[4]/b[1]")).Text.Replace(".", "");
                    if (Convert.ToInt64(charisma) > maxStatCost)
                        maxStatCost = Convert.ToInt64(charisma);
                }
            }
            catch { }
        }

        private bool CharacterIsFree()
        {
            bool RetValue = false;
            try
            {
                string temp = driver.FindElement(By.Id("rmenu1")).FindElement(By.ClassName("timers")).Text;
                bool Tem = temp.Contains("Я свободен!");
                if (Tem == true)
                {
                    RetValue = true;
                }
            }
            catch
            {
                RetValue = false;
            }
            return RetValue;
        }

        private bool CurrentWork(string WorkName)
        {
            bool RetValue = false;
            try
            {
                string temp = driver.FindElement(By.Id("rmenu1")).FindElement(By.ClassName("timers")).Text;
                bool Tem = temp.Contains(WorkName);
                if (Tem == true)
                {
                    RetValue = true;
                }
                else RetValue = false;
            }
            catch
            {
                RetValue = true;
            }
            return RetValue;
        }

        public void Fly()
        {
            if (ReadFromFile(SettingsFile, "AdditionalSettingsBox")[3] == "True")
            {
                Random rnd = new Random();
                try
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                }
                catch { }

                try
                {
                    //открываем секцию летунов !!!
                    driver.FindElement(By.Id("accordion")).FindElement(By.XPath(".//h3[2]")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(1105, 1199));
                }
                catch { }

                int FlyNumber = 2;
                int settingsIndex = 1;
                bool IsInTrip = true;
                bool MinigameFightFood = false;
                //First Fly
                while (FlyNumber < 9)
                {
                    if (!Convert.ToBoolean(ReadFromFile(SettingsFile, "FlyBox")[20 + FlyNumber / 2]))
                    {
                        try
                        {
                            IWebElement jstimer = driver.FindElement(By.CssSelector(".flyings div:nth-of-type(" + Convert.ToString(FlyNumber) + ") .js_timer"));
                        }
                        catch
                        {
                            try
                            {
                                //если зверушка не в большом приключении
                                if (driver.FindElement(By.CssSelector(".content:nth-of-type(" + Convert.ToString(FlyNumber) + ") a")).Text.Contains("Мини"))//if (driver.FindElement(By.Id("accordion")).FindElement(By.XPath("//div[2]/div/div[" + Convert.ToString(FlyNumber) + "]/center/b/a[contains(@href,'castle')]")).Text == "Мини-игра")
                                {
                                    //проверяем нет ли миниигры
                                    try
                                    {
                                        driver.FindElement(By.Id("accordion")).FindElement(By.XPath("//div[2]/div/div[" + Convert.ToString(FlyNumber) + "]/center/form/input[@value='ПОМОЧЬ']")).Click();
                                        System.Threading.Thread.Sleep(rnd.Next(898, 1089));
                                        try
                                        {
                                            //выибраем один из сундучков
                                            int box = rnd.Next(0, 3);
                                            switch (box)
                                            {
                                                case 1:
                                                    try
                                                    {
                                                        driver.FindElement(By.Id("flying_block")).FindElement(By.XPath(".//center/a[1]")).Click();
                                                        System.Threading.Thread.Sleep(rnd.Next(659, 899));
                                                    }
                                                    catch { }
                                                    break;

                                                case 2:
                                                    try
                                                    {
                                                        driver.FindElement(By.Id("flying_block")).FindElement(By.XPath(".//center/a[2]")).Click();
                                                        System.Threading.Thread.Sleep(rnd.Next(659, 899));
                                                    }
                                                    catch { }
                                                    break;
                                                case 3:
                                                    try
                                                    {
                                                        driver.FindElement(By.Id("flying_block")).FindElement(By.XPath(".//center/a[3]")).Click();
                                                        System.Threading.Thread.Sleep(rnd.Next(659, 899));
                                                    }
                                                    catch { }
                                                    break;
                                            }
                                        }
                                        catch { }
                                    }
                                    catch { }
                                    IsInTrip = false;
                                    MinigameFightFood = true;
                                }
                            }
                            catch { }

                            //проверяем нет ли нападения                
                            try
                            {
                                //если зверушка не в большом приключении
                                if (driver.FindElement(By.CssSelector(".content:nth-of-type(" + Convert.ToString(FlyNumber) + ") a")).Text.Contains("Нападение"))//if (driver.FindElement(By.Id("accordion")).FindElement(By.XPath("//div[2]/div/div[" + Convert.ToString(FlyNumber) + "]/center/b/a[contains(@href,'castle')]")).Text == "Нападение на летуна!")
                                {
                                    //проверяем нет ли миниигры
                                    try
                                    {
                                        driver.FindElement(By.CssSelector(".content:nth-of-type(" + Convert.ToString(FlyNumber) + ") input[value='ПОСМОТРЕТЬ']")).Click();
                                        System.Threading.Thread.Sleep(rnd.Next(898, 1125));
                                        try
                                        {
                                            //выибраем один из сундучков
                                            int box = rnd.Next(0, 3);
                                            switch (box)
                                            {
                                                case 1:
                                                    try
                                                    {
                                                        driver.FindElement(By.Id("flying_block")).FindElement(By.XPath(".//center/a[1]")).Click();
                                                        System.Threading.Thread.Sleep(rnd.Next(659, 899));
                                                    }
                                                    catch { }
                                                    break;

                                                case 2:
                                                    try
                                                    {
                                                        driver.FindElement(By.Id("flying_block")).FindElement(By.XPath(".//center/a[2]")).Click();
                                                        System.Threading.Thread.Sleep(rnd.Next(659, 899));
                                                    }
                                                    catch { }
                                                    break;
                                                case 3:
                                                    try
                                                    {
                                                        driver.FindElement(By.Id("flying_block")).FindElement(By.XPath(".//center/a[3]")).Click();
                                                        System.Threading.Thread.Sleep(rnd.Next(659, 899));
                                                    }
                                                    catch { }
                                                    break;
                                            }
                                        }
                                        catch { }
                                    }
                                    catch { }
                                    IsInTrip = false;
                                    MinigameFightFood = true;
                                }
                            }
                            catch { }

                            //Будим если спит
                            try
                            {
                                IWebElement wake = driver.FindElement(By.CssSelector(".content:nth-of-type(" + Convert.ToString(FlyNumber) + ") a.cmd_asmall_sl"));
                                wake.Click();
                                Delays();
                                //Будим и кормим 2 раза
                                driver.FindElement(By.XPath("//input[@value='РАЗБУДИТЬ']")).Click();
                                Delays();
                                driver.FindElement(By.Id("feed_zoo_did")).FindElement(By.XPath(".//input[@value='КОРМИТЬ']")).Click();
                                Delays();
                                driver.FindElement(By.Id("feed_zoo_did")).FindElement(By.XPath(".//input[@value='КОРМИТЬ']")).Click();
                            }
                            catch { }

                            //кормим если голоден
                            try
                            {
                                string satiety = driver.FindElement(By.CssSelector(".content:nth-of-type(" + Convert.ToString(FlyNumber) + ") u:nth-of-type(1) a")).Text;
                                satiety = satiety.TrimEnd('%');
                                if (Convert.ToInt32(satiety) < 75)
                                {
                                    //кликаем по грибочкув боковой панели
                                    driver.FindElement(By.Id("accordion")).FindElement(By.CssSelector(".content:nth-of-type(" + Convert.ToString(FlyNumber) + ") u:nth-of-type(1) a b")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(899, 1399));
                                    driver.FindElement(By.Id("feed_zoo_did")).FindElement(By.XPath(".//input[@value='КОРМИТЬ']")).Click();
                                    IsInTrip = false;
                                    MinigameFightFood = true;
                                }
                            }
                            catch { }

                            //просто кликаем по иконке летуна если нет ни миниигры, ни нападения и не кормили зверя
                            try
                            {
                                //если зверушка не в большом приключении
                                if (driver.FindElement(By.CssSelector(".content:nth-of-type(" + Convert.ToString(FlyNumber) + ") b a")).Text.Contains("Большое") && MinigameFightFood == false)
                                {

                                }
                            }
                            catch
                            {
                                try
                                {
                                    driver.FindElement(By.CssSelector(".title:nth-of-type(" + Convert.ToString(FlyNumber - 1) + ") a b")).Click();
                                    Delays();
                                    IsInTrip = false;
                                    MinigameFightFood = false;

                                    //Персонально
                                    if (CurrentWork("Спуск") == false)
                                    {
                                        if (driver.FindElement(By.CssSelector(".char_stat.char_stat_with_pets u")).Text.Equals("Aksis"))
                                        {
                                            try
                                            {
                                                //Если есть надпись что летун питомиц
                                                IWebElement statusElement = driver.FindElement(By.XPath(".//b[contains(text(),'Питомец')]"));
                                                //клацаем на яйца
                                                driver.FindElement(By.XPath(".//div[@title='Информация']/span")).Click();
                                                Delays();
                                                //клацаем Сменить
                                                driver.FindElement(By.LinkText("Сменить")).Click();
                                                Delays();
                                                //Меняем на агрессора
                                                driver.FindElement(By.XPath(".//option[text()='Агрессор']")).Click();
                                                Delays();
                                                //подтверждаем смену
                                                driver.FindElement(By.CssSelector("#ch_state input[value='СМЕНИТЬ']")).Click();
                                                Delays();
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                catch { }
                            }

                            //если нe в БП то отправляем туда или в МП если есть доступное время
                            if (IsInTrip == false)
                            {
                                try
                                {
                                    //кликаем по компасу
                                    driver.FindElement(By.Id("fa_events")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(799, 1201));
                                    //Прежде всего отсылаем в малое если нужно
                                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FlyBox")[16 + FlyNumber / 2]) == true)
                                    {
                                        try
                                        {
                                            IWebElement smallTrip = driver.FindElement(By.XPath(".//table/tbody/tr[1]//input[@value='ОТПРАВИТЬ']"));
                                            smallTrip.Click();
                                        }
                                        catch { }
                                    }
                                    //В большое
                                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FlyBox")[settingsIndex]) == true)
                                    {
                                        driver.FindElement(By.Id("flying_block")).FindElement(By.XPath(".//table/tbody/tr[2]/td[3]/form/input[5][@value='ОТПРАВИТЬ']")).Click();
                                        System.Threading.Thread.Sleep(rnd.Next(659, 989));
                                    }
                                    else
                                    {
                                        //На кар-кар
                                        WaitForElementAndClick(driver.FindElement(By.XPath("//input[@class='cmd_all cmd_row4 cmd_arow4']")), 4000);
                                        //Выбираем путь
                                        switch (Convert.ToInt32(ReadFromFile(SettingsFile, "FlyBox")[settingsIndex + 2]))
                                        {
                                            case 1:
                                                WaitForElementAndClick(driver.FindElement(By.XPath("//div[@rel='1']")), 5000);
                                                break;
                                            case 2:
                                                WaitForElementAndClick(driver.FindElement(By.XPath("//div[@rel='2']")), 5000);
                                                break;
                                            case 3:
                                                WaitForElementAndClick(driver.FindElement(By.XPath("//div[@rel='3']")), 5000);
                                                break;
                                            default: break;
                                        }
                                        //вВыбираем кол-во часов
                                        string hrs = ReadFromFile(SettingsFile, "FlyBox")[settingsIndex + 3];
                                        driver.FindElement(By.XPath("//option[@value='" + hrs + "']")).Click();
                                        //Отправить
                                        System.Threading.Thread.Sleep(rnd.Next(1123, 1325));
                                        driver.FindElement(By.CssSelector(".mbuttons.sbt.fl_l")).Click();
                                        //Close
                                        System.Threading.Thread.Sleep(rnd.Next(1123, 1325));
                                        driver.FindElement(By.CssSelector(".iconsp.icon_close")).Click();
                                    }
                                    IsInTrip = true;
                                }
                                catch { }
                            }
                        }
                    }
                    settingsIndex += 4;
                    FlyNumber = FlyNumber + 2;
                }
            }
        }

        public void Underground()
        {
            try
            {
                //если маркнут чекбокс для подзема
                if (ReadFromFile(SettingsFile, "UndergroundBox")[1] == "True" && ReadFromFile(SettingsFile, "UndergroundBox")[2] == "True")
                {
                    //если текущая работа не спуск в подземелье, то пробуем спустится
                    if (CurrentWork("Спуск") == false)
                    {
                        //если таймер позваляет спустится в метро и персонаж свободен
                        if (Timer_Underground.CompareTo(DateTime.Now) < 0 && CharacterIsFree() == true)
                        {
                            try
                            {
                                //садим пета
                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[11]) == true)
                                {
                                    SetPet();
                                }
                                //берем пета
                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[12]) == true)
                                {
                                    GetPet(PetTypeProvider());
                                }
                                //переходим в шахту и кликаем "по лебедке"/"по веревке"
                                driver.FindElement(By.Id("m6")).FindElement(By.XPath(".//b")).Click();
                                System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[4]) == true)
                                {
                                    driver.FindElement(By.XPath("//input[contains(@value,'ЛЕБЕДКЕ')]")).Click();
                                }
                                else
                                {
                                    driver.FindElement(By.XPath("//input[contains(@value,'ВЕРЕВКЕ')]")).Click();
                                }
                                System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                //обнавляем таймер подзема ВоркПендинг
                                TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                                //покупаем ключики
                                ByKeys();
                            }
                            catch { }
                        }
                    }
                    //если текущая работа спусk
                    //глядим сколько еще спускаться/бродить
                    else
                    {
                        try
                        {
                            //читаем значение сколько еще спускаться
                            string temp = driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text;
                            DateTime Timer_Temp = ToDateTime(temp);
                            //если можно спускаться, то спускаемся, если нет, то присваеваем счетчику ожидания работы значени из тем_таймера
                            if (temp == "00:00:00")
                            {
                                //если не на подземном царстве то переходим туда
                                try
                                {
                                    if (driver.Title.Contains("царство") == false && driver.Title.Contains("Лог") == false)
                                    {
                                        //переходим в бодалку
                                        System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                        driver.FindElement(By.Id("m8")).Click();
                                        System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                    }
                                }
                                catch { }
                                //пытаемся напасть
                                try
                                {
                                    driver.FindElement(By.CssSelector("input[value='НАПАСТЬ']")).Click();
                                }
                                catch { }
                                try
                                {
                                    // бродим
                                    driver.FindElement(By.CssSelector("input[value='БРОДИТЬ ПО УРОВНЮ']")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(555, 698));
                                    //читаем таймер сколько спускаться(бродить)
                                    //обнавляем таймер подзема ВоркПендинг
                                    TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                                }
                                catch { }
                                //если нет никого на уровне
                                try
                                {
                                    string TempMessage = "";
                                    try
                                    {
                                        TempMessage = driver.FindElement(By.CssSelector("div.message")).Text;
                                    }
                                    catch { }
                                    if (TempMessage == "На уровне нет никого. Вообще никого. Иди глубже!")
                                    {
                                        //если есть пристанище, то ливаем
                                        if (driver.FindElement(By.CssSelector("th[colspan]")).Text.Contains("Пристанище") == true)
                                        {
                                            try
                                            {
                                                //выходим с подзема
                                                driver.FindElement(By.XPath("//input[@value='ВЫЙТИ ИЗ ПОДЗЕМЕЛЬЯ']")).Click();
                                                System.Threading.Thread.Sleep(rnd.Next(1158, 1354));
                                                driver.FindElement(By.XPath("//input[@value='ТОЧНО ВЫЙТИ?']")).Click();
                                                System.Threading.Thread.Sleep(rnd.Next(1899, 2599));
                                                //обнавляем таймер подзема и ВоркПендинг
                                                Timer_Underground = ToDateTime(GetResourceValue("Время до похода в подземелье")[0]);
                                                TP_Underground = Timer_Underground;
                                                //вскрываем/продаем панды
                                                OpenSalePanda();
                                                //Достаем пета
                                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[11]) == true)
                                                {
                                                    GetPet();
                                                }
                                                //Садим пета
                                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[12]) == true)
                                                {
                                                    SetPet();
                                                }
                                            }
                                            catch { }
                                        }
                                        //если нет пристанища то лезим дальше
                                        else
                                        {
                                            //по лебедке или веревке
                                            if (ReadFromFile(SettingsFile, "UndergroundBox")[4] == "True")
                                            {
                                                driver.FindElement(By.CssSelector("input[value='ПО ЛЕБЕДКЕ']")).Click();
                                                System.Threading.Thread.Sleep(rnd.Next(459, 789));
                                            }
                                            else
                                            {
                                                driver.FindElement(By.CssSelector("input[value='ПО ВЕРЕВКЕ']")).Click();
                                                System.Threading.Thread.Sleep(rnd.Next(500, 819));
                                            }
                                            //читаем таймер сколько спускаться
                                            //обнавляем таймер подзема ВоркПендинг
                                            TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                catch { }
                            }
                            else
                            {
                                //читаем таймер сколько спускаться(бродить)
                                TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        public void ByKeys()
        {
            if (ReadFromFile(SettingsFile, "UndergroundBox")[6] == "True")
            {
                Random rnd = new Random();
                //смотрим сколько у нас ключей, если 0, то докупаем
                if (Convert.ToInt32(GetResourceValue("Ключ от ворот царства Манаглота")[0]) == 0)
                {
                    try
                    {
                        //устрашатели
                        driver.FindElement(By.Id("top_menu")).FindElement(By.XPath(".//a[@title='Устрашатели']")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(1105, 1199));
                        //оружейная
                        driver.FindElement(By.Id("hover_guild_weapon")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(1105, 1199));
                        //Предметы
                        driver.FindElement(By.LinkText("Предметы")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(1105, 1199));

                        //двигаем бегунок
                        IWebElement Slider = driver.FindElement(By.Id("slider_2")).FindElement(By.TagName("a"));
                        Actions builder = new Actions(driver);
                        IAction dragAndDrop = builder.ClickAndHold(Slider).MoveByOffset(0, 0).MoveByOffset(100, 100).Release().Build();
                        dragAndDrop.Perform();
                        System.Threading.Thread.Sleep(rnd.Next(1105, 1199));

                        //купить
                        driver.FindElement(By.XPath("//input[@value='КУПИТЬ']")).Click();
                    }
                    catch { }
                }
            }
        }

        public void UndergroundFast()
        {
            try
            {
                Random rnd = new Random();
                //если маркнут чекбокс для подзема + быстрый подзем
                if (ReadFromFile(SettingsFile, "UndergroundBox")[1] == "True" && ReadFromFile(SettingsFile, "UndergroundBox")[3] == "True")
                {
                    //если текущая работа не спуск в подземелье, то пробуем спустится
                    if (CurrentWork("Спуск") == false)
                    {
                        //если таймер позваляет спустится в метро и персонаж свободен
                        if (Timer_Underground.CompareTo(DateTime.Now) < 0 && CharacterIsFree() == true)
                        {
                            //если время иммуна больше заданного
                            if (ImmunTime() >= Convert.ToInt32(ReadFromFile(SettingsFile, "UndergroundBox")[7]))
                            {
                                try
                                {
                                    //садим пета
                                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[11]) == true)
                                    {
                                        SetPet();
                                    }
                                    //берем пета
                                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[12]) == true)
                                    {
                                        GetPet(PetTypeProvider());
                                    }
                                    //переходим в шахту и кликаем "по лебедке"/"по веревке"
                                    driver.FindElement(By.Id("m6")).FindElement(By.XPath(".//b")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[4]) == true)
                                    {
                                        driver.FindElement(By.XPath("//input[contains(@value,'ЛЕБЕДКЕ')]")).Click();
                                    }
                                    else
                                    {
                                        driver.FindElement(By.XPath("//input[contains(@value,'ВЕРЕВКЕ')]")).Click();
                                    }
                                    System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                    //обнавляем таймер подзема ВоркПендинг
                                    TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                                    //покупаем ключики
                                    ByKeys();
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        //читаем значение сколько еще спускаться
                        string temp = driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text;
                        DateTime Timer_Temp = ToDateTime(temp);
                        //если можно спускаться, то спускаемся, если нет, то присваеваем счетчику ожидания работы значени из тем_таймера
                        if (temp == "00:00:00")
                        {
                            try
                            {
                                //если не на подземном царстве то переходим туда
                                if (driver.Title.Contains("царство") == false && driver.Title.Contains("Лог") == false)
                                {
                                    //переходим в бодалку
                                    System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                    driver.FindElement(By.Id("m8")).Click();
                                }
                                //пытаемся напасть
                                try
                                {
                                    System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                    driver.FindElement(By.CssSelector("input[value='НАПАСТЬ']")).Click();
                                    System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                }
                                catch { }
                                //если есть развилка
                                try
                                {
                                    if (driver.FindElement(By.CssSelector("th[colspan='2']")).Text.Contains("Коварного") == true)
                                    {
                                        //то выбираем куда спускатся
                                        //table1 - в тронный, table2 - в сокровищницу
                                        IList<IWebElement> CordList = driver.FindElements(By.XPath("//input[contains(@value,'ВЕРЕВКЕ')]"));
                                        IList<IWebElement> Winch = driver.FindElements(By.XPath("//input[contains(@value,'ЛЕБЕДКЕ')]"));
                                        if (ReadFromFile(SettingsFile, "UndergroundBox")[5] == "True")
                                        {
                                            //по веревке td1
                                            System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                            CordList[1].Click();

                                        }
                                        else
                                        {
                                            //по лебедке td2
                                            System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                            Winch[1].Click();
                                            //driver.FindElement(By.CssSelector("table:nth-of-type(2) td:nth-of-type(2)")).Click();
                                        }
                                        //читаем таймер сколько спускаться
                                        //обнавляем таймер подзема ВоркПендинг
                                        TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                                    }
                                }
                                catch { }
                                //если есть пристанище
                                try
                                {
                                    if (driver.FindElement(By.CssSelector("th[colspan]")).Text.Contains("Пристанище") == true)
                                    {
                                        // бродим
                                        driver.FindElement(By.CssSelector("input[value='БРОДИТЬ ПО УРОВНЮ']")).Click();
                                        //если на уровне нет никого
                                        string TempMessage = "";
                                        System.Threading.Thread.Sleep(rnd.Next(1100, 1234));
                                        try
                                        {
                                            TempMessage = driver.FindElement(By.CssSelector("div.message")).Text;
                                        }
                                        catch { }
                                        if (TempMessage == "На уровне нет никого. Вообще никого. Иди глубже!")
                                        {
                                            try
                                            {
                                                //выходим с подзема
                                                driver.FindElement(By.XPath("//input[@value='ВЫЙТИ ИЗ ПОДЗЕМЕЛЬЯ']")).Click();
                                                System.Threading.Thread.Sleep(rnd.Next(1158, 1354));
                                                driver.FindElement(By.XPath("//input[@value='ТОЧНО ВЫЙТИ?']")).Click();
                                                System.Threading.Thread.Sleep(rnd.Next(1899, 2599));
                                                //обнавляем таймер подзема и ВоркПендинг
                                                Timer_Underground = ToDateTime(GetResourceValue("Время до похода в подземелье")[0]);
                                                TP_Underground = Timer_Underground;
                                                //вскрываем/продаем панды
                                                OpenSalePanda();
                                                //Достаем пета
                                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[11]) == true)
                                                {
                                                    GetPet();
                                                }
                                                //Садим пета
                                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[12]) == true)
                                                {
                                                    SetPet();
                                                }
                                            }
                                            catch { }
                                        }
                                        //читаем таймер сколько спускаться(бродить)
                                        //обнавляем таймер подзема ВоркПендинг
                                        TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                                    }
                                    else
                                    {
                                        //если нет пристанища то спускаемся по лебедке/веревке
                                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[4]) == true)
                                        {
                                            driver.FindElement(By.XPath("//input[contains(@value,'ЛЕБЕДКЕ')]")).Click();
                                            System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                        }
                                        else
                                        {
                                            driver.FindElement(By.XPath("//input[contains(@value,'ВЕРЕВКЕ')]")).Click();
                                            System.Threading.Thread.Sleep(rnd.Next(459, 598));
                                        }
                                        //читаем таймер сколько спускаться
                                        //обнавляем таймер подзема ВоркПендинг
                                        TP_Underground = ToDateTime(driver.FindElement(By.CssSelector("div.timers span:nth-of-type(1)")).Text);
                                    }
                                }
                                catch { }

                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
        }

        public void OpenNewPand()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[8]) == true)
            {
                int currentPandaCount = Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Ящик Пандоры" + '\u0022' + " style")[0]);
                if (PandaCount < currentPandaCount)
                {
                    OpenSalePanda();
                    PandaCount = currentPandaCount;
                }
                if (PandaCount > currentPandaCount)
                {
                    PandaCount = currentPandaCount;
                }
            }
        }

        public void OpenSalePanda()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[8]) == true)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                //Лист всех панд
                IList<IWebElement> PandList = PandaList();
                for (int i = 0; i < PandList.Count; i++)
                {
                    try
                    {
                        IWebElement temp = PandList[i].FindElement(By.ClassName("level"));
                        //если уровень 1, то открываем
                        if (Convert.ToInt32(temp.Text) == 1)
                        {
                            //кликаем по бордеру чтоб появилась кнопка Открыть
                            PandList[i].FindElement(By.ClassName("border")).Click();
                            Delays();
                            driver.FindElement(By.LinkText("ОТКРЫТЬ")).Click();
                            Delays();
                            //Кликаем авоматически
                            driver.FindElement(By.XPath("//input[@value='АВТОМАТИЧЕСКИ']")).Click();
                            //переприсвоение листу панд новые значения
                            PandList = PandaList();
                        }
                    }
                    catch { }
                }
                int PandaCoun = PandList.Count;
                //продажа панд до заданного уровня
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[9]) == true)
                {
                    while (PandaSale(PandList, Convert.ToInt32(Convert.ToDecimal(ReadFromFile(SettingsFile, "UndergroundBox")[10]))))
                    {
                        //переприсвоение листу панд новые значения
                        PandList = PandaList();
                    }
                }
                //Дооткрывание панд
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[13]) == true)
                {
                    int currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                    while (PandaReOpen(PandList, Convert.ToInt32(Convert.ToDecimal(ReadFromFile(SettingsFile, "UndergroundBox")[14]))) && currenCry > 185)
                    {
                        //переприсвоение листу панд новые значения
                        PandList = PandaList();
                        //число оставшихся кри на считу
                        currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                    }
                }
            }
        }

        private IList<IWebElement> PandaList()
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            try
            {
                //стр персонажа
                driver.FindElement(By.Id("m1")).FindElement(By.XPath(".//b")).Click();
                //переходим в секцию пандор
                driver.FindElement(By.ClassName("inventory_4")).Click();
            }
            catch { }
            //Лист всех панд
            return driver.FindElements(By.XPath("//div[contains(@id,'panda')]"));
        }

        private bool PandaSale(IList<IWebElement> PandList, int MinPandaLvl)
        {
            //если произведена продажа, то флаг переходит в тру, если нет - то фолс
            bool RetValue = false;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            for (int i = 0; i < PandList.Count; i++)
            {
                try
                {
                    IWebElement temp = PandList[i].FindElement(By.ClassName("level"));
                    //если уровень меньше чем нужно то продаем
                    if (Convert.ToInt32(temp.Text) < MinPandaLvl)
                    {
                        //кликаем по бордеру чтоб появилась кнопка Продать и продаем
                        PandList[i].FindElement(By.ClassName("border")).Click();
                        Delays();
                        driver.FindElement(By.LinkText("ПРОДАТЬ")).Click();
                        Delays();
                        //Кликаем ОК - первый батон
                        driver.FindElement(By.TagName("button")).Click();
                        Delays();
                        //переводим флаг в тру, так как произведена продажа
                        RetValue = true;
                        break;
                    }
                }
                catch { }
            }
            return RetValue;
        }

        private bool PandaReOpen(IList<IWebElement> PandList, int MinPandaLvl)
        {
            //если произведена Дооткрытие, то флаг переходит в тру, если нет - то фолс
            bool RetValue = false;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            for (int i = 0; i < PandList.Count; i++)
            {
                try
                {
                    IWebElement temp = PandList[i].FindElement(By.ClassName("level"));
                    //если уровень больше чем нужно то дооткрываем
                    if (Convert.ToInt32(temp.Text) > MinPandaLvl && Convert.ToInt32(temp.Text) != 8)
                    {
                        //кликаем по бордеру чтоб появилась кнопка ОТКРЫТЬ и ОТКРЫВАЕМ
                        PandList[i].FindElement(By.ClassName("border")).Click();
                        Delays();
                        driver.FindElement(By.LinkText("ОТКРЫТЬ")).Click();
                        Delays();
                        driver.FindElement(By.XPath("//input[@value='ОТКРЫТЬ ВСЕ']")).Click();
                        Delays();
                        //переводим флаг в тру, так как произведена продажа
                        RetValue = true;
                        break;
                    }
                }
                catch { }
            }
            return RetValue;
        }

        public void Fight()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[1]) || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[2]) || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[5]))
            {
                try
                {
                    if (CharacterIsFree() == true)
                    {
                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[9]) == true && Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[14]) == false)
                        {

                            GetPet(FightPetProvider());
                        }
                        else
                        {
                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[14]) == true && ImmunTime() > Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[15]))
                            {
                                GetPet(FightPetProvider());
                            }
                        }
                        try
                        {
                            int FightCount = 0;
                            do
                            {
                                FightMonster();
                                FightZorro();
                                FightCommon();
                                FightCount++;
                                if (FightCount > 3)
                                {
                                    break;
                                }
                            }
                            while (TimeToFight() == true);
                        }
                        catch { }
                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[9]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[14]) == true)
                        {
                            SetPet();
                        }
                    }
                }
                catch { }
            }
        }

        private void FightMonster()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[1]) == true && Timer_FightMonster.CompareTo(DateTime.Now) < 0)
            {
                try
                {
                    //возращаем в бодалку
                    if (driver.Title.Contains("Бодалка") == false)
                    {
                        driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                        Delays();
                    }

                    //если всех подряд
                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[29]))
                    {
                        //поиск страшилки
                        driver.FindElement(By.XPath("//form/input[contains(@value,'СТРАШИЛКУ')]")).Click();
                        Delays();
                    }

                    //если по уровню
                    int currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                    if (currenCry > 1)
                    {
                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[30]))
                        {
                            //валюта кристаллы
                            driver.FindElement(By.CssSelector(".sub_title_other input[value='1']")).Click();
                            Delays();
                            string selector = string.Format("//option[text()='{0}']", ReadFromFile(SettingsFile, "FightBox")[31]);
                            driver.FindElement(By.CssSelector(selector)).Click();
                            Delays();
                            driver.FindElement(By.CssSelector(".watch_attack_level input[value='АТАКА']")).Click();
                            Delays();
                        }
                    }
                    else
                    {
                        //если крисов мало, то всех подряд
                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[29]))
                        {
                            //поиск страшилки
                            driver.FindElement(By.XPath("//form/input[contains(@value,'СТРАШИЛКУ')]")).Click();
                            Delays();
                        }
                    }

                    //кликаем напасть
                    driver.FindElement(By.XPath("//form/input[@value='НАПАСТЬ']")).Click();
                    Delays();

                    //возращаем в бодалку
                    driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                    Delays();
                }
                catch { }

                IWebElement Timer = driver.FindElement(By.CssSelector(".default tr:nth-of-type(2) td:nth-of-type(2) span"));
                //читаем таймер до след напа

                Timer_FightMonster = ToDateTime(Timer.Text);
            }
        }

        private void FightZorro()
        {
            //Зорро бодалка
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[2]) == true && Timer_FightZorro.CompareTo(DateTime.Now) < 0)
            {
                try
                {
                    //возращаем в бодалку
                    if (driver.Title.Contains("Бодалка") == false)
                    {
                        driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(300, 480));
                    }

                    try
                    {
                        int Temp_Count = 0;
                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[3]) == true)
                        {
                            IWebElement ZorroLvl = driver.FindElement(By.CssSelector(".default tr:nth-of-type(2) .half:nth-of-type(1) .flright:nth-of-type(2) input"));
                            while (ZorroLvl.Displayed && Temp_Count < 6)
                            {
                                ZorroLvl.Click();
                                Temp_Count++;
                                ZorroLvl = driver.FindElement(By.CssSelector(".default tr:nth-of-type(2) .half:nth-of-type(1) .flright:nth-of-type(2) input"));
                            }
                        }
                        else
                        {
                            IWebElement ZorroList = driver.FindElement(By.CssSelector(".default tr:nth-of-type(2) .half:nth-of-type(1) .watch_attack_type .flright:nth-of-type(1) input"));
                            while (ZorroList.Displayed && Temp_Count < 6)
                            {
                                ZorroList.Click();
                                Temp_Count++;
                                ZorroList = driver.FindElement(By.CssSelector(".default tr:nth-of-type(2) .half:nth-of-type(1) .watch_attack_type .flright:nth-of-type(1) input"));
                            }
                        }
                    }
                    catch
                    {
                        //Напасть
                        try
                        {
                            System.Threading.Thread.Sleep(rnd.Next(300, 312));
                            driver.FindElement(By.XPath("//input[@value='НАПАСТЬ']")).Click();
                        }
                        catch { }
                    }

                    //Определяем страницу, если не на бодалке, то переходим в нее
                    try
                    {
                        if (driver.Url.Contains("dozor") == false || driver.Url.Contains("arena"))
                        {
                            driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(154, 394));
                        }
                    }
                    catch { }

                    IWebElement Timer = driver.FindElement(By.CssSelector(".default tr:nth-of-type(2) .half:nth-of-type(1) span"));
                    //читаем таймер до след напа
                    Timer_FightZorro = ToDateTime(Timer.Text);
                }
                catch { }
            }
        }

        private void FightCommon()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[5]) == true && Timer_FightCommon.CompareTo(DateTime.Now) < 0)
            {
                int counter = 2;
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[8]) == true)
                {
                    counter = 0;
                }
                while (counter < 3)
                {
                    try
                    {
                        //возращаем в бодалку
                        if (driver.Title.Contains("Бодалка") == false)
                        {
                            driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(300, 480));
                        }

                        try
                        {
                            int Temp_Count = 0;
                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[6]) == true)
                            {
                                IWebElement FightLvl = driver.FindElement(By.CssSelector(".default tr:nth-of-type(1) .half:nth-of-type(1) .flright:nth-of-type(2) input"));
                                while (FightLvl.Displayed && Temp_Count < 7)
                                {
                                    FightLvl.Click();
                                    Temp_Count++;
                                    FightLvl = driver.FindElement(By.CssSelector(".default tr:nth-of-type(1) .half:nth-of-type(1) .flright:nth-of-type(2) input"));
                                }
                            }
                            else
                            {
                                IWebElement FightList = driver.FindElement(By.CssSelector(".default tr:nth-of-type(1) .half:nth-of-type(1) .watch_attack_type .flright:nth-of-type(1) input"));
                                while (FightList.Displayed && Temp_Count < 7)
                                {
                                    FightList.Click();
                                    Temp_Count++;
                                    FightList = driver.FindElement(By.CssSelector(".default tr:nth-of-type(1) .half:nth-of-type(1) .watch_attack_type .flright:nth-of-type(1) input"));
                                }
                            }
                        }
                        catch
                        {
                            //Напасть
                            try
                            {
                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[16]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[22]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[23]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[24]) == true)
                                {
                                    if (CommonFightLimits() == true)
                                    {
                                        driver.FindElement(By.XPath("//input[@value='НАПАСТЬ']")).Click();
                                    }
                                    //else counter--;
                                }
                                else driver.FindElement(By.XPath("//input[@value='НАПАСТЬ']")).Click();
                            }
                            catch { }
                        }

                        //Определяем страницу, если не на бодалке, то переходим в нее
                        try
                        {
                            try
                            {
                                //если есть аватар противника, то переходим в бодалку
                                IWebElement tempAvatar = driver.FindElement(By.CssSelector(".default.attack"));
                                driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                                System.Threading.Thread.Sleep(rnd.Next(154, 201));
                            }
                            catch { }
                            if (driver.Url.Contains("dozor") == false || driver.Url.Contains("arena"))
                            {
                                driver.FindElement(By.Id("m8")).FindElement(By.XPath(".//b")).Click();
                                System.Threading.Thread.Sleep(rnd.Next(154, 200));
                            }
                        }
                        catch { }
                    }
                    catch { }
                    counter++;
                }
                try
                {
                    IWebElement Timer = driver.FindElement(By.CssSelector(".default tr:nth-of-type(1) .half:nth-of-type(1) span"));
                    //читаем таймер до след напа
                    Timer_FightCommon = ToDateTime(Timer.Text);
                }
                catch { }
            }
        }

        private bool CommonFightLimits()
        {
            bool retValue = true;
            //контрль по статам
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[16]) == true)
            {
                IList<IWebElement> Stats = driver.FindElements(By.ClassName("c4"));
                if (Convert.ToInt32(Stats[0].Text) >= Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[17]) &&
                    Convert.ToInt32(Stats[1].Text) >= Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[18]) &&
                    Convert.ToInt32(Stats[2].Text) >= Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[19]) &&
                    Convert.ToInt32(Stats[3].Text) >= Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[20]) &&
                    Convert.ToInt32(Stats[4].Text) >= Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[21]))
                {
                    retValue = false;
                }
            }

            //контрль по морали
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[22]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[23]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[24]) == true)
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[22]) == true)
                {
                    try
                    {
                        IWebElement temp = driver.FindElement(By.XPath(".//div[@id='body']//p[3]/span"));
                        if (Convert.ToInt32(temp.Text) < 0)
                        {
                            retValue = false;
                        }
                    }
                    catch { }
                }

                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[23]) == true)
                {
                    try
                    {
                        IWebElement temp = driver.FindElement(By.XPath(".//div[@id='body']//p[3]/span"));
                        if (Convert.ToInt32(temp.Text) > 0)
                        {
                            retValue = false;
                        }
                    }
                    catch { }
                }

                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[24]) == true)
                {
                    try
                    {
                        IWebElement temp = driver.FindElement(By.XPath(".//div[@id='body']//p[3]/span"));
                        if (Convert.ToInt32(temp.Text) == 0)
                        {
                            retValue = false;
                        }
                    }
                    catch { }
                }
            }

            return retValue;
        }

        private bool TimeToFight()
        {
            bool RetVal = false;
            try
            {
                DateTime Timer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
                DateTime duration = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
                duration = ToDateTime("00:00:11");
                //TimeSpan duration = new TimeSpan(0, 0, 0, 11);
                try
                {
                    Timer = ToDateTime(driver.FindElement(By.Id("rmenu1")).FindElement(By.XPath("div[1]/a[2]/span")).Text);
                }
                catch
                {
                    return RetVal = true;
                }
                //if (DateTime.Now.Add(duration).CompareTo(Timer) > 0)
                if (duration.CompareTo(Timer) > 0)
                {
                    //пока фактическое время не будет больше считанного таймера до нападения
                    while (Timer.CompareTo(DateTime.Now) > 0)
                    {
                        System.Threading.Thread.Sleep(300);
                    }
                    RetVal = true;
                    if (driver.FindElement(By.Id("rmenu1")).FindElement(By.XPath("div[1]/a[2]/span")).Text == "00:00:00")
                    {
                        driver.FindElement(By.LinkText("Бодалка")).Click();
                    }
                }
                return RetVal;
            }
            catch
            {
                return RetVal = true;
            }
        }

        private void GetPet(PetType pet = PetType.currentPet)
        {
            try
            {
                //если есть линка посадить в клетку, то проверяем на правильность зверя
                if (driver.FindElement(By.XPath("//a[@title='Посадить в клетку']")).Displayed == true)
                {
                    //если зверь не дефолтный, то сверяем текущего зверя с нужным
                    if (PetNumberProvider(pet) != 0)
                    {
                        string css = string.Format(".pet{0}", PetNumberProvider(pet));
                        //Если нет иконки нужного зверя попадаем в кетч и садим невалидного зверя в клетку,а нужного достаем
                        try
                        {
                            IWebElement temp = driver.FindElement(By.CssSelector(css));
                        }
                        catch
                        {
                            SetPet();

                            //Деревня
                            driver.FindElement(By.Id("m3")).FindElement(By.XPath(".//b")).Click();
                            Delays();
                            //Жилище
                            driver.FindElement(By.XPath(".//div[text()='Жилище']")).Click();
                            Delays();
                            //клетка
                            driver.FindElement(By.LinkText("Клетка")).Click();
                            Delays();
                            //вытащить с клетки
                            if (PetNumberProvider(pet) == 0)
                            {
                                driver.FindElement(By.XPath("//input[@value='ВЫПУСТИТЬ ИЗ КЛЕТКИ']")).Click();
                            }
                            else
                            {
                                try
                                {
                                    string xPath = string.Format("//img[contains(@src,'Pet_{0}')]/ancestor::div[contains(@class,'round_block_round_border')]//input[contains(@value,'ВЫПУСТИТЬ')]", PetNumberProvider(pet));
                                    driver.FindElement(By.XPath(xPath)).Click();
                                    Delays();
                                }
                                catch
                                {
                                    string xPath = string.Format("//img[contains(@src,'Pet_{0}s')]/..//input[@value='ВЗЯТЬ В БОЙ']", PetNumberProvider(pet));
                                    driver.FindElement(By.XPath(xPath)).Click();
                                    Delays();
                                };
                            }
                        }
                    }
                }
            }
            //проверка есть ли зверь
            catch
            {
                //Деревня
                driver.FindElement(By.Id("m3")).FindElement(By.XPath(".//b")).Click();
                Delays();
                //Жилище
                driver.FindElement(By.XPath(".//div[text()='Жилище']")).Click();
                Delays();
                //клетка
                driver.FindElement(By.LinkText("Клетка")).Click();
                Delays();
                //вытащить с клетки
                if (PetNumberProvider(pet) == 0)
                {
                    driver.FindElement(By.XPath("//input[@value='ВЫПУСТИТЬ ИЗ КЛЕТКИ']")).Click();
                }
                else
                {
                    try
                    {
                        string xPath = string.Format("//img[contains(@src,'Pet_{0}')]/ancestor::div[contains(@class,'round_block_round_border')]//input[contains(@value,'ВЫПУСТИТЬ')]", PetNumberProvider(pet));
                        driver.FindElement(By.XPath(xPath)).Click();
                        Delays();
                    }
                    catch
                    {
                        string xPath = string.Format("//img[contains(@src,'Pet_{0}s')]/..//input[@value='ВЗЯТЬ В БОЙ']", PetNumberProvider(pet));
                        driver.FindElement(By.XPath(xPath)).Click();
                        Delays();
                    };
                }
            }
        }

        private int PetNumberProvider(PetType pet)
        {
            int retPet = 0;
            switch (pet)
            {
                case PetType.worm:
                    {
                        retPet = 7;
                    }
                    break;

                //выпустить синего духа
                case PetType.wormBlueSoul:
                    {
                        retPet = 16;
                    }
                    break;

                //выпустить кита
                case PetType.whale:
                    {
                        retPet = 21;
                    }
                    break;

                //выпустить попоугая
                case PetType.parot:
                    {
                        retPet = 23;
                    }
                    break;
                default: break;
            }
            return retPet;
        }

        private enum PetType
        {
            currentPet,
            worm,
            wormBlueSoul,
            whale,
            parot
        }

        private PetType PetTypeProvider()
        {
            var retRep = PetType.currentPet;

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[15]))
            {
                retRep = PetType.wormBlueSoul;
            }

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[16]))
            {
                retRep = PetType.worm;
            }

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "UndergroundBox")[17]))
            {
                retRep = PetType.currentPet;
            }

            return retRep;
        }

        private PetType FightPetProvider()
        {
            var retRep = PetType.currentPet;

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[24]))
            {
                retRep = PetType.whale;
            }

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[25]))
            {
                retRep = PetType.parot;
            }

            return retRep;
        }

        private void SetPet()
        {
            //проверка есть ли зверь
            if (driver.FindElement(By.XPath("//a[@title='Посадить в клетку']")).Displayed == true)
            {
                driver.FindElement(By.XPath("//a[@title='Посадить в клетку']")).Click();
                System.Threading.Thread.Sleep(rnd.Next(541, 789));
                driver.FindElement(By.LinkText("КЛЁВА")).Click();
                /*
                //Деревня
                driver.FindElement(By.Id("m3")).FindElement(By.XPath(".//b")).Click();
                System.Threading.Thread.Sleep(rnd.Next(300, 480));
                //Жилище
                driver.FindElement(By.XPath(".//div[text()='Жилище']")).Click();
                System.Threading.Thread.Sleep(rnd.Next(598, 899));
                //клетка
                driver.FindElement(By.LinkText("Клетка")).Click();
                System.Threading.Thread.Sleep(rnd.Next(598, 899));
                //вытащить с клетки
                driver.FindElement(By.XPath("//input[@value='ВЫПУСТИТЬ ИЗ КЛЕТКИ']")).Click();
                System.Threading.Thread.Sleep(rnd.Next(300, 480));
                 * */
            }
        }

        public void GoldDiscard()
        {
            try
            {
                int Gold = Convert.ToInt32(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[5]);
                if (Gold > 0)
                    if (CurrentWork("Спуск в подземелье") == false)
                    {
                        //для себя
                        int goldForMe = 0;
                        try
                        {
                            goldForMe = Convert.ToInt32(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[6]);
                        }
                        catch { }
                        string CurrentGold = driver.FindElement(By.Id("gold")).FindElement(By.TagName("b")).Text.Replace(".", "");
                        CurrentGold = Convert.ToString(Convert.ToInt32(CurrentGold) - goldForMe);
                        //Обрезаем копейки
                        if (CurrentGold.Length > 3)
                        {
                            CurrentGold = CurrentGold.Remove(CurrentGold.Length - 3, 3);
                            CurrentGold += "000";
                        }
                        if (Convert.ToInt32(CurrentGold) >= Gold)
                        {
                            string text = string.Format("Сливаем голд в казну: текущее кол-во голда {0}", CurrentGold);
                            Logger(text);
                            //клан
                            driver.FindElement(By.LinkText("Клан")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(300, 480));
                            driver.FindElement(By.CssSelector(".clan_main_treasury")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 899));
                            driver.FindElement(By.XPath("//input[@type='text']")).Clear();
                            //int DiscardAmount = Convert.ToInt32(CurrentGold) - rnd.Next(978, 1499);
                            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys(Convert.ToString(CurrentGold));
                            System.Threading.Thread.Sleep(rnd.Next(789, 999));
                            driver.FindElement(By.XPath("//input[@value='ВНЕСТИ']")).Click();
                        }

                    }
            }
            catch { }
        }

        public void Healing()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "HealBox")[1]) == true)
            {
                int ExpectedHPlvl = 0;
                try
                {
                    ExpectedHPlvl = Convert.ToInt32(ReadFromFile(SettingsFile, "HealBox")[2]);
                }
                catch { }
                string HealLevel;

                try
                {
                    //check current HP level
                    HealLevel = driver.FindElement(By.Id("char")).FindElement(By.TagName("i")).Text;
                    HealLevel = HealLevel.TrimEnd('%');
                    //if HealLevel less that expected that healing
                    if (Convert.ToInt32(HealLevel) <= ExpectedHPlvl)
                    {
                        try
                        {
                            //Click heal (Botle) icon
                            driver.FindElement(By.Id("char")).FindElement(By.TagName("a")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 1365));
                            //Buy Read bottle
                            driver.FindElement(By.Id("field_potion_3_2")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 1145));
                            //Drink bottle
                            driver.FindElement(By.Id("potion_td_3")).FindElement(By.TagName("a")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 1498));
                            //Close Healing form
                            driver.FindElement(By.CssSelector(".box_x_button")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 991));
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        public void PetHealing()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "HealBox")[3]) == true)
            {
                string HealLevel;
                try
                {
                    int ExpectedHPlvlPet = Convert.ToInt32(ReadFromFile(SettingsFile, "HealBox")[4]);
                    //check pet HR lvl
                    HealLevel = driver.FindElement(By.Id("pet")).FindElement(By.TagName("i")).Text;
                    HealLevel = HealLevel.TrimEnd('%');
                    //if HealLevel less that expected that healing
                    if (Convert.ToInt32(HealLevel) <= ExpectedHPlvlPet)
                    {
                        try
                        {
                            //Click heal (Botle) icon
                            driver.FindElement(By.Id("pet")).FindElement(By.XPath(".//a[2]")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 1365));
                            //Buy 2 bottle
                            driver.FindElement(By.Id("field_potion_5_2")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 1145));
                            //Drink bottle
                            driver.FindElement(By.Id("potion_td_5")).FindElement(By.TagName("a")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 1498));
                            //Close Healing form
                            driver.FindElement(By.CssSelector(".box_x_button")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(759, 991));
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        public void FightImmuns()
        {
            bool Ogl = Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[10]);
            bool Anti = Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[11]);
            if (Ogl == true || Anti == true)
            {
                if (Timer_FightImmunOgl.CompareTo(DateTime.Now) < 0 || Timer_FightImmunAnti.CompareTo(DateTime.Now) < 0)
                {
                    Timer_FightImmunOgl = ToDateTime(GetResourceValue("Иммунитет к заговору " + "&quot;" + "Оглушка" + "&quot;" + ".")[0]);
                    Timer_FightImmunAnti = ToDateTime(GetResourceValue("Иммунитет к заговору " + "&quot;" + "Антикрут" + "&quot;" + ".")[0]);

                    if (Timer_FightImmunOgl.CompareTo(DateTime.Now) < 0 || Timer_FightImmunAnti.CompareTo(DateTime.Now) < 0)
                    {
                        try
                        {
                            //переход в деревню
                            driver.FindElement(By.LinkText("Деревня")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 798));
                            //святилище
                            driver.FindElement(By.XPath(".//div[text()='Святилище']")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 798));
                            //Услуги шамана
                            driver.FindElement(By.LinkText("Услуги Шамана")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 798));
                            //Выбираем нужные иммуны
                            FightImmunOgl(Ogl);
                            FightImmunAnti(Anti);
                            //выбераем ресурс и активируем
                            ImmunSelectPirCry();
                        }
                        catch { }
                    }
                }
            }
        }

        private void FightImmunOgl(bool ShoulBy)
        {
            if (ShoulBy == true && Timer_FightImmunOgl.CompareTo(DateTime.Now) < 0)
            {
                try
                {
                    driver.FindElement(By.XPath("//input[@rel='1']")).Click();
                }
                catch { }
            }
        }

        private void FightImmunAnti(bool ShoulBy)
        {
            if (ShoulBy == true && Timer_FightImmunAnti.CompareTo(DateTime.Now) < 0)
            {
                try
                {
                    driver.FindElement(By.XPath("//input[@rel='2']")).Click();
                }
                catch { }
            }
        }

        private void ImmunSelectPirCry()
        {
            try
            {
                IList<IWebElement> Radiobuttons = driver.FindElement(By.Id("shaman_serv_1")).FindElements(By.XPath(".//input[@type='radio']"));

                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[12]) == true)
                {
                    Radiobuttons[0].Click();
                }
                else Radiobuttons[1].Click();
                //активировать
                driver.FindElement(By.XPath("//input[@value='АКТИВИРОВАТЬ']")).Click();
            }
            catch { }
        }

        public void NegativeEffects()
        {
            bool Poison = false;
            bool Gold = false;
            bool Anti = false;
            string PageContent = driver.PageSource;

            //Выпиваем оборотку
            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[25]) == true)
                {
                    if (!PageContent.Contains("Действие оборотного зелья: возможность нападения 3 раза подряд"))
                    {
                        driver.FindElement(By.LinkText("Персонаж")).Click();
                        Delays();
                        //Click on botles section
                        driver.FindElement(By.CssSelector(".tabs_mini div:nth-of-type(2)")).Click();
                        Delays();
                        Actions builder = new Actions(driver);
                        builder.MoveToElement(driver.FindElement(By.CssSelector(".ico_item_404"))).Build().Perform();
                        driver.FindElement(By.XPath("//div[contains(@class,'ico_item_404')]//span[text()='ВЫПИТЬ']")).Click();
                        Delays();
                        driver.FindElement(By.XPath("//div[contains(@class,'box_controls')]//span[text()='ВЫПИТЬ']")).Click();
                        try
                        {
                            //если есть надпись что можно стать козленочком
                            driver.FindElement(By.XPath("//span[text()='close']")).Click();
                            Delays();
                        }
                        catch { }
                    }
                }
            }
            catch { }

            //Выпить зелье огромности
            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[29]))
                {
                    if (Timer_BiggestPotion.CompareTo(DateTime.Now) < 0)
                    {
                        driver.FindElement(By.LinkText("Персонаж")).Click();
                        Delays();
                        //Click on botles section
                        driver.FindElement(By.CssSelector(".tabs_mini div:nth-of-type(2)")).Click();
                        Delays();
                        Actions builder = new Actions(driver);
                        builder.MoveToElement(driver.FindElement(By.CssSelector(".ico_item_754"))).Build().Perform();
                        driver.FindElement(By.XPath("//div[contains(@class,'ico_item_754')]//span[text()='ВЫПИТЬ']")).Click();
                        Delays();
                        driver.FindElement(By.XPath("//div[contains(@class,'box_controls')]//span[text()='ВЫПИТЬ']")).Click();
                        Timer_BiggestPotion = ToDateTime(GetResourceValue("Ваши зверушки увеличены в размере и не могут быть проглочены Китушей")[0]);
                        try
                        {
                            //если есть надпись что можно стать козленочком
                            driver.FindElement(By.XPath("//span[text()='close']")).Click();
                            Delays();
                        }
                        catch { }
                    }
                }
            }
            catch { }

            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "EffectsBox")[1]) == true)
                {
                    if (PageContent.Contains("Действие заговора Отравка"))
                    {
                        Poison = true;
                    }
                }
            }
            catch { }

            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "EffectsBox")[2]) == true)
                {
                    if (PageContent.Contains("Действие заговора Золотая чума"))
                    {
                        Gold = true;
                    }
                }
            }
            catch { }

            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "EffectsBox")[3]) == true)
                {
                    if (PageContent.Contains("Действие заговора Антикрут"))
                    {
                        Anti = true;
                    }
                }
            }
            catch { }

            try
            {
                if (Poison == true || Gold == true || Anti == true)
                {
                    //переход в деревню
                    driver.FindElement(By.LinkText("Деревня")).Click();
                    Delays();
                    //святилище
                    driver.FindElement(By.XPath(".//div[text()='Святилище']")).Click();
                    Delays();
                    //Карма
                    if (driver.FindElement(By.XPath("//input[@value='НАЧАТЬ']")).Displayed == true)
                    {
                        driver.FindElement(By.ClassName("cmd_split_504_u")).Click();
                    }
                    if (Poison == true)
                    {
                        driver.FindElement(By.XPath("//input[@value='109']")).Click();
                        Delays();
                    }
                    if (Gold == true)
                    {
                        driver.FindElement(By.XPath("//input[@value='118']")).Click();
                        Delays();
                    }
                    if (Anti == true)
                    {
                        driver.FindElement(By.XPath("//input[@value='122']")).Click();
                        Delays();
                    }
                    //Снять
                    driver.FindElement(By.XPath("//input[@value='СНЯТЬ']")).Click();
                }
            }
            catch { }
        }

        public void PandaEffects()
        {
            try
            {
                if (Timer_PandaEffect1.CompareTo(DateTime.Now) < 0 || Timer_PandaEffect2.CompareTo(DateTime.Now) < 0 || Timer_PandaEffect3.CompareTo(DateTime.Now) < 0 || Timer_PandaEffect4.CompareTo(DateTime.Now) < 0)
                {
                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[1]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[2]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[3]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[4]) == true)
                    {
                        PandaEffectsTimersUpdate();
                        if (Timer_PandaEffect1.CompareTo(DateTime.Now) < 0 || Timer_PandaEffect2.CompareTo(DateTime.Now) < 0 || Timer_PandaEffect3.CompareTo(DateTime.Now) < 0 || Timer_PandaEffect4.CompareTo(DateTime.Now) < 0)
                        {

                            //переход в деревню
                            driver.FindElement(By.LinkText("Деревня")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 798));
                            //святилище
                            driver.FindElement(By.XPath(".//div[text()='Святилище']")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 798));
                            //эффекты панд
                            driver.FindElement(By.LinkText("Эффекты Пандоры")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(499, 798));

                            string PageContent = driver.PageSource;

                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[1]) == true && PageContent.Contains("Увеличение шанса получить билет именно на большую поляну, а не на малую.") == false)
                            {
                                driver.FindElement(By.XPath("//input[@name='pandora_46']")).Click();
                            }

                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[2]) == true && PageContent.Contains("Увеличение в 2 раза вероятности получения билета на большую или малую поляну.") == false)
                            {
                                driver.FindElement(By.XPath("//input[@name='pandora_47']")).Click();
                            }

                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[3]) == true && PageContent.Contains("Любой получаемый опыт увеличивается в 2 раза.") == false)
                            {
                                driver.FindElement(By.XPath("//input[@name='pandora_499']")).Click();
                            }

                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[4]) == true && PageContent.Contains("Увеличение в 2 раза дохода с побежденных страшилок.") == false)
                            {
                                driver.FindElement(By.XPath("//input[@name='pandora_49']")).Click();
                            }

                            switch (ReadFromFile(SettingsFile, "PandaEffectsBox")[5])
                            {
                                case "1 день":
                                    driver.FindElement(By.Id("buy_per_days")).FindElement(By.XPath(".//option[@value='1']")).Click();
                                    break;

                                case "2 дня":
                                    driver.FindElement(By.Id("buy_per_days")).FindElement(By.XPath(".//option[@value='2']")).Click();
                                    break;

                                case "3 дня":
                                    driver.FindElement(By.Id("buy_per_days")).FindElement(By.XPath(".//option[@value='3']")).Click();
                                    break;

                                case "7 дней":
                                    driver.FindElement(By.Id("buy_per_days")).FindElement(By.XPath(".//option[@value='4']")).Click();
                                    break;

                                case "14 дней":
                                    driver.FindElement(By.Id("buy_per_days")).FindElement(By.XPath(".//option[@value='5']")).Click();
                                    break;

                                case "28 дней":
                                    driver.FindElement(By.Id("buy_per_days")).FindElement(By.XPath(".//option[@value='6']")).Click();
                                    break;

                                default:
                                    driver.FindElement(By.Id("buy_per_days")).FindElement(By.XPath(".//option[@value='1']")).Click();
                                    break;
                            }

                            IList<IWebElement> Radio = driver.FindElements(By.XPath("//input[@type='radio']"));

                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[6]) == true)
                            {
                                Radio[0].Click();
                            }
                            else Radio[1].Click();

                            driver.FindElement(By.XPath("//input[@value='АКТИВИРОВАТЬ']")).Click();
                        }
                    }
                }
            }
            catch { }
        }

        private void PandaEffectsTimersUpdate()
        {
            string PageContent = driver.PageSource;

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[1]) == true)
            {
                if (PageContent.Contains("Увеличение шанса получить билет именно на большую поляну, а не на малую.") == true)
                {
                    Timer_PandaEffect1 = ToDateTime(GetResourceValue("Увеличение шанса получить билет именно на большую поляну, а не на малую.")[0]);
                }
            }
            else Timer_PandaEffect1 = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[2]) == true)
            {
                if (PageContent.Contains("Увеличение в 2 раза вероятности получения билета на большую или малую поляну.") == true)
                {
                    Timer_PandaEffect2 = ToDateTime(GetResourceValue("Увеличение в 2 раза вероятности получения билета на большую или малую поляну.")[0]);
                }
            }
            else Timer_PandaEffect2 = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[3]) == true)
            {
                if (PageContent.Contains("Любой получаемый опыт увеличивается в 2 раза.") == true)
                {
                    Timer_PandaEffect3 = ToDateTime(GetResourceValue("Любой получаемый опыт увеличивается в 2 раза.")[0]);
                }
            }
            else Timer_PandaEffect3 = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PandaEffectsBox")[4]) == true)
            {
                if (PageContent.Contains("Увеличение в 2 раза дохода с побежденных страшилок.") == true)
                {
                    Timer_PandaEffect4 = ToDateTime(GetResourceValue("Увеличение в 2 раза дохода с побежденных страшилок.")[0]);
                }
            }
            else Timer_PandaEffect4 = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        public void FarCountrys()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FarCountrBox")[1]) == true)
            {
                if (Timer_FarCountrys.CompareTo(DateTime.Now) < 0)
                {
                    if (Convert.ToInt32(GetResourceValue("Ртутный порошок")[1]) - 5 > Convert.ToInt32(GetResourceValue("Ртутный порошок")[0]))
                    {
                        try
                        {
                            driver.FindElement(By.XPath("//a/div[contains(@class,'f9')]")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(899, 999));
                            driver.FindElement(By.LinkText("ПОЛОЖИТЬ ВСЕ")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(799, 999));
                            IList<IWebElement> Boats = driver.FindElements(By.XPath("//input[@value='НАНЯТЬ']"));
                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FarCountrBox")[2]) == true)
                            {
                                Boats[2].Click();
                            }
                            else Boats[3].Click();
                        }
                        catch { }
                        Timer_FarCountrys = ToDateTime(GetResourceValue("Время до возвращения корабля из дальних стран.")[0]);
                    }
                }
            }
        }

        public void MineByInventory()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MineBox")[10]) == true)
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MineBox")[8]) == true)
                {
                    ByMineInventoryCommon(Convert.ToInt32(GetResourceValue("Кирка")[0]), Convert.ToInt32(GetResourceValue("Очки")[0]), Convert.ToInt32(GetResourceValue("Каска")[0]));
                }
                else ByMineInventoryWorker();
            }
        }

        private void ByMineInventoryCommon(int Pick, int Glasses, int Helmet)
        {
            if (Pick < 5 || Glasses < 5 || Helmet < 5)
            {
                if (driver.Title.Contains("Карьер") == false)
                {
                    try
                    {
                        //go to the mine
                        driver.FindElement(By.XPath("//a/div[contains(@class,'f43')]")).Click();
                    }
                    catch { }
                }
                //перешли на секцию с обынчными инструментами
                try
                {
                    if (driver.FindElement(By.CssSelector("#buy_block_1 .padding5 tr:nth-of-type(1) b:nth-of-type(1)")).Text.Contains("Трудяги"))
                    {
                        driver.FindElement(By.XPath(".//u[text()='Шахтерам']")).Click();
                    }
                }
                catch { }
                if (Pick < 5)
                {
                    IList<IWebElement> byButtons = driver.FindElements(By.XPath(".//input[@value='КУПИТЬ']"));
                    byButtons[0].Click();
                }
                if (Glasses < 5)
                {
                    IList<IWebElement> byButtons = driver.FindElements(By.XPath(".//input[@value='КУПИТЬ']"));
                    byButtons[1].Click();
                }
                if (Helmet < 5)
                {
                    IList<IWebElement> byButtons = driver.FindElements(By.XPath(".//input[@value='КУПИТЬ']"));
                    byButtons[2].Click();
                }
            }
        }

        private void ByMineInventoryWorker()
        {
            if (driver.Title.Contains("Карьер") == false)
            {
                try
                {
                    //go to the mine
                    driver.FindElement(By.XPath("//a/div[contains(@class,'f43')]")).Click();
                }
                catch { }
            }
            //перешли на секцию с обынчными инструментами
            try
            {
                if (driver.FindElement(By.CssSelector("#buy_block_1 .padding5 tr:nth-of-type(1) b:nth-of-type(1)")).Text.Contains("Трудяги") == false)
                {
                    driver.FindElement(By.XPath(".//u[text()='Шахтерам']")).Click();
                }
            }
            catch
            {
                driver.FindElement(By.XPath(".//u[text()='Шахтерам']")).Click();
            }
            IList<IWebElement> coun = driver.FindElements(By.CssSelector("#buy_block_1 .padding5 tr:nth-of-type(2) b:nth-of-type(1)"));
            if (Convert.ToInt32(coun[0].Text) < 5)
            {
                IList<IWebElement> byButtons = driver.FindElements(By.XPath(".//input[@value='КУПИТЬ']"));
                byButtons[0].Click();
            }
            coun = driver.FindElements(By.CssSelector("#buy_block_1 .padding5 tr:nth-of-type(4) b:nth-of-type(1)"));
            if (Convert.ToInt32(coun[0].Text) < 5)
            {
                IList<IWebElement> byButtons = driver.FindElements(By.XPath(".//input[@value='КУПИТЬ']"));
                byButtons[1].Click();
            }
            coun = driver.FindElements(By.CssSelector("#buy_block_1 .padding5 tr:nth-of-type(6) b:nth-of-type(1)"));
            if (Convert.ToInt32(coun[0].Text) < 5)
            {
                IList<IWebElement> byButtons = driver.FindElements(By.XPath(".//input[@value='КУПИТЬ']"));
                byButtons[2].Click();
            }
        }

        public void SellSoap()
        {
            int soapCount = 0;
            try
            {
                soapCount = Convert.ToInt32(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[7]);
                string test = GetResourceValue("title=" + '\u0022' + "Мыльный камень" + '\u0022' + " style")[0].Replace(".", "");
                int currentSoapCount = Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Мыльный камень" + '\u0022' + " style")[0].Replace(".", ""));
                if (currentSoapCount >= soapCount)
                {
                    driver.FindElement(By.LinkText("Гавань")).Click();
                    driver.FindElement(By.LinkText("Торговая площадка")).Click();
                    driver.FindElement(By.LinkText("ПРОДАЖА")).Click();
                    //Находим нужноую секцию продажи для мыла
                    IWebElement soapSection = driver.FindElement(By.XPath(".//b[@title='Мыльный камень']/ancestor::div[contains(@class,'bmarket_harbour_sell_d')]"));
                    soapSection.FindElement(By.XPath(".//a[@title='Максимум']")).Click();
                    driver.FindElement(By.XPath(".//input[@value='ВЫСТАВИТЬ']")).Click();
                }
            }
            catch { }
        }

        public void BySlaves()
        {
            int slavesCount = 0;
            try
            {
                slavesCount = Convert.ToInt32(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[8]);
                int currentSlavesCount = Convert.ToInt32(GetResourceValue("title=" + '\u0022' + "Количество занятых мест на плантации" + '\u0022' + " style")[0]);
                if (currentSlavesCount <= slavesCount)
                {
                    driver.FindElement(By.LinkText("Гавань")).Click();
                    driver.FindElement(By.LinkText("Торговая площадка")).Click();
                    driver.FindElement(By.XPath(".//option[text()='Раб людишко']")).Click();
                    //двигаем бегунок
                    IWebElement Slider = driver.FindElement(By.CssSelector(".ui-slider-handle.ui-state-default.ui-corner-all"));
                    Actions builder = new Actions(driver);
                    IAction dragAndDrop = builder.ClickAndHold(Slider).MoveByOffset(0, 0).MoveByOffset(7, 100).Release().Build();
                    dragAndDrop.Perform();
                    driver.FindElement(By.XPath(".//input[@value='КУПИТЬ']")).Click();
                }
            }
            catch { }
        }

        public void CryStirring()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "PotionMakingBox")[1]) == true && Convert.ToBoolean(ReadFromFile(SettingsFile, "PotionMakingBox")[5]) == true)
            {
                if (Timer_CryStiring.CompareTo(DateTime.Now) < 0)
                {
                    driver.FindElement(By.XPath("//a/div[contains(@class,'f60')]")).Click();
                    //чистим котел
                    driver.FindElement(By.Id("form_alchemy_boiler")).FindElement(By.XPath(".//input[@value='ПОЧИСТИТЬ']")).Click();
                    string duration = Convert.ToString(ReadFromFile(SettingsFile, "PotionMakingBox")[6]);
                    if (duration.Length == 1)
                    {
                        duration = "0" + duration;
                    }
                    Timer_CryStiring = ToDateTime("00:" + duration + ":01");
                }
            }
        }

        public void LitleGuru()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[9]) == true)
            {
                while (true)
                {
                    int currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                    driver.FindElement(By.LinkText("Гавань")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(689, 899));
                    driver.FindElement(By.LinkText("Торговая площадка")).Click();
                    System.Threading.Thread.Sleep(rnd.Next(689, 899));
                    driver.FindElement(By.XPath(".//option[text()='Билет на маленькую поляну']")).Click();

                    while (currenCry > 30)
                    {
                        //двигаем бегунок
                        IWebElement Slider = driver.FindElement(By.CssSelector(".ui-slider-handle.ui-state-default.ui-corner-all"));
                        Actions builder = new Actions(driver);
                        IAction dragAndDrop = builder.ClickAndHold(Slider).MoveByOffset(0, 0).MoveByOffset(200, 150).Release().Build();
                        dragAndDrop.Perform();
                        driver.FindElement(By.XPath(".//input[@value='КУПИТЬ']")).Click();
                        System.Threading.Thread.Sleep(rnd.Next(689, 899));
                        currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                        if (Convert.ToInt32(GetResourceValue("i33")[0]) > (Convert.ToInt32(GetResourceValue("i33")[1]) - 15))
                        {
                            break;
                        }
                    }
                    SmallFields();
                }
            }
        }

        public void BigGuru()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[28]) == true)
            {
                while (true)
                {
                    try
                    {
                        int currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                        driver.FindElement(By.LinkText("Гавань")).Click();
                        Delays();
                        driver.FindElement(By.LinkText("Торговая площадка")).Click();
                        Delays();
                        driver.FindElement(By.XPath(".//option[text()='Билет на большую поляну']")).Click();

                        while (currenCry > 105)
                        {
                            //двигаем бегунок
                            IWebElement Slider = driver.FindElement(By.CssSelector(".ui-slider-handle.ui-state-default.ui-corner-all"));
                            Actions builder = new Actions(driver);
                            IAction dragAndDrop = builder.ClickAndHold(Slider).MoveByOffset(0, 0).MoveByOffset(200, 150).Release().Build();
                            dragAndDrop.Perform();
                            driver.FindElement(By.XPath(".//input[@value='КУПИТЬ']")).Click();
                            Delays();
                            currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                            if (Convert.ToInt32(GetResourceValue("i34")[0]) > (Convert.ToInt32(GetResourceValue("i34")[1]) - 15))
                            {
                                break;
                            }
                        }
                        BigFields(0);
                    }
                    catch { }
                }
            }
        }

        private void LitleGuruSmallFields()
        {
            try
            {
                FinishFieldsOpening();
                int MaxF = Convert.ToInt32(ReadFromFile(SettingsFile, "MineBox")[3]);
                Random rnd = new Random();

                //находим текущее число полянок
                try
                {
                    string[] TempString = GetResourceValue("i33");
                    //если число малых полян больше заданного или больше максимума -3
                    if (Convert.ToInt32(TempString[0]) > 0)
                    {
                        try
                        {
                            //шахта
                            driver.FindElement(By.Id("m6")).FindElement(By.XPath(".//b")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(689, 899));
                            //малая
                            WaitForElementAndClick(driver.FindElement(By.LinkText("МАЛЕНЬКАЯ")), 4000);
                            try
                            {
                                WaitForElementAndClick(driver.FindElement(By.LinkText("МАЛЕНЬКАЯ")), 4000);
                            }
                            catch { }
                            System.Threading.Thread.Sleep(rnd.Next(689, 899));
                        }
                        catch { }

                        //цикл по количесву полянок
                        for (int i = 0; i < Convert.ToInt32(TempString[0]); i++)
                        {
                            try
                            {
                                //ВСЛЕПУЮ
                                driver.FindElement(By.LinkText("ВСЛЕПУЮ")).Click();
                            }
                            catch { }
                            try
                            {
                                //ЕЩЁ
                                driver.FindElement(By.LinkText("ПОПРОБОВАТЬ ЕЩЁ")).Click();
                            }
                            catch { }
                        }
                    }
                }
                catch { }
                FinishFieldsOpening();
            }
            catch { }
        }

        public void TradeField()
        {
            string dontByEnything = ReadFromFile(SettingsFile, "AdditionalSettingsBox")[30];
            if (dontByEnything.Equals("Не скупать ресурсы") == false)
            {
                if (Timer_TFEvery.CompareTo(DateTime.Now) < 0)
                {
                    try
                    {
                        //настраиваем таймер в течении какого времени нужно скупать ресурс
                        string TFDuring = ReadFromFile(SettingsFile, "AdditionalSettingsBox")[31];
                        if (TFDuring.Length == 1)
                            TFDuring = "0" + TFDuring;
                        Timer_TFDuring = ToDateTime(string.Format("00:{0}:{1}", TFDuring, rnd.Next(10, 59)));
                        //идем в сбытку
                        driver.FindElement(By.LinkText("Гавань")).Click();
                        Delays();
                        driver.FindElement(By.LinkText("Торговая площадка")).Click();
                        Delays();
                        //генерим линку покупаемого товара
                        By selector = By.XPath(string.Format(".//option[text()='{0}']", ReadFromFile(SettingsFile, "AdditionalSettingsBox")[30]));
                        driver.FindElement(selector).Click();
                        while (Timer_TFDuring.CompareTo(DateTime.Now) > 0)
                        {
                            driver.FindElement(By.XPath(".//input[@ value='КУПИТЬ']")).Click();
                            System.Threading.Thread.Sleep(rnd.Next(200, 865));
                        }
                    }
                    catch { }
                    finally
                    {
                        string TFEvery = ReadFromFile(SettingsFile, "AdditionalSettingsBox")[32];
                        string TFEvery2 = ReadFromFile(SettingsFile, "AdditionalSettingsBox")[33];
                        int randomEvery = rnd.Next(Convert.ToInt32(TFEvery), Convert.ToInt32(TFEvery2));
                        if (TFEvery.Length == 1)
                            TFEvery = "0" + TFEvery;
                        Timer_TFEvery = ToDateTime(string.Format("00:{0}:{1}", TFEvery, randomEvery));
                    }
                }
            }
        }

        public void OpenAdvPage()
        {
            if (Timer_OpenMySite.CompareTo(DateTime.Now) < 0)
            {
                if (AdvIsOpened == false)
                {
                    try
                    {
                        /*driver.FindElement(By.TagName("body")).SendKeys(OpenQA.Selenium.Keys.Control + 't');
                        driver.Navigate().GoToUrl("http://simplebot.ru/");
                        AdvIsOpened = true;*/
                        string driverUrl = driver.Url;
                        driver.Navigate().GoToUrl("http://simplebot.ru/");
                        System.Threading.Thread.Sleep(11000);
                        AdvTimerAssigne();
                        //ClickRandomAdv();
                        if (Convert.ToBoolean(ReadFromFile(SettingsFile, "LoginBox")[5]) == false)
                        {
                            driver.Navigate().GoToUrl("http://www.botva.ru/");
                            ReHideWindow();
                        }
                        else
                        {
                            driver.Navigate().GoToUrl("http://botva.mail.ru/");
                            ReHideWindow();
                        }
                    }
                    catch { }
                }
            }
        }

        public void SwToAdv()
        {
            driver.FindElement(By.TagName("body")).SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.Tab);
            try
            {
                driver.SwitchTo().DefaultContent();
                IWebElement linkWithDlBot = driver.FindElement(By.XPath(".//a[text()='>>>Скачать бота<<<']"));
            }
            catch
            {
                OpenAdvPage();
            }
        }

        public void SwToBotvaPage()
        {
            driver.SwitchTo().DefaultContent();
            try
            {
                if (driver.Title.Contains("Ботва Онлайн") == false)
                {
                    driver.FindElement(By.TagName("body")).SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.Tab);
                    driver.SwitchTo().DefaultContent();
                }
            }
            catch { }
        }

        private void TryToClick()
        {
            if (rnd.Next(0, 5) == 1)
            {
                IList<IWebElement> advList = driver.FindElements(By.XPath(".//div[@id='pgcontainer']//a[@onfocus]"));
                int advLink = rnd.Next(0, 3);
                switch (advLink)
                {
                    case 0: advList[0].Click();
                        System.Threading.Thread.Sleep(rnd.Next(10000, 20000));
                        break;
                    case 1: advList[1].Click();
                        System.Threading.Thread.Sleep(rnd.Next(10000, 20000));
                        break;
                    case 2: advList[2].Click();
                        System.Threading.Thread.Sleep(rnd.Next(10000, 20000));
                        break;
                    default: break;
                }
            }
        }

        //-------------ADV------------- START

        private void TryToClick2underTopBar()
        {
            try
            {
                int advLink = rnd.Next(0, 5);
                switch (advLink)
                {
                    case 0: driver.FindElement(By.CssSelector("#un1 a")).Click();
                        ReHideWindow();
                        System.Threading.Thread.Sleep(rnd.Next(10000, 20000));
                        break;
                    case 1:
                    case 2: driver.FindElement(By.CssSelector("#un5 a")).Click();
                        ReHideWindow();
                        System.Threading.Thread.Sleep(rnd.Next(10000, 20000));
                        break;
                    case 3:
                    case 4: driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("#un3 iframe")));
                        driver.FindElement(By.CssSelector("a")).Click();
                        ReHideWindow();
                        System.Threading.Thread.Sleep(rnd.Next(10000, 20000));
                        driver.SwitchTo().DefaultContent();
                        break;
                    default: break;
                }
            }
            catch { }
        }

        private void TryToClick2underBelowTheHeader()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("iframe[height='90pt']")));
            IList<IWebElement> advLinks = driver.FindElements(By.CssSelector("td[valign]"));
            int advLink = rnd.Next(0, 3);
            switch (advLink)
            {
                case 0: advLinks[0].FindElement(By.TagName("a")).Click();
                    ReHideWindow();
                    System.Threading.Thread.Sleep(rnd.Next(5000, 12000));
                    break;
                case 1: advLinks[1].FindElement(By.TagName("a")).Click();
                    ReHideWindow();
                    System.Threading.Thread.Sleep(rnd.Next(5000, 12000));
                    break;
                case 2: advLinks[2].FindElement(By.TagName("a")).Click();
                    ReHideWindow();
                    System.Threading.Thread.Sleep(rnd.Next(5000, 12000));
                    break;
                default: break;
            }
            driver.SwitchTo().DefaultContent();
        }

        public void ClickRandomAdv()
        {
            if (rnd.Next(0, 4) == 1)
            {
                int adv = rnd.Next(0, 3);
                switch (adv)
                {
                    case 0:
                        TryToClick2underBelowTheHeader();
                        break;
                    case 1:
                    case 2:
                        TryToClick2underBelowTheHeader();
                        break;
                    default: break;
                }
            }
        }

        //-------------ADV------------- END

        private void AdvTimerAssigne()
        {
            //создаем таймер перехода на рекламу
            string randomMinutes = Convert.ToString(rnd.Next(25, 58));
            //if (randomMinutes.Length == 1)
            //{
            //    randomMinutes = "0" + randomMinutes;
            //}
            Timer_OpenMySite = ToDateTime("03:" + randomMinutes + ":00");
        }

        private void ClickAdv()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://vitaliidolotov.narod2.ru/");
            IWebElement t = driver.FindElement(By.TagName("a"));
            ILocatable loc = (ILocatable)t;
            IMouse mm = ((IHasInputDevices)driver).Mouse;
            mm.MouseMove(loc.Coordinates);
            mm.MouseDown(loc.Coordinates);
            mm.MouseUp(loc.Coordinates);
        }



        public void CheckForNest()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[10]) == true)
            {
                try
                {
                    if (Timer_NestReminder.CompareTo(DateTime.Now) < 0)
                    {
                        IWebElement temp = driver.FindElement(By.Id("menu_monsterpve"));
                        SoundPlayer simpleSound = new SoundPlayer("Underground_Sound.wav");
                        simpleSound.Play();
                        Timer_NestReminder = ToDateTime("00:02:00");
                    }
                }
                catch { }
            }
        }

        public void MassAbilitys()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassAbilityBox")[9]) == true)
            {
                if (Timer_MassAbil.CompareTo(DateTime.Now) < 0)
                {
                    Timer_MassAbil = ToDateTime(GetResourceValue("Время до окончания тренировки массового скилла.")[0]);
                    //переходим в БГ
                    try
                    {
                        driver.FindElement(By.CssSelector(".guilds>div>a:nth-of-type(4)")).Click();
                        Delays();
                        //Тренировочная
                        driver.FindElement(By.LinkText("Тренировочная")).Click();
                        Delays();
                        try
                        {
                            //если есть любая кнопка, кроме учить и Отменить, то кликаем ее, что начать учить заново
                            try
                            {
                                driver.FindElement(By.LinkText("ПРОДОЛЖИТЬ ИЗУЧЕНИЕ")).Click();
                            }
                            catch { }
                            try
                            {
                                driver.FindElement(By.LinkText("ПОВТОРИТЬ ИЗУЧЕНИЕ")).Click();
                            }
                            catch { }
                            IWebElement tempButton = driver.FindElement(By.XPath(".//input[@type='submit'][@value]"));
                            if (tempButton.GetAttribute("value") != "ОТМЕНИТЬ ОБУЧЕНИЕ")
                            {
                                if (tempButton.GetAttribute("value") != "УЧИТЬ")
                                {
                                    tempButton.Click();
                                }
                            }
                        }
                        catch { }
                        //выбираем скил
                        for (int i = 1; i < 9; i++)
                        {
                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassAbilityBox")[i]) == true)
                            {
                                driver.FindElement(By.CssSelector("#selector>div:nth-of-type(" + Convert.ToString(i) + ")")).Click();
                                Delays();
                                break;
                            }
                        }
                        Delays();
                        driver.FindElement(By.TagName("body")).Click();
                        driver.FindElement(By.CssSelector("input[value='УЧИТЬ']")).Click();
                        Delays();
                        Timer_MassAbil = ToDateTime(GetResourceValue("Время до окончания тренировки массового скилла.")[0]);
                    }
                    catch
                    {
                        Timer_MassAbil = ToDateTime(GetResourceValue("Время до окончания тренировки массового скилла.")[0]);
                    }
                }
            }
        }

        public void VillageManager()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[12]) == true)
            {
                if (Timer_VillageManager.CompareTo(DateTime.Now) < 0)
                {
                    try
                    {
                        //Деревеня
                        driver.FindElement(By.LinkText("Деревня")).Click();
                        Delays();
                        //Ферма
                        driver.FindElement(By.XPath(".//div[text()='Ферма']")).Click();
                        Delays();
                        //Управляющий
                        driver.FindElement(By.XPath(".//div[@class='farm_margin']//option[@value='" + Convert.ToString(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[13]) + "']")).Click();
                        Delays();
                        //Работать
                        driver.FindElement(By.CssSelector(".farm_margin>input[value='РАБОТАТЬ']")).Click();
                    }
                    catch { }
                    Timer_VillageManager = ToDateTime(GetResourceValue("Время работы заведующего на ферме")[0]);
                }
            }
        }

        public void Shop()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "ShopBox")[1]))
            {
                //если текущая работа не спуск в подземелье, то пробуем спустится
                if (CurrentWork("Спуск") == false)
                {
                    if (Timer_Shop.CompareTo(DateTime.Now) < 0)
                    {
                        //Узнаем, установлен валидное числовое значение для лимита валюты и сразу сколько заданного ресурса на руках
                        bool isCurrencyValid = false;
                        int currenCurrency = 0;
                        int currencyLimit = 0;

                        switch (ReadFromFile(SettingsFile, "ShopBox")[3])
                        {
                            case "Золото":
                                try
                                {
                                    currencyLimit = Convert.ToInt32(ReadFromFile(SettingsFile, "ShopBox")[8]);
                                    isCurrencyValid = true;
                                    currenCurrency = Convert.ToInt32(driver.FindElement(By.Id("gold")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                                }
                                catch { }
                                break;

                            case "Кристаллы":
                                try
                                {
                                    currencyLimit = Convert.ToInt32(ReadFromFile(SettingsFile, "ShopBox")[9]);
                                    isCurrencyValid = true;
                                    currenCurrency = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                                }
                                catch { }
                                break;

                            case "Зелень":
                                try
                                {
                                    currencyLimit = Convert.ToInt32(ReadFromFile(SettingsFile, "ShopBox")[10]);
                                    isCurrencyValid = true;
                                    currenCurrency = Convert.ToInt32(driver.FindElement(By.Id("green")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                                }
                                catch { }
                                break;

                            default:
                                break;
                        }

                        if (currenCurrency >= currencyLimit & isCurrencyValid)
                        {
                            driver.FindElement(By.LinkText("Деревня")).Click();
                            Delays();
                            driver.FindElement(By.XPath("//div[text()='Сбытница']")).Click();
                            Delays();
                            //Выбираем все типы товаро
                            driver.FindElement(By.XPath("//a[@title='Все']")).Click();
                            Delays();

                            ShopUncheckAllProductTypes();
                            ShopUncheckAllCurrencyTypes();

                            //Вводим название товара
                            driver.FindElement(By.CssSelector("[name='text_filter']")).Clear();
                            driver.FindElement(By.CssSelector("[name='text_filter']")).SendKeys(ReadFromFile(SettingsFile, "ShopBox")[2]);
                            Delays();
                            //Кликаем поиск
                            driver.FindElement(By.CssSelector("[title='Поиск']")).Click();
                            Delays();

                            //выставляем опции сортировки с дешевых по  ддорогие
                            try
                            {
                                IWebElement sortOption = driver.FindElement(By.XPath("//a[text()='Выкуп']/following-sibling::b[contains(@class,'desc')]"));
                            }
                            catch
                            {
                                driver.FindElement(By.XPath("//a[text()='Выкуп']")).Click();
                                Delays();
                            }

                            int lowestPrice = 0;
                            IList<IWebElement> itemsList = driver.FindElements(By.CssSelector(".row.round3"));
                            foreach (var item in itemsList)
                            {
                                IWebElement tempElement;
                                string tempString;
                                //смотрим чтоб товар был в нужной нам валюте, если нет, то выходим
                                tempString = item.FindElement(By.CssSelector(".more [title]")).GetAttribute("title");
                                if (!tempString.Equals(ReadFromFile(SettingsFile, "ShopBox")[3]))
                                    continue;

                                //Сморим чтоб цена подходила была не больше максимальной
                                tempString = item.FindElement(By.CssSelector(".more .price_num")).Text.Replace(".", string.Empty);
                                if (Convert.ToInt64(tempString) > Convert.ToInt64(ReadFromFile(SettingsFile, "ShopBox")[4]))
                                    continue;

                                //смотрим уровень вещи
                                //если установлено значение в 0, то ищем вещь без level
                                if (ReadFromFile(SettingsFile, "ShopBox")[6].Equals("0"))
                                {
                                    try
                                    {
                                        //если у вещи находим тег левел, то она нам не нужна, делаем континью
                                        tempElement = item.FindElement(By.CssSelector(".itemimg_info .level"));
                                        continue;
                                    }
                                    catch { }
                                }
                                else
                                {
                                    tempString = item.FindElement(By.CssSelector(".itemimg_info .level")).Text;
                                    if (!ReadFromFile(SettingsFile, "ShopBox")[6].Equals(tempString))
                                        continue;
                                }

                                //смотрим кол-во пп
                                tempString = item.FindElement(By.CssSelector(".itemimg_info_sells b")).GetAttribute("class").Remove(0, 6);
                                if (Convert.ToInt32(tempString) >= Convert.ToInt32(ReadFromFile(SettingsFile, "ShopBox")[5]) & Convert.ToInt32(tempString) <= Convert.ToInt32(ReadFromFile(SettingsFile, "ShopBox")[12]))
                                {
                                    try
                                    {
                                        //покупаем
                                        item.FindElement(By.CssSelector(".more a")).Click();
                                        //наасайниваем новый таймер
                                        string timer = string.Format("00:{0}:{1}", Convert.ToString(ReadFromFile(SettingsFile, "ShopBox")[7]), rnd.Next(10, 55));
                                        Timer_Shop = ToDateTime(timer);
                                        break;
                                    }
                                    catch { }
                                }
                            }

                        }
                        else
                        //если не хвататет бабла , то переасайниваем таймер
                        {
                            string timer = string.Format("00:{0}:{1}", Convert.ToString(ReadFromFile(SettingsFile, "ShopBox")[7]), rnd.Next(10, 55));
                            Timer_Shop = ToDateTime(timer);
                        }
                    }
                }
            }
        }

        private void ShopUncheckAllProductTypes()
        {
            //анчекаем все активныее ттипы продуктоов
            try
            {
                driver.FindElement(By.CssSelector(".button30.active [title='Обычные']")).Click();
                Delays();
            }
            catch { }

            try
            {
                driver.FindElement(By.CssSelector(".button30.active [title='Редкие']")).Click();
                Delays();
            }
            catch { }

            try
            {
                driver.FindElement(By.CssSelector(".button30.active [title='Реликтовые']")).Click();
                Delays();
            }
            catch { }

            //чекаем нужный ресурсаа тип
            driver.FindElement(By.CssSelector(string.Format(".button30 [title='{0}']", ReadFromFile(SettingsFile, "ShopBox")[11]))).Click();
            Delays();

        }

        private void ShopUncheckAllCurrencyTypes()
        {
            //анчекаем все активныее ттипы продуктоов
            try
            {
                driver.FindElement(By.CssSelector(".button30.active [title='Золото']")).Click();
                Delays();
            }
            catch { }

            try
            {
                driver.FindElement(By.CssSelector(".button30.active [title='Кристаллы']")).Click();
                Delays();
            }
            catch { }

            try
            {
                driver.FindElement(By.CssSelector(".button30.active [title='Зелень']")).Click();
                Delays();
            }
            catch { }

            //чекаем нужный ресурсаа тип
            driver.FindElement(By.CssSelector(string.Format(".button30 [title='{0}']", ReadFromFile(SettingsFile, "ShopBox")[3]))).Click();
            Delays();

        }

        public void DayliGifts()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[14]) == true)
            {
                if (CurrentWork("Спуск") == false)
                {
                    if (Timer_DayliGifts.CompareTo(DateTime.Now) < 0)
                    {
                        try
                        {
                            driver.FindElement(By.LinkText("Школа")).Click();
                            Delays();
                            driver.FindElement(By.XPath("//div[text()='Школьные поручения']")).Click();
                            Delays();
                            try
                            {
                                IList<IWebElement> getList = driver.FindElements(By.XPath(".//input[@value='ПОЛУЧИТЬ НАГРАДУ']"));
                                bool br = false;
                                while (!br)
                                {
                                    getList = driver.FindElements(By.XPath(".//input[@value='ПОЛУЧИТЬ НАГРАДУ']"));
                                    br = true;
                                    foreach (IWebElement get in getList)
                                    {
                                        if (get.Enabled)
                                        {
                                            get.Click();
                                            br = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                        catch { }
                        string minutes = Convert.ToString(rnd.Next(24, 36));
                        Timer_DayliGifts = ToDateTime("00:" + minutes + ":06");
                    }
                }
            }
        }

        public void DrinkOborotka()
        {
            string PageContent = driver.PageSource;

        }

        public void Sawmill()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[17]) == true || Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[18]) == true)
            {
                if (Timer_ForestFarmer.CompareTo(DateTime.Now) < 0)
                {
                    if (CharacterIsFree() == true)
                    {
                        try
                        {
                            driver.FindElement(By.LinkText("Клан")).Click();
                            Delays();
                            driver.FindElement(By.CssSelector(".clan_main_sawmill")).Click();
                            Delays();
                            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[17]) == true)
                            {
                                driver.FindElement(By.CssSelector("#forest_work input[value='ПОЙТИ']")).Click();
                                Delays();
                            }
                            else
                            {
                                driver.FindElement(By.CssSelector("#cutter_work input[value='ПОЙТИ']")).Click();
                                Delays();
                            }
                        }
                        catch { }
                    }
                    Timer_ForestFarmer = ToDateTime("00:21:00");
                }
            }
        }

        public void BuyGifts()
        {
            //if (!Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[23]))
            //{
            //    try
            //    {
            //        int currenCry = Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
            //        if (currenCry > Convert.ToInt32(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[20]))
            //        {
            //            driver.FindElement(By.LinkText("Деревня")).Click();
            //            Delays();
            //            driver.FindElement(By.XPath(".//div[text()='Лавка']")).Click();
            //            Delays();
            //            driver.FindElement(By.LinkText("ПОДРОБНЕЕ")).Click();
            //            Delays();
            //            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[21]))
            //            {
            //                driver.FindElement(By.XPath("//div[contains(@title,'Детская')]/../input")).Click();
            //                Delays();
            //            }
            //            else
            //            {
            //                driver.FindElement(By.XPath("//div[contains(@title,'Просто')]/../input")).Click();
            //                Delays();
            //            }
            //        }
            //    }
            //    catch { }
            //}
        }

        public void GoToOldoMsters()
        {
            try
            {
                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[11]))
                {
                    IWebElement fastMonsterIco = driver.FindElement(By.Id("menu_monsterpve"));
                    if (driver.FindElement(By.CssSelector(".char_stat.char_stat_with_pets u")).Text.Equals("Aksis") || driver.FindElement(By.CssSelector(".char_stat.char_stat_with_pets u")).Text.Equals("StrongPig"))
                    {
                        try
                        {
                            IWebElement temp = driver.FindElement(By.XPath(".//a[@title='Посадить в клетку']"));
                        }
                        catch
                        {
                            try
                            {
                                GoByWorm();
                                IWebElement temp = driver.FindElement(By.XPath(".//div[contains(text(),'У тебя уже есть такая зверушка, со')]"));
                                GetPet(PetType.worm);
                            }
                            catch { }
                        }
                    }

                    if (driver.FindElement(By.CssSelector(".char_stat.char_stat_with_pets u")).Text.Equals("Aksis"))
                    {
                        driver.FindElement(By.XPath(".//a[contains(text(),'статусами')]")).Click();
                        Delays();
                        driver.FindElement(By.XPath(".//option[text()='Питомец']")).Click();
                        Delays();
                        driver.FindElement(By.XPath(".//input[@value='СМЕНИТЬ']")).Click();
                        Delays();
                        //закрыть попап
                        driver.FindElement(By.CssSelector(".popup .close")).Click();
                        Delays();
                    }

                    driver.FindElement(By.Id("menu_monsterpve")).Click();
                    Delays();
                    driver.FindElement(By.XPath(".//input[contains(@value,'Перейти к месту')]")).Click();
                    Delays();
                }
            }
            catch { }
        }

        private void GoByWorm()
        {
            driver.FindElement(By.LinkText("Деревня")).Click();
            Delays();
            driver.FindElement(By.XPath(".//div[text()='Лавка']")).Click();
            Delays();
            driver.FindElement(By.LinkText("Звери")).Click();
            Delays();
            Delays();
            driver.FindElement(By.CssSelector("#shop_cmd_200")).Click();
        }

        public void AlertFight()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "AdditionalSettingsBox")[27]))
            {
                try
                {
                    driver.FindElement(By.CssSelector("#m2.active")).Click();
                    if (IsAlertFightPresent("Тигранище")) SetPet();
                }
                catch { }
            }
        }

        private bool IsAlertFightPresent(string alertEnemy)
        {
            IList<IWebElement> fightList = driver.FindElements(By.CssSelector(".font_normal"));
            return fightList.FirstOrDefault(enemy => enemy.Text.Contains(alertEnemy + " атаковал вас")).Displayed;
        }

        public void MassFight()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassFBox")[1]))
            {
                Timer_MassFight = ToDateTime("00:35:00");
                GoToMassFight();
                while (true)
                {
                    try
                    {
                        //заканчиваем войну если есть спец элемнет
                        try
                        {
                            IWebElement endFightElement = driver.FindElement(By.CssSelector(".bonus .title"));
                            if (endFightElement.Displayed)
                            {
                                driver.FindElement(By.CssSelector(".battleground_exit")).Click();
                                break;
                            }
                        }
                        catch { }

                        MGoToLocation();
                        MHealing();
                        MGoTakeFood();
                        MAskForFood();
                        MAUseShild();
                        MAUseGodDef();
                        MAUseSacrifice();
                        MBeat();
                        MFood();
                        MAGetSomeFood();
                        WaitForNewRaund();

                        //ливаем по времени

                        //ливаем если убили
                        try
                        {
                            IWebElement deadPerson = driver.FindElement(By.CssSelector(".bg_user_panel.dead[style*='background-color']"));
                            if (deadPerson.Displayed)
                            {
                                driver.FindElement(By.CssSelector(".battleground_exit")).Click();
                                break;
                            }
                        }
                        catch { }

                        if (Timer_MassFight.CompareTo(DateTime.Now) < 0)
                        {
                            try
                            {
                                driver.FindElement(By.CssSelector(".battleground_exit")).Click();
                                break;
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.CssSelector(".battleground_exit")).Click();
                                break;
                            }
                            catch { }
                        }

                    }
                    catch { }
                }
            }
        }

        private void MGoToLocation()
        {
            try
            {
                IWebElement currentLocation = driver.FindElement(By.CssSelector("#places_ .active"));
                //если лока не соотв заданной
                if (!currentLocation.GetAttribute("title").Equals(ReadFromFile(SettingsFile, "MassFBox")[7]))
                {
                    //если нужно в лес
                    if (ReadFromFile(SettingsFile, "MassFBox")[7].Equals("Лес"))
                    {
                        if (currentLocation.GetAttribute("title").Equals("Горы"))
                        {
                            Actions action = new Actions(driver);
                            action.MoveToElement(driver.FindElement(By.CssSelector("#plains"))).Build().Perform();
                            driver.FindElement(By.CssSelector("#plains b")).Click();
                            SmallDelays();
                            return;
                        }

                        if (currentLocation.GetAttribute("title").Equals("Равнина"))
                        {
                            Actions action = new Actions(driver);
                            action.MoveToElement(driver.FindElement(By.CssSelector("#forest"))).Build().Perform();
                            driver.FindElement(By.CssSelector("#forest b")).Click();
                            SmallDelays();
                            return;
                        }
                    }

                    //если нужно в горы
                    if (ReadFromFile(SettingsFile, "MassFBox")[7].Equals("Горы"))
                    {
                        if (currentLocation.GetAttribute("title").Equals("Лес"))
                        {
                            Actions action = new Actions(driver);
                            action.MoveToElement(driver.FindElement(By.CssSelector("#plains"))).Build().Perform();
                            driver.FindElement(By.CssSelector("#plains b")).Click();
                            SmallDelays();
                            return;
                        }

                        if (currentLocation.GetAttribute("title").Equals("Равнина"))
                        {
                            Actions action = new Actions(driver);
                            action.MoveToElement(driver.FindElement(By.CssSelector("#mountains"))).Build().Perform();
                            driver.FindElement(By.CssSelector("#mountains b")).Click();
                            SmallDelays();
                            return;
                        }
                    }

                    //если нужно на равнину
                    if (ReadFromFile(SettingsFile, "MassFBox")[7].Equals("Равнина"))
                    {
                        Actions action = new Actions(driver);
                        action.MoveToElement(driver.FindElement(By.CssSelector("#plains"))).Build().Perform();
                        driver.FindElement(By.CssSelector("#plains b")).Click();
                        SmallDelays();
                        return;
                    }
                }
            }
            catch { }
        }

        private void GoToMassFight()
        {
            try
            {
                string selector = string.Format("//div[@title='{0}']", ReadFromFile(SettingsFile, "MassFBox")[2]);
                driver.FindElement(By.XPath(selector)).Click();
                Delays();
                //if (driver.FindElement(By.CssSelector(selector)).Displayed)
                //{
                string mineSelector = string.Format("{0} .status span:nth-of-type(1) a", MMainSelectorProvider(ReadFromFile(SettingsFile, "MassFBox")[2]));
                driver.FindElement(By.CssSelector(mineSelector))/*.FindElement(By.LinkText("ВСТУПИТЬ"))*/.Click();
                SmallDelays();
                //}
            }
            catch { }
        }

        private string MMainSelectorProvider(string mineName)
        {
            switch (mineName)
            {
                case "Соль земли":
                    return ".single.round3[data-id='1']";

                case "Кофейный лист":
                    return ".single.round3[data-id='2']";

                case "Рыжий сланец":
                    return ".single.round3[data-id='3']";

                case "Горная слеза":
                    return ".single.round3[data-id='4']";

                case "Сверкающий адамант":
                    return ".single.round3[data-id='5']";

                case "Тяжелый мифрил":
                    return ".single.round3[data-id='6']";

                default: return string.Empty;
            }
        }

        private Int64 MGetCurrentHeal()
        {
            string currentHeal = driver.FindElement(By.CssSelector(".bg_user_panel[style*='background-color'] .bg_health_scale_left")).GetAttribute("title").Replace(" ", string.Empty).Split('/').FirstOrDefault();
            return Convert.ToInt64(currentHeal);
        }

        private Int64 MGetMaxHeal()
        {
            string maxHeal = driver.FindElement(By.CssSelector(".bg_user_panel[style*='background-color'] .bg_health_scale_left")).GetAttribute("title").Replace(" ", string.Empty).Split('/').LastOrDefault();
            return Convert.ToInt64(maxHeal);
        }

        private int MGetCurrentFood()
        {
            return Convert.ToInt32(driver.FindElement(By.CssSelector("#ap_available")).Text);
        }

        private void MGoTakeFood()
        {
            if (MGetCurrentFood() < 25)
            {
                driver.FindElement(By.XPath("//a[text()='Уйти за едой']")).Click();
                //убираем фокус
                Actions action = new Actions(driver);
                action.MoveToElement(driver.FindElement(By.CssSelector("#close_battle"))).Build().Perform();
                SmallDelays();
            }
        }

        private Int64 MMinHeal()
        {
            return 31 * MGetMaxHeal() / 100;
        }

        private void MHealing()
        {
            try
            {
                Int64 minH = MMinHeal();
                Int64 currentH = MGetCurrentHeal();
                if (currentH <= minH)
                {
                    driver.FindElement(By.CssSelector(".talant_ico_524")).Click();
                    //убираем фокус
                    Actions action = new Actions(driver);
                    action.MoveToElement(driver.FindElement(By.CssSelector("#close_battle"))).Build().Perform();
                    SmallDelays();
                }
            }
            catch { }
        }

        private void MFood()
        {
            try
            {
                int foodCount = MGetCurrentFood();
                IList<IWebElement> foodIcons = driver.FindElements(By.XPath("//div[contains(@id,'user_slot')]//span[contains(@class,'ico_bg_food_request')]"));
                foreach (var foodico in foodIcons)
                {
                    try
                    {
                        if (foodico.Displayed & foodico.Enabled)
                        {
                            if (foodCount > 20)
                            {
                                foodico.Click();
                                SmallDelays();
                                foodCount = MGetCurrentFood();
                            }
                            else
                                break;
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void MBeat()
        {
            try
            {
                int foodCount = MGetCurrentFood();
                IList<IWebElement> swordIcons = driver.FindElements(By.CssSelector("span[type='ico_attack']"));
                foreach (var swordIcon in swordIcons)
                {
                    if (foodCount > 20)
                    {
                        swordIcon.Click();
                        SmallDelays();
                        foodCount = MGetCurrentFood();
                    }
                    else
                        break;
                }
            }
            catch { }
        }

        private void MAGetSomeFood()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassFBox")[6]))
            {
                try
                {
                    driver.FindElement(By.CssSelector(".talant_ico_800")).Click();
                    SmallDelays();
                    //убираем фокус
                    Actions action = new Actions(driver);
                    action.MoveToElement(driver.FindElement(By.CssSelector("#close_battle"))).Build().Perform();
                    SmallDelays();
                }
                catch { }
            }
        }

        private void MAUseShild()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassFBox")[3]))
            {
                try
                {
                    driver.FindElement(By.CssSelector(".talant_ico_519")).Click();
                    SmallDelays();
                    //Клацнуть на себя
                    Actions action = new Actions(driver);
                    action.MoveToElement(driver.FindElement(By.CssSelector("#close_battle"))).Build().Perform();
                    SmallDelays();
                    driver.FindElement(By.CssSelector(".bg_user_panel[style*='background-color'] .bg_name")).Click();
                    SmallDelays();
                }
                catch { }
            }
        }

        private void MAUseGodDef()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassFBox")[4]))
            {
                try
                {
                    driver.FindElement(By.CssSelector(".talant_ico_518")).Click();
                    SmallDelays();
                    //Клацнуть на себя
                    Actions action = new Actions(driver);
                    action.MoveToElement(driver.FindElement(By.CssSelector("#close_battle"))).Build().Perform();
                    SmallDelays();
                    driver.FindElement(By.CssSelector(".bg_user_panel[style*='background-color'] .bg_name")).Click();
                    SmallDelays();
                }
                catch { }
            }
        }

        private void MAUseSacrifice()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "MassFBox")[5]))
            {
                try
                {
                    driver.FindElement(By.CssSelector(".talant_ico_520")).Click();
                    SmallDelays();
                    //Клацнуть на себя
                    Actions action = new Actions(driver);
                    action.MoveToElement(driver.FindElement(By.CssSelector("#close_battle"))).Build().Perform();
                    SmallDelays();
                    driver.FindElement(By.CssSelector(".bg_user_panel[style*='background-color'] .bg_name")).Click();
                    SmallDelays();
                }
                catch { }
            }
        }

        private void MAskForFood()
        {
            try
            {
                driver.FindElement(By.CssSelector("#ap_request")).Click();
                SmallDelays();
                //убираем фокус
                Actions action = new Actions(driver);
                action.MoveToElement(driver.FindElement(By.CssSelector("#close_battle"))).Build().Perform();
                SmallDelays();
            }
            catch { }
        }

        private void WaitForNewRaund()
        {
            try
            {
                IWebElement timer = driver.FindElement(By.CssSelector("#main_timer i"));
                while (timer.Displayed)
                {
                    SmallDelays();
                    timer = driver.FindElement(By.CssSelector("#main_timer i"));
                }
            }
            catch
            {
                System.Threading.Thread.Sleep(7600);
            }
        }


        private void Sort(string[] enemysBm, string[] enemyName)
        {
            for (int i = 0; i < enemysBm.Length - 1; i++)
            {
                for (int y = 0; y < enemysBm.Length - 1; y++)
                {
                    if (Convert.ToInt32(enemysBm[y]) > Convert.ToInt32(enemysBm[y + 1]) & y + 1 != enemysBm.Length)
                    {
                        //Перемещаем бм
                        string tempBm = enemysBm[y + 1];
                        enemysBm[y + 1] = enemysBm[y];
                        enemysBm[y] = tempBm;

                        //Перемещаем имена
                        string tempName = enemyName[y + 1];
                        enemyName[y + 1] = enemyName[y];
                        enemyName[y] = tempName;
                    }
                }
            }
        }

        public void ArenaFight()
        {
            if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[26]))
            {
                if (Timer_Arena.CompareTo(DateTime.Now) < 0)
                {
                    //если текущая работа не спуск в подземелье
                    if (CurrentWork("Спуск") == false)
                    {
                        try
                        {
                            //Определяем страницу, если не на бодалке, то переходим в нее
                            try
                            {
                                if (driver.Url.Contains("dozor") == false || driver.Url.Contains("arena"))
                                {
                                    driver.FindElement(By.LinkText("Бодалка")).Click();
                                    Delays();
                                }
                            }
                            catch { }

                            //число оставшихся кри на считу
                            int currenCry =
                                Convert.ToInt32(driver.FindElement(By.Id("crystal")).FindElement(By.TagName("b")).Text.Replace(".", ""));
                            if (currenCry > 5)
                            {
                                //driver.FindElement(By.XPath(".//a[contains(text(),'ПОИСК:')]")).Click();
                                try
                                {
                                    driver.FindElement(By.XPath(".//a[text()='ПОИСК: ']")).Click();
                                }
                                catch { }
                                Delays();
                                string[] enemysBm = new string[4];
                                string[] enemysName = new string[4];
                                IList<IWebElement> enemys = driver.FindElements(By.CssSelector(".arena_enemy"));
                                int iterator = 0;
                                foreach (var enemy in enemys)
                                {
                                    enemysName[iterator] = enemy.FindElement(By.CssSelector(".arena_enemy_name")).Text;
                                    //если ориентируемся по БМ, тогда один силектор, если нет, то другой
                                    if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[32]))
                                    {
                                        enemysBm[iterator] = enemy.FindElement(By.CssSelector(".arena_enemy_stat div:nth-of-type(2)"))
                                                                  .Text.Replace(".", "");
                                    }
                                    else
                                    {
                                        enemysBm[iterator] = enemy.FindElement(By.CssSelector(".arena_enemy_stat div:nth-of-type(1)"))
                                                                  .Text.Replace(".", "");
                                    }
                                    iterator++;
                                }
                                Sort(enemysBm, enemysName);

                                //бъем по БМ или славе
                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[32]))
                                {
                                    driver.FindElement(
                                        By.XPath(string.Format(".//div[text()='{0}']/..",
                                                               enemysName[Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[27])]))).Click();
                                }
                                else
                                {
                                    driver.FindElement(By.XPath(string.Format(".//div[text()='{0}']/..", enemysName[Convert.ToInt32(ReadFromFile(SettingsFile, "FightBox")[34])]))).Click();
                                }

                                //Если каждые 5 минут, то ассайни соотв таймер
                                if (Convert.ToBoolean(ReadFromFile(SettingsFile, "FightBox")[28]))
                                    Timer_Arena = ToDateTime(string.Format("00:05:{0}", rnd.Next(15, 58)));
                                else
                                    Timer_Arena = ToDateTime(string.Format("00:15:{0}", rnd.Next(15, 58)));
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }
    }
}
