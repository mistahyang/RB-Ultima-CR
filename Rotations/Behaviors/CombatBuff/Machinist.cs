using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Machinist
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await GaussBarrel()) return true;
            if (await RookAutoturret()) return true;
            if (await BishopAutoturret()) return true;
            return await Invigorate();
        }
    }
}