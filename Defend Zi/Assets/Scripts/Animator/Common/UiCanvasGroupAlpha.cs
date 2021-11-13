//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Desdiene.Coroutines;
//using Desdiene.MonoBehaviourExtension;
//using Desdiene.UI.Animators;
//using Desdiene.UI.Elements;
//using UnityEngine;

//public class UiCanvasGroupAlpha : MonoBehaviourExtContainer, IUiElementAnimation
//{
//    private readonly CanvasGroup _canvasGroup;
//    private ICoroutine _animation;

//    public UiCanvasGroupAlpha(MonoBehaviourExt mono, CanvasGroup canvasGroup) : base(mono)
//    {
//        _canvasGroup = canvasGroup != null
//            ? canvasGroup
//            : throw new ArgumentNullException(nameof(canvasGroup));
//    }

//    void IUiElementAnimation.Show(Action OnEnded)
//    {
//        SetHidden();
//        _animation.ReStart(ToDisplayed(OnEnded));
//    }

//    void IUiElementAnimation.Hide(Action OnEnded)
//    {
//        SetDisplayed();
//        _animation.ReStart(ToHidden(OnEnded));
//    }

//    private float Alpha => _canvasGroup.alpha;
    
//    private IEnumerator ToHidden(Action OnEnded)
//    {
//        while (Alpha > 0)
//        {
//            float delta = 1f / animationTime * Time.unscaledDeltaTime;
//            SetColorAlpha(Color.a - delta);
//            yield return null;
//        }
//        SetHidden();
//        OnEnded?.Invoke();
//    }

//    private IEnumerator ToDisplayed(Action OnEnded)
//    {
//        SetRaycastTarget(true);
//        while (Color.a < 1)
//        {
//            float delta = 1f / animationTime * Time.unscaledDeltaTime;
//            SetColorAlpha(Color.a + delta);
//            yield return null;
//        }
//        SetDisplayed();
//        OnEnded?.Invoke();
//    }

//    private void SetDisplayed()
//    {
//        SetRaycastTarget(true);
//        SetColorAlpha(1);
//    }

//    private void SetHidden()
//    {
//        SetRaycastTarget(false);
//        SetColorAlpha(0);
//    }

//}
