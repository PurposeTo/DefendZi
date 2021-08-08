using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

[RequireComponent(typeof(IRectangleIn2DGetter))]
[DisallowMultipleComponent] 
public class IRectangleIn2DComponent : InterfaceComponent<IRectangleIn2DGetter> { }
