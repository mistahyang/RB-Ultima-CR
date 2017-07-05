using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Ninja
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await KissOfTheViper()) return true;
            if (await KissOfTheWasp()) return true;
            if (await Assassinate()) return true;
            if (await Huton()) return true;
            if (await Kassatsu()) return true;
            if (await Duality()) return true;
            if (await BloodForBlood()) return true;
            if (await InternalRelease()) return true;
            if (await Mug()) return true;
            if (await DreamWithinADream()) return true;
            if (await Goad()) return true;
            if (await Invigorate()) return true;
            if (await ShadeShift()) return true;
            return await SecondWind();
        }
    }
}