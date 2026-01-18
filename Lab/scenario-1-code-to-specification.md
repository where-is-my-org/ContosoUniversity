# 場景一：舊程式碼→規格文件

---

## 目標 / Objective

從現有的舊程式碼中分析需求，並將其轉換為結構化的規格文件。此過程對於維護遺留系統、重構專案或了解未文件化的功能至關重要。

---

## 流程概覽 / Process Overview

```
舊程式碼 (Legacy Code)
    ↓
初步分析 (Initial Analysis)
    ↓
功能識別 (Feature Identification)
    ↓
需求提取 (Requirement Extraction)
    ↓
規格文件撰寫 (Specification Writing)
    ↓
驗證與審查 (Validation & Review)
```

---

## 步驟詳解 / Detailed Steps

### 步驟 1：初步分析 (Initial Analysis)

#### 目標 / Objective
了解程式碼的整體結構、依賴關係和主要組件。

#### 重點 / Key Points

1. **識別入口點 (Identify Entry Points)**
   - 找出主要的類別、方法和事件處理器
   - 確認程式執行的起始位置

2. **分析命名空間和類別結構 (Analyze Namespace and Class Structure)**
   - 檢視類別之間的繼承關係
   - 識別使用的框架和庫

3. **記錄外部依賴 (Document External Dependencies)**
   - NuGet 套件
   - 系統 API
   - 第三方服務

#### 注意事項 / Precautions

⚠️ **不要假設程式碼的意圖** - 只記錄觀察到的行為
⚠️ **Don't assume code intent** - Only document observed behavior

⚠️ **注意未使用的程式碼** - 可能是廢棄的功能或未完成的開發
⚠️ **Watch for unused code** - May be deprecated features or incomplete development

⚠️ **記錄版本資訊** - 框架版本、.NET 版本等
⚠️ **Document version information** - Framework versions, .NET versions, etc.

#### 驗證方法 / Validation Methods

✓ 使用 IDE 的類別圖功能視覺化結構

✓ 執行靜態程式碼分析工具（如 ReSharper、SonarQube）

✓ 建立依賴關係圖

---

### 步驟 2：功能識別 (Feature Identification)

#### 目標 / Objective
識別程式碼實現的所有功能和使用者互動。

#### 重點 / Key Points

1. **使用者介面元素 (UI Elements)**
   - 按鈕、文字框、選單等控制項
   - 視窗、對話框

2. **事件處理 (Event Handlers)**
   - 使用者操作（點擊、拖曳、鍵盤輸入）
   - 系統事件（計時器、載入、關閉）

3. **商業邏輯 (Business Logic)**
   - 資料驗證
   - 計算和轉換
   - 狀態管理

#### 注意事項 / Precautions

⚠️ **區分核心功能和輔助功能** - 明確主要功能的優先級

⚠️ **注意隱藏功能** - 某些功能可能只在特定條件下觸發

⚠️ **記錄錯誤處理** - 異常處理和容錯機制

#### 驗證方法 / Validation Methods

✓ 建立功能清單，與利害關係人確認

✓ 執行程式並測試每個識別的功能

✓ 使用偵錯工具追蹤執行流程

---

### 步驟 3：需求提取 (Requirement Extraction)

#### 目標 / Objective
從程式碼的實作細節中提取出高層次的需求。

#### 重點 / Key Points

1. **功能性需求 (Functional Requirements)**
   - 系統必須做什麼
   - 輸入和輸出
   - 處理流程

2. **非功能性需求 (Non-Functional Requirements)**
   - 效能（計時器間隔、動畫速度）
   - 可用性（使用者體驗）
   - 可靠性（錯誤處理）

3. **約束條件 (Constraints)**
   - 技術限制（.NET 版本、Windows 版本）
   - 資料限制（大小、格式）

#### 注意事項 / Precautions

⚠️ **從「如何」轉換為「什麼」** - 關注需求而非實作

⚠️ **避免過度技術化** - 使用業務語言而非技術術語

⚠️ **識別隱含的需求** - 程式碼中可能包含未明說的假設

#### 驗證方法 / Validation Methods

✓ 需求追溯性矩陣：需求 ↔ 程式碼

✓ 與原始開發者或使用者訪談

✓ 檢視歷史變更記錄和問題追蹤

---

### 步驟 4：規格文件撰寫 (Specification Writing)

#### 目標 / Objective
創建結構化、可維護的規格文件。

#### 重點 / Key Points

