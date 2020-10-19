# BUG.Net #9 Spotkane Bydgoszcz .Net User Group                             
Redis - więcej niz cache (.NET Core)

### Uruchomienie Redis w dockerze

~~~
docker run --name bug-net-redis -d -p 6379:6379 redis
~~~

Uruchomienie trybu interaktywnego
~~~
docker exec -it bug-net-redis redis-cli
~~~

Uruchomienie kontenera
~~~ bash
docker start bug-net-redis 
~~~

Zatrzymanie kontenera
~~~ bash
docker stop bug-net-redis
~~~

## Diagnostyka

Sprawdzenie czy Redis odpowiada
~~~
ping
~~~

Śledzenie Redisa
~~~
monitor
~~~


## Funkcje Redis

### Klucze

Dodanie wartości
~~~
SET current:user Marcin
~~~
Kolejne wywołanie nadpisze poprzednią wartość (update).

Jeśli chcemy tego uniknąć należy dodać atrybut NX
~~~
SET current:user Marcin NX
~~~

Pobranie wartości
~~~
GET current:user
~~~


Usunięcie klucza
~~~
DEL current:user
~~~


### TTL

Dodanie wartości na określony czas (TTL)
~~~
set vehicle1 ready ex 120
~~~

Ustawienie czasu życia klucza
~~~
expire vehicle1 60
~~~



Pobranie czasu, który pozostał do wygaśnienia klucza
~~~
ttl vehicle1
~~~

Pobranie wszystkich kluczy wg szablonu
~~~
keys *
~~~


 ### Inkrementacja
 
 Dodanie 1 do klucza
 ~~~
 incr points
 ~~~
 
 Dodanie określonej liczby do klucza
 ~~~
  incrby points 10
 ~~~
 
 Odjęcie 1 od klucza
 ~~~
 decr points
 ~~~
 
 Odjęcie określonej liczby do klucza
 ~~~
 decrby points 10
 ~~~
 
### Operacje bitowe

Ustawienie bitu
~~~
setbit article1:today 50 1
setbit article2:today 50 1
setbit article2:today 50 1
~~~

Pobranie bitu
~~~
getbit article1:today 50
~~~

Operacja AND
~~~
bitop and 50:readboth article1:today article2:today
~~~

Operacja OR
~~~
bitop or 50:readanyarticle article1:today article2:today
~~~

Obliczenie ilości ustawionych bitów na 1
~~~
bitcount result
~~~

### Baza danych

Wybór bazy danych
~~~
select 1
~~~


###  Tablice asocjacyjne (HASH)

Jeśli chcesz zmodyfikować cały obiekt zwykły string wystarczy:

~~~ 
SET users:marcin { 'email': 'marcin.sulecki@gmail.com', speed: 120}
~~~

Natomiast w przypadku, gdy chcesz mieć dostęp do pojedynczych pól lepszym rozwiązaniem będą tablice asosjacyjne.

Dodanie
~~~
HSET users:marcin email marcin.sulecki@gmail.com
HSET users:marcin speed 120
~~~


Pobranie wybranego pola
~~~
HGET users:marcin email
~~~
 

Dodanie wielu wartości
~~~
HMSET users:marcin speed 120 email marcin.sulecki@gmail.com
~~~

Pobranie wszystich pól
~~~
HGETALL users:marcin
~~~

Ustawienie czasu życia
~~~
EXPIRE users:Marcin
~~~
 

### Listy

Wstawianie elementu do listy (na początek)
~~~ 
lpush pages page1
lpush pages page2
lpush pages page3
~~~

Pobranie elementów z listy na podstawie zakresu
~~~
lrange pages 0 3
~~~


Usunięcie i pobranie pierwszego elementu z listy
~~~
lpop pages
~~~

Usunięcie i pobranie ostatniego elementu z listy
~~~
rpop pages
~~~

Dołączenie elementu do listy (na koniec)
~~~ 
lpush orders order1
lpush orders order2
lpush orders order3
~~~


Pobranie elementu z listy na podstawie indeksu
~~~
lindex orders 2
~~~

Ustawienie wartości elementu na liście na podstawie indeksu
~~~
lset orders 2 order10
~~~

Usunięcie określonej ilości elementów z listy począwszy od podanej wartości
~~~
lrem ordered 2 order5 
~~~

Usunięcie ostatniego elementu z listy, dołączenie go do innej listy i zwrócenie

~~~
rpoplpush ordered delivered
~~~

### Zbiory

Dodanie wartości do zbioru
~~~
sadd online user1
sadd online user2
sadd online user3
sadd offline user4
sadd offline user5
~~~

Pobranie elementów zbioru
~~~
smembers online
~~~

Usunięcie elementu ze zbioru
~~~
srem online user1
~~~

Przesunięcie elementu pomiędzy zbiorami
~~~
smove offline online user5
~~~

Suma zbiorów
~~~
sunion online offline
~~~

Część wspólna zbiorów
~~~
sinter online offline
~~~

Różnica zbiorów
~~~
sdiff
~~~

### Posortowane zbiory

