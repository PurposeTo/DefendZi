using System.Collections;
using UnityEngine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Coroutine;

public class PopUpScoreUIController : MonoBehaviourExt
{
    [SerializeField] private ScoreCollectorTracker collectorTracker;
    [SerializeField] private TextView creatingScoreView;
    
    protected override void Constructor()
    {
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private void CreatePopUp(int score)
    {
        var textView = Instantiate(creatingScoreView, transform);
        textView.SetText($"+{score}");
        DestroyPopUpEnumerator(textView.gameObject);
    }

    private void DestroyPopUpEnumerator(GameObject gameObject)
    {
        ICoroutine routine = new CoroutineWrap(this);
        routine.StartContinuously(DestroyPopUp(gameObject));
    }

    private IEnumerator DestroyPopUp(GameObject gameObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void SubcribeEvents()
    {
        collectorTracker.OnTracked += CreatePopUp;
    }

    private void UnsubcribeEvents()
    {
        collectorTracker.OnTracked -= CreatePopUp;
    }
}
