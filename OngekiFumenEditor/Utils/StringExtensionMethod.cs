﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Utils
{
    public static class StringExtensionMethod
    {
        public static int ToInt(this string str, int defaultVal = default) => int.TryParse(str, out var val) ? val : defaultVal;
        public static float ToFloat(this string str, float defaultVal = default) => float.TryParse(str, out var val) ? val : defaultVal;
        public static long ToLong(this string str, long defaultVal = default) => long.TryParse(str, out var val) ? val : defaultVal;
        public static double ToDouble(this string str, double defaultVal = default) => double.TryParse(str, out var val) ? val : defaultVal;
        public static byte ToByte(this string str, byte defaultVal = default) => byte.TryParse(str, out var val) ? val : defaultVal;
    }
}
