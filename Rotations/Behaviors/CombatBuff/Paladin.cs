using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Paladin
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await HallowedGround()) return true;
            if (await Rampart()) return true;
            if (await Sentinel()) return true;
            if (await Bulwark()) return true;
            if (await Convalescence()) return true;
		    return await Sheltron();
        }
    }
}