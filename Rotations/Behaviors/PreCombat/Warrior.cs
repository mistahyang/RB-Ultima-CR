﻿using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Warrior
    {
        public override async Task<bool> PreCombatBuff()
        {
            return await Deliverance();
        }
    }
}