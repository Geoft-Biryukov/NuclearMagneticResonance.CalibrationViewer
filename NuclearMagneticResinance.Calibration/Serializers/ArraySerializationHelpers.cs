using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers
{
    public static class ArraySerializationHelpers
    {
        private const string separator = ";";
        private const int decimals = 3;

        public static string ToString(double[] array, IFormatProvider formatProvider)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (formatProvider == null)
                throw new ArgumentNullException(nameof(formatProvider));

            return ToString(array, formatProvider, (i, arr) => Math.Round(arr[i], decimals), val => val.ToString(formatProvider));
        }

        public static string ToString(short[] array, IFormatProvider formatProvider)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (formatProvider == null)
                throw new ArgumentNullException(nameof(formatProvider));

            return ToString(array, formatProvider, (i, arr) => arr[i], val => val.ToString(formatProvider));
        }

        private static string ToString<T>(T[] array, IFormatProvider formatProvider, Func<int, T[], T> arrayElementSelector, Func<T, string> toStringConverter)
            => string.Join(separator, array.Select(item => toStringConverter(item)));

        public static double[] FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new double[0];

            var result = new List<double>();

            var strArray = value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in strArray)
            {
                if (!double.TryParse(item, out double current))
                    return new double[0];

                result.Add(current);
            }
            return result.ToArray();
        }
    }
}
