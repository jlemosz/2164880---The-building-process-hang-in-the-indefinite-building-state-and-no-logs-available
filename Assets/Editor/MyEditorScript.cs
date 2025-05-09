using UnityEditor;
using UnityEngine;

class MyEditorScript
{
    static void PerformBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.target = BuildTarget.StandaloneOSX; // More details on BuildTarget doc
        buildPlayerOptions.options = BuildOptions.StrictMode;
        buildPlayerOptions.locationPathName = "OSXBuild";

        // Force a lighting configuration that can trigger the error
        ForceGPUUnsupportedSettings();

        var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

        // Check the result
        if (buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
        {
            Debug.LogError("Build failed due to errors reported during the build.");
        }
        else if (buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded without any errors.");
        }
        else
        {
            Debug.LogWarning("Build completed with warnings or unexpected results.");
        }
    }

    private static void ForceGPUUnsupportedSettings()
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