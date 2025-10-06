# Unity Build Command

## Mục đích
Tự động hóa quá trình build Unity project cho các platform khác nhau

## Quy trình thực hiện

### 1. Pre-build Checks
- Kiểm tra Unity version compatibility
- Verify build settings
- Check for missing references
- Validate scene dependencies
- Run automated tests

### 2. Build Configuration
- **PC (Windows)**: Standalone Windows 64-bit
- **Mac**: Standalone OSX Universal
- **Linux**: Standalone Linux 64-bit
- **Android**: Android APK/AAB
- **iOS**: iOS Xcode project
- **WebGL**: WebGL

### 3. Build Settings
```csharp
// Build settings cho PC
BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
buildPlayerOptions.scenes = new[] { "Assets/Scenes/Main.unity" };
buildPlayerOptions.locationPathName = "Builds/PC/Game.exe";
buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
buildPlayerOptions.options = BuildOptions.None;
```

### 4. Post-build Actions
- Copy additional files
- Run post-processing scripts
- Generate build report
- Upload to distribution platform

## Commands
- `unity-build:pc` - Build cho Windows
- `unity-build:android` - Build cho Android
- `unity-build:ios` - Build cho iOS
- `unity-build:webgl` - Build cho WebGL
- `unity-build:all` - Build tất cả platforms

## Build Scripts

### PC Build Script
```csharp
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.IO;

public class PCBuildScript
{
    [MenuItem("Build/PC Build")]
    public static void BuildPC()
    {
        // Pre-build checks
        if (!PreBuildChecks())
        {
            Debug.LogError("Pre-build checks failed!");
            return;
        }

        // Build settings
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/PC/Game.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;

        // Build
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("PC Build succeeded!");
            PostBuildActions("PC");
        }
        else
        {
            Debug.LogError("PC Build failed!");
        }
    }

    private static bool PreBuildChecks()
    {
        // Check for missing references
        if (HasMissingReferences())
        {
            Debug.LogError("Missing references found!");
            return false;
        }

        // Check scene dependencies
        if (!ValidateScenes())
        {
            Debug.LogError("Scene validation failed!");
            return false;
        }

        return true;
    }

    private static bool HasMissingReferences()
    {
        // Implementation to check for missing references
        return false;
    }

    private static bool ValidateScenes()
    {
        // Implementation to validate scenes
        return true;
    }

    private static string[] GetScenePaths()
    {
        return new string[]
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/GameLevel.unity",
            "Assets/Scenes/GameOver.unity"
        };
    }

    private static void PostBuildActions(string platform)
    {
        // Copy additional files
        CopyAdditionalFiles(platform);
        
        // Generate build report
        GenerateBuildReport(platform);
        
        // Upload to distribution platform
        UploadToDistribution(platform);
    }

    private static void CopyAdditionalFiles(string platform)
    {
        string buildPath = $"Builds/{platform}/";
        
        // Copy README
        if (File.Exists("README.md"))
        {
            File.Copy("README.md", buildPath + "README.md");
        }
        
        // Copy configuration files
        if (Directory.Exists("Config"))
        {
            CopyDirectory("Config", buildPath + "Config");
        }
    }

    private static void GenerateBuildReport(string platform)
    {
        string reportPath = $"Builds/{platform}/BuildReport.txt";
        string report = $"Build Report for {platform}\n";
        report += $"Build Time: {System.DateTime.Now}\n";
        report += $"Unity Version: {Application.unityVersion}\n";
        report += $"Platform: {platform}\n";
        
        File.WriteAllText(reportPath, report);
    }

    private static void UploadToDistribution(string platform)
    {
        // Implementation for uploading to distribution platform
        Debug.Log($"Uploading {platform} build to distribution platform...");
    }

    private static void CopyDirectory(string sourceDir, string destDir)
    {
        Directory.CreateDirectory(destDir);
        
        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string fileName = Path.GetFileName(file);
            File.Copy(file, Path.Combine(destDir, fileName));
        }
        
        foreach (string subDir in Directory.GetDirectories(sourceDir))
        {
            string dirName = Path.GetFileName(subDir);
            CopyDirectory(subDir, Path.Combine(destDir, dirName));
        }
    }
}
```

### Android Build Script
```csharp
public class AndroidBuildScript
{
    [MenuItem("Build/Android Build")]
    public static void BuildAndroid()
    {
        // Pre-build checks
        if (!PreBuildChecks())
        {
            Debug.LogError("Pre-build checks failed!");
            return;
        }

        // Android specific settings
        PlayerSettings.Android.bundleVersionCode = GetVersionCode();
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel21;

        // Build settings
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/Android/Game.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        // Build
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Android Build succeeded!");
            PostBuildActions("Android");
        }
        else
        {
            Debug.LogError("Android Build failed!");
        }
    }

    private static int GetVersionCode()
    {
        // Get version code from version control or configuration
        return 1;
    }
}
```

