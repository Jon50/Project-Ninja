//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TheVegetationEngine;

public class TVEShaderCoreGUI : ShaderGUI
{
    bool multiSelection = false;
    bool showInternalProperties = false;
    bool showHiddenProperties = false;
    bool showAdditionalInfo = false;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        var material0 = materialEditor.target as Material;
        var materials = materialEditor.targets;

        if (materials.Length > 1)
            multiSelection = true;

        DrawDynamicInspector(material0, materialEditor, props);

        foreach (Material material in materials)
        {
            TVEShaderUtils.UpgradeMaterialTo200(material);
            TVEShaderUtils.UpgradeMaterialTo210(material);
            TVEShaderUtils.UpgradeMaterialTo220(material);
            TVEShaderUtils.UpgradeMaterialTo230(material);
            TVEShaderUtils.UpgradeMaterialTo300(material);

            TVEShaderUtils.SetMaterialSettings(material);
        }
    }

    void DrawDynamicInspector(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
    {
        var customPropsList = new List<MaterialProperty>();

        if (multiSelection)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (prop.flags == MaterialProperty.PropFlags.HideInInspector)
                    continue;

                if (material.HasProperty("_LocalColors"))
                {
                    if (prop.name == "_LocalColors")
                        continue;
                }

                customPropsList.Add(prop);
            }
        }
        else
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (prop.flags == MaterialProperty.PropFlags.HideInInspector && !showHiddenProperties)
                {
                    continue;
                }

                if (material.HasProperty("_RenderMode"))
                {
                    if (material.GetInt("_RenderMode") == 0 && prop.name == "_RenderBlend")
                        continue;

                    if (material.GetInt("_RenderMode") == 0 && prop.name == "_RenderZWrite")
                        continue;

                    if (material.GetInt("_RenderMode") == 0 && prop.name == "_RenderPriority")
                        continue;
                }

                if (!material.HasProperty("_render_normals_options"))
                {
                    if (prop.name == "_RenderNormals")
                        continue;
                }

                if (material.HasProperty("_RenderCull"))
                {
                    if (material.GetInt("_RenderCull") == 2 && prop.name == "_RenderNormals")
                        continue;
                }

                if (material.HasProperty("_RenderClip"))
                {
                    if (material.GetInt("_RenderClip") == 0 && prop.name == "_Cutoff")
                        continue;
                    if (material.GetInt("_RenderClip") == 0 && prop.name == "_FadeCameraValue")
                        continue;
                    if (material.GetInt("_RenderClip") == 0 && prop.name == "_FadeGlancingValue")
                        continue;
                    if (material.GetInt("_RenderClip") == 0 && prop.name == "_FadeHorizontalValue")
                        continue;
                    if (material.GetInt("_RenderClip") == 0 && prop.name == "_FadeVerticalValue")
                        continue;
                    if (material.GetInt("_RenderClip") == 0 && prop.name == "_FadeSpace")
                        continue;
                }

                int fadeSpace = 0;

                if (material.HasProperty("_FadeCameraValue"))
                {
                    fadeSpace++;
                }

                if (material.HasProperty("_FadeGlancingValue"))
                {
                    fadeSpace++;
                }

                if (material.HasProperty("_FadeHorizontalValue"))
                {
                    fadeSpace++;
                }

                if (fadeSpace == 0)
                {
                    if (prop.name == "_FadeSpace")
                        continue;
                }

                if (material.HasProperty("_GlobalColors"))
                {
                    if (material.GetFloat("_GlobalColors") == 0 && prop.name == "_ColorsMaskRemap")
                        continue;

                    if (material.GetFloat("_GlobalColors") == 0 && prop.name == "_ColorsVariationValue")
                        continue;
                }

                if (material.HasProperty("_GlobalOverlay"))
                {
                    if (material.GetFloat("_GlobalOverlay") == 0 && prop.name == "_OverlayMaskRemap")
                        continue;

                    if (material.GetFloat("_GlobalOverlay") == 0 && prop.name == "_OverlayBottomValue")
                        continue;

                    if (material.GetFloat("_GlobalOverlay") == 0 && prop.name == "_OverlayVariationValue")
                        continue;
                }

                if (material.HasProperty("_GlobalAlpha"))
                {
                    if (material.GetFloat("_GlobalAlpha") == 0 && prop.name == "_AlphaVariationValue")
                        continue;
                }

                if (material.HasProperty("_LocalColors"))
                {
                    if (prop.name == "_LocalColors")
                        continue;
                }

                if (!material.HasProperty("_SecondColor"))
                {
                    if (prop.name == "_DetailCat")
                        continue;

                    if (prop.name == "_DetailMode")
                        continue;

                    if (prop.name == "_DetailTypeMode")
                        continue;

                    if (prop.name == "_DetailMapsMode")
                        continue;

                    if (prop.name == "_DetailSpace")
                        continue;
                }

                if (!material.HasProperty("_VertexOcclusionColor"))
                {
                    if (prop.name == "_OcclusionCat")
                        continue;
                }

                if (material.GetTag("RenderPipeline", false) != "HDRenderPipeline" || material.HasProperty("_IsPropShader") || material.HasProperty("_IsBarkShader"))
                {
                    if (prop.name == "_SubsurfaceDiffusion")
                        continue;
                }

                if (!material.HasProperty("_SubsurfaceValue"))
                {
                    if (prop.name == "_SubsurfaceCat")
                        continue;
                }

                if (!material.HasProperty("_IsTranslucencyShader"))
                {
                    if (prop.name == "_TranslucencyIntensityValue")
                        continue;

                    if (prop.name == "_TranslucencyNormalValue")
                        continue;

                    if (prop.name == "_TranslucencyScatteringValue")
                        continue;

                    if (prop.name == "_TranslucencyDirectValue")
                        continue;

                    if (prop.name == "_TranslucencyAmbientValue")
                        continue;

                    if (prop.name == "_TranslucencyShadowValue")
                        continue;

                    if (prop.name == "_TranslucencyHDMessage")
                        continue;
                }
                else
                {
                    if (material.GetTag("RenderPipeline", false) != "HDRenderPipeline")
                    {
                        if (prop.name == "_TranslucencyHDMessage")
                            continue;
                    }
                }

                if (!material.HasProperty("_GradientColorOne"))
                {
                    if (prop.name == "_GradientCat")
                        continue;
                }

                if (!material.HasProperty("_NoiseColorOne"))
                {
                    if (prop.name == "_NoiseCat")
                        continue;
                }

                if (!material.HasProperty("_PerspectivePushValue"))
                {
                    if (prop.name == "_PerspectiveCat")
                        continue;
                }

                if (!material.HasProperty("_MotionAmplitude_10"))
                {
                    if (prop.name == "_MotionCat")
                        continue;
                }

                if (!material.HasProperty("_MotionHighlightColor"))
                {
                    if (prop.name == "_MotionSpace")
                        continue;
                }

                if (!material.HasProperty("_SizeFadeMode"))
                {
                    if (prop.name == "_SizeFadeCat")
                        continue;
                }

                if (!material.HasProperty("_EmissiveColor"))
                {
                    if (prop.name == "_EmissiveCat")
                        continue;
                }

                if (!material.HasProperty("_IsPropShader"))
                {
                    if (prop.name == "_DetailTypeMode")
                        continue;
                }

                if (material.HasProperty("_DetailTypeMode"))
                {
                    if (material.GetInt("_DetailTypeMode") == 0 && prop.name == "_DetailProjectionMode")
                        continue;

                    if (material.GetInt("_DetailTypeMode") == 1 && prop.name == "_DetailCoordMode")
                        continue;
                }

                if (material.HasProperty("_DetailMapsMode"))
                {
                    if (material.GetInt("_DetailMapsMode") == 0 && prop.name == "_SecondPackedTex")
                        continue;

                    if (material.GetInt("_DetailMapsMode") == 1 && prop.name == "_SecondAlbedoTex")
                        continue;

                    if (material.GetInt("_DetailMapsMode") == 1 && prop.name == "_SecondNormalTex")
                        continue;

                    if (material.GetInt("_DetailMapsMode") == 1 && prop.name == "_SecondMetallicValue")
                        continue;

                    if (material.GetInt("_DetailMapsMode") == 1 && prop.name == "_SecondOcclusionValue")
                        continue;
                }

                if (material.HasProperty("_VertexDataMode"))
                {
                    if (material.GetInt("_VertexDataMode") == 1 && prop.name == "_GlobalSize")
                        continue;

                    if (material.GetInt("_VertexDataMode") == 1 && prop.name == "_SizeFadeCat")
                        continue;

                    if (material.GetInt("_VertexDataMode") == 1 && prop.name == "_SizeFadeMode")
                        continue;

                    if (material.GetInt("_VertexDataMode") == 1 && prop.name == "_SizeFadeStartValue")
                        continue;

                    if (material.GetInt("_VertexDataMode") == 1 && prop.name == "_SizeFadeEndValue")
                        continue;

                    if (material.GetInt("_VertexDataMode") == 1 && prop.name == "_VertexPivotMode")
                        continue;

                    if (material.GetInt("_VertexDataMode") == 1 && prop.name == "_PivotsMessage")
                        continue;
                }

                customPropsList.Add(prop);
            }
        }

        //Draw Custom GUI
        for (int i = 0; i < customPropsList.Count; i++)
        {
            var prop = customPropsList[i];

            if (prop.type == MaterialProperty.PropType.Texture)
            {
                EditorGUI.BeginChangeCheck();

                EditorGUI.showMixedValue = prop.hasMixedValue;

                var debug = "";

                if (showInternalProperties)
                {
                    debug = "  (" + customPropsList[i].name + ")";
                }

                var tex = (Texture2D)EditorGUILayout.ObjectField(prop.displayName + debug, prop.textureValue, typeof(Texture2D), true, GUILayout.Height(50));

                EditorGUI.showMixedValue = false;

                if (EditorGUI.EndChangeCheck())
                {
                    prop.textureValue = tex;
                }
            }
            else
            {
                var label = customPropsList[i].displayName;

                if (EditorGUIUtility.currentViewWidth > 500)
                {
                    if (label.Contains("Colors Mask") && material.shader.name.Contains("Grass"))
                    {
                        label = "Colors Mask (Bottom Mask)";
                    }

                    if (label.Contains("Colors Mask") && material.shader.name.Contains("Leaf"))
                    {
                        label = "Colors Mask (Mask Blue)";
                    }

                    if (label.Contains("Colors Mask") && material.shader.name.Contains("Cross"))
                    {
                        label = "Colors Mask (Mask Blue)";
                    }

                    if (label.Contains("Overlay Mask"))
                    {
                        label = "Overlay Mask (Albedo Green)";
                    }

                    if (label == "Main Metallic")
                    {
                        label = "Main Metallic (Mask Red)";
                    }

                    if (label == "Main Occlusion")
                    {
                        label = "Main Occlusion (Mask Green)";
                    }

                    if (label == "Main Smoothness")
                    {
                        label = "Main Smoothness (Mask Alpha)";
                    }

                    if (label == "Vertex Occlusion Mask")
                    {
                        label = "Vertex Occlusion Mask (Vertex Green)";
                    }

                    if (label == "Gradient Mask")
                    {
                        label = "Gradient Mask (Vertex Alpha)";
                    }

                    if (label == "Noise Mask")
                    {
                        label = "Noise Mask (World Noise)";
                    }

                    if (label == "Subsurface Mask" && material.shader.name.Contains("Grass"))
                    {
                        label = "Subsurface Mask (Top Mask)";
                    }

                    if (label == "Subsurface Mask" && material.shader.name.Contains("Leaf"))
                    {
                        label = "Subsurface Mask (Mask Blue)";
                    }

                    if (label == "Subsurface Mask" && material.shader.name.Contains("Cross"))
                    {
                        label = "Subsurface Mask (Mask Blue)";
                    }

                    if (material.HasProperty("_DetailMapsMode"))
                    {
                        if (material.GetInt("_DetailMapsMode") == 0)
                        {
                            if (label == "Detail Metallic")
                            {
                                label = "Detail Metallic (Mask Red)";
                            }

                            if (label == "Detail Occlusion")
                            {
                                label = "Detail Occlusion (Mask Green)";
                            }

                            if (label == "Detail Smoothness")
                            {
                                label = "Detail Smoothness (Mask Alpha)";
                            }
                        }
                        else
                        {
                            if (label == "Detail Smoothness")
                            {
                                label = "Detail Smoothness (Mask Blue)";
                            }
                        }
                    }

                    if (label == "Detail Mask Source")
                    {
                        label = "Detail Mask Source (Mask Blue)";
                    }

                    if (label == "Detail Mask Invert")
                    {
                        label = "Detail Mask Invert (Detail Mask)";
                    }

                    if (material.HasProperty("_DetailTypeMode"))
                    {
                        if (material.GetInt("_DetailTypeMode") == 0)
                        {
                            if (label == "Detail Mask Offset")
                            {
                                label = "Detail Mask Offset (Vertex Blue)";
                            }
                        }
                        else
                        {
                            if (label == "Detail Mask Offset")
                            {
                                label = "Detail Mask Offset (Projection Mask)";
                            }
                        }
                    }
                    else
                    {
                        if (label == "Detail Mask Offset")
                        {
                            label = "Detail Mask Offset (Vertex Blue)";
                        }
                    }
                }

                var debug = "";

                if (showInternalProperties)
                {
                    debug = "  (" + customPropsList[i].name + ")";
                }

                materialEditor.ShaderProperty(customPropsList[i], label + debug);
            }
        }

        materialEditor.EnableInstancingField();

        GUILayout.Space(10);

        materialEditor.DoubleSidedGIField();
        materialEditor.LightmapEmissionProperty(0);
        materialEditor.RenderQueueField();

        GUILayout.Space(10);

        TVEShaderUtils.DrawCopySettingsFromGameObject(material);

        GUILayout.Space(10);

        showInternalProperties = EditorGUILayout.Toggle("Show Internal Properties", showInternalProperties);
        showHiddenProperties = EditorGUILayout.Toggle("Show Hidden Properties", showHiddenProperties);
        showAdditionalInfo = EditorGUILayout.Toggle("Show Additional Info", showAdditionalInfo);

        if (showAdditionalInfo)
        {
            GUILayout.Space(10);

            TVEShaderUtils.DrawTechnicalDetails(material);
        }

        GUILayout.Space(20);

        TVEShaderUtils.DrawPoweredByTheVegetationEngine();
    }
}

