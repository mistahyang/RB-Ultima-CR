using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using ff14bot.Enums;
using System.Threading.Tasks;
using UltimaCR.Spells.Main;
using System;

namespace UltimaCR.Rotations
{
    public sealed partial class Warrior
    {
        private WarriorSpells _mySpells;

        private WarriorSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new WarriorSpells()); }
        }

        #region Class Spells

        private async Task<bool> HeavySwing()
        {
            return await MySpells.HeavySwing.Cast();
        }

        private async Task<bool> Foresight()
        {
            return await MySpells.Foresight.Cast();
        }

        private async Task<bool> SkullSunder()
        {
            if ((ActionManager.LastSpell.Name == MySpells.HeavySwing.Name &&
			Core.Player.HasAura(MySpells.StormsEye.Name,true,4500)) ||
			(!ActionManager.HasSpell(MySpells.StormsEye.Name) &&
			Core.Player.CurrentTarget.HasAura(MySpells.Maim.Name,true,3000)))
            {
                return await MySpells.SkullSunder.Cast();
            }
            return false;
        }

        private async Task<bool> Fracture()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		!Core.Player.HasAura(86) ||
		
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		!Core.Player.HasAura(86, false, 3000))
            {
                if (ActionManager.LastSpell.Name == MySpells.StormsEye.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Fracture.Name, true, 6500) &&
		    ActionManager.HasSpell(MySpells.StormsEye.Name)  ||

		    ActionManager.LastSpell.Name == MySpells.ButchersBlock.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Fracture.Name, true, 6500) &&
		    ActionManager.HasSpell(MySpells.ButchersBlock.Name) &&
		    Core.Player.HasAura(MySpells.Maim.Name, false, 6000) ||

		    ActionManager.LastSpell.Name == MySpells.StormsPath.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Fracture.Name, true, 6500) &&
		    ActionManager.HasSpell(MySpells.StormsPath.Name) &&
		    Core.Player.HasAura(MySpells.Maim.Name, false, 6000) ||

		    !ActionManager.HasSpell(MySpells.ButchersBlock.Name) &&
		    ActionManager.LastSpell.Name == MySpells.SkullSunder.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Fracture.Name, true, 4500) ||

		    !ActionManager.HasSpell(MySpells.ButchersBlock.Name) &&
		    ActionManager.LastSpell.Name == MySpells.Maim.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Fracture.Name, true, 4500))
                {
                    return await MySpells.Fracture.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Bloodbath()
       	{
            if (Ultima.UltSettings.MultiTarget &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 60)
            {
                return await MySpells.Bloodbath.Cast();
            }
            return false;
        }

        private async Task<bool> BrutalSwing()
        {
            if (Ultima.UltSettings.WarriorBrutalSwing &&
		Ultima.LastSpell.Name != MySpells.Tomahawk.Name)
            {
                if (!ActionManager.HasSpell(MySpells.StormsEye.Name) ||

		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth / 4 ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 4 &&
		    ActionManager.HasSpell(MySpells.StormsEye.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.StormsEye.Name) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 4 &&
		    Core.Player.CurrentTarget.HasAura("Dancing Edge"))
                {
                    return await MySpells.BrutalSwing.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Overpower()
        {
            if (!ActionManager.HasSpell(MySpells.ButchersBlock.Name) &&
		ActionManager.LastSpell.Name == MySpells.SkullSunder.Name &&
		Core.Player.CurrentTP > 600 &&
		Helpers.EnemiesNearPlayer(6) >= 2 ||

		!ActionManager.HasSpell(MySpells.StormsPath.Name) &&
		ActionManager.LastSpell.Name == MySpells.Maim.Name &&
		Core.Player.CurrentTP > 600 &&
		Helpers.EnemiesNearPlayer(6) >= 2 ||

		ActionManager.HasSpell(MySpells.StormsPath.Name) &&
		ActionManager.LastSpell.Name == MySpells.StormsPath.Name &&
		Core.Player.CurrentTP > 600 &&
		Helpers.EnemiesNearPlayer(6) >= 2 ||

		ActionManager.HasSpell(MySpells.StormsEye.Name) &&
		ActionManager.LastSpell.Name == MySpells.StormsEye.Name &&
		Core.Player.CurrentTP > 600 &&
		Helpers.EnemiesNearPlayer(6) >= 2 ||

		ActionManager.HasSpell(MySpells.ButchersBlock.Name) &&
		ActionManager.LastSpell.Name == MySpells.ButchersBlock.Name &&
		Core.Player.CurrentTP > 600 &&
		Helpers.EnemiesNearPlayer(6) >= 2)
            {
                return await MySpells.Overpower.Cast();
            }
            return false;
        }

        private async Task<bool> Tomahawk()
        {
            if (ActionManager.LastSpell.Name != MySpells.Tomahawk.Name &&
		        Core.Player.CurrentTarget.CurrentHealthPercent >= 99)
            {
                 return await MySpells.Tomahawk.Cast();
		    }
            return false;
        }

        private async Task<bool> Maim()
        {

        	if (ActionManager.LastSpell.Name == MySpells.HeavySwing.Name)
	        {

				return await MySpells.Maim.Cast();
			}
			return false;
        }

        private async Task<bool> Berserk()
        {
            if ((!ActionManager.HasSpell(MySpells.StormsEye.Name) ||
			Core.Player.HasAura(MySpells.StormsEye.Name, true, 5000)) &&
			ActionResourceManager.Warrior.BeastGauge == 100 &&
			ActionManager.LastSpell.Name == MySpells.HeavySwing.Name)
                {
                    return await MySpells.Berserk.Cast();
                }
            return false;
        }

		private async Task<bool> InnerRelease()
        {
            if (Core.Player.HasAura(MySpells.Berserk.Name, true))
                {
                    return await MySpells.InnerRelease.Cast();
                }
            return false;
        }

        private async Task<bool> MercyStroke()
        {
            if (Ultima.UltSettings.WarriorMercyStroke)
            {
                return await MySpells.MercyStroke.Cast();
            }
            return false;
        }

        private async Task<bool> ButchersBlock()
        {
            if (ActionManager.LastSpell.Name == MySpells.SkullSunder.Name)
            {
                return await MySpells.ButchersBlock.Cast();
            }
            return false;
        }

        private async Task<bool> ThrillOfBattle()
        {
            if (Core.Player.CurrentHealthPercent <= 20)
	    	{
				return await MySpells.ThrillOfBattle.Cast(); 
	    	}
			return false;
        }

        private async Task<bool> StormsPath()
        {
            if (ActionManager.LastSpell.Name == MySpells.Maim.Name &&
				(!ActionManager.HasSpell(MySpells.StormsEye.Name) ||
					Core.Player.HasAura(MySpells.StormsEye.Name, true, 3000)))
                {
                    return await MySpells.StormsPath.Cast();
				}
            return false;
        }

        private async Task<bool> Holmgang()
        {
            return await MySpells.Holmgang.Cast();
        }

        private async Task<bool> Vengeance()
        {
	    	if (Core.Player.CurrentHealthPercent <= 50 && 
            !Core.Player.HasAura(MySpells.CrossClass.Rampart.Name,true) &&
            ActionManager.LastSpell.Name != MySpells.CrossClass.Rampart.Name)
	    	{
				return await MySpells.Vengeance.Cast(); 
	    	}
	    	return false;
		}

		private async Task<bool> Rampart()
        {
	    	if (Core.Player.CurrentHealthPercent <= 50 && 
            !Core.Player.HasAura(MySpells.Vengeance.Name,true) &&
            ActionManager.LastSpell.Name != MySpells.Vengeance.Name)
	    	{
				return await MySpells.CrossClass.Rampart.Cast(); 
	    	}
	    	return false;
		}

		private async Task<bool> Anticipation()
        {
	    	if (Core.Player.CurrentHealthPercent <= 65 && Helpers.EnemiesNearPlayer(6) >= 3)
	    	{
				return await MySpells.Anticipation.Cast(); 
	    	}
	    	return false;
		}

        private async Task<bool> StormsEye()
        {
            if (ActionManager.LastSpell.Name == MySpells.Maim.Name &&
			!Core.Player.HasAura(MySpells.StormsEye.Name, true, 8000))
        	{
            	return await MySpells.StormsEye.Cast();
        	}
            return false;
        }

		private async Task<bool> Upheaval1()
        {
            if (Core.Player.HasAura(MySpells.Defiance.Name,true) && ActionResourceManager.Warrior.BeastGauge >= 20)
        	{
            	return await MySpells.Upheaval.Cast();
        	}
            return false;
        }

		private async Task<bool> Upheaval2()
        {
			if (Core.Player.HasAura(MySpells.Berserk.Name,true))
			{
				if (Core.Player.HasAura(MySpells.InnerRelease.Name,true) && ActionResourceManager.Warrior.BeastGauge <= 55)
				{
            		return await MySpells.Upheaval.Cast();
        		}

				if (ActionResourceManager.Warrior.BeastGauge < 50)
				{
					return await MySpells.Upheaval.Cast();
				}
			}
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Gladiator

        private async Task<bool> SavageBlade()
        {
            if (Ultima.UltSettings.WarriorSavageBlade)
            {
                return await MySpells.CrossClass.SavageBlade.Cast();
            }
            return false;
        }

        private async Task<bool> Flash()
        {
            if (Ultima.LastSpell.Name == MySpells.Tomahawk.Name &&
		Helpers.EnemiesNearPlayer(5) >= 2 ||

		!ActionManager.HasSpell(MySpells.ButchersBlock.Name) &&
		ActionManager.LastSpell.Name == MySpells.SkullSunder.Name &&
		Core.Player.CurrentTP <= 600 &&
		Helpers.EnemiesNearPlayer(5) >= 2 ||

		!ActionManager.HasSpell(MySpells.StormsPath.Name) &&
		ActionManager.LastSpell.Name == MySpells.Maim.Name &&
		Core.Player.CurrentTP <= 600 &&
		Helpers.EnemiesNearPlayer(5) >= 2 ||

		ActionManager.HasSpell(MySpells.ButchersBlock.Name) &&
		ActionManager.LastSpell.Name == MySpells.ButchersBlock.Name &&
		Core.Player.CurrentTP <= 600 &&
		Helpers.EnemiesNearPlayer(5) >= 2 ||

		ActionManager.HasSpell(MySpells.StormsPath.Name) &&
		ActionManager.LastSpell.Name == MySpells.StormsPath.Name &&
		Core.Player.CurrentTP <= 600 &&
		Helpers.EnemiesNearPlayer(5) >= 2 ||

		ActionManager.HasSpell(MySpells.StormsEye.Name) &&
		ActionManager.LastSpell.Name == MySpells.StormsEye.Name &&
		Core.Player.CurrentTP <= 600 &&
		Helpers.EnemiesNearPlayer(5) >= 2)
            {
                return await MySpells.CrossClass.Flash.Cast();
            }
            return false;
        }

        private async Task<bool> Convalescence()
        {
            if (Core.Player.CurrentHealthPercent <= 35)
            {
                return await MySpells.CrossClass.Convalescence.Cast();
            }
            return false;
        }

        private async Task<bool> Provoke()
        {
            if (Ultima.UltSettings.WarriorProvoke)
            {
                return await MySpells.CrossClass.Provoke.Cast();
            }
            return false;
        }

        private async Task<bool> Awareness()
        {
            if (Ultima.UltSettings.WarriorAwareness)
            {
                return await MySpells.CrossClass.Awareness.Cast();
            }
            return false;
        }

        #endregion

        #region Pugilist

        private async Task<bool> Featherfoot()
        {
            if (Ultima.UltSettings.WarriorFeatherfoot)
            {
                return await MySpells.CrossClass.Featherfoot.Cast();
            }
            return false;
        }

        private async Task<bool> SecondWind()
        {
            if (Ultima.UltSettings.WarriorSecondWind &&
		ActionManager.HasSpell(MySpells.Equilibrium.Name) &&
		Core.Player.HasAura(91) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 25 ||

		Ultima.UltSettings.WarriorSecondWind &&
		ActionManager.HasSpell(MySpells.Equilibrium.Name) &&
		!Core.Player.HasAura(91) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 50 ||

		Ultima.UltSettings.WarriorSecondWind &&
		!ActionManager.HasSpell(MySpells.Equilibrium.Name) &&
		Core.Player.HasAura(91) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 25 ||

		Ultima.UltSettings.WarriorSecondWind &&
		!ActionManager.HasSpell(MySpells.Equilibrium.Name) &&
		!Core.Player.HasAura(91) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 50)
            {
                return await MySpells.CrossClass.SecondWind.Cast();
            }
            return false;
        }

        private async Task<bool> Haymaker()
        {
            if (Ultima.UltSettings.WarriorHaymaker)
            {
                return await MySpells.CrossClass.Haymaker.Cast();
            }
            return false;
        }

        private async Task<bool> InternalRelease()
        {
            if (Ultima.UltSettings.WarriorInternalRelease &&
		!Core.Player.HasAura(MySpells.Defiance.Name) &&
		!Core.Player.HasAura(MySpells.Deliverance.Name) &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.TargetDistance(1-5) ||

		Ultima.UltSettings.WarriorInternalRelease &&
		!Core.Player.HasAura("Defiance") &&
		Core.Player.HasAura(732) &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.TargetDistance(1-5) ||

		Ultima.UltSettings.WarriorInternalRelease &&
		!Core.Player.HasAura("Defiance") &&
		!ActionManager.CanCast(MySpells.Berserk.Name, Core.Player) &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.TargetDistance(1-5) ||

		Ultima.UltSettings.WarriorInternalRelease &&
		Core.Player.HasAura("Defiance") &&
		Core.Player.HasAura(95) &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.TargetDistance(1-5)) 
            {
                return await MySpells.CrossClass.InternalRelease.Cast();
            }
            return false;
        }

        private async Task<bool> Mantra()
        {
            if (Ultima.UltSettings.WarriorMantra)
            {
                return await MySpells.CrossClass.Mantra.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> Defiance()
        {
			if (!Core.Player.HasAura("Defiance") && !Core.Player.HasAura("Deliverance"))
            {
            return await MySpells.Defiance.Cast();
			}
			return false;
        }

        private async Task<bool> InnerBeast()
        {
	    if (Core.Player.HasAura(MySpells.Berserk.Name, true) && ActionResourceManager.Warrior.BeastGauge >= 50)
            {
                return await MySpells.InnerBeast.Cast(); 
            }

			if (!Core.Player.HasAura(MySpells.Berserk.Name,true) && DataManager.GetSpellData(MySpells.Berserk.ID).Cooldown >= new TimeSpan(0, 0, 0, 25))
			{
				return await MySpells.InnerBeast.Cast(); 
			}
            return false;
			
		}

        private async Task<bool> Unchained()
        {
	    if (Core.Player.HasAura(91) &&
		Core.Player.HasAura(97) &&
		ActionManager.CanCast(MySpells.Unchained.Name, Core.Player))
            {
                return await MySpells.Unchained.Cast();
            }
	    return false;
        }

        private async Task<bool> SteelCyclone()
        {
			if (Helpers.EnemiesNearPlayer(5) > 2 && Core.Player.HasAura(MySpells.Defiance.Name,true))
					{
						return await MySpells.SteelCyclone.Cast(); 
					}
					return false;
        }

        private async Task<bool> Infuriate()
        {
	    if (Core.Player.HasAura(MySpells.Berserk.Name,true) &&
			ActionResourceManager.Warrior.BeastGauge <= 50)
            {
				return await MySpells.Infuriate.Cast();
	    	}
	    	return false;
        }

        private async Task<bool> Deliverance()
        {
	    if (!Core.Player.HasAura("Defiance") &&
		!Core.Player.HasAura("Deliverance"))
            {
                return await MySpells.Deliverance.Cast();
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

        private async Task<bool> FellCleave()
		{
            if (Core.Player.HasAura(MySpells.Berserk.Name, true))
            {
				if (Core.Player.HasAura(MySpells.InnerRelease.Name,true) && ActionResourceManager.Warrior.BeastGauge >= 25)
				{
            		return await MySpells.FellCleave.Cast();
        		}

				if (ActionResourceManager.Warrior.BeastGauge >= 50)
				{
					return await MySpells.FellCleave.Cast();
				}
                
            }

			if (!Core.Player.HasAura(MySpells.Berserk.Name,true) && DataManager.GetSpellData(MySpells.Berserk.ID).Cooldown >= new TimeSpan(0, 0, 0, 25))
			{
				return await MySpells.FellCleave.Cast();
			}
            return false;
        }



        private async Task<bool> RawIntuition()
        {
	    if (Core.Player.HasAura(729) &&
		Core.Player.HasAura(86) &&
		Core.Player.HasAura(731) ||
		Core.Player.HasAura(729) &&
		Core.Player.HasAura(86) &&
		Core.Player.HasAura(732) ||
		Core.Player.HasAura(91) &&
		Core.Player.HasAura(86) &&
		Core.Player.HasAura(95) ||
		Core.Player.HasAura(91) &&
		Core.Player.HasAura(86) &&
		Core.Player.HasAura(96))
	    {
		return await MySpells.RawIntuition.Cast(); 
	    }
	    return false;
	}

        private async Task<bool> Equilibrium()
        {
			if ((Core.Player.HasAura(MySpells.Defiance.Name,true) &&
			Core.Player.CurrentHealthPercent <= 50) ||
			(Core.Player.HasAura(MySpells.Deliverance.Name,true) &&
			Core.Player.CurrentTP <= 670))
            {
                return await MySpells.Equilibrium.Cast();
            }
            return false;
        }

        private async Task<bool> Decimate()
        {
            if (Core.Player.HasAura(MySpells.StormsEye.Name, true, 5000) &&
            Helpers.EnemiesNearTarget(5) > 3 &&
			Core.Player.HasAura(MySpells.Deliverance.Name,true))
			{
				return await MySpells.Decimate.Cast(); 
			}
			return false;
        }

        #endregion

        #region PvP Spells

        private async Task<bool> FullSwing()
        {
            return await MySpells.PvP.FullSwing.Cast();
        }

        private async Task<bool> MythrilTempest()
        {
            return await MySpells.PvP.MythrilTempest.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        private async Task<bool> ThrillOfWar()
        {
            return await MySpells.PvP.ThrillOfWar.Cast();
        }

        #endregion
    }
}