using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.ExtensionsMethods {
    public static class EnumsExtensions {

        public static string ToDescription(this Enum enumeration) {
            var attribute = GetText<DescriptionAttribute>(enumeration);
            return attribute.Description;
        }

        public static T GetText<T>(Enum enumeration) where T : Attribute {
            Type type = enumeration.GetType();
            MemberInfo[] memberInfo = type.GetMember(enumeration.ToString());
            if(!memberInfo.Any()) {
                throw new ArgumentException($"No public members for the argument '{enumeration}'.");
            }
            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            if(attributes == null || attributes.Length != 1) {
                throw new ArgumentException($"Can't find an attribute matching '{typeof(T).Name}' for the argument '{enumeration}'");
            }
            return attributes.Single() as T;
        }

    }
}
