# Ask Command - Phân tích vấn đề Unity

## Mục đích
Khi người dùng gặp vấn đề Unity, command này sẽ:
- **TỰ ĐỘNG tóm tắt** cuộc trò chuyện trước khi phân tích
- **KHÔNG thực hiện** bất kỳ thay đổi code nào
- **Chỉ phân tích** và đưa ra giải pháp
- **Điều tra nguyên nhân** gốc rễ
- **Đề xuất phương án** khả thi với ưu/nhược điểm

## Quy trình thực hiện

### 0. 📊 TÓM TẮT CUỘC TRÒ CHUYỆN (BẮT BUỘC ĐẦU TIÊN)
- **Tự động thực hiện** `/summarize-chat` trước khi phân tích
- **Gom ý chính** và file quan trọng từ cuộc trò chuyện
- **Xác định context** và vấn đề đã được thảo luận
- **Chuẩn bị thông tin** đầy đủ cho việc điều tra
- **Tăng hiệu quả** phân tích vấn đề

### 1. 🔍 Điều tra nguyên nhân (BẮT BUỘC)
- **Sử dụng thông tin** từ bước tóm tắt cuộc trò chuyện
- **Phân tích Unity Console** để xác định lỗi
- **Kiểm tra Inspector** và component settings
- **Xác định script/component** gây ra vấn đề
- **Tìm hiểu context** và dependencies
- **KHÔNG đưa ra phương án** cho đến khi điều tra xong

### 2. 📍 Xác định vị trí vấn đề
- Script cụ thể gây lỗi
- Dòng code có vấn đề
- Component/GameObject liên quan
- **Xác nhận nguyên nhân** trước khi đề xuất fix

### 3. 💡 Đề xuất phương án (CHỈ SAU KHI ĐIỀU TRA XONG)

| Phương án | Mô tả | ✅ Ưu điểm | ⚠️ Nhược điểm | 🎯 Độ khó | ⏱️ Thời gian |
|-----------|-------|------------|---------------|-----------|-------------|
| **Phương án 1** | Sửa trực tiếp | ✅ Nhanh chóng ✅ Ít thay đổi | ⚠️ Có thể gây side effect | 🟢 Dễ | 🟢 < 1h |
| **Phương án 2** | Refactor script | ✅ Code sạch hơn ✅ Dễ maintain | ⚠️ Cần test kỹ | 🟡 Trung bình | 🟡 2-4h |
| **Phương án 3** | Thay đổi architecture | ✅ Giải quyết triệt để | ⚠️ Thay đổi lớn | 🔴 Khó | 🔴 > 1 ngày |
| **Phương án 4** | Workaround tạm thời | ✅ Giải quyết ngay | ⚠️ Không bền vững | 🟢 Dễ | 🟢 < 30 phút |

### 4. ⭐ Best Choice
- **Phương án được khuyến nghị** với lý do cụ thể
- **Rủi ro và lợi ích** chi tiết
- **Timeline thực hiện** và **dependencies**

### 5. ⚠️ Rủi ro và cách xử lý
- **Các lỗi có thể xảy ra** khi thực hiện Best Choice
- **Tác động phụ** đến các script/component khác
- **Cách sửa lỗi** cụ thể cho từng trường hợp
- **Checklist kiểm tra** sau khi thực hiện

## Unity Troubleshooting

### 1. Console Error Analysis
```csharp
// Common Unity Errors và cách fix
// NullReferenceException
if (rb != null)
{
    rb.AddForce(Vector3.up * jumpForce);
}

// MissingComponentException
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
```

### 2. Inspector Issues
- Kiểm tra missing references
- Verify component settings
- Check layer assignments
- Validate tag assignments

### 3. Performance Issues
- Sử dụng Unity Profiler
- Check for memory leaks
- Analyze draw calls
- Monitor frame rate

## Ví dụ sử dụng
```
ask: Tại sao player không di chuyển được?
ask: Lỗi NullReferenceException trong PlayerController
ask: Performance chậm khi load scene
ask: UI không hiển thị đúng trên mobile
ask: Audio không phát được
ask: Animation không chạy
ask: Physics không hoạt động
ask: Build bị lỗi
```

## Kết quả mong đợi
- 📊 **Tóm tắt cuộc trò chuyện** (tự động thực hiện)
- 🔍 **Phân tích chi tiết** nguyên nhân
- 🎮 **Unity Console analysis** (nếu có lỗi)
- 📍 **Xác định vị trí** vấn đề cụ thể
- 💡 **Bảng so sánh** các phương án
- ⭐ **Khuyến nghị** phương án tốt nhất
- ⚠️ **Cảnh báo rủi ro** và cách xử lý
- 🚫 **KHÔNG thay đổi** code hay assets

