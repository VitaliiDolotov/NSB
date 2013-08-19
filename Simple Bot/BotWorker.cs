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

                        Bot.CollectStones();

                        Bot.MrIdiot(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.AlertFight(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.LitleGuru(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.BigGuru(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.PotionBoil(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.TanksMaking(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.MineGetCry(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.GoToOldoMsters(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.StatsUp(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.ArenaFight(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.FightImmuns(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.Fight(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.NegativeEffects(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.BigFields(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.SmallFields(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.GoldDiscard(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.UndergroundFast(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.Underground(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.StatsUp(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.GoldDiscard(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.MineByInventory(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.MineGoWork(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.Sawmill(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.CrystalDustMaking(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.SoapMaking(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.Fishing(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.Fly(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.Healing(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.PetHealing(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.FarCountrys(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.PandaEffects(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.OpenNewPand(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.SellSoap(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.BySlaves(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.CryStirring(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.MassAbilitys(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.CheckForNest(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.VillageManager(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.DayliGifts(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.BuyGifts(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.TradeField(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.Shop(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.CulonsUp(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.ChameleonsHeal(); Bot.WaitUntilThreadBecomeAvailable();
                        Bot.OpenDreamCasket(); Bot.WaitUntilThreadBecomeAvailable();


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
