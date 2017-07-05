using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using ff14bot.Enums;
using System.Threading.Tasks;
using UltimaCR.Spells;
using UltimaCR.Spells.Main;

namespace UltimaCR.Rotations
{
    public sealed partial class Samurai
    {
        private SamuraiSpells _mySpells;

        private SamuraiSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new SamuraiSpells()); }
        }

        #region Class Spells

        private async Task<bool> Hakaze()
        {
                return await MySpells.Hakaze.Cast();
        }

        private async Task<bool> Jinpu()
        {
            if (ActionManager.LastSpell.Name == MySpells.Hakaze.Name)
            {
                if (/*(int)ActionResourceManager.Samurai.Sen != 2 || 
                (int)ActionResourceManager.Samurai.Sen != 3 || 
                (int)ActionResourceManager.Samurai.Sen != 6 &&*/
                !Core.Player.HasAura(MySpells.Jinpu.Name, true, 20000))
                {
                    return await MySpells.Jinpu.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Shifu()
        {
            if (ActionManager.LastSpell.Name == MySpells.Hakaze.Name)
            {
                if (/*(int)ActionResourceManager.Samurai.Sen != 4 || 
                (int)ActionResourceManager.Samurai.Sen != 5 || 
                (int)ActionResourceManager.Samurai.Sen != 6 &&*/
                !Core.Player.HasAura(MySpells.Shifu.Name, true, 20000))
                {
                    return await MySpells.Shifu.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Yukikaze()
        {
            if (ActionManager.LastSpell.Name == MySpells.Hakaze.Name &&
            (int)ActionResourceManager.Samurai.Sen == 6)
            {
                return await MySpells.Yukikaze.Cast();
            }
            return false;
        }

        private async Task<bool> Gekko()
	    {
            if (ActionManager.LastSpell.Name == MySpells.Jinpu.Name)
	        {
                return await MySpells.Gekko.Cast();
            }
            return false;
        }

        private async Task<bool> Kasha()
        {
            if (ActionManager.LastSpell.Name == MySpells.Shifu.Name)
	        {
                return await MySpells.Kasha.Cast();
            }
            return false;
        }

        private async Task<bool> Fuga()
        {
            if (Helpers.EnemiesNearTarget(8) > 4 && 
            Core.Player.HasAura(MySpells.Shifu.Name, true, 4500) &&
            Core.Player.HasAura(MySpells.Jinpu.Name, true, 4500))
            {
                return await MySpells.Fuga.Cast();
            }
            return false;
        }

        private async Task<bool> Mangetsu()
        {
            if (ActionManager.LastSpell.Name == MySpells.Fuga.Name && Helpers.EnemiesNearTarget(8) > 4)
            {
                    return await MySpells.Mangetsu.Cast();
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

        private async Task<bool> Oka()
        {
            if (ActionManager.LastSpell.Name == MySpells.Fuga.Name && Helpers.EnemiesNearTarget(8) > 4
            && (int)ActionResourceManager.Samurai.Sen == 2)
            {
                    return await MySpells.Oka.Cast();
            }
            return false;
        }

        private async Task<bool> Enpi()
        {
                return await MySpells.Enpi.Cast();
        }


        private async Task<bool> MeikyoShisui()
        {
                return await MySpells.MeikyoShisui.Cast();
        }

        private async Task<bool> Ageha()
        {
            if (ActionManager.CanCast(MySpells.Ageha.Name,Core.Player.CurrentTarget))
            {
                return await MySpells.Ageha.Cast();
            }
            return false;    
        }

        private async Task<bool> Iaijutsu()
        {
                    return await MySpells.Iaijutsu.Cast();
        }

        private async Task<bool> Higanbana()
        {
            if (ActionManager.CanCast(MySpells.Higanbana.Name,Core.Player.CurrentTarget) && 
            !Core.Player.CurrentTarget.HasAura(MySpells.Higanbana.Name, true, 4500))
            {
                return await MySpells.Higanbana.Cast();
            }
            return false;            
        }

        private async Task<bool> TenkaGoken()
        {
            if (ActionManager.CanCast(MySpells.TenkaGoken.Name,Core.Player.CurrentTarget) &&
            Helpers.EnemiesNearTarget(8) > 3)
            {
                    return await MySpells.TenkaGoken.Cast();
            }
            return false;
        }

        private async Task<bool> MidareSetsugekka()
        {
            if (ActionManager.CanCast(MySpells.MidareSetsugekka.Name,Core.Player.CurrentTarget))
            {
                return await MySpells.MidareSetsugekka.Cast();
            }
            return false;    
        }

        #endregion
    }
}