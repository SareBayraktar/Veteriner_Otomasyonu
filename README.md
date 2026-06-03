🐾 LULU Veteriner Kliniği Otomasyonu
🏥 LULU Veteriner Kliniği Yönetim Sistemi
❤️ "Sevgiyle, Sağlıkla, Yanınızdayız."

📌 Entity Framework Code First yaklaşımı kullanılarak geliştirilmiş, veteriner kliniklerinde hasta takibi, sahip yönetimi, muayene işlemleri, ürün yönetimi ve satış süreçlerini dijital ortamda yönetmek amacıyla hazırlanmış masaüstü otomasyon sistemidir.








📖 Proje Hakkında

LULU Veteriner Kliniği Otomasyonu;

🐶 Hayvan kayıtlarının tutulması

👤 Hayvan sahiplerinin yönetilmesi

👨‍⚕️ Veteriner bilgilerinin saklanması

🩺 Muayene kayıtlarının oluşturulması

📦 Ürün stoklarının yönetilmesi

🛒 Satış işlemlerinin gerçekleştirilmesi

📊 Klinik raporlarının görüntülenmesi

amacıyla geliştirilmiştir.

Proje C# Windows Forms, Entity Framework 6 ve SQL Server kullanılarak hazırlanmıştır.

🚀 Kullanılan Teknolojiler
Teknoloji	Açıklama
C#	Uygulama geliştirme
Windows Forms	Kullanıcı arayüzü
Entity Framework 6	ORM Yapısı
SQL Server	Veritabanı
LINQ	Veri sorgulama
Code First	Veritabanı oluşturma yaklaşımı
DataGridView	Veri listeleme
ComboBox	İlişkisel veri seçimi
🗂️ Veritabanı Yapısı

Sistemde toplam 6 adet tablo bulunmaktadır.

👤 Tablo_Sahip

Hayvan sahiplerinin bilgilerini saklar.

Alanlar
Sahip_Id
Sahip_Adi
Sahip_Soyadi
Sahip_Telefon
Sahip_Email
🐾 Tablo_Hayvan

Kliniğe kayıtlı hayvan bilgilerini saklar.

Alanlar
Hayvan_Id
Hayvan_Adi
Hayvan_Turu
Hayvan_Cinsi
Sahip_Id (FK)
👨‍⚕️ Tablo_Veteriner

Veteriner hekim bilgilerini saklar.

Alanlar
Veteriner_Id
Veteriner_Adi
Veteriner_Soyadi
Veteriner_Uzmanlik
🩺 Tablo_Muayene

Gerçekleştirilen muayeneleri saklar.

Alanlar
Muayene_Id
Muayene_Tarihi
Teshis
Tedavi
Muayene_Ucreti
Hayvan_Id (FK)
Veteriner_Id (FK)
📦 Tablo_Urun

Veteriner ürünlerini saklar.

Alanlar
Urun_Id
Urun_Adi
Urun_Turu
Urun_Fiyati
Stok_Miktari
🛒 Tablo_Satis

Gerçekleştirilen satışları saklar.

Alanlar
Satis_Id
Satis_Tarihi
Miktar
Toplam_Tutar
Urun_Id (FK)
Sahip_Id (FK)
🔗 Tablolar Arasındaki İlişkiler
SAHİP
 │
 ├──────► HAYVAN
 │
 └──────► SATIŞ

HAYVAN
 │
 └──────► MUAYENE

VETERİNER
 │
 └──────► MUAYENE

ÜRÜN
 │
 └──────► SATIŞ
🏠 Ana Menü

Ana menü üzerinden sistemin tüm modüllerine erişim sağlanmaktadır.

Menü Butonları

👤 Sahipler

🐾 Hayvanlar

👨‍⚕️ Veterinerler

🩺 Muayeneler

📦 Ürünler

🛒 Satışlar

👤 Sahip Yönetimi

Sahip modülü üzerinden;

✅ Yeni sahip ekleme

✅ Sahip listeleme

✅ Sahip güncelleme

✅ Sahip silme

✅ Telefon doğrulama

✅ E-posta doğrulama

işlemleri gerçekleştirilmektedir.

Validasyonlar

📞 Telefon numarası 11 haneli olmak zorundadır.

📞 Telefon alanına yalnızca rakam girilebilir.

📧 E-posta adresi kontrol edilmektedir.

⚠️ Boş alan bırakılması engellenmiştir.

Güvenlik

Bir sahibe ait hayvan kaydı bulunuyorsa silme işlemi engellenmektedir.

🐾 Hayvan Yönetimi

Hayvan modülü üzerinden;

✅ Hayvan ekleme

✅ Hayvan listeleme

✅ Hayvan güncelleme

✅ Hayvan silme

✅ Sahip atama

işlemleri gerçekleştirilmektedir.

Özellikler

🔄 ComboBox üzerinden sahip seçimi

📋 Sahip adı otomatik görüntüleme

📊 DataGridView listeleme

Güvenlik

Bir hayvana ait muayene kaydı bulunuyorsa silme işlemi engellenmektedir.

👨‍⚕️ Veteriner Yönetimi

Veteriner kayıtlarının tutulduğu modüldür.

Özellikler

✅ Veteriner ekleme

✅ Veteriner listeleme

✅ Veteriner güncelleme

✅ Veteriner silme

✅ Uzmanlık alanı kaydetme

Güvenlik

Veterinere ait muayene kaydı bulunuyorsa silme işlemi engellenmektedir.

🩺 Muayene Yönetimi

Muayene işlemlerinin yürütüldüğü modüldür.