1. **文件結構 (Document Structure)**
   ```
   1. 概述 (Overview)
   2. 系統架構 (System Architecture)
   3. 功能規格 (Functional Specifications)
      3.1 功能 A (Feature A)
      3.2 功能 B (Feature B)
   4. 資料模型 (Data Models)
   5. 介面規格 (Interface Specifications)
   6. 非功能性需求 (Non-Functional Requirements)
   7. 約束與假設 (Constraints & Assumptions)
   ```

2. **每個功能應包含 (Each Feature Should Include)**
   - 功能描述
   - 使用者故事或使用案例
   - 接受標準
   - 前置條件和後置條件
   - 異常情況

3. **視覺化元素 (Visual Elements)**
   - 流程圖
   - 序列圖
   - UI 模型或截圖

#### 注意事項 / Precautions

⚠️ **保持一致性** - 使用統一的術語和格式

⚠️ **可測試性** - 每個需求都應該可以驗證

⚠️ **版本控制** - 追蹤文件變更

#### 驗證方法 / Validation Methods

✓ 同行審查（Peer Review）

✓ 利害關係人確認

✓ 文件完整性檢查清單
---

### 步驟 5：驗證與審查 (Validation & Review)

#### 目標 / Objective
確保規格文件準確反映程式碼的行為和需求。

#### 重點 / Key Points

1. **完整性檢查 (Completeness Check)**
   - 所有功能都有記錄
   - 所有輸入/輸出都有說明
   - 所有異常情況都有涵蓋

2. **一致性檢查 (Consistency Check)**
   - 規格與程式碼行為一致
   - 術語使用一致

3. **可追溯性 (Traceability)**
   - 建立需求到程式碼的對應
   - 建立測試案例到需求的對應

#### 注意事項 / Precautions

⚠️ **多次驗證** - 第一次審查可能遺漏問題

⚠️ **實際測試** - 根據規格執行程式以驗證

⚠️ **記錄差異** - 如果發現程式碼與預期不符，記錄為已知問題

#### 驗證方法 / Validation Methods

✓ 建立測試案例並執行

✓ 程式碼審查會議

✓ 使用者驗收測試（UAT）

---

## 實例演示：AnimatedLogoForm / Example: AnimatedLogoForm

### 程式碼分析 / Code Analysis

讓我們以 `AnimatedLogoForm.cs` 為例，示範如何應用上述流程。

### 步驟 1：初步分析

**觀察結果 / Observations:**

```csharp
namespace dotnet_winforms_examples;
public partial class AnimatedLogoForm : Form
```

- 類別繼承自 `Form`（Windows Forms 基類）
- Class inherits from `Form` (Windows Forms base class)
- 屬於 `dotnet_winforms_examples` 命名空間
- Belongs to `dotnet_winforms_examples` namespace

**依賴項 / Dependencies:**
- System.Windows.Forms
- System.Drawing.Imaging
- System.Diagnostics

### 步驟 2：功能識別

**已識別功能 / Identified Features:**

1. **動畫播放 (Animation Playback)**
   - 使用 GIF 或多幀圖像
   - 自動循環播放
   - Uses GIF or multi-frame images
   - Automatic looping

2. **自動關閉 (Auto-Close)**
   - 10 秒後自動關閉視窗
   - Auto-closes window after 10 seconds

3. **視窗移動 (Window Movement)**
   - 使用者可拖曳視窗
   - User can drag window
   - 僅在正常視窗狀態下可用
   - Only available in normal window state

### 步驟 3：需求提取

#### 功能性需求 / Functional Requirements

**FR-1: 動畫顯示**
- **描述 / Description:** 系統應在啟動時顯示動畫標誌
- **接受標準 / Acceptance Criteria:**
  - 動畫應循環播放
  - 使用圖像的原生幀率

**FR-2: 自動關閉**
- **描述 / Description:** 視窗應在顯示 10 秒後自動關閉
- **接受標準 / Acceptance Criteria:**
  - 精確計時：10.0 秒
  - 關閉應優雅（不顯示錯誤）

**FR-3: 視窗拖曳**
- **描述 / Description:** 使用者可以拖曳視窗到螢幕上的任何位置
- **接受標準 / Acceptance Criteria:**
  - 僅在正常視窗狀態下可用
  - 滑鼠按下時開始拖曳
  - 滑鼠釋放時停止拖曳

#### 非功能性需求 / Non-Functional Requirements

**NFR-1: 效能**
- 動畫更新間隔：1 毫秒（高頻率更新）

