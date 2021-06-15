using System.Collections;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ScoreReceiverVfxController : MonoBehaviourExt
{
    [SerializeField, NotNull] private ScoreReceiver scoreReceiver;
    [SerializeField, NotNull] private TextView creatingScoreView;

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
        scoreReceiver.OnReceived += CreatePopUp;
    }

    private void UnsubcribeEvents()
    {
        scoreReceiver.OnReceived -= CreatePopUp;
    }
}
