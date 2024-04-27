using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NuclearMagneticResonance.Calibration.Serializers
{
    internal static class AttributeParsers
    {
        public static bool TryParseAttribute(XmlNode element, string name, out byte value)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"string.IsNullOrEmpty({nameof(name)})");

            value = default(byte);

            var attr = element.Attributes[name];

            if (attr == null)
                return false;

            if (!byte.TryParse(attr.Value, out value))
                return false;

            return true;
        }

        public static bool TryParseAttribute(XmlNode element, string name, out int value)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"string.IsNullOrEmpty({nameof(name)})");

            value = default(int);

            var attr = element.Attributes[name];

            if (attr == null)
                return false;

            if (!int.TryParse(attr.Value, out value))
                return false;

            return true;
        }

        public static bool TryParseAttribute(XmlNode element, string name, out double value)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"string.IsNullOrEmpty({nameof(name)})");

            value = default(double);

            var attr = element.Attributes[name];

            if (attr == null)
                return false;

            if (!double.TryParse(attr.Value, System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return false;

            return true;
        }

        public static bool TryParseAttribute(XmlNode element, string name, out bool value)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"string.IsNullOrEmpty({nameof(name)})");

            value = default(bool);

            var attr = element.Attributes[name];

            if (attr == null)
                return false;

            if (!bool.TryParse(attr.Value, out value))
                return false;

            return true;
        }
    }
}
