# Unity Optimization Command

## Mục đích
Tối ưu hóa Unity project để cải thiện performance

## Các lĩnh vực tối ưu

### 1. Graphics Optimization
- **Texture Optimization**:
  - Giảm kích thước texture không cần thiết
  - Sử dụng texture atlases
  - Tối ưu compression settings
- **Model Optimization**:
  - Giảm polygon count
  - Sử dụng LOD (Level of Detail)
  - Optimize mesh topology

### 2. Script Optimization
- **Performance Profiling**:
  - Sử dụng Unity Profiler
  - Identify performance bottlenecks
  - Optimize Update() methods
- **Memory Management**:
  - Object pooling
  - Garbage collection optimization
  - Resource loading/unloading

### 3. Audio Optimization
- **Compression Settings**:
  - Tối ưu audio compression
  - Sử dụng appropriate load types
  - Audio pooling

### 4. UI Optimization
- **Canvas Optimization**:
  - Separate UI canvases
  - Use UI pooling
  - Optimize draw calls

## Commands
- `unity-optimize:graphics` - Tối ưu graphics
- `unity-optimize:scripts` - Tối ưu scripts
- `unity-optimize:audio` - Tối ưu audio
- `unity-optimize:ui` - Tối ưu UI
- `unity-optimize:all` - Tối ưu toàn bộ

## Optimization Scripts

### Graphics Optimizer
```csharp
using UnityEngine;
using UnityEditor;
using System.IO;

public class GraphicsOptimizer : MonoBehaviour
{
    [MenuItem("Tools/Optimize Graphics")]
    public static void OptimizeGraphics()
    {
        // Optimize textures
        OptimizeTextures();
        
        // Optimize models
        OptimizeModels();
        
        // Optimize materials
        OptimizeMaterials();
        
        // Optimize shaders
        OptimizeShaders();
        
        Debug.Log("Graphics optimization completed!");
    }

    private static void OptimizeTextures()
    {
        string[] textureGuids = AssetDatabase.FindAssets("t:Texture2D");
        
        foreach (string guid in textureGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
            
            // Apply optimization settings
            importer.textureCompression = TextureImporterCompression.Compressed;
            importer.compressionQuality = 50;
            importer.maxTextureSize = 1024; // Reduce max size
            
            // Platform specific settings
            importer.SetPlatformTextureSettings("Android", 1024, TextureImporterFormat.ASTC_6x6);
            importer.SetPlatformTextureSettings("iPhone", 1024, TextureImporterFormat.ASTC_6x6);
            importer.SetPlatformTextureSettings("Standalone", 1024, TextureImporterFormat.DXT5);
            
            AssetDatabase.ImportAsset(path);
        }
    }

    private static void OptimizeModels()
    {
        string[] modelGuids = AssetDatabase.FindAssets("t:Model");
        
        foreach (string guid in modelGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = (ModelImporter)AssetImporter.GetAtPath(path);
            
            // Apply optimization settings
            importer.meshCompression = ModelImporterMeshCompression.Medium;
            importer.isReadable = false;
            importer.optimizeMesh = true;
            importer.importBlendShapes = false;
            
            AssetDatabase.ImportAsset(path);
        }
    }

    private static void OptimizeMaterials()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        
        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            
            // Check for unused properties
            if (material.HasProperty("_Metallic") && material.GetFloat("_Metallic") == 0)
            {
                material.DisableKeyword("_METALLICGLOSSMAP");
            }
            
            EditorUtility.SetDirty(material);
        }
    }

    private static void OptimizeShaders()
    {
        string[] shaderGuids = AssetDatabase.FindAssets("t:Shader");
        
        foreach (string guid in shaderGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
            
            // Check for unused shader variants
            // This is a simplified example
            EditorUtility.SetDirty(shader);
        }
    }
}
```

### Script Optimizer
```csharp
public class ScriptOptimizer : MonoBehaviour
{
    [MenuItem("Tools/Optimize Scripts")]
    public static void OptimizeScripts()
    {
        // Find all scripts
        string[] scriptGuids = AssetDatabase.FindAssets("t:MonoScript");
        
        foreach (string guid in scriptGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            
            // Analyze script for optimization opportunities
            AnalyzeScript(script);
        }
        
        Debug.Log("Script optimization analysis completed!");
    }

    private static void AnalyzeScript(MonoScript script)
    {
        // This is a simplified example
        // In practice, you would use reflection or AST parsing
        // to analyze the script for optimization opportunities
        
        string scriptContent = script.text;
        
        // Check for common performance issues
        if (scriptContent.Contains("FindObjectOfType"))
        {
            Debug.LogWarning($"Script {script.name} uses FindObjectOfType - consider caching references");
        }
        
        if (scriptContent.Contains("new Vector3"))
        {
            Debug.LogWarning($"Script {script.name} creates Vector3 objects - consider reusing objects");
        }
        
        if (scriptContent.Contains("string +"))
        {
            Debug.LogWarning($"Script {script.name} uses string concatenation - consider StringBuilder");
        }
    }
}
```

