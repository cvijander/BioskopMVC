BioskopMVC 
BioskopMVC je ASP.NET Core MVC veb aplikacije za upravljanje bioskopskim rezervacijama i menjanjem, dodavanjem i uklanjanjem odredjenih podataka. 
BioskopMVC sadrzi dva usera, jedan korisnik (customer) koji moze da vidi trenutnu ponudu filmova , da izlista prema odredjenim kriterijumima i da rezervise karte. Dok sa druge strane imamo administratora (admin) koji je zaduzen za sledece entitete: Bioskop (Cinema), Bioskopske sale (CinemaHall), 
Sedecideo(SeatingArea)  sa jedne strane, dok sa druge strane imamo Entitet Film (Movie), Glumac(Actor), Reditelj(Director), Nacionalnost(Nationlity) , Osoba (Person). Tako da admin moze da kreira / izmeni / brise sledece entitete  : Nacionalost -> koja utice na Person klasu 
koju nasledjuju Actor, Director, Admin i Customer. Actor i Director predstavljaju delove za kreiranje Movie dok sa druge strane entiteta imamo Cinema, CinemaHall i Seating area koje isto administrator moze da kreira/menja /brise. 

Princip po kojem bi se vodio je -> https://www.cineplexx.rs/ < 

