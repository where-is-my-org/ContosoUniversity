# 場景二：規格文件→新規格文件（.NET Framework 4.8 → .NET 8 / ASP.NET Core + React）

---

## 目標 / Objective

分析現有程式碼與程式碼內的舊有規格文件，產出一份符合移轉需求的新規格文件（.NET 8 / ASP.NET Core Web API + React），**不包含任何程式碼實作、IaC/部署腳本或 CI/CD 實作內容**。規格內容聚焦於：架構拆分、相容性風險、需求與替代方案、介面/契約定義、套件與安全性升級目標，以及可部署至 Azure App Service（Linux）與 Azure SQL Database 的部署需求與驗證準則。

---

## 流程概覽 / Process Overview

```
現有規格文件 (Existing Specification)
    ↓
架構分析與相容性評估 (Architecture & Compatibility Assessment)
    ↓
後端遷移需求規格 (Backend Migration Requirements Specification)
    ↓
前端需求規格 (Frontend Requirements Specification)
    ↓
套件與安全性升級目標 (Package & Security Upgrade Targets)
    ↓
服務設計與相依性注入需求 (Services & Dependency Injection Requirements)
    ↓
Azure 部署需求規格 (Azure Deployment Requirements)
    ↓
本地測試與驗證準則 (Local Testing & Validation Criteria)
```

---

## 步驟詳解 / Detailed Steps

### 步驟 1：架構分析與相容性評估 (Architecture & Compatibility Assessment)

#### 目標 / Objective

盤點現有 .NET Framework 4.8 架構與相依套件，識別 .NET 8 / ASP.NET Core 不相容項目與替代方案，並確認前後端分離的可行性。

#### 重點 / Key Points

1. **相容性評估 (Compatibility Assessment)**
   - 檢查 System.Web 依賴並移除
   - 檢查全域組態（Web.config）並規劃移至 appsettings.json

2. **前後端拆分評估 (Separation Assessment)**
   - 盤點 UI 與業務邏輯耦合點
   - API 化可行性與影響範圍

3. **風險評估 (Risk Assessment)**
   - 系統相依套件的版本風險
   - 底層通訊或訊息佇列的替代風險

#### 驗證方法 / Validation Methods

✓ 完成相容性盤點清單

✓ 定義遷移風險與替代方案

---

### 步驟 2：後端遷移需求規格 (Backend Migration Requirements Specification)


#### 目標 / Objective

定義後端遷移至 ASP.NET Core Web API 的「需求規格與設計約束」，包含設定移轉、啟動管線、與 System.Web 替代策略；本步驟不實作任何程式碼。

#### 重點 / Key Points

1. **組態移轉 (Configuration Migration)**
   - 定義需自 Web.config 移轉至 appsettings.json 的設定清單（連線字串、appSettings 等）
   - 定義環境別設定策略（Development/Staging/Production）與機密資訊管理方式（例如使用環境變數或 Key Vault，僅列需求）

2. **ASP.NET Core 啟動流程 (Startup Pipeline)**
   - 定義採用 Program.cs + Minimal Hosting 的啟動模型與必要中介軟體需求
   - 定義路由、錯誤處理、記錄、健康檢查等端點/中介軟體的需求與驗收標準

3. **相容性修正 (Compatibility Fixes)**
   - 列出 System.Web 依賴點盤點方法與替代策略（僅描述替代方案，不提供實作）
   - 定義 HttpContext、Session、Caching 的替代介面與使用準則

#### 注意事項 / Precautions

⚠️ **Web.config 移除** - 所有設定需集中到 appsettings.json

---

### 步驟 3：前端需求規格 (Frontend Requirements Specification)

#### 目標 / Objective

定義 React 前端的需求與 API 整合契約（路由、頁面、狀態管理與錯誤處理策略等），本步驟不產出任何前端程式碼。

#### 重點 / Key Points

1. **React 應用結構 (React App Structure)**
   - 定義前端專案腳手架選型（Vite/CRA）之需求考量與決策準則
   - 定義 pages/components/services 的目錄分層規範（僅規格，不建立專案）

2. **API 整合 (API Integration)**
   - 定義統一 API Client 的需求（fetch/axios 皆可）與呼叫約定
   - 定義錯誤處理、重試、超時與 API 版本化策略

3. **認證流程 (Authentication Flow)**
   - 定義 JWT 儲存、更新與失效處理策略（僅需求）
   - 定義登入狀態管理方案（Context/Redux）的選型準則與驗收條件

#### 注意事項 / Precautions

⚠️ **CORS 設定** - 後端需配置 CORS 允許前端來源

---

### 步驟 4：套件與安全性升級目標 (Package & Security Upgrade Targets)

#### 目標 / Objective

定義 .NET 8 相容套件升級的目標版本、相依關係與安全性基準（CVE 風險與替代方案），本步驟不進行實際套件升級或程式碼修改。

#### 重點 / Key Points

1. **Entity Framework Core**
   - 定義 EF Core 升級目標（例如 8.x）與不相容風險清單
   - 定義 DbContext 設定與遷移策略的需求（僅規格）

2. **SQL Client**
   - 定義 Microsoft.Data.SqlClient 升級目標版本（例如 6.x）與安全性理由（CVE 修補）

3. **Identity / Auth**
   - 定義 Microsoft.Identity.* 相關套件升級範圍與版本策略
   - 定義認證流程與相依設定的需求與驗收條件（僅規格）

4. **Microsoft.Extensions.* 對齊**
   - 定義 Logging/Configuration/Caching/Options 等套件與 .NET 8 的對齊策略

#### 驗證方法 / Validation Methods

✓ 套件目標版本清單與漏洞掃描/例外項目紀錄（僅報告）

✓ 產出相容性風險與回退/替代方案

---

