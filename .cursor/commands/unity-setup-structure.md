# Unity Setup Structure (Cursor Command)

## Mục đích
Tạo cấu trúc thư mục Unity theo chuẩn `_Core/_Features/_Game/Plugins` và sinh `asmdef` mẫu, chạy từ shell (PowerShell/Bash) — không cần mở Unity.

## Targets
- Folders theo `rules/00_unity-project-overview.mdc`
- `asmdef`:
  - `Assets/_Core/_Core.asmdef` (no references)
  - `Assets/_Features/_Features.asmdef` (ref `_Core`)
  - `Assets/_Game/_Game.asmdef` (ref `_Core`, `_Features`)

## Lệnh nhanh (Windows PowerShell)
- Tạo thư mục:
```powershell
# Run at repo root
pwsh -NoProfile -Command "& { 
  $root = 'Assets'
  $folders = @(
    '_Core','_Core/DI','_Core/Events','_Core/TimeInput','_Core/SceneFlow','_Core/SaveData','_Core/Tick','_Core/Pooling','_Core/Audio','_Core/UI','_Core/Utils',
    '_Features',
    '_Game','_Game/Scripts','_Game/Data','_Game/Prefabs','_Game/Materials','_Game/Textures','_Game/Models','_Game/Audio','_Game/Scenes','_Game/Animations','_Game/Shaders','_Game/Resources','_Game/StreamingAssets',
    'Plugins'
  )
  foreach ($f in $folders) { $p = Join-Path $root $f; if (-not (Test-Path $p)) { New-Item -ItemType Directory -Path $p | Out-Null } }
  Write-Host 'Created/ensured folder structure.'
}"
```

- Tạo `asmdef`:
```powershell
# Run at repo root
pwsh -NoProfile -Command "& {
  function Write-Json($path, $obj) {
    $json = $obj | ConvertTo-Json -Depth 6
    $dir = Split-Path $path -Parent
    if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    Set-Content -Path $path -Value $json -Encoding UTF8
  }

  $core = @{
    name = '_Core'
    references = @()
    includePlatforms = @()
    excludePlatforms = @()
    allowUnsafeCode = $false
    overrideReferences = $false
    precompiledReferences = @()
    autoReferenced = $true
    defineConstraints = @()
    versionDefines = @()
    noEngineReferences = $false
  }
  Write-Json 'Assets/_Core/_Core.asmdef' $core

  $features = $core.PSObject.Copy()
  $features.name = '_Features'
  $features.references = @('_Core')
  Write-Json 'Assets/_Features/_Features.asmdef' $features

  $game = $core.PSObject.Copy()
  $game.name = '_Game'
  $game.references = @('_Core','_Features')
  Write-Json 'Assets/_Game/_Game.asmdef' $game

  Write-Host 'Created/updated asmdef files.'
}"
```

- Validate cấu trúc:
```powershell
pwsh -NoProfile -Command "& {
  $must = @(
    'Assets/_Core/DI','Assets/_Core/Events','Assets/_Core/TimeInput','Assets/_Core/SceneFlow','Assets/_Core/SaveData','Assets/_Core/Tick','Assets/_Core/Pooling','Assets/_Core/Audio','Assets/_Core/UI','Assets/_Core/Utils',
    'Assets/_Features',
    'Assets/_Game/Scripts','Assets/_Game/Data','Assets/_Game/Prefabs','Assets/_Game/Materials','Assets/_Game/Textures','Assets/_Game/Models','Assets/_Game/Audio','Assets/_Game/Scenes','Assets/_Game/Animations','Assets/_Game/Shaders','Assets/_Game/Resources','Assets/_Game/StreamingAssets',
    'Assets/Plugins'
  )
  $missing = @()
  foreach ($p in $must) { if (-not (Test-Path $p)) { $missing += $p } }
  foreach ($asm in @('Assets/_Core/_Core.asmdef','Assets/_Features/_Features.asmdef','Assets/_Game/_Game.asmdef')) { if (-not (Test-Path $asm)) { $missing += $asm } }
  if ($missing.Count -eq 0) { Write-Host 'Validate: Structure OK.' }
  else { Write-Host 'Missing:'; $missing | ForEach-Object { Write-Host " - $_" } }
}"
```

