using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Desdiene.UI.Elements
{
    public class CutOutMaskUi : Image
    {
        public override Material materialForRendering
        {
            get
            {
                Material material = new Material(base.materialForRendering);
                material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
                return material;
            }
        }
    }
}
