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
            return await MySpells.Jolt.Cast();
        }

        private async Task<bool> Fleche()
        {
            if (ActionManager.LastSpell.Name != MySpells.Acceleration.Name)
            {
                return await MySpells.Fleche.Cast();
            }
            return false;
        }

        private async Task<bool> Acceleration()
        {
            if (ActionManager.LastSpell.Name != MySpells.Fleche.Name)
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
                return await MySpells.Verthunder.Cast();
            }
            return false;
        }

        private async Task<bool> Veraero()
        {
            if (Core.Player.HasAura("Dualcast") &&
            ActionResourceManager.RedMage.WhiteMana <= ActionResourceManager.RedMage.BlackMana)
            {
                return await MySpells.Veraero.Cast();
            }
            return false;
        }

        private async Task<bool> Verstone()
        {
            if (ActionManager.CanCast(MySpells.Verstone.Name,Core.Player.CurrentTarget))
            {
                return await MySpells.Verstone.Cast();
            }
            return false;
        }

        private async Task<bool> Verfire()
        {
            if (ActionManager.CanCast(MySpells.Verfire.Name,Core.Player.CurrentTarget))
            {
                return await MySpells.Verfire.Cast();
            }
            return false;
        }

        private async Task<bool> Riposte()
        {
            if (ActionResourceManager.RedMage.WhiteMana > 80 &&
            ActionResourceManager.RedMage.BlackMana > 80 &&
            ActionResourceManager.RedMage.WhiteMana != ActionResourceManager.RedMage.BlackMana)
            {
                return await MySpells.Riposte.Cast();
            }
            return false;
        }

        private async Task<bool> Zwerchhau()
        {
            if (ActionManager.LastSpell.Name == MySpells.Riposte.Name)
            {
                return await MySpells.Zwerchhau.Cast();
            }
            return false;
        }

        private async Task<bool> Redoublement()
        {
            if (ActionManager.LastSpell.Name == MySpells.Zwerchhau.Name)
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

        #endregion
    }
}