**NFR-2: 可用性**
- 視窗應可在整個桌面範圍內移動

### 步驟 4：規格文件

#### 詳細規格：動畫標誌視窗 / Detailed Specification: Animated Logo Window

**1. 概述 / Overview**

動畫標誌視窗是一個啟動畫面，顯示動畫化的公司標誌或品牌圖像。該視窗在應用程式啟動時顯示，並在固定時間後自動關閉。

**2. 功能描述 / Feature Description**

**2.1 動畫播放功能 / Animation Playback Feature**

| 項目 / Item | 內容 / Content |
|------------|----------------|
| 功能 ID / Feature ID | ANI-001 |
| 優先級 / Priority | High |
| 輸入 / Input | 多幀圖像檔案（GIF 或類似格式） / Multi-frame image file (GIF or similar) |
| 輸出 / Output | 螢幕上顯示的動畫 / Animated display on screen |
| 前置條件 / Pre-condition | 圖像檔案已載入至 pictureBox1 / Image file loaded in pictureBox1 |
| 後置條件 / Post-condition | 動畫持續播放直到視窗關閉 / Animation continues until window closes |

**處理流程 / Processing Flow:**
1. 系統讀取圖像的幀維度
2. 取得總幀數
3. 啟動計時器（間隔 1ms）
4. 每次計時器觸發時：
   - 增加當前幀索引
   - 如果超過總幀數，重置為 0
   - 顯示當前幀

**2.2 自動關閉功能 / Auto-Close Feature**

| 項目 / Item | 內容 / Content |
|------------|----------------|
| 功能 ID / Feature ID | ANI-002 |
| 優先級 / Priority | High |
| 輸入 / Input | 時間（秒） / Time (seconds) |
| 輸出 / Output | 視窗關閉 / Window closes |
| 前置條件 / Pre-condition | 視窗已開啟 / Window is open |
| 後置條件 / Post-condition | 視窗已關閉，資源已釋放 / Window closed, resources released |

**處理流程 / Processing Flow:**
1. 視窗開啟時啟動計時器
2. 檢查經過時間是否超過 10 秒
3. 如果是，停用計時器並關閉視窗

**2.3 視窗拖曳功能 / Window Drag Feature**

| 項目 / Item | 內容 / Content |
|------------|----------------|
| 功能 ID / Feature ID | ANI-003 |
| 優先級 / Priority | Medium |
| 輸入 / Input | 滑鼠事件（按下、移動、釋放） / Mouse events (down, move, up) |
| 輸出 / Output | 視窗位置改變 / Window position changes |
| 前置條件 / Pre-condition | 視窗狀態為 Normal / Window state is Normal |
| 後置條件 / Post-condition | 視窗位於新位置 / Window at new position |

**處理流程 / Processing Flow:**
1. 滑鼠按下：記錄起始位置，設定拖曳標誌
2. 滑鼠移動：計算偏移量，更新視窗位置
3. 滑鼠釋放：清除拖曳標誌

**3. 資料結構 / Data Structures**

```csharp
// 狀態變數 / State Variables
private FrameDimension dimension;    // 圖像幀維度
private int frameCount;               // 總幀數
private int currentFrame;             // 當前幀索引 (-1 初始值)
private Stopwatch stopWatch;          // 計時器

// 拖曳狀態 / Drag State
private bool Moving;                  // 是否正在拖曳
private (int X, int Y) MovingMouseOrigin;  // 拖曳起始位置
```

**4. 約束與限制 / Constraints & Limitations**

- 必須使用支援多幀的圖像格式（如 GIF）
- 視窗僅在正常狀態下可拖曳（最大化或最小化時不可拖曳）
- 計時器間隔固定為 1ms，不可配置
- Must use multi-frame image format (e.g., GIF)
- Window draggable only in normal state (not when maximized or minimized)
- Timer interval fixed at 1ms, not configurable

**5. 測試案例 / Test Cases**

| 測試 ID / Test ID | 測試項目 / Test Item | 預期結果 / Expected Result |
|-------------------|---------------------|---------------------------|
| TC-ANI-001 | 動畫播放 / Animation playback | 動畫循環播放，無閃爍 / Animation loops smoothly, no flicker |
| TC-ANI-002 | 10秒自動關閉 / 10-second auto-close | 視窗在 10±0.1 秒後關閉 / Window closes after 10±0.1 seconds |
| TC-ANI-003 | 拖曳功能 / Drag functionality | 視窗跟隨滑鼠移動 / Window follows mouse movement |
| TC-ANI-004 | 拖曳狀態限制 / Drag state restriction | 最大化時無法拖曳 / Cannot drag when maximized |
| TC-ANI-005 | 資源釋放 / Resource release | 關閉後無記憶體洩漏 / No memory leak after close |

