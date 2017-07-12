using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Scholar
    {
        public override async Task<bool> Heal()
        {
            if (await Lustrate()) return true;
            if (await Indomitability()) return true;
            if (await Succor()) return true;
            if (await Esuna()) return true;
            //if (await Adloquium()) return true;
	    ///if (await Resurrection()) return true;
            return await Physick();
        }
    }
}