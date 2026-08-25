                                                                TÜRKÇE ANLATIM

Bu repoda paylaştığım proje, eğitim süreci boyunca edindiğim tüm bilgi ve pratikleri birleştirdiğim en kapsamlı çalışmamdır. Bu süreçte odaklandığım ve uyguladığım temel başlıklar şunlardır:

Katmanlı Mimari (Multi-Layered Architecture)

Interface ve Servislerin doğru kullanımı

CORS ve SOLID prensipleri

EF Core ile Veri Tabanı yönetimi ve tasarımı (Yerelden canlıya (production) taşıma süreçleri dahil)

Angular, TypeScript, HTML ve SCSS entegrasyonu ile C# tarafında güvenlik protokolleri

Docker ile konteynerizasyon ve sergileme

Her projenin detayını ayrı ayrı yazamasam da, eğitim dışı geliştirdiğim bu en kapsamlı işimde yukarıdaki maddelerin tamamını aktif olarak kullandım.

PROJE ADI: SKYLİNE PAYROLL

Bu proje, bir şirket otomasyonunun prototipi niteliğindedir. Sistem içerisinde yer alan panel ve işlevler şunlardır:

Ana Sayfa: Oturum açma/kapatma, mesaj servisini barındıran bir zil ikonu, logolar ve yan paneller.

Yönetim Paneli: İşten çıkarma (kovulma), terfi ve yönetici istifası işlemlerini barındırır.

Personel Paneli: İstifa süreci ve personelin işten çıkarılma durumunda onay vermesini sağlayan onaylama sistemi.

Muhasebe Panelleri:

Aylık maaş ödeme sistemi (Ödeme ayın 1'inden gecikirse otomatik özür mesajıyla birlikte herkese bildirim gönderir).

İşten çıkarılma sürecindeki son adım olan tazminat hesaplama ve ödeme işlemleri.

İnsan Kaynakları Paneli: Yönetimden gelen talimat doğrultusunda hesaplamaları yaparak personele duyuran panel.

Giriş / Çıkış ve Yetkilendirme Panelleri: İşe alım sistemi (Sadece İK ve Yönetim departmanları tarafından gerçekleştirilebilir), şifre değiştirme (mail ile doğrulama süreci) ve oturum kontrolü (Bu işlemler yapılmadan diğer paneller görüntülenemez).

Genel Bakış Paneli: Mevcut personellerin ve durumlarının listelendiği ekran (Aktif, Pasif, İbra Sürecinde, İstifa Sürecinde).

Mesaj Servisi: Manuel mesaj yazma yerine, sistem içerisinde bir işlem gerçekleştirildiğinde ilgili kişi veya departmana otomatik bildirim gönderen yapı. Okundu olarak işaretleme veya silme özellikleri bulunur.

                                                                ENGLSİH EXPRESSİON

                                                                
This repository showcases my most comprehensive project, combining all the knowledge and hands-on practices I acquired throughout my software development journey. The core concepts and technologies I focused on and implemented include:

Multi-Layered Architecture

Proper usage of Interfaces and Services

CORS and SOLID principles

Database management and design using EF Core (including deployment from local to production environments)

Angular, TypeScript, HTML, and SCSS integration alongside C# security protocols

Containerization and deployment using Docker

While I cannot document every single project individually, this non-educational, standalone project represents my most detailed and comprehensive work, actively incorporating all the items listed above.

PROJECT NAME: SKYLINE PAYROLL

This project serves as a comprehensive corporate automation prototype. The panels and core functions included in the system are:

Home Page: Login/Logout functionality, a notification bell icon integrating the messaging service, logos, and side navigation panels.

Management Panel: Handles employee termination, promotions, and managerial resignation processes.

Employee Panel: Manages resignation requests and includes an approval workflow for employees facing termination.

Accounting Panels:

Monthly salary disbursement system (automatically sends an apology notification to all staff if payments are delayed past the 1st of the month).

Severance payment processing, which is the final step in termination workflows.

Human Resources Panel: Calculates and announces financial details to employees based on management directives.

Authentication & Authorization Panels: Recruitment system (restricted to HR and Management departments), password reset via email verification, and session management (unauthorized users cannot access other panels without logging in).

Overview Panel: Displays current personnel and their active statuses (Active, Inactive, Under Release/İbra, Resigning).

Messaging Service: An automated event-driven notification system rather than a manual messenger. It triggers notifications to relevant personnel or departments upon specific system actions, with options to mark messages as read or delete them.