### Audio Optimizer
```csharp
public class AudioOptimizer : MonoBehaviour
{
    [MenuItem("Tools/Optimize Audio")]
    public static void OptimizeAudio()
    {
        // Find all audio clips
        string[] audioGuids = AssetDatabase.FindAssets("t:AudioClip");
        
        foreach (string guid in audioGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AudioImporter importer = (AudioImporter)AssetImporter.GetAtPath(path);
            
            // Apply optimization settings
            importer.defaultSampleSettings = new AudioImporterSampleSettings
            {
                loadType = AudioClipLoadType.CompressedInMemory,
                compressionFormat = AudioCompressionFormat.Vorbis,
                quality = 0.5f, // Reduce quality for smaller file size
                sampleRateOverride = 44100
            };
            
            AssetDatabase.ImportAsset(path);
        }
        
        Debug.Log("Audio optimization completed!");
    }
}
```

### UI Optimizer
```csharp
public class UIOptimizer : MonoBehaviour
{
    [MenuItem("Tools/Optimize UI")]
    public static void OptimizeUI()
    {
        // Find all canvases
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        
        foreach (Canvas canvas in canvases)
        {
            OptimizeCanvas(canvas);
        }
        
        Debug.Log("UI optimization completed!");
    }

    private static void OptimizeCanvas(Canvas canvas)
    {
        // Set appropriate render mode
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            // Check if canvas needs world space rendering
            if (canvas.worldCamera != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
            }
        }
        
        // Optimize canvas components
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        if (raycaster != null)
        {
            raycaster.ignoreReversedGraphics = true;
        }
        
        // Optimize UI elements
        OptimizeUIElements(canvas.transform);
    }

    private static void OptimizeUIElements(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Check for unnecessary components
            if (child.GetComponent<Image>() != null && child.GetComponent<Button>() == null)
            {
                // Remove Image component if not needed
                Image image = child.GetComponent<Image>();
                if (image.sprite == null && image.color.a == 0)
                {
                    DestroyImmediate(image);
                }
            }
            
            // Recursively optimize child elements
            OptimizeUIElements(child);
        }
    }
}
```

## Performance Profiler

### Performance Profiler Script
```csharp
public class PerformanceProfiler : MonoBehaviour
{
    [MenuItem("Tools/Profile Performance")]
    public static void ProfilePerformance()
    {
        // Start profiling
        UnityEngine.Profiling.Profiler.BeginSample("Performance Profile");
        
        // Profile different systems
        ProfileGraphics();
        ProfileScripts();
        ProfileAudio();
        ProfileUI();
        
        // End profiling
        UnityEngine.Profiling.Profiler.EndSample();
        
        Debug.Log("Performance profiling completed!");
    }

    private static void ProfileGraphics()
    {
        // Profile graphics performance
        Debug.Log($"Draw Calls: {UnityStats.drawCalls}");
        Debug.Log($"Batches: {UnityStats.batches}");
        Debug.Log($"Triangles: {UnityStats.triangles}");
        Debug.Log($"Vertices: {UnityStats.vertices}");
    }

    private static void ProfileScripts()
    {
        // Profile script performance
        Debug.Log($"Scripts: {FindObjectsOfType<MonoBehaviour>().Length}");
        Debug.Log($"GameObjects: {FindObjectsOfType<GameObject>().Length}");
    }

    private static void ProfileAudio()
    {
        // Profile audio performance
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        Debug.Log($"Audio Sources: {audioSources.Length}");
        
        int playingCount = 0;
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                playingCount++;
            }
        }
        Debug.Log($"Playing Audio Sources: {playingCount}");
    }

    private static void ProfileUI()
    {
        // Profile UI performance
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        Debug.Log($"Canvases: {canvases.Length}");
        
        int totalUIElements = 0;
        foreach (Canvas canvas in canvases)
        {
            totalUIElements += canvas.GetComponentsInChildren<Graphic>().Length;
        }
        Debug.Log($"UI Elements: {totalUIElements}");
    }
}
```

## Memory Optimizer

