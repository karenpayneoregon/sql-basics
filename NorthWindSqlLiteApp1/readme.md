# About


## Connection string/EF Core create data

- `EntityConfiguration` is for EF Core database creation.
  - EntitySettings.Instance.CreateNew
- Get connection string from `appsettings.json`.
   - AppConnections.Instance.MainConnection

appsettings.json
```json
{
  "ConnectionStrings": {
    "MainConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=AppsettingsConfigurations;Integrated Security=True;Encrypt=False"
  },
  "EntityConfiguration": {
    "CreateNew": true
  }
}
```