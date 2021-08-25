using System.Collections;
using Desdiene.CoroutineWrapper;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class Test_CoroutineWrap : MonoBehaviourExt
{

    private CoroutineWrap stoppableCoroutine;

    protected override void AwakeExt()
    {
        stoppableCoroutine = new CoroutineWrap(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(Time.time + " / S Pressed!");
            stoppableCoroutine.StartContinuously(Outer());
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(Time.time + " / Q Pressed!");
            stoppableCoroutine.Terminate();
        }
    }

    private IEnumerator Outer()
    {
        Debug.Log(Time.time + " / Start Outer");
        yield return stoppableCoroutine.StartNested(Inner());
        Debug.Log(Time.time + " / End Outer");
    }

    private IEnumerator Inner()
    {
        int count = 0;
        while (count < 5)
        {
            Debug.Log("Inner running...");
            yield return new WaitForSeconds(0.5f);
            count++;
        }
        yield return stoppableCoroutine.StartNested(InnerInner(1));
        yield return stoppableCoroutine.StartNested(InnerInner(2));
    }

    private IEnumerator InnerInner(int number)
    {
        int count = 0;
        while (true)
        {
            Debug.Log($"InnerInner running... " + number);

            if (count == 9)
            {
               break;
            }
            count++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
