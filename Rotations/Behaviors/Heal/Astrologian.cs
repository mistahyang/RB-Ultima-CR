using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Astrologian
    {
        public override async Task<bool> Heal()
        {
            if (await ExaltedDetriment()) return true;
            if (await EssentialDignity()) return true;
            if (await Helios()) return true;
            if (await AspectedBeneficDIUR()) return true;
            if (await AspectedBeneficNOCT()) return true;
            if (await BeneficIIENHANCED()) return true;
            if (await BeneficII()) return true;
	    ///if (await Ascend()) return true;
	    if (await BeneficREGEN()) return true;
            return await Benefic();
        }
    }
}