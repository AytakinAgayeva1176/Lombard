using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Lombard.Helpers
{
    public static class EnumHelper
    {
        public static Dictionary<int, string> EnumNamedValues<T>() where T : System.Enum
        {
            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
                result.Add(item, Enum.GetName(typeof(T), item));
            return result;
        }


        public static Dictionary<int, string> GetDescriptions<T>() where T : System.Enum
        {
            Dictionary<int, string> enumDictionary = new Dictionary<int, string>();
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            var values = Enum.GetValues(typeof(T));
            foreach (int item in values)
            {
                foreach (var field in fields)
                {
                    var descAtt = field.GetCustomAttribute<DescriptionAttribute>();

                    var desc = descAtt.Description;
                    if (!string.IsNullOrEmpty(desc) && Enum.GetName(typeof(T), item) == field.Name)
                    {
                        enumDictionary[item] = desc;
                        continue;
                    }

                }
            }
            
                return enumDictionary;

        }


        public static Dictionary<string, string> GetDescriptionsSrtingKeyValue<T>() where T : System.Enum
        {
            Dictionary<string, string> enumDictionary = new Dictionary<string, string>();
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            var values = Enum.GetValues(typeof(T));
            foreach (int item in values)
            {
                foreach (var field in fields)
                {
                    var descAtt = field.GetCustomAttribute<DescriptionAttribute>();

                    var desc = descAtt.Description;
                    if (!string.IsNullOrEmpty(desc) && Enum.GetName(typeof(T), item) == field.Name)
                    {
                        enumDictionary[desc] = desc;
                        continue;
                    }

                }
            }

            return enumDictionary;

        }

    }
    }

