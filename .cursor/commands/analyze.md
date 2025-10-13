# Analyze Command - Phân tích và tìm kiếm vấn đề Unity (CHỈ PHÂN TÍCH)

## 🎯 Mục đích
- Tự động khảo sát bối cảnh dự án và cuộc trò chuyện.
- Thu thập bằng chứng (console, logs, cấu hình, scene/inspector).
- Thực hiện chuỗi kiểm tra sâu và có hệ thống theo chế độ.
- **CHỈ đưa ra phân tích, tìm kiếm nguyên nhân, không đưa giải pháp.**

## 🧩 Phạm vi
- Lỗi runtime/Editor, build fail, UI/Audio/Animation/Physics issues.
- Performance: CPU, GC, Draw Calls, Memory, UI Canvas.
- Kiến trúc: asmdef, DI, Update loop, Event usage, TickManager.
- Assets: import settings, missing GUID, Addressables (nếu có).

## 🧠 Đầu vào (Input)
- Mô tả triệu chứng/ngữ cảnh, log/error (nếu có).
- Chọn chế độ: `mode=error|performance|build|asset|architecture` (mặc định: auto).
- Tuỳ chọn: target scene, platform (Standalone/Android/iOS), Unity version.

Ví dụ:
```
analyze: mode=error
analyze: mode=performance platform=Android
analyze: Build Android fail với lỗi Gradle
```

## 📦 Đầu ra (Output)
- Tóm tắt cuộc trò chuyện (bắt buộc, tự động).
- Điều tra chi tiết + Unity Console analysis (nếu có).
- Vị trí vấn đề: script/component/GameObject/dòng code/scene.
- **Phân tích nguyên nhân gốc rễ** (không đưa giải pháp).
- Danh sách "Artifacts cần đính kèm" (log, screenshot, profiler capture).
- **Không có bảng phương án hay Best Choice.**

## 🛠️ Pipeline chuẩn (mọi chế độ)
### 0) Tóm tắt cuộc trò chuyện (BẮT BUỘC)
- Tự động chạy `/summarize-chat`.
- Gom ý chính, files/PRs/edits liên quan, context platform/Unity version.

### 1) Thu thập bằng chứng
- Console errors/warnings (stacktrace, grouping theo root cause).
- Editor logs / Player logs:
  - Windows: `%LOCALAPPDATA%\Unity\Editor\Editor.log`
  - Windows Player: `%USERPROFILE%\AppData\LocalLow\<Company>\<Product>\Player.log`
- Ảnh/chụp Inspector nếu có.
- `asmdef` graph, define symbols, Quality/Physics/Graphics settings.
- Nếu có Profiler: snapshot markers, GC alloc, Update count.

### 2) Kiểm tra tĩnh (static)
- Null/Missing Scripts trong scenes/prefabs.
- `asmdef`: tham chiếu chéo sai, vòng lặp, missing reference.
- `Resources/Addressables` (nếu dùng): key, label, load path.
- Naming/structure theo `_Core/_Features/_Game`; biên giới phụ thuộc.
- Update loop policy: Update rải rác vs `ITickable`.

### 3) Kiểm tra Scene/Inspector
- Missing references, inactive chains, Tag/Layer/SortingLayer sai.
- Physics: collision matrix, rigidbody/constraints.
- UI: Canvas phân tần suất, raycast targets, atlas.
- Audio: import/loadType, 3D settings.

### 4) Kiểm tra Performance (nếu mode=performance hoặc auto phát hiện)
- CPU: top markers, thời gian Update tổng, số lượng Update phân tán.
- GC: alloc per frame, boxing/strings/LINQ.
- Draw Calls/Batches, SetPass, Overdraw.
- Memory: mesh/texture/audio budgets theo chuẩn.

### 5) Kết luận nguyên nhân gốc rễ
- Ràng buộc nguyên nhân với bằng chứng, tái hiện điều kiện (scene, steps).
- **KHÔNG đưa ra giải pháp hay phương án.**

