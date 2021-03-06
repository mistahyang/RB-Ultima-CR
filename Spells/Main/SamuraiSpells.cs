
namespace UltimaCR.Spells.Main
{
    public class SamuraiSpells
    {
        private CrossClass.SamuraiSpells.Crossclass _crossClass;
        public CrossClass.SamuraiSpells.Crossclass CrossClass
        {
            get { return _crossClass ?? (_crossClass = new CrossClass.SamuraiSpells.Crossclass()); }
        }

        private PVP.SamuraiSpells.Pvp _pvp;
        public PVP.SamuraiSpells.Pvp PvP
        {
            get { return _pvp ?? (_pvp = new PVP.SamuraiSpells.Pvp()); }
        }

        private Spell _hakaze;
        public Spell Hakaze
        {
            get
            {
                return _hakaze ??
                       (_hakaze =
                           new Spell
                           {
                               Name = "Hakaze",
                               ID = 7477,
                               Level = 1,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _jinpu;
        public Spell Jinpu
        {
            get
            {
                return _jinpu ??
                       (_jinpu =
                           new Spell
                           {
                               Name = "Jinpu",
                               ID = 7478,
                               Level = 4,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Debuff,
                               CastType = CastType.Target
                           });
            }
        }
        //Hissatsu: Kaiten Action Id:7494
        private Spell _kaiten;
        public Spell Kaiten
        {
            get
            {
                return _kaiten ??
                       (_kaiten =
                           new Spell
                           {
                               Name = "Kaiten",
                               ID = 7494,
                               Level = 52,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _shifu;
        public Spell Shifu
        {
            get
            {
                return _shifu ??
                       (_shifu =
                           new Spell
                           {
                               Name = "Shifu",
                               ID = 7479,
                               Level = 18,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _yukikaze;
        public Spell Yukikaze
        {
            get
            {
                return _yukikaze ??
                       (_yukikaze =
                           new Spell
                           {
                               Name = "Yukikaze",
                               ID = 7480,
                               Level = 50,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Debuff,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _gekko;
        public Spell Gekko
        {
            get
            {
                return _gekko ??
                       (_gekko =
                           new Spell
                           {
                               Name = "Gekko",
                               ID = 7481,
                               Level = 30,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Aura,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _kasha;
        public Spell Kasha
        {
            get
            {
                return _kasha ??
                       (_kasha =
                           new Spell
                           {
                               Name = "Kasha",
                               ID = 7482,
                               Level = 40,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Aura,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _fuga;
        public Spell Fuga
        {
            get
            {
                return _fuga ??
                       (_fuga =
                           new Spell
                           {
                               Name = "Fuga",
                               ID = 7483,
                               Level = 26,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _mangetsu;
        public Spell Mangetsu
        {
            get
            {
                return _mangetsu ??
                       (_mangetsu =
                           new Spell
                           {
                               Name = "Mangetsu",
                               ID = 7484,
                               Level = 35,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _oka;
        public Spell Oka
        {
            get
            {
                return _oka ??
                       (_oka =
                           new Spell
                           {
                               Name = "Oka",
                               ID = 7485,
                               Level = 45,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _enpi;
        public Spell Enpi
        {
            get
            {
                return _enpi ??
                       (_enpi =
                           new Spell
                           {
                               Name = "Enpi",
                               ID = 7486,
                               Level = 15,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _midaresetsugekka;
        public Spell MidareSetsugekka
        {
            get
            {
                return _midaresetsugekka ??
                       (_midaresetsugekka =
                           new Spell
                           {
                               Name = "Midare Setsugekka",
                               ID = 7487,
                               Level = 66,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _tenkagoken;
        public Spell TenkaGoken
        {
            get
            {
                return _tenkagoken ??
                       (_tenkagoken =
                           new Spell
                           {
                               Name = "Tenka Goken",
                               ID = 7488,
                               Level = 1,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _higanbana;
        public Spell Higanbana
        {
            get
            {
                return _higanbana ??
                       (_higanbana =
                           new Spell
                           {
                               Name = "Higanbana",
                               ID = 7489,
                               Level = 1,
                               GCDType = GCDType.On,
                               SpellType = SpellType.DoT,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _thirdeye;
        public Spell ThirdEye
        {
            get
            {
                return _thirdeye ??
                       (_thirdeye =
                           new Spell
                           {
                               Name = "Third Eye",
                               ID = 7498,
                               Level = 6,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Aura,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _meikyoshisui;
        public Spell MeikyoShisui
        {
            get
            {
                return _meikyoshisui ??
                       (_meikyoshisui =
                           new Spell
                           {
                               Name = "Meikyo Shisui",
                               ID = 7499,
                               Level = 50,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Cooldown,
                               CastType = CastType.Self
                           });
            }
        }
        private Spell _ageha;
        public Spell Ageha
        {
            get
            {
                return _ageha ??
                       (_ageha =
                           new Spell
                           {
                               Name = "Ageha",
                               ID = 7500,
                               Level = 10,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Execute,
                               CastType = CastType.Target
                           });
            }
        }
        private Spell _iaijutsu;
        public Spell Iaijutsu
        {
            get
            {
                return _iaijutsu ??
                       (_iaijutsu =
                           new Spell
                           {
                               Name = "Iaijutsu",
                               ID = 7867,
                               Level = 30,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //Merciful Eyes Action Id:7502
        private Spell _mercifuleyes;
        public Spell MercifulEyes
        {
            get
            {
                return _mercifuleyes ??
                       (_mercifuleyes =
                           new Spell
                           {
                               Name = "Merciful Eyes",
                               ID = 7502,
                               Level = 6,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Defensive,
                               CastType = CastType.Self
                           });
            }
        }
        //Hissatsu: Shinten Action:7490
        private Spell _shinten;
        public Spell Shinten
        {
            get
            {
                return _shinten ??
                       (_shinten =
                           new Spell
                           {
                               Name = "Hissatsu: Shinten",
                               ID = 7490,
                               Level = 62,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //Hissatsu: Guren Action Id:7496
        private Spell _guren;
        public Spell Guren
        {
            get
            {
                return _guren ??
                       (_guren =
                           new Spell
                           {
                               Name = "Hissatsu: Guren",
                               ID = 7496,
                               Level = 70,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Damage,
                               CastType = CastType.Target
                           });
            }
        }
        //Meditate Action Id:7497
        private Spell _meditate;
        public Spell Meditate
        {
            get
            {
                return _meditate ??
                       (_meditate =
                           new Spell
                           {
                               Name = "Meditate",
                               ID = 7497,
                               Level = 60,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Self
                           });
            }
        }
        //7495
        private Spell _hagakure;
        public Spell Hagakure
        {
            get
            {
                return _hagakure ??
                       (_hagakure =
                           new Spell
                           {
                               Name = "Hagakure",
                               ID = 7495,
                               Level = 68,
                               GCDType = GCDType.On,
                               SpellType = SpellType.Buff,
                               CastType = CastType.Self
                           });
            }
        }
    }
}