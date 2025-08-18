using System;
using System.Collections.Generic;
using UnityEngine;

public class VRBrushPositionSmoother : MonoBehaviour
{
    [Header("����")]
    public Transform tipTransform; // �ʼ�ԭʼλ��

    [Header("ƽ������")]
    [Range(0f, 1f)] public float smoothingFactor = 0.2f; // ָ��ƽ��ϵ����ԽСԽƽ����
    public int movingAverageWindow = 6; // �ƶ�ƽ�����ڴ�С

    [Header("�������")]
    public float minStep = 0.005f; // ��ֵ���㣬��λΪ�����������

    // �ڲ�״̬
    private Vector3 expSmoothedPos;
    private Queue<Vector3> movAvgQueue = new Queue<Vector3>();
    private Vector3 lastOutputPos;

    // �ⲿ���Ľӿڣ�ÿ�����㶼��ص���֪ͨ�����߻���
    public event Action<Vector3> OnSmoothedDrawPoint;

    void Start()
    {
        if (tipTransform == null)
            Debug.LogWarning("tipTransform δ����");
        expSmoothedPos = tipTransform != null ? tipTransform.position : Vector3.zero;
        lastOutputPos = expSmoothedPos;
    }

    void Update()
    {
        if (tipTransform == null) return;

        Vector3 rawPos = tipTransform.position;

        // 1. ָ��ƽ��
        expSmoothedPos = Vector3.Lerp(expSmoothedPos, rawPos, 1f - smoothingFactor);

        // 2. �ƶ�ƽ��
        movAvgQueue.Enqueue(rawPos);
        while (movAvgQueue.Count > movingAverageWindow)
            movAvgQueue.Dequeue();

        Vector3 movAvgPos = Vector3.zero;
        foreach (var p in movAvgQueue) movAvgPos += p;
        movAvgPos /= movAvgQueue.Count;

        // 3. ѡ��������ƽ�������Ϊ���յ㣨������ָ��ƽ��������Ըģ�
        Vector3 smoothPos = expSmoothedPos;

        // 4. ��ֵ���㣬��֤�켣����
        float dist = Vector3.Distance(lastOutputPos, smoothPos);
        if (dist > minStep)
        {
            int steps = Mathf.CeilToInt(dist / minStep);
            for (int i = 1; i <= steps; i++)
            {
                Vector3 interp = Vector3.Lerp(lastOutputPos, smoothPos, i / (float)steps);
                OnSmoothedDrawPoint?.Invoke(interp);
            }
        }
        else
        {
            OnSmoothedDrawPoint?.Invoke(smoothPos);
        }

        lastOutputPos = smoothPos;
    }
}
