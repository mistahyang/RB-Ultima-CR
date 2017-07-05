using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using System.Threading.Tasks;
using UltimaCR.Spells.Main;

namespace UltimaCR.Rotations
{
    public sealed partial class Summoner
    {
        private SummonerSpells _mySpells;

        private SummonerSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new SummonerSpells()); }
        }

        #region Class Spells

        private async Task<bool> Ruin()
        {
            if (!ActionManager.HasSpell(MySpells.RuinIII.Name) ||

		Core.Player.CurrentTarget.CurrentHealth <= Core.Player.CurrentHealth * 2 &&
		ActionManager.HasSpell(MySpells.RuinIII.Name) &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Core.Player.CurrentManaPercent < 40 && 
		!Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth * 2 &&
                Core.Player.HasAura(190, false, 5500) &&
		ActionManager.HasSpell(MySpells.RuinIII.Name) &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Core.Player.CurrentManaPercent < 40 && 
		!Core.Player.HasAura(MySpells.DreadwyrmTrance.Name))
            {
                return await MySpells.Ruin.Cast();
            }
            return false;
        }

        private async Task<bool> Bio()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.CurrentHealth * .33 &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.Bio.Name)
		
            {
                if (!ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4000) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    !ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4150) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name, true, 5000) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4150) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		    ActionManager.CanCast(MySpells.Tridisaster.Name, Core.Player) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 9000)) 
                {
                    return await MySpells.Bio.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Summon()
        {
            if (Core.Player.Pet == null &&
		Ultima.LastSpell.Name != MySpells.Summon.Name &&
                Ultima.UltSettings.SummonerSummonPet &&
		Ultima.UltSettings.SummonerGaruda ||

		Core.Player.Pet == null &&
                Ultima.UltSettings.SummonerSummonPet &&
		Ultima.LastSpell.Name != MySpells.Summon.Name &&
                !ActionManager.HasSpell(MySpells.SummonII.Name) &&
                Ultima.UltSettings.SummonerTitan ||
		
		Core.Player.Pet == null &&
                Ultima.UltSettings.SummonerSummonPet &&
		Ultima.LastSpell.Name != MySpells.Summon.Name &&
                !ActionManager.HasSpell(MySpells.SummonIII.Name) &&
                Ultima.UltSettings.SummonerIfrit)
		
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
            if (Ultima.UltSettings.SummonerPhysick)
            {
                if (Core.Player.InCombat &&
		    Core.Player.CurrentHealthPercent < 60)
                {
                    return await MySpells.Physick.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Aetherflow()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||

		MovementManager.IsMoving)
            {
                if (!Core.Player.HasAura(MySpells.Aetherflow.Name) ||
		    !Core.Player.HasAura(MySpells.Aetherflow.Name) &&
		    !Core.Player.HasTarget &&
		    Core.Player.InCombat)  
			
                {
                    return await MySpells.Aetherflow.Cast();
		}
            }
            return false;
        }	

        private async Task<bool> EnergyDrain()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name &&
		!ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		Core.Player.HasAura(MySpells.Aetherflow.Name) ||

		ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		Core.Player.HasAura(MySpells.Aetherflow.Name) ||

		MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		!ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		Core.Player.HasAura(MySpells.Aetherflow.Name))
            {
                if (!ActionManager.HasSpell(MySpells.Fester.Name) &&
          	    Core.Player.CurrentManaPercent <= 90 ||

                    ActionManager.HasSpell(MySpells.Fester.Name) &&
		    Core.Player.CurrentManaPercent < 50)  
			
                {
                    return await MySpells.EnergyDrain.Cast();
		}
            }
            return false;
        }	

        private async Task<bool> Miasma()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * .66 &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.Miasma.Name)
		
            {
                if (!ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 5000) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    !ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 5250) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 5250))  
			
                {
                    return await MySpells.Miasma.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Virus()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Painflare.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||

		MovementManager.IsMoving)
	    {
            	return await MySpells.Virus.Cast();
            }
            return false;
        }	

        private async Task<bool> SummonII()
        {
            if (Core.Player.Pet == null &&
		Ultima.LastSpell.Name != MySpells.SummonII.Name &&
                Ultima.UltSettings.SummonerSummonPet &&
                Ultima.UltSettings.SummonerTitan)
		
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
		Ultima.LastSpell.Name != MySpells.Sustain.Name &&
                !Core.Player.Pet.HasAura(MySpells.Sustain.Name))
            {
                return await MySpells.Sustain.Cast();
            }
            return false;
        }

        private async Task<bool> Resurrection()
	{
            return await MySpells.Resurrection.Cast();
        }

        private async Task<bool> BioII()
	{
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.BioII.Name)
		
            {
                if (!ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 5000) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    !ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 5250) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 5250))    
                {
                    return await MySpells.BioII.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Bane()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Painflare.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||
		
		MovementManager.IsMoving)
            {
                if (Core.Player.HasAura(MySpells.Aetherflow.Name) &&
                    Core.Player.CurrentManaPercent >= 40 &&
		    Helpers.EnemiesNearTarget(8) > 1 &&
                    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 8000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 8000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 5000))  
			
                {
                    return await MySpells.Bane.Cast();
		}
            }
            return false;
        }

        private async Task<bool> EyeForAnEye()
        {
            return await MySpells.EyeForAnEye.Cast();
        }

        private async Task<bool> RuinII()
        {
            if (Ultima.LastSpell.Name == MySpells.Tridisaster.Name &&
		!Core.Player.InCombat)
            {
                return await MySpells.RuinII.Cast();
            }
            if (MovementManager.IsMoving &&
		Core.Player.CurrentManaPercent > 30 &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		!Core.Player.HasAura(MySpells.DreadwyrmTrance.Name))
            {
                if (ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player) &&
                    !Core.Player.HasAura(MySpells.Aetherflow.Name) ||
                    ActionManager.CanCast(MySpells.Fester.Name, Core.Player.CurrentTarget) &&
                    Helpers.EnemiesNearTarget(8) <= 1 &&
                    Ultima.UltSettings.SmartTarget ||
                    ActionManager.CanCast(MySpells.Fester.Name, Core.Player.CurrentTarget) &&
                    Ultima.UltSettings.SingleTarget ||
                    ActionManager.CanCast(MySpells.Bane.Name, Core.Player.CurrentTarget) &&
                    !Ultima.UltSettings.SingleTarget &&
                    Helpers.EnemiesNearTarget(8) > 1 &&
                    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 8000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 8000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 5000) ||
                    ActionManager.CanCast(MySpells.Rouse.Name, Core.Player) ||
                    ActionManager.CanCast(MySpells.Spur.Name, Core.Player))
                {
                    return await MySpells.RuinII.Cast();
                }
            }
            return false;
        }

	private async Task<bool> RuinIIGCD()
        {
            if (Ultima.LastSpell.Name == MySpells.Tridisaster.Name &&
		!Core.Player.InCombat)
            {
                return await MySpells.RuinII.Cast();
            }
            return false;
        }

        private async Task<bool> Rouse()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Painflare.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||
		
		MovementManager.IsMoving)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth)  
			
                {
                    return await MySpells.Rouse.Cast();
		}
            }
            return false;
        }

        private async Task<bool> MiasmaII()
        {
            if (Helpers.EnemiesNearPlayer(5) > 2 &&
                !Core.Player.CurrentTarget.HasAura(MySpells.MiasmaII.Name, true, 3000))
            {
                return await MySpells.MiasmaII.Cast();
            }
            return false;
        }

        private async Task<bool> ShadowFlare()
        {
            if (Ultima.LastSpell.Name != MySpells.ShadowFlare.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) &&
                    !Core.Player.HasAura(190, false, 5250) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 6000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 6000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4000) ||
 
		    Ultima.LastSpell.Name != MySpells.ShadowFlare.Name &&
		    Helpers.EnemiesNearTarget(5) > 2 &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) &&
                    !Core.Player.HasAura(190, false, 5250))
                {
                    return await MySpells.ShadowFlare.Cast();
		}
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Archer

        private async Task<bool> RagingStrikes()
        {
            if (!MovementManager.IsMoving &&
		Ultima.UltSettings.SummonerRagingStrikes &&
		Core.Player.InCombat &&
		Core.Player.HasTarget)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) ||
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) ||
		    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name) ||
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name) ||
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name)) 
			
                {
                    return await MySpells.CrossClass.RagingStrikes.Cast();
		}
            }
            return false;
        }

        private async Task<bool> HawksEye()
        {
            if (Ultima.UltSettings.SummonerHawksEye)
            {
                return await MySpells.CrossClass.HawksEye.Cast();
            }
            return false;
        }

        private async Task<bool> QuellingStrikes()
        {
            if (Ultima.UltSettings.SummonerQuellingStrikes)
            {
                return await MySpells.CrossClass.QuellingStrikes.Cast();
            }
            return false;
        }

        #endregion

        #region Thaumaturge

        private async Task<bool> Surecast()
        {
            if (Ultima.UltSettings.SummonerSurecast)
            {
                return await MySpells.CrossClass.Surecast.Cast();
            }
            return false;
        }

        private async Task<bool> BlizzardII()
        {
            if (Ultima.UltSettings.SummonerBlizzardII)
            {
                return await MySpells.CrossClass.BlizzardII.Cast();
            }
            return false;
        }

        private async Task<bool> Swiftcast()
        {
            if (Ultima.UltSettings.SummonerSwiftcast)
            {
                return await MySpells.CrossClass.Swiftcast.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> Shockwave()
        {
            return await MySpells.Shockwave.Cast();
        }

        private async Task<bool> MountainBuster()
        {
            return await MySpells.MountainBuster.Cast();
        }

        private async Task<bool> AerialSlash()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Painflare.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||

		MovementManager.IsMoving)
            {
                if (Core.Player.Pet != null &&
                    Core.Player.Pet.Name == "Garuda-Egi" &&
		    !ActionManager.CanCast(MySpells.Spur.Name, Core.Player) &&
		    !ActionManager.CanCast(MySpells.Rouse.Name, Core.Player) ||

		    Core.Player.Pet != null &&
                    Core.Player.Pet.Name == "Emerald Carbuncle" &&
		    !ActionManager.CanCast(MySpells.Spur.Name, Core.Player) &&
		    !ActionManager.CanCast(MySpells.Rouse.Name, Core.Player))  
			
                {
                    return await MySpells.AerialSlash.Cast();
		}
            }
            return false;
        }

        private async Task<bool> EarthenWard()
        {
            return await MySpells.EarthenWard.Cast();
        }

        private async Task<bool> SummonIII()
        {
            if (Core.Player.Pet == null &&
		Ultima.LastSpell.Name != MySpells.SummonIII.Name &&
                Ultima.UltSettings.SummonerSummonPet &&
                Ultima.UltSettings.SummonerIfrit)
		
            {
                if (Ultima.UltSettings.SummonerSwiftcast &&
		    ActionManager.CanCast(MySpells.CrossClass.Swiftcast.Name, Core.Player))
                {
                    if (await MySpells.CrossClass.Swiftcast.Cast())
                    {
                        await Coroutine.Wait(3000, () => Core.Player.HasAura(167));
                    }
                }
                return await MySpells.SummonIII.Cast();
            }
            return false;
        }

        private async Task<bool> CrimsonCyclone()
        {
            return await MySpells.CrimsonCyclone.Cast();
        }

        private async Task<bool> RadiantShield()
        {
            return await MySpells.RadiantShield.Cast();
        }

        private async Task<bool> Contagion()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name &&
		Ultima.LastSpell.Name != MySpells.Bio.Name &&
		Ultima.LastSpell.Name != MySpells.Miasma.Name &&
		Ultima.LastSpell.Name != MySpells.BioII.Name ||
		
		MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.Bio.Name &&
		Ultima.LastSpell.Name != MySpells.Miasma.Name &&
		Ultima.LastSpell.Name != MySpells.BioII.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.Pet != null &&
                    Core.Player.Pet.Name == "Garuda-Egi" &&
                    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 4500) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 4500) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4500) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
		    Core.Player.Pet != null &&
                    Core.Player.Pet.Name == "Emerald Carbuncle" &&
                    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 4500) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 4500) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4500))
                {
                    return await MySpells.Contagion.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Landslide()
        {
            return await MySpells.Landslide.Cast();
        }

        private async Task<bool> FlamingCrush()
        {
            return await MySpells.FlamingCrush.Cast();
        }

        private async Task<bool> Fester()
        {
            if (!MovementManager.IsMoving &&
		!ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		Core.Player.HasAura(MySpells.Aetherflow.Name) &&
                Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true) &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||

		ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		Core.Player.HasAura(MySpells.Aetherflow.Name) &&
		Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true) ||
	
		MovementManager.IsMoving &&
		!ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		Core.Player.HasAura(MySpells.Aetherflow.Name) &&
                Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true))	
            {
                if (Core.Player.HasAura(MySpells.Aetherflow.Name) &&
                    Core.Player.CurrentManaPercent >= 50)  
			
                {
                    return await MySpells.Fester.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Tribind()
        {
            return await MySpells.Tribind.Cast();
        }

        private async Task<bool> Spur()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Painflare.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||
		
		MovementManager.IsMoving)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth)
                {
                    return await MySpells.Spur.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Enkindle()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Aetherflow.Name &&
		Ultima.LastSpell.Name != MySpells.EnergyDrain.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Fester.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Painflare.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||

		MovementManager.IsMoving)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.CanCast(MySpells.Rouse.Name, Core.Player) &&
                    !ActionManager.CanCast(MySpells.Spur.Name, Core.Player))
                {
                    return await MySpells.Enkindle.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Painflare()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||

		MovementManager.IsMoving)
            {
                if (!ActionManager.CanCast(MySpells.DreadwyrmTrance.Name, Core.Player) &&
		    Core.Player.HasAura(MySpells.Aetherflow.Name) &&
		    ActionManager.CanCast(MySpells.Aetherflow.Name, Core.Player))
                {
                    return await MySpells.Painflare.Cast();
		}
            }
            return false;
        }

        private async Task<bool> RuinIII()
        {
            if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth * 2 &&
		Core.Player.CurrentManaPercent >= 40 ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 2 &&
                Core.Player.HasAura(190, false, 5500) &&
		Core.Player.CurrentManaPercent >= 40 ||	

		Core.Player.HasAura(MySpells.DreadwyrmTrance.Name))
            {
                return await MySpells.RuinIII.Cast();
            }
            return false;
        }

        private async Task<bool> Tridisaster()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Bane.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name &&
		Ultima.LastSpell.Name != MySpells.Bio.Name &&
		Ultima.LastSpell.Name != MySpells.Miasma.Name &&
		Ultima.LastSpell.Name != MySpells.BioII.Name ||
	
		MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Ultima.LastSpell.Name != MySpells.Bio.Name &&
		Ultima.LastSpell.Name != MySpells.Miasma.Name &&
		Ultima.LastSpell.Name != MySpells.BioII.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 3 &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
                    !Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true) &&
                    !Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 3 &&
		    !ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    !ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name, true, 4500) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name, false, 5000) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true) &&
		    !Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 9500) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name, false, 5000) &&
                    !Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 7500) ||

		    ActionManager.HasSpell(MySpells.DreadwyrmTrance.Name) &&
		    ActionManager.HasSpell(MySpells.Deathflare.Name) &&
		    Core.Player.HasAura(MySpells.DreadwyrmTrance.Name) &&
		    !Core.Player.HasAura(MySpells.DreadwyrmTrance.Name, false, 5000) &&
                    !Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 5500))
                {
                    return await MySpells.Tridisaster.Cast();
		}
            }
            return false;
        }
 
        private async Task<bool> DreadwyrmTrance()
        {
            if (!MovementManager.IsMoving &&
		Ultima.LastSpell.Name != MySpells.AerialSlash.Name &&
		Ultima.LastSpell.Name != MySpells.Contagion.Name &&
		Ultima.LastSpell.Name != MySpells.Virus.Name &&
		Ultima.LastSpell.Name != MySpells.Rouse.Name &&
		Ultima.LastSpell.Name != MySpells.Spur.Name &&
		Ultima.LastSpell.Name != MySpells.Enkindle.Name &&
		Ultima.LastSpell.Name != MySpells.Tridisaster.Name &&
		Ultima.LastSpell.Name != MySpells.Deathflare.Name ||

		MovementManager.IsMoving)
            {
                if (Core.Player.CurrentTarget.HasAura(MySpells.BioII.Name, true, 7000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Miasma.Name, true, 7000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.Bio.Name, true, 4000) ||

		    ActionManager.CanCast(MySpells.Tridisaster.Name, Core.Player.CurrentTarget) ||

		    Core.Player.CurrentTarget.CurrentHealth < Core.Player.MaxHealth)
                {
                    return await MySpells.DreadwyrmTrance.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Deathflare()
        {
            if (Ultima.LastSpell.Name != MySpells.DreadwyrmTrance.Name &&
		Core.Player.HasAura(MySpells.DreadwyrmTrance.Name, true) &&
		!Core.Player.HasAura(MySpells.DreadwyrmTrance.Name, true, 4500))
            {
                return await MySpells.Deathflare.Cast();
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

        private async Task<bool> MistyVeil()
        {
            return await MySpells.PvP.MistyVeil.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        private async Task<bool> Wither()
        {
            return await MySpells.PvP.Wither.Cast();
        }

        #endregion
    }
}