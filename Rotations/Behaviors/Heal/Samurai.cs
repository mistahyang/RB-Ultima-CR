using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Samurai
    {
        public override async Task<bool> Heal()
        {
            if (await Bloodbath()) return true;
            if (await MercifulEyes()) return true;
            return await SecondWind();
        }
    }
}