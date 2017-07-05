using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Machinist
    {
        public override async Task<bool> PreCombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await QuickReload()) return true;
            if (await RookAutoturret()) return true;
            if (await BishopAutoturret()) return true;
            return await GaussBarrel();
        }
    }
}