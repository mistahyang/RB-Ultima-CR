﻿using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Machinist
    {
        public override async Task<bool> Heal()
        {
            return await SecondWind();
        }
    }
}