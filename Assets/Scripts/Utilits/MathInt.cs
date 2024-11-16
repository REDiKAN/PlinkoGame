using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utilits
{
    public static class MathInt
    {
        public static int Lerp(int start, int end, float percentage)
        {
            percentage = Math.Clamp(percentage, 0f, 1f);
            float interpolatedValue = start + percentage * (end - start);
            return (int)Math.Round(interpolatedValue);
        }
    }
}
