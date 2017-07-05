using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Bard
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await Refresh()) return true;
            if (await Invigorate()) return true;
            return await SecondWind();
        }
    }
}