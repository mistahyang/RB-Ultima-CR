using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class RedMage
    {
        public override async Task<bool> PreCombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            return false;
        }
    }
}