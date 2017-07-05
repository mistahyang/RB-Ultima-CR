using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Samurai : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                /*if (await Jump()) return true;
                if (await LifeSurge()) return true;*/
                if (await Ageha()) return true;
                if (await TenkaGoken()) return true;
                if (await MidareSetsugekka()) return true;
                if (await Higanbana()) return true;
                if (await Oka()) return true;
                if (await Mangetsu()) return true;
                if (await Fuga()) return true;
                if (await Kasha()) return true;
                if (await Gekko()) return true;
                if (await Yukikaze()) return true;
                if (await Jinpu()) return true;
                if (await Shifu()) return true;
                return await Hakaze();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
                /*if (await Jump()) return true;
		        if (await BloodOfTheDragon()) return true;
            	if (await BattleLitany()) return true;
                if (await Geirskogul()) return true;
                if (await LifeSurge()) return true;
                if (await FangAndClaw()) return true;
                if (await WheelingThrust()) return true;
                if (await FullThrust()) return true;
                if (await VorpalThrust()) return true;
                if (await ChaosThrust()) return true;
                if (await Disembowel()) return true;
                if (await HeavyThrust()) return true;
                if (await ImpulseDrive()) return true;*/
                if (await Gekko()) return true;
                if (await Jinpu()) return true;
                return await Hakaze();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await SummonChocobo()) return true;
                if (await Ageha()) return true;
                if (await TenkaGoken()) return true;
                if (await MidareSetsugekka()) return true;
                if (await Higanbana()) return true;
                if (await Oka()) return true;
                if (await Mangetsu()) return true;
                if (await Fuga()) return true;
                if (await Kasha()) return true;
                if (await Gekko()) return true;
                if (await Yukikaze()) return true;
                if (await Jinpu()) return true;
                if (await Shifu()) return true;
                return await Hakaze();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}