### Memory Optimizer Script
```csharp
public class MemoryOptimizer : MonoBehaviour
{
    [MenuItem("Tools/Optimize Memory")]
    public static void OptimizeMemory()
    {
        // Force garbage collection
        System.GC.Collect();
        
        // Log memory usage
        LogMemoryUsage();
        
        // Optimize memory usage
        OptimizeTextureMemory();
        OptimizeAudioMemory();
        OptimizeScriptMemory();
        
        Debug.Log("Memory optimization completed!");
    }

    private static void LogMemoryUsage()
    {
        Debug.Log($"Total Memory: {System.GC.GetTotalMemory(false) / 1024f / 1024f:F1} MB");
        Debug.Log($"Used Memory: {UnityEngine.Profiling.Profiler.GetTotalAllocatedMemory(false) / 1024f / 1024f:F1} MB");
        Debug.Log($"Reserved Memory: {UnityEngine.Profiling.Profiler.GetTotalReservedMemory(false) / 1024f / 1024f:F1} MB");
    }

    private static void OptimizeTextureMemory()
    {
        // Unload unused textures
        Resources.UnloadUnusedAssets();
        
        // Force garbage collection
        System.GC.Collect();
    }

    private static void OptimizeAudioMemory()
    {
        // Stop unused audio sources
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying && source.clip != null)
            {
                source.clip = null;
            }
        }
    }

    private static void OptimizeScriptMemory()
    {
        // Find and optimize scripts with memory issues
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        
        foreach (MonoBehaviour script in scripts)
        {
            // This is a simplified example
            // In practice, you would analyze each script for memory optimization opportunities
        }
    }
}
```

## Build Size Optimizer

### Build Size Optimizer Script
```csharp
public class BuildSizeOptimizer : MonoBehaviour
{
    [MenuItem("Tools/Optimize Build Size")]
    public static void OptimizeBuildSize()
    {
        // Strip unused code
        PlayerSettings.stripEngineCode = true;
        
        // Optimize for size
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
        
        // Compress textures
        CompressTextures();
        
        // Compress audio
        CompressAudio();
        
        // Remove unused assets
        RemoveUnusedAssets();
        
        Debug.Log("Build size optimization completed!");
    }

    private static void CompressTextures()
    {
        string[] textureGuids = AssetDatabase.FindAssets("t:Texture2D");
        
        foreach (string guid in textureGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
            
            // Apply aggressive compression
            importer.textureCompression = TextureImporterCompression.Compressed;
            importer.compressionQuality = 25; // Lower quality for smaller size
            
            AssetDatabase.ImportAsset(path);
        }
    }

    private static void CompressAudio()
    {
        string[] audioGuids = AssetDatabase.FindAssets("t:AudioClip");
        
        foreach (string guid in audioGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AudioImporter importer = (AudioImporter)AssetImporter.GetAtPath(path);
            
            // Apply aggressive compression
            importer.defaultSampleSettings = new AudioImporterSampleSettings
            {
                loadType = AudioClipLoadType.CompressedInMemory,
                compressionFormat = AudioCompressionFormat.Vorbis,
                quality = 0.3f, // Lower quality for smaller size
                sampleRateOverride = 22050 // Lower sample rate
            };
            
            AssetDatabase.ImportAsset(path);
        }
    }

    private static void RemoveUnusedAssets()
    {
        // This is a simplified example
        // In practice, you would use more sophisticated methods to identify unused assets
        
        // Find assets not referenced in scenes
        string[] assetGuids = AssetDatabase.FindAssets("t:Object");
        
        foreach (string guid in assetGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            
            // Check if asset is referenced
            if (!IsAssetReferenced(path))
            {
                // Mark for deletion (don't actually delete in this example)
                Debug.Log($"Unused asset found: {path}");
            }
        }
    }

    private static bool IsAssetReferenced(string assetPath)
    {
        // This is a simplified example
        // In practice, you would use more sophisticated methods to check references
        
        // Check if asset is in Resources folder
        if (assetPath.Contains("Resources/"))
        {
            return true;
        }
        
        // Check if asset is referenced in scenes
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
        
        foreach (string sceneGuid in sceneGuids)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            // Check if asset is referenced in scene
            // This would require more complex implementation
        }
        
        return false;
    }
}
```

## Best Practices

### 1. Regular Optimization
- Run optimization tools regularly
- Monitor performance metrics
- Test on target devices
- Update optimization settings

### 2. Performance Monitoring
- Use Unity Profiler
- Monitor memory usage
- Check frame rate
- Analyze build size

### 3. Optimization Strategy
- Optimize bottlenecks first
- Measure before and after
- Test on multiple devices
- Consider platform differences

### 4. Code Quality
- Avoid expensive operations in Update
- Use object pooling
- Cache frequently used references
- Minimize garbage collection

---

**Lưu ý**: Optimization là quá trình liên tục. Luôn test và measure để đảm bảo improvements thực sự hiệu quả.
