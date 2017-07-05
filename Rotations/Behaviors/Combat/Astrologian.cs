using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Astrologian : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
				if (await ExaltedDetriment()) return true;
	    		if (await Redraw()) return true;
            	if (await RoyalRoad()) return true;
				if (await Spread()) return true;
				if (await AspectedHelios()) return true;
				if (await LuminiferousAether()) return true;
				if (await Gravity()) return true;
                if (await CombustII()) return true;
            	if (await Combust()) return true;
				if (await Aero()) return true;
            	if (await MaleficII()) return true;
            	return await Malefic();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
				if (await ExaltedDetriment()) return true;
				if (await Redraw()) return true;
            	if (await RoyalRoad()) return true;
				if (await Spread()) return true;
				if (await AspectedHelios()) return true;
				if (await LuminiferousAether()) return true;
                if (await CombustII()) return true;
            	if (await Combust()) return true;
            	if (await Aero()) return true;
            	if (await MaleficII()) return true;
            	return await Malefic();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
	    	if (await Redraw()) return true;
            	if (await RoyalRoad()) return true;
		if (await Spread()) return true;
		if (await LuminiferousAether()) return true;
            	if (await MaleficII()) return true;
            	return await Malefic();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}