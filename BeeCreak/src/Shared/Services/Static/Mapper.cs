using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BeeCreak.Shared.Services.Static;

public static class Mapper
{
    public static List<PropertyInfo> GetPublicProperties(object obj)
    {
        return obj
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();
    }
}
