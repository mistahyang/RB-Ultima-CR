using Buddy.Coroutines;
using Clio.Utilities;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Helpers;
using ff14bot.Managers;
using ff14bot.Navigation;
using ff14bot.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UltimaCR.Spells
{
    public class Spell
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public byte Level { get; set; }
        public GCDType GCDType { private get; set; }
        public SpellType SpellType { private get; set; }
        public CastType CastType { private get; set; }

        public static readonly Dictionary<string, DateTime> RecentSpell = new Dictionary<string, DateTime>();

        readonly Random _rnd = new Random();

        private int GetMultiplier() { return _rnd.NextDouble() < 0.5 ? 1 : -1; }

        public async Task<bool> Cast(GameObject target = null)
        {
            #region Target
            if (target == null)
            {
                switch (CastType)
                {
                    case CastType.TargetLocation:
                        if (!Core.Player.HasTarget)
                        {
                            return false;
                        }
                        target = Core.Player.CurrentTarget;
                        break;
                    case CastType.Target:
                        if (!Core.Player.HasTarget)
                        {
                            return false;
                        }
                        target = Core.Player.CurrentTarget;
                        break;
                    default:
                        target = Core.Player;
                        break;
                }
            }
            #endregion

            #region Recent Spell Check
            RecentSpell.RemoveAll(t => DateTime.UtcNow > t);

            if (RecentSpell.ContainsKey(target.ObjectId.ToString("X") + "-" + Name))
            {
                return false;
            }
            #endregion

            #region Bard Song Check

            if (Core.Player.CurrentJob == ClassJobType.Bard &&
                SpellType == SpellType.Buff)
            {
                if (Core.Player.HasAura(135, true) ||
                    Core.Player.HasAura(137, true))
                {
                    return false;
                }
            }

            #endregion

            #region AoE Check

            if (SpellType == SpellType.AoE &&
                Ultima.UltSettings.SmartTarget)
            {
                var EnemyCount = Helpers.EnemyUnit.Count(eu => eu.Distance2D(target) - eu.CombatReach - target.CombatReach <= DataManager.GetSpellData(ID).Radius);

                switch (Core.Player.CurrentJob)
                {
                    case ClassJobType.Arcanist:
                    case ClassJobType.Scholar:
                    case ClassJobType.Summoner:
                        if (EnemyCount < 2)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Archer:
                    case ClassJobType.Bard:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Astrologian:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Conjurer:
                    case ClassJobType.WhiteMage:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.DarkKnight:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Gladiator:
                    case ClassJobType.Paladin:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Lancer:
                    case ClassJobType.Dragoon:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Machinist:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Marauder:
                    case ClassJobType.Warrior:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Pugilist:
                    case ClassJobType.Monk:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Rogue:
                    case ClassJobType.Ninja:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                    case ClassJobType.Thaumaturge:
                    case ClassJobType.BlackMage:
                        if (EnemyCount < 3)
                        {
                            return false;
                        }
                        break;
                }
            }

            #region Cone Check

            if (ID == 106 || ID == 41 || ID == 70)
            {
                if (!Helpers.InView(Core.Player.Location, Core.Player.Heading, target.Location))
                {
                    return false;
                }
            }

            #endregion

            #region Rectangle Check

            if (ID == 86)
            {
                if (!Core.Player.IsFacing(target))
                {
                    return false;
                }
            }

            #endregion

            #endregion

            #region Pet Exception
            if (SpellType == SpellType.Pet)
            {
                if (Core.Player.Pet == null)
                {
                    return false;
                }

                if (PetManager.PetMode != PetMode.Obey)
                {
                    if (!await Coroutine.Wait(1000, () => PetManager.DoAction("Obey", Core.Player)))
                    {
                        return false;
                    }
                    Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: Pet Obey");
                }

                #region IsMounted Check

                if (Core.Player.IsMounted)
                {
                    return false;
                }

                #endregion

                #region CanCast

                if (!PetManager.CanCast(Name, target))
                {
                    return false;
                }

                #endregion

                #region DoAction

                if (!await Coroutine.Wait(5000, () => PetManager.DoAction(Name, target)))
                {
                    return false;
                }

                #endregion

                Ultima.LastSpell = this;

                #region Recent Spell Add
                var key = target.ObjectId.ToString("X") + "-" + Name;
                var val = DateTime.UtcNow + DataManager.GetSpellData(Name).AdjustedCastTime + TimeSpan.FromSeconds(5);
                RecentSpell.Add(key, val);
                #endregion

                Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name);

                return true;
            }
            #endregion

            #region Card Exception

            if (SpellType == SpellType.Card)
            {
                if (!await Coroutine.Wait(1000, () => ActionManager.DoAction(ID, target)))
                {
                    return false;
                }
                Ultima.LastSpell = this;
                #region Recent Spell Add
                var key = target.ObjectId.ToString("X") + "-" + Name;
                var val = DateTime.UtcNow + DataManager.GetSpellData(3590).AdjustedCastTime + TimeSpan.FromSeconds(5);
                RecentSpell.Add(key, val);
                #endregion
                Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name);
                return true;
            }

            #endregion

            #region CanAttack Check

            if (!target.CanAttack &&
                CastType != CastType.Self)
            {
                switch (SpellType)
                {
                    case SpellType.Damage:
                    case SpellType.DoT:
                    case SpellType.Movement:
                    case SpellType.Cooldown:
                    case SpellType.Interrupt:
                    case SpellType.Execute:
                    case SpellType.Knockback:
                    case SpellType.Debuff:
                    case SpellType.Flank:
                    case SpellType.Behind:
                        return false;
                }
            }

            #endregion

            #region Ninjutsu Exception

            if (SpellType == SpellType.Ninjutsu ||
                SpellType == SpellType.Mudra)
            {
                #region Player Movement

                if (BotManager.Current.IsAutonomous)
                {
                    switch (ActionManager.InSpellInRangeLOS(2247, target))
                    {
                        case SpellRangeCheck.ErrorNotInLineOfSight:
                            Navigator.MoveTo(target.Location);
                            return false;
                        case SpellRangeCheck.ErrorNotInRange:
                            Navigator.MoveTo(target.Location);
                            return false;
                        /*case SpellRangeCheck.ErrorNotInFront:
                            if (!target.InLineOfSight())
                            {
                                Navigator.MoveTo(target.Location);
                                return false;
                            }
                            target.Face();
                            return false;*/
                        case SpellRangeCheck.Success:
                            if (MovementManager.IsMoving)
                            {
                                Navigator.PlayerMover.MoveStop();
                            }
                            break;
                    }
                }

                #endregion

                #region IsMounted Check

                if (Core.Player.IsMounted)
                {
                    return false;
                }

                #endregion

                #region CanCast

                if (Ultima.UltSettings.QueueSpells)
                {
                    if (!ActionManager.CanCastOrQueue(DataManager.GetSpellData(ID), target))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!ActionManager.CanCast(ID, target))
                    {
                        return false;
                    }
                }

                #endregion

                #region Wait For Animation

                if (Ultima.LastSpell.ID != null &&
                    Ultima.LastSpell.SpellType != SpellType.Ninjutsu &&
                    Ultima.LastSpell.SpellType != SpellType.Mudra)
                {
                    await Coroutine.Wait(1000, () => DataManager.GetSpellData(Ultima.LastSpell.ID).Cooldown.TotalMilliseconds <= DataManager.GetSpellData(Ultima.LastSpell.ID).AdjustedCooldown.TotalMilliseconds - 1000);
                }

                #endregion

                #region DoAction

                if (!await Coroutine.Wait(1000, () => ActionManager.DoAction(ID, target)))
                {
                    return false;
                }

                #endregion

                #region Wait For Cast Completion

                await Coroutine.Wait(2000, () => !ActionManager.CanCast(ID, target));

                #endregion

                Ultima.LastSpell = this;

                #region Recent Spell Add

                if (SpellType == SpellType.Mudra)
                {
                    var key = target.ObjectId.ToString("X") + "-" + Name;
                    var val = DateTime.UtcNow + DataManager.GetSpellData(ID).AdjustedCastTime + TimeSpan.FromSeconds(1);
                    RecentSpell.Add(key, val);
                }

                #endregion

                Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name);

                return true;
            }

            #endregion

            #region HasSpell Check

            if (!ActionManager.HasSpell(ID))
            {
                return false;
            }

            #endregion

            #region Player Movement

            if (BotManager.Current.IsAutonomous)
            {
                Stopwatch timer = new Stopwatch();
                switch (ActionManager.InSpellInRangeLOS(ID, target))
                {
                    case SpellRangeCheck.ErrorNotInLineOfSight:
                        Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: SpellRangeCheck.ErrorNotInLineOfSight :: moving closer");
                        while (!timer.IsRunning || timer.ElapsedMilliseconds < 10000)
                        {
                            if (!timer.IsRunning) timer.Start();
                            Navigator.MoveTo(target.Location);
                            await Coroutine.Yield();
                            if (Core.Me.CurrentTarget == null || !(Core.Me.CurrentTarget as BattleCharacter).IsAlive ||
                                Core.Me.Distance(Core.Me.CurrentTarget.Location) < 1 || ActionManager.InSpellInRangeLOS(ID, target) == SpellRangeCheck.Success) break;
                        }
                        timer.Reset();
                        return false;
                    case SpellRangeCheck.ErrorNotInRange:
                        Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: SpellRangeCheck.ErrorNotInRange :: moving closer");
                        while (!timer.IsRunning || timer.ElapsedMilliseconds < 10000)
                        {
                            if (!timer.IsRunning) timer.Start();
                            Navigator.MoveTo(target.Location);
                            await Coroutine.Yield();
                            if (Core.Me.CurrentTarget == null || !(Core.Me.CurrentTarget as BattleCharacter).IsAlive ||
                                Core.Me.Distance(Core.Me.CurrentTarget.Location) < 1 || ActionManager.InSpellInRangeLOS(ID, target) == SpellRangeCheck.Success)
                                break;
                        }
                        timer.Reset();
                        return false;
                    /*case SpellRangeCheck.ErrorNotInFront:
                        if (!target.InLineOfSight())
                        {
                            Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: SpellRangeCheck.ErrorNotInFront_NotInLineOfSight :: moving closer");
                            while (!timer.IsRunning || timer.ElapsedMilliseconds < 10000)
                            {
                                if (!timer.IsRunning) timer.Start();
                                Navigator.MoveTo(target.Location);
                                await Coroutine.Yield();
                                if (Core.Me.CurrentTarget == null || !(Core.Me.CurrentTarget as BattleCharacter).IsAlive ||
                                    Core.Me.Distance(Core.Me.CurrentTarget.Location) < 1 || ActionManager.InSpellInRangeLOS(ID, target) == SpellRangeCheck.Success)
                                    break;
                            }
                            timer.Reset();
                            return false;
                        }
                        Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: SpellRangeCheck.ErrorNotInFront :: facing target");
                        target.Face();
                        await Coroutine.Yield();
                        return false;*/
                    case SpellRangeCheck.Success:
                        if (CastType == CastType.TargetLocation &&
                            Core.Player.Distance2D(target) + Core.Player.CombatReach + target.CombatReach > 25)
                        {
                            Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: CastType.TargetLocation out of range :: moving closer");
                            while (!timer.IsRunning || timer.ElapsedMilliseconds < 10000)
                            {
                                if (!timer.IsRunning) timer.Start();
                                Navigator.MoveTo(target.Location);
                                await Coroutine.Yield();
                                if (Core.Me.CurrentTarget == null || !(Core.Me.CurrentTarget as BattleCharacter).IsAlive ||
                                    Core.Me.Distance(Core.Me.CurrentTarget.Location) < 1 || Core.Player.Distance2D(target) + Core.Player.CombatReach + target.CombatReach <= 25)
                                    break;
                            }
                            timer.Reset();
                            return false;
                        }
                        if (MovementManager.IsMoving || MovementManager.IsTurning)
                        {
                            Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: SpellRangeCheck.Success :: stopping movement");
                            Navigator.PlayerMover.MoveStop();
                        }
                        break;
                    case SpellRangeCheck.undefined:
                        Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: SpellRangeCheck.undefined");
                        break;
                    default:
                        Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " :: SpellRangeCheck.default");
                        break;
                }

                if (Core.Player.HasTarget &&
                    !MovementManager.IsMoving &&
                    Core.Player.IsMounted)
                {
                    if (MovementManager.IsFlying)
                    {
                        Logging.Write(Colors.Yellow, @"[Ultima] Currently flying; descending to try to land and dismount");

                        /*
                        DescendToResult descent = await ff14bot.Behavior.CommonTasks.DescendTo(Core.Me.Location.Y - 10);
                        while ((!timer.IsRunning || timer.ElapsedMilliseconds < 2000) && !MovementManager.IsFlying)
                        {
                            if (!timer.IsRunning) timer.Start();
                            descent = await ff14bot.Behavior.CommonTasks.DescendTo(Core.Me.Location.Y - 10);
                            await Coroutine.Yield();
                        }
                        timer.Reset();
                        //await Coroutine.Wait(2000, () => !MovementManager.IsFlying);
                        Logging.Write(Colors.OrangeRed, @"[Ultima] Landing result is " + descent.ToString());
                        MovementManager.StopDescending();
                        if (MovementManager.IsFlying)
                        {
                            Logging.Write(Colors.OrangeRed, @"[Ultima] Landing took longer then 2 seconds; moving closer and retrying");
                            while (!timer.IsRunning || timer.ElapsedMilliseconds < 1000)
                            {
                                if (!timer.IsRunning) timer.Start();
                                Navigator.MoveTo(target.Location);
                                await Coroutine.Yield();
                                if (Core.Me.CurrentTarget == null || !(Core.Me.CurrentTarget as BattleCharacter).IsAlive ||
                                    Core.Me.Distance(Core.Me.CurrentTarget.Location) < 1)
                                    break;
                            }
                            timer.Reset();
                            return false;
                        }
                        */
                        
                        MovementManager.StartDescending();
                        await Coroutine.Wait(2000, () => !MovementManager.IsFlying);
                        MovementManager.StopDescending();
                        if (MovementManager.IsFlying)
                        {
                            Logging.Write(Colors.OrangeRed, @"[Ultima] Landing took longer then 2 seconds; moving closer and retrying");
                            while (!timer.IsRunning || timer.ElapsedMilliseconds < 1000)
                            {
                                if (!timer.IsRunning) timer.Start();
                                Navigator.MoveTo(target.Location);
                                await Coroutine.Yield();
                                if (Core.Me.CurrentTarget == null || !(Core.Me.CurrentTarget as BattleCharacter).IsAlive ||
                                    Core.Me.Distance(Core.Me.CurrentTarget.Location) < 1)
                                    break;
                            }
                            timer.Reset();
                            return false;
                        }
                        
                    }
                    Logging.Write(Colors.Yellow, @"[Ultima] Dismounting...");
                    ActionManager.Dismount();
                    await Coroutine.Sleep(1000);
                }
                else if (Core.Player.HasTarget &&
                    MovementManager.IsMoving &&
                    Core.Player.IsMounted)
                {
                    Logging.Write(Colors.Yellow, @"[Ultima] Waiting to stop before dismounting");
                    return false;
                }
            }

            #endregion

            #region IsMounted Check

            if (Core.Player.IsMounted)
            {
                return false;
            }

            #endregion

            #region CanCast Check

            switch (CastType)
            {
                case CastType.TargetLocation:
                case CastType.SelfLocation:
                    if (!ActionManager.CanCastLocation(ID, target.Location))
                    {
                        return false;
                    }
                    break;
                default:
                    if (Ultima.UltSettings.QueueSpells)
                    {
                        if (!ActionManager.CanCastOrQueue(DataManager.GetSpellData(ID), target))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!ActionManager.CanCast(ID, target))
                        {
                            return false;
                        }
                    }
                break;
            }

            if (MovementManager.IsMoving &&
                DataManager.GetSpellData(ID).AdjustedCastTime.TotalMilliseconds > 0)
            {
                if (!BotManager.Current.IsAutonomous)
                {
                    return false;
                }
                Navigator.PlayerMover.MoveStop();
            }

            #endregion

            #region InView Check

            if (GameSettingsManager.FaceTargetOnAction == false &&
                CastType == CastType.Target &&
                SpellType != SpellType.Heal &&
                SpellType != SpellType.Defensive &&
                !Helpers.InView(Core.Player.Location, Core.Player.Heading, target.Location))
            {
                return false;
            }

            #endregion

            #region Off-GCD Check
            if (GCDType == GCDType.Off)
            {
                switch (Core.Player.CurrentJob)
                {
                    case ClassJobType.Arcanist:
                    case ClassJobType.Scholar:
                    case ClassJobType.Summoner:
                    if (Core.Player.ClassLevel >= 38 &&
                        Core.Player.CurrentManaPercent > 40 &&
                        ID != 166 &&
                        DataManager.GetSpellData(163).Cooldown.TotalMilliseconds < 500)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Archer:
                    case ClassJobType.Bard:
                    if (DataManager.GetSpellData(97).Cooldown.TotalMilliseconds <= 700)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Astrologian:
                    if (DataManager.GetSpellData(3596).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Conjurer:
                    case ClassJobType.WhiteMage:
                    if (DataManager.GetSpellData(119).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.DarkKnight:
                    if (DataManager.GetSpellData(3617).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Gladiator:
                    case ClassJobType.Paladin:
                    if (DataManager.GetSpellData(9).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Lancer:
                    case ClassJobType.Dragoon:
                    if (DataManager.GetSpellData(75).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Machinist:
                    if (DataManager.GetSpellData(2866).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Marauder:
                    case ClassJobType.Warrior:
                    if (DataManager.GetSpellData(31).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Pugilist:
                    case ClassJobType.Monk:
                    if (DataManager.GetSpellData(53).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Rogue:
                    case ClassJobType.Ninja:
                    if (DataManager.GetSpellData(2240).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                    case ClassJobType.Thaumaturge:
                    case ClassJobType.BlackMage:
                    if (DataManager.GetSpellData(142).Cooldown.TotalMilliseconds <= 1000)
                    {
                        return false;
                    }
                    break;
                }
            }
            #endregion

            #region DoAction
            switch (CastType)
            {
                case CastType.TargetLocation:
                    if (Ultima.UltSettings.RandomCastLocation)
                    {
                        var rndx = (target.CombatReach * _rnd.NextDouble() * GetMultiplier());
                        var rndz = (target.CombatReach * _rnd.NextDouble() * GetMultiplier());
                        var rndxz = new Vector3((float)rndx, 0f, (float)rndz);
                        var tarloc = target.Location;
                        var rndloc = tarloc + rndxz;

                        if (!await Coroutine.Wait(1000, () => ActionManager.DoActionLocation(ID, rndloc)))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!await Coroutine.Wait(1000, () => ActionManager.DoActionLocation(ID, target.Location)))
                        {
                            return false;
                        }
                    }
                    break;
                case CastType.SelfLocation:
                    if (Ultima.UltSettings.RandomCastLocation)
                    {
                        var rndx = ((1f * _rnd.NextDouble() + 1f) * GetMultiplier());
                        var rndz = ((1f * _rnd.NextDouble() + 1f) * GetMultiplier());
                        var rndxz = new Vector3((float)rndx, 0f, (float)rndz);
                        var tarloc = target.Location;
                        var rndloc = tarloc + rndxz;

                        if (!await Coroutine.Wait(1000, () => ActionManager.DoActionLocation(ID, rndloc)))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!await Coroutine.Wait(1000, () => ActionManager.DoActionLocation(ID, target.Location)))
                        {
                            return false;
                        }
                    }
                    break;
                default:
                    if (!await Coroutine.Wait(1000, () => ActionManager.DoAction(ID, target)))
                    {
                        return false;
                    }
                    break;
            }
            #endregion

            #region Wait For Cast Completion

            switch (CastType)
            {
                case CastType.SelfLocation:
                case CastType.TargetLocation:
                    await Coroutine.Wait(3000, () => !ActionManager.CanCastLocation(ID, target.Location));
                    break;
                default:
                    await Coroutine.Wait(3000, () => !ActionManager.CanCast(ID, target));
                    break;
            }

            #endregion

            Ultima.LastSpell = this;

            #region Recent Spell Add
            if (SpellType != SpellType.Damage &&
                SpellType != SpellType.Heal &&
                SpellType != SpellType.AoE &&
                SpellType != SpellType.Behind &&
                SpellType != SpellType.Flank &&
                await SpellNotInterupted(this))
            {
                var key = target.ObjectId.ToString("X") + "-" + Name;
                var val = DateTime.UtcNow + DataManager.GetSpellData(ID).AdjustedCastTime + TimeSpan.FromSeconds(5);
                RecentSpell.Add(key, val);
            }
            if (SpellType == SpellType.Heal)
            {
                var key = target.ObjectId.ToString("X") + "-" + Name;
                var val = DateTime.UtcNow + DataManager.GetSpellData(ID).AdjustedCastTime + TimeSpan.FromSeconds(3);
                RecentSpell.Add(key, val);
            }
            #endregion

            Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name);

            return true;
        }
        public async Task<bool> SpellNotInterupted(Spell spell)
        {
            TimeSpan adjustedCastTime = DataManager.GetSpellData(spell.ID).AdjustedCastTime;
            if (spell.SpellType == SpellType.DoT && adjustedCastTime.TotalMilliseconds > 0)
            {
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                await Coroutine.Wait(adjustedCastTime, () => Core.Me.IsCasting);
                while (timer.ElapsedMilliseconds < adjustedCastTime.TotalMilliseconds)// - 400)
                {
                    if (!Core.Me.IsCasting && (Core.Target == null || ActionManager.CanCast(spell.ID, Core.Target)))// || spell.CastType == CastType.TargetLocation))
                    {
                        Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " ended due to IsCasting " + Core.Me.IsCasting + " while cast timer is at " + 
                            timer.ElapsedMilliseconds  + " which is " + (adjustedCastTime.TotalMilliseconds - timer.ElapsedMilliseconds) + 
                            " less then " + adjustedCastTime.TotalMilliseconds);
                        return false;
                    }
                    Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " yielding");
                    await Coroutine.Yield();
                }
                Logging.Write(Colors.OrangeRed, @"[Ultima] Ability: " + Name + " timer of " + adjustedCastTime.TotalMilliseconds + "expired");
            }
            return true;
        }
    }
}