using System;
using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using System.Linq;
using System.Threading.Tasks;
using UltimaCR.Spells;
using UltimaCR.Spells.Main;

namespace UltimaCR.Rotations
{
    public sealed partial class Ninja
    {
        private NinjaSpells _mySpells;

        private NinjaSpells MySpells
        {
            get { return _mySpells ?? (_mySpells = new NinjaSpells()); }
        }

        #region Class Spells

        private async Task<bool> SpinningEdge()
        {
            return await MySpells.SpinningEdge.Cast();
        }

        private async Task<bool> ShadeShift()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		Core.Player.InCombat &&
		Ultima.LastSpell.Name != MySpells.CrossClass.SecondWind.Name &&
		Core.Player.CurrentHealthPercent <= 40 ||
		
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth / 2 &&
		Ultima.LastSpell.Name != MySpells.CrossClass.SecondWind.Name &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 70)
            {
                return await MySpells.ShadeShift.Cast();
            }
            return false;
        }

        private async Task<bool> GustSlash()
        {
            if (ActionManager.LastSpell.Name == MySpells.SpinningEdge.Name)
            {
                return await MySpells.GustSlash.Cast();
            }
            return false;
        }

        private async Task<bool> KissOfTheWasp()
        {
            if (Ultima.UltSettings.NinjaKissOfTheWasp ||
                !ActionManager.HasSpell(MySpells.KissOfTheViper.Name))
            {
                if (!Core.Player.HasAura(MySpells.KissOfTheWasp.Name))
                {
                    return await MySpells.KissOfTheWasp.Cast();
                }
            }
            return false;
        }

        private async Task<bool> Mutilate()
        {
            if (!ActionManager.HasSpell(MySpells.AeolianEdge.Name) &&
		ActionManager.LastSpell.Name != MySpells.SpinningEdge.Name ||

		ActionManager.HasSpell(MySpells.AeolianEdge.Name) &&
		ActionManager.LastSpell.Name != MySpells.SpinningEdge.Name &&
		ActionManager.LastSpell.Name != MySpells.GustSlash.Name)
            {
                if (!Ultima.UltSettings.MultiTarget &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.HasSpell(MySpells.AeolianEdge.Name) &&
		    !Core.Player.CurrentTarget.HasAura("Mutilation", true, 3250) ||

		    !Ultima.UltSettings.MultiTarget &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.HasSpell(MySpells.ShadowFang.Name) &&
		    !Core.Player.CurrentTarget.HasAura("Mutilation", true, 3250) ||

		    !Ultima.UltSettings.MultiTarget &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.HasSpell(MySpells.ShadowFang.Name) &&
		    !Core.Player.CurrentTarget.HasAura("Mutilation", true, 3250) ||

		    !Ultima.UltSettings.MultiTarget &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    ActionManager.HasSpell(MySpells.ShadowFang.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.ShadowFang.Name, true, 5150) &&
		    !Core.Player.CurrentTarget.HasAura("Mutilation", true, 3250) ||

		    Ultima.UltSettings.MultiTarget &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    ActionManager.HasSpell(MySpells.ShadowFang.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.ShadowFang.Name, true, 5150) &&
		    Core.Player.CurrentTarget.HasAura("Mutilation", true) &&
		    !Core.Player.CurrentTarget.HasAura("Mutilation", true, 4000))
                {
                    return await MySpells.Mutilate.Cast();
		}
            }
            return false;
        }

        private async Task<bool> RaidOpenerMutilate()
        {
            if (Core.Player.CurrentTarget.HasAura(MySpells.ShadowFang.Name, true, 5150) &&
		!Core.Player.CurrentTarget.HasAura("Mutilation", true, 3250))
            {
                if (Core.Player.CurrentTarget.CurrentHealthPercent >= 96 &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 80)
                {
                    return await MySpells.Mutilate.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Hide()
        {
            return await MySpells.Hide.Cast();
        }

        private async Task<bool> Assassinate()
        {
            if (Ultima.UltSettings.NinjaAssassinate)
            {
                return await MySpells.Assassinate.Cast();
            }
            return false;
        }

        private async Task<bool> ThrowingDagger()
        {
            if (Core.Player.TargetDistance(10))
            {
                return await MySpells.ThrowingDagger.Cast();
            }
            return false;
        }

        private async Task<bool> Mug()
	{
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.Jugulate.Name &&
		Ultima.LastSpell.Name != MySpells.SneakAttack.Name &&
		Ultima.LastSpell.Name != MySpells.TrickAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Duality.Name &&
		Ultima.LastSpell.Name != MySpells.Ten.Name &&
		Ultima.LastSpell.Name != MySpells.Chi.Name &&
		Ultima.LastSpell.Name != MySpells.Jin.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.Ninjutsu.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.Suiton.Name &&
		Ultima.LastSpell.Name != MySpells.DreamWithinADream.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name)
            {
                if (!ActionManager.HasSpell(MySpells.DancingEdge.Name) ||

		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    ActionManager.HasSpell(MySpells.DancingEdge.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, true, 2100) ||

                    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.CurrentTarget.HasAura(90))
                {
                    return await MySpells.Mug.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Goad()
        {
            if (Ultima.UltSettings.NinjaGoad)
            {
                var target = Helpers.GoadManager.FirstOrDefault();

                if (target != null)
                {
                    return await MySpells.Goad.Cast(target);
                }
            }
            return false;
        }

        private async Task<bool> SneakAttack()
        {
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Mug.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.Jugulate.Name &&
		Ultima.LastSpell.Name != MySpells.TrickAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Duality.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.DreamWithinADream.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
                    Core.Player.CurrentTarget.IsFacing(Core.Player) &&
                    Core.Player.HasAura("Suiton") ||

		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
                    Core.Player.CurrentTarget.IsFlanking &&
                    Core.Player.HasAura("Suiton") ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
                    Core.Player.CurrentTarget.IsFacing(Core.Player) &&
                    Core.Player.HasAura("Suiton") &&
		    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, true, 2100) ||

		    Core.Player.HasTarget &&
                    Core.Player.CurrentTarget.IsFacing(Core.Player) &&
                    Core.Player.HasAura("Suiton") &&
		    Core.Player.CurrentTarget.HasAura(90))
                {
                    return await MySpells.SneakAttack.Cast();
		}
            }
            return false;
        }

        private async Task<bool> KissOfTheViper()
        {
            if (Ultima.UltSettings.NinjaKissOfTheViper &&
                !Core.Player.HasAura(MySpells.KissOfTheViper.Name))
            {
                return await MySpells.KissOfTheViper.Cast();
            }
            return false;
        }

        private async Task<bool> Jugulate()
        {
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Mug.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.SneakAttack.Name &&
		Ultima.LastSpell.Name != MySpells.TrickAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Duality.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.Suiton.Name &&
		Ultima.LastSpell.Name != MySpells.DreamWithinADream.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name)
            {
                if (Ultima.UltSettings.NinjaJugulate &&
		    !ActionManager.HasSpell(MySpells.DancingEdge.Name) ||

		    Ultima.UltSettings.NinjaJugulate &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth ||

		    Ultima.UltSettings.NinjaJugulate &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    ActionManager.HasSpell(MySpells.DancingEdge.Name) &&
		    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, true, 2100) ||

		    Ultima.UltSettings.NinjaJugulate &&
                    Core.Player.CurrentTarget.HasAura(90))
                {
                    return await MySpells.Jugulate.Cast();
		}
            }
            return false;
        }

        private async Task<bool> AeolianEdgeBOSSNODIRECTIONAL()
	{
            if (ActionManager.LastSpell.Name == MySpells.GustSlash.Name &&
		Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 80 &&
		ActionManager.HasSpell(MySpells.DancingEdge.Name))
            {
                if (!ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 6250) ||

		    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
                    Core.Player.CurrentTarget.HasAura(90) ||

		    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
		    Core.Player.HasAura(MySpells.Huton.Name) &&
                    Core.Player.HasAura(MySpells.Huton.Name, false, 21000) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 6250) ||

		    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
		    Core.Player.HasAura(MySpells.Huton.Name) &&
                    Core.Player.HasAura(MySpells.Huton.Name, false, 21000) &&
                    Core.Player.CurrentTarget.HasAura(90))
                {
                    return await MySpells.AeolianEdge.Cast();
		}
            }
            return false;
        }

        private async Task<bool> AeolianEdge()
	{
            if (ActionManager.LastSpell.Name == MySpells.GustSlash.Name)
            {
		    if (Ultima.UltSettings.SingleTarget)
		    {
			if (!ActionManager.HasSpell(MySpells.DancingEdge.Name) &&
			    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) ||

			    ActionManager.HasSpell(MySpells.DancingEdge.Name) &&
			    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsBehind &&
                    	    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 6250) ||

			    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsBehind &&
		    	    Core.Player.HasAura(MySpells.Huton.Name) &&
                    	    Core.Player.HasAura(MySpells.Huton.Name, false, 21000) &&
                    	    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 6250) ||

			    Core.Player.CurrentTarget.IsBehind &&
		    	    Core.Player.HasAura(MySpells.Huton.Name) &&
                    	    Core.Player.HasAura(MySpells.Huton.Name, false, 21000) &&
                    	    Core.Player.CurrentTarget.HasAura(90))
		        {
                            return await MySpells.AeolianEdge.Cast();
                        }
		    }

		    if (!Ultima.UltSettings.SingleTarget)
		    {
			if (!ActionManager.HasSpell(MySpells.DancingEdge.Name) &&
			    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) ||

			    ActionManager.HasSpell(MySpells.DancingEdge.Name) &&
			    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsBehind ||

			    ActionManager.HasSpell(MySpells.DancingEdge.Name) &&
			    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsBehind &&
		    	    Core.Player.HasAura(MySpells.Huton.Name) &&
                    	    Core.Player.HasAura(MySpells.Huton.Name, true, 21000))
		        {
                            return await MySpells.AeolianEdge.Cast();
                        }
		    }
            }
            return false;
        }

        private async Task<bool> DancingEdge()
        {
            if (ActionManager.LastSpell.Name == MySpells.GustSlash.Name)
            {
                    if (Ultima.UltSettings.SingleTarget)
		    {
			if (!ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsFacing(Core.Player) ||

			    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsFlanking ||

			    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsFacing(Core.Player) &&
                            Core.Player.HasAura(MySpells.Huton.Name, true, 21000)  ||

                            !Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 6250) &&
                            !Core.Player.CurrentTarget.HasAura(90))
		        {
                            return await MySpells.DancingEdge.Cast();
                        }
		    }

		    if (!Ultima.UltSettings.SingleTarget)
		    {
			if (!ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsFacing(Core.Player) ||

			    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsFlanking ||

			    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
			    Core.Player.CurrentTarget.IsFacing(Core.Player) &&
                            Core.Player.HasAura(MySpells.Huton.Name, true, 21000))
		        {
                            return await MySpells.DancingEdge.Cast();
                        }
		    }
            }
            return false;
        }

        private async Task<bool> ArmorCrush()
        {
            if (ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
		ActionManager.LastSpell.Name == MySpells.GustSlash.Name)
            {
		    if (Ultima.UltSettings.SingleTarget)
		    {
			if (Core.Player.HasAura(MySpells.Huton.Name) &&
                            !Core.Player.HasAura(MySpells.Huton.Name, false, 21000) ||

			    Core.Player.CurrentTarget.IsFlanking &&
                            Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 6250) ||

			    Core.Player.CurrentTarget.IsFlanking &&
                            Core.Player.CurrentTarget.HasAura(90))
		        {
                            return await MySpells.ArmorCrush.Cast();
                        }
		    }

		    if (Ultima.UltSettings.SmartTarget)
		    {
			if (Core.Player.HasAura(MySpells.Huton.Name) &&
                            !Core.Player.HasAura(MySpells.Huton.Name, true, 21000) ||

			    Core.Player.CurrentTarget.IsFlanking)
		        {
                            return await MySpells.ArmorCrush.Cast();
                        }
		    }

		    if (Ultima.UltSettings.MultiTarget)
		    {
			if (Core.Player.HasAura(MySpells.Huton.Name) &&
                            !Core.Player.HasAura(MySpells.Huton.Name, true, 21000))
		        {
                            return await MySpells.ArmorCrush.Cast();
                        }
		    }
            }
            return false;
        }

        private async Task<bool> DeathBlossom()
        {
            if (ActionManager.LastSpell.Name != MySpells.SpinningEdge.Name &&
		ActionManager.LastSpell.Name != MySpells.GustSlash.Name)
            {
                if (Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentTP > 200 &&
		    Helpers.EnemiesNearPlayer(4) > 2 &&
		    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) ||

		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentTP > 200 &&
		    Helpers.EnemiesNearPlayer(4) > 2 &&
		    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
		    Core.Player.HasAura(MySpells.Huton.Name, true, 21000))
                {
                    return await MySpells.DeathBlossom.Cast();
		}
            }
            return false;
        }

        private async Task<bool> ShadowFang()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * .7 &&
		!ActionManager.HasSpell(MySpells.Jin.Name) ||

		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * .7 &&
		ActionManager.HasSpell(MySpells.Jin.Name) &&
		Core.Player.HasAura(MySpells.Huton.Name, true, 21000))
            {
                if (Core.Player.CurrentTarget.MaxHealth <= Core.Player.MaxHealth * 80 &&
		    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 6250) ||

		    Core.Player.CurrentTarget.MaxHealth <= Core.Player.MaxHealth * 80 &&
                    Core.Player.CurrentTarget.HasAura(90) ||

		    Core.Player.CurrentTarget.MaxHealth > Core.Player.MaxHealth * 80)
                {
                    if (!Ultima.UltSettings.MultiTarget &&
			!Core.Player.CurrentTarget.HasAura(MySpells.ShadowFang.Name, true, 5150) &&
                        ActionManager.LastSpell.Name == MySpells.SpinningEdge.Name ||

			Ultima.UltSettings.MultiTarget &&
			Core.Player.CurrentTarget.HasAura(MySpells.ShadowFang.Name, true) &&
			!Core.Player.CurrentTarget.HasAura(MySpells.ShadowFang.Name, true, 6000) &&
                        ActionManager.LastSpell.Name == MySpells.SpinningEdge.Name)
                    {
                        return await MySpells.ShadowFang.Cast();
                    }
                }
            }
            return false;
        }

        private async Task<bool> RaidOpenerShadowFang()
        {
            if (ActionManager.LastSpell.Name == MySpells.SpinningEdge.Name &&
		!Core.Player.CurrentTarget.HasAura(MySpells.ShadowFang.Name, true, 5150))
            {
                if (Core.Player.CurrentTarget.CurrentHealthPercent >= 96 &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth * 80)
                {
                    return await MySpells.ShadowFang.Cast();
		}
            }
            return false;
        }

        private async Task<bool> TrickAttack()
        {
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Mug.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.Jugulate.Name &&
		Ultima.LastSpell.Name != MySpells.SneakAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Ten.Name &&
		Ultima.LastSpell.Name != MySpells.Chi.Name &&
		Ultima.LastSpell.Name != MySpells.Jin.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.DreamWithinADream.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
                    Core.Player.CurrentTarget.IsBehind &&
                    Core.Player.HasAura("Suiton") &&
		    !Core.Player.CurrentTarget.HasAura("Vulnerability Up") ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
                    Core.Player.CurrentTarget.IsBehind &&
                    Core.Player.HasAura("Suiton") &&
 	    	    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 2100) &&
		    !Core.Player.CurrentTarget.HasAura("Vulnerability Up") ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
                    Core.Player.CurrentTarget.IsBehind &&
                    Core.Player.HasAura("Suiton") &&
                    Core.Player.CurrentTarget.HasAura(90, false, 2100) &&
		    !Core.Player.CurrentTarget.HasAura("Vulnerability Up"))
                {
                    return await MySpells.TrickAttack.Cast();
		}
            }
            return false;
        }

        #endregion

        #region Cross Class Spells

        #region Lancer

        private async Task<bool> Feint()
        {
            if (Ultima.UltSettings.NinjaFeint)
            {
                return await MySpells.CrossClass.Feint.Cast();
            }
            return false;
        }

        private async Task<bool> KeenFlurry()
        {
            if (Ultima.UltSettings.NinjaKeenFlurry)
            {
                return await MySpells.CrossClass.KeenFlurry.Cast();
            }
            return false;
        }

        private async Task<bool> Invigorate()
        {
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Mug.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.Jugulate.Name &&
		Ultima.LastSpell.Name != MySpells.SneakAttack.Name &&
		Ultima.LastSpell.Name != MySpells.TrickAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Ten.Name &&
		Ultima.LastSpell.Name != MySpells.Chi.Name &&
		Ultima.LastSpell.Name != MySpells.Jin.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.Ninjutsu.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.Suiton.Name &&
		Ultima.LastSpell.Name != MySpells.DreamWithinADream.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name)
            {
                if (Ultima.UltSettings.NinjaInvigorate &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false) &&
                    Core.Player.CurrentTP < 540)
                {
                    return await MySpells.CrossClass.Invigorate.Cast();
		}
            }
            return false;
        }

        private async Task<bool> BloodForBlood()
        {
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Mug.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.Jugulate.Name &&
		Ultima.LastSpell.Name != MySpells.SneakAttack.Name &&
		Ultima.LastSpell.Name != MySpells.TrickAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Ten.Name &&
		Ultima.LastSpell.Name != MySpells.Chi.Name &&
		Ultima.LastSpell.Name != MySpells.Jin.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.Ninjutsu.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.Suiton.Name &&
		Ultima.LastSpell.Name != MySpells.DreamWithinADream.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name)
            {
                if (Ultima.UltSettings.NinjaBloodForBlood &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false) ||

		    Ultima.UltSettings.NinjaBloodForBlood &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.CurrentTarget.CurrentHealthPercent > 70 &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false))
                {
                    return await MySpells.CrossClass.BloodForBlood.Cast();
		}
            }
            return false;
        }

        #endregion

        #region Pugilist

        private async Task<bool> Featherfoot()
        {
            if (Ultima.UltSettings.NinjaFeatherfoot)
            {
                return await MySpells.CrossClass.Featherfoot.Cast();
            }
            return false;
        }

        private async Task<bool> SecondWind()
        {
            if (Ultima.LastSpell.Name != MySpells.ShadeShift.Name &&
		Core.Player.InCombat &&
		Core.Player.CurrentHealthPercent <= 40 ||
		
		Ultima.LastSpell.Name != MySpells.ShadeShift.Name &&
		Core.Player.CurrentTarget.IsFacing(Core.Player) &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.TargetDistance(4, false) &&
		Core.Player.CurrentHealthPercent <= 60)
            {
                return await MySpells.CrossClass.SecondWind.Cast();
            }
            return false;
        }

        private async Task<bool> Haymaker()
        {
            if (Ultima.UltSettings.NinjaHaymaker)
            {
                return await MySpells.CrossClass.Haymaker.Cast();
            }
            return false;
        }

        private async Task<bool> InternalRelease()
        {
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Mug.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.Jugulate.Name &&
		Ultima.LastSpell.Name != MySpells.SneakAttack.Name &&
		Ultima.LastSpell.Name != MySpells.TrickAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.Suiton.Name &&
		Ultima.LastSpell.Name != MySpells.DreamWithinADream.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name)
            {
                if (Ultima.UltSettings.NinjaInternalRelease &&
		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false) ||

		    Ultima.UltSettings.NinjaInternalRelease &&
		    Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&
		    Core.Player.CurrentTarget.CurrentHealthPercent > 70 &&
		    Core.Player.HasTarget &&
		    Core.Player.InCombat &&
		    Core.Player.TargetDistance(4, false))
                {
                    return await MySpells.CrossClass.InternalRelease.Cast();
		}
            }
            return false;
        }

        private async Task<bool> Mantra()
        {
            if (Ultima.UltSettings.NinjaMantra)
            {
                return await MySpells.CrossClass.Mantra.Cast();
            }
            return false;
        }

        #endregion

        #endregion

        #region Job Spells

        private async Task<bool> Ninjutsu()
        {
            if (ActionManager.CanCast(MySpells.Ten.ID, Core.Player) &&
                Core.Player.HasAura("Mudra"))
            {
                return await MySpells.Ninjutsu.Cast();
            }
            return false;
        }

        private async Task<bool> Shukuchi()
        {
            if (Ultima.UltSettings.NinjaShukuchi &&
                Core.Player.TargetDistance(10))
            {
                return await MySpells.Shukuchi.Cast();
            }
            return false;
        }

        private async Task<bool> Kassatsu()
        {
            if (Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		Core.Player.HasTarget &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.HasAura("Vulnerability Up"))
            {
                return await MySpells.Kassatsu.Cast();
            }
            return false;
        }

        private async Task<bool> FumaShuriken()
        {
            if (Ultima.UltSettings.NinjaFumaShuriken &&
		!Core.Player.CurrentTarget.HasAura(478) &&
		!Core.Player.CurrentTarget.HasAura(941) ||
		Core.Player.CurrentTarget.HasAura(477) ||
		Core.Player.CurrentTarget.HasAura(942) ||
                Core.Player.ClassLevel < MySpells.Raiton.Level)
            {
                if (!ActionManager.HasSpell(MySpells.Jin.Name) &&
		    ActionManager.CanCast(MySpells.Ten.ID, Core.Player) &&
                    DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
                    Core.Player.TargetDistance(25, false) &&
                    Core.Player.CurrentTarget.CanAttack &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentTarget.CurrentHealthPercent < 99 &&
                    Core.Player.CurrentTarget.InLineOfSight() ||

		    ActionManager.HasSpell(MySpells.Jin.Name) &&
		    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
		    Core.Player.HasAura(MySpells.Huton.Name, true, 21000) &&
		    ActionManager.CanCast(MySpells.Ten.ID, Core.Player) &&
                    DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
                    Core.Player.TargetDistance(25, false) &&
                    Core.Player.CurrentTarget.CanAttack &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentTarget.CurrentHealthPercent < 99 &&
                    Core.Player.CurrentTarget.InLineOfSight() ||

		    ActionManager.HasSpell(MySpells.Jin.Name) &&
		    ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
		    Core.Player.HasAura(MySpells.Huton.Name, true, 7000) &&
		    ActionManager.CanCast(MySpells.Ten.ID, Core.Player) &&
                    DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
                    Core.Player.TargetDistance(25, false) &&
                    Core.Player.CurrentTarget.CanAttack &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentTarget.CurrentHealthPercent < 99 &&
                    Core.Player.CurrentTarget.InLineOfSight())
                {
                    if (!ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    	Ultima.LastSpell.ID != MySpells.Chi.ID &&
                    	Ultima.LastSpell.ID != MySpells.Jin.ID &&
                    	Ultima.LastSpell.ID != MySpells.Ninjutsu.ID)
                    {
                        if (await MySpells.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player));
                        }
                    }
                    if (ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                        (Ultima.LastSpell.ID == MySpells.Ten.ID ||
                        Ultima.LastSpell.ID == MySpells.FumaShuriken.ID))
                    {
                        if (await MySpells.FumaShuriken.Cast())
                        {
                            await Coroutine.Wait(6000, () => !Core.Player.HasAura("Mudra"));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Katon()
        {
            if (!Core.Player.HasAura(614) &&
		!Core.Player.HasAura(615) &&
		!Core.Player.HasAura(1108) &&
		!Core.Player.CurrentTarget.HasAura(477) &&
		!Core.Player.CurrentTarget.HasAura(942) &&
		ActionManager.CanCast(MySpells.Chi.ID, Core.Player) &&
                DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
		Core.Player.TargetDistance(15, false) &&
                Core.Player.CurrentTarget.CanAttack &&
                Core.Player.CurrentTarget.InLineOfSight() ||
                Core.Player.HasAura("Mudra"))
            {
                if (Helpers.EnemiesNearTarget(5) > 2 ||

		    Helpers.EnemiesNearTarget(5) > 2 &&
		    ActionManager.HasSpell(MySpells.Jin.Name) &&
		    !ActionManager.HasSpell(MySpells.ArmorCrush.Name) &&
		    Core.Player.HasAura(MySpells.Huton.Name, true, 21000) ||

		    Helpers.EnemiesNearTarget(5) > 2 &&
		    ActionManager.HasSpell(MySpells.ArmorCrush.Name))
                {
                    if (!ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    	Ultima.LastSpell.ID != MySpells.Ten.ID &&
                    	Ultima.LastSpell.ID != MySpells.Jin.ID &&
                    	Ultima.LastSpell.ID != MySpells.Ninjutsu.ID)
                    {
                        if (await MySpells.Chi.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player));
                        }
                    }
                    if (ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                        Ultima.LastSpell.ID == MySpells.Chi.ID)
                    {
                        if (await MySpells.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ten.ID, Core.Player));
                        }
                    }
                    if (Ultima.LastSpell.ID == MySpells.Ten.ID ||
                        Ultima.LastSpell.ID == MySpells.Katon.ID)
                    {
                        if (await MySpells.Katon.Cast())
                        {
                            await Coroutine.Wait(6000, () => !Core.Player.HasAura("Mudra"));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Raiton()
        {
            if (Ultima.UltSettings.NinjaRaiton &&
		!Core.Player.CurrentTarget.HasAura(477) &&
		!Core.Player.CurrentTarget.HasAura(942) ||
		Core.Player.CurrentTarget.HasAura(478) ||
		Core.Player.CurrentTarget.HasAura(941))
            {
                if (!ActionManager.CanCast(MySpells.TrickAttack.Name, Core.Player.CurrentTarget) &&
		    ActionManager.HasSpell(MySpells.Chi.Name) &&
		    !ActionManager.HasSpell(MySpells.Jin.Name) &&
		    ActionManager.CanCast(MySpells.Chi.ID, Core.Player) &&
                    DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
		    Core.Player.TargetDistance(15, false) &&
                    Core.Player.CurrentTarget.CanAttack &&
		    Core.Player.InCombat &&
		    Core.Player.CurrentTarget.CurrentHealthPercent < 99 &&
                    Core.Player.CurrentTarget.InLineOfSight())
                {
                    if (!ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                        Ultima.LastSpell.ID != MySpells.Chi.ID &&
                        Ultima.LastSpell.ID != MySpells.Jin.ID &&
                        Ultima.LastSpell.ID != MySpells.Ninjutsu.ID)
                    {
                        if (await MySpells.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player));
                        }
                    }
                    if (ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                        Ultima.LastSpell.ID == MySpells.Ten.ID)
                    {
                        if (await MySpells.Chi.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Chi.ID, Core.Player));
                        }
                    }
                    if (Ultima.LastSpell.ID == MySpells.Chi.ID ||
                        Ultima.LastSpell.ID == MySpells.Raiton.ID)
                    {
                        if (await MySpells.Raiton.Cast())
                        {
                            await Coroutine.Wait(6000, () => !Core.Player.HasAura("Mudra"));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Hyoton()
        {
            if (ActionManager.CanCast(MySpells.Jin.ID, Core.Player) &&
                DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
		Core.Player.TargetDistance(25, false) &&
                Core.Player.CurrentTarget.CanAttack &&
                Core.Player.CurrentTarget.InLineOfSight() ||

                Core.Player.HasAura("Mudra"))
            {
                if (!ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    Ultima.LastSpell.ID != MySpells.Chi.ID &&
                    Ultima.LastSpell.ID != MySpells.Jin.ID &&
                    Ultima.LastSpell.ID != MySpells.Ninjutsu.ID)
                {
                    if (await MySpells.Ten.Cast())
                    {
                        await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player));
                    }
                }
                if (ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    Ultima.LastSpell.ID == MySpells.Ten.ID)
                {
                    if (await MySpells.Jin.Cast())
                    {
                        await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Jin.ID, Core.Player));
                    }
                }
                if (Ultima.LastSpell.ID == MySpells.Jin.ID ||
                    Ultima.LastSpell.ID == MySpells.Hyoton.ID)
                {
                    if (await MySpells.Hyoton.Cast())
                    {
                        await Coroutine.Wait(2000, () => !Core.Player.HasAura("Mudra"));
                        return true;
                    }
                }
            }
            return false;
        }

        private async Task<bool> Huton()
        {
            if (!Core.Player.HasAura(614) &&
		!Core.Player.HasAura(615) &&
		!Core.Player.HasAura(1108) &&
		ActionManager.CanCast(MySpells.Jin.ID, Core.Player) &&
		DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000)
            {
                if (ActionResourceManager.Bard.Timer <= new TimeSpan(0, 0, 0, 8, 0))
                {
                    if (!ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    	Ultima.LastSpell.ID != MySpells.Ten.ID &&
                    	Ultima.LastSpell.ID != MySpells.Jin.ID &&
                    	Ultima.LastSpell.ID != MySpells.Ninjutsu.ID)
                    {
                        if (await MySpells.Jin.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player));
                        }
                    }
                    if (ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                        Ultima.LastSpell.ID == MySpells.Jin.ID)
                    {
                        if (await MySpells.Chi.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Jin.ID, Core.Player));
                        }
                    }
                    if (Ultima.LastSpell.ID == MySpells.Chi.ID)
                    {
                        if (await MySpells.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ten.ID, Core.Player));
                        }
                    }
                    if (Ultima.LastSpell.ID == MySpells.Ten.ID ||
                        Ultima.LastSpell.ID == MySpells.Huton.ID)
                    {
                        if (await MySpells.Huton.Cast())
                        {
                            await Coroutine.Wait(2000, () => !Core.Player.HasAura("Mudra"));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Doton()
        {
            if (ActionManager.CanCast(MySpells.Jin.ID, Core.Player) &&
                DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
                Core.Player.HasAura(MySpells.Kassatsu.Name) ||

                Core.Player.HasAura("Mudra"))
            {
                if (Helpers.EnemiesNearPlayer(5) > 2)
                {
                    if (!ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    	Ultima.LastSpell.ID != MySpells.Chi.ID &&
                    	Ultima.LastSpell.ID != MySpells.Jin.ID &&
                    	Ultima.LastSpell.ID != MySpells.Ninjutsu.ID)
                    {
                        if (await MySpells.Ten.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player));
                        }
                    }
                    if (ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                        Ultima.LastSpell.ID == MySpells.Ten.ID)
                    {
                        if (await MySpells.Jin.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Jin.ID, Core.Player));
                        }
                    }
                    if (Ultima.LastSpell.ID == MySpells.Jin.ID)
                    {
                        if (await MySpells.Chi.Cast())
                        {
                            await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Chi.ID, Core.Player));
                        }
                    }
                    if (Ultima.LastSpell.ID == MySpells.Chi.ID ||
                        Ultima.LastSpell.ID == MySpells.Doton.ID)
                    {
                        if (await MySpells.Doton.Cast())
                        {
                            await Coroutine.Wait(2000, () => !Core.Player.HasAura("Mudra"));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private async Task<bool> Suiton()
        {
            if (!Core.Player.CurrentTarget.HasAura(477) &&
		!Core.Player.CurrentTarget.HasAura(497) &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Core.Player.HasAura(MySpells.Huton.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		ActionManager.CanCast(MySpells.Jin.ID, Core.Player) &&
                DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
                DataManager.GetSpellData(MySpells.TrickAttack.ID).Cooldown.TotalMilliseconds == 0 &&
		Core.Player.TargetDistance(15, false) &&
                Core.Player.CurrentTarget.CanAttack &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 99 &&
                !Core.Player.CurrentTarget.HasAura(MySpells.TrickAttack.Name, false, 3000) ||

		!Core.Player.CurrentTarget.HasAura(942) &&
		!Core.Player.CurrentTarget.HasAura(497) &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Core.Player.HasAura(MySpells.Huton.Name) &&
		Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		ActionManager.CanCast(MySpells.Jin.ID, Core.Player) &&
                DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(2240).AdjustedCooldown.TotalMilliseconds - 1000 &&
                DataManager.GetSpellData(MySpells.TrickAttack.ID).Cooldown.TotalMilliseconds == 0 &&
		Core.Player.TargetDistance(15, false) &&
                Core.Player.CurrentTarget.CanAttack &&
		Core.Player.InCombat &&
		Core.Player.CurrentTarget.CurrentHealthPercent < 99 &&
                !Core.Player.CurrentTarget.HasAura(MySpells.TrickAttack.Name, false, 3000))
            {
                if (!ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    Ultima.LastSpell.ID != MySpells.Chi.ID &&
                    Ultima.LastSpell.ID != MySpells.Jin.ID &&
                    Ultima.LastSpell.ID != MySpells.Ninjutsu.ID)

                {
                    if (await MySpells.Ten.Cast())
                    {
                        await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player));
                    }
                }
                if (ActionManager.CanCast(MySpells.Ninjutsu.ID, Core.Player) &&
                    Ultima.LastSpell.ID == MySpells.Ten.ID)
                {
                    if (await MySpells.Chi.Cast())
                    {
                        await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Chi.ID, Core.Player));
                    }
                }
                if (Ultima.LastSpell.ID == MySpells.Chi.ID)
                {
                    if (await MySpells.Jin.Cast())
                    {
                        await Coroutine.Wait(2000, () => ActionManager.CanCast(MySpells.Jin.ID, Core.Player));
                    }
                }
                if (Ultima.LastSpell.ID == MySpells.Jin.ID ||
                        Ultima.LastSpell.ID == MySpells.Suiton.ID)
                {
                    if (await MySpells.Suiton.Cast())
                    {
                        await Coroutine.Wait(2000, () => !Core.Player.HasAura("Mudra"));
                        return true;
                    }
                }
            }
            return false;
        }

        private async Task<bool> SmokeScreen()
        {
            return await MySpells.SmokeScreen.Cast();
        }

        private async Task<bool> Shadewalker()
        {
            return await MySpells.Shadewalker.Cast();
        }

        private async Task<bool> Duality()
	{
            if (ActionManager.LastSpell.Name == MySpells.GustSlash.Name &&
		Core.Player.HasAura(MySpells.Huton.Name, true, 21000) &&
		!ActionManager.CanCast(MySpells.Jin.Name, Core.Player) &&
		!ActionManager.CanCast(MySpells.TrickAttack.Name, Core.Player.CurrentTarget))
            {
                if (Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 9000) ||

		    Core.Player.CurrentTarget.HasAura(90))
                {
                    return await MySpells.Duality.Cast();
		}
            }
            return false;
        }

        private async Task<bool> DreamWithinADream()
        {
            if (Ultima.LastSpell.Name != MySpells.Assassinate.Name &&
		Ultima.LastSpell.Name != MySpells.Mug.Name &&
		Ultima.LastSpell.Name != MySpells.Goad.Name &&
		Ultima.LastSpell.Name != MySpells.Jugulate.Name &&
		Ultima.LastSpell.Name != MySpells.SneakAttack.Name &&
		Ultima.LastSpell.Name != MySpells.TrickAttack.Name &&
		Ultima.LastSpell.Name != MySpells.Kassatsu.Name &&
		Ultima.LastSpell.Name != MySpells.Duality.Name &&
		Ultima.LastSpell.Name != MySpells.Huton.Name &&
		Ultima.LastSpell.Name != MySpells.Doton.Name &&
		Ultima.LastSpell.Name != MySpells.Katon.Name &&
		Ultima.LastSpell.Name != MySpells.FumaShuriken.Name &&
		Ultima.LastSpell.Name != MySpells.Raiton.Name &&
		Ultima.LastSpell.Name != MySpells.Suiton.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.Invigorate.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.BloodForBlood.Name &&
		Ultima.LastSpell.Name != MySpells.CrossClass.InternalRelease.Name)
            {
                if (Core.Player.CurrentTarget.CurrentHealth <= Core.Player.MaxHealth &&

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.CanCast(MySpells.Duality.Name, Core.Player) &&
                    Core.Player.CurrentTarget.HasAura(90) ||

		    Core.Player.CurrentTarget.CurrentHealth > Core.Player.MaxHealth &&
		    !ActionManager.CanCast(MySpells.Duality.Name, Core.Player) &&
                    Core.Player.CurrentTarget.HasAura(MySpells.DancingEdge.Name, false, 5000))


                {
                    return await MySpells.DreamWithinADream.Cast();
		}
            }
            return false;
        }

        #endregion

        #region Custom Spells

        private static bool RecentMudra
        {
            get
            {
                return (Spell.RecentSpell.Keys.Any(rm => rm.Contains("Ten") || rm.Contains("Chi") || rm.Contains("Jin")));
            }
        }
        
        #endregion

        #region PvP Spells

        private async Task<bool> Detect()
        {
            return await MySpells.PvP.Detect.Cast();
        }

        private async Task<bool> IllWind()
        {
            return await MySpells.PvP.IllWind.Cast();
        }

        private async Task<bool> Malmsight()
        {
            return await MySpells.PvP.Malmsight.Cast();
        }

        private async Task<bool> Overwhelm()
        {
            return await MySpells.PvP.Overwhelm.Cast();
        }

        private async Task<bool> Purify()
        {
            return await MySpells.PvP.Purify.Cast();
        }

        private async Task<bool> Recouperate()
        {
            return await MySpells.PvP.Recouperate.Cast();
        }

        #endregion
    }
}