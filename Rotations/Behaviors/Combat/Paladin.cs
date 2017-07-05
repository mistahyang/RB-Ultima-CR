using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Paladin : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
				/*Manage Goring Blade
				Holy Spirit if you have excess MP
				Spirits Within
				Circle of Scorn
				Shield Swipe
				Sheltron
				Rage of Halone if enmity is getting low
				Royal Authority if all other conditions are met.*/
				if (await Requiescat()) return true;
				if (await HolySpirit()) return true;
				if (await FightOrFlight()) return true;
				if (await ShieldLob()) return true;
				if (await TotalEclipse()) return true;
				if (await SpiritsWithin()) return true;
				if (await ShieldSwipe()) return true;
            	if (await CircleOfScorn()) return true;
            	if (await GoringBlade()) return true;
            	if (await RageOfHalone()) return true;
            	if (await RiotBlade1()) return true;
            	if (await SavageBlade()) return true;
            	return await FastBlade();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
				if (await Requiescat()) return true;
				if (await HolySpirit()) return true;
				if (await FightOrFlight()) return true;
				if (await ShieldLob()) return true;
				if (await TotalEclipse()) return true;
				if (await SpiritsWithin()) return true;
				if (await ShieldSwipe()) return true;
            	if (await CircleOfScorn()) return true;
            	if (await GoringBlade()) return true;
				if (await RoyalAuthority()) return true;
            	if (await RiotBlade2()) return true;
            	return await FastBlade();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
            	if (await CircleOfScorn()) return true;
				if (await ShieldSwipe()) return true;
            	if (await RoyalAuthority()) return true;
            	if (await GoringBlade()) return true;
            	if (await RageOfHalone()) return true;
            	if (await RiotBlade1()) return true;
            	if (await SavageBlade()) return true;
            	return await FastBlade();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}