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
    class Mechanics : ConfigMenu
    {
        public static void EventLoader() //Call OrbwalkerModes
        {
            Obj_AI_Hero.OnProcessSpellCast += Combo_Timer;
        }
        public static int hammerqtime, hammerwtime, hammeretime, cannonqtime, cannonwtime, cannonetime, hammerrtime, cannonrtime;




        /// <summary>
        ///  OnProcessSpellcast, in this case being used as a cooldown/ready checker for multiple spells.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void Combo_Timer(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            //JayceToTheSkies           //Q
            //JayceBasicAttack          //A
            //JayceThunderingBlow       //E
            //JayceStaticField          //W
            //JayceStanceHtG            //HR
            //jayceshockblast           //QR
            //jaycehypercharge          //WR
            //jayceaccelerationgate     //ER
            //jaycestancegth            //RR
            //jaycepassiverangedattack //PAA
            //jaycepassivemeleeattack  //PAA

            if (!sender.IsMe) return;
            var spell = args.SData;

            switch (spell.Name)
            {
                case "JayceToTheSkies": //Hammer [Q]
                    hammerqtime = Utils.GameTimeTickCount;
                    break;
                case "JayceStaticField": //Hammer [W]
                    hammerwtime = Utils.GameTimeTickCount;
                    break;
                case "JayceThunderingBlow": //Hammer [E]
                    hammeretime = Utils.GameTimeTickCount;
                    break;
                case "jayceshockblast": //Cannon [Q]
                    cannonqtime = Utils.GameTimeTickCount;
                    break;
                case "jaycehypercharge": //Cannon [W]
                    cannonwtime = Utils.GameTimeTickCount;
                    break;
                case "jayceaccelerationgate": //Cannon [E]
                    cannonetime = Utils.GameTimeTickCount;
                    break;
                case "JayceStanceHTG": //Switch to Cannon
                    hammerrtime = Utils.GameTimeTickCount;
                    break;
                case "jaycestancegth": //Switch to Hammer
                    cannonrtime = Utils.GameTimeTickCount;
                    return;

                    //if (sender.IsMe)
                    //{
                    //    Printchat(args.SData.Name);
                    //}
            }
        }

        #region -------------------- Ready Checks -------------------------------
        public static float Ready_Hammer_Q()
        {
            return 0;
        }
        public static float Ready_Hammer_W()
        {
            return 0;
        }
        public static float Ready_Hammer_E()
        {
            return 0;
        }
        public static float Ready_Hammer_R()
        {
            return 0;
        }
        public static float Ready_Cannon_Q()
        {
            return 0;
        }
        public static float Ready_Cannon_W()
        {
            return 0;
        }
        public static float Ready_Cannon_E()
        {
            return 0;
        }
        public static float Ready_Cannon_R()
        {
            return 0;
        }
        #endregion ------------------ End Ready Checks ----------------------------

        #region   ------------------- Get Cooldown times --------------------------
        public static int Hammer_Q_CD() //Actual Hammer Q cooldown - From The Skies!
        {
            return
                new int[] { 0, 16, 14, 12, 10, 8 }[CannonQ.Level];
        }
        public static int Hammer_E_CD() //Actual Hammer E cooldown - Thundering Blow
        {
            return
                new int[] { 14, 12, 12, 11, 10, 8 }[HammerE.Level];
        }
        public static int Cannon_E_CD() //Actual Cannon E Cooldown - Acceleration Gate
        {
            return
                new int[] { 0, 14, 12, 12, 11, 10 }[CannonE.Level];
        }

        #endregion ------------------ Get Cooldown Times --------------------------

    }
}
    

