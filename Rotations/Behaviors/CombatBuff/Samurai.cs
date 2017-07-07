using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Samurai
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Goad()) return true;
            if (await ThirdEye()) return true;
            return await Invigorate();
        }
    }
}