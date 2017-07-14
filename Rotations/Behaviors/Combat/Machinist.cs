using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Machinist : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
            	if (await GaussRound()) return true;
            	if (await HotShot()) return true;
            	if (await Wildfire()) return true;
            	//if (await QuickReload()) return true;
            	//if (await Hypercharge()) return true;
            	if (await RapidFire()) return true;
				if (await Reload()) return true;
            	if (await Heartbreak()) return true;
            	if (await Reassemble()) return true;
				if (await CleanShot()) return true;
            	if (await SlugShot()) return true;
            	return await SplitShot();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
				if (await Cooldown()) return true;
				if (await GaussRound()) return true;
            	if (await HotShot()) return true;
				if (await QuickReload()) return true;
            	if (await Wildfire()) return true;
				if (await Ricochet()) return true;
            	if (await Hypercharge()) return true;
            	if (await RapidFire()) return true;
				if (await Reload()) return true;
            	if (await Heartbreak()) return true;
            	if (await Reassemble()) return true;
				if (await CleanShot()) return true;
            	if (await SlugShot()) return true;
            	return await SplitShot();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
				if (await SummonChocobo()) return true;
				if (await Cooldown()) return true;
                if (await GaussRound()) return true;
            	if (await HotShot()) return true;
				if (await QuickReload()) return true;
            	if (await Wildfire()) return true;
				if (await Ricochet()) return true;
            	if (await Hypercharge()) return true;
            	if (await RapidFire()) return true;
				if (await Reload()) return true;
            	if (await Heartbreak()) return true;
            	if (await Reassemble()) return true;
				if (await CleanShot()) return true;
            	if (await SlugShot()) return true;
            	return await SplitShot();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}