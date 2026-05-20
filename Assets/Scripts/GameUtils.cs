using UnityEngine;

public static class GameUtils
{

    public static void ScaleJump(GameObject gameObject)
    {
        gameObject.transform.localScale = Vector3.one;
        LeanTween.scale(gameObject: gameObject,
            to: Settings.Instance._scaleJumpTargetScale * Vector3.one,
            time: Settings.Instance._scaleJumpTime * 0.5f).setLoopPingPong(loops: 1);


            /*
            setOnComplete(() =>
            {
                LeanTween.scale(gameObject, to: Vector3.one, time: Settings.Instance._scaleJumpTime * 0.5f);
            } );
            */
    }

}
