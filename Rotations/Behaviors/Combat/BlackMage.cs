using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class BlackMage : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
		if (await Swiftcast()) return true;
		if (await Scathe()) return true;
                if (await Thundercloud()) return true;
		if (await Firestarter()) return true;
                if (await LeyLines()) return true;
                if (await Enochian()) return true;
                if (await RagingStrikes()) return true;
                if (await Sharpcast()) return true;
                if (await BlizzardIV()) return true;	
                if (await FireIV()) return true;
                if (await BlizzardIII()) return true;
                if (await FireIII()) return true;
                if (await Blizzard()) return true;
                return await Fire();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
		if (await Swiftcast()) return true;
		if (await Scathe()) return true;
                if (await Thundercloud()) return true;
		if (await Firestarter()) return true;
                if (await LeyLines()) return true;
                if (await Enochian()) return true;
                if (await RagingStrikes()) return true;
                if (await Sharpcast()) return true;
                if (await BlizzardIV()) return true;	
                if (await ThunderII()) return true;
                if (await Thunder()) return true;
                if (await FireIV()) return true;
                if (await BlizzardIII()) return true;
                if (await FireIII()) return true;
                if (await Blizzard()) return true;
                return await Fire();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
		if (await Swiftcast()) return true;
		if (await Scathe()) return true;
		if (await Thundercloud()) return true;
		if (await Firestarter()) return true;
                if (await BlizzardIV()) return true;
                if (await ThunderII()) return true;
                if (await Thunder()) return true;	
                if (await FireIV()) return true;
                if (await BlizzardIII()) return true;
                if (await FireIII()) return true;
                if (await Blizzard()) return true;
                return await Fire();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}