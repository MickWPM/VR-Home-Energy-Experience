using UnityEngine;

public class AnimateScaleWithCurve : MonoBehaviour
{
    public AnimationCurve scaleCurve;
    public float animationTime = 0.6f;
    public bool destroyOnComplete = false;
    public GameObject rootGo;
    private GameObject destroyGo;
    private void Start()
    {
        destroyGo = rootGo == null ? gameObject: rootGo;
        DoAnimate();
    }

    [ContextMenu("Do Animate")]
    private void DoAnimate()
    {
        gameObject.SetActive(true);
        AnimateScale();
    }

    private async void AnimateScale()
    {
        float elapsedTime = 0;
        float t = 0;
        while (t < 1)
        {
            t = elapsedTime / animationTime;
            float scale = scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            await Awaitable.EndOfFrameAsync();
            elapsedTime += Time.deltaTime;
        }

        if (destroyOnComplete) Destroy(destroyGo);
        else gameObject.SetActive(false);
    }
}
