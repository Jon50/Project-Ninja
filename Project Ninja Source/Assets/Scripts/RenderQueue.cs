using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
[ExecuteAlways]
#endif
public class RenderQueue : MonoBehaviour
{
    public RenderType renderType = RenderType.MeshRenderer;
    public int renderOrder = 1;

    private void Start()
    {
        if (renderType == RenderType.MeshRenderer)
            GetComponent<MeshRenderer>().sharedMaterial.renderQueue += renderOrder;
        else
            GetComponent<Image>().material.renderQueue += renderOrder;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (renderType == RenderType.MeshRenderer)
            GetComponent<MeshRenderer>().sharedMaterial.renderQueue = 3000 + renderOrder;
        else
            GetComponent<Image>().material.renderQueue = 3000 + renderOrder;
    }
#endif
}
public enum RenderType { MeshRenderer, Image }
