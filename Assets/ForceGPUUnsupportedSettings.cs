using UnityEditor;
using UnityEngine;

public class ForceGPUUnsupportedSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get active lighting settings or create a new one
        LightingSettings lightingSettings = new LightingSettings();

        // Set progressive GPU as the lightmapper
        lightingSettings.lightmapper = LightingSettings.Lightmapper.ProgressiveGPU;

        // Force very high resolution to stress the GPU
        lightingSettings.lightmapResolution = 4096;  // Unrealistically high resolution per texel
        lightingSettings.indirectResolution = 4;     // Very high indirect light resolution

        // Create and apply the new settings
        Lightmapping.lightingSettings = lightingSettings;

        Debug.LogWarning("Forced settings to trigger GPU fallback to CPU lightmapper.");
    }
}