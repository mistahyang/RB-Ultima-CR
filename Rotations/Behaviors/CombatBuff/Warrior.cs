using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Warrior
    {
        public override async Task<bool> CombatBuff()
        {
            if (await ThrillOfBattle()) return true;
            if (await Anticipation()) return true;
            if (await Convalescence()) return true;
            if (await Vengeance()) return true;
            if (await Rampart()) return true;
            return await Equilibrium();
        }
    }
}