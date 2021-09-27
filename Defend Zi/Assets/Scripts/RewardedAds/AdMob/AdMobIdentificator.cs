using System.Collections.Generic;
using UnityEngine;

public class AdMobIdentificator
{
    private readonly Dictionary<IdType, string> AdIdentificators = new Dictionary<IdType, string>()
    {
        { IdType.Empty, "" },
        { IdType.AndroidForTest, "ca-app-pub-3940256099942544/5224354917" },
        { IdType.IosForTest, "ca-app-pub-3940256099942544/1712485313" },
        { IdType.AndroidForProduction, "ca-app-pub-8365272256827287/5620865565" }
    };

    private enum IdType
    {
        Empty,
        AndroidForTest,
        IosForTest,
        AndroidForProduction
    }

    public string GetForTest()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                return AdIdentificators[IdType.Empty];
            case RuntimePlatform.Android:
                return AdIdentificators[IdType.AndroidForTest];
            case RuntimePlatform.IPhonePlayer:
                return AdIdentificators[IdType.IosForTest];
            default:
                return AdIdentificators[IdType.Empty];
        }
    }

    public string GetForProduction()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                return AdIdentificators[IdType.Empty];
            case RuntimePlatform.Android:
                return AdIdentificators[IdType.AndroidForProduction];
            case RuntimePlatform.IPhonePlayer:
                return AdIdentificators[IdType.Empty];
            default:
                return AdIdentificators[IdType.Empty];
        }
    }
}