## Unity Commands Reference
Khi sử dụng `/ask` với Unity issues, có thể tham khảo các lệnh sau:

### Kiểm tra Console
```csharp
// Enable console logging
Debug.Log("Debug message");
Debug.LogWarning("Warning message");
Debug.LogError("Error message");

// Conditional logging
#if UNITY_EDITOR
Debug.Log("Editor only message");
#endif
```

### Inspector Debugging
```csharp
// Show values in Inspector
[SerializeField] private bool showDebugInfo = true;

private void OnDrawGizmos()
{
    if (showDebugInfo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
```

### Performance Debugging
```csharp
// Frame rate monitoring
private void OnGUI()
{
    GUILayout.Label($"FPS: {1.0f / Time.deltaTime:F1}");
    GUILayout.Label($"Memory: {System.GC.GetTotalMemory(false) / 1024f / 1024f:F1} MB");
}
```

## ⚠️ QUAN TRỌNG: Quy tắc điều tra trước khi đưa ra phương án

### 1. **BẮT BUỘC điều tra kỹ**:
- Phân tích Unity Console chi tiết
- Kiểm tra Inspector và component settings
- Xác định script/component gây lỗi
- **KHÔNG đưa ra phương án** cho đến khi điều tra xong

### 2. **Tránh đưa ra phương án sai**:
- **Ví dụ sai**: "Script đã đúng" → thực tế có lỗi syntax
- **Ví dụ sai**: "Component đã setup" → thực tế missing reference
- **Ví dụ sai**: "Performance OK" → thực tế có bottleneck

### 3. **Quy trình điều tra chuẩn**:
```
1. Phân tích Unity Console
2. Kiểm tra Inspector settings
3. Xác định nguyên nhân gốc rễ
4. CHỈ SAU ĐÓ mới đưa ra phương án
```

## Prompt mẫu cho AI Agent Chat
```text
Bạn là trợ lý kỹ thuật Unity. Nhiệm vụ: CHỈ PHÂN TÍCH, KHÔNG sửa code hay assets.

Bối cảnh:
- Dự án: Unity game development
- Chuẩn phản hồi: Ngắn gọn, có bảng phương án, đề xuất Best Choice

Yêu cầu thực hiện:
0) **BẮT BUỘC tóm tắt cuộc trò chuyện** trước khi phân tích:
   - Tự động thực hiện /summarize-chat
   - Gom ý chính và file quan trọng
   - Xác định context và vấn đề đã thảo luận
   - Chuẩn bị thông tin đầy đủ cho việc điều tra

1) **BẮT BUỘC điều tra kỹ** trước khi đưa ra phương án:
   - Sử dụng thông tin từ bước tóm tắt
   - Phân tích Unity Console chi tiết
   - Kiểm tra Inspector và component settings
   - Xác định script/component gây lỗi
   - **KHÔNG đưa ra phương án** cho đến khi điều tra xong

2) **Phân tích Unity Console** nếu có lỗi:
   - Kiểm tra error messages và stack traces
   - Xác định loại lỗi (NullReference, MissingComponent, etc.)
   - Phân tích nguyên nhân gốc rễ
   - Kiểm tra component references và settings

3) Xác định vị trí vấn đề (script, component, GameObject, scene).

4) **CHỈ SAU KHI ĐIỀU TRA XONG** mới đề xuất tối thiểu 3 phương án (bảng: Mô tả, Ưu/nhược, Độ khó, Thời gian).

5) Chỉ ra Best Choice + lý do, rủi ro, và checklist các bước thực hiện.

6) **CẢNH BÁO RỦI RO**: Liệt kê các lỗi có thể xảy ra khi thực hiện Best Choice:
   - Lỗi compilation khi sửa script
   - Lỗi missing references khi di chuyển assets
   - Lỗi build khi thay đổi settings
   - Lỗi runtime khi thay đổi logic
   - Cách sửa từng loại lỗi cụ thể

7) Tuyệt đối không thay đổi code/assets. Nếu cần validate, chỉ đưa lệnh kiểm tra (không tự chạy).

Đầu vào:
<dán lỗi/triệu chứng/ngữ cảnh ở đây>

Đầu ra bắt buộc:
- **Tóm tắt cuộc trò chuyện** (từ bước 0)
- **Điều tra chi tiết** nguyên nhân gốc rễ
- **Unity Console analysis** (nếu có lỗi)
- Vị trí lỗi (script/component/GameObject nếu xác định được)
- **CHỈ SAU KHI ĐIỀU TRA XONG** mới có bảng phương án so sánh
- Best Choice + checklist bước làm
- **Cảnh báo rủi ro** + cách xử lý từng loại lỗi
```
