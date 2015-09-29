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

        }

        public static void laneclear_Cannon()
        {
            //cannonclear.AddItem(new MenuItem("RangedClearQ", "Use Q").SetValue(true));
            //cannonclear.AddItem(new MenuItem("RangedClearQHit", "If Minion Hit").SetValue(new Slider(2, 6, 0)));
            //cannonclear.AddItem(new MenuItem("RangedClearW", "Use W").SetValue(true));
            //cannonclear.AddItem(new MenuItem("RangedClearQE", "Use Q + E").SetValue(true));
            //cannonclear.AddItem(new MenuItem("RangedClearQEHit", "If Minions Hit").SetValue(new Slider(3, 6, 0)));;

            if (Player.IsWindingUp)
                return;

            var allMinionsCannonQ = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, CannonQ.Range + CannonQ.Width);
            var allMinionsCannonW = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Orbwalking.GetAttackRange(Player));
            var allMinionsCannonE = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, CannonE.Range + CannonE.Width);

            var CannonQfarmpos = CannonQ.GetCircularFarmLocation(allMinionsCannonQ, CannonQ.Width);

            var useq = Config.Item("RangedClearQ").GetValue<bool>();
            var usew = Config.Item("RangedClearW").GetValue<bool>();
            var usee = Config.Item("RangedClearE").GetValue<bool>();


            if (CannonQ.IsReady() && CannonQfarmpos.MinionsHit >=
                Config.Item("RangedClearQHit").GetValue<Slider>().Value)
            {
                CannonQ.Cast(CannonQfarmpos.Position);
            }
                
            if (CannonW.IsReady() && usew)
            {
                CannonW.Cast(Player);
            } 
        }

        public static void laneclear_Hammer()
        {
            if (Player.IsWindingUp)
                return;
            
            var allMinionsHammerQ = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, HammerQ.Range + HammerQ.Width).OrderBy(m => m.Health);
            var allMinionsHammerW = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Orbwalking.GetAttackRange(Player));
            var allMinionsHammerE = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, HammerE.Range + HammerE.Width);

            var useq = Config.Item("RangedClearQ").GetValue<bool>();
            var usew = Config.Item("RangedClearW").GetValue<bool>();
            var usee = Config.Item("RangedClearE").GetValue<bool>();

            if (HammerQ.IsReady())
            {
                HammerQ.Cast(allMinionsHammerQ.FirstOrDefault());
            }

            if (HammerW.IsReady() && allMinionsHammerW.Count >= 3)
            {
                HammerW.Cast(Player);
            }
        }
    }
}
