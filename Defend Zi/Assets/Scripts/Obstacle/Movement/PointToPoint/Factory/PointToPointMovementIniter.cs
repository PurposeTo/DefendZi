using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

public static class PointToPointMovementIniter
{
    public static void Init()
    {
        var types = GetAllDerivedTypes();
        IEnumerable<MethodInfo> initializeMethodInfo;
        // TODO: обработка исключений?
        initializeMethodInfo = types.Select(type => type.GetMethod("Init"));

        foreach (var initializeMethod in initializeMethodInfo)
        {
            initializeMethod.Invoke(null, null);
        }
    }

    private static IEnumerable<Type> GetAllDerivedTypes()
    {
        return Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(PointToPointMovement)));
    }
}
