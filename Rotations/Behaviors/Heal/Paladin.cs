﻿using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Paladin
    {
        public override async Task<bool> Heal()
        {
            return await Clemency();
        }
    }
}