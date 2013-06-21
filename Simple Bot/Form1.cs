using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Firefox;
using System.Threading;
using Simple_Bot.ocr;
using System.Runtime.InteropServices;
using System.Media;


namespace Simple_Bot
{
    public partial class Form1 : Form
    {
        bool isDonatePlayer = false;
        int BotVersion = 2527;

        Random rnd = new Random();

        MD5 md5Hash = MD5.Create();

        string lable29Text;

        int ClickCount = 0;
        static DateTime Timer_OpenSite = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_OpenWindow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_GoBack = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_Reload = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_CloseBot = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);
        static DateTime Timer_ChromeDriverKiller = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);

        bool chromeDriverCiller = false;
        bool botStatus;

        string SettingsFile = "settings";
        string SettingsFileExtantion = ".bin";

        public Form1()
        {
            InitializeComponent();

            File.Delete("chromedriver.log");

            this.Size = new System.Drawing.Size(217, 268);

            Timer_CloseBot = ToDateTime(string.Format("05:{0}:00", (rnd.Next(11, 19))));
            Timer_ChromeDriverKiller = ToDateTime("00:01:15");

            Timer_OpenSite = ToDateTime("00:" + Convert.ToString(rnd.Next(25, 29)) + ":00");
            Timer_OpenWindow = ToDateTime("00:" + Convert.ToString(rnd.Next(30, 34)) + ":00");
            Timer_Reload = ToDateTime("01:" + Convert.ToString(rnd.Next(10, 57)) + ":00");
            //Timer_OpenSite = ToDateTime("00:00:02");
            //Timer_OpenWindow = ToDateTime("00:00:04");
            //Timer_GoBack = ToDateTime("00:00:16");

            timer1.Start();
            backgroundWorker1.RunWorkerAsync();

            // Add a link to the LinkLabel.
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://simplebot.ru";
            linkLabel2.Links.Add(link);


            LinkLabel.Link link2 = new LinkLabel.Link();
            link2.LinkData = "https://docs.google.com/forms/d/1ACUpGzPo7TVtaDWq6ZOH3uQNEJwq3S8_9Umr7yvyWl0/viewform";
            linkLabel1.Links.Add(link2);

            //Login
            try
            {
                comboBox1.Text = ReadFromFile(SettingsFile, LoginBox.Name)[1];
                textBox1.Text = ReadFromFile(SettingsFile, LoginBox.Name)[2];
                textBox2.Text = ReadFromFile(SettingsFile, LoginBox.Name)[3];
                comboBox2.Text = ReadFromFile(SettingsFile, LoginBox.Name)[4];
                checkBox1.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, LoginBox.Name)[5]);
            }
            catch { }

            //Mine Settings
            try
            {
                checkBoxWorkInMine.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[1]);
                numericUpDownMineImmun.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, MineBox.Name)[2]);
                numericUpDownMaxSmallFields.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, MineBox.Name)[3]);
                numericUpDownMaxBigFields.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, MineBox.Name)[4]);
                checkBoxPickBy.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[5]);
                checkBoxGlassesBy.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[6]);
                checkBoxHelmetBy.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[7]);
                radioButtonMineInventoryByCommon.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[8]);
                radioButtonMineInventorySlogger.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[9]);
                checkBoxMineInventory.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[10]);
            }
            catch { }

            //Potionsetting Box
            try
            {
                checkBoxPotionMaking.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[1]);
                checkBoxTankMaking.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[2]);
                checkBoxBoilRent.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[3]);
                radioButtonUseClearPotion.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[4]);
                radioButtonStiringByCry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[5]);
                numericUpDownStiringByCryMin.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, PotionMakingBox.Name)[6]);
            }
            catch { }

            //StutsUp Settings
            try
            {
                checkBoxPower.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[1]);
                checkBoxBlock.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[2]);
                checkBoxDexterity.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[3]);
                checkBoxEndurance.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[4]);
                checkBoxCharisma.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[5]);
            }
            catch { }

            //Additional Settings
            try
            {
                checkBoxCryDust.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[1]);
                checkBoxFish.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[2]);
                checkBoxFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[3]);
                checkBoxSoapMaking.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[4]);
                textBoxGold.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[5];
                textBoxGoldForMe.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[6];
                textBoxSoapToTP.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[7];
                textBoxBySlaves.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[8];
                checkBoxLitleGuru.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[9]);
                checkBoxReminder.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[10]);
                checkBoxTray.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[11]);
                checkBoxVillageManager.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[12]);
                numericUpDownVillageManagerTime.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[13]);
                checkBoxDayliGifts.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[14]);
                checkBoxHideBrowser.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[15]);
                textBoxAdv.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[16];
                radioButtonGoToForest.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[17]);
                radioButtonMakeTree.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[18]);
                radioButtonDontWork.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[19]);
                numericUpDownGiftsCryNumber.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[20]);
                radioButtonSmallGift.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[21]);
                radioButtonMiddleGift.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[22]);
                radioButtonDonBuyGifts.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[23]);
                radioButtonWhale.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[24]);
                radioButtonParot.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[25]);
                radioButtonCurrentPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[26]);
                checkBoxAlarmBox.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[27]);
                checkBoxBigGguru.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[28]);
                checkBoxBiggestPotion.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[29]);
                comboBoxTFResource.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[30];
                numericUpDownTFDuringTime.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[31]);
                numericUpDownTFEveryTime.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[32]);
                numericUpDownTFEveryTime2.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[33]);
            }
            catch { }

            //Underground Settings
            try
            {
                checkBoxUnderground.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[1]);
                radioButtonUnderground.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[2]);
                radioButtonFastUnderground.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[3]);
                radioButtonWinch.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[4]);
                radioButtonCord.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[5]);
                checkBoxByKeys.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[6]);
                numericUpDownUndergroundImm.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, UndergroundBox.Name)[7]);
                checkBoxOpenPanda.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[8]);
                checkBoxSalePanda.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[9]);
                numericUpDownPandaLvl.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, UndergroundBox.Name)[10]);
                checkBoxUndergroundSetPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[11]);
                checkBoxUndGetPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[12]);
                checkBoxPandaOpenCry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[13]);
                numericUpDownPandaLvlForSale.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, UndergroundBox.Name)[14]);
                radioButtonWorbBlueSoul.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[15]);
                radioButtonWormPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[16]);
                radioButtonDefaultUndrPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[17]);
            }
            catch { }

            //Fight Settings
            try
            {
                checkBoxFightMonsters.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[1]);
                checkBoxFightZorro.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[2]);
                radioButtonZorroLvl.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[3]);
                radioButtonZorroList.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[4]);
                checkBoxFight.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[5]);
                radioButtonFightLvl.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[6]);
                radioButtonFightList.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[7]);
                checkBoxOborotka.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[8]);
                checkBoxGetPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[9]);
                checkBoxImmunOgl.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[10]);
                checkBoxImmunAnti.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[11]);
                radioButtonImmunPir.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[12]);
                radioButtonImmunCry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[13]);
                checkBoxPetImmun.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[14]);
                numericUpDownPetImmun.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[15]);
                checkBoxEnemyPower.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[16]);
                numericUpDownEnemyPower.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[17]);
                numericUpDownEnemyBlock.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[18]);
                numericUpDownEnemyDex.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[19]);
                numericUpDownEnemyEd.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[20]);
                numericUpDownEnemyChar.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[21]);
                checkBoxMoralityMinus.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[22]);
                checkBoxMoralityPlus.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[23]);
                checkBoxMoralityZero.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[24]);
                checkBoxDrinkOborotka.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[25]);
				checkBoxArenaFight.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[26]);
				numericUpDownArenaEnemy.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[27]);
                checkBoxArenaEvery5min.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[28]);
            }
            catch { }

            //Heal settings
            try
            {
                checkBoxHeal.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, HealBox.Name)[1]);
                numericUpDownHeal.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, HealBox.Name)[2]);
                checkBoxPetHeal.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, HealBox.Name)[3]);
                numericUpDownPetHeal.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, HealBox.Name)[4]);
            }
            catch { }

            //Effects Settings
            try
            {
                checkBoxEffPoison.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, EffectsBox.Name)[1]);
                checkBoxEffGold.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, EffectsBox.Name)[2]);
                checkBoxEffAnti.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, EffectsBox.Name)[3]);
            }
            catch { }

            //Panda Effects box
            try
            {
                checkBoxPandaEffect1.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[1]);
                checkBoxPandaEffect2.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[2]);
                checkBoxPandaEffect3.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[3]);
                checkBoxPandaEffect4.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[4]);
                comboBoxPandaEffects.Text = ReadFromFile(SettingsFile, PandaEffectsBox.Name)[5];
                radioButtonPEpir.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[6]);
                radioButtonPEcry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[7]);
            }
            catch { }

            //Far Countrys
            try
            {
                checkBoxGetRP.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FarCountrBox.Name)[1]);
                radioButtonFirstBoat.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FarCountrBox.Name)[2]);
                radioButtonSecondBoat.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FarCountrBox.Name)[3]);
            }
            catch { }

            //Fly Settings
            try
            {
                radioButtonBTFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[1]);
                radioButtonKarFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[2]);
                numericUpDownTrackFFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[3]);
                numericUpDownHrsFFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[4]);

                radioButtonBTSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[5]);
                radioButtonKarSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[6]);
                numericUpDownTrackSFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[7]);
                numericUpDownHrsSFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[8]);

                radioButtonBTTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[9]);
                radioButtonKarTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[10]);
                numericUpDownTrackTFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[11]);
                numericUpDownHrsTFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[12]);

                radioButtonBTFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[13]);
                radioButtonKarFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[14]);
                numericUpDownTrackFoFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[15]);
                numericUpDownHrsFoFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[16]);

                radioButtonSTFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[17]);
                radioButtonSTSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[18]);
                radioButtonSTTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[19]);
                radioButtonSTFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[20]);

                checkBoxDontUseFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[21]);
                checkBoxDontUseSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[22]);
                checkBoxDontUseTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[23]);
                checkBoxDontUseFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[24]);
            }
            catch { }

            //Ability settings
            try
            {
                radioButtonSK1.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[1]);
                radioButtonSK2.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[2]);
                radioButtonSK3.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[3]);
                radioButtonSK4.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[4]);
                radioButtonSK5.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[5]);
                radioButtonSK6.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[6]);
                radioButtonSK7.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[7]);
                radioButtonSK8.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[8]);
                checkBoxMassAbilitys.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[9]);
            }
            catch { }

            //Shop Box settings
            try
            {
                checkBoxShop.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, ShopBox.Name)[1]);
                textBoxProdutName.Text = ReadFromFile(SettingsFile, ShopBox.Name)[2];
                comboBoxCurrencyType.Text = ReadFromFile(SettingsFile, ShopBox.Name)[3];
                textBoxMaxValue.Text = ReadFromFile(SettingsFile, ShopBox.Name)[4];
                numericUpDownPPvalue.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, ShopBox.Name)[5]);
                numericUpDownItemLevel.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, ShopBox.Name)[6]);
                numericUpDownTryByMin.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, ShopBox.Name)[7]);
                textBoxCurrentGold.Text = ReadFromFile(SettingsFile, ShopBox.Name)[8];
                textBoxCurrenCry.Text = ReadFromFile(SettingsFile, ShopBox.Name)[9];
                textBoxCurrentGren.Text = ReadFromFile(SettingsFile, ShopBox.Name)[10];
            }
            catch { }

            CBWorkInMine();
            CBUnderground();
            CBPotionMaking();
            RBFastUnderground();
            CBSalePanda();
            CBFight();
            CBFightZorro();
            CBHeal();
            CBPetHeal();
            CBUndergroundSetPet();
            CBUndGetPet();
            CBPetImmun();
            CBGetRP();
            CBEnemyPower();
            CBSoapMaking();
            CBOpenPanda();
            CBVillageManager();
        }

        void CheckBotStatus()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://dl.dropbox.com/s/gixvi3853twwks3/UpdateFile.csv?dl=1");
                StreamReader reader = new StreamReader(stream);
                string FileContent = reader.ReadToEnd();
                if (FileContent.Contains("SimpleBotLibrary"))
                {
                    botStatus = true;
                }
                else botStatus = false;
            }
            catch
            {
                botStatus = false;
            }
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString().Remove(21);
        }

        private void RemovingOldUpdater()
        {
            if (File.Exists("Updater_Temp.exe") == true)
            {
                try
                {
                    File.Delete("Updater.exe");
                    System.IO.File.Move("Updater_Temp.exe", "Updater.exe");
                }
                catch { }
            }
        }

        private void CheckForUpdates()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://dl.dropbox.com/s/gixvi3853twwks3/UpdateFile.csv?dl=1");
                StreamReader reader = new StreamReader(stream);
                string[] FileContent = reader.ReadToEnd().Split(';');
                int NewBotVersion = Convert.ToInt32(FileContent[0]);
                if (NewBotVersion > BotVersion)
                {
                    Process UpdateProcess = new Process();
                    UpdateProcess.StartInfo.FileName = "Updater.exe";
                    UpdateProcess.Start();
                    Environment.Exit(0);
                }
            }
            catch
            {
                MessageBox.Show("Simple Bot can't check latest version", "Error during updates checking ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void ChromeDriverKillerProcess()
        {
            Process cdkProc = new Process();
            cdkProc.StartInfo.FileName = "chromedriverKiller.exe";
            cdkProc.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button4.Text = "Back";
            RemovingOldUpdater();
            CheckForUpdates();

            //StutsUp Setting
            string[] StutsUpSettings = { Convert.ToString(checkBoxPower.Checked), Convert.ToString(checkBoxBlock.Checked), Convert.ToString(checkBoxDexterity.Checked), Convert.ToString(checkBoxEndurance.Checked), Convert.ToString(checkBoxCharisma.Checked) };
            CompareValuesInFile(StutsUpBox.Name, StutsUpSettings);
            checkBoxPower.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[1]);
            checkBoxBlock.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[2]);
            checkBoxDexterity.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[3]);
            checkBoxEndurance.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[4]);
            checkBoxCharisma.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, StutsUpBox.Name)[5]);

            //Login
            string[] LoginBoxValues = { comboBox1.Text, textBox1.Text, textBox2.Text, comboBox2.Text, Convert.ToString(checkBox1.Checked) };
            CompareValuesInFile(LoginBox.Name, LoginBoxValues);
            comboBox1.Text = ReadFromFile(SettingsFile, LoginBox.Name)[1];
            textBox1.Text = ReadFromFile(SettingsFile, LoginBox.Name)[2];
            textBox2.Text = ReadFromFile(SettingsFile, LoginBox.Name)[3];
            comboBox2.Text = ReadFromFile(SettingsFile, LoginBox.Name)[4];
            checkBox1.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, LoginBox.Name)[5]);

            //Mine Settings
            string[] MineSettings = { Convert.ToString(checkBoxWorkInMine.Checked), Convert.ToString(numericUpDownMineImmun.Value), Convert.ToString(numericUpDownMaxSmallFields.Value), Convert.ToString(numericUpDownMaxBigFields.Value),
                                    Convert.ToString(checkBoxPickBy.Checked),Convert.ToString(checkBoxGlassesBy.Checked),Convert.ToString(checkBoxHelmetBy.Checked),
                                    Convert.ToString(radioButtonMineInventoryByCommon.Checked),
                                    Convert.ToString(radioButtonMineInventorySlogger.Checked),Convert.ToString(checkBoxMineInventory.Checked)};
            CompareValuesInFile(MineBox.Name, MineSettings);
            checkBoxWorkInMine.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[1]);
            numericUpDownMineImmun.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, MineBox.Name)[2]);
            numericUpDownMaxSmallFields.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, MineBox.Name)[3]);
            numericUpDownMaxBigFields.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, MineBox.Name)[4]);
            checkBoxPickBy.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[5]);
            checkBoxGlassesBy.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[6]);
            checkBoxHelmetBy.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[7]);
            radioButtonMineInventoryByCommon.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[8]);
            radioButtonMineInventorySlogger.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[9]);
            checkBoxMineInventory.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MineBox.Name)[10]);

            //Potion Making Settings
            string[] PotionMakingSettings = { Convert.ToString(checkBoxPotionMaking.Checked), Convert.ToString(checkBoxTankMaking.Checked), Convert.ToString(checkBoxBoilRent.Checked),
                                            Convert.ToString(radioButtonUseClearPotion.Checked),Convert.ToString(radioButtonStiringByCry.Checked),Convert.ToString(numericUpDownStiringByCryMin.Value)};
            CompareValuesInFile(PotionMakingBox.Name, PotionMakingSettings);
            checkBoxPotionMaking.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[1]);
            checkBoxTankMaking.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[2]);
            checkBoxBoilRent.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[3]);
            radioButtonUseClearPotion.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[4]);
            radioButtonStiringByCry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PotionMakingBox.Name)[5]);
            numericUpDownStiringByCryMin.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, PotionMakingBox.Name)[6]);

            //Additional Settings
            string[] AdditionalSettings = { Convert.ToString(checkBoxCryDust.Checked), Convert.ToString(checkBoxFish.Checked), Convert.ToString(checkBoxFly.Checked),
                                              Convert.ToString(checkBoxSoapMaking.Checked), textBoxGold.Text, textBoxGoldForMe.Text, textBoxSoapToTP.Text, textBoxBySlaves.Text,
                                              Convert.ToString(checkBoxLitleGuru.Checked), Convert.ToString(checkBoxReminder.Checked), Convert.ToString(checkBoxTray.Checked),
                                              Convert.ToString(checkBoxVillageManager.Checked), Convert.ToString(numericUpDownVillageManagerTime.Value), Convert.ToString(checkBoxDayliGifts.Checked),
                                              Convert.ToString(checkBoxHideBrowser.Checked), textBoxAdv.Text, Convert.ToString(radioButtonGoToForest.Checked), Convert.ToString(radioButtonMakeTree.Checked),
                                              Convert.ToString(radioButtonDontWork.Checked),Convert.ToString(numericUpDownGiftsCryNumber.Value),Convert.ToString(radioButtonSmallGift.Checked),Convert.ToString(radioButtonMiddleGift.Checked),
                                              Convert.ToString(radioButtonDonBuyGifts.Checked), Convert.ToString(radioButtonWhale.Checked), Convert.ToString(radioButtonParot.Checked),
                                              Convert.ToString(radioButtonCurrentPet.Checked), Convert.ToString(checkBoxAlarmBox.Checked), Convert.ToString(checkBoxBigGguru.Checked),Convert.ToString(checkBoxBiggestPotion.Checked),
                                              comboBoxTFResource.Text, Convert.ToString(numericUpDownTFDuringTime.Value), Convert.ToString(numericUpDownTFEveryTime.Value),Convert.ToString(numericUpDownTFEveryTime2.Value)};
            CompareValuesInFile(AdditionalSettingsBox.Name, AdditionalSettings);
            checkBoxCryDust.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[1]);
            checkBoxFish.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[2]);
            checkBoxFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[3]);
            checkBoxSoapMaking.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[4]);
            textBoxGold.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[5];
            textBoxGoldForMe.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[6];
            textBoxSoapToTP.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[7];
            textBoxBySlaves.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[8];
            checkBoxLitleGuru.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[9]);
            checkBoxReminder.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[10]);
            checkBoxTray.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[11]);
            checkBoxVillageManager.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[12]);
            numericUpDownVillageManagerTime.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[13]);
            checkBoxDayliGifts.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[14]);
            checkBoxHideBrowser.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[15]);
            textBoxAdv.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[16];
            radioButtonGoToForest.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[17]);
            radioButtonMakeTree.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[18]);
            radioButtonDontWork.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[19]);
            numericUpDownGiftsCryNumber.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[20]);
            radioButtonSmallGift.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[21]);
            radioButtonMiddleGift.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[22]);
            radioButtonDonBuyGifts.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[23]);
            radioButtonWhale.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[24]);
            radioButtonParot.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[25]);
            radioButtonCurrentPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[26]);
            checkBoxAlarmBox.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[27]);
            checkBoxBigGguru.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[28]);
            checkBoxBiggestPotion.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[29]);
            comboBoxTFResource.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[30];
            numericUpDownTFDuringTime.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[31]);
            numericUpDownTFEveryTime.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[32]);
            numericUpDownTFEveryTime2.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[33]);

            //Underground Settings
            string[] UndergroundSettings = { Convert.ToString(checkBoxUnderground.Checked), Convert.ToString(radioButtonUnderground.Checked), Convert.ToString(radioButtonFastUnderground.Checked),
                                               Convert.ToString(radioButtonWinch.Checked), Convert.ToString(radioButtonCord.Checked), Convert.ToString(checkBoxByKeys.Checked),
                                               Convert.ToString(numericUpDownUndergroundImm.Value), Convert.ToString(checkBoxOpenPanda.Checked), Convert.ToString(checkBoxSalePanda.Checked),
                                               Convert.ToString(numericUpDownPandaLvl.Value), Convert.ToString(checkBoxUndergroundSetPet.Checked), Convert.ToString(checkBoxUndGetPet.Checked),
                                               Convert.ToString(checkBoxPandaOpenCry.Checked), Convert.ToString(numericUpDownPandaLvlForSale.Value), Convert.ToString(radioButtonWorbBlueSoul.Checked),
                                               Convert.ToString(radioButtonWormPet.Checked), Convert.ToString(radioButtonDefaultUndrPet.Checked)};
            CompareValuesInFile(UndergroundBox.Name, UndergroundSettings);
            checkBoxUnderground.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[1]);
            radioButtonUnderground.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[2]);
            radioButtonFastUnderground.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[3]);
            radioButtonWinch.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[4]);
            radioButtonCord.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[5]);
            checkBoxByKeys.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[6]);
            numericUpDownUndergroundImm.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, UndergroundBox.Name)[7]);
            checkBoxOpenPanda.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[8]);
            checkBoxSalePanda.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[9]);
            numericUpDownPandaLvl.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, UndergroundBox.Name)[10]);
            checkBoxUndergroundSetPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[11]);
            checkBoxUndGetPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[12]);
            checkBoxPandaOpenCry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[13]);
            numericUpDownPandaLvlForSale.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, UndergroundBox.Name)[14]);
            radioButtonDefaultUndrPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[15]);
            radioButtonWormPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[16]);
            radioButtonWorbBlueSoul.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, UndergroundBox.Name)[17]);

            //Fight settings
            string[] FightSettings = { Convert.ToString(checkBoxFightMonsters.Checked), Convert.ToString(checkBoxFightZorro.Checked), Convert.ToString(radioButtonZorroLvl.Checked), Convert.ToString(radioButtonZorroList.Checked), Convert.ToString(checkBoxFight.Checked), Convert.ToString(radioButtonFightLvl.Checked), Convert.ToString(radioButtonFightList.Checked), Convert.ToString(checkBoxOborotka.Checked), Convert.ToString(checkBoxGetPet.Checked), Convert.ToString(checkBoxImmunOgl.Checked), Convert.ToString(checkBoxImmunAnti.Checked), Convert.ToString(radioButtonImmunPir.Checked), Convert.ToString(radioButtonImmunCry.Checked), Convert.ToString(checkBoxPetImmun.Checked), Convert.ToString(numericUpDownPetImmun.Value), Convert.ToString(checkBoxEnemyPower.Checked),
                                         Convert.ToString(numericUpDownEnemyPower.Value),Convert.ToString(numericUpDownEnemyBlock.Value),Convert.ToString(numericUpDownEnemyDex.Value),Convert.ToString(numericUpDownEnemyEd.Value),Convert.ToString(numericUpDownEnemyChar.Value),
                                         Convert.ToString(checkBoxMoralityMinus.Checked),Convert.ToString(checkBoxMoralityPlus.Checked),Convert.ToString(checkBoxMoralityZero.Checked),
                                         Convert.ToString(checkBoxDrinkOborotka.Checked), Convert.ToString(checkBoxArenaFight.Checked), Convert.ToString(numericUpDownArenaEnemy.Value),
                                         Convert.ToString(checkBoxArenaEvery5min.Checked)};
            CompareValuesInFile(FightBox.Name, FightSettings);
            checkBoxFightMonsters.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[1]);
            checkBoxFightZorro.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[2]);
            radioButtonZorroLvl.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[3]);
            radioButtonZorroList.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[4]);
            checkBoxFight.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[5]);
            radioButtonFightLvl.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[6]);
            radioButtonFightList.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[7]);
            checkBoxOborotka.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[8]);
            checkBoxGetPet.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[9]);
            checkBoxImmunOgl.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[10]);
            checkBoxImmunAnti.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[11]);
            radioButtonImmunPir.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[12]);
            radioButtonImmunCry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[13]);
            checkBoxPetImmun.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[14]);
            numericUpDownPetImmun.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[15]);
            checkBoxEnemyPower.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[16]);
            numericUpDownEnemyPower.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[17]);
            numericUpDownEnemyBlock.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[18]);
            numericUpDownEnemyDex.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[19]);
            numericUpDownEnemyEd.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[20]);
            numericUpDownEnemyChar.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[21]);
            checkBoxMoralityMinus.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[22]);
            checkBoxMoralityPlus.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[23]);
            checkBoxMoralityZero.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[24]);
            checkBoxDrinkOborotka.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[25]);
			checkBoxArenaFight.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[26]);
			numericUpDownArenaEnemy.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FightBox.Name)[27]);
            checkBoxArenaEvery5min.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FightBox.Name)[28]);

            //Heal settings
            string[] HealSettings = { Convert.ToString(checkBoxHeal.Checked), Convert.ToString(numericUpDownHeal.Value), Convert.ToString(checkBoxPetHeal.Checked), Convert.ToString(numericUpDownPetHeal.Value) };
            CompareValuesInFile(HealBox.Name, HealSettings);
            checkBoxHeal.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, HealBox.Name)[1]);
            numericUpDownHeal.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, HealBox.Name)[2]);
            checkBoxPetHeal.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, HealBox.Name)[3]);
            numericUpDownPetHeal.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, HealBox.Name)[4]);

            //Effects box
            string[] EffectsSettings = { Convert.ToString(checkBoxEffPoison.Checked), Convert.ToString(checkBoxEffGold.Checked), Convert.ToString(checkBoxEffAnti.Checked) };
            CompareValuesInFile(EffectsBox.Name, EffectsSettings);
            checkBoxEffPoison.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, EffectsBox.Name)[1]);
            checkBoxEffGold.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, EffectsBox.Name)[2]);
            checkBoxEffAnti.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, EffectsBox.Name)[3]);

            //Panda Effects box
            string[] PandaEffectsSettings = { Convert.ToString(checkBoxPandaEffect1.Checked), Convert.ToString(checkBoxPandaEffect2.Checked), Convert.ToString(checkBoxPandaEffect3.Checked), Convert.ToString(checkBoxPandaEffect4.Checked), comboBoxPandaEffects.Text, Convert.ToString(radioButtonPEpir.Checked), Convert.ToString(radioButtonPEcry.Checked) };
            CompareValuesInFile(PandaEffectsBox.Name, PandaEffectsSettings);
            checkBoxPandaEffect1.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[1]);
            checkBoxPandaEffect2.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[2]);
            checkBoxPandaEffect3.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[3]);
            checkBoxPandaEffect4.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[4]);
            comboBoxPandaEffects.Text = ReadFromFile(SettingsFile, PandaEffectsBox.Name)[5];
            radioButtonPEpir.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[6]);
            radioButtonPEcry.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, PandaEffectsBox.Name)[7]);

            //Far countrys
            string[] FarCountrSettings = { Convert.ToString(checkBoxGetRP.Checked), Convert.ToString(radioButtonFirstBoat.Checked), Convert.ToString(radioButtonSecondBoat.Checked) };
            CompareValuesInFile(FarCountrBox.Name, FarCountrSettings);
            checkBoxGetRP.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FarCountrBox.Name)[1]);
            radioButtonFirstBoat.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FarCountrBox.Name)[2]);
            radioButtonSecondBoat.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FarCountrBox.Name)[3]);


            //First Fly box
            string[] FlySettings = { Convert.ToString(radioButtonBTFFly.Checked), Convert.ToString(radioButtonKarFFly.Checked), Convert.ToString(numericUpDownTrackFFly.Value), Convert.ToString(numericUpDownHrsFFly.Value),
                                   Convert.ToString(radioButtonBTSFly.Checked), Convert.ToString(radioButtonKarSFly.Checked), Convert.ToString(numericUpDownTrackSFly.Value), Convert.ToString(numericUpDownHrsSFly.Value),
                                   Convert.ToString(radioButtonBTTFly.Checked), Convert.ToString(radioButtonKarTFly.Checked), Convert.ToString(numericUpDownTrackTFly.Value), Convert.ToString(numericUpDownHrsTFly.Value),
                                   Convert.ToString(radioButtonBTFoFly.Checked), Convert.ToString(radioButtonKarFoFly.Checked), Convert.ToString(numericUpDownTrackFoFly.Value), Convert.ToString(numericUpDownHrsFoFly.Value),
                                   Convert.ToString(radioButtonSTFFly.Checked),Convert.ToString(radioButtonSTSFly.Checked),Convert.ToString(radioButtonSTTFly.Checked),Convert.ToString(radioButtonSTFoFly.Checked),
                                   Convert.ToString(checkBoxDontUseFFly.Checked), Convert.ToString(checkBoxDontUseSFly.Checked), Convert.ToString(checkBoxDontUseTFly.Checked), Convert.ToString(checkBoxDontUseFoFly.Checked)};
            CompareValuesInFile(FlyBox.Name, FlySettings);
            radioButtonBTFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[1]);
            radioButtonKarFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[2]);
            numericUpDownTrackFFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[3]);
            numericUpDownHrsFFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[4]);

            radioButtonBTSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[5]);
            radioButtonKarSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[6]);
            numericUpDownTrackSFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[7]);
            numericUpDownHrsSFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[8]);

            radioButtonBTTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[9]);
            radioButtonKarTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[10]);
            numericUpDownTrackTFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[11]);
            numericUpDownHrsTFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[12]);

            radioButtonBTFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[13]);
            radioButtonKarFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[14]);
            numericUpDownTrackFoFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[15]);
            numericUpDownHrsFoFly.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, FlyBox.Name)[16]);

            radioButtonSTFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[17]);
            radioButtonSTSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[18]);
            radioButtonSTTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[19]);
            radioButtonSTFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[20]);

            checkBoxDontUseFFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[21]);
            checkBoxDontUseSFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[22]);
            checkBoxDontUseTFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[23]);
            checkBoxDontUseFoFly.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, FlyBox.Name)[24]);

            //Abilitys Box countrys
            string[] AbilitysSettings = { Convert.ToString(radioButtonSK1.Checked), Convert.ToString(radioButtonSK2.Checked), Convert.ToString(radioButtonSK3.Checked), Convert.ToString(radioButtonSK4.Checked), 
                                          Convert.ToString(radioButtonSK5.Checked), Convert.ToString(radioButtonSK6.Checked), Convert.ToString(radioButtonSK7.Checked), Convert.ToString(radioButtonSK8.Checked),Convert.ToString(checkBoxMassAbilitys.Checked)};
            CompareValuesInFile(MassAbilityBox.Name, AbilitysSettings);
            radioButtonSK1.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[1]);
            radioButtonSK2.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[2]);
            radioButtonSK3.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[3]);
            radioButtonSK4.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[4]);
            radioButtonSK5.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[5]);
            radioButtonSK6.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[6]);
            radioButtonSK7.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[7]);
            radioButtonSK8.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[8]);
            checkBoxMassAbilitys.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, MassAbilityBox.Name)[9]);

            //Shop Box countrys
            string[] ShopSettings = { Convert.ToString(checkBoxShop.Checked), textBoxProdutName.Text, comboBoxCurrencyType.Text, textBoxMaxValue.Text, Convert.ToString(numericUpDownPPvalue.Value), Convert.ToString(numericUpDownItemLevel.Value),
                                    Convert.ToString(numericUpDownTryByMin.Value), textBoxCurrentGold.Text, textBoxCurrenCry.Text, textBoxCurrentGren.Text };
            CompareValuesInFile(ShopBox.Name, ShopSettings);
            checkBoxShop.Checked = Convert.ToBoolean(ReadFromFile(SettingsFile, ShopBox.Name)[1]);
            textBoxProdutName.Text = ReadFromFile(SettingsFile, ShopBox.Name)[2];
            comboBoxCurrencyType.Text = ReadFromFile(SettingsFile, ShopBox.Name)[3];
            textBoxMaxValue.Text = ReadFromFile(SettingsFile, ShopBox.Name)[4];
            numericUpDownPPvalue.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, ShopBox.Name)[5]);
            numericUpDownItemLevel.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, ShopBox.Name)[6]);
            numericUpDownTryByMin.Value = Convert.ToDecimal(ReadFromFile(SettingsFile, ShopBox.Name)[7]);
            textBoxCurrentGold.Text = ReadFromFile(SettingsFile, ShopBox.Name)[8];
            textBoxCurrenCry.Text = ReadFromFile(SettingsFile, ShopBox.Name)[9];
            textBoxCurrentGren.Text = ReadFromFile(SettingsFile, ShopBox.Name)[10];



            Thread BotThread = new Thread(new ThreadStart(WorkThreadFunction));
            BotThread.Start();

        }

        private void CheckBotMessage()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://dl.dropbox.com/s/vhrstr5i424la7s/BotMessage.txt?token_hash=AAESnSj9Ws8Wzw7OFJdygkt5RUMF1rlmEHD7I7n_H8qyjg&dl=1");
                StreamReader reader = new StreamReader(stream);
                string fileContent = reader.ReadToEnd();
                if (fileContent != "Null")
                {
                    lable29Text = fileContent;
                }
            }
            catch { }
        }

        private IList<string> GetDonatePlayersList()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://dl.dropboxusercontent.com/s/309hd9u59aii20y/donate.txt?token_hash=AAFD8tTztvO2tzfrqYc115h-Y6OAUwG_k7FhqVWQB6yULA&dl=1");
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(';');
        }

        private bool IsDonatePlayer(IList<string> playersList)
        {
            for (int i = 0; i < playersList.Count - 1; i++)
            {
                if (playersList[i] == textBoxMd5.Text)
                {
                    if (dateTimeCompare(playersList[i + 1].Split('/')))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool dateTimeCompare(string[] dateTime)
        {
            if (Convert.ToInt32(dateTime[2]) > DateTime.Now.Year)
            {
                return true;
            }
            else
            {
                if (Convert.ToInt32(dateTime[2]) == DateTime.Now.Year)
                {
                    if (Convert.ToInt32(dateTime[1]) > DateTime.Now.Month)
                    {
                        return true;
                    }
                    else
                    {
                        if (Convert.ToInt32(dateTime[1]) == DateTime.Now.Month)
                        {
                            if (Convert.ToInt32(dateTime[0]) >= DateTime.Now.Day)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

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

        public void WorkThreadFunction()
        {
            try
            {
                BotvaClass Bot = new BotvaClass();
                try
                {
                    Bot.EnvironmentSetUp();
                }
                catch { }
                while (true)
                {
                    try
                    {
                        Bot.GoToOldoMsters();
                        Bot.AlertFight();
                        Bot.LitleGuru();
                        Bot.BigGuru();
                        Bot.PotionBoil();
                        Bot.TanksMaking();
                        Bot.MineGetCry();
                        Bot.StatsUp();
                        Bot.FightImmuns();
                        Bot.Fight();
						Bot.ArenaFight();
                        Bot.NegativeEffects();
                        Bot.BigFields();
                        Bot.SmallFields();
                        Bot.UndergroundFast();
                        Bot.Underground();
                        Bot.StatsUp();
                        Bot.GoldDiscard();
                        Bot.MineByInventory();
                        Bot.MineGoWork();
                        Bot.Sawmill();
                        Bot.CrystalDustMaking();
                        Bot.SoapMaking();
                        Bot.Fishing();
                        Bot.Fly();
                        Bot.Healing();
                        Bot.PetHealing();
                        Bot.FarCountrys();
                        Bot.PandaEffects();
                        Bot.OpenNewPand();
                        Bot.SellSoap();
                        Bot.BySlaves();
                        Bot.CryStirring();
                        Bot.MassAbilitys();
                        Bot.CheckForNest();
                        Bot.VillageManager();
                        Bot.DayliGifts();
                        Bot.BuyGifts();
                        Bot.TradeField();

                        //Adv
                        Bot.OpenAdvPage();

                    }
                    catch
                    {
                    }
                    finally
                    {
                        Bot.ReLogIn();
                    }
                }
            }
            catch { }
        }

        private void CompareValuesInFile(string Boxname, string[] ValuesFromForm)
        {
            string[] valuesFromFile = ReadFromFile(SettingsFile, Boxname);
            //если в файле значение нул или пустое или файла вообще нет
            if ((valuesFromFile[0] == null || valuesFromFile[0] == "") || File.Exists(SettingsFile + SettingsFileExtantion) == false)
            {
                WreateToFile(SettingsFile, Boxname, ValuesFromForm);
            }
            else
            {
                for (int i = 0; i < ValuesFromForm.Length; i++)
                {
                    try
                    {
                        if (ValuesFromForm[i] != valuesFromFile[i + 1])
                        {
                            WreateToFile(SettingsFile, Boxname, ValuesFromForm);
                            break;
                        }
                    }
                    catch
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
            if (File.Exists(FileName + SettingsFileExtantion) == true)
            {
                var reader = new StreamReader(File.OpenRead(FileName + SettingsFileExtantion));
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
            if (File.Exists(FileName + SettingsFileExtantion) == true)
            {
                var reader = new StreamReader(File.OpenRead(FileName + SettingsFileExtantion));
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
                File.Delete(FileName + SettingsFileExtantion);
                var writer = new StreamWriter(File.OpenWrite(FileName + SettingsFileExtantion));
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
                var writer = new StreamWriter(File.OpenWrite(FileName + SettingsFileExtantion));
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosingChromeDriverProcces();
            Environment.Exit(0);
        }

        private void UIFormDissapiring()
        {
            double Opacity = 1;
            for (int i = 0; i < 22; i++)
            {
                this.Opacity = Opacity;
                System.Threading.Thread.Sleep(3);
                Opacity -= 0.04;
            }
            Opacity += 1;
            foreach (Control c in Controls)
            {
                if (c.Name.Contains("Box"))
                {
                    c.Visible = false;
                }
            }
        }

        private void UIFormAppearing()
        {
            double Opacity = 0.1;
            for (int i = 0; i < 22; i++)
            {
                this.Opacity = Opacity;
                System.Threading.Thread.Sleep(4);
                Opacity += 0.04;
            }
            Opacity -= 1;
            foreach (Control c in Controls)
            {
                if (c.Name.Contains("Box"))
                {
                    c.Visible = true;
                }
            }
        }

        private void UIBoxDisplay(int FormSizex, int FormSizeY, string BoxName)
        {
            UIFormDissapiring();
            foreach (Control c in Controls)
            {
                if (c.Name.Contains("Box"))
                {
                    c.Location = new Point(800, 800);
                }
                if (c.Name.Contains(BoxName))
                {
                    c.Location = new Point(7, 4);
                    this.Size = new System.Drawing.Size(c.Width + 20, c.Height + 40);
                }
            }
            UIFormAppearing();
        }

        private void ClosingChromeDriverProcces()
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains("chromedriver"))
                {
                    clsProcess.Kill();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "Login");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://simplebot.ru");
        }

        private void checkBoxWorkInMine_CheckStateChanged(object sender, EventArgs e)
        {
            CBWorkInMine();
        }

        private void CBWorkInMine()
        {
            if (checkBoxWorkInMine.Checked == true)
            {
                panelMineBox.Enabled = true;
            }
            else panelMineBox.Enabled = false;
        }

        private void checkBoxUnderground_CheckStateChanged(object sender, EventArgs e)
        {
            CBUnderground();
        }

        private void CBUnderground()
        {
            if (checkBoxUnderground.Checked == true)
            {
                panelUnderground.Enabled = true;
            }
            else panelUnderground.Enabled = false;
        }

        private void checkBoxPotionMaking_CheckedChanged(object sender, EventArgs e)
        {
            CBPotionMaking();
        }

        private void CBPotionMaking()
        {
            if (checkBoxPotionMaking.Checked == true)
            {
                panelPotionMaking.Enabled = true;
            }
            else panelPotionMaking.Enabled = false;
        }

        private void radioButtonFastUnderground_CheckedChanged(object sender, EventArgs e)
        {
            RBFastUnderground();
        }

        private void RBFastUnderground()
        {
            if (radioButtonFastUnderground.Checked == true)
            {
                numericUpDownUndergroundImm.Enabled = true;
            }
            else numericUpDownUndergroundImm.Enabled = false;
        }

        private void CBSalePanda()
        {
            if (checkBoxSalePanda.Checked == true)
            {
                numericUpDownPandaLvl.Enabled = true;
                label9.Enabled = true;
            }
            else
            {
                numericUpDownPandaLvl.Enabled = false;
                label9.Enabled = false;
            }
        }

        private void checkBoxSalePanda_CheckedChanged(object sender, EventArgs e)
        {
            CBSalePanda();
        }

        private void checkBoxFight_CheckedChanged(object sender, EventArgs e)
        {
            CBFight();
        }

        private void CBFight()
        {
            if (checkBoxFight.Checked == true)
            {
                FightPanel.Enabled = true;
            }
            else FightPanel.Enabled = false;
        }

        private void checkBoxFightZorro_CheckedChanged(object sender, EventArgs e)
        {
            CBFightZorro();
        }

        private void CBFightZorro()
        {
            if (checkBoxFightZorro.Checked == true)
            {
                ZorroFightPanel.Enabled = true;
            }
            else ZorroFightPanel.Enabled = false;
        }

        private void checkBoxHeal_CheckedChanged(object sender, EventArgs e)
        {
            CBHeal();
        }

        private void CBHeal()
        {
            if (checkBoxHeal.Checked == true)
            {
                numericUpDownHeal.Enabled = true;
            }
            else numericUpDownHeal.Enabled = false;
        }

        private void checkBoxPetHeal_CheckedChanged(object sender, EventArgs e)
        {
            CBPetHeal();
        }

        private void CBPetHeal()
        {
            if (checkBoxPetHeal.Checked == true)
            {
                numericUpDownPetHeal.Enabled = true;
            }
            else numericUpDownPetHeal.Enabled = false;
        }

        private void checkBoxUndGetPet_CheckedChanged(object sender, EventArgs e)
        {
            CBUndGetPet();
        }

        private void CBUndGetPet()
        {
            if (checkBoxUndGetPet.Checked == true)
            {
                checkBoxUndergroundSetPet.Checked = false;
            }
        }

        private void checkBoxUndergroundSetPet_CheckedChanged(object sender, EventArgs e)
        {
            CBUndergroundSetPet();
        }

        private void CBUndergroundSetPet()
        {
            if (checkBoxUndergroundSetPet.Checked == true)
            {
                checkBoxUndGetPet.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "PandaEffectsBox");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "ImmunEffetsBox");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void checkBoxPetImmun_CheckedChanged(object sender, EventArgs e)
        {
            CBPetImmun();
        }

        private void CBPetImmun()
        {
            if (checkBoxPetImmun.Checked == true)
            {
                numericUpDownPetImmun.Enabled = true;
            }
            else numericUpDownPetImmun.Enabled = false;
        }

        private void checkBoxGetPet_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGetPet.Checked == false)
            {
                checkBoxPetImmun.Checked = false;
            }
        }

        private void checkBoxGetRP_CheckedChanged(object sender, EventArgs e)
        {
            CBGetRP();
        }

        private void CBGetRP()
        {
            if (checkBoxGetRP.Checked == false)
            {
                radioButtonFirstBoat.Enabled = false;
                radioButtonSecondBoat.Enabled = false;
            }
            else
            {
                radioButtonFirstBoat.Enabled = true;
                radioButtonSecondBoat.Enabled = true;
            }
        }

        private void checkBoxEnemyPower_CheckedChanged(object sender, EventArgs e)
        {
            CBEnemyPower();
        }

        private void CBEnemyPower()
        {
            if (checkBoxEnemyPower.Checked == true)
            {
                numericUpDownEnemyPower.Enabled = true;
                button12.Enabled = true;
            }
            else button12.Enabled = false; ;
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "FlyBox");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            if (Timer_CloseBot.CompareTo(DateTime.Now) < 0 & isDonatePlayer == false)
            {
                this.Close();
            }

            if (isDonatePlayer)
            {
                textBoxMd5.BackColor = Color.LightGreen;
                //убийца ошибки хромдрайвера
                if (Timer_ChromeDriverKiller.CompareTo(DateTime.Now) < 0 & !chromeDriverCiller)
                {
                    ChromeDriverKillerProcess();
                    chromeDriverCiller = true;
                }
				//Подрубаем Арену если донат
	            button39.Enabled = true;
            }
            //OpenSite();
            //PanelDisplay();//BrowserDisplay();
            //GoBackToSite();
            //BrowserReloadContent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            CheckBotStatus();
            CheckBotMessage();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isDonatePlayer == false)
                isDonatePlayer = IsDonatePlayer(GetDonatePlayersList());
            label29.Text = lable29Text;
            pictureStatusNone.Visible = false;
            if (botStatus == false)
            {
                pictureStatusFalse.Visible = true;
                pictureStatusTrue.Visible = false;
            }
            else
            {
                pictureStatusFalse.Visible = false;
                pictureStatusTrue.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MineInventoryBox");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "BankBox");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "EnemyBox");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "SoapBox");
        }

        private void checkBoxSoapMaking_CheckedChanged(object sender, EventArgs e)
        {
            CBSoapMaking();
        }

        private void CBSoapMaking()
        {
            if (checkBoxSoapMaking.Checked == false)
            {
                textBoxSoapToTP.Text = "Нет";
                textBoxBySlaves.Text = "Нет";
            }
            else
            {
                try
                {
                    textBoxSoapToTP.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[7];
                    textBoxBySlaves.Text = ReadFromFile(SettingsFile, AdditionalSettingsBox.Name)[8];
                }
                catch { }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "StiringBox");
        }

        private void checkBoxLitleGuru_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLitleGuru.Checked == true)
            {
                checkBoxBigGguru.Checked = false;
                MessageBox.Show("При включении прокачки Малого гуру\r\nвсе остальные функции работать НЕ будут", "Simpe Bot: Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {

            Point l2 = Cursor.Position;
            System.Threading.Thread.Sleep(3000);
            Click(l2.X, l2.Y);
            int x = Screen.PrimaryScreen.WorkingArea.Width;
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://vitaliidolotov.narod2.ru/");
            driver.Manage().Window.Position = new Point(0, 0);
            IWebElement t = driver.FindElement(By.TagName("a"));
            driver.Manage().Window.Size = new Size(300, 300);

            ILocatable loc = (ILocatable)t;
            Point p = loc.LocationOnScreenOnceScrolledIntoView;
            IMouse mm = ((IHasInputDevices)driver).Mouse;
            mm.MouseMove(loc.Coordinates, 100, 100);

            //new Actions(driver).DragAndDrop(driver.FindElement(By.XPath(Xpath)), driver.FindElement(By.ClassName("ui-sortable"))).Build().Perform();

            mm.MouseMove(loc.Coordinates, 100, 100);
            mm.MouseDown(loc.Coordinates);
            mm.MouseUp(loc.Coordinates);


        }
        private const int MOUSEEVENTF_LEFTDOWN = 0x2;
        private const int MOUSEEVENTF_LEFTUP = 0x4;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);

        public void Click(int x, int y)
        {
            Random rnd = new Random();
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            System.Threading.Thread.Sleep(rnd.Next(130, 260));
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private void checkBoxTaskBar_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBoxTray.Checked == true)
            //{
            //    this.ShowInTaskbar = false;
            //}
            //else this.ShowInTaskbar = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int sizeBox = LoginBox.Size.Height;
            int sizeForm = this.Size.Height;
            for (int i = 0; i < 22; i++)
            {

                this.Size = new System.Drawing.Size(this.Size.Width, sizeForm);
                LoginBox.Size = new System.Drawing.Size(LoginBox.Size.Width, sizeBox);
                System.Threading.Thread.Sleep(4);
                sizeForm += 2;
                sizeBox += 2;
            }

        }

        private void button17_MouseHover(object sender, EventArgs e)
        {
            button17.Visible = false;
            checkBoxHideBrowser.Visible = true;
            int sizeBox = LoginBox.Size.Height;
            int sizeForm = this.Size.Height;
            for (int i = 0; i < 38; i++)
            {

                this.Size = new System.Drawing.Size(this.Size.Width, sizeForm);
                LoginBox.Size = new System.Drawing.Size(LoginBox.Size.Width, sizeBox);
                System.Threading.Thread.Sleep(4);
                sizeForm += 2;
                sizeBox += 2;
            }
            button18.Visible = true;
        }

        private void button18_MouseHover(object sender, EventArgs e)
        {
            button18.Visible = false;
            checkBoxHideBrowser.Visible = false;
            int sizeBox = LoginBox.Size.Height;
            int sizeForm = this.Size.Height;
            for (int i = 0; i < 38; i++)
            {

                this.Size = new System.Drawing.Size(this.Size.Width, sizeForm);
                LoginBox.Size = new System.Drawing.Size(LoginBox.Size.Width, sizeBox);
                System.Threading.Thread.Sleep(4);
                sizeForm -= 2;
                sizeBox -= 2;
            }
            button17.Visible = true;
        }

        private void checkBoxReminder_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxReminder.Checked == true)
            {
                SoundPlayer simpleSound = new SoundPlayer("Underground_Sound.wav");
                simpleSound.Play();
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {

        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://vk.com/club50060455");
        }

        private void checkBoxOpenPanda_CheckedChanged(object sender, EventArgs e)
        {
            CBOpenPanda();
        }

        private void CBOpenPanda()
        {
            if (checkBoxOpenPanda.Checked == true)
            {
                checkBoxPandaOpenCry.Enabled = true;
                numericUpDownPandaLvlForSale.Enabled = true;
                label34.Enabled = true;
            }
            else
            {
                checkBoxPandaOpenCry.Enabled = false;
                numericUpDownPandaLvlForSale.Enabled = false;
                label34.Enabled = false;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "PandaBox");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MoralityControlBox");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "StutsUpBox");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MassAbilityBox");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "VillageBox");
        }

        private void checkBoxVillageManager_CheckedChanged(object sender, EventArgs e)
        {
            CBVillageManager();
        }

        private void CBVillageManager()
        {
            if (checkBoxVillageManager.Checked == true)
            {
                numericUpDownVillageManagerTime.Enabled = true;
                label35.Enabled = true;
                label36.Enabled = true;
            }
            else
            {
                numericUpDownVillageManagerTime.Enabled = false;
                label35.Enabled = false;
                label36.Enabled = false;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "ShopBox");
        }

        private void button17_Click_1(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void OpenSite()
        {
            if (Timer_OpenSite.CompareTo(DateTime.Now) < 0)
            {
                webBrowser1.Navigate("http://simplebot.ru/");
                webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                Timer_OpenSite = ToDateTime("99:00:00");
            }
        }

        private void GoBackToSite()
        {
            if (Timer_GoBack.CompareTo(DateTime.Now) < 0)
            {
                webBrowser1.Navigate("http://simplebot.ru/");
                Timer_GoBack = ToDateTime("99:00:00");
            }
        }

        private void PanelDisplay()
        {
            if (Timer_OpenWindow.CompareTo(DateTime.Now) < 0 && DateTime.Now.Day != Convert.ToInt32(textBoxAdv.Text))
            {
                textBoxAdv.Text = Convert.ToString(DateTime.Now.Day);
                if (rnd.Next(0, 3) == 0)
                {
                    panelBrowser.Location = new Point(0, 0);
                    this.Size = new System.Drawing.Size(panelBrowser.Width + 2, panelBrowser.Height + 2);
                    this.MinimizeBox = false;
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                    this.Focus();
                }
                Timer_OpenWindow = ToDateTime("99:00:00");
                //Additional Settings
                string[] AdditionalSettings = { Convert.ToString(checkBoxCryDust.Checked), Convert.ToString(checkBoxFish.Checked), Convert.ToString(checkBoxFly.Checked),
                                              Convert.ToString(checkBoxSoapMaking.Checked), textBoxGold.Text, textBoxGoldForMe.Text, textBoxSoapToTP.Text, textBoxBySlaves.Text,
                                              Convert.ToString(checkBoxLitleGuru.Checked), Convert.ToString(checkBoxReminder.Checked), Convert.ToString(checkBoxTray.Checked),
                                              Convert.ToString(checkBoxVillageManager.Checked), Convert.ToString(numericUpDownVillageManagerTime.Value), Convert.ToString(checkBoxDayliGifts.Checked),
                                              Convert.ToString(checkBoxHideBrowser.Checked), textBoxAdv.Text};
                CompareValuesInFile(AdditionalSettingsBox.Name, AdditionalSettings);
            }
        }

        private void BrowserDisplay()
        {
            if (Timer_OpenWindow.CompareTo(DateTime.Now) < 0 && DateTime.Now.Day > Convert.ToInt32(textBoxAdv.Text))
            {

                textBoxAdv.Text = Convert.ToString(DateTime.Now.Day);
                if (rnd.Next(0, 3) == 0)
                {
                    if (webBrowser1.Document != null)
                    {
                        HtmlElementCollection elems = webBrowser1.Document.GetElementsByTagName("td");
                        webBrowser1.Document.Body.ScrollTop = elems[3].OffsetRectangle.Top;
                    }
                    panelBrowser.Location = new Point(0, 0);
                    this.Size = new System.Drawing.Size(panelBrowser.Width + 2, panelBrowser.Height + 2);
                    this.MinimizeBox = false;
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                    this.Focus();
                    webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                }
                Timer_OpenWindow = ToDateTime("99:00:00");
                //Additional Settings
                string[] AdditionalSettings = { Convert.ToString(checkBoxCryDust.Checked), Convert.ToString(checkBoxFish.Checked), Convert.ToString(checkBoxFly.Checked),
                                              Convert.ToString(checkBoxSoapMaking.Checked), textBoxGold.Text, textBoxGoldForMe.Text, textBoxSoapToTP.Text, textBoxBySlaves.Text,
                                              Convert.ToString(checkBoxLitleGuru.Checked), Convert.ToString(checkBoxReminder.Checked), Convert.ToString(checkBoxTray.Checked),
                                              Convert.ToString(checkBoxVillageManager.Checked), Convert.ToString(numericUpDownVillageManagerTime.Value), Convert.ToString(checkBoxDayliGifts.Checked),
                                              Convert.ToString(checkBoxHideBrowser.Checked), textBoxAdv.Text};
                CompareValuesInFile(AdditionalSettingsBox.Name, AdditionalSettings);
            }
        }

        private void BrowserReloadContent()
        {
            if (Timer_Reload.CompareTo(DateTime.Now) < 0)
            {
                if (webBrowser1.Document != null)
                {
                    webBrowser1.Refresh();
                }
                Timer_Reload = ToDateTime("03:" + Convert.ToString(rnd.Next(10, 57)) + ":00");
            }
        }

        private void BrowserHide()
        {
            UIBoxDisplay(3, 4, "LoginBox");
            this.MinimizeBox = true;
            panelBrowser.Location = new Point(800, 800);
            ClickCount = 0;
        }

        public void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.MouseUp += new HtmlElementEventHandler(Document_MouseUp);

        }
        public void Document_MouseUp(object sender, HtmlElementEventArgs e)
        {
            if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.Left)
            {
                ClickCount++;
                if (ClickCount == 4)
                {
                    BrowserHide();
                    Timer_GoBack = ToDateTime("00:" + "0" + Convert.ToString(rnd.Next(1, 4)) + ":" + Convert.ToString(rnd.Next(10, 53)));
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            BrowserHide();
        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "FightBox");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            button33.Visible = false;
            button20.BackColor = Color.Lime;
            button20.Text = "Вернуться назад";
            System.Diagnostics.Process.Start("http://simplebot.ru/");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "PetForUndergr");
        }

        private void button34_Click_1(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "GiftsBox");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string loginMd5 = GetMd5Hash(md5Hash, login);
            textBoxMd5.Text = loginMd5;
        }

        private void checkBoxBigGguru_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBigGguru.Checked == true)
            {
                checkBoxLitleGuru.Checked = false;
                MessageBox.Show("При включении прокачки Большого гуру\r\nвсе остальные функции работать НЕ будут", "Simpe Bot: Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button38_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "TradeFieldBox");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            UIBoxDisplay(3, 4, "MenuBox");
        }

        private void numericUpDownTFEveryTime_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownTFEveryTime2.Value = numericUpDownTFEveryTime.Value + 3;
        }

        private void numericUpDownTFEveryTime2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownTFEveryTime2.Value <= numericUpDownTFEveryTime.Value)
                numericUpDownTFEveryTime2.Value = numericUpDownTFEveryTime.Value + 1;
        }

		private void button40_Click(object sender, EventArgs e)
		{
			UIBoxDisplay(3, 4, "MenuBox");
		}

		private void button39_Click(object sender, EventArgs e)
		{
			UIBoxDisplay(3, 4, "ArenaBox");
		}
    }
}