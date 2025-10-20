# Unity Debug System Examples

## Tổng quan

Thư mục này chứa các ví dụ sử dụng Unity Debug System với Interface pattern, tag support và define symbols.

## Cấu trúc Files

### Examples
- **Example_PlayerController.cs** - Ví dụ sử dụng IDebugLogger với DI pattern
- **PerformanceDebugger.cs** - Ví dụ advanced debug cho performance metrics

## Cách sử dụng

### 1. Copy vào Unity Project
Copy các example files này vào `Assets/_Core/Debug/Examples/` trong Unity project của bạn.

### 2. Setup Define Symbols
Trong Player Settings > Scripting Define Symbols, thêm:
- Development: `ENABLE_DEBUG_LOGS`
- Production: Remove debug symbols

### 3. Service Registration
Đăng ký IDebugLogger trong CompositionRoot:
```csharp
ServiceContainer.Register<IDebugLogger>(DebugLogger.Instance);
```

### 4. Sử dụng trong Scripts
```csharp
private const string TAG = "[YourScriptName]";
private IDebugLogger logger;

// Inject qua DI hoặc sử dụng static access
logger.Log(TAG, "Your debug message");
```

## Tag Convention

- Single class: `[PlayerController]`
- With module: `[Core.GameManager]`
- Always use const: `private const string TAG = "[ClassName]";`

## Performance Notes

- Tất cả debug code được wrap trong `#if ENABLE_DEBUG_LOGS`
- Production builds sẽ tự động loại bỏ debug code
- Sử dụng conditional logging cho expensive operations

## Testing

Sử dụng MockDebugLogger cho unit tests để verify logging behavior.