Özellikler

✅ Muayene ekleme

✅ Muayene listeleme

✅ Muayene güncelleme

✅ Muayene silme

✅ Hayvan seçimi

✅ Veteriner seçimi

✅ Teşhis kaydı

✅ Tedavi kaydı

✅ Ücret kaydı

✅ Tarih seçimi

Veri Doğrulama

💰 Muayene ücretine yalnızca sayısal veri girilebilir.

⚠️ Boş alan bırakılması engellenmiştir.

📊 Muayene Raporları
🏆 En Çok Muayene Edilen Hayvan Türü

Sistemde en fazla muayene edilen hayvan türünü görüntüler.

📅 Bu Ayki Muayeneler

Bulunulan ay içerisinde yapılan muayeneleri listeler.

💰 Muayene Gelirleri

Gösterilen bilgiler:

Toplam muayene geliri
Bu aya ait muayene geliri
📦 Ürün Yönetimi

Veteriner ürünlerinin yönetildiği modüldür.

Özellikler

✅ Ürün ekleme

✅ Ürün listeleme

✅ Ürün güncelleme

✅ Ürün silme

✅ Stok takibi

✅ Fiyat takibi

Veri Doğrulama

💰 Fiyat alanı sayısal olmalıdır.

📦 Stok alanı sayısal olmalıdır.

⚠️ Boş alan bırakılması engellenmiştir.

Güvenlik

Ürüne ait satış kaydı bulunuyorsa silme işlemi engellenmektedir.

🛒 Satış Yönetimi

Projenin en gelişmiş modüllerinden biridir.

Özellikler

✅ Satış ekleme

✅ Satış listeleme

✅ Satış güncelleme

✅ Satış silme

✅ Toplam tutar hesaplama

✅ Stok düşürme

✅ Stok geri ekleme

✅ Kritik stok kontrolü

📉 Kritik Stok Sistemi

Satış gerçekleştirildiğinde ürün stoğu otomatik düşürülmektedir.

Eğer stok miktarı:

20 veya altına düşerse

sistem otomatik olarak uyarı vermektedir.

Uyarı Mesajı

⚠️ Kritik Stok Uyarısı

Kalan stok bilgisi kullanıcıya gösterilir.

🔄 Otomatik Stok Güncelleme Sistemi
Satış Eklendiğinde
Ürün Stoğu = Eski Stok - Satış Miktarı
Satış Silindiğinde
Ürün Stoğu = Eski Stok + Satış Miktarı
Satış Güncellendiğinde
Eski stok geri eklenir
↓
Yeni stok düşülür

Bu sayede stok tutarsızlıkları önlenmiştir.

📈 Satış Raporları
🏆 En Çok Satan Ürün

Sistemde en çok satılan ürünü görüntüler.

💰 Sahiplerin Toplam Harcamaları

Tüm sahiplerin toplam harcamalarını listeler.

🎨 Arayüz Tasarımı

Proje tamamen veteriner kliniği konseptine uygun şekilde tasarlanmıştır.

Kullanılan Tasarım Unsurları

🐾 Pati figürleri

🐶 Köpek görselleri

🐱 Kedi görselleri

🐰 Tavşan görselleri

🦜 Kuş görselleri

🦔 Kirpi görselleri

🐹 Hamster görselleri

🏥 Veteriner kliniği ortamları

💊 Veteriner ürün rafları

🩺 Klinik ekipmanları

🎨 Mavi ve beyaz ağırlıklı kurumsal renk paleti

💡 Entity Framework Özellikleri

Projede Entity Framework Code First yaklaşımı kullanılmıştır.

Kullanılan Yapılar

✅ DbContext

✅ DbSet

✅ Navigation Property

✅ Foreign Key

✅ Data Annotation

Kullanılan Annotationlar
[Table]
[Key]
[Required]
[MaxLength]
🧠 Kullanılan LINQ Sorguları

Projede LINQ kullanılarak;

📊 Gruplama

📈 Toplama

🔍 Filtreleme

📋 Listeleme

🏆 En yüksek değer bulma

işlemleri gerçekleştirilmiştir.

🛡️ Hata Yönetimi

Tüm modüllerde:

try
{
}
catch
{
}

yapıları kullanılarak hata kontrolü sağlanmıştır.

📷 Uygulama Ekranları

🏠 Ana Menü

👤 Sahip Formu

🐾 Hayvan Formu

👨‍⚕️ Veteriner Formu

🩺 Muayene Formu

📦 Ürün Formu

🛒 Satış Formu

🎯 Projenin Kazanımları

Bu proje sayesinde;

✅ Entity Framework öğrenildi.

✅ Code First yaklaşımı uygulandı.

✅ SQL Server ile çalışma deneyimi kazanıldı.

✅ CRUD işlemleri geliştirildi.

✅ İlişkisel veritabanı mantığı uygulandı.

✅ Raporlama ekranları geliştirildi.

✅ Stok yönetim sistemi geliştirildi.

✅ Windows Forms tasarımı geliştirildi.

✅ Gerçek hayata uygun otomasyon sistemi oluşturuldu.

👩‍💻 Geliştirici
Sare Bayraktar

🎓 Mehmet Akif Ersoy Üniversitesi

💻 Yönetim Bilişim Sistemleri

📚 Görsel Programlama & Entity Framework Projesi

🏥 Lulu Veteriner Kliniği Otomasyonu

⭐ Bu proje eğitim amaçlı geliştirilmiştir.

🐾 LULU Veteriner Kliniği – Sevgiyle, Sağlıkla, Yanınızdayız.
