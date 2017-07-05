using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class WhiteMage : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
           	if (await Assize()) return true;
           	if (await AeroIII()) return true;
            	if (await AeroII()) return true;
            	if (await Aero()) return true;
           	if (await MedicaII()) return true;
            	if (await StoneIII()) return true;
            	if (await StoneII()) return true;
            	if (await Holy()) return true;
            	return await Stone();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
           	if (await Assize()) return true;
			if (await Virus()) return true;
            	if (await FluidAura()) return true;
           	if (await AeroIII()) return true;
            	if (await AeroII()) return true;
            	if (await Aero()) return true;
           	if (await MedicaII()) return true;
            	if (await StoneIII()) return true;
            	if (await StoneII()) return true;
            	return await Stone();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
           	if (await Assize()) return true;
            	if (await StoneIII()) return true;
            	if (await StoneII()) return true;
            	return await Stone();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}