## appsetting.json
- ConnectionStrings: Chuỗi kết nối SQL Server
Để tránh hardcode mật khẩu nên để trống file cấu hình và lấy "secret: riêng

"ConnectionStrings": {
  "DefaultConnection": ""
}

Sau đó dùng User Secrets .NET:
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.;Database=GameStore;User ID=sa;Password=123456;TrustServerCertificate=True;MultipleActiveResultSets=true"