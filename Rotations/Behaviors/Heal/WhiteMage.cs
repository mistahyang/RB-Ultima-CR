using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class WhiteMage
    {
        public override async Task<bool> Heal()
        {
            if (await Benediction()) return true;
            if (await Tetragrammaton()) return true;
            if (await Esuna()) return true;
            if (await Medica()) return true;
            if (await Regen()) return true;
            if (await CureIII()) return true;
            if (await CureIIFREECURE()) return true;
            if (await CureII()) return true;
            if (await Raise()) return true;
            if (await CureREGEN()) return true;
            return await Cure();
        }
    }
}