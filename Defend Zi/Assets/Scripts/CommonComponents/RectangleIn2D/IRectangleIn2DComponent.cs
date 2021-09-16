using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Rectangles;
using UnityEngine;

[RequireComponent(typeof(IRectangleIn2DAccessor))]
[DisallowMultipleComponent]
public class IRectangleIn2DComponent : InterfaceComponent<IRectangleIn2DAccessor> { }
