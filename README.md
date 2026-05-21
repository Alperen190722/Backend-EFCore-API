Evet bu burada yayınladığım son proje deposu bir çok proje yaptım eğitimlerle bu süreçte öğrendiklerim :
- Katmanlı Mimari
- Interfaceler ve Servicelerin doğru kullanımı
- CORS ve SOLID prensipleri 
- EF CORE ile Veri Tabanı oluşturma
- Doğru ve iyi Veri Tabanı tasarlama hatta en son yaptığım işlem veri tabanını yerelden canlıya kullanıma açma
- Anuglar TypeScript, HTML, SCSS bunların beraber kullanımı ve aynı zamanda C# la beraber güvenlik protokolleri
- En son da Docker de sergileme

Ve burada her ne kadar her projeyle ilgili bir şeyler yazamayacak olsam da son projem eğitim dışı ve en detaylı ve kapsamlı işim oldu yukarı da saydıklarımın da 
hepsini bir nevi kullanmış oldum 

PROJE ADI : SKYLİNE PAYROLL

Aslında bir şirket otomasyonu prototipi denebilir evet bunlarda panel içinde olan işlevler :

1.Ana Sayfa : Oturum Aç/Kapa,Mesaj Servisini barındıran bir zil ikonu 2 tane logo ve yan tarafta paneller
2.Yönetim Paneli : Kovulma,Terfi ve Yönetici istifasından oluşmaktadır
3.Personel Paneli : İstifa ve eğer personel kovulma sürecinde ise personel onayı için gelen oanylama sistemi
4.Muhsabe Panelleri : Biri aylık maaş ödenmesi (Eğer ayın 1'i gecikirse özür mesajıyla beraber herkese haber veriyor) diğer kısım ise kovulma sürecinde son işlem olan tazminat için
5.İnsan Kaynakları Paneli : Kovulma esnasında Yönetimden gelen emirle hesapları yaparak personele duyurur
6.Giriş/Çıkış için Paneller : İşe alım sistemi ama önce giriş yapılmalı çünkü sadece İnsan Kaynakları ve Yönetim departmanı yapabilir, Şifre değiştirme bu klasiktir zaten Mail gönderilir ve yeni şifre yazılır Oturum Açma/Kapama bu işlemler yapılmadan diğer paneller görüntülenemez
7.Genel Bakış Paneli : Burada sadece mevcut personeller ve durumları gösterilir : Aktif,Pasif,İbra Sürecinde,İstifa Sürecinde
8.Mesaj Servisi : Bu bir panel değil ama çlışma mantığını açıklamak istedim mesaj gönderme özel olarak yazılarak değil işlem yapıldığı zaman gerekli kişi yada departmana gönderilerek çalışır ve eğer istenirse mesaj okundu olarak yada direkt silinebilir.

Not : "Docker yüklü olması yeterli, docker-compose up yazın çalışsın"