## Lệnh nhanh (macOS/Linux Bash)
- Tạo thư mục:
```bash
# Run at repo root
root="Assets"
folders=(
  "_Core" "_Core/DI" "_Core/Events" "_Core/TimeInput" "_Core/SceneFlow" "_Core/SaveData" "_Core/Tick" "_Core/Pooling" "_Core/Audio" "_Core/UI" "_Core/Utils"
  "_Features"
  "_Game" "_Game/Scripts" "_Game/Data" "_Game/Prefabs" "_Game/Materials" "_Game/Textures" "_Game/Models" "_Game/Audio" "_Game/Scenes" "_Game/Animations" "_Game/Shaders" "_Game/Resources" "_Game/StreamingAssets"
  "Plugins"
)
for f in "${folders[@]}"; do
  mkdir -p "${root}/${f}"
done
echo "Created/ensured folder structure."
```

- Tạo `asmdef`:
```bash
mkdir -p Assets/_Core Assets/_Features Assets/_Game

cat > Assets/_Core/_Core.asmdef << 'JSON'
{
  "name": "_Core",
  "references": [],
  "includePlatforms": [],
  "excludePlatforms": [],
  "allowUnsafeCode": false,
  "overrideReferences": false,
  "precompiledReferences": [],
  "autoReferenced": true,
  "defineConstraints": [],
  "versionDefines": [],
  "noEngineReferences": false
}
JSON

cat > Assets/_Features/_Features.asmdef << 'JSON'
{
  "name": "_Features",
  "references": ["_Core"],
  "includePlatforms": [],
  "excludePlatforms": [],
  "allowUnsafeCode": false,
  "overrideReferences": false,
  "precompiledReferences": [],
  "autoReferenced": true,
  "defineConstraints": [],
  "versionDefines": [],
  "noEngineReferences": false
}
JSON

cat > Assets/_Game/_Game.asmdef << 'JSON'
{
  "name": "_Game",
  "references": ["_Core","_Features"],
  "includePlatforms": [],
  "excludePlatforms": [],
  "allowUnsafeCode": false,
  "overrideReferences": false,
  "precompiledReferences": [],
  "autoReferenced": true,
  "defineConstraints": [],
  "versionDefines": [],
  "noEngineReferences": false
}
JSON

echo "Created/updated asmdef files."
```

- Validate cấu trúc:
```bash
missing=0
check() { if [ ! -e "$1" ]; then echo "Missing: $1"; missing=$((missing+1)); fi; }
for p in \
  Assets/_Core/DI Assets/_Core/Events Assets/_Core/TimeInput Assets/_Core/SceneFlow Assets/_Core/SaveData Assets/_Core/Tick Assets/_Core/Pooling Assets/_Core/Audio Assets/_Core/UI Assets/_Core/Utils \
  Assets/_Features \
  Assets/_Game/Scripts Assets/_Game/Data Assets/_Game/Prefabs Assets/_Game/Materials Assets/_Game/Textures Assets/_Game/Models Assets/_Game/Audio Assets/_Game/Scenes Assets/_Game/Animations Assets/_Game/Shaders Assets/_Game/Resources Assets/_Game/StreamingAssets \
  Assets/Plugins; do check "$p"; done
for a in Assets/_Core/_Core.asmdef Assets/_Features/_Features.asmdef Assets/_Game/_Game.asmdef; do check "$a"; done
if [ $missing -eq 0 ]; then echo "Validate: Structure OK."; fi
```

## Sử dụng nhanh
- Windows (PowerShell):
  - Chạy lần lượt 3 đoạn: Tạo thư mục → Tạo asmdef → Validate
- macOS/Linux (Bash):
  - Chạy lần lượt 3 đoạn tương ứng

## Ghi chú
- Theo quy tắc tham chiếu:
  - `_Game` → `_Core`, `_Features`
  - `_Features` → `_Core`
  - `_Core` → không tham chiếu module khác
- Đặt assets/gameplay vào `_Game`, modules tái sử dụng vào `_Features`, hạ tầng vào `_Core`.


