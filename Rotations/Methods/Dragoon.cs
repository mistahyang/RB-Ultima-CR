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
    public sealed partial class Dragoon
    {
        private DragoonSpells _mySpells;

        private DragoonSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new DragoonSpells()); }
        }

        #region Class Spells

        private async Task<bool> TrueThrust()
        {
                return await MySpells.TrueThrust.Cast();
        }

        private async Task<bool> Feint()
        {
            return await MySpells.Feint.Cast();
        }

        private async Task<bool> VorpalThrust()
        {
            if (ActionManager.LastSpell.Name == MySpells.TrueThrust.Name)
            {
                return await MySpells.VorpalThrust.Cast();
            }
            return false;
        }

        private async Task<bool> KeenFlurry()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.CurrentHealthPercent <= 85)
            {
                return await MySpells.KeenFlurry.Cast();
            }
            return false;
        }

        private async Task<bool> ImpulseDrive()
	{
            if (ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		        !ActionManager.HasSpell(MySpells.ChaosThrust.Name) &&
                !Core.Player.CurrentTarget.HasAura("Piercing Resistance Down", false, 6000) ||

		        ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		        ActionManager.HasSpell(MySpells.ChaosThrust.Name) &&
		        !Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) &&
                !Core.Player.CurrentTarget.HasAura("Piercing Resistance Down", false, 10000) ||

		        ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		        ActionManager.HasSpell(MySpells.ChaosThrust.Name) &&
		        Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) &&
                !Core.Player.CurrentTarget.HasAura("Piercing Resistance Down", false, 12000))
	    {
                return await MySpells.ImpulseDrive.Cast();
            }
            return false;
        }

        private async Task<bool> HeavyThrust()
        {
                if (!ActionManager.HasSpell(MySpells.FullThrust.Name) &&
		            !Core.Player.HasAura(MySpells.HeavyThrust.Name, true, 3000) ||

		            ActionManager.HasSpell(MySpells.FullThrust.Name) &&
		            !ActionManager.HasSpell(MySpells.FangAndClaw.Name) &&
		            !Core.Player.HasAura(MySpells.HeavyThrust.Name, true, 6000) ||

		            ActionManager.HasSpell(MySpells.FangAndClaw.Name) &&
		            Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 11000) &&
		            !Core.Player.HasAura(MySpells.HeavyThrust.Name, true, 9500) ||

		            ActionManager.HasSpell(MySpells.FangAndClaw.Name) &&
		            !Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) &&
		            !Core.Player.HasAura(MySpells.HeavyThrust.Name, true, 6000))
                {
                    return await MySpells.HeavyThrust.Cast();
                }
            return false;
        }

        private async Task<bool> PiercingTalon()
        {
            if (Core.Player.TargetDistance(10))
            {
                return await MySpells.PiercingTalon.Cast();
            }
            return false;
        }

        private async Task<bool> LifeSurge()
        {
            if (Core.Player.HasTarget &&
	        Core.Player.InCombat)
            {
                if (ActionManager.LastSpell.Name == MySpells.VorpalThrust.Name)
                {
                    return await MySpells.LifeSurge.Cast();
		        }
            }
            return false;
        }

        private async Task<bool> Invigorate()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.CurrentTP <= 440 &&
		Core.Player.TargetDistance(4, false))
            {
                return await MySpells.Invigorate.Cast();
            }
            return false;
        }

        private async Task<bool> FullThrust()
        {
            if (ActionManager.LastSpell.Name == MySpells.VorpalThrust.Name)
		
            {
                if (Ultima.LastSpell.Name == MySpells.VorpalThrust.Name &&
		    ActionManager.HasSpell(MySpells.FullThrust.Name))
                {
                    if (await MySpells.LifeSurge.Cast())
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(116));
                    }
                }
                return await MySpells.FullThrust.Cast();
            }
            return false;
        }


        private async Task<bool> Phlebotomize()
        {
            if (!Ultima.UltSettings.MultiTarget &&
		!ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true, 6000) ||

		!Ultima.UltSettings.MultiTarget &&
		ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 8000) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		!Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true, 6000) ||

		!Ultima.UltSettings.MultiTarget &&
		ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 8000) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 11000) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true, 6000) ||

		Ultima.UltSettings.MultiTarget &&
		ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 8000) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		!Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true, 7000) ||

		Ultima.UltSettings.MultiTarget &&
		ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 8000) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 11000) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true, 7000))
            {
                return await MySpells.Phlebotomize.Cast();
            }
            return false;
        }

        private async Task<bool> RaidOpenerPhlebotomize()
        {
            if (!Core.Player.CurrentTarget.HasAura(MySpells.Phlebotomize.Name, true, 6000))
            {
                if (Core.Player.CurrentTarget.CurrentHealthPercent >= 97 &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 20)
                {
                    return await MySpells.Phlebotomize.Cast();
		}
            }
            return false;
        }

        private async Task<bool> BloodForBlood()
        {
            if (Ultima.LastSpell.Name != MySpells.Jump.Name &&
		Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name &&
		Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
		Ultima.LastSpell.Name != MySpells.LegSweep.Name &&
		Ultima.LastSpell.Name != MySpells.PowerSurge.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.BloodOfTheDragon.Name &&
		Ultima.LastSpell.Name != MySpells.Geirskogul.Name &&
		!Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false))
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth ||

		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.CurrentTarget.CurrentHealthPercent > 70)
                {
                    return await MySpells.BloodForBlood.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Disembowel()
        {
            if (ActionManager.LastSpell.Name == MySpells.ImpulseDrive.Name)
            {
                return await MySpells.Disembowel.Cast();
            }
            return false;
        }

        private async Task<bool> DoomSpike()
        {
            if (!Core.Player.HasAura(116) &&
		    Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
            Helpers.EnemiesNearTarget(8) > 3 &&
		    Core.Player.HasAura(MySpells.HeavyThrust.Name, true, 3000))
            {
                return await MySpells.DoomSpike.Cast();
            }
            return false;
        }

        private async Task<bool> RingOfThorns()
        {
            if (ActionManager.LastSpell.Name == MySpells.HeavyThrust.Name)
            {
                return await MySpells.RingOfThorns.Cast();
            }
            return false;
        }

        private async Task<bool> DragonSight()
        {
            if (PartyManager.IsInParty && Core.Player.HasAura(MySpells.HeavyThrust.Name,true))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                hm.IsDPS() && hm.Distance2D(Core.Player) <= 6);
                
                if (target == null)
                {
                    target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsTank() && hm.Distance2D(Core.Player) <= 6);

                    if (target != null)
                    {
                        return await MySpells.DragonSight.Cast(target);
                    }
                }

                if (target != null)
                {
                    return await MySpells.DragonSight.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Goad()
        {
            if (PartyManager.IsInParty)
            {
                var target = Helpers.GoadManager.FirstOrDefault();

                if (target != null)
                {
                    return await MySpells.CrossClass.Goad.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> ChaosThrust()
        {
            if (ActionManager.LastSpell.Name == MySpells.Disembowel.Name)
            {
                return await MySpells.ChaosThrust.Cast();
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Marauder

        private async Task<bool> Foresight()
        {
            if (Ultima.UltSettings.DragoonForesight)
            {
                return await MySpells.CrossClass.Foresight.Cast();
            }
            return false;
        }

        private async Task<bool> SkullSunder()
        {
            if (Ultima.UltSettings.DragoonSkullSunder)
            {
                return await MySpells.CrossClass.SkullSunder.Cast();
            }
            return false;
        }

        private async Task<bool> Fracture()
        {
            if (Ultima.UltSettings.DragoonFracture &&
                !Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Fracture.Name, true, 4000))
            {
                return await MySpells.CrossClass.Fracture.Cast();
            }
            return false;
        }

        private async Task<bool> Bloodbath()
        {
            if (Core.Player.InCombat &&
            Core.Player.CurrentHealthPercent <= 75)
            {
                return await MySpells.CrossClass.Bloodbath.Cast();
            }
            return false;
        }

        private async Task<bool> MercyStroke()
        {
            if (Ultima.UltSettings.DragoonMercyStroke)
            {
                return await MySpells.CrossClass.MercyStroke.Cast();
            }
            return false;
        }

        #endregion

        #region Pugilist

        private async Task<bool> Featherfoot()
        {
            if (Ultima.UltSettings.DragoonFeatherfoot)
            {
                return await MySpells.CrossClass.Featherfoot.Cast();
            }
            return false;
        }

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

        private async Task<bool> Haymaker()
        {
            if (Ultima.UltSettings.DragoonHaymaker)
            {
                return await MySpells.CrossClass.Haymaker.Cast();
            }
            return false;
        }

        private async Task<bool> InternalRelease()
        {
            if (Ultima.UltSettings.DragoonInternalRelease &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		Ultima.LastSpell.Name != MySpells.Jump.Name &&
		Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name &&
		Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
		Ultima.LastSpell.Name != MySpells.LegSweep.Name &&
		Ultima.LastSpell.Name != MySpells.PowerSurge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.BloodOfTheDragon.Name &&
		Ultima.LastSpell.Name != MySpells.Geirskogul.Name &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false))
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth ||
		
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.CurrentTarget.CurrentHealthPercent > 70)
                {
                    return await MySpells.CrossClass.InternalRelease.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Mantra()
        {
            if (Ultima.UltSettings.DragoonMantra)
            {
                return await MySpells.CrossClass.Mantra.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> Jump()
	    {
            if (Core.Player.HasAura("Dive Ready",true))
            {
                return await MySpells.MirageDive.Cast();
            }

            if (
                Ultima.LastSpell.Name != MySpells.Jump.Name &&
		        Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name &&
		        Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
                ActionResourceManager.Bard.Timer > new TimeSpan(0, 0, 0, 3, 0) &&
		        Core.Player.HasAura(MySpells.HeavyThrust.Name, false, 4000))
            {
                if ((ActionManager.HasSpell(MySpells.Disembowel.Name) &&
                Core.Player.CurrentTarget.HasAura("Piercing Resistance Down", true, 4000)) ||
                !ActionManager.HasSpell(MySpells.Disembowel.Name))
		        {
                    if (ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget))
                    {
                        return await MySpells.Jump.Cast();
                    }

                    if (ActionManager.CanCast(MySpells.SpineshatterDive.Name, Core.Player.CurrentTarget))
                    {
                        return await MySpells.SpineshatterDive.Cast();
                    }

                    if (ActionManager.CanCast(MySpells.DragonfireDive.Name, Core.Player.CurrentTarget))
                    {                    
                        return await MySpells.DragonfireDive.Cast();
                    }
		        }
            }
            return false;
        }

        /*private async Task<bool> Goad()
        {
            var target = Helpers.GoadManager.FirstOrDefault();

            if (target != null)
            {
                return await MySpells.CrossClass.Goad.Cast(target);
            }

            return false;
        }*/

        private async Task<bool> SonicThrust()
        {
            if (ActionManager.HasSpell(MySpells.SonicThrust.Name) &&
                ActionManager.LastSpell.Name == MySpells.DoomSpike.Name)
            {
                return await MySpells.SonicThrust.Cast();
            }
            return false;
        }

        private async Task<bool> ElusiveJump()
        {
            return await MySpells.ElusiveJump.Cast();
        }

        private async Task<bool> SpineshatterDive()
        {
            if (Ultima.UltSettings.DragoonSpineshatterDive &&
		!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.Jump.Name &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name &&
		Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
		Ultima.LastSpell.Name != MySpells.LegSweep.Name &&
		Ultima.LastSpell.Name != MySpells.PowerSurge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.BloodOfTheDragon.Name &&
		Ultima.LastSpell.Name != MySpells.Geirskogul.Name &&
		Core.Player.HasAura(MySpells.HeavyThrust.Name, false, 4000) &&
		!Core.Player.HasAura(116) &&
		!Core.Player.HasAura(802) &&
		!Core.Player.HasAura(803) &&
		Core.Player.TargetDistance(4, false) ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		!Ultima.UltSettings.DragoonSpineshatterDive &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 94 &&
		Core.Player.TargetDistance(12, true) &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name && 
		Helpers.EnemiesNearTarget(5) < 2 ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		!Ultima.UltSettings.DragoonSpineshatterDive &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 94 &&
		Core.Player.TargetDistance(12, true) &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name &&
		!ActionManager.CanCast(MySpells.SpineshatterDive.Name, Core.Player.CurrentTarget))
            {
                if (Core.Player.HasAura(120) &&
		    !ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) ||

		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2 ||

		    ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		    !ActionManager.HasSpell(MySpells.BloodOfTheDragon.Name) &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) ||

		    ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		    ActionManager.HasSpell(MySpells.BloodOfTheDragon.Name) &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    !Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) &&
		    !ActionManager.CanCast(MySpells.BloodOfTheDragon.Name, Core.Player) ||

		    ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		    ActionManager.HasSpell(MySpells.BloodOfTheDragon.Name) &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 10000) &&
		    Ultima.LastSpell.Name != MySpells.FullThrust.Name ||

		    ActionManager.HasSpell(MySpells.Disembowel.Name) &&
		    ActionManager.HasSpell(MySpells.BloodOfTheDragon.Name) &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 10000) &&
		    Ultima.LastSpell.Name != MySpells.ChaosThrust.Name)
			
                {
                    return await MySpells.SpineshatterDive.Cast();
		}
            }
            return false;
        }

        private async Task<bool> PowerSurge()
        {
            if (Ultima.UltSettings.DragoonPowerSurge &&
		Core.Player.InCombat &&
		!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.Jump.Name &&
		Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name &&
		Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
		Ultima.LastSpell.Name != MySpells.LegSweep.Name &&
		Ultima.LastSpell.Name != MySpells.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.BloodOfTheDragon.Name &&
		Ultima.LastSpell.Name != MySpells.Geirskogul.Name &&
		Core.Player.HasAura(MySpells.HeavyThrust.Name, false, 4000) &&
		!Core.Player.HasAura(116) &&
		!Core.Player.HasAura(802) &&
		!Core.Player.HasAura(803))
	    {
                if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2 &&
		    ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) ||


		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    !Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 5000) &&
		    Ultima.LastSpell.Name != MySpells.FullThrust.Name ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 5000) &&
		    Ultima.LastSpell.Name != MySpells.ChaosThrust.Name ||


		    Ultima.UltSettings.DragoonSpineshatterDive &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    !ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) &&
		    ActionManager.CanCast(MySpells.SpineshatterDive.Name, Core.Player.CurrentTarget) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    !Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) &&
		    !ActionManager.CanCast(MySpells.BloodOfTheDragon.Name, Core.Player) ||

		    Ultima.UltSettings.DragoonSpineshatterDive &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    !ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) &&
		    ActionManager.CanCast(MySpells.SpineshatterDive.Name, Core.Player.CurrentTarget) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 5000) &&
		    Ultima.LastSpell.Name != MySpells.FullThrust.Name ||

		    Ultima.UltSettings.DragoonSpineshatterDive &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    !ActionManager.CanCast(MySpells.Jump.Name, Core.Player.CurrentTarget) &&
		    ActionManager.CanCast(MySpells.SpineshatterDive.Name, Core.Player.CurrentTarget) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 5000) &&
		    Ultima.LastSpell.Name != MySpells.ChaosThrust.Name)
		{
                    return await MySpells.PowerSurge.Cast();
		}
            }
            return false;
        }

        private async Task<bool> DragonfireDive()
        {
            if (Ultima.UltSettings.DragoonDragonfireDive &&
		!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.Jump.Name &&
		Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
		Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
		Ultima.LastSpell.Name != MySpells.LegSweep.Name &&
		Ultima.LastSpell.Name != MySpells.PowerSurge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.BloodOfTheDragon.Name &&
		Ultima.LastSpell.Name != MySpells.Geirskogul.Name &&
		Core.Player.HasAura(MySpells.HeavyThrust.Name, false, 4000) &&
		!Core.Player.HasAura(116) &&
		!Core.Player.HasAura(802) &&
		!Core.Player.HasAura(803) ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		!Ultima.UltSettings.DragoonDragonfireDive &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 94 &&
		Core.Player.TargetDistance(12, true) &&
		Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
		Helpers.EnemiesNearTarget(5) > 1 ||
		
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		!Ultima.UltSettings.DragoonDragonfireDive &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 94 &&
		Core.Player.TargetDistance(12, true) &&
		Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
		!ActionManager.CanCast(MySpells.SpineshatterDive.Name, Core.Player.CurrentTarget))
            {
                if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2 ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    !Core.Player.HasAura(MySpells.BloodOfTheDragon.Name) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 10000) &&
		    Ultima.LastSpell.Name != MySpells.FullThrust.Name ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Disembowel.Name, false, 4000) &&
		    Core.Player.HasAura(MySpells.BloodOfTheDragon.Name, true, 10000) &&
		    Ultima.LastSpell.Name != MySpells.ChaosThrust.Name)
                {
                    return await MySpells.DragonfireDive.Cast();
		}
            }
            return false;
        }

        private async Task<bool> BattleLitany()
        {
            if (Ultima.UltSettings.DragoonBattleLitany &&
		!MovementManager.IsMoving &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.TargetDistance(4, false) &&
		Ultima.LastSpell.Name != MySpells.Jump.Name &&
		Ultima.LastSpell.Name != MySpells.SpineshatterDive.Name &&
                Ultima.LastSpell.Name != MySpells.DragonfireDive.Name &&
		Ultima.LastSpell.Name != MySpells.LifeSurge.Name &&
		Ultima.LastSpell.Name != MySpells.LegSweep.Name &&
		Ultima.LastSpell.Name != MySpells.PowerSurge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.BloodOfTheDragon.Name &&
		Ultima.LastSpell.Name != MySpells.Geirskogul.Name &&
		Core.Player.HasAura(MySpells.HeavyThrust.Name, false, 4000) &&
		!Core.Player.HasAura(116) &&
		!Core.Player.HasAura(786))
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 ||

		    Helpers.EnemiesNearTarget(4) > 1)
                {
                    return await MySpells.BattleLitany.Cast();
		}
            }
            return false;
        }

        private async Task<bool> BloodOfTheDragon()
        {
            if (ActionManager.CanCast(MySpells.BloodOfTheDragon.Name, Core.Player) &&
                ActionResourceManager.Bard.Timer <= new TimeSpan(0, 0, 0, 3, 0))
            {
                if (ActionManager.LastSpell.Name == MySpells.Disembowel.Name ||
                    ActionManager.LastSpell.Name == MySpells.VorpalThrust.Name)
                {
                    return await MySpells.BloodOfTheDragon.Cast();
                }
            }
            return false;
        }

        private async Task<bool> FangAndClaw()
        {
            return await MySpells.FangAndClaw.Cast();
        }

        private async Task<bool> WheelingThrust()
        {
            return await MySpells.WheelingThrust.Cast();
        }

        private async Task<bool> Geirskogul()
        {
                if (ActionManager.CanCast(MySpells.Geirskogul.Name,Core.Player.CurrentTarget) &&
                    !Core.Player.HasAura(MySpells.LifeSurge.Name,true))
                {
                    return await MySpells.Geirskogul.Cast();
		        }
            return false;
        }

        private async Task<bool> Nastrond()
        {
                if (ActionManager.CanCast(MySpells.Nastrond.Name,Core.Player.CurrentTarget) &&
                    !Core.Player.HasAura(MySpells.LifeSurge.Name,true))
                {
                    return await MySpells.Nastrond.Cast();
		        }
            return false;
        }

        #endregion

        #region PvP Spells

        private async Task<bool> Enliven()
        {
            return await MySpells.PvP.Enliven.Cast();
        }

        private async Task<bool> FetterWard()
        {
            return await MySpells.PvP.FetterWard.Cast();
        }

        private async Task<bool> ImpulseRush()
        {
            return await MySpells.PvP.ImpulseRush.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        private async Task<bool> Skewer()
        {
            return await MySpells.PvP.Skewer.Cast();
        }

        private async Task<bool> WeaponThrow()
        {
            return await MySpells.PvP.WeaponThrow.Cast();
        }

        #endregion
    }
}