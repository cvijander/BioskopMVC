# BioskopMVC

**BioskopMVC** je ASP.NET Core MVC veb aplikacija za upravljanje bioskopskim rezervacijama, kao i za dodavanje, menjanje i uklanjanje određenih podataka. Aplikacija sadrži dva tipa korisnika:

## 1. Korisnik (Customer):
- Može da pregleda trenutnu ponudu filmova.
- Može da izlistava filmove prema određenim kriterijumima.
- Može da rezerviše karte.

## 2. Administrator (Admin):
Ima pristup upravljanju sledećim entitetima:
- **Bioskop (Cinema)**
- **Bioskopske sale (CinemaHall)**
- **Raspored sedenja (SeatingArea)**
- **Film(Movie)**
- **Glumac(Actor)**
- **Reditelj(Director)**
- **Osoba(Person)**
- **Nacionalnost(Nationality)**

### Entiteti:
- **Film (Movie)**: Entitet koji povezuje glumca i reditelja 
- **Glumac (Actor)**: Entitet koji predstavlja glumca u filmu.
- **Reditelj (Director)**: Entitet koji predstavlja reditelja filma.
- **Nacionalnost (Nationality)**: Utice na klasu **Person**, koju nasleđuju **Actor**, **Director**, **Admin**, i **Customer**.
- **Osoba (Person)**: Baza za kreiranje drugih entiteta poput **Actor**, **Director**, **Admin**, i **Customer**.
- **Bioskop(Cinema)** - Osnova za kreiranje bioskopa, sadrzi naziv, lokaciju, broj sala, broj sedista
- **BioskopskaSala(CinemaHall)** - Bisokospska sala je slab objekat i vezuje za za bioskop cime omogucava da u Bioskopu mozemo da imamo vise sala koje pustaju u raznim terminima razlicite filmove
- **SedajuciDeo(SeatingArea)** Deo bioskopskih sala na kojima se sedi i zaduzene su za podele koristeci broj sedista, red, naziv.
  

### Administratorske funkcionalnosti:
Administrator može da:
- Kreira, menja i briše entitete povezane sa filmovima (**Movie**, **Actor**, **Director**,**Cinema**, **CinemaHall**, **SeatingArea**,**Person**, **Nationality**).

