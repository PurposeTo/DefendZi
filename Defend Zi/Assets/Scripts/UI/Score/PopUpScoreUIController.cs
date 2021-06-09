using System.Collections;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PopUpScoreUIController : MonoBehaviourExt
{
    [SerializeField] private ScoreCollectorTracker collectorTracker;
    [SerializeField] private TextView creatingScoreView;

    private readonly float viewLifeTime = 1f;

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
        yield return new WaitForSeconds(viewLifeTime);
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