### 步驟 5：驗證

**驗證檢查清單 / Validation Checklist:**

- [x] 所有公開方法都有文件說明 / All public methods documented
- [x] 所有事件處理器都有對應的需求 / All event handlers mapped to requirements
- [x] 測試案例覆蓋所有功能 / Test cases cover all features
- [x] 非功能性需求已識別並記錄 / Non-functional requirements identified and documented
- [x] 約束條件明確列出 / Constraints clearly listed

---

## 工具建議 / Recommended Tools

### 程式碼分析工具 / Code Analysis Tools
- **Visual Studio IntelliCode** - AI 輔助程式碼理解
- **ReSharper** - 程式碼導航和分析
- **SonarQube** - 程式碼品質和安全性分析
- **NDepend** - .NET 程式碼分析和架構檢視

### 文件工具 / Documentation Tools
- **Markdown** - 輕量級文件格式
- **PlantUML** - 圖表即程式碼（UML 圖）
- **draw.io / diagrams.net** - 視覺化圖表工具
- **Confluence / Notion** - 協作文件平台

### 追蹤工具 / Traceability Tools
- **Azure DevOps** - 需求管理和追蹤
- **Jira** - 問題追蹤和專案管理
- **GitHub Issues** - 輕量級問題追蹤

---

## 最佳實踐 / Best Practices

### 1. 迭代式方法 / Iterative Approach

不要試圖一次完成所有分析。使用迭代式方法：

1. 第一次迭代：識別主要功能（80% 的功能）
2. 第二次迭代：補充細節和邊緣案例
3. 第三次迭代：驗證和精煉

### 2. 協作式分析 / Collaborative Analysis

- 與團隊成員一起審查程式碼
- 諮詢原始開發者（如果可能）
- 與使用者討論實際使用情況


### 3. 保持文件更新 / Keep Documentation Current

- 將規格文件納入版本控制
- 程式碼變更時同步更新文件
- 定期審查和更新過時的資訊

### 4. 使用範本 / Use Templates

建立標準化的規格文件範本，包括：

- 需求範本（功能性、非功能性）
- 測試案例範本
- 變更記錄範本

---

## 常見陷阱與解決方案 / Common Pitfalls & Solutions

### 陷阱 1：過度技術化 / Pitfall 1: Over-Technicalization

**問題 / Problem:** 規格文件充滿技術術語，非技術人員無法理解。

**解決方案 / Solution:** 
- 使用業務語言描述需求
- 提供技術附錄供開發者參考
- 使用圖表和視覺化元素

### 陷阱 2：忽略隱含需求 / Pitfall 2: Ignoring Implicit Requirements

**問題 / Problem:** 未記錄程式碼中隱含的假設和約束。

**解決方案 / Solution:**
- 詢問「為什麼」：為什麼要這樣實作？
- 測試邊緣案例
- 查看歷史變更記錄

### 陷阱 3：文件與程式碼不同步 / Pitfall 3: Documentation Out of Sync

**問題 / Problem:** 規格文件變成歷史文件，與實際程式碼不符。

**解決方案 / Solution:**
- 將文件納入 CI/CD 流程
- 程式碼審查時同時審查文件
- 使用自動化工具檢查一致性

---

## 總結 / Summary

從舊程式碼轉換為規格文件是一個系統性的過程，需要：


1. **仔細分析** - 理解程式碼的結構和行為
2. **功能識別** - 找出所有實作的功能
3. **需求提取** - 從實作中抽象出需求
4. **結構化撰寫** - 創建清晰、可維護的文件
5. **持續驗證** - 確保文件準確性

透過遵循本指南的步驟和最佳實踐，您可以有效地將遺留程式碼轉換為高品質的規格文件，為系統的維護、重構和演進奠定堅實的基礎。

---

## 參考資源 / References

- IEEE 830-1998: Recommended Practice for Software Requirements Specifications
- ISO/IEC/IEEE 29148-2018: Systems and software engineering — Life cycle processes — Requirements engineering
- "Working Effectively with Legacy Code" by Michael Feathers
- "Software Requirements" by Karl Wiegers and Joy Beatty

---

**版本 / Version:** 1.0  
**最後更新 / Last Updated:** 2024  
**維護者 / Maintainer:** Documentation Team