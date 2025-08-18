using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateAndDestroySequence : MonoBehaviour
{
    [Header("ABC ���� Collider")]
    public Collider objectA;
    public Collider objectB;
    public Collider objectC;

    [Header("��Ӧ������ Mesh/���� (���ԹҶ��)")]
    public GameObject[] collectionA;
    public GameObject[] collectionB;
    public GameObject[] collectionC;

    [Header("�ӳ����ټ���ʱ�� (��)")]
    public float holdTime = 3f;

    [Header("������������壨����ʱ���پ����壩")]
    public GameObject firstObject;
    public GameObject secondObject;
    public GameObject thirdObject;

    [Header("ÿ��������ʾ����ʱ��")]
    public float firstDuration = 3f;
    public float secondDuration = 5f;

    private HashSet<GameObject> destroyedObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("D OnTriggerEnter: " + other.name);

        if (other == objectA)
            StartCoroutine(DestroyCollectionsAndActivateSequence(collectionB, collectionC));
        else if (other == objectB)
            StartCoroutine(DestroyCollectionsAndActivateSequence(collectionA, collectionC));
        else if (other == objectC)
            StartCoroutine(DestroyCollectionsAndActivateSequence(collectionA, collectionB));
    }

    private IEnumerator DestroyCollectionsAndActivateSequence(GameObject[] firstCollection, GameObject[] secondCollection)
    {
        // �ӳ����ټ���
        yield return new WaitForSeconds(holdTime);

        // ���ٶ�Ӧ����
        DestroyCollection(firstCollection);
        DestroyCollection(secondCollection);

        GameObject currentActive = null;

        // === �����һ������ ===
        if (firstObject != null)
        {
            currentActive = firstObject;
            firstObject.SetActive(true);
            Debug.Log("����: " + firstObject.name);
        }

        yield return new WaitForSeconds(firstDuration);

        // ����ڶ�������ʱ���ٵ�һ��
        if (currentActive != null)
        {
            Destroy(currentActive);
            Debug.Log("����: " + currentActive.name);
        }

        if (secondObject != null)
        {
            currentActive = secondObject;
            secondObject.SetActive(true);
            Debug.Log("����: " + secondObject.name);
        }

        yield return new WaitForSeconds(secondDuration);

        // �������������ʱ���ٵڶ���
        if (currentActive != null)
        {
            Destroy(currentActive);
            Debug.Log("����: " + currentActive.name);
        }

        if (thirdObject != null)
        {
            thirdObject.SetActive(true);
            Debug.Log("����: " + thirdObject.name);
        }
    }

    private void DestroyCollection(GameObject[] objects)
    {
        if (objects == null) return;

        foreach (var obj in objects)
        {
            if (obj != null && !destroyedObjects.Contains(obj))
            {
                Destroy(obj);
                destroyedObjects.Add(obj);
                Debug.Log("����: " + obj.name);
            }
        }
    }
}
