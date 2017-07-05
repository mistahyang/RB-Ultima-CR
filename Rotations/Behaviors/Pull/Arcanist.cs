using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Arcanist
    {
        public override async Task<bool> Pull()
        {
            if (await Aero()) return true;
            return await Combat();
        }
    }
}