### iOS Build Script
```csharp
public class iOSBuildScript
{
    [MenuItem("Build/iOS Build")]
    public static void BuildiOS()
    {
        // Pre-build checks
        if (!PreBuildChecks())
        {
            Debug.LogError("Pre-build checks failed!");
            return;
        }

        // iOS specific settings
        PlayerSettings.iOS.bundleVersion = GetBundleVersion();
        PlayerSettings.iOS.targetOSVersionString = "12.0";
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;

        // Build settings
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/iOS/";
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;

        // Build
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("iOS Build succeeded!");
            PostBuildActions("iOS");
        }
        else
        {
            Debug.LogError("iOS Build failed!");
        }
    }

    private static string GetBundleVersion()
    {
        // Get bundle version from version control or configuration
        return "1.0.0";
    }
}
```

### WebGL Build Script
```csharp
public class WebGLBuildScript
{
    [MenuItem("Build/WebGL Build")]
    public static void BuildWebGL()
    {
        // Pre-build checks
        if (!PreBuildChecks())
        {
            Debug.LogError("Pre-build checks failed!");
            return;
        }

        // WebGL specific settings
        PlayerSettings.WebGL.memorySize = 512;
        PlayerSettings.WebGL.dataCaching = true;
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;

        // Build settings
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/WebGL/";
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.None;

        // Build
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("WebGL Build succeeded!");
            PostBuildActions("WebGL");
        }
        else
        {
            Debug.LogError("WebGL Build failed!");
        }
    }
}
```

## Build Automation

### Command Line Build
```bash
# Windows
Unity.exe -batchmode -quit -projectPath "C:\MyProject" -executeMethod PCBuildScript.BuildPC

# Mac
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath "/Users/MyProject" -executeMethod PCBuildScript.BuildPC

# Linux
/opt/unity/Editor/Unity -batchmode -quit -projectPath "/home/MyProject" -executeMethod PCBuildScript.BuildPC
```

### CI/CD Integration
```yaml
# GitHub Actions example
name: Unity Build
on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    
    - name: Cache Unity Library
      uses: actions/cache@v2
      with:
        path: Library
        key: Library-${{ hashFiles('Assets/**', 'Packages/**', 'ProjectSettings/**') }}
        restore-keys: |
          Library-
    
    - name: Build PC
      uses: game-ci/unity-builder@v2
      with:
        targetPlatform: StandaloneWindows64
        buildName: MyGame
        buildPath: Builds/PC
        
    - name: Build Android
      uses: game-ci/unity-builder@v2
      with:
        targetPlatform: Android
        buildName: MyGame
        buildPath: Builds/Android
```

## Build Optimization

### Build Size Optimization
```csharp
// Build size optimization
public class BuildOptimizer
{
    [MenuItem("Build/Optimize Build Size")]
    public static void OptimizeBuildSize()
    {
        // Strip unused code
        PlayerSettings.stripEngineCode = true;
        
        // Compress textures
        OptimizeTextures();
        
        // Compress audio
        OptimizeAudio();
        
        // Remove unused assets
        RemoveUnusedAssets();
    }

    private static void OptimizeTextures()
    {
        // Implementation for texture optimization
    }

    private static void OptimizeAudio()
    {
        // Implementation for audio optimization
    }

    private static void RemoveUnusedAssets()
    {
        // Implementation for removing unused assets
    }
}
```

### Build Performance Optimization
```csharp
// Build performance optimization
public class BuildPerformanceOptimizer
{
    [MenuItem("Build/Optimize Build Performance")]
    public static void OptimizeBuildPerformance()
    {
        // Enable build caching
        EditorPrefs.SetBool("BuildPlayerWindow.DevelopmentBuild", false);
        
        // Optimize compilation
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
        
        // Enable incremental builds
        EditorPrefs.SetBool("BuildPlayerWindow.IncrementalBuild", true);
    }
}
```

## Build Validation

### Build Validation Script
```csharp
public class BuildValidator
{
    [MenuItem("Build/Validate Build")]
    public static void ValidateBuild()
    {
        bool isValid = true;
        
        // Check for missing references
        if (HasMissingReferences())
        {
            Debug.LogError("Build validation failed: Missing references found!");
            isValid = false;
        }
        
        // Check for unused assets
        if (HasUnusedAssets())
        {
            Debug.LogWarning("Build validation warning: Unused assets found!");
        }
        
        // Check for performance issues
        if (HasPerformanceIssues())
        {
            Debug.LogWarning("Build validation warning: Performance issues found!");
        }
        
        if (isValid)
        {
            Debug.Log("Build validation passed!");
        }
    }

    private static bool HasMissingReferences()
    {
        // Implementation to check for missing references
        return false;
    }

    private static bool HasUnusedAssets()
    {
        // Implementation to check for unused assets
        return false;
    }

    private static bool HasPerformanceIssues()
    {
        // Implementation to check for performance issues
        return false;
    }
}
```

## Best Practices

### 1. Pre-build Checklist
- [ ] All scenes are valid and loadable
- [ ] No missing references
- [ ] All required assets are included
- [ ] Build settings are correct
- [ ] Version numbers are updated
- [ ] Tests are passing

### 2. Build Optimization
- Use incremental builds when possible
- Enable build caching
- Strip unused code
- Compress assets
- Remove unused assets

### 3. Post-build Actions
- Copy additional files
- Generate build reports
- Upload to distribution
- Notify team members
- Archive builds

### 4. Error Handling
- Implement proper error handling
- Log build errors
- Send notifications on failure
- Retry failed builds
- Clean up on failure

---

**Lưu ý**: Luôn test builds trên target platforms trước khi release.
