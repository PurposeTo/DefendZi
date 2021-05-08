using UnityEngine;

public class MobileController : Controller
{
    public override bool IsActive => Input.touchCount > 0;
}
