using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Monk : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                if (await Rockbreaker()) return true;
                if (await SnapPunch()) return true;
            	if (await ElixirField()) return true;
            	if (await HowlingFist()) return true;
            	if (await SteelPeak()) return true;
                if (await TrueStrike()) return true;
                if (await TwinSnakes()) return true;
                if (await ArmOfTheDestroyer()) return true;
                if (await DragonKick()) return true;
            	if (await Bootshine()) return true;
                return await Meditation();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
                if (await Demolish()) return true;
                if (await SnapPunch()) return true;
            	if (await ElixirField()) return true;
            	if (await HowlingFist()) return true;
            	if (await SteelPeak()) return true;
                if (await TouchOfDeath()) return true;
                if (await TwinSnakes()) return true;
                if (await DragonKick()) return true;
                if (await TrueStrike()) return true;
            	if (await Bootshine()) return true;
                return await Meditation();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await Demolish()) return true;
                if (await SnapPunch()) return true;
                if (await TouchOfDeath()) return true;
                if (await TrueStrike()) return true;
                if (await TwinSnakes()) return true;
            	if (await Bootshine()) return true;
                return await Meditation();
            }
            return false;

        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}