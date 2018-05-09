using nobnak.Gist.ObjectExt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSatisfiedPackage {
        
	[System.Serializable]
    public class NeverSatisfied : System.IDisposable {
		public const string FILENAME_MATERIAL = "NeverSatisfied";

		public const string PROP_SOURCE_TEX = "_SourceTex";
        public const string PROP_LASTING = "_Lasting";
		
		public event System.Action<NeverSatisfied> TextureOnUpdate;

		[SerializeField]
        protected Vector4 lasting = new Vector4 (1f, 1f, 0f, 0f);

        Material mat;
        RenderTexture tmp0, tmp1;

		#region IDisposable
		public void Dispose() {
			ReleaseTextures();
		}
		#endregion

		#region public
		public void Step(RenderTexture src, float dt) {
			if (tmp0 == null || tmp0.width != src.width || tmp0.height != src.height) {
				ReleaseTextures();
				tmp0 = new RenderTexture(src.width, src.height, 0, RenderTextureFormat.ARGBFloat);
				tmp1 = new RenderTexture(tmp0);
			}

			Mat.SetVector(PROP_LASTING, dt * lasting);
			Mat.SetTexture(PROP_SOURCE_TEX, src);
			Graphics.Blit(tmp0, tmp1, Mat);
			SwapTextures();
			NotifyTextureOnUpdate();
		}

		public void Step(RenderTexture src) {
			Step(src, Time.deltaTime);
		}
		public Vector4 Lasting {
			get { return lasting; }
			set { lasting = value; }
		}
		public Texture Target { get { return tmp0; } }
		#endregion

		#region private
		protected Material Mat {
			get {
				if (mat == null)
					mat = Resources.Load<Material>(FILENAME_MATERIAL);
				return mat;
			}
		}
		protected void NotifyTextureOnUpdate() {
			if (TextureOnUpdate != null)
				TextureOnUpdate(this);
		}
		protected void ReleaseTextures() {
			tmp0.Destroy();
			tmp1.Destroy();
		}
        protected void SwapTextures () {
            var t = tmp0;
            tmp0 = tmp1;
            tmp1 = t;
        }
		#endregion
	}
}
