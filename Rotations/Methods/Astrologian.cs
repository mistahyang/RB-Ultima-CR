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
    public sealed partial class Astrologian
    {
        private AstrologianSpells _mySpells;

        private AstrologianSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new AstrologianSpells()); }
        }

        #region Job Spells

        private async Task<bool> Malefic()
        {
            return await MySpells.Malefic.Cast();
        }

        private async Task<bool> BeneficREGEN()
        {
            if (Ultima.UltSettings.AstrologianBenefic &&
		Ultima.LastSpell.Name != MySpells.BeneficII.Name &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Core.Player.HasAura(839) &&
		!Core.Player.HasAura(815))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
		    hm.HasAura(835) &&
                    hm.IsHealer() && hm.CurrentHealthPercent <= 60 ||
		    hm.HasAura(835) &&
                    hm.IsDPS() && hm.CurrentHealthPercent <= 60 ||
		    hm.HasAura(835) &&
                    hm.IsTank() && hm.CurrentHealthPercent <= 70);

                if (target != null)
                {
                    return await MySpells.Benefic.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Benefic()
        {
            if (Ultima.UltSettings.AstrologianBenefic &&
		Ultima.LastSpell.Name != MySpells.BeneficII.Name &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		!Core.Player.HasAura(815))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsHealer() && hm.CurrentHealthPercent <= 70 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 70 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 80);

                if (target != null)
                {
                    return await MySpells.Benefic.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Combust()
        {
            if (Core.Player.CurrentTarget.CurrentHealthPercent > 14 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 10 ||

		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10)
            {
                if (!ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    !Core.Player.CurrentTarget.HasAura(838, true, 3000) ||
		    
		    !Ultima.UltSettings.SmartTarget &&
		    !Core.Player.CurrentTarget.HasAura(838, true, 3000) ||

		    Ultima.UltSettings.SmartTarget &&
		    ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    Helpers.EnemiesNearTarget(8) < 3 &&
		    !Core.Player.CurrentTarget.HasAura(838, true, 3000))
                {
                    return await MySpells.Combust.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Lightspeed()
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
                if (Core.Player.InCombat &&
		    Helpers.HealManager.Count(hm => hm.CurrentHealthPercent <= 65) >= 2 ||

		    Core.Player.InCombat &&
                    Core.Player.CurrentManaPercent <= 65)
                {
                    return await MySpells.Lightspeed.Cast();
		}
            }
            return false;
        }

        private async Task<bool> LuminiferousAether()
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
                if (Core.Player.InCombat &&
                    Core.Player.CurrentManaPercent <= 75)
                {
                    return await MySpells.LuminiferousAether.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Helios()
        {
            if (ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Helpers.HealManager.Count(hm =>
                hm.Distance2D(Core.Player) - hm.CombatReach - Core.Player.CombatReach <= 20 &&
                (hm.IsHealer() && hm.CurrentHealthPercent <= 75 ||
                hm.IsDPS() && hm.CurrentHealthPercent <= 75 ||
                hm.IsTank() && hm.CurrentHealthPercent <= 85)) > 2)
            {
                return await MySpells.Helios.Cast();
            }
            return false;
        }

        private async Task<bool> Ascend()
        {
            if (!Helpers.HealManager.Any(hm => hm.CurrentHealthPercent <= 70))
            {
                var target = Helpers.PartyMembers.FirstOrDefault(pm =>
                    pm.IsDead &&
                    pm.Type == GameObjectType.Pc &&
                    !pm.HasAura(MySpells.Ascend.Name));

                if (target != null &&
                    ActionManager.CanCast(MySpells.Ascend.Name, target))
                {
                    if (ActionManager.CanCast(MySpells.CrossClass.Swiftcast.Name, Core.Player))
                    {
                        if (await MySpells.CrossClass.Swiftcast.Cast())
                        {
                            await Coroutine.Wait(3000, () => ActionManager.CanCast(MySpells.Ascend.Name, target) &&
                                                             Core.Player.HasAura(MySpells.CrossClass.Swiftcast.Name));
                        }
                    }
                    return await MySpells.Ascend.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> EssentialDignity()
        {
            if (Ultima.UltSettings.AstrologianEssentialDignity &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsHealer() && hm.CurrentHealthPercent <= 35 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 35 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 45);

                if (target != null)
                {
                    return await MySpells.EssentialDignity.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> ExaltedDetriment()
        {
                if (Ultima.LastSpell.Name != MySpells.ExaltedDetriment.Name)
			    {
		            var target = Helpers.PartyMembers.FirstOrDefault(pm =>
                        pm.Type == GameObjectType.Pc &&
                        (pm.HasAura("Paralysis") ||
			            pm.HasAura("Damage Down") ||
			            pm.HasAura("Poison")));
			
			    if (target != null)
                {
				    return await MySpells.ExaltedDetriment.Cast(target);
			    }
			}
			return false;
        }

        private async Task<bool> Stella()
        {
            return await MySpells.Stella.Cast();
        }

        private async Task<bool> BeneficIIENHANCED()
        {
            if (Ultima.UltSettings.AstrologianBeneficII &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Core.Player.HasAura(815))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsHealer() && hm.CurrentHealthPercent <= 40 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 40 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 50);

                if (target != null)
                {
                    return await MySpells.BeneficII.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> BeneficII()
        {
            if (Ultima.UltSettings.AstrologianBeneficII &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		!Core.Player.HasAura(815))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.IsHealer() && hm.CurrentHealthPercent <= 50 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 50 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 60);

                if (target != null)
                {
                    return await MySpells.BeneficII.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Draw()
        {
            if (Ultima.UltSettings.AstrologianDraw &&
		Ultima.LastSpell.Name != MySpells.Spread.Name)
            {
                if (Ultima.LastSpell.Name != MySpells.Spread.Name)
                {
                    return await MySpells.Draw.Cast();
		}
            }
            return false;
        }

        private async Task<bool> DiurnalSect()
        {
            if (!Core.Player.HasAura(MySpells.DiurnalSect.Name) &&
		!Core.Player.HasAura(MySpells.NocturnalSect.Name))
            {
                return await MySpells.DiurnalSect.Cast();
            }
            return false;
        }

        private async Task<bool> AspectedBeneficDIUR()
        {
            if (Core.Player.InCombat &&
		Ultima.UltSettings.AstrologianAspectedBenefic &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Core.Player.HasAura(MySpells.DiurnalSect.Name))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    !hm.HasAura(835, true) &&
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
                    return await MySpells.AspectedBenefic.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> AspectedBeneficNOCT()
        {
            if (Ultima.UltSettings.AstrologianAspectedBenefic &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Core.Player.HasAura(MySpells.NocturnalSect.Name))
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
		    !hm.HasAura(297) &&
		    !hm.HasAura(837) &&
                    hm.IsHealer() && hm.CurrentHealthPercent <= 1 ||
                    hm.IsDPS() && hm.CurrentHealthPercent <= 1 ||
                    hm.IsTank() && hm.CurrentHealthPercent <= 70);

                if (target != null)
                {
                    return await MySpells.AspectedBenefic.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Disable()
        {
            return await MySpells.Disable.Cast();
        }

        private async Task<bool> RoyalRoad()
        {
            if (Ultima.UltSettings.AstrologianDraw &&
		Core.Player.CurrentManaPercent > 35 &&
		Core.Player.HasAura(917) &&
		!Core.Player.HasAura(817) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.HasAura(918) &&
		!Core.Player.HasAura(817) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.CurrentManaPercent > 35 &&
		!Core.Player.HasAura(913) &&
		!Core.Player.HasAura(914) &&
		!Core.Player.HasAura(915) &&
		!Core.Player.HasAura(916) &&
		!Core.Player.HasAura(918) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925) &&
		Core.Player.HasAura(917) &&
		Ultima.LastSpell.Name != MySpells.Redraw.Name &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) &&
		Core.Player.HasAura(817) ||

		Ultima.UltSettings.AstrologianDraw &&
		!Core.Player.HasAura(913) &&
		!Core.Player.HasAura(914) &&
		!Core.Player.HasAura(915) &&
		!Core.Player.HasAura(916) &&
		!Core.Player.HasAura(917) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925) &&
		Core.Player.HasAura(918) &&
		Ultima.LastSpell.Name != MySpells.Redraw.Name &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) &&
		Core.Player.HasAura(817) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.CurrentManaPercent > 35 &&
		!Core.Player.HasAura(913) &&
		!Core.Player.HasAura(914) &&
		!Core.Player.HasAura(915) &&
		!Core.Player.HasAura(916) &&
		!Core.Player.HasAura(918) &&
		Core.Player.HasAura(920) &&
		Core.Player.HasAura(921) &&
		Core.Player.HasAura(922) &&
		Core.Player.HasAura(923) &&
		Core.Player.HasAura(924) &&
		Core.Player.HasAura(925) &&
		Core.Player.HasAura(917) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) &&
		Core.Player.HasAura(817) ||

		Ultima.UltSettings.AstrologianDraw &&
		!Core.Player.HasAura(913) &&
		!Core.Player.HasAura(914) &&
		!Core.Player.HasAura(915) &&
		!Core.Player.HasAura(916) &&
		!Core.Player.HasAura(917) &&
		Core.Player.HasAura(920) &&
		Core.Player.HasAura(921) &&
		Core.Player.HasAura(922) &&
		Core.Player.HasAura(923) &&
		Core.Player.HasAura(924) &&
		Core.Player.HasAura(925) &&
		Core.Player.HasAura(918) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) &&
		Core.Player.HasAura(817))
            {
                return await MySpells.RoyalRoad.Cast();
            }
            return false;
        }

        private async Task<bool> Spread()
        {
            if (Ultima.UltSettings.AstrologianDraw &&
		Core.Player.CurrentManaPercent <= 35 &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(917) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925) ||

		Ultima.UltSettings.AstrologianDraw &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) &&
		Core.Player.HasAura(914) &&
		Core.Player.HasAura(817) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.HasAura(913) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.HasAura(915) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925) ||

		Ultima.UltSettings.AstrologianDraw &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) &&
		Core.Player.HasAura(916) &&
		Core.Player.HasAura(817) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925))
            {
                return await MySpells.Spread.Cast();
            }
            return false;
        }

        private async Task<bool> AspectedHelios()
        {
            if (Core.Player.HasAura(MySpells.DiurnalSect.Name) &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CombustII.Name &&
		Ultima.LastSpell.Name != MySpells.Combust.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Aero.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 15 &&
		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		Core.Player.CurrentManaPercent > 30 &&
		Core.Player.InCombat &&
		!Core.Player.HasAura(836, true) &&
		!Core.Player.HasAura(297) &&
		!Core.Player.HasAura(837) &&
		!Core.Player.HasAura(151)||

		Core.Player.HasAura(MySpells.NocturnalSect.Name) &&
		ActionManager.CanCast(MySpells.CrossClass.ClericStance.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.CombustII.Name &&
		Ultima.LastSpell.Name != MySpells.Combust.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Aero.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.ClericStance.Name &&
		Ultima.UltSettings.SingleTarget &&
		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 10 &&
		Core.Player.InCombat &&
		!Core.Player.HasAura(297) &&
		!Core.Player.HasAura(837))
            {
                return await MySpells.AspectedHelios.Cast();
            }
            return false;
        }



        private async Task<bool> Redraw()
        {
            if (Ultima.UltSettings.AstrologianDraw &&
		!Core.Player.HasAura(817) &&
		Core.Player.HasAura(914) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(914) ||

		Ultima.UltSettings.AstrologianDraw &&
		!Core.Player.HasAura(817) &&
		Core.Player.HasAura(916) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(916) ||

		Ultima.UltSettings.AstrologianDraw &&
		!Core.Player.HasAura(817) &&
		Core.Player.HasAura(916) &&
		Core.Player.HasAura(920) ||

		Ultima.UltSettings.AstrologianDraw &&
		!Core.Player.HasAura(817) &&
		Core.Player.HasAura(916) &&
		Core.Player.HasAura(922) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(917) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925) ||

		Ultima.UltSettings.AstrologianDraw &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(918) &&
		!Core.Player.HasAura(920) &&
		!Core.Player.HasAura(921) &&
		!Core.Player.HasAura(922) &&
		!Core.Player.HasAura(923) &&
		!Core.Player.HasAura(924) &&
		!Core.Player.HasAura(925))
            {
                return await MySpells.Redraw.Cast();
            }
            return false;
        }

        private async Task<bool> CombustII()
        {
            if (Ultima.LastSpell.Name != MySpells.CombustII.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 14 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 10 ||

		Ultima.LastSpell.Name != MySpells.CombustII.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10)
            {
                if (!ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    !Core.Player.CurrentTarget.HasAura(843, true, 4000) ||

		    !Ultima.UltSettings.SmartTarget &&
		    ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    !Core.Player.CurrentTarget.HasAura(843, true, 4000) ||

		    Ultima.UltSettings.SmartTarget &&
		    ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    Helpers.EnemiesNearTarget(8) < 3 &&
		    !Core.Player.CurrentTarget.HasAura(843, true, 4000))
                {
                    return await MySpells.CombustII.Cast();
		}
            }
            return false;
        }

        private async Task<bool> NocturnalSect()
        {
            if (!Core.Player.HasAura(MySpells.DiurnalSect.Name) &&
		!Core.Player.HasAura(MySpells.NocturnalSect.Name))
            {
                return await MySpells.NocturnalSect.Cast();
            }
            return false;
        }

        private async Task<bool> Synastry()
        {
            return await MySpells.Synastry.Cast();
        }

        private async Task<bool> Gravity()
        {
            if (Core.Player.HasAura(145) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.CurrentManaPercent >= 40 &&
		Helpers.EnemiesNearTarget(8) > 2)
            {
                if (Ultima.UltSettings.SmartTarget)
                {
                    return await MySpells.Gravity.Cast();
		}
            }
            return false;
        }

        private async Task<bool> MaleficII()
        {
            return await MySpells.MaleficII.Cast();
        }

        private async Task<bool> TimeDilation()
        {
            if (Core.Player.CurrentTarget.HasAura(829, true) ||

		Core.Player.CurrentTarget.HasAura(830, true) ||

		Core.Player.CurrentTarget.HasAura(831, true) ||

		Core.Player.CurrentTarget.HasAura(832, true))
            {
                return await MySpells.TimeDilation.Cast();
            }
            return false;
        }

        private async Task<bool> CollectiveUnconscious()
        {
            return await MySpells.CollectiveUnconscious.Cast();
        }

        private async Task<bool> CelestialOpposition()
        {
            return await MySpells.CelestialOpposition.Cast();
        }

        #endregion

        #region Cross Class Spells

        #region Conjurer

        private async Task<bool> Cure()
        {
            if (Ultima.UltSettings.AstrologianCure)
            {
                return await MySpells.CrossClass.Cure.Cast();
            }
            return false;
        }

        private async Task<bool> Aero()
        {
            if (Ultima.LastSpell.Name != MySpells.CrossClass.Aero.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 14 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth &&
		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 10 ||

		Ultima.LastSpell.Name != MySpells.CrossClass.Aero.Name &&
		Core.Player.CurrentTarget.CurrentHealthPercent > 7 &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 10)
            {
                if (Ultima.UltSettings.AstrologianAero &&
		    !ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Aero.Name, true, 4000) ||

		    Ultima.UltSettings.AstrologianAero &&
		    !Ultima.UltSettings.SmartTarget &&
		    ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Aero.Name, true, 4000) ||

		    Ultima.UltSettings.AstrologianAero &&
		    Ultima.UltSettings.SmartTarget &&
		    ActionManager.HasSpell(MySpells.Gravity.Name) &&
		    Helpers.EnemiesNearTarget(8) < 3 &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.CrossClass.Aero.Name, true, 4000))
                {
                    return await MySpells.CrossClass.Aero.Cast();
		}
            }
            return false;
        }

        private async Task<bool> ClericStance()
        {
            if (Ultima.UltSettings.AstrologianClericStance)
            {
                return await MySpells.CrossClass.ClericStance.Cast();
            }
            return false;
        }

        private async Task<bool> Protect()
        {
            if (Ultima.UltSettings.AstrologianProtect)
                {
                    var target = Helpers.PartyMembers.FirstOrDefault(pm =>
                        pm.Type == GameObjectType.Pc &&
                    !pm.HasAura("Protect"));

                    if (target != null)
                    {
                        return await MySpells.CrossClass.Protect.Cast(target);
                    }
                }
            return false;
        }

        private async Task<bool> Raise()
        {
            if (Ultima.UltSettings.AstrologianRaise)
            {
                return await MySpells.CrossClass.Raise.Cast();
            }
            return false;
        }

        private async Task<bool> Stoneskin()
        {
            if (Ultima.UltSettings.AstrologianStoneskin &&
                !Core.Player.HasAura(MySpells.CrossClass.Stoneskin.Name))
            {
                return await MySpells.CrossClass.Stoneskin.Cast();
            }
            return false;
        }

        #endregion

        #region Thaumaturge

        private async Task<bool> Surecast()
        {
            if (Ultima.UltSettings.AstrologianSurecast)
            {
                return await MySpells.CrossClass.Surecast.Cast();
            }
            return false;
        }

        private async Task<bool> BlizzardII()
        {
            if (Ultima.UltSettings.AstrologianBlizzardII)
            {
                return await MySpells.CrossClass.BlizzardII.Cast();
            }
            return false;
        }

        private async Task<bool> Swiftcast()
        {
            if (Ultima.UltSettings.AstrologianSwiftcast)
            {
                return await MySpells.CrossClass.Swiftcast.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region PvP Spells



        #endregion

        #region Custom Spells

        private async Task<bool> Play()
        {
            if (!MovementManager.IsMoving &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(913) ||



		Core.Player.HasAura(817) &&
		Core.Player.HasAura(820) &&
		Core.Player.HasAura(913) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) ||

		Core.Player.HasAura(817) &&
		Core.Player.HasAura(822) &&
		Core.Player.HasAura(913) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) ||

		Core.Player.HasAura(817) &&
		Core.Player.HasAura(823) &&
		Core.Player.HasAura(913) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player))
            {
                return await MySpells.TheBalance.Cast();
            }

            if (!MovementManager.IsMoving &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(915) ||



		Core.Player.HasAura(817) &&
		Core.Player.HasAura(820) &&
		Core.Player.HasAura(915) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) ||

		Core.Player.HasAura(817) &&
		Core.Player.HasAura(822) &&
		Core.Player.HasAura(915) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) ||

		Core.Player.HasAura(817) &&
		Core.Player.HasAura(823) &&
		Core.Player.HasAura(915) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player))
            {
                return await MySpells.TheArrow.Cast();
            }

            if (!MovementManager.IsMoving &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.HasAura(817) &&
		Core.Player.HasAura(916) ||



		Core.Player.HasAura(817) &&
		Core.Player.HasAura(820) &&
		Core.Player.HasAura(916) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) ||

		Core.Player.HasAura(817) &&
		Core.Player.HasAura(822) &&
		Core.Player.HasAura(916) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player) ||

		Core.Player.HasAura(817) &&
		Core.Player.HasAura(823) &&
		Core.Player.HasAura(916) &&
		!ActionManager.CanCast(MySpells.Redraw.Name, Core.Player))
            {
                return await MySpells.TheSpear.Cast();
            }

            if (Core.Player.CurrentManaPercent < 40 &&
		!ActionManager.CanCast(MySpells.RoyalRoad.Name, Core.Player) &&
		!Core.Player.HasAura(817) &&
		Core.Player.HasAura(917))
            {
                return await MySpells.TheEwer.Cast();
            }
            return false;
        }

        #endregion
    }
}