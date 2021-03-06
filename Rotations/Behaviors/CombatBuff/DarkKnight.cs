﻿using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class DarkKnight
    {
        public override async Task<bool> CombatBuff()
        {
            if (await Ultima.SummonChocobo()) return true;
            if (await LivingDead()) return true;
            if (await DarkArts()) return true;
            if (await Plunge()) return true;
            if (await Darkside()) return true;
            if (await BloodWeapon()) return true;
            if (await BloodPrice()) return true;
            if (await Reprisal()) return true;
            if (await LowBlow()) return true;
            if (await SoleSurvivor()) return true;
            if (await Bloodbath()) return true;
            return await MercyStroke();
        }
    }
}