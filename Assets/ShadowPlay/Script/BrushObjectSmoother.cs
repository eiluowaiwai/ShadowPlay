using UnityEngine;

public class BrushObjectSmoother : MonoBehaviour
{
    [Header("Ŀ��ʼ�Transform���������ϵĻ��ʼ⣩")]
    public Transform targetTip;

    [Header("ƽ��ϵ����0 = ������1 = ֱ�Ӹ���")]
    [Range(0f, 1f)]
    public float smoothFactor = 0.2f;

    private Vector3 smoothedPos;

    void Start()
    {
        if (targetTip == null)
        {
            Debug.LogWarning("BrushObjectSmoother: ������ targetTip���������ʼ�Transform��");
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

        // ָ��ƽ��λ��
        smoothedPos = Vector3.Lerp(smoothedPos, targetTip.position, 1f - smoothFactor);

        // Ӧ�õ����������λ��
        transform.position = smoothedPos;
    }
}
