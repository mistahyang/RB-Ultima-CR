using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class DarkKnight : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
		if (await AbyssalDrain()) return true;
            	if (await Unmend()) return true;
            	if (await Unleash()) return true;
            	if (await SpinningSlash()) return true;
            	if (await SyphonStrike()) return true;
            	if (await CarveAndSpit()) return true;
            	if (await Souleater()) return true;
            	if (await SaltedEarth()) return true;
            	if (await DarkPassenger()) return true;
            	if (await PowerSlash()) return true;
            	return await HardSlash();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
		if (await Unmend()) return true;
            	if (await Unleash()) return true;
            	if (await RaidOpenerScourge()) return true;
            	if (await SpinningSlash()) return true;
		if (await SyphonStrike()) return true;
            	if (await Scourge()) return true;
            	if (await Delirium()) return true;
            	if (await CarveAndSpit()) return true;
            	if (await Souleater()) return true;
            	if (await SaltedEarth()) return true;
            	if (await DarkPassenger()) return true;
            	if (await PowerSlash()) return true;
            	return await HardSlash();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
            	if (await SpinningSlash()) return true;
		if (await SyphonStrike()) return true;
            	if (await CarveAndSpit()) return true;
            	if (await Souleater()) return true;
            	if (await DarkPassenger()) return true;
            	if (await PowerSlash()) return true;
            	return await HardSlash();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}