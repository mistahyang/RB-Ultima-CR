﻿using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Thaumaturge
    {
        public override async Task<bool> Heal()
        {
            return await Cure();
        }
    }
}