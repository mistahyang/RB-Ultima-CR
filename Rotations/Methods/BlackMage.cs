using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using System;
using System.Threading.Tasks;
using UltimaCR.Spells;
using UltimaCR.Spells.Main;

namespace UltimaCR.Rotations
{
    public sealed partial class BlackMage
    {
        private BlackMageSpells _mySpells;

        private BlackMageSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new BlackMageSpells()); }
        }

        #region Class Spells

        private async Task<bool> Blizzard()
        {
            if (!ActionManager.HasSpell(MySpells.Fire.Name) ||


		AstralAura &&
		!ActionManager.HasSpell(MySpells.Transpose.Name) &&
		Core.Player.CurrentManaPercent < 27 ||

		!UmbralAura &&
		Ultima.LastSpell.Name == MySpells.Blizzard.Name &&
		!ActionManager.HasSpell(MySpells.Transpose.Name) &&
		Core.Player.CurrentManaPercent < 27 ||

		UmbralAura &&
		!ActionManager.HasSpell(MySpells.Transpose.Name) &&
		Core.Player.CurrentManaPercent < 89 ||


		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		!AstralAura &&
		!UmbralAura &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		!AstralAura &&
		!UmbralAura &&
		Ultima.LastSpell.Name == MySpells.ThunderII.Name &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		AstralAura &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.Fire.Name) &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		UmbralAura &&
		ActionManager.HasSpell(MySpells.Fire.Name) &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		UmbralAura &&
		ActionManager.HasSpell(MySpells.Fire.Name) &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		!ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) &&
		Ultima.LastSpell.Name != MySpells.Transpose.Name &&
		Core.Player.HasAura("Umbral Ice II", true, 2800) &&
		ActionManager.HasSpell(MySpells.Fire.Name) &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent >= 89 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		Core.Player.HasAura("Umbral Ice", true, 2800) &&
		!Core.Player.InCombat &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		Core.Player.HasAura("Umbral Ice", true, 2500) &&
		!Core.Player.InCombat &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true) &&
		Core.Player.CurrentManaPercent >= 89 ||



		!AstralAura &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		UmbralAura &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		UmbralAura &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		!Core.Player.InCombat &&
		!ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent >= 89 ||

		UmbralAura &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		UmbralAura &&
		Ultima.LastSpell.Name == MySpells.Blizzard.Name &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent < 89 ||

		UmbralAura &&
		!Core.Player.InCombat &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		!ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) &&
		ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		Core.Player.CurrentManaPercent >= 89 ||
		

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		Core.Player.HasAura("Umbral Ice", true, 2800) &&
		!Core.Player.InCombat &&
		Ultima.LastSpell.Name == MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		!Core.Player.HasAura("Firestarter") &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) &&
		Core.Player.CurrentManaPercent < 89 ||



		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&	
		UmbralAura &&
		ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true) &&
		Core.Player.CurrentManaPercent < 89 ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.Transpose.Name &&
		ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 6000) &&
		Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) &&
		Core.Player.CurrentManaPercent < 89)

            {
                return await MySpells.Blizzard.Cast();
            }
            return false;
        }

        private async Task<bool> Fire()
        {
            if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		Ultima.LastSpell.Name == MySpells.FireIII.Name &&
		!AstralAura &&
		!UmbralAura &&
		!Core.Player.HasAura(MySpells.Enochian.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		!AstralAura &&
		!UmbralAura &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth / 2 &&
		!AstralAura &&
		!UmbralAura &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||

		!AstralAura &&
		!UmbralAura &&
		Ultima.LastSpell.Name == MySpells.ThunderII.Name &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||

		!AstralAura &&
		!UmbralAura &&
		Ultima.LastSpell.Name == MySpells.Thunder.Name &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||

		UmbralAura &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) &&
		Core.Player.CurrentManaPercent >= 89 ||

		AstralAura &&
		!ActionManager.HasSpell(MySpells.FireIII.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||


		UmbralAura &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.InCombat &&
		Core.Player.CurrentManaPercent >= 89 ||


		AstralAura &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||

		Ultima.LastSpell.Name != MySpells.Fire.Name &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name	&&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name != MySpells.LeyLines.Name &&
		AstralAura &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Ultima.LastSpell.Name == MySpells.FireIV.Name &&
		!Core.Player.HasAura(175, false, 6000) &&
		Core.Player.CurrentManaPercent >= 25 ||

		
		Ultima.LastSpell.Name != MySpells.Fire.Name &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name	&&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name != MySpells.LeyLines.Name &&
		AstralAura &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Ultima.LastSpell.Name == MySpells.Fire.Name &&
		!Core.Player.HasAura(175, false, 6000) &&
		Core.Player.CurrentManaPercent >= 25 ||

		UmbralAura &&
		Ultima.LastSpell.Name == MySpells.FireIII.Name &&
		!Core.Player.InCombat &&
		ActionManager.HasSpell(MySpells.FireIII.Name) &&
		!ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||


		AstralAura &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		!Core.Player.HasAura(MySpells.Enochian.Name) &&
		Core.Player.CurrentManaPercent >= 25 ||

		Ultima.LastSpell.Name != MySpells.Fire.Name &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name	&&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name != MySpells.LeyLines.Name &&
	 	AstralAura &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		!Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura("Firestarter") &&
		!Core.Player.HasAura(175, false, 6000) &&
		Core.Player.CurrentManaPercent >= 25 ||

		Ultima.LastSpell.Name != MySpells.Fire.Name &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name	&&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name != MySpells.LeyLines.Name &&
		AstralAura &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura("Firestarter") &&
		!Core.Player.HasAura(175, false, 6500) &&
		Core.Player.CurrentManaPercent >= 25 ||

		Ultima.LastSpell.Name != MySpells.Fire.Name &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name	&&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name != MySpells.LeyLines.Name &&
		AstralAura &&
		ActionManager.CanCast(MySpells.Enochian.Name, Core.Player) &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 4000) &&
		!Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura("Firestarter") &&
		!Core.Player.HasAura(175, false, 6000) &&
		Core.Player.CurrentManaPercent >= 25 ||

		Ultima.LastSpell.Name != MySpells.Fire.Name &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.RagingStrikes.Name	&&	
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name != MySpells.LeyLines.Name &&
		AstralAura &&
		ActionManager.CanCast(MySpells.Enochian.Name, Core.Player) &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 4000) &&
		Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura("Firestarter") &&
		!Core.Player.HasAura(175, false, 6000) &&
		Core.Player.CurrentManaPercent >= 25)
            {
                return await MySpells.Fire.Cast();
            }
            return false;
        }

        private async Task<bool> Transpose()
        {
            if (AstralAura)
            {
                if (!Core.Player.HasTarget &&
		    !Core.Player.InCombat &&
		    !Core.Player.HasAura("Astral Fire", false, 2800) &&
		    !Core.Player.HasAura("Astral Fire II", false, 2800) &&
		    !Core.Player.HasAura("Astral Fire III", false, 2800) &&
		    Core.Player.CurrentManaPercent <= 100 ||

		    !Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Helpers.EnemiesNearPlayer(30) < 1 &&
		    !Core.Player.HasAura("Astral Fire", false, 2800) &&
		    !Core.Player.HasAura("Astral Fire II", false, 2800) &&
		    !Core.Player.HasAura("Astral Fire III", false, 2800) &&
		    Core.Player.CurrentManaPercent <= 100 ||

		    !Core.Player.HasTarget &&
		    !Core.Player.InCombat &&
		    Helpers.EnemiesNearPlayer(30) < 1 &&
		    Core.Player.CurrentManaPercent < 60)
                {
                    return await MySpells.Transpose.Cast();
                }
            }
            if (UmbralAura)
            {
                if (!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		    Core.Player.HasAura("Umbral Ice") &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentManaPercent > 89 &&
		    ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) ||

		    !ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		    Core.Player.HasAura("Umbral Ice") &&
		    Core.Player.HasTarget &&
		    !Core.Player.InCombat &&
		    Core.Player.CurrentManaPercent > 89 &&
		    ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) ||



		    !ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		    Core.Player.HasAura("Umbral Ice II") &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentManaPercent > 89 &&
		    ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) ||

		    !ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		    Core.Player.HasAura("Umbral Ice II") &&
		    Core.Player.HasTarget &&
		    !Core.Player.InCombat &&
		    Core.Player.CurrentManaPercent > 89 &&
		    ActionManager.CanCast(MySpells.Transpose.Name, Core.Player) ||


		    
		    Core.Player.HasAura("Firestarter") &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		    !ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		    Core.Player.CurrentManaPercent > 89 ||
		
		    Core.Player.HasAura("Firestarter") &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		    !ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 6500) &&
		    Core.Player.CurrentManaPercent > 89 ||

		    Core.Player.HasAura("Firestarter") &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		    ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		    Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		    Core.Player.CurrentManaPercent > 89 ||
		
		    Core.Player.HasAura("Firestarter") &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		    ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		    Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) &&
		    Core.Player.CurrentManaPercent > 89 ||

		    Core.Player.HasAura("Firestarter") &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		    ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		    !Core.Player.HasAura(MySpells.Enochian.Name) &&
		    Core.Player.CurrentManaPercent > 89 ||
		
		    Core.Player.HasAura("Firestarter") &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		    ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		    !Core.Player.HasAura(MySpells.Enochian.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) &&
		    Core.Player.CurrentManaPercent > 89)

                {
                    return await MySpells.Transpose.Cast();
                }
	    }
            if (ActionManager.HasSpell(MySpells.Fire.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
                Core.Player.CurrentManaPercent < 27)
            {
                return await MySpells.Transpose.Cast();
            }
            return false;
        }

        private async Task<bool> Thunder()
        {
	    if (Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 3.5 &&
		!ActionManager.HasSpell(MySpells.ThunderII.Name) &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!AstralAura &&
		!UmbralAura &&
		Ultima.LastSpell.Name != MySpells.Thunder.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) ||

		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 3.5 &&
		!ActionManager.HasSpell(MySpells.ThunderII.Name) &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.Thunder.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) ||

		!ActionManager.HasSpell(MySpells.ThunderII.Name) &&
		Core.Player.HasAura("Thundercloud"))
            {
                return await MySpells.Thunder.Cast();
            }
            return false;
        }

        private async Task<bool> Surecast()
        {
            return await MySpells.Surecast.Cast();
        }

        private async Task<bool> Sleep()
        {
            return await MySpells.Sleep.Cast();
        }

        private async Task<bool> BlizzardII()
        {
            return await MySpells.BlizzardII.Cast();
        }

        private async Task<bool> Scathe()
        {
            if (Core.Player.CurrentTarget.CurrentHealth < Core.Player.MaxHealth / 8 &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 20 &&
		Core.Player.InCombat &&
                Core.Player.CurrentManaPercent >= 34 &&
                !Core.Player.HasAura(MySpells.Swiftcast.Name) ||

		MovementManager.IsMoving &&
		Core.Player.InCombat &&
		Ultima.LastSpell.Name != MySpells.ThunderIII.Name &&
		ActionManager.HasSpell(MySpells.Sharpcast.Name) &&
		Core.Player.HasAura(MySpells.Sharpcast.Name) &&		
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		!Core.Player.HasAura("Thundercloud") &&
		!Core.Player.HasAura("Firestarter") &&
		Core.Player.CurrentManaPercent > 34 ||

		MovementManager.IsMoving &&
		Core.Player.InCombat &&
		Ultima.LastSpell.Name != MySpells.ThunderIII.Name &&
		ActionManager.HasSpell(MySpells.Sharpcast.Name) &&
		Core.Player.HasAura(MySpells.Sharpcast.Name) &&			
		!Core.Player.HasAura(MySpells.Enochian.Name) &&
		!Core.Player.HasAura("Thundercloud") &&
		!Core.Player.HasAura("Firestarter") &&
		Core.Player.CurrentManaPercent > 34)
            {
                return await MySpells.Scathe.Cast();
            }
            return false;
        }

        private async Task<bool> FireII()
        {
            if (AstralAura &&
		Helpers.EnemiesNearTarget(5) > 2 &&
		Core.Player.CurrentManaPercent >= 25 &&
		!Core.Player.HasAura(MySpells.Enochian.Name))
            {
                return await MySpells.FireII.Cast();
            }
            return false;
        }

        private async Task<bool> ThunderII()
        {
	    if (Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 3.5 &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		!AstralAura &&
		!UmbralAura &&
		Ultima.LastSpell.Name != MySpells.ThunderII.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) ||

		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 3.5 &&
		ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.ThunderII.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) ||

		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 3.5 &&
		ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 8000) &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.ThunderII.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) ||

		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 3.5 &&
		ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		!Core.Player.HasAura(MySpells.Enochian.Name) &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.ThunderII.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) ||

		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 3.5 &&
		ActionManager.HasSpell(MySpells.ThunderIII.Name) &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		UmbralAura &&
		Ultima.LastSpell.Name != MySpells.ThunderII.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.Thunder.Name, true, 11500) ||

		!ActionManager.HasSpell(MySpells.ThunderIII.Name) &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		Core.Player.HasAura("Thundercloud"))
            {
                return await MySpells.ThunderII.Cast();
            }
            return false;
        }

        private async Task<bool> Swiftcast()
        {
            if (MovementManager.IsMoving &&
		ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		ActionManager.CanCast(MySpells.BlizzardIV.Name, Core.Player.CurrentTarget) &&
		UmbralAura &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		!Core.Player.HasAura(MySpells.Enochian.Name, true, 9650))
	    {
                return await MySpells.Swiftcast.Cast();
            }
            return false;
        }

        private async Task<bool> Manaward()
        {
            if (Core.Player.InCombat &&
		Core.Player.CurrentHealthPercent <= 40 ||
		
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 70)
            {
                return await MySpells.Manaward.Cast();
            }
            return false;
        }

        private async Task<bool> FireIII()
        {
            if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		!AstralAura &&
		!UmbralAura &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.Sharpcast.Name &&
		Core.Player.CurrentManaPercent >= 60 ||

		UmbralAura &&
		!Core.Player.InCombat &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Ultima.LastSpell.Name != MySpells.Transpose.Name &&
		Core.Player.CurrentManaPercent >= 89 ||

		UmbralAura &&
		Core.Player.InCombat &&
		!ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Ultima.LastSpell.Name != MySpells.Transpose.Name &&
		Core.Player.CurrentManaPercent >= 89 ||

		UmbralAura &&
		!Core.Player.InCombat &&
		ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Ultima.LastSpell.Name != MySpells.FireIII.Name &&
		Core.Player.CurrentManaPercent >= 89 ||

		UmbralAura &&
		Core.Player.InCombat &&
		ActionManager.HasSpell(MySpells.BlizzardIII.Name) &&
		Core.Player.CurrentManaPercent >= 89)
	    {
                return await MySpells.FireIII.Cast();
            }
            return false;
        }

	private async Task<bool> BlizzardIII()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		!UmbralAura &&
		!AstralAura &&
		!Core.Player.InCombat &&
		Ultima.LastSpell.Name != MySpells.BlizzardIII.Name ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2.5 &&
		!AstralAura &&
		!UmbralAura &&
		Core.Player.InCombat &&
		Ultima.LastSpell.Name != MySpells.BlizzardIII.Name &&
		Core.Player.CurrentManaPercent < 60 ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2.5 &&
		!UmbralAura &&
		!AstralAura &&
		Core.Player.InCombat &&
		Ultima.LastSpell.Name != MySpells.BlizzardIII.Name ||

		!ActionManager.HasSpell(MySpells.FireIV.Name) &&
		AstralAura &&
		Core.Player.CurrentManaPercent < 28 ||

		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		AstralAura &&
		Core.Player.CurrentManaPercent < 28 ||
		
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		AstralAura &&
		ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name) &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 9650))

            {
                return await MySpells.BlizzardIII.Cast();
            }
            return false;
        }

        private async Task<bool> Lethargy()
        {
            return await MySpells.Lethargy.Cast();
        }

        private async Task<bool> ThunderIII()
        {
            return await MySpells.ThunderIII.Cast();
        }

        private async Task<bool> AetherialManipulation()
        {
            return await MySpells.AetherialManipulation.Cast();
        }

        #endregion

        #region Cross Class Spells

        #region Arcanist

        private async Task<bool> Ruin()
        {
            if (Ultima.UltSettings.BlackMageRuin)
            {
                return await MySpells.CrossClass.Ruin.Cast();
            }
            return false;
        }

        private async Task<bool> Physick()
        {
            if (Ultima.UltSettings.BlackMagePhysick)
            {
                return await MySpells.CrossClass.Physick.Cast();
            }
            return false;
        }

        private async Task<bool> Virus()
        {
            if (Ultima.UltSettings.BlackMageVirus)
            {
                return await MySpells.CrossClass.Virus.Cast();
            }
            return false;
        }

        private async Task<bool> EyeForAnEye()
        {
            if (Ultima.UltSettings.BlackMageEyeForAnEye)
            {
                return await MySpells.CrossClass.EyeForAnEye.Cast();
            }
            return false;
        }

        #endregion

        #region Archer

        private async Task<bool> RagingStrikes()
        {
            if (Ultima.UltSettings.BlackMageRagingStrikes &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 4 &&
		!MovementManager.IsMoving &&
		Core.Player.HasTarget &&
		Core.Player.InCombat ||
		Ultima.UltSettings.BlackMageRagingStrikes &&
		!MovementManager.IsMoving &&
		Helpers.EnemiesNearTarget(6) > 1)
            {
                if (AstralAura &&
                    Core.Player.CurrentManaPercent >= 60)
                {
                    return await MySpells.CrossClass.RagingStrikes.Cast();
                }
            }
            return false;
        }

        private async Task<bool> HawksEye()
        {
            if (Ultima.UltSettings.BlackMageHawksEye)
            {
                return await MySpells.CrossClass.HawksEye.Cast();
            }
            return false;
        }

        private async Task<bool> QuellingStrikes()
        {
            if (Ultima.UltSettings.BlackMageQuellingStrikes)
            {
                return await MySpells.CrossClass.QuellingStrikes.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> Freeze()
        {
            return await MySpells.Freeze.Cast();
        }

        private async Task<bool> Apocatastasis()
        {
            return await MySpells.Apocatastasis.Cast();
        }

        private async Task<bool> Manawall()
        {
            if (Core.Player.InCombat &&
		Core.Player.CurrentHealthPercent <= 40 ||
		
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 70)
            {
                return await MySpells.Manawall.Cast();
            }
            return false;
        }


        private async Task<bool> Flare()
        {
            return await MySpells.Flare.Cast();
        }

        private async Task<bool> LeyLines()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 4 &&
		!MovementManager.IsMoving &&
		!UmbralAura &&
		Core.Player.HasTarget ||

		!MovementManager.IsMoving &&
		!UmbralAura &&
		Core.Player.HasTarget &&
		Helpers.EnemiesNearTarget(6) > 1)
	    {
                return await MySpells.LeyLines.Cast();
            }
            return false;
        }

        private async Task<bool> Sharpcast()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name == MySpells.FireIV.Name &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 20000) &&
		!Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura(175, false, 7000) &&
		Core.Player.CurrentManaPercent >= 79 ||

		!MovementManager.IsMoving &&
		Ultima.LastSpell.Name == MySpells.FireIV.Name &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 20000) &&
		Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura(175, false, 6000) &&
		Core.Player.CurrentManaPercent >= 79 ||
		
		MovementManager.IsMoving &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 12000) ||

		MovementManager.IsMoving &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		!Core.Player.HasAura(MySpells.Enochian.Name) ||

		!MovementManager.IsMoving &&
		Core.Player.HasAura("Thundercloud") &&
		Ultima.LastSpell.Name == MySpells.Fire.Name &&
		Core.Player.InCombat &&
		Core.Player.HasTarget &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		Core.Player.HasAura(175, true, 6000))
	    {
                return await MySpells.Sharpcast.Cast();
            }
            return false;
        }

        private async Task<bool> Enochian()
        {
            if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2 &&
		!UmbralAura &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 14250) &&
		Helpers.EnemiesNearTarget(6) > 1 ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Ultima.LastSpell.Name == MySpells.ThunderII.Name &&
		UmbralAura &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 14250) ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Ultima.LastSpell.Name == MySpells.ThunderIII.Name &&
		UmbralAura &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 14250) ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		Ultima.LastSpell.Name == MySpells.Blizzard.Name &&
		UmbralAura &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 14250) ||

		Ultima.LastSpell.Name != MySpells.BlizzardIV.Name &&
		Core.Player.HasAura(MySpells.Enochian.Name) &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 14250))
            {
                return await MySpells.Enochian.Cast();
            }
            return false;
        }

        private async Task<bool> BlizzardIV()
        {
            if (Ultima.LastSpell.Name != MySpells.BlizzardIV.Name &&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 9650) ||

		Ultima.LastSpell.Name != MySpells.BlizzardIV.Name &&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name == MySpells.Blizzard.Name &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 13650) ||

		Ultima.LastSpell.Name != MySpells.BlizzardIV.Name &&
		Ultima.LastSpell.Name != MySpells.Enochian.Name &&
		Ultima.LastSpell.Name == MySpells.ThunderII.Name &&
		!Core.Player.HasAura(MySpells.Enochian.Name, false, 13650))
		
            {
                if (MovementManager.IsMoving &&
		    ActionManager.CanCast(MySpells.BlizzardIV.Name, Core.Player.CurrentTarget) &&
		    UmbralAura &&
		    Core.Player.HasAura(MySpells.Enochian.Name) &&
		    !Core.Player.HasAura(MySpells.Enochian.Name, false, 9650))
                {
                    if (await MySpells.Swiftcast.Cast())
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(167));
                    }
                }
                return await MySpells.BlizzardIV.Cast();
            }
            return false;
        }

        private async Task<bool> FireIV()
        {
            if (Core.Player.CurrentManaPercent >= 25 &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		!Core.Player.HasAura("Firestarter") &&
		Core.Player.HasAura(175, true, 6000) ||

		Core.Player.CurrentManaPercent >= 25 &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		Core.Player.HasAura("Firestarter") &&
		Core.Player.HasAura(175, true, 4000) ||



		ActionManager.CanCast(MySpells.Enochian.Name, Core.Player) &&
		Core.Player.CurrentManaPercent >= 25 &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 6000) &&
		!Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura("Firestarter") &&
		Core.Player.HasAura(175, true, 6000) ||

		ActionManager.CanCast(MySpells.Enochian.Name, Core.Player) &&
		Core.Player.CurrentManaPercent >= 25 &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 6000) &&
		!Core.Player.HasAura(MySpells.LeyLines.Name) &&
		Core.Player.HasAura("Firestarter") &&
		Core.Player.HasAura(175, true, 4000) ||



		ActionManager.CanCast(MySpells.Enochian.Name, Core.Player) &&
		Core.Player.CurrentManaPercent >= 25 &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 6000) &&
		Core.Player.HasAura(MySpells.LeyLines.Name) &&
		!Core.Player.HasAura("Firestarter") &&
		Core.Player.HasAura(175, true, 5000) ||

		ActionManager.CanCast(MySpells.Enochian.Name, Core.Player) &&
		Core.Player.CurrentManaPercent >= 25 &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 6000) &&
		Core.Player.HasAura(MySpells.LeyLines.Name) &&
		Core.Player.HasAura("Firestarter") &&
		Core.Player.HasAura(175, true, 3000))
            {
                return await MySpells.FireIV.Cast();
            }
            return false;
        }

        #endregion

        #region Custom Spells

        private static bool UmbralAura
        {
            get
            {
                return (Core.Player.HasAura("Umbral Ice") ||
                       Core.Player.HasAura("Umbral Ice II") ||
                       Core.Player.HasAura("Umbral Ice III"));
            }
        }

        private static bool AstralAura
        {
            get
            {
                return (Core.Player.HasAura("Astral Fire") ||
                       Core.Player.HasAura("Astral Fire II") ||
                       Core.Player.HasAura("Astral Fire III"));
            }
        }

        private static bool LowMP
        {
            get
            {
                return (Core.Player.CurrentManaPercent <= 32 &&
                    (Ultima.UltSettings.SmartTarget &&
                    Helpers.EnemiesNearTarget(5) >= 3 ||
                    Ultima.UltSettings.MultiTarget) ||
                    Core.Player.CurrentManaPercent <= 27);
            }
        }

        private async Task<bool> Thundercloud()
        {
            if (Core.Player.HasAura("Thundercloud"))
            {
                if (!UmbralAura)
                {
                    if (Ultima.LastSpell.Name == MySpells.Fire.Name ||

			!ActionManager.CanCast(MySpells.Sharpcast.Name, Core.Player) &&
			ActionManager.HasSpell(MySpells.Sharpcast.Name) &&
			!ActionManager.HasSpell(MySpells.BlizzardIV.Name) ||

			!ActionManager.CanCast(MySpells.Sharpcast.Name, Core.Player) &&
			ActionManager.HasSpell(MySpells.Sharpcast.Name) &&
			!ActionManager.HasSpell(MySpells.FireIV.Name) &&
                        !Core.Player.HasAura("Thundercloud", false, 5000) ||

			!ActionManager.CanCast(MySpells.Sharpcast.Name, Core.Player) &&
			ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
			!ActionManager.HasSpell(MySpells.FireIV.Name) &&
			Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
			!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
			Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) ||

			!ActionManager.CanCast(MySpells.Sharpcast.Name, Core.Player) &&
			ActionManager.HasSpell(MySpells.FireIV.Name) &&
			Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
			!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
			!Core.Player.HasAura(MySpells.LeyLines.Name) &&
			Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
			Core.Player.HasAura(175, true, 6000) ||

			!ActionManager.CanCast(MySpells.Sharpcast.Name, Core.Player) &&
			ActionManager.HasSpell(MySpells.FireIV.Name) &&
			Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
			!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
			Core.Player.HasAura(MySpells.LeyLines.Name) &&
			Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
			Core.Player.HasAura(175, true, 5000) ||
			
			MovementManager.IsMoving &&
			!ActionManager.CanCast(MySpells.Sharpcast.Name, Core.Player) &&
			Core.Player.HasAura(MySpells.Sharpcast.Name))
			
                    {
                        if (!ActionManager.HasSpell(MySpells.ThunderIII.Name))
                        {
                            return await MySpells.ThunderII.Cast();
                        }
                        return await MySpells.ThunderIII.Cast();
                    }
                }
            }
            return false;
        }

        private async Task<bool> Firestarter()
        {
            if (!UmbralAura &&
                Core.Player.HasAura("Firestarter") &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		Core.Player.CurrentManaPercent > 89 ||

		!ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		!UmbralAura &&
                Core.Player.HasAura("Firestarter") &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) ||

		ActionManager.HasSpell(MySpells.BlizzardIV.Name) &&
		!ActionManager.HasSpell(MySpells.FireIV.Name) &&
		!UmbralAura &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
                Core.Player.HasAura("Firestarter") &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) ||

		AstralAura &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		!Core.Player.HasAura(MySpells.Enochian.Name) &&
		Core.Player.HasAura("Firestarter") &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) ||

		AstralAura &&
		Ultima.LastSpell.Name != MySpells.Fire.Name &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		Core.Player.HasAura("Firestarter") &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		!Core.Player.HasAura(175, false, 3000) &&
		Core.Player.CurrentManaPercent > 89 ||

		AstralAura &&
		ActionManager.HasSpell(MySpells.FireIV.Name) &&
		Core.Player.HasAura(MySpells.Enochian.Name, true, 9650) &&
		Core.Player.HasAura("Firestarter") &&
		Ultima.LastSpell.Name != MySpells.Swiftcast.Name &&
		!Core.Player.HasAura(MySpells.Swiftcast.Name) &&
		Core.Player.CurrentManaPercent < 89)
            {
                if (await MySpells.FireIII.Cast())
                {
                    await Coroutine.Wait(5000, () => AstralAura);
                    return true;
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

        private async Task<bool> Equanimity()
        {
            return await MySpells.PvP.Equanimity.Cast();
        }

        private async Task<bool> ManaDraw()
        {
            return await MySpells.PvP.ManaDraw.Cast();
        }

        private async Task<bool> NightWing()
        {
            return await MySpells.PvP.NightWing.Cast();
        }

        private async Task<bool> PhantomDart()
        {
            return await MySpells.PvP.PhantomDart.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }
        #endregion
    }
}