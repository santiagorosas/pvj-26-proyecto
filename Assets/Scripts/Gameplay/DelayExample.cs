using System.Collections;
using UnityEngine;

public class DelayExample : ReusableBehaviour
{
    [SerializeField] float _timeBetweenThings = 0.2f;

    void Start()
    {
        // Estas 4 líneas son equivalentes:
        Invoke("SomeMethod", 3);
        Invoke(nameof(SomeMethod), 3);
        CallDelayed(SomeMethod, 3);
        StartCoroutine(SomeMethodCoroutine());
    }

    private IEnumerator SomeMethodCoroutine()
    {
        yield return new WaitForSeconds(3);
        SomeMethod();
    }

    private IEnumerator SequenceCoroutine()
    {
        SomeMethod();
        yield return new WaitForSeconds(4);
        SomeMethod2();
        yield return new WaitForSeconds(2);
        SomeMethod3();
    }

    private IEnumerator LoopCoroutine()
    {
        int maxCount = 10;
        int count = 0;

        while (count < maxCount)
        {
            DropThing();
            yield return new WaitForSeconds(_timeBetweenThings);
        }
    }

    private void DropThing()
    {
        Debug.Log("drop thing");
    }

    private void SomeMethod()
    {

    }

    private void SomeMethod2()
    {

    }

    private void SomeMethod3()
    {

    }

}
