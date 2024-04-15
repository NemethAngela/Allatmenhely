# Állatmenhely - vizsgaremek
## Készítette: Námeth Angéla és Király Péter

### A projekt célja

A projekt célja egy olyan webes alkalmazás létrehozása, amelyben egy állatmenhely állatait lehet megnézni és örökbe fogadni. A cél valós igényeken alapul, mert egyre több az elárvult, gazdátlanodott kisállat, aki szerető otthonra vágyik, és szerencsére egyre többen vannak azok, akik szívesen fogadnának örökbe kutyát, macskát, nyulat, ill. egyéb kis kedvencet.

### A projekt beüzemelése

A projekt két fő komponense saját könyvtárban van: „backend” illetve „frontend”, és külön-külön el kell indítani:
#### Backend indítása
A „backend” könyvtárban terminál ablakban ki kell adni a **’dotnet watch’** parancsot. A sikeres indítás után egy swagger felület nyílik meg az alapértelmezett böngészőben.
##### Frontend indítása
A „frontend” könyvtárban első lépésben a függőségeket kell telepíteni, mivel a „node_moduls” könyvtár nem kerül eltárolásra GitHub-on.
A függőséget telepítéséhez az **’npm install’** parancsot kell kiadni terminálból, majd a telepítés sikeres lefutása után el lehet indítani az Angular alapú frontendet az **’ng sereve -o’** paranccsal. A parancs sikeres lefutásával a projekt felülete betöltődik az alapértelmezett böngészőben. Amennyiben nem történne meg az automatikus megnyitása a felületnek, akkor azt a ’http://localhost:4200/’ címen lehet elérni.



