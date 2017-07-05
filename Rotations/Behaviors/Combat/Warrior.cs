using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Warrior : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                if (await Tomahawk()) return true;
                if (await Berserk()) return true;
            	if (await Infuriate()) return true;  
                if (await InnerBeast()) return true;
	            if (await FellCleave()) return true;
                if (await Upheaval1()) return true;
	            if (await Upheaval2()) return true;
                //if (await Overpower()) return true;
	            //if (await SteelCyclone()) return true;
	            //if (await Decimate()) return true;
                if (await ButchersBlock()) return true;
                if (await StormsEye()) return true;
                if (await StormsPath()) return true;
                if (await SkullSunder()) return true;
                if (await Maim()) return true;
                return await HeavySwing();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
                if (await Tomahawk()) return true;
                if (await Berserk()) return true;
                if (await InnerRelease()) return true;
            	if (await Infuriate()) return true;  
                if (await InnerBeast()) return true;
	            if (await FellCleave()) return true;
                if (await Upheaval1()) return true;
	            if (await Upheaval2()) return true;
                //if (await Overpower()) return true;
	            //if (await SteelCyclone()) return true;
	            //if (await Decimate()) return true;
                if (await ButchersBlock()) return true;
                if (await StormsEye()) return true;
                if (await StormsPath()) return true;
                if (await Maim()) return true;
                return await HeavySwing();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await SummonChocobo()) return true;
                if (await Tomahawk()) return true;
                if (await Berserk()) return true;
            	if (await Infuriate()) return true;  
	            if (await FellCleave()) return true;
                if (await Upheaval1()) return true;
	            if (await Upheaval2()) return true;
                //if (await Overpower()) return true;
	            //if (await SteelCyclone()) return true;
	            //if (await Decimate()) return true;
                if (await ButchersBlock()) return true;
                if (await StormsEye()) return true;
                if (await StormsPath()) return true;
                if (await Maim()) return true;
                return await HeavySwing();
            }
            return false;

        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}