[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/b6TD_5zP)
# Matservice

Obligatorisk självständig inlämningsuppgift för **BY-SUVNET OOP2**

## Inlämning
- **Datum:** 10 april
- **Tid:** kl 09.00
- **Kodgenomgång:** Obligatoriskt, sker individuellt. Fokus ligger på frågor såsom:
  - Vad finns det för för- och nackdelar med den projekttyp/arkitektur du valt?
  - Beskriv hur API-funktion X fungerar från början till slut.
  - Beskriv hur systemregel Y är implementerad.
  - **OBS!** Se även kriterierna för att bli godkänd i kursen Testning, längst ner i detta dokument.

---

## Checklista för inlämning
- [ ] Jag har läst instruktionerna noggrant.
- [ ] Jag har ställt frågor om det är något som är otydligt.
- [ ] Jag har tagit bort oanvänd kod.
- [ ] Jag har tagit bort onödiga kommentarer.
- [ ] Jag har formaterat min kod fint.
- [ ] Jag har förstått hur min kod fungerar.  
  *Prova att förklara högt för dig själv vad din kod gör. Fråga dig t.ex.: "Hur går det till när jag lägger en order?" eller "Vad händer om jag försöker göra funktion X?"*
- [ ] Jag har checkat av vilka funktionskrav jag anser att jag klarat genom att skriva ett x innanför [ ] i denna readmefil.

---

## Betygskriterier

### För G
- [ ] Användning av **Asp.Net**, **EF Core** och **SQLite**.
- [ ] Samtliga krav för hemsidan är uppfyllda.
- [ ] Samtliga krav för REST-APIet är uppfyllda.
- [ ] Minst 2 av 4 systemregler implementerade.
- [ ] Checklista för inlämning (se ovan) har genomförts.

### För VG
- Samtliga G-krav ovan, plus:
  - [ ] Alla G-systemregler.
  - [ ] En övergripande bra struktur på koden med motivering kring val under code review.
  - [ ] Minst 7 av 14 extrakrav:
  - [ ] God felhantering av data.
  - [ ] Hantering av eventuella nyinkomna kravspecändringar.

**Viktigt:**  
Se till att kryssa i alla krav du lyckats med. Sätt bara ett x innanför [ ] i denna readmefil så bockas de i.

---

## Uppgiftsbeskrivning

Du har träffat en väldigt peppad investerare som är övertygad om att sälja mat på nätet är en bra idé, och du har fått i uppdrag att ta fram en ny hemsida! På denna site ska det gå att:
- Välja mat från en restaurang.
- Lägga en beställning för att få maten levererad hem till dörren.

*Tänk Foodora, DoorDash eller Uber Eats.*

### Projektets delar
Projektet består av **två delar**:
1. En **publik hemsida**.
2. Ett **REST API**.

---

## Publik Hemsida

Besökare ska kunna:
- [ ] **Se vilka restauranger** som finns.
- [ ] **Se restaurangers menyer**.
- [ ] **Välja en maträtt** som ska beställas samt ange antal.
- [ ] **Lägga en order** på vald maträtt, där totalpriset visas (summan för maten plus leveransavgift plus 5% serviceavgift).
- [ ] **Se status för ordern** och hur lång tid det är kvar innan den levereras.

---

## REST API

API:et ska fungera som ett administrationsverktyg (investeraren har för närvarande inte råd med ett användarvänligt gränssnitt).

Funktioner via REST API:
- [ ] **Registrera** nya restauranger.
- [ ] **Lägga till** maträtter till restaurangers menyer.
- [ ] **Hämta** en lista på alla ordrar.
- [ ] **Hämta** en lista på ordrar med en specifik status (t.ex. alla ordrar som väntar på ett bud) och för en specifik restaurang.
- [ ] **Ändra orderstatus** på en order.
- [ ] **Tilldela** en viss order till ett specifikt bud.

---

## Regler för Systemet

### Order
- [ ] En order får bara skapas om den innehåller all nödvändig information:
  - Beställd maträtt
  - Namn
  - Telefonnummer
  - Adress

