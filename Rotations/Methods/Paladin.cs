using ff14bot;
using ff14bot.Managers;
using System.Linq;
using System.Threading.Tasks;
using UltimaCR.Spells.Main;
using System;

namespace UltimaCR.Rotations
{
    public sealed partial class Paladin
    {
        private PaladinSpells _mySpells;

        private PaladinSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new PaladinSpells()); }
        }

        #region Class Spells

        private async Task<bool> FastBlade()
        {
            return await MySpells.FastBlade.Cast();
        }

        private async Task<bool> RiotBlade1()
        {
            if (ActionManager.HasSpell(MySpells.GoringBlade.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.GoringBlade.Name, true, 5000) &&
            ActionManager.LastSpell.Name == MySpells.FastBlade.Name)
            {
                return await MySpells.RiotBlade.Cast();
            }
            return false;
        }

        private async Task<bool> Anticipation()
        {
	    	if (Core.Player.CurrentHealthPercent <= 65 && Helpers.EnemiesNearPlayer(6) >= 3)
	    	{
				return await MySpells.CrossClass.Anticipation.Cast(); 
	    	}
	    	return false;
		}

        private async Task<bool> RiotBlade2()
        {
            if (ActionManager.LastSpell.Name == MySpells.FastBlade.Name)
            {
                return await MySpells.RiotBlade.Cast();
            }
            return false;
        }

        private async Task<bool> SavageBlade()
        {
            if (ActionManager.LastSpell.Name == MySpells.FastBlade.Name)
            {
                return await MySpells.SavageBlade.Cast();
            }
            return false;
        }

        private async Task<bool> OPENERFightOrFlight()
        {
            if (!Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.CurrentTarget.CurrentHealthPercent >= 99)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 4)
                {
                    return await MySpells.FightOrFlight.Cast();
		}
            }
            return false;
        }

        private async Task<bool> FightOrFlight()
        {
            if (!ActionManager.CanCast(MySpells.Requiescat.Name,Core.Player.CurrentTarget) &&
            !Core.Player.HasAura(MySpells.Requiescat.Name) &&
            Core.Player.CurrentManaPercent < 20)
            {
                {
                    return await MySpells.FightOrFlight.Cast();
		        }
            }

            if (!ActionManager.HasSpell(MySpells.Requiescat.Name))
            {
                return await MySpells.FightOrFlight.Cast();
            }
            return false;
        }

        private async Task<bool> Flash()
        {
            if (ActionManager.LastSpell.Name == MySpells.SavageBlade.Name &&
		Helpers.EnemiesNearPlayer(5) > 1 ||

		ActionManager.LastSpell.Name == MySpells.RiotBlade.Name &&
		Helpers.EnemiesNearPlayer(5) > 1 ||

		!ActionManager.HasSpell(MySpells.CircleOfScorn.Name) &&
		Ultima.LastSpell.Name == MySpells.ShieldLob.Name &&
		Helpers.EnemiesNearPlayer(5) > 1 ||

		ActionManager.HasSpell(MySpells.CircleOfScorn.Name) &&
		Ultima.LastSpell.Name == MySpells.CircleOfScorn.Name &&
		Helpers.EnemiesNearPlayer(5) > 1)
            {
                return await MySpells.Flash.Cast();
            }
            return false;
        }

        private async Task<bool> Convalescence()
        {
            if (Core.Player.CurrentHealthPercent <= 40)
            {
                return await MySpells.CrossClass.Convalescence.Cast();
            }
            return false;
        }

        private async Task<bool> Rampart()
        {
	    	if (Core.Player.CurrentHealthPercent <= 50 && !Core.Player.HasAura(MySpells.Sentinel.Name,true))
	    	{
				return await MySpells.CrossClass.Rampart.Cast(); 
	    	}
	    	return false;
		}

        private async Task<bool> ShieldLob()
        {
            if (ActionManager.LastSpell.Name != MySpells.ShieldLob.Name &&
		        Core.Player.CurrentTarget.CurrentHealthPercent >= 99)
            {
                 return await MySpells.ShieldLob.Cast();
		    }
            return false;
        }

        private async Task<bool> ShieldBash()
        {
            return await MySpells.ShieldBash.Cast();
        }

        private async Task<bool> Provoke()
        {
            var target = Helpers.NotTargetingPlayer.FirstOrDefault();

            if (target != null)
            {
                return await MySpells.Provoke.Cast(target);
            }
            return false;
        }

        private async Task<bool> RageOfHalone()
        {
            if (ActionManager.LastSpell.Name == MySpells.SavageBlade.Name)
            {
                return await MySpells.RageOfHalone.Cast();
            }
            return false;
        }

        private async Task<bool> GoringBlade()
        {
            if (ActionManager.LastSpell.Name == MySpells.RiotBlade.Name)
            {
                if (!Core.Player.CurrentTarget.HasAura(MySpells.GoringBlade.Name, true, 4000))
                {
                    return await MySpells.GoringBlade.Cast();
                }
            }
            return false;
        }

        private async Task<bool> RoyalAuthority()
        {
            if (ActionManager.LastSpell.Name == MySpells.RiotBlade.Name)
            {
                return await MySpells.RoyalAuthority.Cast();
            }
            return false;
        }

        private async Task<bool> ShieldSwipe()
        {
            if (ActionManager.CanCast(MySpells.ShieldSwipe.Name,Core.Player.CurrentTarget))
            {
                    return await MySpells.ShieldSwipe.Cast();
            }
            return false;
        }

        private async Task<bool> TotalEclipse()
        {
            if (Helpers.EnemiesNearPlayer(5) > 3)
            {
                    return await MySpells.TotalEclipse.Cast();
            }
            return false;
        }

        private async Task<bool> Awareness()
        {
            return await MySpells.Awareness.Cast();
        }

        private async Task<bool> Sentinel()
        {
            if (Core.Player.CurrentHealthPercent <= 40 && !Core.Player.HasAura(MySpells.CrossClass.Rampart.Name,true))
	    	{
				return await MySpells.Sentinel.Cast(); 
	    	}
	    	return false;
        }

        private async Task<bool> TemperedWill()
        {
            return await MySpells.TemperedWill.Cast();
        }

        private async Task<bool> Bulwark()
        {
            return await MySpells.Bulwark.Cast();
        }

        private async Task<bool> CircleOfScorn()
        {
                if (Helpers.EnemiesNearPlayer(5) > 0)
                {
                    return await MySpells.CircleOfScorn.Cast();
		        }
            return false;
        }

        private async Task<bool> Requiescat()
        {
                if (Core.Player.CurrentManaPercent >= 80 &&
                ActionManager.LastSpell.Name == MySpells.GoringBlade.Name)
                {
                    return await MySpells.Requiescat.Cast();
		        }
            return false;
        }

        private async Task<bool> HolySpirit()
        {
            if (Core.Player.HasAura(MySpells.Requiescat.Name))
            {
                return await MySpells.HolySpirit.Cast();
            }

            if (!Core.Player.HasAura(MySpells.Requiescat.Name) &&
            Core.Player.CurrentManaPercent >= 95 &&
            DataManager.GetSpellData(MySpells.Requiescat.ID).Cooldown >= new TimeSpan(0, 0, 0, 10))
            {
                return await MySpells.HolySpirit.Cast();
            }

            return false;
        }


        #endregion

        #region Cross Class Spells

        #region Conjurer

        private async Task<bool> Cure()
        {
            if (Ultima.UltSettings.PaladinCure)
            {
                return await MySpells.CrossClass.Cure.Cast();
            }
            return false;
        }

        private async Task<bool> Protect()
        {
            if (Ultima.UltSettings.PaladinProtect &&
		!Core.Player.InCombat &&
                !Core.Player.HasAura(MySpells.CrossClass.Protect.Name))
            {
                return await MySpells.CrossClass.Protect.Cast();
            }
            return false;
        }

        private async Task<bool> Raise()
        {
            if (Ultima.UltSettings.PaladinRaise)
            {
                return await MySpells.CrossClass.Raise.Cast();
            }
            return false;
        }

        private async Task<bool> Stoneskin()
        {
            if (Ultima.UltSettings.PaladinStoneskin &&
		!Core.Player.InCombat &&
                !Core.Player.HasAura(MySpells.CrossClass.Stoneskin.Name))
            {
                return await MySpells.CrossClass.Stoneskin.Cast();
            }
            return false;
        }

        #endregion

        #region Marauder

        private async Task<bool> Foresight()
        {
            if (Ultima.UltSettings.PaladinForesight)
            {
                return await MySpells.CrossClass.Foresight.Cast();
            }
            return false;
        }

        private async Task<bool> SkullSunder()
        {
            if (Ultima.UltSettings.PaladinSkullSunder)
            {
                return await MySpells.CrossClass.SkullSunder.Cast();
            }
            return false;
        }

        private async Task<bool> Fracture()
        {
            if (Ultima.UltSettings.PaladinFracture &&
                !Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Fracture.Name, true, 4000))
            {
                return await MySpells.CrossClass.Fracture.Cast();
            }
            return false;
        }

        private async Task<bool> Bloodbath()
        {
            if (Ultima.LastSpell.Name != MySpells.ShieldSwipe.Name &&
		Ultima.LastSpell.Name != MySpells.Sheltron.Name &&
		Ultima.LastSpell.Name != MySpells.SpiritsWithin.Name &&
		Ultima.LastSpell.Name != MySpells.FightOrFlight.Name &&
		Ultima.LastSpell.Name != MySpells.CircleOfScorn.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false) &&
		    Core.Player.CurrentHealthPercent <= 60)
                {
                    return await MySpells.CrossClass.Bloodbath.Cast();
		}
            }
            return false;
        }

        private async Task<bool> MercyStroke()
        {
            if (Ultima.LastSpell.Name != MySpells.ShieldSwipe.Name &&
		Ultima.LastSpell.Name != MySpells.Sheltron.Name &&
		Ultima.LastSpell.Name != MySpells.SpiritsWithin.Name &&
		Ultima.LastSpell.Name != MySpells.FightOrFlight.Name &&
		Ultima.LastSpell.Name != MySpells.CircleOfScorn.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Bloodbath.Name)
            {
                if (Ultima.UltSettings.PaladinMercyStroke)
                {
                    return await MySpells.CrossClass.MercyStroke.Cast();
		}
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> SwordOath()
        {
            if (Ultima.UltSettings.PaladinSwordOath &&
		!Core.Player.HasAura(MySpells.SwordOath.Name) &&
		!Core.Player.HasAura(MySpells.ShieldOath.Name) ||
                !ActionManager.HasSpell(MySpells.ShieldOath.Name) &&
		!Core.Player.HasAura(MySpells.SwordOath.Name) &&
		!Core.Player.HasAura(MySpells.ShieldOath.Name))
            {
                if (Core.Player.CurrentHealthPercent >= 89 &&
		    Core.Player.HasTarget &&
		    Core.Player.CurrentTarget.CanAttack &&
		    !Core.Player.InCombat ||
		    Core.Player.InCombat &&
		    Core.Player.HasTarget)
                {
                    return await MySpells.SwordOath.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Cover()
        {
            return await MySpells.Cover.Cast();
        }

        private async Task<bool> ShieldOath()
        {
            if (Ultima.UltSettings.PaladinShieldOath &&
		!Core.Player.HasAura(MySpells.SwordOath.Name) &&
		!Core.Player.HasAura(MySpells.ShieldOath.Name))
            {
                if (Core.Player.CurrentHealthPercent >= 89 &&
		    Core.Player.HasTarget &&
		    !Core.Player.InCombat ||
		    Core.Player.InCombat &&
		    Core.Player.HasTarget)
                {
                    return await MySpells.ShieldOath.Cast();
                }
            }
            return false;
        }

        private async Task<bool> SpiritsWithin()
        {
            if (ActionManager.CanCast(MySpells.SpiritsWithin.Name, Core.Player.CurrentTarget))
            {
                return await MySpells.SpiritsWithin.Cast();
		    }
            return false;
        }

        private async Task<bool> HallowedGround()
        {
            if (Core.Player.CurrentHealthPercent <= 10)
            {
                return await MySpells.HallowedGround.Cast();
            }
            return false;
        }

        private async Task<bool> Sheltron()
        {
                if (Core.Player.CurrentHealthPercent < 80 &&
                ActionResourceManager.Paladin.Oath >= 90)
                {
                    return await MySpells.Sheltron.Cast();
		        }
            return false;
        }

        private async Task<bool> DivineVeil()
        {
            return await MySpells.DivineVeil.Cast();
        }

        private async Task<bool> Clemency()
        {
            var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.CurrentHealthPercent <= 30);

            if (target != null)
            {
                return await MySpells.Clemency.Cast(target);
            }
            
            return false;
        }

        #endregion

        #region PvP Spells

        private async Task<bool> Enliven()
        {
            return await MySpells.PvP.Enliven.Cast();
        }

        private async Task<bool> FullSwing()
        {
            return await MySpells.PvP.FullSwing.Cast();
        }

        private async Task<bool> GlorySlash()
        {
            return await MySpells.PvP.GlorySlash.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        private async Task<bool> Testudo()
        {
            return await MySpells.PvP.Testudo.Cast();
        }

        #endregion
    }
}