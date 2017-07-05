using Buddy.Coroutines;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Managers;
using System.Linq;
using System.Threading.Tasks;
using UltimaCR.Spells.Main;
using ff14bot.Objects;

namespace UltimaCR.Rotations
{
    public sealed partial class Scholar
    {
        private ScholarSpells _mySpells;

        private ScholarSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new ScholarSpells()); }
        }

        #region Class Spells

        private async Task<bool> Ruin()
        {
            if (!ActionManager.HasSpell(MySpells.Broil.Name) ||

		!ActionManager.HasSpell(MySpells.Broil.Name) &&
		Ultima.LastSpell.Name == MySpells.ShadowFlare.Name &&
		!Core.Player.HasAura(190) ||

		ActionManager.HasSpell(MySpells.Broil.Name) &&
		Core.Player.CurrentManaPercent < 50)
            {
                return await MySpells.Ruin.Cast();
            }
            return false;
        }

        private async Task<bool> Bio()
        {
            if (!Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4000))
            {
                return await MySpells.Bio.Cast();
            }
            return false;
        }

	private async Task<bool> Summon()
        {
            if (Core.Player.Pet == null &&
                Ultima.UltSettings.ScholarSummonPet &&
		Ultima.UltSettings.ScholarEos ||
		Core.Player.Pet == null &&
                Ultima.UltSettings.ScholarSummonPet &&
                !ActionManager.HasSpell(MySpells.SummonII.Name))
		
            {
                if (Ultima.UltSettings.SummonerSwiftcast &&
		    ActionManager.CanCast(MySpells.CrossClass.Swiftcast.Name, Core.Player))
                {
                    if (await MySpells.CrossClass.Swiftcast.Cast())
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(167));
                    }
                }
                return await MySpells.Summon.Cast();
            }
            return false;
        }

        private async Task<bool> Physick()
        {
            if (PartyManager.IsInParty)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.CurrentHealthPercent <= 80);

                if (target != null)
                {
                    return await MySpells.Physick.Cast(target);
                }
            }
            else
            {

                if (Core.Player.CurrentHealthPercent < 80)
                {
                    return await MySpells.Physick.Cast();
                }

            }
            
            return false;
        }

        private async Task<bool> Aetherflow()
        {
            if (!Core.Player.HasAura(MySpells.Aetherflow.Name) ||
		!Core.Player.HasAura(MySpells.Aetherflow.Name) &&
		!Core.Player.HasTarget &&
		Core.Player.InCombat)
            {
                return await MySpells.Aetherflow.Cast();
            }
            return false;
        }

        private async Task<bool> EnergyDrain()
        {
            if (Core.Player.HasAura(MySpells.Aetherflow.Name))
            {
                if (Core.Player.CurrentManaPercent <= 80 ||
		
		    Ultima.LastSpell.Name == MySpells.Bio.Name &&
		    ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player) ||

		    Ultima.LastSpell.Name == MySpells.CrossClass.Aero.Name &&
		    ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player) ||

		    Ultima.LastSpell.Name == MySpells.Broil.Name &&
		    ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player) ||

		    MovementManager.IsMoving &&
		    ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player))
                {
                    return await MySpells.EnergyDrain.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Miasma()
        {
            if (!Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 5000))
            {
                return await MySpells.Miasma.Cast();
            }
            return false;
        }

        private async Task<bool> Virus()
        {
            return await MySpells.Virus.Cast();
        }

        private async Task<bool> SummonII()
        {
            if (Ultima.LastSpell.Name != MySpells.SummonII.Name &&
                Core.Player.Pet == null &&
                Ultima.UltSettings.ScholarSummonPet &&
                Ultima.UltSettings.ScholarSelene)
		
            {
                if (Ultima.UltSettings.SummonerSwiftcast &&
		    ActionManager.CanCast(MySpells.CrossClass.Swiftcast.Name, Core.Player))
                {
                    if (await MySpells.CrossClass.Swiftcast.Cast())
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(167));
                    }
                }
                return await MySpells.SummonII.Cast();
            }
            return false;
        }

        private async Task<bool> Sustain()
        {
            if (Core.Player.Pet != null &&
                Core.Player.Pet.CurrentHealthPercent < 70 &&
                !Core.Player.Pet.HasAura(MySpells.Sustain.Name))
            {
                return await MySpells.Sustain.Cast();
            }
            return false;
        }

        private async Task<bool> Resurrection()
        {
            if (!Helpers.HealManager.Any(hm => hm.CurrentHealthPercent <= 70))
            {
                var target = Helpers.PartyMembers.FirstOrDefault(pm =>
                    pm.IsDead &&
                    pm.Type == GameObjectType.Pc &&
                    !pm.HasAura(MySpells.Resurrection.Name));

                if (target != null &&
                    ActionManager.CanCast(MySpells.Resurrection.Name, target))
                {
                    if (ActionManager.CanCast(MySpells.CrossClass.Swiftcast.Name, Core.Player))
                    {
                        if (await MySpells.CrossClass.Swiftcast.Cast())
                        {
                            await Coroutine.Wait(3000, () => ActionManager.CanCast(MySpells.Resurrection.Name, target) &&
                                                             Core.Player.HasAura(MySpells.CrossClass.Swiftcast.Name));
                        }
                    }
                    return await MySpells.Resurrection.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> BioII()
        {
            if (!Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 5000))
            {
                return await MySpells.BioII.Cast();
            }
            return false;
        }

        private async Task<bool> Bane()
        {
            if (Core.Player.HasAura(MySpells.Aetherflow.Name) &&
                Core.Player.CurrentManaPercent > 40 &&
                Helpers.EnemiesNearTarget(8) > 1 &&
                Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 8000) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 8000) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 5000))
            {
                return await MySpells.Bane.Cast();
            }
            return false;
        }

        private async Task<bool> EyeForAnEye()
        {
            return await MySpells.EyeForAnEye.Cast();
        }

        private async Task<bool> RuinII()
        {
            if (MovementManager.IsMoving)
            {
                return await MySpells.RuinII.Cast();
            }
            if (!ActionManager.HasSpell(MySpells.Broil.Name) &&
		Core.Player.CurrentManaPercent > 40 ||

		ActionManager.HasSpell(MySpells.Broil.Name) &&
		Core.Player.CurrentManaPercent < 50)
            {
                if (ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player) &&
                    !Core.Player.HasAura(MySpells.Aetherflow.Name) ||
                    ActionManager.CanCast(MySpells.EnergyDrain.Name, Core.Player.CurrentTarget) &&
                    Core.Player.CurrentManaPercent <= 90 &&
                    Helpers.EnemiesNearTarget(8) <= 1 &&
                    Ultima.UltSettings.SmartTarget ||

                    ActionManager.CanCast(MySpells.EnergyDrain.Name, Core.Player.CurrentTarget) &&
                    Core.Player.CurrentManaPercent <= 90 &&
                    Ultima.UltSettings.SingleTarget ||

                    ActionManager.CanCast(MySpells.Bane.Name, Core.Player.CurrentTarget) &&
                    !Ultima.UltSettings.SingleTarget &&
                    Helpers.EnemiesNearTarget(8) > 1 &&
                    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true) ||
                    ActionManager.CanCast(MySpells.Rouse.Name, Core.Player))
                {
                    return await MySpells.RuinII.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Rouse()
        {
            return await MySpells.Rouse.Cast();
        }

        private async Task<bool> MiasmaII()
        {
            if (Helpers.EnemiesNearPlayer(5) > 3 &&
                Core.Player.CurrentTarget.HasAura(MySpells.MiasmaII.Name, true, 3000))
            {
                return await MySpells.MiasmaII.Cast();
            }
            return false;
        }

        private async Task<bool> ShadowFlare()
        {
            if (Ultima.LastSpell.Name != MySpells.ShadowFlare.Name)
            {
                if (!Core.Player.HasAura(190, false, 4500) &&
		            Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true))
                    {
                        return await MySpells.ShadowFlare.Cast();
		            }
            }
            return false;
        }


        #endregion

        #region Cross Class Spells

        #region Conjurer

        private async Task<bool> Cure()
        {
            if (Ultima.UltSettings.ScholarCure)
            {
                return await MySpells.CrossClass.Cure.Cast();
            }
            return false;
        }

        private async Task<bool> Aero()
        {
            if (Ultima.UltSettings.ScholarAero &&
                !Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Aero.Name, true, 4000))
            {
                return await MySpells.CrossClass.Aero.Cast();
            }
            return false;
        }

        private async Task<bool> ClericStance()
        {
            if (ActionManager.HasSpell(MySpells.CrossClass.ClericStance.Name))
            {
                return await MySpells.CrossClass.ClericStance.Cast();
            }
            return false;
        }

        private async Task<bool> Protect()
        {
            if (PartyManager.IsInParty && Ultima.LastSpell.Name != MySpells.CrossClass.Protect.Name)
            {
                var target = Helpers.PartyMembers.FirstOrDefault(pm =>
                pm.Type == GameObjectType.Pc &&
                !pm.HasAura("Protect"));

                if (target != null)
                {
                    return await MySpells.CrossClass.Protect.Cast(target);
                }
            }
            else
            {
                if (!Core.Player.HasAura("Protect") && Ultima.LastSpell.Name != MySpells.CrossClass.Protect.Name)
                {
                    return await MySpells.CrossClass.Protect.Cast();
                }
            }
            

            return false;
        }

        private async Task<bool> Raise()
        {
            if (Ultima.UltSettings.ScholarRaise)
            {
                return await MySpells.CrossClass.Raise.Cast();
            }
            return false;
        }

        private async Task<bool> Stoneskin()
        {
            if (Ultima.UltSettings.ScholarStoneskin)
            {
                var target =
                    Helpers.HealManager.FirstOrDefault(hm => !hm.HasAura(MySpells.CrossClass.Stoneskin.Name) && !hm.InCombat);

                if (target != null)
                {
                    return await MySpells.CrossClass.Stoneskin.Cast(target);
                }
            }
            return false;
        }

        #endregion

        #region Thaumaturge

        private async Task<bool> Surecast()
        {
            if (Ultima.UltSettings.ScholarSurecast)
            {
                return await MySpells.CrossClass.Surecast.Cast();
            }
            return false;
        }

        private async Task<bool> BlizzardII()
        {
            if (Helpers.EnemiesNearPlayer(5) > 3)
            {
                return await MySpells.CrossClass.BlizzardII.Cast();
            }
            return false;
        }

        private async Task<bool> Swiftcast()
        {
            if (Ultima.UltSettings.ScholarSwiftcast)
            {
                return await MySpells.CrossClass.Swiftcast.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> Adloquium()
        {
            if (PartyManager.IsInParty)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
		            !hm.HasAura(297) &&
		            !hm.HasAura(837) &&
                    hm.IsTank() && hm.CurrentHealthPercent <= 70);

                if (target != null)
                {
                    return await MySpells.Adloquium.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Succor()
        {
            if (
		        !Core.Player.HasAura(297) &&
		        !Core.Player.HasAura(837))
                {
                    return await MySpells.Succor.Cast();
                }
            return false;
        }

        private async Task<bool> Leeches()
        {
            if (Ultima.LastSpell.Name != MySpells.Leeches.Name)
			{
		        var target = Helpers.PartyMembers.FirstOrDefault(pm =>
                    pm.Type == GameObjectType.Pc &&
                    (pm.HasAura("Paralysis") ||
			        pm.HasAura("Damage Down") ||
			        pm.HasAura("Poison") ||
                    pm.HasAura("Silence") ||
                    pm.HasAura("Sleep")));
			
			    if (target != null)
                {
				    return await MySpells.Leeches.Cast(target);
			    }
		    }
		return false;
        }
        

        private async Task<bool> SacredSoil()
        {
            return await MySpells.SacredSoil.Cast();
        }

        private async Task<bool> Lustrate()
        {
            if (ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
		    !hm.HasAura(297) &&
                    hm.IsHealer() && hm.CurrentHealthPercent <= 40 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 40 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 35);

                if (target != null)
                {
                    return await MySpells.Lustrate.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Indomitability()
        {
            if (ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		Core.Player.HasAura(MySpells.Aetherflow.Name) &&
		Core.Player.HasAura(297, true) &&
		Helpers.HealManager.Count(hm =>
                hm.Distance2D(Core.Player) - hm.CombatReach - Core.Player.CombatReach <= 20 &&
                (hm.IsHealer() && hm.CurrentHealthPercent <= 70 ||
                hm.IsDPS() && hm.CurrentHealthPercent <= 70 ||
                hm.IsTank() && hm.CurrentHealthPercent <= 80)) > 2)
            {
                return await MySpells.Indomitability.Cast();
            }
            return false;
        }

        private async Task<bool> Broil()
        {
            if (Ultima.UltSettings.SingleTarget &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 14 &&
		Core.Player.CurrentTarget.MaxHealth <= Core.Player.CurrentHealth * 10 &&
		Core.Player.CurrentManaPercent >= 50 &&
		Core.Player.HasAura(190, false) &&
		Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true) ||

		Ultima.UltSettings.SingleTarget &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentTarget.MaxHealth > Core.Player.CurrentHealth * 10 &&
		Core.Player.CurrentManaPercent >= 50 &&
		Core.Player.HasAura(190, false) &&
		Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true) ||

		Ultima.UltSettings.SingleTarget &&
		Core.Player.CurrentTarget.CurrentHealthPercent <= 14 &&
		Core.Player.CurrentTarget.MaxHealth <= Core.Player.CurrentHealth * 10 &&
		Core.Player.CurrentManaPercent >= 50 ||

		Ultima.UltSettings.SingleTarget &&
		Core.Player.CurrentTarget.CurrentHealthPercent <= 7 &&
		Core.Player.CurrentTarget.MaxHealth > Core.Player.CurrentHealth * 10 &&
		Core.Player.CurrentManaPercent >= 50 ||

		Ultima.UltSettings.SingleTarget &&
		Core.Player.CurrentTarget.MaxHealth < Core.Player.CurrentHealth * 2 ||

		Ultima.UltSettings.SmartTarget &&
		Core.Player.CurrentManaPercent >= 50 &&
		Core.Player.HasAura(190, false) ||

		Ultima.UltSettings.MultiTarget &&
		Core.Player.CurrentManaPercent >= 50 ||

		Ultima.LastSpell.Name == MySpells.ShadowFlare.Name &&
		!Core.Player.HasAura(190))
            {
                return await MySpells.Broil.Cast();
            }
            return false;
        }

        private async Task<bool> DeploymentTactics()
        {
            return await MySpells.DeploymentTactics.Cast();
        }

        private async Task<bool> EmergencyTactics()
        {
            return await MySpells.EmergencyTactics.Cast();
        }

        private async Task<bool> Dissipation()
        {
            return await MySpells.Dissipation.Cast();
        }

        #endregion

        #region PvP Spells

        private async Task<bool> Attunement()
        {
            return await MySpells.PvP.Attunement.Cast();
        }

        private async Task<bool> AuraBlast()
        {
            return await MySpells.PvP.AuraBlast.Cast();
        }

        private async Task<bool> Focalization()
        {
            return await MySpells.PvP.Focalization.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        #endregion
    }
}