# 🔍 CODE-ANALYZE COMMAND - HỆ THỐNG PHÂN TÍCH CẤU TRÚC CODE

## 📋 TỔNG QUAN
Phân tích cấu trúc code, luồng chạy và kiến trúc Unity (CHỈ PHÂN TÍCH - KHÔNG SỬA)

---

# 🎯 MỤC ĐÍCH VÀ CHỨC NĂNG CHÍNH
- Phân tích cấu trúc code và kiến trúc dự án.
- Truy vết luồng chạy code: chạy ở đâu, khi nào, như thế nào.
- Kiểm tra patterns, dependencies và relationships.
- **CHỈ đưa ra phân tích cấu trúc, không đưa giải pháp.**

---

# 🧩 PHẠM VI PHÂN TÍCH VÀ KIỂM TRA
- Kiến trúc: asmdef, DI, Update loop, Event usage, TickManager.
- Code flow: method calls, event chains, lifecycle hooks.
- Dependencies: references, interfaces, inheritance.
- Patterns: Singleton, Observer, Factory, State Machine.

---

# 🧠 ĐẦU VÀO VÀ CÁCH SỬ DỤNG
- Mô tả component/script cần phân tích.
- Chọn chế độ: `mode=architecture|flow|dependencies|patterns` (mặc định: auto).
- Tuỳ chọn: target script, scene, platform, Unity version.

Ví dụ:
```
code-analyze: mode=architecture
code-analyze: mode=flow script=PlayerController
code-analyze: Phân tích luồng chạy của Inventory System
```

---

# 📦 ĐẦU RA VÀ KẾT QUẢ PHÂN TÍCH
- Tóm tắt cuộc trò chuyện (bắt buộc, tự động).
- Phân tích cấu trúc code chi tiết.
- Sơ đồ luồng chạy và dependencies.
- **Phân tích patterns và kiến trúc** (không đưa giải pháp).
- Danh sách "Artifacts cần đính kèm" (code files, diagrams).
- **Không có bảng phương án hay Best Choice.**

---

# 🛠️ QUY TRÌNH PHÂN TÍCH CHUẨN

## Áp dụng cho mọi chế độ

### 🔄 BƯỚC 1: TÓM TẮT CUỘC TRÒ CHUYỆN (BẮT BUỘC)
- Tự động chạy `/summarize-chat`.
- Gom ý chính, files/PRs/edits liên quan, context platform/Unity version.

### 📊 BƯỚC 2: THU THẬP THÔNG TIN CODE
- Đọc và phân tích script files liên quan.
- Kiểm tra assembly definitions và references.
- Phân tích scene hierarchy và component relationships.
- **Đọc code để hiểu cấu trúc:**
  - Class definitions và inheritance
  - Method signatures và parameters
  - Event subscriptions và unsubscriptions
  - Interface implementations
  - Dependency injections

### 🔍 BƯỚC 3: PHÂN TÍCH KIẾN TRÚC VÀ CẤU TRÚC
- `asmdef` graph: dependencies, circular references.
- DI/ServiceContainer: registration, resolution patterns.
- Event system: publisher-subscriber relationships.
- Update loop policy: Update rải rác vs `ITickable`.
- Naming/structure theo `_Core/_Features/_Game`; biên giới phụ thuộc.

### 🎮 BƯỚC 4: PHÂN TÍCH LUỒNG CHẠY VÀ LIFECYCLE
- Method call chains và execution order.
- Event flow: trigger → handler → response.
- Lifecycle hooks: Awake, Start, Update, OnDestroy.
- State transitions và state machine patterns.
- Coroutine flows và async operations.

### ⚡ BƯỚC 5: PHÂN TÍCH PATTERNS VÀ DESIGN PATTERNS
- Singleton usage và thread safety.
- Observer pattern implementation.
- Factory pattern và object creation.
- Command pattern và undo/redo.
- MVC/MVP/MVVM architecture patterns.

### 🎯 BƯỚC 6: KẾT LUẬN VÀ XÁC ĐỊNH CẤU TRÚC
- Tóm tắt kiến trúc và patterns được sử dụng.
- Sơ đồ luồng chạy và dependencies.
- **KHÔNG đưa ra giải pháp hay phương án.**

