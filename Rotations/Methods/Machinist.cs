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
    public sealed partial class Machinist
    {
        private MachinistSpells _mySpells;

        private MachinistSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new MachinistSpells()); }
        }

        #region Job Spells

        private async Task<bool> SplitShot()
        {
            return await MySpells.SplitShot.Cast();
        }

        private async Task<bool> SlugShot()
        {
            if (!ActionManager.HasSpell(MySpells.CleanShot.Name) &&
		Core.Player.HasAura(856) ||
		ActionManager.HasSpell(MySpells.CleanShot.Name) &&
		!Core.Player.HasAura(851) &&
		Core.Player.HasAura(856) &&
		!Core.Player.HasAura(857))
            {
                return await MySpells.SlugShot.Cast();
            }
            return false;
        }
        private async Task<bool> Reload()
        {
            if ((!ActionManager.HasSpell(MySpells.HotShot.Name) ||
                Core.Player.HasAura(MySpells.HotShot.Name, true, 9000)) &&
				ActionResourceManager.Machinist.Ammo == 0)
            {
                return await MySpells.Reload.Cast();
            }
            return false;
        }

        private async Task<bool> QuickReload()
        {
            if (!ActionManager.CanCast(MySpells.Reload.Name, Core.Player) &&
			ActionResourceManager.Machinist.Ammo < 3 &&
			ActionResourceManager.Machinist.Heat >= 60)
            {
                return await MySpells.QuickReload.Cast();
            }
            return false;
        }

        private async Task<bool> RaidOpenerLeadShot()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent >= 92 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10 &&
		!Core.Player.HasAura(851) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.LeadShot.Name, true))
            {
                return await MySpells.LeadShot.Cast();
            }
            return false;
        }

        private async Task<bool> LeadShot()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * .70 &&
		!Core.Player.HasAura(851) &&
		!Core.Player.HasAura(862) &&
		Ultima.LastSpell.Name != MySpells.Reload.Name &&
		Ultima.LastSpell.Name != MySpells.QuickReload.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.LeadShot.Name, true, 4000))
            {
                return await MySpells.LeadShot.Cast();
            }
            return false;
        }

        private async Task<bool> Heartbreak()
        {
            if (Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.HawksEye.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.Blank.Name &&
		Ultima.LastSpell.Name != MySpells.Reload.Name &&
		Ultima.LastSpell.Name != MySpells.Heartbreak.Name &&
		Ultima.LastSpell.Name != MySpells.LegGraze.Name &&
		Ultima.LastSpell.Name != MySpells.Reassemble.Name &&
		Ultima.LastSpell.Name != MySpells.FootGraze.Name &&
		Ultima.LastSpell.Name != MySpells.QuickReload.Name &&
		Ultima.LastSpell.Name != MySpells.RapidFire.Name &&
		Ultima.LastSpell.Name != MySpells.HeadGraze.Name &&
		Ultima.LastSpell.Name != MySpells.Wildfire.Name &&
		Ultima.LastSpell.Name != MySpells.RookAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Promotion.Name &&
		Ultima.LastSpell.Name != MySpells.SuppressiveFire.Name &&
		Ultima.LastSpell.Name != MySpells.BishopAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Dismantle.Name &&
		Ultima.LastSpell.Name != MySpells.GaussRound.Name &&
		Ultima.LastSpell.Name != MySpells.RendMind.Name &&
		Ultima.LastSpell.Name != MySpells.Hypercharge.Name &&
		Ultima.LastSpell.Name != MySpells.Ricochet.Name)
            {
                if (!Ultima.UltSettings.SingleTarget &&
		    Ultima.UltSettings.MachinistHeartbreak ||

		    Ultima.UltSettings.SingleTarget &&
		    Core.Player.CurrentTarget.CurrentHealth < Core.Player.MaxHealth * 4 &&
		    Ultima.UltSettings.MachinistHeartbreak ||

		    Ultima.UltSettings.SingleTarget &&
		    Core.Player.CurrentTarget.CurrentHealth >= Core.Player.MaxHealth * 4 &&
		    Ultima.UltSettings.MachinistHeartbreak &&
		    !ActionManager.CanCast(MySpells.Wildfire.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.Heartbreak.Cast();
		}
            }
            return false;
        }

        private async Task<bool> LegGraze()
        {
            return await MySpells.LegGraze.Cast();
        }

        private async Task<bool> Reassemble()
        {
            if ((!ActionManager.HasSpell(MySpells.CleanShot.Name) &&
				Core.Player.HasAura("Enhanced Slug Shot"))  ||
				(Core.Player.HasAura("Cleaner Shot") ||
				ActionManager.CanCast(MySpells.Ricochet.Name,Core.Player.CurrentTarget)) &&
				Core.Player.HasAura(MySpells.HotShot.Name, true, 5000) &&
				Core.Player.CurrentTarget.HasAura(MySpells.Wildfire.Name,true))
            {
                return await MySpells.Reassemble.Cast();
            }
            return false;
        }

        private async Task<bool> Blank()
        {
            if (Ultima.UltSettings.MachinistBlank &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.HawksEye.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.Reload.Name &&
		Ultima.LastSpell.Name != MySpells.Heartbreak.Name &&
		Ultima.LastSpell.Name != MySpells.LegGraze.Name &&
		Ultima.LastSpell.Name != MySpells.Reassemble.Name &&
		Ultima.LastSpell.Name != MySpells.FootGraze.Name &&
		Ultima.LastSpell.Name != MySpells.QuickReload.Name &&
		Ultima.LastSpell.Name != MySpells.RapidFire.Name &&
		Ultima.LastSpell.Name != MySpells.HeadGraze.Name &&
		Ultima.LastSpell.Name != MySpells.Wildfire.Name &&
		Ultima.LastSpell.Name != MySpells.RookAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Promotion.Name &&
		Ultima.LastSpell.Name != MySpells.SuppressiveFire.Name &&
		Ultima.LastSpell.Name != MySpells.BishopAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Dismantle.Name &&
		Ultima.LastSpell.Name != MySpells.GaussRound.Name &&
		Ultima.LastSpell.Name != MySpells.RendMind.Name &&
		Ultima.LastSpell.Name != MySpells.Hypercharge.Name &&
		Ultima.LastSpell.Name != MySpells.Ricochet.Name)
            {
                if (!Ultima.UltSettings.SmartTarget &&
		    Core.Player.CurrentTarget.MaxHealth <= Core.Player.MaxHealth * 10 &&
		    Core.Player.CurrentTarget.IsFacing(Core.Player) ||

		    !Ultima.UltSettings.SmartTarget &&
		    Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		    Core.Player.CurrentTarget.MaxHealth >= Core.Player.MaxHealth &&
		    Ultima.UltSettings.MachinistHeartbreak &&
		    !ActionManager.CanCast(MySpells.Wildfire.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.Blank.Cast();
		}
            }
            return false;
        }

        private async Task<bool> SpreadShot()
        {
            if (Helpers.EnemiesNearTarget(8) > 2 &&
		!Core.Player.HasAura(856) &&
		!Core.Player.HasAura(857))
            {
                return await MySpells.SpreadShot.Cast();
            }
            return false;
        }

        private async Task<bool> FootGraze()
        {
            return await MySpells.FootGraze.Cast();
        }

        private async Task<bool> RaidOpenerHotShot()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent >= 92 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10 &&
		!Core.Player.HasAura(851) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.HotShot.Name, true))
            {
                return await MySpells.HotShot.Cast();
            }
            return false;
        }

        private async Task<bool> HotShot()
        {
            if (Ultima.UltSettings.SmartTarget &&
		!Core.Player.HasAura(851) &&
		!Core.Player.HasAura(MySpells.HotShot.Name, true, 5000) ||

		!Core.Player.HasAura(851) &&
		!Core.Player.HasAura(862) &&
		!Core.Player.HasAura(MySpells.HotShot.Name, true, 5000))
            {
                return await MySpells.HotShot.Cast();
            }
            return false;
        }

        private async Task<bool> RapidFire()
        {
            if (Ultima.LastSpell.Name != MySpells.Heartbreak.Name &&
			Ultima.LastSpell.Name != MySpells.LegGraze.Name &&
			Ultima.LastSpell.Name != MySpells.Reassemble.Name &&
			Ultima.LastSpell.Name != MySpells.QuickReload.Name &&
			Ultima.LastSpell.Name != MySpells.RapidFire.Name &&
			Ultima.LastSpell.Name != MySpells.Wildfire.Name &&
			Ultima.LastSpell.Name != MySpells.RookAutoturret.Name &&
			Ultima.LastSpell.Name != MySpells.BishopAutoturret.Name &&
			Ultima.LastSpell.Name != MySpells.Dismantle.Name &&
			Ultima.LastSpell.Name != MySpells.Hypercharge.Name &&
			Ultima.LastSpell.Name != MySpells.Ricochet.Name &&
			ActionResourceManager.Machinist.Ammo == 3)
            {
                return await MySpells.RapidFire.Cast();
			}
            return false;
        }

        private async Task<bool> HeadGraze()
        {
            if (Ultima.UltSettings.MachinistHeadGraze &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.HawksEye.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.Reload.Name &&
		Ultima.LastSpell.Name != MySpells.Heartbreak.Name &&
		Ultima.LastSpell.Name != MySpells.LegGraze.Name &&
		Ultima.LastSpell.Name != MySpells.Reassemble.Name &&
		Ultima.LastSpell.Name != MySpells.Blank.Name &&
		Ultima.LastSpell.Name != MySpells.FootGraze.Name &&
		Ultima.LastSpell.Name != MySpells.QuickReload.Name &&
		Ultima.LastSpell.Name != MySpells.RapidFire.Name &&
		Ultima.LastSpell.Name != MySpells.Wildfire.Name &&
		Ultima.LastSpell.Name != MySpells.RookAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Promotion.Name &&
		Ultima.LastSpell.Name != MySpells.SuppressiveFire.Name &&
		Ultima.LastSpell.Name != MySpells.BishopAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Dismantle.Name &&
		Ultima.LastSpell.Name != MySpells.GaussRound.Name &&
		Ultima.LastSpell.Name != MySpells.RendMind.Name &&
		Ultima.LastSpell.Name != MySpells.Hypercharge.Name &&
		Ultima.LastSpell.Name != MySpells.Ricochet.Name)
            {
                if (!Ultima.UltSettings.SingleTarget ||

		    Ultima.UltSettings.SingleTarget &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 4 &&
		    ActionManager.HasSpell(MySpells.Wildfire.Name) ||

		    Ultima.UltSettings.SingleTarget &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 4 &&
		    ActionManager.HasSpell(MySpells.Wildfire.Name) &&
		    !ActionManager.CanCast(MySpells.Wildfire.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.HeadGraze.Cast();
		}
            }
            return false;
        }

        private async Task<bool> CleanShot()
        {
            if (!Core.Player.HasAura(862) &&
		Core.Player.HasAura(857) ||
		Core.Player.HasAura(857) &&
		Core.Player.HasAura(851) ||
		Core.Player.HasAura(857) &&
		!Core.Player.HasAura(857, false, 5250) ||
		Core.Player.HasAura(857) &&
		!Core.Player.HasAura(857, false, 5250) &&
		Core.Player.HasAura(851) ||
		Core.Player.HasAura(856) &&
		Core.Player.HasAura(857) ||
		Core.Player.HasAura(851) &&
		Core.Player.HasAura(856) &&
		Core.Player.HasAura(857))
            {
                return await MySpells.CleanShot.Cast();
            }
            return false;
        }

        private async Task<bool> Wildfire()
        {
            if (Core.Player.HasAura(MySpells.RapidFire.Name))
            {
                return await MySpells.Wildfire.Cast();
            }
            return false;
        }

        private async Task<bool> RookAutoturret()
        {
            if (Ultima.UltSettings.MachinistSummonTurret &&
                Core.Player.HasTarget &&
                Core.Player.CurrentTarget.CanAttack)
            {
                if (Ultima.UltSettings.MachinistRook ||
                    !ActionManager.HasSpell(MySpells.BishopAutoturret.Name))
                {
                    if (Core.Player.Pet == null ||
                        Core.Player.Pet.Distance2D(Core.Player.CurrentTarget) - Core.Player.CurrentTarget.CombatReach - Core.Player.Pet.CombatReach > 20)
                    {
                        return await MySpells.RookAutoturret.Cast();
                    }
                }
            }
            return false;
        }

        private async Task<bool> TurretRetrieval()
        {
            return await MySpells.TurretRetrieval.Cast();
        }

        private async Task<bool> Promotion()
        {
            return await MySpells.Promotion.Cast();
        }

        private async Task<bool> SuppressiveFire()
        {
            if (Ultima.UltSettings.MachinistHeadGraze &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.HawksEye.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.Reload.Name &&
		Ultima.LastSpell.Name != MySpells.Heartbreak.Name &&
		Ultima.LastSpell.Name != MySpells.LegGraze.Name &&
		Ultima.LastSpell.Name != MySpells.Reassemble.Name &&
		Ultima.LastSpell.Name != MySpells.Blank.Name &&
		Ultima.LastSpell.Name != MySpells.FootGraze.Name &&
		Ultima.LastSpell.Name != MySpells.QuickReload.Name &&
		Ultima.LastSpell.Name != MySpells.RapidFire.Name &&
		Ultima.LastSpell.Name != MySpells.Wildfire.Name &&
		Ultima.LastSpell.Name != MySpells.RookAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Promotion.Name &&
		Ultima.LastSpell.Name != MySpells.SuppressiveFire.Name &&
		Ultima.LastSpell.Name != MySpells.BishopAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Dismantle.Name &&
		Ultima.LastSpell.Name != MySpells.GaussRound.Name &&
		Ultima.LastSpell.Name != MySpells.RendMind.Name &&
		Ultima.LastSpell.Name != MySpells.Hypercharge.Name &&
		Ultima.LastSpell.Name != MySpells.Ricochet.Name)
            {
                if (!Ultima.UltSettings.SingleTarget ||

		    Ultima.UltSettings.SingleTarget &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 4 &&
		    ActionManager.HasSpell(MySpells.Wildfire.Name) ||

		    Ultima.UltSettings.SingleTarget &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 4 &&
		    ActionManager.HasSpell(MySpells.Wildfire.Name) &&
		    !ActionManager.CanCast(MySpells.Wildfire.Name, Core.Player.CurrentTarget))
                {
                    return await MySpells.SuppressiveFire.Cast();
		}
            }
            return false;
        }

        private async Task<bool> GrenadoShot()
        {
            if (Helpers.EnemiesNearTarget(4) > 2 &&
		!Core.Player.HasAura(856)&&
		!Core.Player.HasAura(857))
            {
                return await MySpells.GrenadoShot.Cast();
            }
            return false;
        }

        private async Task<bool> BishopAutoturret()
        {
            if (Ultima.UltSettings.MachinistSummonTurret &&
                Ultima.UltSettings.MachinistBishop &&
                Core.Player.HasTarget &&
                Core.Player.CurrentTarget.CanAttack)
            {
                if (Core.Player.Pet == null &&
                    Helpers.EnemyUnit.Count(eu => eu.Distance2D(Core.Player.CurrentTarget) - eu.CombatReach - 1 <= 5) >= 3 ||
                    Core.Player.Pet != null &&
                    Core.Player.Pet.Distance2D(Core.Player.CurrentTarget) - Core.Player.CurrentTarget.CombatReach - Core.Player.Pet.CombatReach > 5 &&
                    Helpers.EnemyUnit.Count(eu => eu.Distance2D(Core.Player.CurrentTarget) - eu.CombatReach - 1 <= 5) >= 3)
                {
                    return await MySpells.BishopAutoturret.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Dismantle()
        {
            return await MySpells.Dismantle.Cast();
        }

        private async Task<bool> GaussBarrel()
        {
            if (!ActionResourceManager.Machinist.GaussBarrel)
            {
                return await MySpells.GaussBarrel.Cast();
            }
            return false;
        }

        private async Task<bool> GaussRound()
        {
            if (Ultima.LastSpell.Name != MySpells.Reload.Name &&
		Ultima.LastSpell.Name != MySpells.Heartbreak.Name &&
		Ultima.LastSpell.Name != MySpells.LegGraze.Name &&
		Ultima.LastSpell.Name != MySpells.Reassemble.Name &&
		Ultima.LastSpell.Name != MySpells.QuickReload.Name &&
		Ultima.LastSpell.Name != MySpells.RapidFire.Name &&
		Ultima.LastSpell.Name != MySpells.Wildfire.Name &&
		Ultima.LastSpell.Name != MySpells.RookAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.BishopAutoturret.Name &&
		Ultima.LastSpell.Name != MySpells.Dismantle.Name &&
		Ultima.LastSpell.Name != MySpells.Hypercharge.Name &&
		Ultima.LastSpell.Name != MySpells.Ricochet.Name &&
		!Core.Player.HasAura(MySpells.Reassemble.Name))
        	{
                return await MySpells.GaussRound.Cast();
            }
            return false;
        }

        private async Task<bool> RendMind()
        {
            return await MySpells.RendMind.Cast();
        }

        private async Task<bool> Hypercharge()
        {
            if (Core.Player.HasAura(MySpells.RapidFire.Name))
            {
                return await MySpells.Hypercharge.Cast();
            }
            return false;
        }

        private async Task<bool> Ricochet()
        {
            if ((!Core.Player.HasAura("Cleaner Shot") &&
				Core.Player.HasAura(MySpells.Reassemble.Name)) ||
				Core.Player.CurrentTarget.HasAura(MySpells.Wildfire.Name, true) &&
				!Core.Player.HasAura(MySpells.Reassemble.Name))
    		{
                    return await MySpells.Ricochet.Cast();
			}
            return false;
        }

		private async Task<bool> SummonChocobo()
		{
			if (!ChocoboManager.Summoned)
            {
                ChocoboManager.ForceSummon();
				return true;
            }

            if (ChocoboManager.Summoned)
            {
				if (ChocoboManager.Stance != CompanionStance.Healer)
                {
                    ChocoboManager.HealerStance();
                    return true;
                }
			}

			return false;
		}

		private async Task<bool> Cooldown()
        {
            if (!ActionManager.CanCast(MySpells.QuickReload.Name,Core.Player) &&
			ActionResourceManager.Machinist.Heat >= 80 &&
			Ultima.LastSpell.Name != MySpells.QuickReload.Name)
            {
                return await MySpells.Cooldown.Cast();
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Archer

		private async Task<bool> SecondWind()
        {
            if (Core.Player.CurrentHealthPercent <= 40)
            {
                return await MySpells.CrossClass.SecondWind.Cast();
            }
            return false;
        }

        private async Task<bool> RagingStrikes()
        {
            if (Ultima.UltSettings.MachinistRagingStrikes &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		ActionManager.CanCast(MySpells.Wildfire.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.Hypercharge.Name, Core.Player))
            {
                return await MySpells.CrossClass.RagingStrikes.Cast();
            }
            return false;
        }

        private async Task<bool> HawksEye()
        {
            if (Ultima.UltSettings.MachinistHawksEye &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		!ActionManager.CanCast(MySpells.CrossClass.RagingStrikes.Name, Core.Player))
            {
                return await MySpells.CrossClass.HawksEye.Cast();
            }
            return false;
        }

        private async Task<bool> QuellingStrikes()
        {
            if (Ultima.UltSettings.MachinistQuellingStrikes)
            {
                return await MySpells.CrossClass.QuellingStrikes.Cast();
            }
            return false;
        }

        #endregion

        #region Lancer

        private async Task<bool> Feint()
        {
            if (Ultima.UltSettings.MachinistFeint &&
		MovementManager.IsMoving &&
		Core.Player.HasAura(858) &&
		!Core.Player.HasAura(856) &&
		!Core.Player.HasAura(857) &&
		!Core.Player.HasAura(851))
            {
                return await MySpells.CrossClass.Feint.Cast();
            }
            return false;
        }

        private async Task<bool> KeenFlurry()
        {
            if (Ultima.UltSettings.MachinistKeenFlurry)
            {
                return await MySpells.CrossClass.KeenFlurry.Cast();
            }
            return false;
        }

        private async Task<bool> Invigorate()
        {
            if (Ultima.UltSettings.MachinistInvigorate &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
                Core.Player.CurrentTP <= 540)
            {
                return await MySpells.CrossClass.Invigorate.Cast();
            }
            return false;
        }

        private async Task<bool> BloodForBlood()
        {
            if (Ultima.UltSettings.MachinistBloodForBlood &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		!ActionManager.CanCast(MySpells.CrossClass.HawksEye.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.HawksEye.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name)
            {
                return await MySpells.CrossClass.BloodForBlood.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region PvP Spells



        #endregion
    }
}