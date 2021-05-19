// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TheVegetationEngine
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
    [AddComponentMenu("BOXOPHOBIC/The Vegetation Engine/TVE Volume")]
#endif
    public class TVEVolume : StyledMonoBehaviour
    {
        [StyledBanner(0.890f, 0.745f, 0.309f, "Volume", "", "https://docs.google.com/document/d/145JOVlJ1tE-WODW45YoJ6Ixg23mFc56EnB_8Tbwloz8/edit#heading=h.5mdxx6mkzwhn")]
        public bool styledBanner;

        [StyledCategory("Volume Settings", 5, 10)]
        public bool volumeCat;

#if UNITY_EDITOR
        [StyledMessage("Warning", "Volume data is missing! Create a new buffer to control the shaders by right clicking in the project window > BOXOPHOBIC > The Vegetation Engine > Volume Data.", 0, 10)]
        public bool missingBufferMessage = false;

        [StyledMessage("Info", "This volume uses a built-in buffer data! Built-in buffers will reset to default on upgrading the asset to a new version! Make sure to create your own buffers to change the setting by right clicking in the project window > BOXOPHOBIC > The Vegetation Engine > Volume Data.", 0, 10)]
        public bool internalBufferMessage = false;
#endif
        [Tooltip("Controls the Volume gizmo color in scene view.")]
        public Color volumeGizmo = Color.cyan;
        [Tooltip("Sets the Volume buffer data which will render the elements based on their type.")]
        public TVEVolumeData volumeData;

        //[StyledCategory("Follow Settings")]
        //public bool followCat;

        [Space(10)]
        [Tooltip("Sets a gameobject to follow when more resolutions is needed for the Volume buffer renderer.")]
        public Transform followTransform;
        [Tooltip("Controls the offset from the Follow Transform postion.")]
        public Vector3 followOffset;

        //#if UNITY_EDITOR
        //        [StyledCategory("Buffer Preview")]
        //        public bool previewCat;

        //        [StyledTexturePreview]
        //        public RenderTexture internalTex;
        //#endif

        [StyledSpace(5)]
        public bool styledSpace0;

        void OnEnable()
        {
            AddEntityToVolume();
        }

        void OnDestroy()
        {
            RemoveEntityFromVolume();
        }

        void OnDisable()
        {
            RemoveEntityFromVolume();
        }

        void Update()
        {
            gameObject.transform.eulerAngles = Vector3.zero;

            if (followTransform != null && volumeData != null)
            {
                UpdateVolumeTransformWithFollow();
            }

#if UNITY_EDITOR
            //if (volumeData != null && volumeData.internalTex != null)
            //{
            //    internalTex = volumeData.internalTex;
            //}
            //else
            //{
            //    internalTex = null;
            //}

            if (Application.isPlaying)
            {
                return;
            }

            if (volumeData == null)
            {
                missingBufferMessage = true;
                internalBufferMessage = false;
            }
            else
            {
                missingBufferMessage = false;

                if (volumeData.name.Contains("Default"))
                {
                    internalBufferMessage = true;
                }
                else
                {
                    internalBufferMessage = false;
                }
            }
#endif
        }

        void AddEntityToVolume()
        {
            if (TVEManager.Instance == null)
                return;

            var entities = TVEManager.Instance.globalVolume.volumeEntities;
            var entityExists = false;

            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i] == this)
                    {
                        entityExists = true;
                        break;
                    }
                }

                if (entityExists == false)
                {
                    TVEManager.Instance.globalVolume.volumeEntities.Add(this);
                }
            }
        }

        void RemoveEntityFromVolume()
        {
            if (TVEManager.Instance == null)
                return;

            var entities = TVEManager.Instance.globalVolume.volumeEntities;

            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i] == this)
                    {
                        entities.RemoveAt(i);
                    }
                }
            }
        }

        //void UpdateVolumeTransform()
        //{
        //    float gridX = transform.lossyScale.x / (float)volumeData.textureResolution;
        //    float posX = Mathf.Round(transform.position.x / gridX) * gridX;

        //    float gridZ = transform.lossyScale.z / (float)volumeData.textureResolution;
        //    float posZ = Mathf.Round(transform.position.z / gridZ) * gridZ;

        //    var position = new Vector3(posX, transform.position.y, posZ);

        //    gameObject.transform.position = position;
        //}

        void UpdateVolumeTransformWithFollow()
        {
            float gridX = transform.lossyScale.x / (float)volumeData.textureResolution;
            float posX = Mathf.Round((followTransform.position.x + followOffset.x) / gridX) * gridX;

            float gridZ = transform.lossyScale.z / (float)volumeData.textureResolution;
            float posZ = Mathf.Round((followTransform.position.z + followOffset.z) / gridZ) * gridZ;

            var position = new Vector3(posX, followTransform.position.y + followOffset.y, posZ);

            gameObject.transform.position = position;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(volumeGizmo.r, volumeGizmo.g, volumeGizmo.b, volumeGizmo.a);
            Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.1f);
            Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));
        }
    }
}
