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
    public sealed partial class RedMage
    {
        private RedMageSpells _mySpells;

        private RedMageSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new RedMageSpells()); }
        }

        #region Class Spells

        private async Task<bool> Jolt()
        {
            if (ActionResourceManager.RedMage.WhiteMana < 80 ||
                ActionResourceManager.RedMage.BlackMana < 80 ||
                ActionResourceManager.RedMage.WhiteMana == ActionResourceManager.RedMage.BlackMana)
            {
                if (!ActionManager.HasSpell(MySpells.Redoublement.Name) &&
                    !ActionManager.HasSpell(MySpells.Zwerchhau.Name))
                {
                    return await MySpells.Jolt.Cast();
                }
                else
                {
                    if (ActionManager.HasSpell(MySpells.Redoublement.Name) && 
                        (Ultima.LastSpell.Name != MySpells.Zwerchhau.Name ||
                        Ultima.LastSpell.Name != MySpells.Riposte.Name))
                    {
                        return await MySpells.Jolt.Cast();
                    }
                    else
                    {
                        if (Ultima.LastSpell.Name != MySpells.Riposte.Name)
                        {
                            return await MySpells.Jolt.Cast();
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Vercure()
        {
            if (PartyManager.IsInParty)
            {
                var target = Helpers.HealManager.FirstOrDefault(hm =>
                    hm.CurrentHealthPercent <= 50);

                if (target != null)
                {
                    return await MySpells.Vercure.Cast(target);
                }
            }
            else
            {

                if (Core.Player.CurrentHealthPercent < 50)
                {
                    return await MySpells.Vercure.Cast();
                }

            }
            
            return false;
        }

        private async Task<bool> Fleche()
        {
            if (Ultima.LastSpell.Name != MySpells.Acceleration.Name)
            {
                return await MySpells.Fleche.Cast();
            }
            return false;
        }

        private async Task<bool> Acceleration()
        {
            if (Ultima.LastSpell.Name != MySpells.Fleche.Name)
            {
                return await MySpells.Acceleration.Cast();
            }
            return false;
        }

        private async Task<bool> Erase()
        {
            if (Core.Player.CurrentHealthPercent <= 40)
            {
                return await MySpells.CrossClass.Erase.Cast();
            }
            return false;
        }

        private async Task<bool> Verthunder()
        {
            if (Core.Player.HasAura("Dualcast") &&
            ActionResourceManager.RedMage.BlackMana <= ActionResourceManager.RedMage.WhiteMana)
            {
                if (!ActionManager.HasSpell(MySpells.Redoublement.Name) &&
                !ActionManager.HasSpell(MySpells.Zwerchhau.Name))
                {
                    return await MySpells.Verthunder.Cast();
                }
                else
                {
                    if (ActionManager.HasSpell(MySpells.Redoublement.Name) && 
                    (Ultima.LastSpell.Name != MySpells.Zwerchhau.Name ||
                    Ultima.LastSpell.Name != MySpells.Riposte.Name))
                    {
                        return await MySpells.Verthunder.Cast();
                    }
                    else
                    {
                        if (Ultima.LastSpell.Name != MySpells.Riposte.Name)
                        {
                            return await MySpells.Verthunder.Cast();
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Veraero()
        {
            if (Core.Player.HasAura("Dualcast") &&
            ActionResourceManager.RedMage.WhiteMana <= ActionResourceManager.RedMage.BlackMana)
            {
                if (!ActionManager.HasSpell(MySpells.Redoublement.Name) &&
                !ActionManager.HasSpell(MySpells.Zwerchhau.Name))
                {
                    return await MySpells.Veraero.Cast();
                }
                else
                {
                    if (ActionManager.HasSpell(MySpells.Redoublement.Name) && 
                    (Ultima.LastSpell.Name != MySpells.Zwerchhau.Name ||
                    Ultima.LastSpell.Name != MySpells.Riposte.Name))
                    {
                        return await MySpells.Veraero.Cast();
                    }
                    else
                    {
                        if (Ultima.LastSpell.Name != MySpells.Riposte.Name)
                        {
                            return await MySpells.Veraero.Cast();
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Verstone()
        {
            if (!ActionManager.HasSpell(MySpells.Redoublement.Name) &&
            !ActionManager.HasSpell(MySpells.Zwerchhau.Name))
            {
                return await MySpells.Verstone.Cast();
            }
            else
            {
                if (ActionManager.HasSpell(MySpells.Redoublement.Name) && 
                (Ultima.LastSpell.Name != MySpells.Zwerchhau.Name ||
                Ultima.LastSpell.Name != MySpells.Riposte.Name))
                {
                    return await MySpells.Verstone.Cast();
                }
                else
                {
                    if (Ultima.LastSpell.Name != MySpells.Riposte.Name)
                    {
                        return await MySpells.Verstone.Cast();
                    }
                }
            }
            return false;
        }

        private async Task<bool> Verfire()
        {
            if (!ActionManager.HasSpell(MySpells.Redoublement.Name) &&
            !ActionManager.HasSpell(MySpells.Zwerchhau.Name))
            {
                return await MySpells.Verfire.Cast();
            }
            else
            {
                if (ActionManager.HasSpell(MySpells.Redoublement.Name) && 
                (Ultima.LastSpell.Name != MySpells.Zwerchhau.Name ||
                Ultima.LastSpell.Name != MySpells.Riposte.Name))
                {
                    return await MySpells.Verfire.Cast();
                }
                else
                {
                    if (Ultima.LastSpell.Name != MySpells.Riposte.Name)
                    {
                        return await MySpells.Verfire.Cast();
                    }
                }
            }
            return false;
        }

        private async Task<bool> Riposte()
        {
            if (!ActionManager.HasSpell(MySpells.Redoublement.Name) &&
            !ActionManager.HasSpell(MySpells.Zwerchhau.Name))
            {
                if (ActionResourceManager.RedMage.WhiteMana > 30 &&
                ActionResourceManager.RedMage.BlackMana > 30)
                {
                    return await MySpells.Riposte.Cast();
                }
            }
            else
            {
                if (ActionManager.HasSpell(MySpells.Redoublement.Name) && 
                (ActionResourceManager.RedMage.WhiteMana >= 80 &&
                ActionResourceManager.RedMage.BlackMana >= 80 &&
                ActionResourceManager.RedMage.WhiteMana != ActionResourceManager.RedMage.BlackMana) ||
                (ActionResourceManager.RedMage.WhiteMana == 100 &&
                ActionResourceManager.RedMage.BlackMana == 100))
                {
                    return await MySpells.Riposte.Cast();
                }
                else
                {
                    if (!ActionManager.HasSpell(MySpells.Redoublement.Name) &&
                    ActionResourceManager.RedMage.WhiteMana > 55 &&
                    ActionResourceManager.RedMage.BlackMana > 55)
                    {
                        return await MySpells.Riposte.Cast();
                    }
                }
            }
            return false;
        }

        private async Task<bool> Zwerchhau()
        {
            if (Ultima.LastSpell.Name == MySpells.Riposte.Name)
            {
                return await MySpells.Zwerchhau.Cast();
            }
            return false;
        }

        private async Task<bool> Redoublement()
        {
            if (Ultima.LastSpell.Name == MySpells.Zwerchhau.Name)
            {
                return await MySpells.Redoublement.Cast();
            }
            return false;
        }

        private async Task<bool> LucidDreaming()
        {
            if (Core.Player.CurrentManaPercent <= 50)
            {
                return await MySpells.CrossClass.LucidDreaming.Cast();
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

        #endregion
    }
}