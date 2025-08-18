using UnityEngine;

public class SwitchUIOnClick : MonoBehaviour
{
    public GameObject componentToHide; // Ҫ���ص����
    public GameObject componentToShow; // Ҫ��ʾ�����

    public void SwitchUI()
    {
        if (componentToHide != null)
            componentToHide.SetActive(false);

        if (componentToShow != null)
            componentToShow.SetActive(true);
    }
}
