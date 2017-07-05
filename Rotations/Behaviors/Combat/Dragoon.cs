using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Dragoon : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                if (await Jump()) return true;
                if (await SonicThrust()) return true;
                if (await BloodOfTheDragon()) return true;
                if (await Nastrond()) return true;
                if (await Geirskogul()) return true;
                if (await LifeSurge()) return true;
                if (await FangAndClaw()) return true;
                if (await WheelingThrust()) return true;
                if (await FullThrust()) return true;
                if (await VorpalThrust()) return true;
                if (await ChaosThrust()) return true;
                if (await Disembowel()) return true;
                if (await HeavyThrust()) return true;
                if (await DoomSpike()) return true;
                if (await ImpulseDrive()) return true;
                return await TrueThrust();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
                if (await Jump()) return true;
                if (await DragonSight()) return true;
		        if (await BloodOfTheDragon()) return true;
                if (await BloodForBlood()) return true;                
            	if (await BattleLitany()) return true;
                if (await Nastrond()) return true;
                if (await Geirskogul()) return true;
                if (await LifeSurge()) return true;
                if (await FangAndClaw()) return true;
                if (await WheelingThrust()) return true;
                if (await FullThrust()) return true;
                if (await VorpalThrust()) return true;
                if (await ChaosThrust()) return true;
                if (await Disembowel()) return true;
                if (await HeavyThrust()) return true;
                if (await ImpulseDrive()) return true;
                return await TrueThrust();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await FangAndClaw()) return true;
                if (await WheelingThrust()) return true;
                if (await FullThrust()) return true;
                if (await VorpalThrust()) return true;
                if (await HeavyThrust()) return true;
                if (await Phlebotomize()) return true;
                return await TrueThrust();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}