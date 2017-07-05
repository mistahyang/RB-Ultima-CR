using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using System.Threading.Tasks;
using UltimaCR.Spells.Main;

namespace UltimaCR.Rotations
{
    public sealed partial class DarkKnight
    {
        private DarkKnightSpells _mySpells;

        private DarkKnightSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new DarkKnightSpells()); }
        }

        #region Job Spells

        private async Task<bool> HardSlash()
        {
            return await MySpells.HardSlash.Cast();
        }

        private async Task<bool> Shadowskin()
        {
            return await MySpells.Shadowskin.Cast();
        }

        private async Task<bool> SpinningSlash()
        {
            if (ActionManager.LastSpell.Name == MySpells.HardSlash.Name )
            {
                if (!ActionManager.HasSpell(MySpells.SyphonStrike.Name) ||

		    ActionManager.HasSpell(MySpells.PowerSlash.Name) &&
		    !ActionManager.HasSpell(MySpells.Grit.Name) &&
		    Core.Player.CurrentManaPercent >= 30 ||

		    ActionManager.HasSpell(MySpells.Grit.Name) &&
		    !ActionManager.HasSpell(MySpells.Souleater.Name) &&
		    Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 30 ||

		    ActionManager.HasSpell(MySpells.Grit.Name) &&
		    !ActionManager.HasSpell(MySpells.Souleater.Name) &&
		    !Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 15 ||

		    ActionManager.HasSpell(MySpells.Souleater.Name) &&
		    Core.Player.HasAura(743) &&
		    Core.Player.CurrentTarget.CurrentHealthPercent >= 98 &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 3 ||

		    ActionManager.HasSpell(MySpells.SyphonStrike.Name) &&
		    !ActionManager.HasSpell(MySpells.PowerSlash.Name) &&
		    Core.Player.CurrentManaPercent >= 40)
                {
                    return await MySpells.SpinningSlash.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Scourge()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.75)
            {
                if (!ActionManager.HasSpell(MySpells.PowerSlash.Name) &&
		    ActionManager.LastSpell.Name == MySpells.SpinningSlash.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Scourge.Name, true, 4000) ||

		    !ActionManager.HasSpell(MySpells.PowerSlash.Name) &&
		    ActionManager.LastSpell.Name == MySpells.SyphonStrike.Name&&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Scourge.Name, true, 4000) ||

		    ActionManager.LastSpell.Name == MySpells.Souleater.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Scourge.Name, true, 6000) ||

		    ActionManager.LastSpell.Name == MySpells.Delirium.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Scourge.Name, true, 6000) ||

		    ActionManager.LastSpell.Name == MySpells.PowerSlash.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Scourge.Name, true, 6000))
                {
                    return await MySpells.Scourge.Cast();
		}
            }
            return false;
        }

        private async Task<bool> RaidOpenerScourge()
        {
            if (!Core.Player.CurrentTarget.HasAura(MySpells.Scourge.Name, true, 6000))
            {
                if (Core.Player.CurrentTarget.CurrentHealthPercent >= 97 &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 20)
                {
                    return await MySpells.Scourge.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Unleash()
        {
            if (!Ultima.UltSettings.SingleTarget &&
		Ultima.LastSpell.Name == MySpells.Unmend.Name &&
		Core.Player.CurrentManaPercent > 30 &&
		Helpers.EnemiesNearPlayer(5) > 1 ||

		!Ultima.UltSettings.SingleTarget &&
		Ultima.LastSpell.Name == MySpells.SyphonStrike.Name &&
		Core.Player.CurrentManaPercent > 30 &&
		Helpers.EnemiesNearPlayer(5) > 1 ||

		!Ultima.UltSettings.SingleTarget &&
		Ultima.LastSpell.Name == MySpells.SpinningSlash.Name &&
		Core.Player.CurrentManaPercent > 30 &&
		Helpers.EnemiesNearPlayer(5) > 1 ||
	
		Core.Player.HasAura(814) &&
		Core.Player.TargetDistance(4, false))
            {
                return await MySpells.Unleash.Cast();
            }
            return false;
        }

        private async Task<bool> LowBlow()
        {
            if (Ultima.UltSettings.DarkKnightLowBlow &&
		ActionManager.LastSpell.Name != MySpells.Unmend.Name &&
		ActionManager.LastSpell.Name != MySpells.SyphonStrike.Name &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.DarkPassenger.Name &&
		Ultima.LastSpell.Name != MySpells.CarveAndSpit.Name &&
		Ultima.LastSpell.Name != MySpells.SaltedEarth.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.Plunge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodPrice.Name &&
		Ultima.LastSpell.Name != MySpells.BloodWeapon.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name &&
		Ultima.LastSpell.Name != MySpells.Reprisal.Name)
            {
                return await MySpells.LowBlow.Cast();
            }
            return false;
        }

        private async Task<bool> SyphonStrike()
        {
            if (ActionManager.LastSpell.Name == MySpells.HardSlash.Name)
            {
                return await MySpells.SyphonStrike.Cast();
            }
            return false;
        }

        private async Task<bool> Unmend()
        {
            if (!ActionManager.HasSpell(MySpells.AbyssalDrain.Name) &&
		!ActionManager.HasSpell(MySpells.Grit.Name) &&
		!Core.Player.InCombat &&
		Core.Player.TargetDistance(7-12) ||

		!ActionManager.HasSpell(MySpells.AbyssalDrain.Name) &&
		ActionManager.HasSpell(MySpells.Grit.Name) &&
		Core.Player.HasAura(743) &&
		!Core.Player.InCombat &&
		Core.Player.TargetDistance(7-12) ||

		!ActionManager.HasSpell(MySpells.AbyssalDrain.Name) &&
		ActionManager.HasSpell(MySpells.Grit.Name) &&
		Core.Player.HasAura(743) &&
		Ultima.LastSpell.Name != MySpells.Unmend.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent >= 98 &&
		Core.Player.TargetDistance(10) ||

		ActionManager.HasSpell(MySpells.AbyssalDrain.Name) &&
		Core.Player.HasAura(743) &&
		!Core.Player.InCombat &&
		Core.Player.TargetDistance(7-12) &&
		Helpers.EnemiesNearTarget(4) < 2 ||

		ActionManager.HasSpell(MySpells.AbyssalDrain.Name) &&
		Core.Player.HasAura(743) &&
		Ultima.LastSpell.Name != MySpells.Unmend.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent >= 99 &&
		Core.Player.TargetDistance(10) &&
		Helpers.EnemiesNearTarget(4) < 2)
            {
                return await MySpells.Unmend.Cast();
            }
            return false;
        }

        private async Task<bool> BloodWeapon()
        {
            if (Ultima.UltSettings.DarkKnightBloodWeapon &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.Unmend.Name &&
		Ultima.LastSpell.Name != MySpells.LowBlow.Name &&
		Ultima.LastSpell.Name != MySpells.DarkPassenger.Name &&
		Ultima.LastSpell.Name != MySpells.CarveAndSpit.Name &&
		Ultima.LastSpell.Name != MySpells.SaltedEarth.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.Plunge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodPrice.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name &&
		Ultima.LastSpell.Name != MySpells.Reprisal.Name &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 3 &&
		!Core.Player.HasAura(743) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false))
		
            {
                return await MySpells.BloodWeapon.Cast();
            }
            return false;
        }

        private async Task<bool> Reprisal()
        {
            if (Ultima.UltSettings.DarkKnightReprisal &&
		ActionManager.LastSpell.Name != MySpells.SyphonStrike.Name &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.LowBlow.Name &&
		Ultima.LastSpell.Name != MySpells.DarkPassenger.Name &&
		Ultima.LastSpell.Name != MySpells.CarveAndSpit.Name &&
		Ultima.LastSpell.Name != MySpells.SaltedEarth.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.Plunge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodPrice.Name &&
		Ultima.LastSpell.Name != MySpells.BloodWeapon.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name)
            {
                return await MySpells.Reprisal.Cast();
            }
            return false;
        }

        private async Task<bool> PowerSlash()
        {
            if (ActionManager.LastSpell.Name == MySpells.SpinningSlash.Name)
            {
                return await MySpells.PowerSlash.Cast();
            }
            return false;
        }

        private async Task<bool> Darkside()
        {
            if (Core.Player.HasTarget &&
		Core.Player.InCombat &&
		!Core.Player.HasAura(MySpells.Darkside.Name) ||

		!Core.Player.HasTarget &&		
                Core.Player.InCombat &&
		Helpers.EnemiesNearPlayer(20) == 0 &&
		Core.Player.CurrentManaPercent < 25 &&
		Core.Player.HasAura(MySpells.Darkside.Name))
            {
                return await MySpells.Darkside.Cast();
            }
            return false;
        }

        private async Task<bool> Grit()
        {
            if (!Core.Player.HasAura(743))
            {
                return await MySpells.Grit.Cast();
            }
            return false;
        }


        private async Task<bool> DarkDance()
        {
            return await MySpells.DarkDance.Cast();
        }

        private async Task<bool> BloodPrice()
        {
            if (ActionManager.LastSpell.Name != MySpells.SyphonStrike.Name &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.LowBlow.Name &&
		Ultima.LastSpell.Name != MySpells.DarkPassenger.Name &&
		Ultima.LastSpell.Name != MySpells.CarveAndSpit.Name &&
		Ultima.LastSpell.Name != MySpells.SaltedEarth.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.Plunge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodWeapon.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name &&
		Ultima.LastSpell.Name != MySpells.Reprisal.Name &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false))
            {
                if (Core.Player.HasAura(743) &&
		    Ultima.LastSpell.Name != MySpells.Unmend.Name &&
		    Core.Player.CurrentManaPercent < 60 ||

		    !Core.Player.HasAura(743) &&
		    !Core.Player.HasAura(MySpells.BloodWeapon.Name) &&
		    Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		    Core.Player.CurrentManaPercent < 20)
                {
                    return await MySpells.BloodPrice.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Souleater()
        {
            if (ActionManager.LastSpell.Name == MySpells.SyphonStrike.Name)
            {
                if (!Ultima.UltSettings.DarkKnightDelirium ||

		    Core.Player.CurrentTarget.HasAura(MySpells.Delirium.Name, false, 4000) ||

                    Core.Player.CurrentTarget.HasAura("Dragon Kick"))

                {
		    if (ActionManager.LastSpell.Name == MySpells.SyphonStrike.Name &&
			Ultima.UltSettings.DarkKnightDarkArts &&
                    	!Core.Player.HasAura(MySpells.DarkArts.Name) &&
                    	Core.Player.TargetDistance(4, false))
		    {
			if (Core.Player.HasAura(743) &&
                    	    Core.Player.CurrentManaPercent >= 70 ||

		    	    Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
                    	    Core.Player.CurrentManaPercent >= 50 ||

		    	    Core.Player.HasAura(MySpells.BloodWeapon.Name) &&
		    	    !Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
                    	    Core.Player.CurrentManaPercent >= 60 ||

		    	    !Core.Player.HasAura(MySpells.BloodWeapon.Name) &&
		    	    !Core.Player.HasAura(743) &&
		    	    Core.Player.CurrentManaPercent >= 70)
			{
                            if (await MySpells.DarkArts.Cast())
                            {
                                await Coroutine.Wait(3000, () => Core.Player.HasAura(MySpells.DarkArts.Name));
                            }
			}
		    }
                }
                return await MySpells.Souleater.Cast();
            }
            return false;
        }

        private async Task<bool> DarkPassenger()
        {
            if (ActionManager.LastSpell.Name != MySpells.SyphonStrike.Name &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.LowBlow.Name &&
		Ultima.LastSpell.Name != MySpells.CarveAndSpit.Name &&
		Ultima.LastSpell.Name != MySpells.SaltedEarth.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.Plunge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodPrice.Name &&
		Ultima.LastSpell.Name != MySpells.BloodWeapon.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name &&
		Ultima.LastSpell.Name != MySpells.Reprisal.Name &&
		Core.Player.InCombat &&
		!ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget))
            {
                if (Core.Player.HasAura(743) &&
		    Ultima.LastSpell.Name != MySpells.Unmend.Name &&
		    Core.Player.CurrentManaPercent >= 35 ||

		    !Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 25)
                {
                    return await MySpells.DarkPassenger.Cast();
		}
            }
            return false;
        }

        private async Task<bool> DarkMind()
        {
            return await MySpells.DarkMind.Cast();
        }

        private async Task<bool> DarkArts()
        {
            if (Ultima.LastSpell.Name != MySpells.AbyssalDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Unmend.Name &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		!Core.Player.HasAura(MySpells.DarkArts.Name) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false))
            {
                if (Core.Player.HasAura(743) &&
		    !ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
                    Core.Player.CurrentManaPercent >= 80 ||

		    Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
		    !ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
                    Core.Player.CurrentManaPercent >= 75 ||

		    Core.Player.HasAura(MySpells.BloodWeapon.Name) &&
		    !Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
		    !ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
                    Core.Player.CurrentManaPercent >= 80 ||

		    !Core.Player.HasAura(MySpells.BloodWeapon.Name) &&
		    !Core.Player.HasAura(743) &&
		    !ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    Core.Player.CurrentManaPercent >= 80 ||

		    Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 70 &&
		    !ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    ActionManager.CanCast(MySpells.DarkPassenger.Name, Core.Player.CurrentTarget) ||

		    !Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 60 &&
		    !ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    ActionManager.CanCast(MySpells.DarkPassenger.Name, Core.Player.CurrentTarget) ||

		    Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
	    	    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) &&
                    Core.Player.CurrentManaPercent >= 45 ||

		    Core.Player.HasAura(MySpells.BloodWeapon.Name) &&
		    !Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
	    	    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) &&
                    Core.Player.CurrentManaPercent >= 55 ||

		    Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
	    	    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    !ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) &&
		    ActionManager.CanCast(MySpells.DarkPassenger.Name, Core.Player.CurrentTarget) &&
                    Core.Player.CurrentManaPercent >= 55 ||

		    Core.Player.HasAura(MySpells.BloodWeapon.Name) &&
		    !Core.Player.HasAura(MySpells.BloodWeapon.Name, true, 7500) &&
		    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    !ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) &&
		    ActionManager.CanCast(MySpells.DarkPassenger.Name, Core.Player.CurrentTarget) &&
                    Core.Player.CurrentManaPercent >= 65 ||

		    Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 74 &&
		    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) ||

		    !Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 45 &&
		    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) ||

		    Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 70 &&
		    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    !ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) &&
		    ActionManager.CanCast(MySpells.DarkPassenger.Name, Core.Player.CurrentTarget) ||

		    !Core.Player.HasAura(743) &&
		    Core.Player.CurrentManaPercent >= 50 &&
		    ActionManager.HasSpell(MySpells.CarveAndSpit.Name) &&
		    !ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) &&
		    ActionManager.CanCast(MySpells.DarkPassenger.Name, Core.Player.CurrentTarget))
		{
                    return await MySpells.DarkArts.Cast();
		}
              }
            return false;
        }

        private async Task<bool> ShadowWall()
        {
            return await MySpells.ShadowWall.Cast();
        }

        private async Task<bool> Delirium()
        {
            if (Ultima.UltSettings.DarkKnightDelirium &&
		!Core.Player.HasAura(MySpells.DarkArts.Name) &&
		ActionManager.LastSpell.Name == MySpells.SyphonStrike.Name)
            {
                if (!Core.Player.HasAura(743) ||

		    Core.Player.HasAura(743) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Delirium.Name, false, 4000) &&
                    !Core.Player.CurrentTarget.HasAura("Dragon Kick") &&
		    Core.Player.CurrentHealthPercent >= 35 &&
		    Helpers.EnemiesNearTarget(5) < 2)
                {
                    return await MySpells.Delirium.Cast();
		}
            }
            return false;
        }

        private async Task<bool> LivingDead()
        {
            if (Core.Player.InCombat &&
		Core.Player.CurrentHealthPercent <= 19)
            {
                return await MySpells.LivingDead.Cast();
            }
            return false;
        }

        private async Task<bool> SaltedEarth()
        {
            if (Ultima.UltSettings.DarkKnightSaltedEarth &&
		!MovementManager.IsMoving &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		ActionManager.LastSpell.Name != MySpells.SyphonStrike.Name &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.LowBlow.Name &&
		Ultima.LastSpell.Name != MySpells.CarveAndSpit.Name &&
		Ultima.LastSpell.Name != MySpells.DarkPassenger.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.Plunge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodPrice.Name &&
		Ultima.LastSpell.Name != MySpells.BloodWeapon.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name &&
		Ultima.LastSpell.Name != MySpells.Reprisal.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 3 ||

		    Helpers.EnemiesNearTarget(5) > 1)
                {
                    return await MySpells.SaltedEarth.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Plunge()
        {
            if (ActionManager.LastSpell.Name != MySpells.SyphonStrike.Name &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.LowBlow.Name &&
		Ultima.LastSpell.Name != MySpells.CarveAndSpit.Name &&
		Ultima.LastSpell.Name != MySpells.DarkPassenger.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.SaltedEarth.Name &&
		Ultima.LastSpell.Name != MySpells.BloodPrice.Name &&
		Ultima.LastSpell.Name != MySpells.BloodWeapon.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name &&
		Ultima.LastSpell.Name != MySpells.Reprisal.Name)
            {
                if (Ultima.UltSettings.DarkKnightPlunge &&
		    Core.Player.InCombat ||

		    !Ultima.UltSettings.DarkKnightPlunge &&
		    Core.Player.TargetDistance(10) &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentTarget.CurrentHealthPercent < 95 ||

		    Ultima.UltSettings.DarkKnightPlunge &&
		    Ultima.LastSpell.Name == MySpells.Unmend.Name)
                {
                    return await MySpells.Plunge.Cast();
		}
            }
            return false;
        }


        private async Task<bool> AbyssalDrain()
        {
            if (!Core.Player.InCombat &&
		Core.Player.TargetDistance(10, false) &&
		Core.Player.HasAura(743) &&
		Core.Player.CurrentManaPercent > 20 &&
		Helpers.EnemiesNearTarget(4) > 1 ||

		Ultima.LastSpell.Name != MySpells.AbyssalDrain.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent >= 97 &&
		Core.Player.TargetDistance(10) &&
		Helpers.EnemiesNearTarget(4) > 1)
            {
                return await MySpells.AbyssalDrain.Cast();
            }
            return false;
        }

        private async Task<bool> SoleSurvivor()
        {
            if (Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealth < Core.Player.MaxHealth * .50 &&
		Core.Player.CurrentTarget.MaxHealth < Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 30 ||

		Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealth < Core.Player.MaxHealth * 2 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		Core.Player.CurrentTarget.MaxHealth >= Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 50)
		
            {
                return await MySpells.SoleSurvivor.Cast();
            }
            return false;
        }

        private async Task<bool> CarveAndSpit()
        {
            if (Ultima.LastSpell.Name != MySpells.LowBlow.Name &&
		Ultima.LastSpell.Name != MySpells.DarkArts.Name &&
		Ultima.LastSpell.Name != MySpells.DarkPassenger.Name &&
		Ultima.LastSpell.Name != MySpells.SoleSurvivor.Name &&
		Ultima.LastSpell.Name != MySpells.Plunge.Name &&
		Ultima.LastSpell.Name != MySpells.BloodPrice.Name &&
		Ultima.LastSpell.Name != MySpells.BloodWeapon.Name &&
		Ultima.LastSpell.Name != MySpells.Darkside.Name &&
		Ultima.LastSpell.Name != MySpells.Reprisal.Name &&
		Ultima.LastSpell.Name != MySpells.SaltedEarth.Name &&
		Ultima.LastSpell.Name != MySpells.Souleater.Name)
            {
                if (!Core.Player.HasAura(743) &&
		    Core.Player.HasAura(MySpells.DarkArts.Name) ||

		    Core.Player.HasAura(743) &&
		    Core.Player.HasAura(MySpells.DarkArts.Name) &&
		    ActionManager.LastSpell.Name != MySpells.SyphonStrike.Name ||

		    Core.Player.HasAura(743) &&
		    ActionManager.CanCast(MySpells.CarveAndSpit.Name, Core.Player.CurrentTarget) &&
		    Core.Player.CurrentManaPercent <= 35)
                {
                    return await MySpells.CarveAndSpit.Cast();
		}
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Gladiator

        private async Task<bool> SavageBlade()
        {
            if (Ultima.UltSettings.DarkKnightSavageBlade)
            {
                return await MySpells.CrossClass.SavageBlade.Cast();
            }
            return false;
        }

        private async Task<bool> Flash()
        {
            if (Ultima.UltSettings.DarkKnightFlash)
            {
                return await MySpells.CrossClass.Flash.Cast();
            }
            return false;
        }

        private async Task<bool> Convalescence()
        {
            if (Ultima.UltSettings.DarkKnightConvalescence)
            {
                return await MySpells.CrossClass.Convalescence.Cast();
            }
            return false;
        }

        private async Task<bool> Provoke()
        {
            if (Ultima.UltSettings.DarkKnightProvoke)
            {
                return await MySpells.CrossClass.Provoke.Cast();
            }
            return false;
        }

        private async Task<bool> Awareness()
        {
            if (Ultima.UltSettings.DarkKnightAwareness)
            {
                return await MySpells.CrossClass.Awareness.Cast();
            }
            return false;
        }

        #endregion

        #region Marauder

        private async Task<bool> Foresight()
        {
            if (Ultima.UltSettings.DarkKnightForesight)
            {
                return await MySpells.CrossClass.Foresight.Cast();
            }
            return false;
        }

        private async Task<bool> SkullSunder()
        {
            if (Ultima.UltSettings.DarkKnightSkullSunder)
            {
                return await MySpells.CrossClass.SkullSunder.Cast();
            }
            return false;
        }

        private async Task<bool> Fracture()
        {
            if (Ultima.UltSettings.DarkKnightFracture)
            {
                return await MySpells.CrossClass.Fracture.Cast();
            }
            return false;
        }

        private async Task<bool> Bloodbath()
        {
            if (Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 60)
            {
                return await MySpells.CrossClass.Bloodbath.Cast();
            }
            return false;
        }

        private async Task<bool> MercyStroke()
        {
            if (Ultima.UltSettings.DarkKnightMercyStroke)
            {
                return await MySpells.CrossClass.MercyStroke.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region PvP Spells



        #endregion
    }
}