using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simple_Bot.Resources
{
    public static class BotWorker
    {
        static BotvaClass Bot;// = new BotvaClass();
        static BotvaClass Bg;

        static Thread MassFightThread;

        static DateTime Timer_Bg = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second - 1);

        public static void WorkThreadFunction()
        {
            try
            {
                if (Bot == null)
                    Bot = new BotvaClass();
            }
            catch { }
            try
            {
                try
                {
                    Bot.EnvironmentSetUp();
                }
                catch { }
                while (true)
                {
                    try
                    {
                        if (Timer_Bg.CompareTo(DateTime.Now) < 0)
                            if (Bot.IsNecessaryMineIsOpened())
                            {
                                Timer_Bg = ToDateTime("00:35:00");
                                MassFight();
                            }

                        Bot.GoToOldoMsters();
                        Bot.AlertFight();
                        Bot.LitleGuru();
                        Bot.BigGuru();
                        Bot.PotionBoil();
                        Bot.TanksMaking();
                        Bot.MineGetCry();
                        Bot.StatsUp();
                        Bot.ArenaFight();
                        Bot.FightImmuns();
                        Bot.Fight();
                        Bot.NegativeEffects();
                        Bot.BigFields();
                        Bot.SmallFields();
                        Bot.GoldDiscard();
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
                        Bot.Shop();
                        //Bot.MassFight();




                        //Adv
                        //Bot.OpenAdvPage();

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

        public static void Rest()
        {
            Bot.Rest();
            Quit(Bot);
            Bot = null;
        }

        public static void MassFight()
        {
            Bg = new BotvaClass();

            try
            {
                if (MassFightThread.IsAlive)
                    MassFightThread.Abort();
            }
            catch { }

            MassFightThread = new Thread(new ThreadStart(BotWorker.FightOnMassFight));
            MassFightThread.Start();
        }

        private static void FightOnMassFight()
        {
            Bg.MassFight();
        }

        private static DateTime ToDateTime(string CounterTime)
        {
            char Separator = ':';
            string[] SeparatedTime;
            SeparatedTime = CounterTime.Split(Separator);
            DateTime ReturnTime = DateTime.Now;
            TimeSpan TimeToAdd = new TimeSpan(0, Convert.ToInt32(SeparatedTime[0]), Convert.ToInt32(SeparatedTime[1]), Convert.ToInt32(SeparatedTime[2]));
            ReturnTime = DateTime.Now.Add(TimeToAdd);
            return ReturnTime;
        }

        private static void Quit(BotvaClass botvaObject)
        {
            botvaObject.Quit();
        }
    }
}
