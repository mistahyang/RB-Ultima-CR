﻿using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Monk
    {
        public override async Task<bool> Pull()
        {
            return await Combat();
        }
    }
}