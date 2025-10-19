# 🔍 ANALYZE COMMAND - HỆ THỐNG PHÂN TÍCH UNITY

## 📋 TỔNG QUAN
Phân tích và tìm kiếm vấn đề Unity (CHỈ PHÂN TÍCH - KHÔNG SỬA)

---

# 🎯 MỤC ĐÍCH VÀ CHỨC NĂNG CHÍNH
- Tự động khảo sát bối cảnh dự án và cuộc trò chuyện.
- Thu thập bằng chứng (console, logs, cấu hình, scene/inspector).
- Thực hiện chuỗi kiểm tra sâu và có hệ thống theo chế độ.
- **CHỈ đưa ra phân tích, tìm kiếm nguyên nhân, không đưa giải pháp.**

---

# 🧩 PHẠM VI PHÂN TÍCH VÀ KIỂM TRA
- Lỗi runtime/Editor, build fail, UI/Audio/Animation/Physics issues.
- Performance: CPU, GC, Draw Calls, Memory, UI Canvas.
- Kiến trúc: asmdef, DI, Update loop, Event usage, TickManager.
- Assets: import settings, missing GUID, Addressables (nếu có).

---

# 🧠 ĐẦU VÀO VÀ CÁCH SỬ DỤNG
- Mô tả triệu chứng/ngữ cảnh, log/error (nếu có).
- Chọn chế độ: `mode=error|performance|build|asset|architecture` (mặc định: auto).
- Tuỳ chọn: target scene, platform (Standalone/Android/iOS), Unity version.

Ví dụ:
```
analyze: mode=error
analyze: mode=performance platform=Android
analyze: Build Android fail với lỗi Gradle
```

---

# 📦 ĐẦU RA VÀ KẾT QUẢ PHÂN TÍCH
- Tóm tắt cuộc trò chuyện (bắt buộc, tự động).
- Điều tra chi tiết + Unity Console analysis (nếu có).
- Vị trí vấn đề: script/component/GameObject/dòng code/scene.
- **Phân tích nguyên nhân gốc rễ** (không đưa giải pháp).
- Danh sách "Artifacts cần đính kèm" (log, screenshot, profiler capture).
- **Không có bảng phương án hay Best Choice.**

---

# 🛠️ QUY TRÌNH PHÂN TÍCH CHUẨN

## Áp dụng cho mọi chế độ

### 🔄 BƯỚC 1: TÓM TẮT CUỘC TRÒ CHUYỆN (BẮT BUỘC)
- Tự động chạy `/summarize-chat`.
- Gom ý chính, files/PRs/edits liên quan, context platform/Unity version.

### 📊 BƯỚC 2: THU THẬP BẰNG CHỨNG VÀ DỮ LIỆU
- Console errors/warnings (stacktrace, grouping theo root cause).
- Editor logs / Player logs:
  - Windows: `%LOCALAPPDATA%\Unity\Editor\Editor.log`
  - Windows Player: `%USERPROFILE%\AppData\LocalLow\<Company>\<Product>\Player.log`
- Ảnh/chụp Inspector nếu có.
- **Đọc scene để lấy cấu trúc hierarchy:**
  - GameObject hierarchy và parent-child relationships
  - Component references và missing components
  - Active/Inactive state của objects
  - Transform positions, rotations, scales
  - Tag và Layer assignments
- `asmdef` graph, define symbols, Quality/Physics/Graphics settings.
- Nếu có Profiler: snapshot markers, GC alloc, Update count.

### 🔍 BƯỚC 3: KIỂM TRA TĨNH VÀ CẤU TRÚC DỰ ÁN
- Null/Missing Scripts trong scenes/prefabs.
- `asmdef`: tham chiếu chéo sai, vòng lặp, missing reference.
- `Resources/Addressables` (nếu dùng): key, label, load path.
- Naming/structure theo `_Core/_Features/_Game`; biên giới phụ thuộc.
- Update loop policy: Update rải rác vs `ITickable`.

### 🎮 BƯỚC 4: KIỂM TRA SCENE VÀ INSPECTOR
- Missing references, inactive chains, Tag/Layer/SortingLayer sai.
- Physics: collision matrix, rigidbody/constraints.
- UI: Canvas phân tần suất, raycast targets, atlas.
- Audio: import/loadType, 3D settings.

