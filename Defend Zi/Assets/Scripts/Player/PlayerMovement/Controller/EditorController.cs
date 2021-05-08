using UnityEngine;

public class EditorController : Controller
{
    public override bool IsActive => Input.GetKey(KeyCode.Space);
}
