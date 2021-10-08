using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Partner.Comms.Common
{
    public static class Extensions
    {

        public static string Description(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(DisplayAttribute)) as DisplayAttribute;

            return displayAttribute?.Description ?? string.Empty;
        }

        public static T CloneObjectSerializable<T>(this T obj) where T : class
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);
            ms.Position = 0;
            object result = bf.Deserialize(ms);
            ms.Close();
            return (T)result;
        }
    }
}