- En order kan bara läggas om:
  - [ ] Restaurangen är öppen.
  - [ ] Ordern läggs innan "Tid för sista beställning".

- [ ] **Orderstatus** ska följa följande flöde:
  - `received` → `confirmed` → `courier_accepted` → `preparing` → `ready_for_pickup` → `in_transit` → `delivered`.

### Bud
- Ett bud kan bara acceptera en order om:
  - [ ] Inget annat bud har accepterat den redan.
  - [ ] Orderstatus är `confirmed`.

---

## Extra VG-krav

### Hemsidan:
- [ ] Det ska gå att lägga till **flera maträtter** till en beställning.
- [ ] Det ska vara möjligt att **avbryta en order**.
- [ ] **Externt API:**  
  T.ex. [TheMealDB](https://www.themealdb.com/api.php) för att ladda in bilder på maträtter eller [smhis-api](#) för att lägga på en extra kostnad vid ful-väder.
- [ ] **Rating:** Möjlighet att sätta betyg på en restaurang via en order som är `delivered`.
- **Rekommendationer:**  
  - [ ] Populära restauranger visas på förstasidan.
  - [ ] Populära rätter visas överst på menysidorna.
- [ ] **Betalning:**  
  Använd det fejkade API:et *Swipe* för kortbetalningar innan en order godkänns. Se separat dokumentation.
- [ ] **Leverans eller Upphämtning:**  
  Möjlighet att välja mellan leverans (med utkomstningskostnad) eller att hämta upp ordern.
- [ ] **Pay It Forward:**  
  När en order betalas kan en extra summa läggas till i en gemensam pott. Vid senare beställning kan ordern betalas helt eller delvis från den ihopsamlade potten (kräver Swipe-betalningslösningen).
- [ ] **Dricks:**  
  Efter mottagen order kan användaren välja att ge dricks till restaurangen eller budet (kräver Swipe-betalningslösningen).

### REST API:
- [ ] API:et kräver **autentisering** (valfri metod).

#### Regler för REST API:
- [ ] En order kan endast **avbrytas** av kunden så länge den inte har nått status `preparing`.
- [ ] En order som är satt att **Hämtas Upp** kan inte få ett bud tilldelat.
- [ ] En order som inte är betald börjar med status `awaiting_payment`.

---

## System och Arkitektur

Hur du väljer att strukturera uppgiften är upp till dig, men du måste använda **ASP.Net**. Exempel på arkitekturer:
- **Monolitisk applikation i MVC**, där REST API:et ingår i samma projekt.
- **Blazor** med frontend-specifik funktionalitet och ett separat REST API för admin-delen.
- **Blazor WASM** som frontend med ett separat REST API som backend.

### Databas
- **SQLite** ska användas i projektet som committas.  
  (Under utvecklingen kan du använda InMemory med seeding, men för inlämningen ska SQLite användas.)

---

## Tips

- Angrip projektet **lugnt och metodiskt** – gör inte allt på en gång!
- Analysera kravspecifikationen och identifiera vilka objekt du behöver. Tänk på de tidigare övningarna med att analysera substantiv och verb.
- Det är okej att skapa extra endpoints och klasser utöver de som beskrivs, men ovanstående är **minimumkrav**.
- Se detta som ett **uppdrag från en kund** snarare än en vanlig inlämningsuppgift.  
  Ställ frågor och kom med förslag om du tror att projektet kan förbättras.
- **Testning** är en del av uppgiften – se till att läsa dokumentet för testning!

---

## Bra länkar

- [GitHub - ardalis/DotNetDataAccessTour: A tour of different data access approaches in .NET 8+](https://github.com/ardalis/DotNetDataAccessTour)

---
# Testning

Projektet skall ha ett eller två separata test-projekt som innehåller enhets- och intregrationstester.

## Betygskriterier

### För G
- Gör minst ett enhetstest på något i applikationens kärna
- Testa API-endpointen för att hämta alla ordrar, med hjälp av ett integrationstest

### För VG
- Använd en mock i ett enhetstest
- Skriv ett enhetstest som syftar till att skydda en domäninvariant (protect domain invariants)
- Konfigurera databasen i integrationstestet så att det inte är den riktiga databasen som används