---

# 🔎 CÁC CHẾ ĐỘ PHÂN TÍCH CHUYÊN BIỆT
- mode=architecture: `asmdef` graph, DI/ServiceContainer, event rules, TickManager policy.
- mode=flow: Method calls, event chains, lifecycle hooks, coroutine flows.
- mode=dependencies: References, interfaces, inheritance, circular dependencies.
- mode=patterns: Singleton, Observer, Factory, State Machine, MVC patterns.

---

# 📋 DANH SÁCH ARTIFACTS CẦN CUNG CẤP

## Khi thiếu thông tin
- Script files liên quan (full source code).
- Scene files và prefab structures.
- Assembly definition files.
- Interface definitions và abstract classes.
- Event system implementations.

---

# 🧪 MẪU PROMPT CHO AI AGENT
```text
Bạn là trợ lý Unity, CHỈ PHÂN TÍCH CẤU TRÚC CODE VÀ LUỒNG CHẠY, KHÔNG sửa code, KHÔNG đưa giải pháp.

Thực hiện theo pipeline:
0) /summarize-chat
1) Thu thập thông tin code (scripts, scenes, asmdef)
2) Phân tích kiến trúc (dependencies, patterns, structure)
3) Truy vết luồng chạy (method calls, events, lifecycle)
4) Phân tích patterns (Singleton, Observer, Factory, etc.)
5) Tạo sơ đồ cấu trúc và luồng chạy
6) Artifacts còn thiếu cần yêu cầu

Đầu vào:
<dán script/component/ngữ cảnh cần phân tích ở đây>

Đầu ra bắt buộc:
- Tóm tắt cuộc trò chuyện
- Phân tích cấu trúc code chi tiết
- Sơ đồ luồng chạy và dependencies
- Patterns và kiến trúc được sử dụng
- KHÔNG có bảng phương án hay Best Choice
```

---

# 🧭 NGUYÊN TẮC VÀNG VÀ QUY TẮC PHÂN TÍCH
- Phân tích code trước, kết luận sau.
- Bằng chứng code trước, kết luận sau.
- Không giả định khi thiếu source code quan trọng → yêu cầu bổ sung.
- Không thực thi thay đổi; chỉ hướng dẫn kiểm tra/đối chiếu.
- **Tuyệt đối không đưa ra giải pháp hay phương án.**

---

# 🧷 VÍ DỤ SỬ DỤNG VÀ CÁC TRƯỜNG HỢP THỰC TẾ
```
code-analyze: mode=architecture
code-analyze: mode=flow script=PlayerController
code-analyze: Phân tích luồng chạy của Inventory System
code-analyze: Kiến trúc Event System trong dự án
code-analyze: Dependencies của GameManager
```

---

# 📊 BẢNG KIỂM TRA NHANH THEO TỪNG CHẾ ĐỘ

## 🏗️ CHẾ ĐỘ ARCHITECTURE - PHÂN TÍCH KIẾN TRÚC
```
✅ Assembly Definition references
✅ DI/ServiceContainer setup
✅ Event subscription/unsubscription
✅ Update loop consolidation
✅ _Core/_Features/_Game boundaries
```

## 🔄 CHẾ ĐỘ FLOW - PHÂN TÍCH LUỒNG CHẠY
```
✅ Method call chains
✅ Event trigger → handler → response
✅ Lifecycle hooks execution order
✅ Coroutine flows và async operations
✅ State transitions
```

## 🔗 CHẾ ĐỘ DEPENDENCIES - PHÂN TÍCH PHỤ THUỘC
```
✅ Interface implementations
✅ Inheritance hierarchies
✅ Circular dependencies
✅ Reference chains
✅ Injection patterns
```

## 🎨 CHẾ ĐỘ PATTERNS - PHÂN TÍCH DESIGN PATTERNS
```
✅ Singleton usage và thread safety
✅ Observer pattern implementation
✅ Factory pattern và object creation
✅ Command pattern và undo/redo
✅ MVC/MVP/MVVM architecture
```
