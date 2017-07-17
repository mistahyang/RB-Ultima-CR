
namespace UltimaCR.Spells.Main
{
    public class RedMageSpells
    {
        private CrossClass.RedMageSpells.Crossclass _crossClass;
        public CrossClass.RedMageSpells.Crossclass CrossClass
        {
            get { return _crossClass ?? (_crossClass = new CrossClass.RedMageSpells.Crossclass()); }
        }

        private PVP.RedMageSpells.Pvp _pvp;
        public PVP.RedMageSpells.Pvp PvP
        {
            get { return _pvp ?? (_pvp = new PVP.RedMageSpells.Pvp()); }
        }

        
        //[00:24:11.563 N] Action Name:Jolt Action Id:7503
        private Spell _jolt;
        public Spell Jolt
        {
            get
            {
                return _jolt ??
                       (_jolt =
                           new Spell
                           {
                               Name = "Jolt",
                               ID = 7503,
                               Level = 2,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Riposte Action Id:7504
        private Spell _riposte;
        public Spell Riposte
        {
            get
            {
                return _riposte ??
                       (_riposte =
                           new Spell
                           {
                               Name = "Riposte",
                               ID = 7504,
                               Level = 1,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Verthunder Action Id:7505
        private Spell _verthunder;
        public Spell Verthunder
        {
            get
            {
                return _verthunder ??
                       (_verthunder =
                           new Spell
                           {
                               Name = "Verthunder",
                               ID = 7505,
                               Level = 4,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Corps-a-corps Action Id:7506
        private Spell _corpsacorps;
        public Spell CorpsACorps
        {
            get
            {
                return _corpsacorps ??
                       (_corpsacorps =
                           new Spell
                           {
                               Name = "Corps-a-corps",
                               ID = 7506,
                               Level = 6,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Veraero Action Id:7507
        private Spell _veraero;
        public Spell Veraero
        {
            get
            {
                return _veraero ??
                       (_veraero =
                           new Spell
                           {
                               Name = "Veraero",
                               ID = 7507,
                               Level = 10,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Tether Action Id:7508
        private Spell _tether;
        public Spell Tether
        {
            get
            {
                return _tether ??
                       (_tether =
                           new Spell
                           {
                               Name = "Tether",
                               ID = 7508,
                               Level = 15,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Scatter Action Id:7509
        private Spell _scatter;
        public Spell Scatter
        {
            get
            {
                return _scatter ??
                       (_scatter =
                           new Spell
                           {
                               Name = "Scatter",
                               ID = 7509,
                               Level = 18,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Verfire Action Id:7510
        private Spell _verfire;
        public Spell Verfire
        {
            get
            {
                return _verfire ??
                       (_verfire =
                           new Spell
                           {
                               Name = "Verfire",
                               ID = 7510,
                               Level = 26,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Verstone Action Id:7511
        private Spell _verstone;
        public Spell Verstone
        {
            get
            {
                return _verstone ??
                       (_verstone =
                           new Spell
                           {
                               Name = "Verstone",
                               ID = 7511,
                               Level = 30,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Zwerchhau Action Id:7512
        private Spell _zwerchhau;
        public Spell Zwerchhau
        {
            get
            {
                return _zwerchhau ??
                       (_zwerchhau =
                           new Spell
                           {
                               Name = "Zwerchhau",
                               ID = 7512,
                               Level = 35,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Displacement Action Id:7515
        private Spell _displacement;
        public Spell Displacement
        {
            get
            {
                return _displacement ??
                       (_displacement =
                           new Spell
                           {
                               Name = "Displacement",
                               ID = 7515,
                               Level = 40,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Redoublement Action Id:7516
        private Spell _redoublement;
        public Spell Redoublement
        {
            get
            {
                return _redoublement ??
                       (_redoublement =
                           new Spell
                           {
                               Name = "Redoublement",
                               ID = 7516,
                               Level = 50,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Fleche Action Id:7517
        private Spell _fleche;
        public Spell Fleche
        {
            get
            {
                return _fleche ??
                       (_fleche =
                           new Spell
                           {
                               Name = "Fleche",
                               ID = 7517,
                               Level = 45,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //[00:24:11.563 N] Action Name:Acceleration Action Id:7518
        private Spell _acceleration;
        public Spell Acceleration
        {
            get
            {
                return _acceleration ??
                       (_acceleration =
                           new Spell
                           {
                               Name = "Acceleration",
                               ID = 7518,
                               Level = 50,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Self
                           });
            }
        }
        //7514
        private Spell _vercure;
        public Spell Vercure
        {
            get
            {
                return _vercure ??
                       (_vercure =
                           new Spell
                           {
                               Name = "Vercure",
                               ID = 7514,
                               Level = 54,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Heal,
                               CastType = CastType.Target
                           });
            }
        }
    }
}