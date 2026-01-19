# Contoso University - 本地設置指南

本指南說明如何在 GitHub Codespaces 或本地環境中運行 Contoso University 應用程式。

## 前置需求

- .NET 8.0 SDK
- Docker（用於運行 SQL Server）
- Git

## 本地開發設置（Codespace）

### 1. 設置 SQL Server 密碼（可選）

您可以使用環境變數自訂 SQL Server 密碼：

```bash
# Linux/Mac/Codespace
export SQL_PASSWORD="YourCustomPassword"

# Windows PowerShell
$env:SQL_PASSWORD="YourCustomPassword"
```

如果不設置，將使用預設密碼 `YourStrong@Passw0rd`。

⚠️ **安全提示**: 不要在生產環境使用預設密碼！

### 2. 啟動 SQL Server 容器

```bash
# 在專案根目錄執行
docker-compose up -d

# 檢查容器狀態
docker-compose ps

# 查看日誌
docker-compose logs sqlserver
```

### 3. 等待 SQL Server 就緒

SQL Server 需要約 10-20 秒啟動。您可以使用以下命令檢查（使用您設置的密碼）:

```bash
# 如果使用環境變數
docker-compose exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "${SQL_PASSWORD:-YourStrong@Passw0rd}" -C -Q "SELECT @@VERSION"

# 或直接使用預設密碼
docker-compose exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -C -Q "SELECT @@VERSION"
```

### 4. 設置環境變數並啟動應用

```bash
# 切換到 ContosoUniversity 專案目錄
cd ContosoUniversity

# 設置環境為 Development
export ASPNETCORE_ENVIRONMENT=Development

# 運行應用程式
dotnet run
```

### 4. 訪問應用

應用程式將在以下地址啟動：
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

在 Codespace 中，會自動轉發端口，您可以通過 Codespace 提供的 URL 訪問。

## 資料庫連接字串

Development 環境使用以下連接字串（在 `appsettings.Development.json` 中）:

```
Server=localhost,1433;Database=ContosoUniversity;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=True
```

⚠️ **注意**: 如果您使用自訂密碼（`SQL_PASSWORD` 環境變數），請相應更新 `appsettings.Development.json` 中的連接字串。

## 資料庫初始化

應用程式首次啟動時會自動:
1. 創建資料庫（如果不存在）
2. 創建資料表
3. 載入預存程序（`ToDoStoredProcedures.sql`）
4. 填充種子資料（學生、課程、教師等）

## 測試功能

### CRUD 操作測試

訪問以下頁面測試各項功能：

1. **Students** - `http://localhost:5000/Students`
   - Create, Read, Update, Delete 學生資料
   - 搜索和分頁功能

2. **Courses** - `http://localhost:5000/Courses`
   - Create, Read, Update, Delete 課程資料
   - 上傳教材圖片

3. **Instructors** - `http://localhost:5000/Instructors`
   - Create, Read, Update, Delete 教師資料
   - 指派課程

4. **Departments** - `http://localhost:5000/Departments`
   - Create, Read, Update, Delete 部門資料

5. **ToDo List** - `http://localhost:5000/ToDos`
   - 使用預存程序的 ToDo 功能

6. **Notifications** - `http://localhost:5000/Notifications`
   - 查看系統通知（in-memory queue）

## 停止環境

```bash
# 停止 SQL Server 容器
docker-compose down

# 停止並刪除數據卷（謹慎使用）
docker-compose down -v
```

## 清除資料庫

如果需要重置資料庫:

```bash
# 停止容器並刪除數據卷
docker-compose down -v

# 重新啟動
docker-compose up -d
```

## 常見問題

### 1. 無法連接到資料庫

**問題**: 應用無法連接到 SQL Server

**解決方案**:
- 確認 Docker 容器正在運行: `docker-compose ps`
- 檢查 SQL Server 日誌: `docker-compose logs sqlserver`
- 等待 SQL Server 完全啟動（約 10-20 秒）

### 2. 端口衝突

**問題**: 端口 1433 已被佔用

**解決方案**:
修改 `docker-compose.yml` 中的端口映射:
```yaml
ports:
  - "1434:1433"  # 使用 1434 替代
```

然後更新 `appsettings.Development.json` 中的連接字串:
```
Server=localhost,1434;...
```

### 3. 權限錯誤

**問題**: SQL Server 容器無法創建數據卷

**解決方案**:
```bash
# Linux/Mac
sudo chown -R 10001:0 /var/lib/docker/volumes/

# 或使用具名卷
docker volume create sqlserver_data
```

## 開發工作流程

1. 啟動 Docker 容器
2. 修改代碼
3. 使用 `dotnet run` 測試更改（支援熱重載）
4. 使用 `dotnet build` 驗證編譯
5. 提交更改到 Git

## 資料庫管理工具

您可以使用以下工具連接到 SQL Server:

### Azure Data Studio (推薦)
- Server: `localhost,1433`
- Authentication: SQL Login
- Username: `sa`
- Password: `YourStrong@Passw0rd`
- Trust Server Certificate: Yes

### SQL Server Management Studio (SSMS)
- 使用相同的連接資訊

### VS Code Extension
- 安裝 "SQL Server (mssql)" 擴展
- 使用相同的連接資訊

## 下一步

- 查看 [DEPLOYMENT_GUIDE.md](DEPLOYMENT_GUIDE.md) 了解 Azure 部署
- 查看 [UPGRADE_REPORT.md](UPGRADE_REPORT.md) 了解遷移細節
- 查看 [README.md](../README.md) 了解專案架構
