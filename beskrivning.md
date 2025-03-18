# Uppgiftsbeskrivning  

Du har träffat en väldigt pepp investerare som är övertygad om att sälja mat på nätet är en bra grej, och du har fått i uppdrag att ta fram denna nya hemsida! På denna site ska det gå att välja mat från en restaurang och lägga en beställning för att senare få den levererad hem till din dörr. Smidigt! Tänk typ Foodora, DoorDash eller Uber Eats.  

## Projektstruktur  
Projektet kommer att ha två delar:  
1. **En publik hemsida**  
2. **Ett REST API**  

---

## Publik Hemsida  
På den publika hemsidan ska besökare kunna:  
- [x] Se vilka restauranger som finns  
- [x] Se restaurangers menyer  
- [x] Välja en maträtt som ska beställas samt antal  
- [x] Lägga en order på vald maträtt där totalpriset för ordern visas  
  -[x] (summan för maten + leveransavgift + 5% serviceavgift)  
- [x] Se status för ordern och 
[x] hur lång tid det är kvar innan den levereras  

---

## REST API  
Via REST API:et ska det gå att:  
- [ ] Registrera nya restauranger  
- [ ] Lägga till maträtter till restaurangers menyer  
- [ ] Hämta hem en lista på alla ordrar  
- [ ] Hämta hem en lista på ordrar med en specifik status  
  - (exempel: alla ordrar som väntar på ett bud)  
- [ ] Hämta hem en lista på ordrar med en specifik restaurang  
- [ ] Ändra orderstatus på en order  
- [ ] Tilldela en viss order till ett visst bud  

---

## Regler för systemet  
- [ ] En order får bara skapas om den innehåller all information  
  - (Beställd maträtt, namn, telefonnummer och adress)  
- [ ] En order kan bara läggas om restaurangen är öppen och innan "Tid för sista beställning"  
- [ ] En orders status ska sättas enligt följande flöde:  
  1. `received` →  
  2. `confirmed` →  
  3. `courier_accepted` →  
  4. `preparing` →  
  5. `ready_for_pickup` →  
  6. `in_transit` →  
  7. `delivered`  
- [ ] Ett bud kan bara acceptera en order om:  
  - [ ] Inget annat bud har accepterat den redan  
  - [ ] Status är `"confirmed"`

---

## Extra VG-krav  
### Hemsidan  
- [ ] Det ska gå att lägga till flera olika maträtter till en beställning  
- [ ] Det ska gå att avbryta en order  
- [ ] Använd ett externt API för något på hemsidan, exempelvis:  
  - [ ] [TheMealDB API](https://www.themealdb.com/api.php) för att ladda in bilder på maträtter  
  - [ ] SMHI:s API för att lägga på extra leveranskostnad vid dåligt väder  
- [ ] **Rating**  
  - Det ska gå att sätta betyg på en restaurang via en order som är `"delivered"`  
- [ ] **Rekommendationer**  
  - På förstasidan kan en eller flera populära restauranger visas först  
  - På menysidor kan populära rätter visas överst  
- [ ] **Betalning**  
  - Använd det fejkade API:et **Swipe** för att göra kortbetalningar innan en order godkänns  
  - Se separat dokumentation  
- [ ] **Leveransval**  
  - Det ska gå att välja mellan att en order ska **levereras** eller **hämtas upp**  
  - (ingen utkörningskostnad vid upphämtning)  
- [ ] **Pay It Forward!**  
  - När en order betalas kan en extra summa läggas som hamnar i en pott  
  - När en order läggs finns det en chans att den betalas helt eller delvis från den ihopsamlade potten  
  - *(Kräver Swipe-betalningslösningen ovan)*  
- [ ] **Dricks**  
  - Efter att en order mottagits kan användaren välja att betala in dricks till restaurangen eller budet  
  - *(Kräver Swipe-betalningslösningen ovan)*  

---

## REST API  
- [ ] API:et kräver autentisering för att kunna användas (valfri metod)  

---

## Regler för REST API  
- [ ] En order kan bara avbrytas av kunden så länge den inte har kommit in i status `"preparing"`  
- [ ] En order som är satt till att **hämtas upp** kan inte få ett bud tilldelat  
- [ ] En order som inte är betald börjar med status `"awaiting_payment"`  

---

## Systemet  
Hur du väljer att strukturera denna uppgift är upp till dig, men du måste använda **ASP.Net**.  
### Exempel på arkitektur:  
- [ ] **Monolitisk applikation i MVC**  
  - med REST API:et i samma projekt  
- [ ] **Blazor**  
  - med frontend-specifik funktionalitet och ett separat REST API för admin  
- [ ] **Blazor WASM**  
  - som frontend och ett enda REST API som backend  

---

## Databas  
- [ ] Använd **SQLite** i projektet som du committar  
- [ ] Under utveckling kan **InMemory** och **seeding** användas  
- [ ] I den slutliga versionen ska **SQLite** användas  

---

## Tips  
✔ Angrip projektet lugnt och metodiskt! Gör inte allt på en och samma gång.  
✔ Läs kravspecen och analysera vilka objekt du kommer behöva.  
✔ Tänk på tidigare övningar där vi analyserade substantiv och verb i beskrivningarna.  
✔ Det är okej att skapa extra endpoints och klasser utöver de som beskrivs här.  
✔ Se detta som ett kunduppdrag, inte en skoluppgift.  
✔ Ställ frågor, föreslå alternativa lösningar och be om förklaringar om något är oklart.  
✔ Kunden har erfarenhet av datasystem men kan ha otydliga krav – fråga i **Fråge-kanalen på Discord** vid behov.  
✔ **Testning är en del av uppgiften** – läs testningsdokumentationen!  

---

## Bra länkar  
🔗 [GitHub - ardalis/DotNetDataAccessTour](https://github.com/ardalis/DotNetDataAccessTour)  
