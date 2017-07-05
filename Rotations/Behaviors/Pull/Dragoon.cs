using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Dragoon
    {
        public override async Task<bool> Pull()
        {
            if (await Jump()) return true;
            if (await DragonfireDive()) return true;
            if (await SpineshatterDive()) return true;
            return await Combat();
        }
    }
}