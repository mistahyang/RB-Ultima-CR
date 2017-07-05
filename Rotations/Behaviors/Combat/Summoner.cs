using System.Threading.Tasks;
using ff14bot.Helpers;

namespace UltimaCR.Rotations
{
    public sealed partial class Summoner : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                if (await Aetherflow()) return true;
                if (await Deathflare()) return true;
                if (await DreadwyrmTrance()) return true;
                if (await Fester()) return true;
                if (await Contagion()) return true;
                if (await Enkindle()) return true;
                if (await Rouse()) return true;
                if (await Spur()) return true;
                if (await RagingStrikes()) return true;
                if (await AerialSlash()) return true;
                if (await BioII()) return true;
                if (await Miasma()) return true;
                if (await Bio()) return true;
                if (await EnergyDrain()) return true;
                if (await ShadowFlare()) return true;
                if (await RuinIII()) return true;
                if (await Ruin()) return true;
                return await RuinII();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
                if (await RagingStrikes()) return true;
                if (await Tridisaster()) return true;
                if (await RuinIIGCD()) return true;
                if (await BioII()) return true;
                if (await Miasma()) return true;
                if (await Bio()) return true;
                if (await Deathflare()) return true;
                if (await DreadwyrmTrance()) return true;
                if (await EnergyDrain()) return true;
                if (await Contagion()) return true;
                if (await Fester()) return true;
                if (await Painflare()) return true;
                if (await Enkindle()) return true;
                if (await AerialSlash()) return true;
                if (await Spur()) return true;
                if (await Rouse()) return true;
                if (await ShadowFlare()) return true;
                if (await RuinIII()) return true;
                if (await Ruin()) return true;
                return await RuinII();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await Aetherflow()) return true;
                if (await DreadwyrmTrance()) return true;
                if (await Deathflare()) return true;
                if (await Painflare()) return true;
                if (await EnergyDrain()) return true;
                if (await ShadowFlare()) return true;
                if (await RuinIII()) return true;
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