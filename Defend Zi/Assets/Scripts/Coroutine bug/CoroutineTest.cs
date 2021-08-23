using System.Collections;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class CoroutineTest : MonoBehaviourExt
{

    private StoppableCoroutine stoppableCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            stoppableCoroutine = new StoppableCoroutine(this, Outer());
            stoppableCoroutine.Start();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(Time.time + " / Q Pressed!");
            stoppableCoroutine.Stop();
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
        yield return stoppableCoroutine.StartNested(InnerInner());
    }

    private IEnumerator InnerInner()
    {
        int count = 0;
        while (true)
        {
            Debug.Log($"InnerInner running...");

            if (count == 9)
            {
                stoppableCoroutine.Stop();
            }
            count++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
