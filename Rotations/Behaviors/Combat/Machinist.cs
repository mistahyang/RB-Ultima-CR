using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Machinist : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
            	if (await HotShot()) return true;
            	if (await Reload()) return true;
            	if (await QuickReload()) return true;
                if (await RagingStrikes()) return true;
            	if (await HawksEye()) return true;
            	if (await BloodForBlood()) return true;
            	if (await RapidFire()) return true;
            	if (await Heartbreak()) return true;
            	if (await Reassemble()) return true;
            	if (await SpreadShot()) return true;
            	if (await CleanShot()) return true;
            	if (await SplitShot()) return true;
            	if (await SlugShot()) return true;
            	if (await GaussRound()) return true;
            	if (await Ricochet()) return true;
            	if (await SuppressiveFire()) return true;
            	return await HeadGraze();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
            	if (await RaidOpenerHotShot()) return true;
            	if (await RaidOpenerLeadShot()) return true;
                if (await Feint()) return true;
            	if (await HotShot()) return true;
            	if (await LeadShot()) return true;
            	if (await Wildfire()) return true;
            	if (await Reload()) return true;
            	if (await QuickReload()) return true;
            	if (await Hypercharge()) return true;
            	if (await RagingStrikes()) return true;
            	if (await HawksEye()) return true;
            	if (await BloodForBlood()) return true;
            	if (await RapidFire()) return true;
            	if (await Heartbreak()) return true;
            	if (await Reassemble()) return true;
            	if (await CleanShot()) return true;
            	if (await SplitShot()) return true;
            	if (await SlugShot()) return true;
            	if (await GaussRound()) return true;
            	if (await Ricochet()) return true;
            	if (await SuppressiveFire()) return true;
            	if (await HeadGraze()) return true;
            	return await Blank();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await Feint()) return true;
            	if (await HotShot()) return true;
            	if (await LeadShot()) return true;
            	if (await Reload()) return true;
            	if (await QuickReload()) return true;
            	if (await Heartbreak()) return true;
            	if (await Reassemble()) return true;
            	if (await CleanShot()) return true;
            	if (await SplitShot()) return true;
            	if (await SlugShot()) return true;
            	if (await GaussRound()) return true;
            	if (await SuppressiveFire()) return true;
            	if (await HeadGraze()) return true;
            	return await Blank();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}