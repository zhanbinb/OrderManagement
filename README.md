# OrderManagement

基于 ASP.NET Core 的订单管理 Web API 示例项目，涵盖分层结构、EF Core 数据访问、依赖注入生命周期、Options 配置模式、全局异常处理与 Serilog 结构化日志等常用实践。


## 技术栈

| 组件 | 说明 |
| --- | --- |
| .NET 10 (net10.0) | 运行时与 SDK |
| ASP.NET Core Web API | 控制器风格的 REST API |
| Entity Framework Core 10 | ORM（当前使用 Npgsql / PostgreSQL） |
| Serilog | 结构化日志（控制台 + 按天滚动文件） |
| Swagger UI (Swashbuckle) | 开发环境 API 文档与调试 |

## 项目结构

```text
OrderManagement/
├── OrderManagement.slnx
└── src/
    └── OrderManagement.Api/
        ├── Controllers/        # 控制器（Orders CRUD、DI 生命周期演示、Options 演示等）
        ├── DTOs/Orders/        # 请求 / 响应 DTO
        ├── Data/               # ApplicationDbContext
        ├── Entities/           # Order 实体
        ├── Enums/              # OrderStatus 等枚举
        ├── Exceptions/         # 全局异常处理器（IExceptionHandler）
        ├── Migrations/         # EF Core 数据库迁移
        ├── Services/           # 业务服务与 DI 生命周期演示接口
        ├── Settings/           # OrderSettings（Options 模式）
        ├── Program.cs          # 应用入口与依赖注册
        └── appsettings.json    # 配置文件
```

## 环境要求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL（默认连接 `localhost:5432`，库名 `OrderManagementDb`）
- EF Core 命令行工具（如未安装）：`dotnet tool install --global dotnet-ef`

## 快速开始

1. 修改 `src/OrderManagement.Api/appsettings.json` 中的数据库连接串：

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=OrderManagementDb;Username=postgres;Password=<your-password>"
   }
   ```

2. 应用数据库迁移：

   ```bash
   cd src/OrderManagement.Api
   dotnet ef database update
   ```

3. 启动项目：

   ```bash
   dotnet run --project src/OrderManagement.Api
   ```

   | 地址 | 说明 |
   | --- | --- |
   | http://localhost:5019 | HTTP |
   | https://localhost:7082 | HTTPS |
   | https://localhost:7082/swagger | Swagger UI（仅开发环境） |

## API 概览

| 方法 | 路由 | 说明 |
| --- | --- | --- |
| POST | `/api/orders` | 创建订单 |
| GET | `/api/orders` | 查询订单列表 |
| GET | `/api/orders/{id}` | 查询单个订单 |
| PUT | `/api/orders/{id}` | 更新订单 |
| DELETE | `/api/orders/{id}` | 删除订单 |
| GET | `/api/orders/test-error` | 触发异常，验证全局异常处理 |

另有 `HelloController`、`LifecycleController`、`OptionsExperimentController`、`SettingsController` 等用于学习演示的接口。

## 日志

- **控制台**：实时输出
- **文件**：`logs/order-yyyyMMdd.log`，按天滚动，单文件上限 10MB，保留最近 30 个文件

## 安全提示

`appsettings.json` 中包含数据库明文密码，仅建议用于本地开发。生产环境请使用环境变量、`dotnet user-secrets` 或密钥管理服务注入敏感配置。
