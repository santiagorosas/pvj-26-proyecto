using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ReusableBehaviour : MonoBehaviour
{
    public Coroutine CallDelayed(UnityAction method, float delay)
    {
        return StartCoroutine(CallDelayedRoutine(method, delay));
    }

    private IEnumerator CallDelayedRoutine(UnityAction method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method?.Invoke();
    }
}
