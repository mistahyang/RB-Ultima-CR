using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Scholar : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                if (await Bane()) return true;
		        if (await BioII()) return true;
                if (await Miasma()) return true;
                if (await Bio()) return true;
                if (await Aero()) return true;
                if (await MiasmaII()) return true;
            	//if (await Succor()) return true;
                if (await BlizzardII()) return true;
                if (await EnergyDrain()) return true;
                if (await ShadowFlare()) return true;
                if (await Broil()) return true;
                if (await Ruin()) return true;
                return await RuinII();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
		        if (await BioII()) return true;
                if (await Miasma()) return true;
                if (await Bio()) return true;
                if (await Aero()) return true;
            	//if (await Succor()) return true;
                if (await EnergyDrain()) return true;
                if (await ShadowFlare()) return true;
                if (await Broil()) return true;
                if (await Ruin()) return true;
                return await RuinII();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await Aero()) return true;
                if (await EnergyDrain()) return true;
                if (await Broil()) return true;
                if (await Ruin()) return true;
                return await RuinII();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}