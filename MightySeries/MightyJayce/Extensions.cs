using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.Data;
using SharpDX;

namespace MightyJayce
{
    class Extensions : ConfigMenu
    {
        public static void ExtensionsLoader() //Call Utility Events
        {
            Game.OnUpdate += Game_OnGameUpdate;
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {               
                case Orbwalking.OrbwalkingMode.LaneClear:
                    laneclear_Cannon();
                    laneclear_Hammer();
                    laneclear_AutoSwitch();
                    break;
            }
        }
        public static void laneclear_Cannon()
        {
            //cannonclear.AddItem(new MenuItem("RangedClearQ", "Use Q").SetValue(true));
            //cannonclear.AddItem(new MenuItem("RangedClearQHit", "If Minion Hit").SetValue(new Slider(2, 6, 0)));
            //cannonclear.AddItem(new MenuItem("RangedClearW", "Use W").SetValue(true));
            //cannonclear.AddItem(new MenuItem("RangedClearQE", "Use Q + E").SetValue(true));
            //cannonclear.AddItem(new MenuItem("RangedClearQEHit", "If Minions Hit").SetValue(new Slider(3, 6, 0)));;

            var minion = ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsEnemy && !m.IsDead).FirstOrDefault();
            var aa = Player.GetAutoAttackDamage(minion, true);

            if (Player.IsWindingUp || !minion.IsValidTarget() 
                ||minion.IsValidTarget(CannonQE.Range + 75)|| minion.Team == GameObjectTeam.Neutral)
                return;

            var allMinionsCannonQ = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, CannonQ.Range + CannonQ.Width);
            var allMinionsCannonW = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Orbwalking.GetAttackRange(Player));
            var allMinionsCannonE = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, CannonE.Range + CannonE.Width);

            var CannonQfarmpos = CannonQ.GetCircularFarmLocation(allMinionsCannonQ, CannonQ.Width);

            var useq = Config.Item("RangedClearQ").GetValue<bool>();
            var usew = Config.Item("RangedClearW").GetValue<bool>();

            // USE Q
            if (CannonQ.IsReady() && CannonQfarmpos.MinionsHit >=
                Config.Item("RangedClearQHit").GetValue<Slider>().Value && useq)
            {
                CannonQ.Cast(CannonQfarmpos.Position);
            }

            // USE W 
            if (CannonQfarmpos.MinionsHit <= 2 && useq && CannonQ.IsReady() 
                && minion.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Player)))
            {
                return;
            }

            else if (CannonW.IsReady() && usew && minion.Health >= aa * 2
                    && minion.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Player))) 
                {
                    CannonW.Cast(Player);
                }                 
        }

        public static void laneclear_Hammer()
        {
            var minion = ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsEnemy && !m.IsDead).FirstOrDefault();
            var aa = Player.GetAutoAttackDamage(minion, true);

            if (!Mechanics.HammerMode || Player.IsWindingUp)
                return;

            else if (!minion.IsValidTarget()
                || minion.IsValidTarget(CannonQE.Range + 75) || minion.Team == GameObjectTeam.Neutral)
            {
                return;
            }
    
            var allMinionsHammerQ = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, HammerQ.Range + HammerQ.Width).OrderBy(m => m.Health);
            var allMinionsHammerW = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Orbwalking.GetAttackRange(Player));
            var allMinionsHammerE = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, HammerE.Range + HammerE.Width);

            var useq = Config.Item("HammerClearQ").GetValue<bool>();
            var usew = Config.Item("HammerClearW").GetValue<bool>();
            var usee = Config.Item("HammerClearE").GetValue<bool>();

            if (HammerQ.IsReady() && useq)
            {
                HammerQ.Cast(allMinionsHammerQ.FirstOrDefault());
            }

            if (HammerW.IsReady() && allMinionsHammerW.Count >= 3 && usew)
            {
                HammerW.Cast(Player);
            }

            //var hammerclear = laneclear.AddSubMenu(new Menu("Hammer", "Hammerclear"));
            //hammerclear.AddItem(new MenuItem("HammerClearQ", "Use Q").SetValue(true));
            //hammerclear.AddItem(new MenuItem("HammerClearW", "Use W").SetValue(true));
            //hammerclear.AddItem(new MenuItem("HammerClearE", "Use E").SetValue(true));

        }
        private static void laneclear_AutoSwitch()
        {
            throw new NotImplementedException();
        }

    }
}
