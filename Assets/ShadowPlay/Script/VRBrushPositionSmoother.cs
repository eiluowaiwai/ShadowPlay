using System;
using System.Collections.Generic;
using UnityEngine;

public class VRBrushPositionSmoother : MonoBehaviour
{
    [Header("输入")]
    public Transform tipTransform; // 笔尖原始位置

    [Header("平滑参数")]
    [Range(0f, 1f)] public float smoothingFactor = 0.2f; // 指数平滑系数（越小越平滑）
    public int movingAverageWindow = 6; // 移动平均窗口大小

    [Header("补点参数")]
    public float minStep = 0.005f; // 插值补点，单位为世界坐标距离

    // 内部状态
    private Vector3 expSmoothedPos;
    private Queue<Vector3> movAvgQueue = new Queue<Vector3>();
    private Vector3 lastOutputPos;

    // 外部订阅接口：每个补点都会回调，通知调用者绘制
    public event Action<Vector3> OnSmoothedDrawPoint;

    void Start()
    {
        if (tipTransform == null)
            Debug.LogWarning("tipTransform 未设置");
        expSmoothedPos = tipTransform != null ? tipTransform.position : Vector3.zero;
        lastOutputPos = expSmoothedPos;
    }

    void Update()
    {
        if (tipTransform == null) return;

        Vector3 rawPos = tipTransform.position;

        // 1. 指数平滑
        expSmoothedPos = Vector3.Lerp(expSmoothedPos, rawPos, 1f - smoothingFactor);

        // 2. 移动平均
        movAvgQueue.Enqueue(rawPos);
        while (movAvgQueue.Count > movingAverageWindow)
            movAvgQueue.Dequeue();

        Vector3 movAvgPos = Vector3.zero;
        foreach (var p in movAvgQueue) movAvgPos += p;
        movAvgPos /= movAvgQueue.Count;

        // 3. 选择用哪种平滑结果作为最终点（这里用指数平滑，你可以改）
        Vector3 smoothPos = expSmoothedPos;

        // 4. 插值补点，保证轨迹连续
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
