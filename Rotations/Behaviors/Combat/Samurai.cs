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
                if (await Shinten()) return true;
                if (await Ageha()) return true;
                if (await TenkaGoken()) return true;
                if (await Oka()) return true;
                if (await Mangetsu()) return true;
                if (await Fuga()) return true;
                if (await Kaiten()) return true;
                if (await MidareSetsugekka()) return true;
                if (await Higanbana()) return true;
                if (await Kasha()) return true;
                if (await Gekko()) return true;
                if (await Yukikaze()) return true;
                if (await Shifu()) return true;
                if (await Jinpu()) return true;
                return await Hakaze();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
                if (await Shinten()) return true;
                if (await Guren()) return true;
                if (await Hagakure()) return true;
                if (await Ageha()) return true;
                if (await Kaiten()) return true;
                if (await MidareSetsugekka()) return true;
                if (await Higanbana()) return true;
                if (await Kasha()) return true;
                if (await Gekko()) return true;
                if (await Yukikaze()) return true;
                if (await Shifu()) return true;
                if (await Jinpu()) return true;
                return await Hakaze();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await SummonChocobo()) return true;
                if (await Shinten()) return true;
                if (await Ageha()) return true;
                if (await TenkaGoken()) return true;
                if (await Kaiten()) return true;
                if (await MidareSetsugekka()) return true;
                if (await Oka()) return true;
                if (await Mangetsu()) return true;
                if (await Fuga()) return true;
                if (await Higanbana()) return true;
                if (await Kasha()) return true;
                if (await Gekko()) return true;
                if (await Yukikaze()) return true;
                if (await Shifu()) return true;
                if (await Jinpu()) return true;
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