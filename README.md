# 🐾 LULU Veteriner Kliniği Otomasyon Sistemi

![Version](https://img.shields.io/badge/versiyon-1.0.0-blue?style=for-the-badge)
![Platform](https://img.shields.io/badge/platform-Windows-lightblue?style=for-the-badge&logo=windows)
![Language](https://img.shields.io/badge/C%23-.NET%20Framework-purple?style=for-the-badge&logo=csharp)
![Database](https://img.shields.io/badge/SQL%20Server-Express-red?style=for-the-badge&logo=microsoftsqlserver)
![ORM](https://img.shields.io/badge/Entity%20Framework-6.5.2-green?style=for-the-badge)

> *"Sevgiyle, Sağlıkla, Yanınızdayız."* 🏥

LULU Veteriner Kliniği Otomasyon Sistemi, veteriner kliniklerinin günlük işlemlerini dijital ortama taşımak amacıyla geliştirilmiş kapsamlı bir masaüstü uygulamasıdır. **Database First** yaklaşımıyla geliştirilen bu projede önce SQL Server tarafında tablolar oluşturulmuş, ardından Entity Framework kullanılarak C# tarafında her tabloya karşılık gelen model sınıfları yazılmıştır. Hayvan sahiplerinden muayene kayıtlarına, ürün satışından stok takibine kadar tüm klinik süreçleri tek bir sistemde yönetilmektedir.

---

## 📋 İçindekiler

- [✨ Özellikler](#-özellikler)
- [🗄️ Veritabanı Yapısı](#️-veritabanı-yapısı)
- [📁 Proje Yapısı](#-proje-yapısı)
- [⚙️ Kurulum](#️-kurulum)
- [🛠️ Kullanılan Teknolojiler](#️-kullanılan-teknolojiler)
- [📊 LINQ Sorguları](#-linq-sorguları)
- [🔗 UML Diyagramı](#-uml-diyagramı)

---

## ✨ Özellikler

### 👤 Sahip Yönetimi
- Hayvan sahiplerini listeleme, ekleme, güncelleme ve silme (4 CRUD işlemi)
- Telefon numarası format kontrolü (11 haneli, sadece rakam)
- E-posta format doğrulaması
- Satıra tıklayınca bilgilerin otomatik forma aktarılması
- Sahibe ait hayvan kaydı varken silme engelleme

### 🐾 Hayvan Yönetimi
- Hayvanları listeleme, ekleme, güncelleme ve silme (4 CRUD işlemi)
- Hayvan sahibini ComboBox ile seçme
- Ad, Tür ve Cins bilgisi yönetimi
- Hayvana ait muayene kaydı varken silme engelleme

### 👨‍⚕️ Veteriner Yönetimi
- Veterinerleri listeleme, ekleme, güncelleme ve silme (4 CRUD işlemi)
- Ad, Soyad ve Uzmanlık bilgisi yönetimi
- Veterinere ait muayene kaydı varken silme engelleme

### 🏥 Muayene Yönetimi
- Muayeneleri listeleme, ekleme, güncelleme ve silme (4 CRUD işlemi)
- Hayvan ve Veteriner ComboBox ile seçimi
- Muayene tarihi, teşhis, tedavi ve ücret bilgisi yönetimi
- 3 adet LINQ raporu: en çok muayene edilen hayvan türü, bu ayki muayeneler, toplam ve aylık muayene gelirleri

### 💊 Ürün Yönetimi
- Ürünleri listeleme, ekleme, güncelleme ve silme (4 CRUD işlemi)
- Ürün adı, türü, fiyatı ve stok miktarı yönetimi
- Sayısal alan format doğrulaması (fiyat ve stok)
- Ürüne ait satış kaydı varken silme engelleme

### 🛒 Satış Yönetimi
- Satışları listeleme, ekleme, güncelleme ve silme (4 CRUD işlemi)
- Ürün ve Sahip ComboBox ile seçimi
- Otomatik toplam tutar hesaplama (Fiyat × Miktar)
- Stok kontrolü — yetersiz stok uyarısı
- Kritik stok uyarısı (stok 20'nin altına düşünce)
- Satış silindiğinde stok otomatik geri ekleme
- 2 adet LINQ raporu: en çok satan ürün, sahibin toplam harcaması

---

## 🗄️ Veritabanı Yapısı

Sistem 6 adet birbiriyle ilişkili tablo içermektedir. **Database First** yaklaşımı kullanılmıştır: tablolar önce SQL Server'da oluşturulmuş, ardından her tabloya karşılık gelen C# model sınıfları `[Table]` ve `[Key]` attribute'ları ile eşleştirilmiştir.

```
Tablo_Sahip
├── Sahip_Id (PK)
├── Sahip_Adi
├── Sahip_Soyadi
├── Sahip_Telefon
└── Sahip_Email

Tablo_Hayvan
├── Hayvan_Id (PK)
├── Hayvan_Adi
├── Hayvan_Turu
├── Hayvan_Cinsi
└── Sahip_Id (FK → Tablo_Sahip)

Tablo_Veteriner
├── Veteriner_Id (PK)
├── Veteriner_Adi
├── Veteriner_Soyadi
└── Veteriner_Uzmanlik

Tablo_Muayene
├── Muayene_Id (PK)
├── Muayene_Tarihi
├── Teshis
├── Tedavi
├── Muayene_Ucreti
├── Hayvan_Id (FK → Tablo_Hayvan)
└── Veteriner_Id (FK → Tablo_Veteriner)

Tablo_Urun
├── Urun_Id (PK)
├── Urun_Adi
├── Urun_Turu
├── Urun_Fiyati
└── Stok_Miktari

Tablo_Satis
├── Satis_Id (PK)
├── Satis_Tarihi
├── Miktar
├── Toplam_Tutar
├── Urun_Id (FK → Tablo_Urun)
└── Sahip_Id (FK → Tablo_Sahip)
```

### Tablo İlişkileri

```
Sahip     (1) ──── (∞) Hayvan
Hayvan    (1) ──── (∞) Muayene
Veteriner (1) ──── (∞) Muayene
Urun      (1) ──── (∞) Satis
Sahip     (1) ──── (∞) Satis
```

---

## 📁 Proje Yapısı

```
Veteriner_Otomasyonu/
│
├── Model Sınıfları (Entity Framework - Database First)
│   ├── Sahip.cs              → Tablo_Sahip
│   ├── Hayvan.cs             → Tablo_Hayvan
│   ├── Veteriner.cs          → Tablo_Veteriner
│   ├── Muayene.cs            → Tablo_Muayene
│   ├── Urun.cs               → Tablo_Urun
│   ├── Satis.cs              → Tablo_Satis
│   └── VeterinerDbContext.cs → DbContext (tüm tablolar)
│
├── Form Dosyaları
│   ├── AnaMenu.cs            → Ana menü ekranı
│   ├── Form_Sahip.cs         → Sahip yönetimi
│   ├── Form_Hayvan.cs        → Hayvan yönetimi
│   ├── Form_Veteriner.cs     → Veteriner yönetimi
│   ├── Form_Muayene.cs       → Muayene yönetimi + LINQ raporları
│   ├── Form_Urun.cs          → Ürün yönetimi
│   └── Form_Satis.cs         → Satış yönetimi + LINQ raporları
│
└── Yapılandırma
    ├── App.config            → ConnectionString
    └── packages.config       → NuGet paketleri
```

---

## ⚙️ Kurulum

### Gereksinimler
- Windows 10 veya üzeri
- Visual Studio 2019 / 2022
- SQL Server Express
- .NET Framework 4.7.2

### Adımlar

**1. Repoyu klonlayın:**
```bash
git clone https://github.com/SareBayraktar/Veteriner_Otomasyonu.git
```

**2. SQL Server'da veritabanını oluşturun:**
```sql
CREATE DATABASE Veteriner_Otomasyonu;
USE Veteriner_Otomasyonu;

CREATE TABLE Tablo_Sahip (
    Sahip_Id      INT PRIMARY KEY IDENTITY(1,1),
    Sahip_Adi     NVARCHAR(50) NOT NULL,
    Sahip_Soyadi  NVARCHAR(50) NOT NULL,
    Sahip_Telefon NVARCHAR(20) NOT NULL,
    Sahip_Email   NVARCHAR(50) NOT NULL
);

CREATE TABLE Tablo_Veteriner (
    Veteriner_Id       INT PRIMARY KEY IDENTITY(1,1),
    Veteriner_Adi      NVARCHAR(50) NOT NULL,
    Veteriner_Soyadi   NVARCHAR(50) NOT NULL,
    Veteriner_Uzmanlik NVARCHAR(50) NOT NULL
);

CREATE TABLE Tablo_Hayvan (
    Hayvan_Id    INT PRIMARY KEY IDENTITY(1,1),
    Hayvan_Adi   NVARCHAR(50) NOT NULL,
    Hayvan_Turu  NVARCHAR(30) NOT NULL,
    Hayvan_Cinsi NVARCHAR(50) NOT NULL,
    Sahip_Id     INT NOT NULL,
    FOREIGN KEY (Sahip_Id) REFERENCES Tablo_Sahip(Sahip_Id)
);

CREATE TABLE Tablo_Muayene (
    Muayene_Id     INT PRIMARY KEY IDENTITY(1,1),
    Muayene_Tarihi DATE NOT NULL,
    Teshis         NVARCHAR(200) NOT NULL,
    Tedavi         NVARCHAR(200) NOT NULL,
    Muayene_Ucreti DECIMAL(10,2) NOT NULL,
    Hayvan_Id      INT NOT NULL,
    Veteriner_Id   INT NOT NULL,
    FOREIGN KEY (Hayvan_Id)    REFERENCES Tablo_Hayvan(Hayvan_Id),
    FOREIGN KEY (Veteriner_Id) REFERENCES Tablo_Veteriner(Veteriner_Id)
);

CREATE TABLE Tablo_Urun (
    Urun_Id      INT PRIMARY KEY IDENTITY(1,1),
    Urun_Adi     NVARCHAR(100) NOT NULL,
    Urun_Turu    NVARCHAR(50) NOT NULL,
    Urun_Fiyati  DECIMAL(10,2) NOT NULL,
    Stok_Miktari INT NOT NULL
);

CREATE TABLE Tablo_Satis (
    Satis_Id     INT PRIMARY KEY IDENTITY(1,1),
    Satis_Tarihi DATE NOT NULL,
    Miktar       INT NOT NULL,
    Toplam_Tutar DECIMAL(10,2) NOT NULL,
    Urun_Id      INT NOT NULL,
    Sahip_Id     INT NOT NULL,
    FOREIGN KEY (Urun_Id)  REFERENCES Tablo_Urun(Urun_Id),
    FOREIGN KEY (Sahip_Id) REFERENCES Tablo_Sahip(Sahip_Id)
);
```

**3. App.config dosyasında server adınızı güncelleyin:**
```xml
<connectionStrings>
  <add name="VeterinerDbContext"
       connectionString="Data Source=SUNUCU_ADINIZ\SQLEXPRESS;
                         Initial Catalog=Veteriner_Otomasyonu;
                         Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**4. Visual Studio'da projeyi açın ve çalıştırın.**

---

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Versiyon | Kullanım Amacı |
|-----------|----------|----------------|
| **C#** | .NET Framework 4.7.2 | Ana programlama dili |
| **Windows Forms** | — | Masaüstü arayüz |
| **Entity Framework** | 6.5.2 | ORM — Database First yaklaşımı |
| **SQL Server Express** | 16.0 | Veritabanı yönetim sistemi |
| **LINQ** | — | Veri sorgulama ve raporlama |
| **NuGet** | — | Paket yönetimi |

---

## 📊 LINQ Sorguları

### Muayene Raporları

```csharp
// En çok muayene edilen hayvan türü
db.Muayeneler
    .GroupBy(m => m.Hayvan.Hayvan_Turu)
    .Select(g => new { HayvanTuru = g.Key, MuayeneSayisi = g.Count() })
    .OrderByDescending(x => x.MuayeneSayisi)

// Bu ayki muayeneler
db.Muayeneler
    .Where(m => m.Muayene_Tarihi.Month == buAy && m.Muayene_Tarihi.Year == buYil)

// Toplam ve aylık gelir
db.Muayeneler.Select(m => (decimal?)m.Muayene_Ucreti).Sum()
```

### Satış Raporları

```csharp
// En çok satan ürün
db.Satislar
    .GroupBy(s => s.Urun.Urun_Adi)
    .Select(g => new { UrunAdi = g.Key, ToplamSatis = g.Sum(s => s.Miktar) })
    .OrderByDescending(x => x.ToplamSatis)

// Sahibin toplam harcaması
db.Satislar
    .GroupBy(s => new { s.Sahip.Sahip_Adi, s.Sahip.Sahip_Soyadi })
    .Select(g => new {
        SahibiAdi = g.Key.Sahip_Adi + " " + g.Key.Sahip_Soyadi,
        ToplamHarcama = g.Sum(s => s.Toplam_Tutar)
    })
    .OrderByDescending(x => x.ToplamHarcama)
```

---

## 🔗 UML Diyagramı

```
┌─────────────────────┐          ┌──────────────────────┐
│        Sahip        │          │      Veteriner       │
├─────────────────────┤          ├──────────────────────┤
│ + Sahip_Id (PK)     │          │ + Veteriner_Id (PK)  │
│ + Sahip_Adi         │          │ + Veteriner_Adi      │
│ + Sahip_Soyadi      │          │ + Veteriner_Soyadi   │
│ + Sahip_Telefon     │          │ + Veteriner_Uzmanlik │
│ + Sahip_Email       │          └──────────┬───────────┘
│ + Hayvanlar [ ]     │                     │ 1
│ + Satislar  [ ]     │                     │
└──────────┬──────────┘                     │ *
           │ 1              ┌───────────────┴───────────┐
           │                │          Muayene          │
           │ *              ├───────────────────────────┤
┌──────────┴──────────┐     │ + Muayene_Id (PK)         │
│        Hayvan       │ 1 * │ + Muayene_Tarihi          │
├─────────────────────┤─────┤ + Teshis                  │
│ + Hayvan_Id (PK)    │     │ + Tedavi                  │
│ + Hayvan_Adi        │     │ + Muayene_Ucreti          │
│ + Hayvan_Turu       │     │ + Hayvan_Id (FK)          │
│ + Hayvan_Cinsi      │     │ + Veteriner_Id (FK)       │
│ + Sahip_Id (FK)     │     └───────────────────────────┘
│ + Muayeneler [ ]    │
└─────────────────────┘

┌─────────────────────┐          ┌──────────────────────┐
│        Urun         │          │        Satis         │
├─────────────────────┤          ├──────────────────────┤
│ + Urun_Id (PK)      │ 1      * │ + Satis_Id (PK)      │
│ + Urun_Adi          ├──────────┤ + Satis_Tarihi       │
│ + Urun_Turu         │          │ + Miktar             │
│ + Urun_Fiyati       │          │ + Toplam_Tutar       │
│ + Stok_Miktari      │          │ + Urun_Id (FK)       │
│ + Satislar [ ]      │          │ + Sahip_Id (FK)      │
└─────────────────────┘          └──────────────────────┘
                                            │ *
                                            │
                                 ┌──────────┴───────────┐
                                 │        Sahip         │
                                 │    (yukarıda)        │
                                 └──────────────────────┘
```

---

*C# Windows Forms · Entity Framework 6 (Database First) · SQL Server Express*

[![GitHub](https://img.shields.io/badge/GitHub-SareBayraktar-black?style=for-the-badge&logo=github)](https://github.com/SareBayraktar/Veteriner_Otomasyonu)
