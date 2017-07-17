using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class RedMage : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                /*if (await Jump()) return true;
                if (await LifeSurge()) return true;*/

                if (await Fleche()) return true;
                if (await Acceleration()) return true;
                if (await Redoublement()) return true;
                if (await Zwerchhau()) return true;
                if (await Riposte()) return true;
                if (await Verstone()) return true;
                if (await Verfire()) return true;
                if (await Verthunder()) return true;
                if (await Veraero()) return true;
                return await Jolt();
            }
            if (Ultima.UltSettings.SingleTarget)
            {

                if (await Fleche()) return true;
                if (await Acceleration()) return true;
                if (await Redoublement()) return true;
                if (await Zwerchhau()) return true;
                if (await Riposte()) return true;
                if (await Verstone()) return true;
                if (await Verfire()) return true;
                if (await Verthunder()) return true;
                if (await Veraero()) return true;
                return await Jolt();
            }
            if (Ultima.UltSettings.MultiTarget)
            {

                if (await Fleche()) return true;
                if (await Acceleration()) return true;
                if (await Redoublement()) return true;
                if (await Zwerchhau()) return true;
                if (await Riposte()) return true;
                if (await Verstone()) return true;
                if (await Verfire()) return true;
                if (await Verthunder()) return true;
                if (await Veraero()) return true;
                return await Jolt();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}