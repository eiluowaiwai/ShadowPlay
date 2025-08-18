using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateAndDestroySequence : MonoBehaviour
{
    [Header("ABC 物体 Collider")]
    public Collider objectA;
    public Collider objectB;
    public Collider objectC;

    [Header("对应的销毁 Mesh/数集 (可以挂多个)")]
    public GameObject[] collectionA;
    public GameObject[] collectionB;
    public GameObject[] collectionC;

    [Header("延迟销毁集合时间 (秒)")]
    public float holdTime = 3f;

    [Header("连续激活的物体（激活时销毁旧物体）")]
    public GameObject firstObject;
    public GameObject secondObject;
    public GameObject thirdObject;

    [Header("每个物体显示持续时间")]
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
        // 延迟销毁集合
        yield return new WaitForSeconds(holdTime);

        // 销毁对应集合
        DestroyCollection(firstCollection);
        DestroyCollection(secondCollection);

        GameObject currentActive = null;

        // === 激活第一个物体 ===
        if (firstObject != null)
        {
            currentActive = firstObject;
            firstObject.SetActive(true);
            Debug.Log("激活: " + firstObject.name);
        }

        yield return new WaitForSeconds(firstDuration);

        // 激活第二个物体时销毁第一个
        if (currentActive != null)
        {
            Destroy(currentActive);
            Debug.Log("销毁: " + currentActive.name);
        }

        if (secondObject != null)
        {
            currentActive = secondObject;
            secondObject.SetActive(true);
            Debug.Log("激活: " + secondObject.name);
        }

        yield return new WaitForSeconds(secondDuration);

        // 激活第三个物体时销毁第二个
        if (currentActive != null)
        {
            Destroy(currentActive);
            Debug.Log("销毁: " + currentActive.name);
        }

        if (thirdObject != null)
        {
            thirdObject.SetActive(true);
            Debug.Log("激活: " + thirdObject.name);
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
                Debug.Log("销毁: " + obj.name);
            }
        }
    }
}
