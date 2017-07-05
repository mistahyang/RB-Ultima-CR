using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Ninja : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
            	if (await Huton()) return true;
                if (await DeathBlossom()) return true;
                if (await Katon()) return true;
            	if (await Raiton()) return true;
            	if (await FumaShuriken()) return true;
                if (await Jugulate()) return true;
                if (await ShadowFang()) return true;
		if (await Mutilate()) return true;
                if (await AeolianEdge()) return true;
                if (await ArmorCrush()) return true;
                if (await DancingEdge()) return true;
                if (await GustSlash()) return true;
                return await SpinningEdge();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
            	if (await Huton()) return true;
                if (await RaidOpenerShadowFang()) return true;
		if (await RaidOpenerMutilate()) return true;
                if (await TrickAttack()) return true;
                if (await SneakAttack()) return true;
                if (await Suiton()) return true;
            	if (await Raiton()) return true;
            	if (await FumaShuriken()) return true;
                if (await Jugulate()) return true;
                if (await ShadowFang()) return true;
		if (await Mutilate()) return true;
                if (await AeolianEdge()) return true;
                if (await ArmorCrush()) return true;
                if (await DancingEdge()) return true;
                if (await GustSlash()) return true;
                return await SpinningEdge();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
	        if (await Huton()) return true;
                if (await RaidOpenerShadowFang()) return true;
		if (await RaidOpenerMutilate()) return true;
            	if (await Raiton()) return true;
            	if (await FumaShuriken()) return true;
                if (await ShadowFang()) return true;
		if (await Mutilate()) return true;
                if (await AeolianEdgeBOSSNODIRECTIONAL()) return true;
                if (await AeolianEdge()) return true;
                if (await ArmorCrush()) return true;
                if (await DancingEdge()) return true;
                if (await GustSlash()) return true;
                return await SpinningEdge();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}