using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using System.Threading.Tasks;
using UltimaCR.Spells.Main;

namespace UltimaCR.Rotations
{
    public sealed partial class Monk
    {
        private MonkSpells _mySpells;

        private MonkSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new MonkSpells()); }
        }

        #region Class Spells

        private async Task<bool> Bootshine()
        {
            if (Ultima.UltSettings.MultiTarget ||

		!Core.Player.HasAura(107) &&
		!Core.Player.HasAura(108) &&
		!Core.Player.HasAura(109) ||

		!ActionManager.HasSpell(MySpells.DragonKick.Name) ||

		ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		!Core.Player.HasAura(110) &&
		Core.Player.HasAura(107) &&
		Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.SnapPunch.Name &&
		Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.Demolish.Name &&
		Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.TouchOfDeath.Name &&
		Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.Rockbreaker.Name &&
		Core.Player.CurrentTarget.HasAura(98, false, 6000))
            {
                return await MySpells.Bootshine.Cast();
            }
            return false;
        }


        private async Task<bool> TrueStrike()
        {
            if (!ActionManager.HasSpell(MySpells.TwinSnakes.Name) ||

		!Core.Player.HasAura(110) &&
		ActionManager.HasSpell(MySpells.TwinSnakes.Name) &&
		Core.Player.HasAura(MySpells.TwinSnakes.Name, true, 5000))
            {
                return await MySpells.TrueStrike.Cast();
            }
            return false;
        }

        private async Task<bool> Featherfoot()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 3 &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.CurrentHealthPercent <= 85)
            {
                return await MySpells.Featherfoot.Cast();
            }
            return false;
        }

        private async Task<bool> SnapPunch()
        {
            if (Ultima.LastSpell.Name != MySpells.Demolish.Name &&
		Ultima.LastSpell.Name != MySpells.Rockbreaker.Name &&
		!Core.Player.HasAura(110) ||

                Core.Player.HasAura(110) &&
		!Core.Player.HasAura(113))
            {
                return await MySpells.SnapPunch.Cast();
            }
            return false;
        }

        private async Task<bool> SecondWind()
        {
            if (Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 40 ||

		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 60)
            {
                return await MySpells.SecondWind.Cast();
            }
            return false;
        }


        private async Task<bool> Haymaker()
        {
            if (Ultima.LastSpell.Name == MySpells.SnapPunch.Name ||
		Ultima.LastSpell.Name == MySpells.Demolish.Name ||
		Ultima.LastSpell.Name == MySpells.Rockbreaker.Name)
            {
                return await MySpells.Haymaker.Cast();
            }
            return false;
        }

        private async Task<bool> InternalRelease()
        {
            if (Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.CurrentTarget.CurrentHealthPercent > 70 &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false))
                {
                    return await MySpells.InternalRelease.Cast();
		}
            }
            return false;
        }

        private async Task<bool> TouchOfDeath()
        {
            if (!Ultima.UltSettings.MultiTarget &&
		!ActionManager.HasSpell(MySpells.TwinSnakes.Name) &&
		!ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.8 &&
		!Core.Player.CurrentTarget.HasAura(MySpells.TouchOfDeath.Name, true, 4000) ||

		!Ultima.UltSettings.MultiTarget &&
		ActionManager.HasSpell(MySpells.TwinSnakes.Name) &&
		!ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.8 &&
		Core.Player.HasAura(MySpells.TwinSnakes.Name, true, 3000) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.TouchOfDeath.Name, true, 5000) ||

		!Ultima.UltSettings.MultiTarget &&
		ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.8 &&
		Core.Player.HasAura(MySpells.TwinSnakes.Name, true, 3000) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.TouchOfDeath.Name, true, 4000) &&
		!Core.Player.HasAura(113) ||

		!Ultima.UltSettings.MultiTarget &&
		ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.8 &&
		Core.Player.HasAura(MySpells.TwinSnakes.Name, true, 3000) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.TouchOfDeath.Name, true, 4000) &&
		Core.Player.HasAura(113, true, 5000) ||

		!Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 1.8 &&
		Core.Player.CurrentTarget.CurrentHealth >= Core.Player.MaxHealth * .8 &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.TouchOfDeath.Name, true, 4000) ||

		Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.8 &&
		Core.Player.HasAura(MySpells.TwinSnakes.Name, true, 3000) &&
		Core.Player.CurrentTarget.HasAura(MySpells.TouchOfDeath.Name, true) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.TouchOfDeath.Name, true, 4000)) 
            {
                if (NoGreasedLightning ||
                    HasGreasedLightning)
                {
                    return await MySpells.TouchOfDeath.Cast();
                }
                return false;
            }
            return false;
        }

        private async Task<bool> TwinSnakes()
        {
            if (!Core.Player.HasAura(110) &&
		!Core.Player.HasAura(MySpells.TwinSnakes.Name, true, 5000) ||

		Core.Player.HasAura(110) &&
		!Core.Player.HasAura(MySpells.TwinSnakes.Name, true, 5000) &&
		Core.Player.HasAura(113))
            {
                return await MySpells.TwinSnakes.Cast();
            }
            return false;
        }

        private async Task<bool> FistsOfEarth()
        {
            if (Core.Player.InCombat ||
		!MovementManager.IsMoving &&
		!Core.Player.InCombat &&
		!Core.Player.HasTarget &&
		Core.Player.CurrentHealthPercent > 89)
            {
                if (!ActionManager.HasSpell(MySpells.FistsOfFire.Name) &&
		    !Core.Player.HasAura(MySpells.FistsOfFire.Name) &&
		    !Core.Player.HasAura(MySpells.FistsOfEarth.Name) &&
		    !Core.Player.HasAura(MySpells.FistsOfWind.Name))
                {
                    return await MySpells.FistsOfEarth.Cast();
		}
            }
            return false;
        }

        private async Task<bool> ArmOfTheDestroyer()
        {
            if (Core.Player.HasTarget &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentTP >= 250 &&
		Helpers.EnemiesNearTarget(5) > 3)
            {
                return await MySpells.ArmOfTheDestroyer.Cast();
            }
            return false;
        }

        private async Task<bool> Demolish()
        {
            if (!Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.6 &&
		!Core.Player.HasAura(110) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Demolish.Name, true, 5000) ||

		Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.6 &&
		!Core.Player.HasAura(110) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Demolish.Name, true) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Demolish.Name, true, 5000) ||

		!Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 1.6 &&
		Core.Player.HasAura(110) &&
		!Core.Player.HasAura(113) ||

		!Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 1.8 &&
		Core.Player.CurrentTarget.CurrentHealth >= Core.Player.MaxHealth * .3 &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Demolish.Name, true, 5000))
            {
                return await MySpells.Demolish.Cast();
            }
            return false;
        }

        private async Task<bool> FistsOfWind()
        {
            if (Core.Player.InCombat ||
		!MovementManager.IsMoving &&
		!Core.Player.InCombat &&
		!Core.Player.HasTarget &&
		Core.Player.CurrentHealthPercent > 89)
            {
                if (!Core.Player.HasAura(MySpells.FistsOfEarth.Name) &&
		    !Core.Player.HasAura(MySpells.FistsOfWind.Name) &&
		    !Core.Player.HasAura(MySpells.FistsOfFire.Name))
                {
                    return await MySpells.FistsOfWind.Cast();
		}
            }
            return false;
        }

        private async Task<bool> SteelPeak()
	{
            if (Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (!ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 5000) ||

		    ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		    Core.Player.HasAura(113, true, 3000) &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 3000) ||
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth / 2)
                {
                    return await MySpells.SteelPeak.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Mantra()
        {
            return await MySpells.Mantra.Cast();
        }

        private async Task<bool> HowlingFist()
        {
            if (Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 3000) &&
		    Core.Player.TargetDistance(5, false) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    ActionManager.HasSpell(MySpells.DragonKick.Name) &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    Core.Player.HasAura(113) &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 3000) &&
		    Core.Player.TargetDistance(5, false) ||

		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 3000) &&
		    Core.Player.TargetDistance(5, false))
                {
                    return await MySpells.HowlingFist.Cast();
		}
            }
            return false;
        }

        private async Task<bool> PerfectBalance()
        {
            if (Ultima.UltSettings.MonkPerfectBalance &&
                Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 8 &&
		Core.Player.TargetDistance(4, false) &&
		!Core.Player.HasAura(113))
            {
                if (await MySpells.PerfectBalance.Cast())
                {
                    await Coroutine.Wait(5000, () => Core.Player.HasAura(110));
                    return true;
                }
                return false;
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Lancer

        private async Task<bool> Feint()
        {
            if (Ultima.UltSettings.MonkFeint)
            {
                return await MySpells.CrossClass.Feint.Cast();
            }
            return false;
        }

        private async Task<bool> KeenFlurry()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 3 &&
		!ActionManager.CanCast(MySpells.Featherfoot.Name, Core.Player) &&
		!Core.Player.HasAura(MySpells.Featherfoot.Name) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.CurrentHealthPercent <= 85)
            {
                return await MySpells.CrossClass.KeenFlurry.Cast();
            }
            return false;
        }

        private async Task<bool> Invigorate()
	{
            if (Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (Ultima.UltSettings.MonkInvigorate &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
                    Core.Player.CurrentTP < 540 &&
		    Core.Player.TargetDistance(4, false))
                {
                    return await MySpells.CrossClass.Invigorate.Cast();
		}
            }
            return false;
        }

        private async Task<bool> BloodForBlood()
        {
            if (Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (Ultima.UltSettings.MonkBloodForBlood &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.CurrentTarget.CurrentHealthPercent > 70 &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    !Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		    Core.Player.TargetDistance(4, false) ||

		    Ultima.UltSettings.MonkBloodForBlood &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    !Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		    Core.Player.TargetDistance(4, false))
                {
                    return await MySpells.CrossClass.BloodForBlood.Cast();
		}
            }
            return false;
        }

        #endregion

        #region Marauder

        private async Task<bool> Foresight()
        {
            if (Ultima.UltSettings.MonkForesight)
            {
                return await MySpells.CrossClass.Foresight.Cast();
            }
            return false;
        }

        private async Task<bool> SkullSunder()
        {
            if (Ultima.UltSettings.MonkSkullSunder)
            {
                return await MySpells.CrossClass.SkullSunder.Cast();
            }
            return false;
        }

        private async Task<bool> Fracture()
        {
            if (Ultima.UltSettings.MonkFracture &&
                !Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Fracture.Name, true, 4000))
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
		Core.Player.CurrentHealthPercent <= 75)
            {
                return await MySpells.CrossClass.Bloodbath.Cast();
            }
            return false;
        }

        private async Task<bool> MercyStroke()
        {
            if (ActionManager.LastSpell.Name != MySpells.TrueStrike.Name &&
		ActionManager.LastSpell.Name != MySpells.TwinSnakes.Name &&
		Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name)
            {
                if (Ultima.UltSettings.MonkMercyStroke)
                {
                    return await MySpells.CrossClass.MercyStroke.Cast();
		}
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> Rockbreaker()
        {
            if (Core.Player.HasTarget &&
		Core.Player.CurrentTP >= 250 &&
		Helpers.EnemiesNearTarget(5) > 2 ||
		Core.Player.HasAura(110) &&
		Core.Player.CurrentTP >= 250 &&
		Helpers.EnemiesNearTarget(5) > 2)
            {
                return await MySpells.Rockbreaker.Cast();
            }
            return false;
        }

        private async Task<bool> ShoulderTackle()
        {
            if (Ultima.UltSettings.MonkShoulderTackle)
            {
                return await MySpells.ShoulderTackle.Cast();
            }
            return false;
        }

        private async Task<bool> FistsOfFire()
        {
            if (Core.Player.InCombat ||
		!MovementManager.IsMoving &&
		!Core.Player.InCombat &&
		!Core.Player.HasTarget &&
		Core.Player.CurrentHealthPercent > 89)
            {
                if (!Core.Player.HasAura(MySpells.FistsOfFire.Name) &&
		    !Core.Player.HasAura(MySpells.FistsOfWind.Name) &&
		    !Core.Player.HasAura(MySpells.FistsOfFire.Name))
                {
                    return await MySpells.FistsOfFire.Cast();
		}
            }
            return false;
        }

        private async Task<bool> OneIlmPunch()
        {
            return await MySpells.OneIlmPunch.Cast();
        }

        private async Task<bool> DragonKick()
        {
            if (Core.Player.HasAura(110) &&
		Core.Player.HasAura(113) &&
		!Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		!Core.Player.HasAura(110) &&
		Core.Player.HasAura(107) &&
		!Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.SnapPunch.Name &&
		!Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.Demolish.Name &&
		!Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.TouchOfDeath.Name &&
		!Core.Player.CurrentTarget.HasAura(98, false, 6000) ||

		!Core.Player.HasAura(110) &&
		ActionManager.LastSpell.Name == MySpells.Rockbreaker.Name &&
		!Core.Player.CurrentTarget.HasAura(98, false, 6000))
            {
                return await MySpells.DragonKick.Cast();
            }
            return false;
        }

        private async Task<bool> FormShift()
        {
            if (!Ultima.UltSettings.MultiTarget &&
		Core.Player.HasTarget &&
		!Core.Player.HasAura(109) &&
		!Core.Player.HasAura(113, true, 9000) &&
		!Core.Player.HasAura(112, true, 9000) &&
		!Core.Player.HasAura(111, true, 9000) &&
		Core.Player.TargetDistance(7, true) ||

		!Ultima.UltSettings.MultiTarget &&
		!Core.Player.HasTarget &&
		!Core.Player.HasAura(MySpells.FistsOfWind.Name) &&
		!ActionManager.HasSpell(MySpells.Meditation.Name) &&
		!Core.Player.HasAura(109) &&
		!Core.Player.HasAura(113, true, 6000) &&
		!Core.Player.HasAura(112, true, 6000) &&
		!Core.Player.HasAura(111, true, 6000) ||

		!Ultima.UltSettings.MultiTarget &&
		!Core.Player.HasTarget &&
		!Core.Player.HasAura(MySpells.FistsOfWind.Name) &&
		!Core.Player.InCombat &&
		Core.Player.HasAura(797) &&
		!Core.Player.HasAura(109) &&
		!Core.Player.HasAura(113, true, 6000) &&
		!Core.Player.HasAura(112, true, 6000) &&
		!Core.Player.HasAura(111, true, 6000) ||

		!Core.Player.HasTarget &&
		!Core.Player.HasAura(MySpells.FistsOfWind.Name) &&
		Core.Player.InCombat &&
		!Core.Player.HasAura(109) &&
		!Core.Player.HasAura(113, true, 6000) &&
		!Core.Player.HasAura(112, true, 6000) &&
		!Core.Player.HasAura(111, true, 6000))
            {
                return await MySpells.FormShift.Cast();
            }
            return false;
        }

        private async Task<bool> Meditation()
        {
            if (Ultima.UltSettings.MonkMeditation)
            {
                if (!Ultima.UltSettings.MultiTarget &&
		    !Core.Player.HasTarget &&
		    Helpers.EnemiesNearPlayer(5) == 0 &&
		    !Core.Player.HasAura(797) ||

		    Core.Player.InCombat &&
		    !Core.Player.HasTarget &&
		    Helpers.EnemiesNearPlayer(5) == 0 &&
		    !Core.Player.HasAura(797))
                {
                    return await MySpells.Meditation.Cast();
                }
            }
            return false;
        }

        private async Task<bool> TheForbiddenChakra()
        {
            if (Ultima.UltSettings.SingleTarget &&
		ActionManager.LastSpell.Name != MySpells.TrueStrike.Name &&
		ActionManager.LastSpell.Name != MySpells.TwinSnakes.Name &&
		Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (Ultima.UltSettings.MonkTheForbiddenChakra &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasAura(107) &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name) &&
		    Core.Player.HasAura(113) &&
		    Core.Player.HasAura(797) &&
		    Core.Player.CurrentTP >= 640 ||

		    Ultima.UltSettings.MonkTheForbiddenChakra &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name) &&
		    Core.Player.HasAura(797) &&
		    Core.Player.CurrentTP >= 640)
                {
                    return await MySpells.TheForbiddenChakra.Cast();
		}
            }
            return false;
        }

        private async Task<bool> ElixirField()
        {
            if (ActionManager.LastSpell.Name != MySpells.TrueStrike.Name &&
		ActionManager.LastSpell.Name != MySpells.TwinSnakes.Name &&
		Ultima.LastSpell.Name != MySpells.Purification.Name &&
		Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (Ultima.UltSettings.MonkElixirField &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    Core.Player.HasAura(113) &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 3000) &&
                    Core.Player.TargetDistance(4, false) ||

		    Ultima.UltSettings.MonkElixirField &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 3000) &&
		    Core.Player.TargetDistance(4, false) ||

		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    Core.Player.HasAura(MySpells.TwinSnakes.Name, false, 3000) &&
		    Core.Player.TargetDistance(4, false) &&
		    Helpers.EnemiesNearTarget(6) > 2)
                {
                    return await MySpells.ElixirField.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Purification()
        {
            if (Ultima.LastSpell.Name != MySpells.ShoulderTackle.Name &&
		Ultima.LastSpell.Name != MySpells.TheForbiddenChakra.Name &&
		Ultima.LastSpell.Name != MySpells.ElixirField.Name &&
		Ultima.LastSpell.Name != MySpells.SteelPeak.Name &&
		Ultima.LastSpell.Name != MySpells.HowlingFist.Name &&
		Ultima.LastSpell.Name != MySpells.PerfectBalance.Name &&
		Ultima.LastSpell.Name != MySpells.InternalRelease.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.MercyStroke.Name)
            {
                if (!ActionManager.CanCast(MySpells.CrossClass.Invigorate.Name, Core.Player) &&
		    Core.Player.CurrentTP < 640 &&
		    Core.Player.InCombat &&
		    Core.Player.HasTarget &&
		    Core.Player.TargetDistance(4, false))
                {
                    return await MySpells.Purification.Cast();
		}
            }
            return false;
        }

        private async Task<bool> TornadoKick()
        {
            if (Ultima.UltSettings.SingleTarget &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2 &&
		Core.Player.CurrentTarget.CurrentHealthPercent <= 20 &&
		Core.Player.HasAura(113) &&

		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 40 &&
		Core.Player.CurrentTarget.CurrentHealthPercent <= 1 &&
		Core.Player.HasAura(113) &&

		Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * .40 &&
		Core.Player.CurrentTarget.CurrentHealthPercent <= 50 &&
		Core.Player.HasAura(113) &&
		Helpers.EnemiesNearTarget(7) < 2)
            {
                return await MySpells.TornadoKick.Cast();
            }
            return false;
        }

        #endregion

        #region Custom Spells
        private static bool NoGreasedLightning
        {
            get
            {
                return !Core.Player.HasAura(111) &&
                       !Core.Player.HasAura(112) &&
                       !Core.Player.HasAura(113);
            }
        }
        private static bool HasGreasedLightning
        {
            get
            {
                return Core.Player.HasAura(111, true, 4000) ||
                       Core.Player.HasAura(112, true, 4000) ||
                       Core.Player.HasAura(113, true, 4000);
            }
        }

        #endregion

        #region PvP Spells

        private async Task<bool> WeaponThrow()
        {
            return await MySpells.PvP.WeaponThrow.Cast();
        }

        private async Task<bool> Somersault()
        {
            return await MySpells.PvP.Somersault.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        private async Task<bool> FetterWard()
        {
            return await MySpells.PvP.FetterWard.Cast();
        }

        private async Task<bool> Enliven()
        {
            return await MySpells.PvP.Enliven.Cast();
        }

        private async Task<bool> AxeKick()
        {
            return await MySpells.PvP.AxeKick.Cast();
        }

        #endregion
    }
}