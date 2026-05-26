using UnityEngine;

public class AnimatedSquare : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<2000; i++)
        {
            Debug.Log("Something");
        }
    }

    public void DoSomething()
    {
        Debug.Log("Event!");
    }
}