## 🔎 Chế độ chuyên biệt
- mode=error: Ưu tiên Console/stacktrace, MissingComponent/NullRef, Script/line pinpoint.
- mode=performance: Ưu tiên Profiler/GC, Update consolidation, batching, texture formats.
- mode=build: PlayerSettings, Gradle/Xcode logs, define symbols, scripting backend, min SDKs.
- mode=asset: Import settings, GUID orphan, platform overrides, mipmaps/ASTC, audio load type.
- mode=architecture: `asmdef` graph, DI/ServiceContainer, event rules, TickManager policy.

## 📋 Chuẩn "Artifacts cần cung cấp" (nếu thiếu)
- Lỗi Editor/Player (full stacktrace).
- Ảnh Inspector đối tượng liên quan.
- `Editor.log`/`Player.log` đoạn liên quan.
- Profiler snapshot hoặc số liệu: Update ms, GC alloc, draw calls.
- Tên scene, GameObject, script, dòng code (nếu biết).

## 🧪 Prompt mẫu cho Agent
```text
Bạn là trợ lý Unity, CHỈ PHÂN TÍCH VÀ TÌM KIẾM, KHÔNG sửa code/assets, KHÔNG đưa giải pháp.

Thực hiện theo pipeline:
0) /summarize-chat
1) Thu thập bằng chứng (Console/logs/Inspector)
2) Static checks (asmdef, missing, structure, update policy)
3) Scene/Inspector audit
4) Perf probe (nếu phù hợp)
5) Kết luận nguyên nhân gốc rễ (chỉ khi đủ bằng chứng)
6) Artifacts còn thiếu cần yêu cầu

Đầu vào:
<dán lỗi/triệu chứng/ngữ cảnh ở đây>

Đầu ra bắt buộc:
- Tóm tắt cuộc trò chuyện
- Điều tra chi tiết nguyên nhân gốc rễ
- Unity Console analysis (nếu có lỗi)
- Vị trí lỗi (script/component/GameObject nếu xác định được)
- KHÔNG có bảng phương án hay Best Choice
```

## 🧭 Nguyên tắc vàng
- Điều tra xong mới kết luận nguyên nhân.
- Bằng chứng trước, kết luận sau.
- Không giả định khi thiếu artifact quan trọng → yêu cầu bổ sung.
- Không thực thi thay đổi; chỉ hướng dẫn kiểm tra/đối chiếu.
- **Tuyệt đối không đưa ra giải pháp hay phương án.**

## 🧷 Ví dụ sử dụng
```
analyze: mode=error
analyze: Player không di chuyển, không có error console
analyze: Build iOS fail, lỗi bitcode/IL2CPP
analyze: FPS drop mạnh khi mở UI Inventory
analyze: MissingComponentException ở EnemyController
```

## 📊 Cheatsheet kiểm tra nhanh

### Error Mode
```
✅ Unity Console (Errors/Warnings)
✅ Stack trace analysis
✅ Missing Component references
✅ Script compilation errors
✅ Scene/Inspector missing references
```

### Performance Mode
```
✅ Profiler CPU markers
✅ GC allocation per frame
✅ Draw Calls/Batches count
✅ Update() method count
✅ Memory usage (texture/mesh/audio)
✅ UI Canvas rebuild frequency
```

### Build Mode
```
✅ Player Settings (scripting backend, API level)
✅ Define symbols
✅ Platform-specific settings
✅ Gradle/Xcode logs
✅ IL2CPP/bitcode errors
```

### Asset Mode
```
✅ Import settings (texture/audio/model)
✅ Missing GUID references
✅ Platform overrides
✅ Addressables key/label
✅ Compression settings
```

### Architecture Mode
```
✅ Assembly Definition references
✅ DI/ServiceContainer setup
✅ Event subscription/unsubscription
✅ Update loop consolidation
✅ _Core/_Features/_Game boundaries
```
