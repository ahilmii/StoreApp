using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"));
});

/* 
İşte sihrin gerçekleştiği yer burası. Bu kod, yukarıda bahsettiğimiz Tercüman (RepositoryContext) 
ile Adres Defteri'ni (ConnectionString) bir araya getirir ve bunu uygulamanın hizmetine sunar.


builder.Services.AddDbContext<RepositoryContext>(...): Bu satır, ASP.NET Core'un kalbi olan Dependency Injection (Bağımlılık Enjeksiyonu) 
sistemine RepositoryContext'i tanıtıyor. Anlamı şu: "Ey uygulama! Senin herhangi bir yerinde (örneğin bir Controller içinde) RepositoryContext 
tipinde bir nesneye ihtiyaç duyulursa, onu nasıl oluşturacağını ben sana burada öğretiyorum. Artık bu nesneyi yaratma görevi sende."

options.UseSqlite(...): Bu, DbContext'e "Sen bir SQLite veritabanı ile konuşacaksın" talimatını verir. 
Eğer SQL Server kullanıyor olsaydık burada options.UseSqlServer(...) yazardı.

builder.Configuration.GetConnectionString("sqlconnection"): İşte en kritik kısım! Bu kod, appsettings.json dosyasını okur, 
içindeki "ConnectionStrings" bölümünü bulur ve oradaki "sqlconnection" anahtarına sahip olan adres bilgisini alır.


Uygulamanın herhangi bir yerinde (örn: bir ProductController'da) "veritabanına erişmen gerektiğinde", Dependency Injection sisteminden bir RepositoryContext nesnesi istersin.
DI sistemi, Program.cs'teki kayda bakar ve yeni bir RepositoryContext nesnesi oluşturmaya başlar.
Oluştururken, appsettings.json'dan "sqlconnection" adresini okur.
Bu adresi kullanarak, RepositoryContext'in doğru SQLite veritabanına bağlanmasını sağlar.
Artık elinde, doğru veritabanına bağlanmış, kullanıma hazır bir RepositoryContext nesnesi vardır ve sen C# kodunla veritabanı işlemlerini yapmaya başlayabilirsin.

*/


var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();