Aplikacija  je  planirano da bude slicna  kao što je primenjeno na sajtu [Cineplexx](https://www.cineplexx.rs/).


## Pokretanje projekta
### Potrebni alati
- **.NET SDK (verzija X.X ili novija)**
- **SQL Server ili drugi sistem za upravljanje bazama podataka**
- **Entity Framework (opciono, ako se koristi za pristup bazi podataka)**

### Instalacija
1. Klonirajte repozitorijum:

```bash
    git clone https://github.com/cvijander/BioskopMVC.git
```

2. Uđite u direktorijum projekta:
```bash
      cd BioskopMVC
```

3. Restorujte potrebne NuGet pakete:
```bash
      dotnet restore
```

4. Konfigurišite konekcioni string za bazu podataka u appsettings.json fajlu:

```json

    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=DESKTOP-FEP7AFG\\SQLEXPRESS;Database=BioskopDB;Trusted_Connection=True;"
      }
    }
```
5. Pokrenite aplikaciju:

```bash

    dotnet run
```
6. Otvorice Vam se browser sa aplikacijom.


## Trenutni prikaz aplikacije 
![Prikaz aplikacije - Pocetna stranica ](./images/Pocetna1.png)




## Status zadatka 
 - [x] Kreiran osnovni model aplikacije
 - [ ] Da moze da se klijent loginuje na aplikaciju
 - [ ] Svaki korisnik mora da se loginuje i da ne moze da pristupi aplikaciji pre nego sto se loginuje
 - [ ] Registracija je obavezna
 - [ ] Prilikom registracie treba nam Email (da bi se slalo promo materijalo ) ali da se moze logovati preko emaila ili korisnickog imena
 - [ ] Nice to have -> Na login stranici da ima mogucnost da korisnici ne mogu da se loginuju, pre nego sto se nalog validira, a on se validira preko emaila
 - [ ] Svi korisnici moraju da imaju email i verifirkovace svoj nalog preko mejla
 - [ ] Pasword minimalno 8 karaktera, 1 broj i 1 poseban simbol
 - [ ] username mora biti jedna rec
 - [ ] Korisnik bi trebalo da moze da sacuva svoju sesiju do sat vremena (tako da u tom vremenu je ulogovan)
 - [ ] Za registraciju , treba email, username, ime, prezime, sifra , da se ponovi sifra i datum rodjenja.
 - [ ] Ogracnicenje za godine, preko 18 godina
 - [ ] Login info ce da stoji na posebnoj stranici
 - [ ] Na home stranici da se vidi sve izlistani podaci (sve main entitete ) u nekom gridu
 - [ ] 3 proizvoda  u jednom redu ,svi podaci da budu  grupisano u jednu karticu
 - [ ] kao vise stranica koje se refresiju, kao infinite scroll,
 - [ ] treba na neki nacin limitirati kolicinu entiteta koji se vide na home stranici
 - [ ]  Postavljanjem kategorija da dobijemo listu proizvoda
 - [ ]  Da mogu da se sortiraju podaci po proizvoljnom propertiju 
 - [ ]  Da u nasim podacima ima obavezan property Created at Datetime
 - [ ]  Da mogu da se sortiraju podaci po created Date i po imenima, i po nekoj integer ili decimal vrednosti
 - [ ]  nice to have -> Search  tab
 - [ ]  ocenjivanje, komentarisanje  filmova od strane korisnika
 -  

### Nationality

- [x] Kreirana tabela
- [x] Partialview  - lista nacionalnosti
- [x] Create  - kreiranje nove nacionalnosti
- [x] Edit  - modifikovanje postojece
- [x] Delete - brisanje postojece nacionalnosti
- [x] Index - tabela nacionalnosti

### Person

- [x] Kreirana tabela
- [x] Prikaz lista osoba
- [x] Create  - kreiranje nove osobe
- [x] Edit  - modifikovanje postojece osobe 
- [x] Delete - brisanje postojece osobe

- ### Actor

- [x] Kreirana tabela
- [x] Prikaz - postojecih glumaca 
- [ ] Create  - 
- [ ] Edit  - 
- [ ] Delete - 

### Director

- [ ] Kreirana tabela
- [ ] Peikaz  
- [ ] Create  -
- [ ] Edit  - 
- [ ] Delete -

- ### Movie

- [ ] Kreirana tabela
- [ ] Peikaz  
- [ ] Create  -
- [ ] Edit  - 
- [ ] Delete - 

 ### Cinema

- [ ] Kreirana tabela
- [ ] Peikaz  
- [ ] Create  -
- [ ] Edit  - 
- [ ] Delete -

- ### CinemaHall

- [ ] Kreirana tabela
- [ ] Peikaz  
- [ ] Create  -
- [ ] Edit  - 
- [ ] Delete -

- ### SeatingArea

- [ ] Kreirana tabela
- [ ] Peikaz  
- [ ] Create  -
- [ ] Edit  - 
- [ ] Delete -

## Nationality 

- [x] - **Kreirana tabela u bazi** 
![Kreirana tabela ](./images/NacionalistTabela.png)

- [x] - **Lista nacionalnosti**
![Lista nacionalnosti](./images/GetNacionity.png)

- [x] - **Kreiranje nove nacionalnosti**
![Kreiranje nove nacionalnosti](./images/NationalityCreate.png)

- [x] - **Modifikovanje postojece nacionalnosti**
![Modifikovanje postojece nacionalnosti](./images/NationlityEdit.png)

- [x] - **Brisanje postojece nacionalnosti**
![Brisanje postojece nacionalnosti](./images/NationalityDelete.png)      
      
### Person

- [x] - **Kreirana tabela**
![Kreirana tabela](./images/PersonTabela.png)
      
- [x] - **Prikaz lista osoba**
![Prikaz lista osoba](./images/PersonIndex.png)

- [x] - **Create  - kreiranje nove osobe**
![Kreiranje nove osobe](./images/PersonCreate.png)
      
- [x] - **Edit  - modifikovanje postojece osobe**
![Modifikovanje postojece osobe](./images/PersonEdit.png)

- [x] - **Delete - brisanje postojece osobe**
![Brisanje postojece osobe](./images/PersonDelete.png)      

- ### Actor

- [x] Kreirana tabela
- [x] Prikaz - postojecih glumaca 
- [ ] Create  - 
- [ ] Edit  - 
- [ ] Delete -


      
