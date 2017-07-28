using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSatisfiedPackage {
        
    [ExecuteInEditMode]
    public class NeverSatisfied : MonoBehaviour {
        const string PROP_TARGET_TEX = "_TargetTex";
        const string PROP_LASTING = "_Lasting";

        [SerializeField]
        Shader shader;
        [SerializeField]
        Vector4 lasting = new Vector4 (1f, 1f, 0f, 0f);

        Material mat;
        RenderTexture tmp0, tmp1;

        void OnEnable() {
            mat = new Material (shader);
        }
        void OnRenderImage(RenderTexture src, RenderTexture dst) {
            if (tmp0 == null || tmp0.width != src.width || tmp0.height != src.height) {
                ReleaseObject (tmp0);
                ReleaseObject (tmp1);
                tmp0 = new RenderTexture (src.width, src.height, 0, RenderTextureFormat.ARGBFloat);
                tmp1 = new RenderTexture (tmp0);
            }

            mat.SetVector (PROP_LASTING, lasting);
            mat.SetTexture (PROP_TARGET_TEX, src);
            Graphics.Blit (tmp0, tmp1, mat);
            SwapTextures ();
            Graphics.Blit (tmp0, dst);
        }
        void OnDisable() {
            ReleaseObject (mat);
        }

        void SwapTextures () {
            var t = tmp0;
            tmp0 = tmp1;
            tmp1 = t;
        }

        static void ReleaseObject (Object obj) {
            if (Application.isPlaying)
                Destroy (obj);
            else
                DestroyImmediate (obj);
        }
    }
}
