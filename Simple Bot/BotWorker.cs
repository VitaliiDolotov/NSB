using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple_Bot.Resources
{
    public static class BotWorker
    {
        static BotvaClass Bot = new BotvaClass();

        public static void WorkThreadFunction()
        {
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
                        Bot.MassFight();


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
        }

        public static void Quit()
        {
            Bot.Quit();
        }
    }
}
