using UnityEngine;

public class SwitchUIOnClick : MonoBehaviour
{
    public GameObject componentToHide; // 要隐藏的组件
    public GameObject componentToShow; // 要显示的组件

    public void SwitchUI()
    {
        if (componentToHide != null)
            componentToHide.SetActive(false);

        if (componentToShow != null)
            componentToShow.SetActive(true);
    }
}
