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
- **Nacionalnost(Nationlity)**

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

Aplikacija funkcioniše po sličnom principu kao što je primenjeno na sajtu [Cineplexx](https://www.cineplexx.rs/).
