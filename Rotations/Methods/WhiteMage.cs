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
    public sealed partial class WhiteMage
    {
        private WhiteMageSpells _mySpells;

        private WhiteMageSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new WhiteMageSpells()); }
        }

        #region Class Spells

        private async Task<bool> Stone()
        {
            return await MySpells.Stone.Cast();
        }

        private async Task<bool> CureREGEN()
        {
            if (Ultima.LastSpell.Name != MySpells.CureII.Name &&
		ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name &&
		!Core.Player.HasAura(155))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
		    hm.HasAura(MySpells.Regen.Name, true) &&
                    hm.IsHealer() && hm.CurrentHealthPercent <= 60 ||
		    hm.HasAura(MySpells.Regen.Name, true) &&
                    hm.IsDPS() && hm.CurrentHealthPercent <= 60 ||
		    hm.HasAura(MySpells.Regen.Name, true) &&
                    hm.IsTank() && hm.CurrentHealthPercent <= 70);

                if (target != null)
                {
                    return await MySpells.Cure.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Cure()
        {
            if (Ultima.LastSpell.Name != MySpells.CureII.Name &&
		ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name &&
		!Core.Player.HasAura(155))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
		    !hm.HasAura(MySpells.Regen.Name, true) &&
                hm.CurrentHealthPercent <= 75);
                
                if (target == null && ChocoboManager.Summoned && ChocoboManager.Object.CurrentHealthPercent <= 50)
                {
                    target = ChocoboManager.Object;
                }

                if (target != null)
                {
                    return await MySpells.Cure.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Aero()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent > 14 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 10 ||

		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10)
            {
                if (!Core.Player.CurrentTarget.HasAura(MySpells.Aero.Name, true, 3000))
                {
                    return await MySpells.Aero.Cast();
		}
            }
            return false;
        }

        private async Task<bool> ClericStance()
        {
            return await MySpells.ClericStance.Cast();
        }

        private async Task<bool> Protect()
        {
            if (Ultima.UltSettings.WhiteMageProtect &&
                !Helpers.HealManager.Any(hm => hm.CurrentHealthPercent <= 70 || hm.IsDead))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm => hm.Type == GameObjectType.Pc && !hm.HasAura(MySpells.Protect.Name));

                if (target != null)
                {
                    return await MySpells.Protect.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Medica()
        {
            if (ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name &&
		Helpers.HealManager.Count(hm =>
                hm.Distance2D(Core.Player) - hm.CombatReach - Core.Player.CombatReach <= 20 &&
                (hm.IsHealer() && hm.CurrentHealthPercent <= 75 ||
                hm.IsDPS() && hm.CurrentHealthPercent <= 75 ||
                hm.IsTank() && hm.CurrentHealthPercent <= 85)) > 2)
            {
                return await MySpells.Medica.Cast();
            }
            return false;
        }

        private async Task<bool> Raise()
        {
                var target = Helpers.PartyMembers.FirstOrDefault(pm =>
                    pm.IsDead &&
                    pm.Type == GameObjectType.Pc &&
                    !pm.HasAura(MySpells.Raise.Name));

                if (target != null &&
                    ActionManager.CanCast(MySpells.Raise.Name, target))
                {
                    if (ActionManager.CanCast(MySpells.CrossClass.Swiftcast.Name, Core.Player))
                    {
                        if (await MySpells.CrossClass.Swiftcast.Cast())
                        {
                            await Coroutine.Wait(3000, () => ActionManager.CanCast(MySpells.Raise.Name, target) &&
                                                             Core.Player.HasAura(MySpells.CrossClass.Swiftcast.Name));
                        }
                    }
                    return await MySpells.Raise.Cast(target);
                }
            return false;
        }

        private async Task<bool> FluidAura()
        {
            if (Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		Ultima.LastSpell.Name == MySpells.Aero.Name ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		Ultima.LastSpell.Name == MySpells.Aero.Name &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.TargetDistance(3, false))
            {
                return await MySpells.FluidAura.Cast();
            }
            return false;
        }

        private async Task<bool> Esuna()
        {
            if (Ultima.LastSpell.Name != MySpells.Esuna.Name)
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
				        return await MySpells.Esuna.Cast(target);
			        }
			    }
			return false;
        }

        private async Task<bool> StoneII()
        {
            return await MySpells.StoneII.Cast();
        }

        private async Task<bool> Repose()
        {
            return await MySpells.Repose.Cast();
        }

        private async Task<bool> CureIIFREECURE()
        {
            if (ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name &&
		Core.Player.HasAura(155))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsHealer() && hm.CurrentHealthPercent <= 70 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 70 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 80);

                if (target != null)
                {
                    return await MySpells.CureII.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> CureII()
        {
            if (ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
		    !Core.Player.HasAura(158, false) &&
                    hm.IsHealer() && hm.CurrentHealthPercent <= 50 ||
		    !Core.Player.HasAura(158, false) &&
                    hm.IsDPS() && hm.CurrentHealthPercent <= 50 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 60);

                if (target != null)
                {
                    return await MySpells.CureII.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Stoneskin()
        {
            if (Ultima.UltSettings.WhiteMageStoneskin &&
                Ultima.LastSpell.Name != MySpells.StoneskinII.Name)
            {
                var target =
                    Helpers.HealManager.FirstOrDefault(hm => !hm.HasAura(MySpells.Stoneskin.Name) && !hm.InCombat);

                if (target != null)
                {
                    return await MySpells.Stoneskin.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> ShroudOfSaints()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent > 40 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentManaPercent > 40 ||

		Core.Player.CurrentTarget.CurrentHealthPercent > 20 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentManaPercent > 40 ||

		Core.Player.CurrentManaPercent <= 40)
            {
                if (Ultima.UltSettings.WhiteMageShroudOfSaints &&
		    Ultima.LastSpell.Name != MySpells.Assize.Name &&
		    Core.Player.InCombat &&
                    Core.Player.CurrentManaPercent <= 75)
                {
                    return await MySpells.ShroudOfSaints.Cast();
		}
            }
            return false;
        }

        private async Task<bool> CureIII()
        {
            if (ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name)
            {
                var target = Helpers.HealManager.FirstOrDefault(pm1 =>
                    Helpers.HealManager.Count(pm2 =>
                        pm1.Distance2D(pm2) - pm1.CombatReach - pm2.CombatReach <= 8 &&
                        (pm2.IsHealer() && pm2.CurrentHealthPercent <= 45 ||
                        pm2.IsDPS() && pm2.CurrentHealthPercent <= 45 ||
                        pm2.IsTank() && pm2.CurrentHealthPercent <= 75)) > 2);

                if (target != null)
                {
                    return await MySpells.CureIII.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> AeroII()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent > 14 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 10 ||

		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10)
            {
                if (Ultima.LastSpell.Name != MySpells.AeroII.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.AeroII.Name, true, 4000))
                {
                    return await MySpells.AeroII.Cast();
		}
            }
            return false;
        }

        private async Task<bool> MedicaII()
        {
            if (!Core.Player.HasAura(297) &&
		!Core.Player.HasAura(837))
            {
                if (ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		    Ultima.LastSpell.Name != MySpells.AeroIII.Name &&
		    Ultima.LastSpell.Name != MySpells.AeroII.Name &&
		    Ultima.LastSpell.Name != MySpells.Aero.Name &&
		    Ultima.LastSpell.Name != MySpells.FluidAura.Name &&
		    Ultima.LastSpell.Name != MySpells.ClericStance.Name &&
		    Ultima.LastSpell.Name != MySpells.PresenceOfMind.Name &&
		    Core.Player.CurrentTarget.CurrentHealthPercent > 15 &&
		    Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		    Core.Player.CurrentManaPercent > 30 &&
		    Core.Player.InCombat &&
		    !Core.Player.HasAura(157) &&
		    !Core.Player.HasAura(151) &&
		    !Core.Player.HasAura(150, true))
                {
                    return await MySpells.MedicaII.Cast();
		}
            }
            return false;
        }

        private async Task<bool> StoneskinII()
        {
            if (Ultima.UltSettings.WhiteMageStoneskinII &&
                Helpers.HealManager.Count(hm => !hm.HasAura(MySpells.Stoneskin.Name) && hm.Distance2D(Core.Player) - hm.CombatReach - Core.Player.CombatReach <= 15) >= 3)
            {
                return await MySpells.StoneskinII.Cast();
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Arcanist

        private async Task<bool> Ruin()
        {
            if (Ultima.UltSettings.WhiteMageRuin)
            {
                return await MySpells.CrossClass.Ruin.Cast();
            }
            return false;
        }

        private async Task<bool> Physick()
        {
            if (Ultima.UltSettings.WhiteMagePhysick)
            {
                return await MySpells.CrossClass.Physick.Cast();
            }
            return false;
        }

        private async Task<bool> Virus()
        {
            if (!Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Virus.Name, true, 4000))
            {
                return await MySpells.CrossClass.Virus.Cast();
            }
            return false;
        }

        private async Task<bool> EyeForAnEye()
        {
            if (Ultima.UltSettings.WhiteMageEyeForAnEye)
            {
                return await MySpells.CrossClass.EyeForAnEye.Cast();
            }
            return false;
        }

        #endregion

        #region Thaumaturge

        private async Task<bool> BlizzardII()
        {
            if (Ultima.UltSettings.WhiteMageBlizzardII)
            {
                return await MySpells.CrossClass.BlizzardII.Cast();
            }
            return false;
        }

        private async Task<bool> Surecast()
        {
            if (Ultima.UltSettings.WhiteMageSurecast)
            {
                return await MySpells.CrossClass.Surecast.Cast();
            }
            return false;
        }

        private async Task<bool> Swiftcast()
        {
            if (Ultima.UltSettings.WhiteMageSwiftcast)
            {
                return await MySpells.CrossClass.Swiftcast.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> PresenceOfMind()
        {
            if (Ultima.UltSettings.WhiteMagePresenceOfMind)
            {
                if (!ActionManager.HasSpell(MySpells.AeroII.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Aero.Name, true, 3000) ||

		    !ActionManager.HasSpell(MySpells.AeroIII.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.AeroII.Name, true, 4000) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Aero.Name, true, 3000) ||

		    ActionManager.HasSpell(MySpells.AeroIII.Name) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.AeroIII.Name, true, 5000) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.AeroII.Name, true, 4000) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Aero.Name, true, 3000))
                {
                    return await MySpells.PresenceOfMind.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Regen()
        {
            if (Core.Player.CurrentTarget.MaxHealth <= Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 45 &&
		ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name &&
		Core.Player.InCombat ||

		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name &&
		Core.Player.InCombat)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    !hm.HasAura(MySpells.Regen.Name, true) &&
                    (hm.IsHealer() && hm.CurrentHealthPercent <= 60 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 60 ||
		    !hm.HasAura(79) &&
		    !hm.HasAura(91) &&
		    !hm.HasAura(743) &&
                    hm.IsTank() && hm.CurrentHealthPercent <= 60 ||
		    hm.HasAura(79) &&
                    hm.IsTank() && hm.CurrentHealthPercent <= 90 ||
		    hm.HasAura(91) &&
                    hm.IsTank() && hm.CurrentHealthPercent <= 90 ||
		    hm.HasAura(743) &&
                    hm.IsTank() && hm.CurrentHealthPercent <= 90));

                if (target != null)
                {
                    return await MySpells.Regen.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> DivineSeal()
        {
            if (Ultima.UltSettings.WhiteMageDivineSeal &&
                Helpers.HealManager.Count(hm => hm.CurrentHealthPercent <= 65) >= 2)
            {
                return await MySpells.DivineSeal.Cast();
            }
            return false;
        }

        private async Task<bool> Holy()
        {
            if (Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(10, false) &&
		Core.Player.CurrentManaPercent >= 40 &&
		Helpers.EnemiesNearPlayer(10) > 2)
            {
                if (Ultima.UltSettings.SmartTarget)
                {
                    return await MySpells.Holy.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Benediction()
        {
            if (Ultima.UltSettings.WhiteMageBenediction)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm => hm.CurrentHealthPercent <= 20);

                if (target != null)
                {
                    return await MySpells.Benediction.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Asylum()
        {
            return await MySpells.Asylum.Cast();
        }

        private async Task<bool> StoneIII()
        {
            return await MySpells.StoneIII.Cast();
        }

        private async Task<bool> Assize()
        {
            if (Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentManaPercent <= 85 &&
		Ultima.LastSpell.Name == MySpells.Aero.Name &&
		!Core.Player.HasAura(154) &&
		Core.Player.TargetDistance(20, false) ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth / 3 &&
		Ultima.LastSpell.Name == MySpells.Aero.Name &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.TargetDistance(20, false))
            {
                return await MySpells.Assize.Cast();
            }
            return false;
        }

        private async Task<bool> AeroIII()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent > 14 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 10 ||

		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10)
            {
                if (Ultima.LastSpell.Name != MySpells.AeroIII.Name &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.AeroIII.Name, true, 5000))
                {
                    return await MySpells.AeroIII.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Tetragrammaton()
        {
            if (Ultima.UltSettings.WhiteMageTetragrammaton &&
		ActionManager.CanCast(MySpells.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.ClericStance.Name)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsHealer() && hm.CurrentHealthPercent <= 35 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 35 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 45);

                if (target != null)
                {
                    return await MySpells.Tetragrammaton.Cast(target);
                }
            }
            return false;
        }

        #endregion

        #region PvP Spells

        private async Task<bool> AethericBurst()
        {
            return await MySpells.PvP.AethericBurst.Cast();
        }

        private async Task<bool> Attunement()
        {
            return await MySpells.PvP.Attunement.Cast();
        }

        private async Task<bool> DivineBreath()
        {
            return await MySpells.PvP.DivineBreath.Cast();
        }

        private async Task<bool> Equanimity()
        {
            return await MySpells.PvP.Equanimity.Cast();
        }

        private async Task<bool> Focalization()
        {
            return await MySpells.PvP.Focalization.Cast();
        }

        private async Task<bool> ManaDraw()
        {
            return await MySpells.PvP.ManaDraw.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        private async Task<bool> SacredPrism()
        {
            return await MySpells.PvP.SacredPrism.Cast();
        }

        #endregion
    }
}