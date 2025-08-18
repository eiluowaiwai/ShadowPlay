using UnityEngine;

public class ActivateAndDestroy : MonoBehaviour
{
    [Header("要检测的物体 (直接拖拽 A/B/C 进来)")]
    public GameObject[] targetObjects;

    [Header("要激活的物体 E (默认隐藏)")]
    public GameObject objectE;

    [Header("要销毁的物体 F")]
    public GameObject objectF;

    [Header("要销毁的物体 G")]
    public GameObject objectG;

    [Header("延迟时间（秒）")]
    public float delay = 3f;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject target in targetObjects)
        {
            if (other.gameObject == target)
            {
                // 销毁 F
                if (objectF != null)
                {
                    Destroy(objectF);
                    Debug.Log("销毁了 F");
                }

                // 销毁 G
                if (objectG != null)
                {
                    Destroy(objectG);
                    Debug.Log("销毁了 G");
                }

                // 延迟激活 E
                if (objectE != null)
                {
                    Invoke(nameof(ActivateE), delay);
                }
            }
        }
    }

    private void ActivateE()
    {
        objectE.SetActive(true);
        Debug.Log("激活了 E");
    }
}
