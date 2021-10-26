using UnityEngine;

[ExecuteInEditMode]
public class StealLightmap : MonoBehaviour
{

    private MeshRenderer currentRenderer;
    public MeshRenderer lightmappedObject;

    private void OnEnable()
    {
        currentRenderer = gameObject.GetComponent<MeshRenderer>();
        RendererInfoTransfer();
    }

#if UNITY_EDITOR
    void OnBecameVisible()
    {
        RendererInfoTransfer();
    }
#endif

    void RendererInfoTransfer()
    {
        if(lightmappedObject == null || currentRenderer == null)
            return;

        currentRenderer.lightmapIndex = lightmappedObject.lightmapIndex;
        currentRenderer.lightmapScaleOffset = lightmappedObject.lightmapScaleOffset;
        currentRenderer.realtimeLightmapIndex = lightmappedObject.realtimeLightmapIndex;
        currentRenderer.realtimeLightmapScaleOffset = lightmappedObject.realtimeLightmapScaleOffset;
        currentRenderer.lightProbeUsage = lightmappedObject.lightProbeUsage;
    }
}
