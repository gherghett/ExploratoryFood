Nu är där ett API, det kan för tillfället räkna ut ett förväntat pris, som Web kan kalla på för att visa i browsern. I Order.cshtml gör vi api-calls med fallback till js-beräkningar.

kanske nästa grej är
[x] hur lång tid det är kvar innan den levereras 
eftersom detta är del av core

innan testing börjar, osså får test byggas i samband med att api:et expanderar?

----

Det är lite buggigt med pris-kalkylationen när man ska köpa ngt, priset uppdataeras inte nödvändigtvis om man går fram och tillbaka i webbläsaren, detta kommer inte fixas tror jag

Nästa: göra alla obligatoriska API-endpoints.
Authentisering är ett "extra-krav"