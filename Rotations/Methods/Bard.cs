using System;
using Buddy.Coroutines;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Managers;
using System.Linq;
using System.Threading.Tasks;
using UltimaCR.Spells;
using UltimaCR.Spells.Main;
using ff14bot.Objects;

namespace UltimaCR.Rotations
{
    public sealed partial class Bard
    {
        private BardSpells _mySpells;

        private BardSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new BardSpells()); }
        }

        #region Class Spells

        private async Task<bool> HeavyShot()
        {
            if (!ActionManager.CanCast(MySpells.EmpyrealArrow.Name, Core.Player.CurrentTarget))
            {
                return await MySpells.HeavyShot.Cast();
            }
            return false;
        }

        private async Task<bool> PitchPerfect()
        {
            if (ActionResourceManager.Bard.ActiveSong == ActionResourceManager.Bard.BardSong.WanderersMinuet)
            {
                if (ActionResourceManager.Bard.Repertoire == 3||ActionResourceManager.Bard.Timer <= new TimeSpan(0, 0, 0, 1, 0))
                {
                    return await MySpells.PitchPerfect.Cast();
                }
            }

            return false;
        }

        private async Task<bool> StraightShot()
        {
            if (Ultima.LastSpell.Name != MySpells.StraightShot.Name && 
                !Core.Player.HasAura(MySpells.StraightShot.Name, true, 3350))
                {
                    return await MySpells.StraightShot.Cast();
		        }
            return false;
        }

        private async Task<bool> Songs()
        {
            if (ActionResourceManager.Bard.ActiveSong == ActionResourceManager.Bard.BardSong.None &&
                Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		        Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
                Ultima.LastSpell.Name != MySpells.MagesBallad.Name)
            {
                if (ActionManager.CanCast(MySpells.WanderersMinuet.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.WanderersMinuet.Cast();
                }

                if (ActionManager.CanCast(MySpells.ArmysPaeon.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.ArmysPaeon.Cast();
                }
                    
                if (ActionManager.CanCast(MySpells.MagesBallad.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.MagesBallad.Cast();
                }
            }

            if ((ActionResourceManager.Bard.ActiveSong == ActionResourceManager.Bard.BardSong.WanderersMinuet ||
                ActionResourceManager.Bard.ActiveSong == ActionResourceManager.Bard.BardSong.MagesBallad) &&
                ActionResourceManager.Bard.Timer <= new TimeSpan(0, 0, 0, 1, 0) &&
                Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		        Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
                Ultima.LastSpell.Name != MySpells.MagesBallad.Name)
            {
                if (ActionManager.CanCast(MySpells.WanderersMinuet.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.WanderersMinuet.Cast();
                }
                    
                if (ActionManager.CanCast(MySpells.MagesBallad.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.MagesBallad.Cast();
                }

                if (ActionManager.CanCast(MySpells.ArmysPaeon.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.ArmysPaeon.Cast();
                }
		    }

            if ((ActionResourceManager.Bard.ActiveSong == ActionResourceManager.Bard.BardSong.ArmysPaeon) &&
                ActionResourceManager.Bard.Timer <= new TimeSpan(0, 0, 0, 9, 0) &&
                Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		        Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
                Ultima.LastSpell.Name != MySpells.MagesBallad.Name)
            {
                if (ActionManager.CanCast(MySpells.WanderersMinuet.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.WanderersMinuet.Cast();
                }

                if (ActionManager.CanCast(MySpells.MagesBallad.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.MagesBallad.Cast();
                }

                if (ActionManager.CanCast(MySpells.ArmysPaeon.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.ArmysPaeon.Cast();
                }
		    }

            return false;
        }

        private async Task<bool> StraighterShotWMspec()
        {
            if (Core.Player.HasAura("Straighter Shot"))
            {
                if (ActionManager.HasSpell(MySpells.RefulgentArrow.Name))
                {
                    if (DataManager.GetSpellData(MySpells.Barrage.ID).Cooldown > new TimeSpan(0, 0, 0, 4)) /*&&
                    Core.Player.HasAura(MySpells.StraightShot.Name, true, 4250)*/
                    {
                        return await MySpells.RefulgentArrow.Cast();
                    }
                }
                
                if (!ActionManager.HasSpell(MySpells.RefulgentArrow.Name) &&
                !ActionManager.CanCast(MySpells.EmpyrealArrow.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.StraightShot.Cast();
                }
            }
            return false;
        }

        private async Task<bool> RaidRagingStrikes()
        {
            if (ActionManager.CanCast(MySpells.Barrage.Name, Core.Player) &&
		Core.Player.CurrentTarget.CurrentHealthPercent >= 92 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10 &&
		Core.Player.HasTarget)
            {
                return await MySpells.RagingStrikes.Cast();
            }
            return false;
        }

        private async Task<bool> RagingStrikes()
        {
            if (Ultima.LastSpell.Name != MySpells.RagingStrikes.Name &&
		    Ultima.LastSpell.Name != MySpells.FoeRequiem.Name &&
		    Ultima.LastSpell.Name != MySpells.Sidewinder.Name &&
		    Ultima.LastSpell.Name != MySpells.RainOfDeath.Name &&
		    Ultima.LastSpell.Name != MySpells.Bloodletter.Name &&
		    Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		    Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
            Ultima.LastSpell.Name != MySpells.MagesBallad.Name &&
            Ultima.LastSpell.Name != MySpells.NaturesMinne.Name &&
            Ultima.LastSpell.Name != MySpells.BattleVoice.Name &&
            Ultima.LastSpell.Name != MySpells.EmpyrealArrow.Name &&
            Core.Player.HasAura(MySpells.StraightShot.Name,true,4250))
            {
                return await MySpells.RagingStrikes.Cast();
            }
            return false;
        }

        private async Task<bool> RaidVenomousBite()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent >= 92 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10 &&
		Ultima.LastSpell.Name != MySpells.VenomousBite.Name &&
		!Core.Player.HasAura(128) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.VenomousBite.Name, true) &&
		!ActionManager.CanCast(MySpells.RagingStrikes.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.HawksEye.Name, Core.Player))
            {
                return await MySpells.VenomousBite.Cast();
            }
            return false;
        }

        private async Task<bool> VenomousBite()
        {
            if (Ultima.LastSpell.Name != MySpells.VenomousBite.Name &&
            !ActionManager.HasSpell(MySpells.CausticBite.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		!Core.Player.HasAura(128) &&
		!Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.VenomousBite.Name, true, 3350) ||

		Ultima.LastSpell.Name != MySpells.VenomousBite.Name &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		!Core.Player.HasAura(128) &&
		Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.VenomousBite.Name, true, 4000))
            {
                return await MySpells.VenomousBite.Cast();
            }
            return false;
        }

        private async Task<bool> NaturesMinne()
        {
            if (PartyManager.IsInParty)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsTank() && hm.CurrentHealthPercent <= 30);

                if (target != null)
                {
                    return await MySpells.NaturesMinne.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Refresh()
        {
            if (PartyManager.IsInParty)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                hm.IsHealer() && hm.CurrentManaPercent <= 30
                && hm.Distance2D(Core.Player) <= 20);

                
                if (target != null)
                {
                    return await MySpells.Refresh.Cast();
                }
            }
            return false;
        }

        private async Task<bool> CausticBite()
        {
            if (Ultima.LastSpell.Name != MySpells.CausticBite.Name &&
		!Core.Player.HasAura(128) &&
		!Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.CausticBite.Name, true, 3350) ||

		Ultima.LastSpell.Name != MySpells.CausticBite.Name &&
		!Core.Player.HasAura(128) &&
		Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.CausticBite.Name, true, 4000))
            {
                return await MySpells.CausticBite.Cast();
            }
            return false;
        }

        private async Task<bool> RaidWindbite()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent >= 92 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10 &&
		Ultima.LastSpell.Name != MySpells.Windbite.Name &&
		!Core.Player.HasAura(128) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Windbite.Name, true) &&
		!ActionManager.CanCast(MySpells.RagingStrikes.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.HawksEye.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.CrossClass.BloodForBlood.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.CrossClass.InternalRelease.Name, Core.Player))
            {
                return await MySpells.Windbite.Cast();
            }
            return false;
        }

        private async Task<bool> Windbite()
        {
            if (Ultima.LastSpell.Name != MySpells.Windbite.Name &&
            !ActionManager.HasSpell(MySpells.Stormbite.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		!Core.Player.HasAura(128) &&
		!Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Windbite.Name, true, 3350) ||
		
		Ultima.LastSpell.Name != MySpells.Windbite.Name &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		!Core.Player.HasAura(128) &&
		Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Windbite.Name, true, 4000))
            {
                return await MySpells.Windbite.Cast();
            }
            return false;
        }

        private async Task<bool> Stormbite()
        {
            if (Ultima.LastSpell.Name != MySpells.Stormbite.Name &&
		!Core.Player.HasAura(128) &&
		!Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura("Storm Bite", true, 3350) ||
		
		Ultima.LastSpell.Name != MySpells.Stormbite.Name &&
		!Core.Player.HasAura(128) &&
		Core.Player.HasAura(865) &&
		!Core.Player.CurrentTarget.HasAura("Storm Bite", true, 4000))
            {
                return await MySpells.Stormbite.Cast();
            }
            return false;
        }

        private async Task<bool> MiserysEnd()
        {
            if (Ultima.LastSpell.Name != MySpells.RagingStrikes.Name &&
		    Ultima.LastSpell.Name != MySpells.Sidewinder.Name &&
		    Ultima.LastSpell.Name != MySpells.RainOfDeath.Name &&
		    Ultima.LastSpell.Name != MySpells.Bloodletter.Name &&
		    Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		    Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
            Ultima.LastSpell.Name != MySpells.MagesBallad.Name &&
            Ultima.LastSpell.Name != MySpells.NaturesMinne.Name &&
            Ultima.LastSpell.Name != MySpells.BattleVoice.Name &&
            Ultima.LastSpell.Name != MySpells.EmpyrealArrow.Name)
            {
                return await MySpells.MiserysEnd.Cast();
            }
            return false;
        }
 
        private async Task<bool> Shadowbind()
        {
            return await MySpells.Shadowbind.Cast();
        }

        private async Task<bool> Bloodletter()
        {
            if (Ultima.LastSpell.Name != MySpells.RagingStrikes.Name &&
		    Ultima.LastSpell.Name != MySpells.Sidewinder.Name &&
		    Ultima.LastSpell.Name != MySpells.RainOfDeath.Name &&
		    Ultima.LastSpell.Name != MySpells.Bloodletter.Name &&
		    Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		    Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
            Ultima.LastSpell.Name != MySpells.MagesBallad.Name &&
            Ultima.LastSpell.Name != MySpells.NaturesMinne.Name &&
            Ultima.LastSpell.Name != MySpells.BattleVoice.Name &&
            Ultima.LastSpell.Name != MySpells.EmpyrealArrow.Name)
            {
                return await MySpells.Bloodletter.Cast();
            }
            return false;
        }
 
        private async Task<bool> RepellingShot()
        {
            if (Core.Player.TargetDistance(5, false) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.HawksEye.Name &&
		Ultima.LastSpell.Name != MySpells.RagingStrikes.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.FoeRequiem.Name &&
		Ultima.LastSpell.Name != MySpells.MiserysEnd.Name &&
		Ultima.LastSpell.Name != MySpells.Sidewinder.Name &&
		Ultima.LastSpell.Name != MySpells.RainOfDeath.Name &&
		Ultima.LastSpell.Name != MySpells.Bloodletter.Name &&
		Ultima.LastSpell.Name != MySpells.BluntArrow.Name &&
		Ultima.LastSpell.Name != MySpells.FlamingArrow.Name)
            {
                return await MySpells.RepellingShot.Cast();
            }
            return false;
        }

        private async Task<bool> QuickNock()
        {
            if (!Core.Player.HasAura(128) &&
		Helpers.EnemiesNearTarget(8) > 3)
            {
                return await MySpells.QuickNock.Cast();
            }
            return false;
        }

        private async Task<bool> Barrage()
        {
            if (Core.Player.HasAura(MySpells.StraightShot.Name, true, 4250) &&
            (ActionManager.CanCast(MySpells.EmpyrealArrow.Name, Core.Player.CurrentTarget) ||
            ActionManager.CanCast(MySpells.RefulgentArrow.Name, Core.Player.CurrentTarget)))
            {
                return await MySpells.Barrage.Cast();
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Lancer

        private async Task<bool> Invigorate()
        {
            if (Core.Player.HasTarget &&
		    Core.Player.InCombat &&
            Core.Player.CurrentTP < 500)
            {
                return await MySpells.CrossClass.Invigorate.Cast();
            }
            return false;
        }

        #endregion

        #region Pugilist

        private async Task<bool> SecondWind()
        {
            if (Core.Player.InCombat &&
		Core.Player.CurrentHealthPercent <= 40 ||
		
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 60)
            {
                return await MySpells.CrossClass.SecondWind.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> MagesBallad()
        {
            return await MySpells.MagesBallad.Cast();
        }

        private async Task<bool> FoeRequiem()
	    {
            if (!Core.Target.HasAura(140) &&
                Core.Player.TargetDistance(20, false) &&
		        Ultima.LastSpell.Name != MySpells.Barrage.Name)
		
            {
                if ((Core.Player.CurrentTarget.CurrentHealthPercent > 20 &&
                    Core.Player.CurrentManaPercent > 60) ||

		            (Core.Player.CurrentTarget.CurrentHealthPercent <= 20 &&
                    Core.Player.CurrentManaPercent > 40))
                    {
                        return await MySpells.FoeRequiem.Cast();
		            }
            }
            return false;
        }

        private async Task<bool> ArmysPaeon()
        {
            return await MySpells.ArmysPaeon.Cast();
        }

        private async Task<bool> RainOfDeath()
        {
            if (Helpers.EnemiesNearTarget(5) > 2 &&
                Ultima.LastSpell.Name != MySpells.RagingStrikes.Name &&
		        Ultima.LastSpell.Name != MySpells.Sidewinder.Name &&
		        Ultima.LastSpell.Name != MySpells.RainOfDeath.Name &&
		        Ultima.LastSpell.Name != MySpells.Bloodletter.Name &&
		        Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		        Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
                Ultima.LastSpell.Name != MySpells.MagesBallad.Name &&
                Ultima.LastSpell.Name != MySpells.NaturesMinne.Name &&
                Ultima.LastSpell.Name != MySpells.BattleVoice.Name &&
                Ultima.LastSpell.Name != MySpells.EmpyrealArrow.Name)
            {
                return await MySpells.RainOfDeath.Cast();
            }
            return false;
        }

        private async Task<bool> BattleVoice()
        {
            if (Ultima.LastSpell.Name != MySpells.RagingStrikes.Name &&
		        Ultima.LastSpell.Name != MySpells.Sidewinder.Name &&
		        Ultima.LastSpell.Name != MySpells.RainOfDeath.Name &&
		        Ultima.LastSpell.Name != MySpells.Bloodletter.Name &&
		        Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		        Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
                Ultima.LastSpell.Name != MySpells.MagesBallad.Name &&
                Ultima.LastSpell.Name != MySpells.NaturesMinne.Name &&
                Ultima.LastSpell.Name != MySpells.BattleVoice.Name &&
                Ultima.LastSpell.Name != MySpells.EmpyrealArrow.Name)
            {
                return await MySpells.BattleVoice.Cast();
            }
	        return false;
        }

        private async Task<bool> EmpyrealArrow()
        {
            if (Core.Player.HasAura(MySpells.StraightShot.Name, true, 3250) &&
            !ActionManager.CanCast(MySpells.RefulgentArrow.Name, Core.Player.CurrentTarget))
            {
                return await MySpells.EmpyrealArrow.Cast();
            }
            return false;
        }

        private async Task<bool> IronJaws()
        {
            if (Ultima.LastSpell.Name != MySpells.IronJaws.Name && 
                Core.Player.CurrentTarget.HasAura(MySpells.VenomousBite.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Windbite.Name, true) &&
                !ActionManager.HasSpell(MySpells.CausticBite.Name) &&
                !Core.Player.HasAura(128))
                {
                    if (!Core.Player.CurrentTarget.HasAura(MySpells.VenomousBite.Name, true, 4500) ||
                        !Core.Player.CurrentTarget.HasAura(MySpells.Windbite.Name, true, 4500))
                        {
                            return await MySpells.IronJaws.Cast();
                        }
                }

            if (Ultima.LastSpell.Name != MySpells.IronJaws.Name && 
                Core.Player.CurrentTarget.HasAura(MySpells.CausticBite.Name, true) &&
                Core.Player.CurrentTarget.HasAura("Storm Bite", true) &&
                !Core.Player.HasAura(128))
                {
                    if (!Core.Player.CurrentTarget.HasAura(MySpells.CausticBite.Name, true, 4500) ||
                        !Core.Player.CurrentTarget.HasAura("Storm Bite", true, 4500))
                        {
                            return await MySpells.IronJaws.Cast();
                        }
                }

            return false;
        }

        private async Task<bool> TheWardensPaean()
        {
            return await MySpells.TheWardensPaean.Cast();
        }

        private async Task<bool> Sidewinder()
        {
            if (Ultima.LastSpell.Name != MySpells.RagingStrikes.Name &&
		        Ultima.LastSpell.Name != MySpells.FoeRequiem.Name &&
		        Ultima.LastSpell.Name != MySpells.Sidewinder.Name &&
		        Ultima.LastSpell.Name != MySpells.RainOfDeath.Name &&
		        Ultima.LastSpell.Name != MySpells.Bloodletter.Name &&
		        Ultima.LastSpell.Name != MySpells.WanderersMinuet.Name &&
		        Ultima.LastSpell.Name != MySpells.ArmysPaeon.Name &&
                Ultima.LastSpell.Name != MySpells.MagesBallad.Name &&
                Ultima.LastSpell.Name != MySpells.NaturesMinne.Name &&
                Ultima.LastSpell.Name != MySpells.BattleVoice.Name &&
                Ultima.LastSpell.Name != MySpells.EmpyrealArrow.Name &&
                ((Core.Player.CurrentTarget.HasAura(MySpells.VenomousBite.Name, true, 4500) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Windbite.Name, true, 4500)) ||
		        (Core.Player.CurrentTarget.HasAura(MySpells.CausticBite.Name, true, 4500) &&
                Core.Player.CurrentTarget.HasAura("Storm Bite", true, 4500))))
                {
                    return await MySpells.Sidewinder.Cast();
                }
            return false;
        }

        #endregion

        #region PvP Spells

        private async Task<bool> BlastShot()
        {
            return await MySpells.PvP.BlastShot.Cast();
        }

        private async Task<bool> Farshot()
        {
            return await MySpells.PvP.Farshot.Cast();
        }

        private async Task<bool> ManaDraw()
        {
            return await MySpells.PvP.ManaDraw.Cast();
        }

        private async Task<bool> Manasong()
        {
            return await MySpells.PvP.Manasong.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        #endregion
    }
}