### 步驟 5：服務設計與相依性注入需求 (Services & Dependency Injection Requirements)

#### 目標 / Objective

定義後端服務層的依賴注入需求與介面邊界，並就 NotificationService 的 MSMQ 不相容議題提出需求面替代方案；不進行任何程式碼重構。

#### 重點 / Key Points

1. **全面 DI (Full DI Adoption)**
   - 盤點現有 Service Locator/靜態服務使用點並定義替代目標（僅盤點與規格）
   - 定義介面化與注入生命週期（Scoped/Singleton/Transient）需求

2. **NotificationService 調整**
   - 明確標示 MSMQ 於 .NET 8 不可用的影響範圍與功能降級策略
   - 定義替代方案的需求（例如 Azure Service Bus 或其他佇列），僅保留抽象介面層級的規格

#### 注意事項 / Precautions

⚠️ **暫停 MSMQ** - 需明確標示不可用功能
---

### 步驟 6：Azure 部署需求規格 (Azure Deployment Requirements)

#### 目標 / Objective

定義前後端部署至 Azure App Service（Linux）與 Azure SQL Database 的需求、設定項與驗收準則；不提供 ARM/Bicep/Terraform、PowerShell 或 GitHub Actions 的實作內容。

#### 重點 / Key Points

1. **後端 App Service（Linux）部署**
   - 定義 .NET 8 Runtime 與必要應用程式設定（App Settings/Connection Strings）需求
   - 定義健康檢查端點需求與監控/記錄的驗收標準（僅規格）

2. **前端 App Service 或 Static Web**
   - 定義 React 靜態檔部署需求與快取策略（僅規格）
   - 定義 API Base URL 的環境變數/設定注入需求

3. **ARM Template 基礎資源**
   - 定義基礎資源需求：App Service Plan、Web App、Azure SQL
   - 定義可選監控需求：Application Insights、Log Analytics

4. **PowerShell 一鍵部署腳本**
   - 僅列出一鍵部署流程的需求與必要輸入/輸出（不提供腳本）

5. **GitHub Actions CI/CD（Publish Profile）**
   - 僅定義 CI/CD 的需求（流程拆分、環境變數、憑證管理），不提供工作流程檔

6. **生產環境安全**
   - 定義強制 HTTPS、最小權限、與機密管理（Key Vault）需求

#### 驗證方法 / Validation Methods

✓ 部署需求檢核清單（設定項、網路、TLS、監控）完成

✓ 連線需求與權限模型定義完成（僅規格）
---

### 步驟 7：本地測試與驗證準則 (Local Testing & Validation Criteria)

#### 目標 / Objective

定義本地開發/測試的需求與驗證準則（含 Codespace），不提供 devcontainer 或任何自動化設定檔實作。

#### 重點 / Key Points

1. **Development 模式**
   - 使用 ASPNETCORE_ENVIRONMENT=Development
   - 啟用詳細錯誤與本機設定

2. **本地資料庫**
   - 定義本地 DB 選項（LocalDB 或 Docker SQL Server）的需求與限制
   - 定義開發用連線字串格式與敏感資訊處理原則

3. **React 本地開發**
   - 使用 Vite/CRA Dev Server
   - 配置 proxy 或 API Base URL

4. **Codespace 支援**
   - 定義 Codespace 所需前置條件與啟動驗收準則（不提供 devcontainer 實作）

#### 驗證方法 / Validation Methods

✓ Codespace 啟動與 API 健康檢查

---

## 遷移檢查清單 / Migration Checklist

### 階段 1：分析 / Analysis

- [ ] 完成相容性與相依套件盤點
- [ ] 完成前後端拆分設計
- [ ] 風險與替代方案清單

### 階段 2：規格 / Specification

- [ ] 後端需求規格完成（設定、管線、相容性替代策略）
- [ ] 前端需求規格完成（結構、API 整合、認證、錯誤處理）
- [ ] 套件/安全升級目標完成（版本、CVE、相依風險）
- [ ] DI/服務設計需求完成（介面邊界、生命週期、替代方案）
- [ ] NotificationService 功能降級與替代方案需求完成

### 階段 3：部署需求 / Deployment Requirements

- [ ] Azure 資源需求清單與設定項定義完成
- [ ] CI/CD 需求（流程、憑證、環境變數）定義完成
- [ ] HTTPS Only 與 Key Vault 等安全需求定義完成

### 階段 4：驗證準則 / Validation Criteria

- [ ] Codespace/本地啟動的驗收條件定義完成
- [ ] 健康檢查與監控驗收條件定義完成
- [ ] DB 連線與權限/網路驗收條件定義完成

---

## 總結 / Summary

本文件用於從既有程式碼與舊規格出發，產出 .NET 8 / ASP.NET Core Web API + React 的移轉「需求規格文件」，內容涵蓋架構拆分、相容性盤點、套件安全升級目標、DI 與服務設計需求，以及 Azure App Service（Linux）與 Azure SQL 的部署需求與驗證準則；**不包含任何程式碼實作或部署自動化檔案**。

---

## 參考資源 / References

- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core) - ASP.NET Core 官方文件
- [.NET 8 Migration Guide](https://learn.microsoft.com/dotnet/core/porting/) - .NET 遷移指南
- [Azure App Service Documentation](https://learn.microsoft.com/azure/app-service/) - Azure App Service 文件
- [Azure SQL Documentation](https://learn.microsoft.com/azure/azure-sql/) - Azure SQL 文件
- [Microsoft.Data.SqlClient](https://github.com/dotnet/SqlClient) - SQL Client 版本與安全公告
- [React Documentation](https://react.dev/) - React 官方文件

---

**版本 / Version:** 2.1  
**最後更新 / Last Updated:** 2026  
**維護者 / Maintainer:** Documentation Team
