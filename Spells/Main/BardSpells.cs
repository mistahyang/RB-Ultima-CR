﻿
namespace UltimaCR.Spells.Main
{
    public class BardSpells : ArcherSpells
    {
        private CrossClass.BardSpells.Crossclass _crossClass;
        public new CrossClass.BardSpells.Crossclass CrossClass
        {
            get { return _crossClass ?? (_crossClass = new CrossClass.BardSpells.Crossclass()); }
        }

        private PVP.BardSpells.Pvp _pvp;
        public new PVP.BardSpells.Pvp PvP
        {
            get { return _pvp ?? (_pvp = new PVP.BardSpells.Pvp()); }
        }

        private Spell _magesballad;
        public Spell MagesBallad
        {
            get
            {
                return _magesballad ??
                       (_magesballad =
                           new Spell
                           {
                               Name = "Mage's Ballad",
                               ID = 114,
                               Level = 30,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _foerequiem;
        public Spell FoeRequiem
        {
            get
            {
                return _foerequiem ??
                       (_foerequiem =
                           new Spell
                           {
                               Name = "Foe Requiem",
                               ID = 115,
                               Level = 35,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Debuff,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _armyspaeon;
        public Spell ArmysPaeon
        {
            get
            {
                return _armyspaeon ??
                       (_armyspaeon =
                           new Spell
                           {
                               Name = "Army's Paeon",
                               ID = 116,
                               Level = 40,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _rainofdeath;
        public Spell RainOfDeath
        {
            get
            {
                return _rainofdeath ??
                       (_rainofdeath =
                           new Spell
                           {
                               Name = "Rain of Death",
                               ID = 117,
                               Level = 45,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _battlevoice;
        public Spell BattleVoice
        {
            get
            {
                return _battlevoice ??
                       (_battlevoice =
                           new Spell
                           {
                               Name = "Battle Voice",
                               ID = 118,
                               Level = 50,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Aura,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _refresh;
        public Spell Refresh
        {
            get
            {
                return _refresh ??
                       (_refresh =
                           new Spell
                           {
                               Name = "Refresh",
                               ID = 7556,
                               Level = 36,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Aura,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _wanderersminuet;
        public Spell WanderersMinuet
        {
            get
            {
                return _wanderersminuet ??
                       (_wanderersminuet =
                           new Spell
                           {
                               Name = "the Wanderer's Minuet",
                               ID = 3559,
                               Level = 52,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _empyrealarrow;
        public Spell EmpyrealArrow
        {
            get
            {
                return _empyrealarrow ??
                       (_empyrealarrow =
                           new Spell
                           {
                               Name = "Empyreal Arrow",
                               ID = 3558,
                               Level = 54,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _ironjaws;
        public Spell IronJaws
        {
            get
            {
                return _ironjaws ??
                       (_ironjaws =
                           new Spell
                           {
                               Name = "Iron Jaws",
                               ID = 3560,
                               Level = 56,
                               GCDType = GCDType.On,
                               SpellType = SpellType.DoT,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _thewardenspaean;
        public Spell TheWardensPaean
        {
            get
            {
                return _thewardenspaean ??
                       (_thewardenspaean =
                           new Spell
                           {
                               Name = "The Warden's Paean",
                               ID = 3561,
                               Level = 58,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Defensive,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _naturesminne;
        public Spell NaturesMinne
        {
            get
            {
                return _naturesminne ??
                       (_naturesminne =
                           new Spell
                           {
                               Name = "Nature's Minne",
                               ID = 7408,
                               Level = 66,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Defensive,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _causticbite;
        public Spell CausticBite
        {
            get
            {
                return _causticbite ??
                       (_causticbite =
                           new Spell
                           {
                               Name = "Caustic Bite",
                               ID = 7406,
                               Level = 64,
                               GCDType = GCDType.On,
                               SpellType = SpellType.DoT,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _stormbite;
        public Spell Stormbite
        {
            get
            {
                return _stormbite ??
                       (_stormbite =
                           new Spell
                           {
                               Name = "Stormbite",
                               ID = 7407,
                               Level = 64,
                               GCDType = GCDType.On,
                               SpellType = SpellType.DoT,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _refulgentarrow;
        public Spell RefulgentArrow
        {
            get
            {
                return _refulgentarrow ??
                       (_refulgentarrow =
                           new Spell
                           {
                               Name = "Refulgent Arrow",
                               ID = 7409,
                               Level = 70,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _sidewinder;
        public Spell Sidewinder
        {
            get
            {
                return _sidewinder ??
                       (_sidewinder =
                           new Spell
                           {
                               Name = "Sidewinder",
                               ID = 3562,
                               Level = 60,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Cooldown,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _pitchperfect;
        public Spell PitchPerfect
        {
            get
            {
                return _pitchperfect ??
                       (_pitchperfect =
                           new Spell
                           {
                               Name = "Pitch Perfect",
                               ID = 7404,
                               Level = 52,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
    }
}