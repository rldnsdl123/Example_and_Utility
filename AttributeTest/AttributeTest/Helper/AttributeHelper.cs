using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AttributeTest
{
    public static class AttributeHelper
    {
        public static int GetOffset(Type type, string propertyName)
        {
            var prop = GetPropertyInfo(type, propertyName);
            if (prop != null)
                return GetOffset(prop);

            throw new Exception($"This property has not Property={propertyName}");
        }
        public static int GetLength(Type type, string propertyName)
        {
            var prop = GetPropertyInfo(type, propertyName);
            if (prop != null)
                return GetOffset(prop);

            throw new Exception($"This property has not Property={propertyName}");
        }

        public static int GetOffset(PropertyInfo pinfo)
        {
            var addrOffsetAttr = pinfo.GetCustomAttribute<PlcDeviceInfoAttribute>();
            if (addrOffsetAttr != null)
                return addrOffsetAttr.offset;

            throw new Exception("This property has not AddressOffsetAttribute");
        }

        public static int GetLength(PropertyInfo pinfo)
        {
            var addrOffsetAttr = pinfo.GetCustomAttribute<PlcDeviceInfoAttribute>();
            if (addrOffsetAttr != null)
                return addrOffsetAttr.length;

            throw new Exception("This property has not AddressOffsetAttribute");
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetProperty(propertyName);
        }
    }
}