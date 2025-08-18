using UnityEngine;

public class ActivateAndDestroy : MonoBehaviour
{
    [Header("Ҫ�������� (ֱ����ק A/B/C ����)")]
    public GameObject[] targetObjects;

    [Header("Ҫ��������� E (Ĭ������)")]
    public GameObject objectE;

    [Header("Ҫ���ٵ����� F")]
    public GameObject objectF;

    [Header("Ҫ���ٵ����� G")]
    public GameObject objectG;

    [Header("�ӳ�ʱ�䣨�룩")]
    public float delay = 3f;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject target in targetObjects)
        {
            if (other.gameObject == target)
            {
                // ���� F
                if (objectF != null)
                {
                    Destroy(objectF);
                    Debug.Log("������ F");
                }

                // ���� G
                if (objectG != null)
                {
                    Destroy(objectG);
                    Debug.Log("������ G");
                }

                // �ӳټ��� E
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
        Debug.Log("������ E");
    }
}
