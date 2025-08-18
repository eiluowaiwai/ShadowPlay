using UnityEngine;

public class BrushColorChanger : MonoBehaviour
{
    [Header("笔尖 Renderer")]
    public Renderer brushTipRenderer;

    [Header("颜色参数（在 Inspector 中设置）")]
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Color1":
                SetBrushColor(color1);
                break;
            case "Color2":
                SetBrushColor(color2);
                break;
            case "Color3":
                SetBrushColor(color3);
                break;
            case "Color4":
                SetBrushColor(color4);
                break;
            case "Color5":
                SetBrushColor(color5);
                break;
            case "Color6":
                SetBrushColor(color6);
                break;
        }
    }

    void SetBrushColor(Color color)
    {
        if (brushTipRenderer != null)
        {
            brushTipRenderer.material.color = color;
        }
    }
}
