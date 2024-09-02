# dotnet ef 指令

```
dotnet ef migrations add InitialCreate -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence

// Update
dotnet ef database update -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence

// Rollback
dotnet ef database update 0 -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence

// Remove
dotnet ef migrations remove -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```

查看遷移列表
```
dotnet ef migrations list -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```

生成遷移
```
dotnet ef migrations add UpdateStockEntity -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```

更新資料庫
```
dotnet ef database update -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```

回滾資料庫到特定遷移
```
dotnet ef database update <MigrationName> -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```

如回滾到初始遷移
```
dotnet ef database update InitialCreate -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```

移除最後一個遷移
```
dotnet ef migrations remove -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```