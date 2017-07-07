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
            if (!Core.Player.HasAura(MySpells.Kaiten.Name))
            {
                return await MySpells.Hakaze.Cast();
            }
            return false;
        }

        private async Task<bool> Jinpu()
        {
            if (ActionManager.LastSpell.Name == MySpells.Hakaze.Name)
            {
                if (/*(int)ActionResourceManager.Samurai.Sen != 2 || 
                (int)ActionResourceManager.Samurai.Sen != 3 || 
                (int)ActionResourceManager.Samurai.Sen != 6 &&*/
                !Core.Player.HasAura(MySpells.Jinpu.Name, true, 17000) &&
                !Core.Player.HasAura(MySpells.Kaiten.Name))
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
                !Core.Player.HasAura(MySpells.Shifu.Name, true, 17000) &&
                !Core.Player.HasAura(MySpells.Kaiten.Name))
                {
                    return await MySpells.Shifu.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Yukikaze()
        {
            if (ActionManager.LastSpell.Name == MySpells.Hakaze.Name &&
            ((int)ActionResourceManager.Samurai.Sen == 6 ||
            (Core.Player.HasAura(MySpells.Shifu.Name, true, 15000) &&
            Core.Player.HasAura(MySpells.Jinpu.Name, true, 15000) &&
            !Core.Player.CurrentTarget.HasAura("Slashing Resistance Down", true, 20000))) &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
            {
                return await MySpells.Yukikaze.Cast();
            }
            return false;
        }

        private async Task<bool> Gekko()
	    {
            if (ActionManager.LastSpell.Name == MySpells.Jinpu.Name &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
	        {
                return await MySpells.Gekko.Cast();
            }
            return false;
        }

        private async Task<bool> Kasha()
        {
            if (ActionManager.LastSpell.Name == MySpells.Shifu.Name &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
	        {
                return await MySpells.Kasha.Cast();
            }
            return false;
        }

        private async Task<bool> Fuga()
        {
            if (Helpers.EnemiesNearTarget(8) > 4 && 
            Core.Player.HasAura(MySpells.Shifu.Name, true, 4500) &&
            Core.Player.HasAura(MySpells.Jinpu.Name, true, 4500) &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
            {
                return await MySpells.Fuga.Cast();
            }
            return false;
        }

        private async Task<bool> Mangetsu()
        {
            if (ActionManager.LastSpell.Name == MySpells.Fuga.Name && 
            Helpers.EnemiesNearTarget(8) > 4 &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
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
            && (int)ActionResourceManager.Samurai.Sen == 2 &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
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
            if (ActionManager.CanCast(MySpells.Ageha.Name,Core.Player.CurrentTarget) &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
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
            !Core.Player.CurrentTarget.HasAura(MySpells.Higanbana.Name, true, 4500) &&
            ((int)ActionResourceManager.Samurai.Sen == 1 ||
            (int)ActionResourceManager.Samurai.Sen == 2 ||
            (int)ActionResourceManager.Samurai.Sen == 4) &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
            {
                return await MySpells.Higanbana.Cast();
            }
            return false;            
        }

        private async Task<bool> TenkaGoken()
        {
            if (ActionManager.CanCast(MySpells.TenkaGoken.Name,Core.Player.CurrentTarget) &&
            Helpers.EnemiesNearTarget(8) > 3 &&
            ((int)ActionResourceManager.Samurai.Sen == 3 ||
            (int)ActionResourceManager.Samurai.Sen == 6 ||
            (int)ActionResourceManager.Samurai.Sen == 5) &&
            !Core.Player.HasAura(MySpells.Kaiten.Name))
            {
                    return await MySpells.TenkaGoken.Cast();
            }
            return false;
        }

        private async Task<bool> MidareSetsugekka()
        {
            if (Core.Player.HasAura(MySpells.Kaiten.Name) ||
            (!ActionManager.CanCast(MySpells.Kaiten.Name,Core.Player) &&
            ActionManager.CanCast(MySpells.MidareSetsugekka.Name,Core.Player.CurrentTarget)))
            {
                return await MySpells.MidareSetsugekka.Cast();
            }
            return false;    
        }

        private async Task<bool> Kaiten()
        {
            if (ActionManager.CanCast(MySpells.MidareSetsugekka.Name,Core.Player.CurrentTarget))
            {
                return await MySpells.Kaiten.Cast();
            }
            return false;    
        }

        private async Task<bool> Goad()
        {
            if (PartyManager.IsInParty)
            {
                var target = Helpers.GoadManager.FirstOrDefault();

                if (target != null)
                {
                    return await MySpells.CrossClass.Goad.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> Bloodbath()
        {
            if (Core.Player.InCombat &&
            Core.Player.CurrentHealthPercent <= 75)
            {
                return await MySpells.CrossClass.Bloodbath.Cast();
            }
            return false;
        }

        private async Task<bool> Invigorate()
        {
            if (Core.Player.CurrentTP <= 440)
            {
                return await MySpells.CrossClass.Invigorate.Cast();
            }
            return false;
        }

        private async Task<bool> SecondWind()
        {
            if (Core.Player.CurrentHealthPercent <= 40)
            {
                return await MySpells.CrossClass.SecondWind.Cast();
            }
            return false;
        }

        private async Task<bool> MercifulEyes()
        {
            if (Core.Player.CurrentHealthPercent <= 80)
            {
                return await MySpells.MercifulEyes.Cast();
            }
            return false;
        }

        private async Task<bool> ThirdEye()
        {
            if (Core.Player.CurrentHealthPercent <= 80)
            {
                return await MySpells.ThirdEye.Cast();
            }
            return false;
        }

        private async Task<bool> Shinten()
        {
            if (ActionResourceManager.Samurai.Kenki >= 45 &&
            ActionManager.LastSpell.Name != MySpells.Shinten.Name)
            {
                return await MySpells.Shinten.Cast();
            }
            return false;
        }

        #endregion
    }
}