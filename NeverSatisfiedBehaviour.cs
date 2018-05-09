using nobnak.Gist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSatisfiedPackage {

	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class NeverSatisfiedBehaviour : MonoBehaviour {
		[SerializeField]
		protected TextureEvent TextureOnUpdate;
		
        [SerializeField]
        protected Vector4 lasting = new Vector4 (1f, 1f, 0f, 0f);
		
		protected NeverSatisfied neversat;

		#region Unity
		protected void OnEnable() {
			neversat = new NeverSatisfied();

			neversat.TextureOnUpdate += tex => TextureOnUpdate.Invoke(tex.Target);
        }
		protected void OnRenderImage(RenderTexture src, RenderTexture dst) {
			neversat.Lasting = lasting;
			neversat.Step(src);
			Graphics.Blit(src, dst);
        }
		protected void OnDisable() {
			neversat.Dispose();
        }
		#endregion
	}
}
