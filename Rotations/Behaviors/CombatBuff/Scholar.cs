using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Scholar
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Protect()) return true;
            if (await Aetherflow()) return true;
            if (await LucidDreaming()) return true;
            return await Summon();
        }
    }
}