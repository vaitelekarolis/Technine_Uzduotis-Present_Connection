# Technical task for Present Connection
## For Junior Developer Position

### The task in Lithuanian:

#### Techninė užduotis:

Yra klientas(užsakovas) ir paslaugų tiekėjas.

* Klientas gali būti fizinis arba juridinis asmuo
* Paslaugų tiekėjas yra juridinis asmuo

Sistemos tikslas - teisingai išrašyti sąskaitą faktūrą. Sąskaitą išrašo paslaugų tiekėjas savo klientui.


Kai paslaugų tiekėjas nėra PVM mokėtojas - PVM mokestis nuo užsakymo sumos nėra skaičiuojamas.


Kai paslaugų tiekėjas yra PVM mokėtojas, o klientas :

* Už EU (Europos sąjungos) ribų - PVM taikomas 0%
* gyvena EU, yra ne PVM mokėtojas, bet gyvena skirtingoje šalyse nei paslaugų tiekėjas. Taikomas PVM x%, kur x - toje šalyje taikomas PVM procentas, pvz.: Lietuva 21 % PVM
* gyvena EU, yra PVM mokėtojas, , bet gyvena skirtingoje šalyse nei paslaugų tiekėjas. Taikomas 0% pagal atvirkštinį apmokęstinimą.
* kai užsakovas ir paslaugų tiekėjas gyvena toje pačioje šalyje - visada taikomas PVM 

 
Užduotis nereikalauja vartotojo sąsajos ! Reikalingi parašyti testai (unit tests) xUnit, NUnit arba MSTest pagalba. Mock’ams naudoti nsubstitute http://nsubstitute.github.io/

 

Platforma - .Net Core.

## Readme

The task is done as an API.
To test it, Swagger UI is integrated. On start the Swagger should open up.
If it does not, just open browser and enter http://localhost:5000/swagger.

Also you can use API testing tool, for example, Postman.
The query example is as follows:
http://localhost:5000/api/Invoice?Client.Juridical=false&Client.Name=Klientas&Client.TaxPayer=false&Client.Country=Lithuania&Provider.Name=Present%20Connection&Provider.TaxPayer=true&Provider.Country=Lithuania&Price=394

We test the tax calculating method to see that all cases are calculated correctly.