Dodanie elementów
~~~
ZADD skills:marcin 100 csharp
ZADD skills:marcin 94 wpf-mvvm
ZADD skills:marcin 2 python
~~~

Pobranie elementów wg rankingu
~~~
ZRANGEBYSCORE skills:marcin 50 100
~~~

### Typy przestrzenne

Dodanie pozycji
~~~
geoadd locations 52.361389 19.115556 Vehicle1
geoadd locations 52.361389 19.115556 Vehicle2
geoadd locations 52.361389 19.115556 Vehicle3
geoadd locations 52.361389 19.115556 Vehicle4
~~~

Pobranie pozycji określonego klucza
~~~
geopos locations Vehicle2
~~~


Obliczenie dystansu pomiędzy dwoma pozycjami
~~~ 
geodist locations Vehicle1 Vehicle4 km
~~~


Wyszukanie pozycji w określonym promieniu
~~~
georadius locations 0 0 200 km
~~~

### Czyszczenie 

Wyczyszczenie wszystkich kluczy ze wszystkich baz danych
~~~
flushall
~~~

Wyczyszczenie wszystkich kluczy z bieżącej bazy danych
~~~
flushdb
~~~


Wyczyszczenie wszystkich kluczy z określonej bazy danych
~~~
-n <database_number> flushdb
~~~


### Pub/Sub

Utworzenie subskrypcji
~~~
subscribe sensors:temp1
~~~

Wysłanie wiadomości
~~~
publish sensors:temp1 54.21
~~~

Usunięcie subskrypcji
~~~
UNSUBSCRIBE
~~~

Utworzenie subskrypcji ze wzorcem 
~~~
psubscribe sensors.temp*
~~~


### Strumień (Stream)
- Pisanie do strumienia
~~~
xadd events * user john action login
xadd events * user john action visit page index.htm
xadd events * user john action purchase item computer
xadd events * user john action purchase item monitor
xadd events * user john action paid amount 1000
~~~

- Czytanie ze strumienia
~~~
xread count 2 streams events 0
xread count 2 streams events 1572983745546-0
~~~

## NET Core

### Przydatne komendy CLI
- ``` dotnet --list-sdks ``` - wyświetlenie listy zainstalowanych SDK
- ``` dotnet --list-runtimes ``` - wyświetlenie listy zainstalowanych środowisk uruchomieniowych
- ``` dotnet new globaljson ``` - utworzenie pliku global.json
- ``` dotnet new globaljson --sdk-version {version} ``` - utworzenie pliku global.json i ustawienie wersji SDK
- ``` dotnet new gitignore ``` - utworzenie pliku gitignore
- ``` dotnet new sln ``` - utworzenie nowego rozwiązania
- ``` dotnet sln {solution.sln} add {project.csproj}``` - dodanie projektu do rozwiązania
- ``` dotnet sln {solution.sln} remove {project.csproj}``` - usunięcie projektu z rozwiązania
- ``` dotnet new {template} ``` - utworzenie nowego projektu na podstawie wybranego szablonu
- ``` dotnet new {template} -o {output} ``` - utworzenie nowego projektu w podanym katalogu
- ``` dotnet add {project.csproj} reference {library.csproj} ``` - dodanie odwołania do biblioteki
- ``` dotnet remove {project.csproj} reference {library.csproj} ``` - usunięcie odwołania do biblioteki

- ``` dotnet restore ``` - pobranie bibliotek nuget na podstawie pliku projektu
- ``` dotnet build ``` - kompilacja projektu

- ``` dotnet run ``` - uruchomienie projektu
- ``` dotnet run {app.dll}``` - uruchomienie aplikacji
- ``` dotnet test ``` - uruchomienie testów jednostkowych
- 
- ``` dotnet run watch``` - uruchomienie projektu w trybie śledzenia zmian
- ``` dotnet test watch``` - uruchomienie testów jednostkowych w trybie śledzenia zmian

- ``` dotnet publish -c Release -r {platform}``` - publikacja aplikacji
- ``` dotnet publish -c Release -r win10-x64``` - publikacja aplikacji dla Windows
- ``` dotnet publish -c Release -r linux-x64``` - publikacja aplikacji dla Linux
- ``` dotnet publish -c Release -r osx-x64``` - publikacja aplikacji dla MacOS


## .NET Core i Redis

### Instalacja biblioteki

~~~ bash
dotnet add package StackExchange.Redis
~~~

### Utworzenie połączenia

~~~ csharp
 ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
 IDatabase db = redis.GetDatabase();
~~~


### Wstrzykiwanie zalezności

- Rejestracja
  
~~~ csharp
public void ConfigureServices(IServiceCollection services)
{
    // dotnet add package StackExchange.Redis
    services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
}
~~~

- Uzycie

~~~ csharp
 public class DbLocationRepository : ILocationRepository
{
    private readonly IConnectionMultiplexer connection;
    private readonly IDatabase db;
    private const string key = "locations";

    public RedisDbLocationRepository(IConnectionMultiplexer connection)
    {
        this.connection = connection;
        this.db = connection.GetDatabase();
    }
}
~~~
