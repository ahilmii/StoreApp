using Microsoft.EntityFrameworkCore;

namespace StoreApp.Models
{
  public class RepositoryContext : DbContext /*
    Bu sınıf, projenizdeki Entity Framework Core'un beynidir. Onu, C# dünyası ile veritabanı dünyası arasında duran bir tercüman veya köprü olarak düşünebilirsin.
    DbContext'ten Miras Alması: Bu sınıfın DbContext'ten miras alması, ona veritabanı ile iletişim kurma, tabloları yönetme, 
    veri ekleme/silme/güncelleme gibi özel güçler verir. */
  {
    public DbSet<Product> Products { get; set; }

    /* 
    !!!!!
    public DbSet<Product> Products { get; set; }: Bu satır, tercümana şunu söyler: "Veritabanında Products adında bir tablo olacak (veya var). 
    Bu tablodaki her bir satır, benim C# projemdeki Product sınıfına karşılık gelecek." Sen C# kodunda Products listesi 
    üzerinde bir işlem yaptığında (context.Products.ToList() gibi), DbContext bunu arka planda gerçek SQL sorgusuna (SELECT * FROM Products) çevirir.  
    */

    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Bu metod, veritabanı modeli oluşturulurken çalıştırılır. Burada, veritabanı tabloları ve ilişkileri yapılandırılır.
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Product>().HasData(
        new Product() { ProductId = 1, ProductName = "Computer", Price = 17000 },
        new Product() { ProductId = 2, ProductName = "Keyboard", Price = 5000 },
        new Product() { ProductId = 3, ProductName = "Mouse", Price = 1000 },
        new Product() { ProductId = 4, ProductName = "Monitor", Price = 9000 },
        new Product() { ProductId = 5, ProductName = "Deck", Price = 500 }
      );

    }
    /* 
    HasData Metodu:
    HasData metodu, veritabanına başlangıç verileri (seed data) eklemek için kullanılır.
    Eğer Products tablosu boşsa, bu veriler tabloya eklenir.
    Eğer tablo zaten doluysa, bu veriler eklenmez veya mevcut veriler değiştirilmez.
    */

    // bu işlemin gerçekleşmesi için dotnet ef migrations add ProductSeedData kullanıyoruz.
    // ilgili değişiklikleri veritabanına yansıtmak için ise: dotnet ef database update


  }
}





/* 
"ConnectionStrings": {
  "sqlconnection": "Data Source=database.db"
}

Bu bloğun anlamı şudur:

Nerede?: Uygulamanın ayar dosyasında. Neden burada? Çünkü veritabanının konumu (veya şifresi) değişebilir. Bu bilgiyi doğrudan C# koduna yazmak 
(hardcode etmek) yerine, böyle bir ayar dosyasına koyarız. Böylece, kodu değiştirmeden sadece bu metni değiştirerek farklı bir veritabanına 
(örneğin test veritabanından gerçek sunucudaki veritabanına) bağlanabiliriz. Bu çok esnek bir yapı sağlar.
Ne İşe Yarar?: Uygulamanıza, "Konuşmak istediğin veritabanı bir SQLite veritabanı ve dosyasının adı database.db" der. Eğer SQL Server kullansaydın, 
burada sunucu adresi, kullanıcı adı, şifre gibi bilgiler olacaktı.

Kısacası ConnectionString, uygulamanızın veritabanını nerede bulacağını söylediği bir adres defteridir.



*/




