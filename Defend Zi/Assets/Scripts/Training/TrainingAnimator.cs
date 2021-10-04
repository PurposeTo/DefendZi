using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class TrainingAnimator : MonoBehaviourExt
{
    [SerializeField, NotNull] private FromTransparentToBlinking _blinkingSprite;
    [SerializeField, NotNull] private ToVisible _toVisibleText;

    public void Enable()
    {
        _blinkingSprite.Enable();
        _toVisibleText.Enable();
    }

    public void Disable()
    {
        _blinkingSprite.Disable();
        _toVisibleText.Disable();
    }
}