### ⚡ BƯỚC 5: PHÂN TÍCH PERFORMANCE (KHI CẦN THIẾT)
- CPU: top markers, thời gian Update tổng, số lượng Update phân tán.
- GC: alloc per frame, boxing/strings/LINQ.
- Draw Calls/Batches, SetPass, Overdraw.
- Memory: mesh/texture/audio budgets theo chuẩn.

### 🎯 BƯỚC 6: KẾT LUẬN VÀ XÁC ĐỊNH NGUYÊN NHÂN GỐC RỄ
- Ràng buộc nguyên nhân với bằng chứng, tái hiện điều kiện (scene, steps).
- **KHÔNG đưa ra giải pháp hay phương án.**

---

# 🔎 CÁC CHẾ ĐỘ PHÂN TÍCH CHUYÊN BIỆT
- mode=error: Ưu tiên Console/stacktrace, MissingComponent/NullRef, Script/line pinpoint.
- mode=performance: Ưu tiên Profiler/GC, Update consolidation, batching, texture formats.
- mode=build: PlayerSettings, Gradle/Xcode logs, define symbols, scripting backend, min SDKs.
- mode=asset: Import settings, GUID orphan, platform overrides, mipmaps/ASTC, audio load type.
- mode=architecture: `asmdef` graph, DI/ServiceContainer, event rules, TickManager policy.

---

# 📋 DANH SÁCH ARTIFACTS CẦN CUNG CẤP

## Khi thiếu thông tin
- Lỗi Editor/Player (full stacktrace).
- Ảnh Inspector đối tượng liên quan.
- `Editor.log`/`Player.log` đoạn liên quan.
- Profiler snapshot hoặc số liệu: Update ms, GC alloc, draw calls.
- Tên scene, GameObject, script, dòng code (nếu biết).

---

# 🧪 MẪU PROMPT CHO AI AGENT
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

---

# 🧭 NGUYÊN TẮC VÀNG VÀ QUY TẮC PHÂN TÍCH
- Điều tra xong mới kết luận nguyên nhân.
- Bằng chứng trước, kết luận sau.
- Không giả định khi thiếu artifact quan trọng → yêu cầu bổ sung.
- Không thực thi thay đổi; chỉ hướng dẫn kiểm tra/đối chiếu.
- **Tuyệt đối không đưa ra giải pháp hay phương án.**

---

# 🧷 VÍ DỤ SỬ DỤNG VÀ CÁC TRƯỜNG HỢP THỰC TẾ
```
analyze: mode=error
analyze: Player không di chuyển, không có error console
analyze: Build iOS fail, lỗi bitcode/IL2CPP
analyze: FPS drop mạnh khi mở UI Inventory
analyze: MissingComponentException ở EnemyController
```

---

# 📊 BẢNG KIỂM TRA NHANH THEO TỪNG CHẾ ĐỘ

## 🔴 CHẾ ĐỘ ERROR - KIỂM TRA LỖI VÀ EXCEPTION
```
✅ Unity Console (Errors/Warnings)
✅ Stack trace analysis
✅ Missing Component references
✅ Script compilation errors
✅ Scene/Inspector missing references
```

## ⚡ CHẾ ĐỘ PERFORMANCE - PHÂN TÍCH HIỆU SUẤT
```
✅ Profiler CPU markers
✅ GC allocation per frame
✅ Draw Calls/Batches count
✅ Update() method count
✅ Memory usage (texture/mesh/audio)
✅ UI Canvas rebuild frequency
```

## 🔨 CHẾ ĐỘ BUILD - KIỂM TRA QUÁ TRÌNH BUILD
```
✅ Player Settings (scripting backend, API level)
✅ Define symbols
✅ Platform-specific settings
✅ Gradle/Xcode logs
✅ IL2CPP/bitcode errors
```

## 📁 CHẾ ĐỘ ASSET - KIỂM TRA TÀI NGUYÊN
```
✅ Import settings (texture/audio/model)
✅ Missing GUID references
✅ Platform overrides
✅ Addressables key/label
✅ Compression settings
```

## 🏗️ CHẾ ĐỘ ARCHITECTURE - PHÂN TÍCH KIẾN TRÚC
```
✅ Assembly Definition references
✅ DI/ServiceContainer setup
✅ Event subscription/unsubscription
✅ Update loop consolidation
✅ _Core/_Features/_Game boundaries
```