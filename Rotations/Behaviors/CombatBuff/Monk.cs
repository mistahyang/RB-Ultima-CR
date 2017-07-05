using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Monk
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await FistsOfFire()) return true;
            if (await FistsOfEarth()) return true;
            if (await FistsOfWind()) return true;
            if (await Purification()) return true;
            if (await Invigorate()) return true;
            if (await PerfectBalance()) return true;
            if (await BloodForBlood()) return true;
            if (await InternalRelease()) return true;
            if (await TheForbiddenChakra()) return true;
	    if (await TornadoKick()) return true;
            if (await ShoulderTackle()) return true;
            if (await MercyStroke()) return true;
            if (await SecondWind()) return true;
            if (await Bloodbath()) return true;
            if (await KeenFlurry()) return true;
            if (await Featherfoot()) return true;
            return await FormShift();
        }
    }
}