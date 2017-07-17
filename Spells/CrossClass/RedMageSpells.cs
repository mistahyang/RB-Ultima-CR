
namespace UltimaCR.Spells.CrossClass
{
    public class RedMageSpells
    {
        public class Crossclass
        {
            #region Cross

            //[00:24:11.563 N] Action Name:Lucid Dreaming Action Id:7562
            private Spell _luciddreaming;
            public Spell LucidDreaming
            {
                get
                {
                    return _luciddreaming ??
                           (_luciddreaming =
                               new Spell
                               {
                                   Name = "Lucid Dreaming",
                                   ID = 7562,
                                   Level = 24,
                                   GCDType = GCDType.Off,
                                   SpellType = SpellType.Buff,
                                   CastType = CastType.Self
                               });
                }
            }
            //[00:24:11.563 N] Action Name:Surecast Action Id:7559
            private Spell _surecast;
            public Spell Surecast
            {
                get {
                    return _surecast ??
                           (_surecast =
                               new Spell
                               {
                                   Name = "Surecast",
                                   ID = 7559,
                                   Level = 44,
                                   GCDType = GCDType.On,
                                   SpellType = SpellType.Buff,
                                   CastType = CastType.Self
                               });
                }
            }
            //[00:24:11.563 N] Action Name:Erase Action Id:7566
            private Spell _erase;
            public Spell Erase
            {
                get
                {
                    return _erase ??
                           (_erase =
                               new Spell
                               {
                                   Name = "Erase",
                                   ID = 7566,
                                   Level = 48,
                                   GCDType = GCDType.On,
                                   SpellType = SpellType.Buff,
                                   CastType = CastType.Self
                               });
                }
            }
            //[00:24:11.563 N] Action Name:Apocatastasis Action Id:7563
            private Spell _apocatastasis;
            public Spell Apocatastasis
            {
                get {
                    return _apocatastasis ??
                           (_apocatastasis =
                               new Spell
                               {
                                   Name = "Apocatastasis",
                                   ID = 7563,
                                   Level = 40,
                                   GCDType = GCDType.On,
                                   SpellType = SpellType.Defensive,
                                   CastType = CastType.Target
                               });
                }
            }
            //[00:24:11.563 N] Action Name:Swiftcast Action Id:7561
            private Spell _swiftcast;
            public Spell Swiftcast
            {
                get {
                    return _swiftcast ??
                           (_swiftcast =
                               new Spell
                               {
                                   Name = "Swiftcast",
                                   ID = 7561,
                                   Level = 32,
                                   GCDType = GCDType.On,
                                   SpellType = SpellType.Buff,
                                   CastType = CastType.Self
                               });
                }
            }
            
            
            
            #endregion
        }
    }
}