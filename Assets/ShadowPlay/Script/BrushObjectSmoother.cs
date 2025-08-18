using UnityEngine;

public class BrushObjectSmoother : MonoBehaviour
{
    [Header("目标笔尖Transform（控制器上的画笔尖）")]
    public Transform targetTip;

    [Header("平滑系数，0 = 不动，1 = 直接跟随")]
    [Range(0f, 1f)]
    public float smoothFactor = 0.2f;

    private Vector3 smoothedPos;

    void Start()
    {
        if (targetTip == null)
        {
            Debug.LogWarning("BrushObjectSmoother: 请设置 targetTip（控制器笔尖Transform）");
            smoothedPos = transform.position;
        }
        else
        {
            smoothedPos = targetTip.position;
        }
    }

    void Update()
    {
        if (targetTip == null) return;

        // 指数平滑位置
        smoothedPos = Vector3.Lerp(smoothedPos, targetTip.position, 1f - smoothFactor);

        // 应用到画笔物体的位置
        transform.position = smoothedPos;
    }
}
