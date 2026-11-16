
# ==========================================
# 第一阶段：Build（编译阶段）
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src
COPY ["OrderManagement.Api.csproj", "./"]
RUN dotnet restore "OrderManagement.Api.csproj"
COPY . .

# 编译并发布
RUN dotnet publish "OrderManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# 第二阶段：Final（最终运行阶段）
# ==========================================

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"]