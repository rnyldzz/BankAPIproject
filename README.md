Elbette. Projenizi GitHub'a yüklediğinize göre, repo'nun ana sayfasında yer alacak, teknik ve anlaşılır bir README metni oluşturalım.

---

### BankAPIproject

Bu proje, finansal işlemleri yönetmek için geliştirilmiş bir ASP.NET Core Web API'sidir. API, kullanıcı ve işlem verilerini yönetmek için MySQL veritabanı ile entegre bir şekilde çalışır. Proje, `transfer` ve `pay_debt` gibi temel bankacılık işlemlerini gerçekleştiren RESTful endpoint'leri sunar.

#### **Teknolojiler**
* **Çatı (Framework):** ASP.NET Core (.NET 8.0)
* **Veritabanı:** MySQL
* **ORM (Object-Relational Mapper):** Entity Framework Core
* **Test ve Geliştirme:** Swagger UI

#### **Veritabanı Yapısı**
Proje, iki ana tablo üzerinde çalışır:
* **`users`:** Kullanıcı bilgilerini ve bakiyelerini saklar.
* **`transactions`:** Tüm para transferi ve borç ödeme işlemlerini kaydeder.
Bu tablolar, veri bütünlüğü için `FOREIGN KEY` ilişkileriyle birbirine bağlıdır.

#### **API Endpoint'leri**
* **`POST /api/Transactions/transfer`**
    * **Açıklama:** İki kullanıcı arasında para transferi gerçekleştirir.
    * **Parametreler:** `fromAccountId`, `toAccountId`, `amount`

* **`POST /api/Transactions/pay_debt`**
    * **Açıklama:** Kullanıcının borcunu "Bank" adlı bir hesaba ödemesini sağlar.
    * **Parametreler:** `accountId`, `amount`

#### **Hata Yönetimi ve Geliştirme Notları**
Geliştirme sürecinde karşılaşılan ve çözülen başlıca teknik sorunlar şunlardır:
* NuGet paket versiyon uyumsuzlukları (`System.TypeLoadException`).
* C# PascalCase ve MySQL snake_case adlandırma uyumsuzluğu (`Unknown column` hatası).
* İstek gövdesi (`JSON`) format hataları (`400 Bad Request`).

#### **Kurulum ve Çalıştırma**
1.  Projeyi klonlayın.
2.  MySQL sunucunuzda `DataBase Adınız` veritabanını ve gerekli tabloları oluşturun.
3.  `appsettings.json` dosyasında veritabanı bağlantı dizenizi (`DefaultConnection`) güncelleyin.
4.  Visual Studio'yu kullanarak projeyi derleyin ve çalıştırın.
