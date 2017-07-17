using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Paladin
    {
        public override async Task<bool> PreCombatBuff()
        {
            if (await Ultima.FoodBuff()) return true;
            return false;
        }
    }
}