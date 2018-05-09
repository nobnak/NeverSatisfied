using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour {
    public float scale = 1f;
    public float freq = 0.1f;

    Vector3 rands;
    Vector3 offset;
    Vector3 seed;

    void OnEnable() {
        rands = 0.01f * new Vector3 (Random.value, Random.value, Random.value);
        offset = 100f * new Vector3 (Random.value, Random.value, Random.value);
        seed = 1000f * new Vector3 (Random.value, Random.value, Random.value);
    }
	void Update () {
        var t = freq * Time.timeSinceLevelLoad;

        transform.position = new Vector3 (
            scale * Noise (t + offset.x, t * rands.x + seed.x),
            scale * Noise (t + offset.y, t * rands.y + seed.y),
            scale * Noise (t + offset.z, t * rands.z + seed.z));
	}

    static float Noise(float x, float y) {
        return 2f * Mathf.PerlinNoise (x, y) - 1f;
    }
}
