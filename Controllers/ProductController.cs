using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly RepositoryContext _context;
        // neden readonly?
        /* 
        Bu alanın değeri sadece bir kez, yani constructor çalışırken atanabilir. Constructor bittikten sonra 
        bu alanın değeri asla değiştirilemez. Bu, _context nesnesinin yanlışlıkla kodun başka bir yerinde 
        değiştirilmesini engelleyen bir güvenlik önlemidir. Bize verilen veritabanı bağlantısını kaybetmememizi sağlar.
        */

        public ProductController(RepositoryContext context) // constructor
        // RepositoryContext context --> ProductController, constructor'ında bu parametreyi isteyerek, 
        // "Beni kim oluşturuyorsa, oluştururken bana bir RepositoryContext nesnesi vermek zorunda"

        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Products.ToList();
            return View(model);
        }

        public IActionResult Get(int id)
        {
            Product product = _context.Products.First(p => p.ProductId.Equals(id));
            return View(product);
        }



    }
}

/* !!! NASIL ÇALIŞIYOR???
Program.cs'e bak.!!!
1) Program.cs'deki o satırı hatırlayalım: builder.Services.AddDbContext<RepositoryContext>(...);

Sen bu kodla ASP.NET Core'un Dependency Injection (DI) Container'ına (yani "Hizmet Sağlayıcısı"na) RepositoryContext 
nesnesinin nasıl oluşturulacağını öğrettin. Ona veritabanının adresini (ConnectionString) verdin.

2) Tarayıcıdan /Product/Index adresine bir istek geldiğinde, ASP.NET Core bir ProductController nesnesi yaratması gerektiğini anlar.

3) ProductController'ın constructor'ına bakar (çünkü nesne oluşturulması gerek, 2. madde.) ve onun bir RepositoryContext nesnesi istediğini görür.

4) Hemen DI Container'ına döner ve sorar: "Bana bir RepositoryContext nesnesi lazım, nasıl yaratacağımı biliyor musun?"

5) DI Container, Program.cs'teki kaydına bakar ve "Evet, biliyorum!" der. Senin öğrettiğin ayarlarla 
(UseSqlite ve ConnectionString ile) yeni bir RepositoryContext nesnesi yaratır.

6) Bu yeni yaratılan nesneyi, ProductController'ın constructor'ına parametre olarak enjekte eder (verir).

7) ProductController da bu nesneyi alıp _context alanına atar ve Index metodunda kullanmaya başlar

*/