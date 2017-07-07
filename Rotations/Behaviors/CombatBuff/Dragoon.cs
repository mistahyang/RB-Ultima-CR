using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Dragoon
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await DragonSight()) return true;
            if (await Goad()) return true;
            if (await Invigorate()) return true;
            if (await SecondWind()) return true;
            return await Bloodbath();
        }
    }
}