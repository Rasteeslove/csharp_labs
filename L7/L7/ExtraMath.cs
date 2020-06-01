using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace L7
{
    class ExtraMath
    {

        public static BigInteger PosLCMofTwo(BigInteger a, BigInteger b)
        {
            return BigInteger.Abs(a * b) / PosGCDofTwo(a, b);
        }

        public static BigInteger PosGCDofTwo(BigInteger a, BigInteger b)
        {
            a = BigInteger.Abs(a);
            b = BigInteger.Abs(b);

            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                } 
                else
                {
                    b -= a;
                }
            }

            return a;
        }

    }
}
