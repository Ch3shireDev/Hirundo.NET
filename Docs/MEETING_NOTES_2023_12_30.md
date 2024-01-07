# Notatki ze spotkania 2023-12-30

Wersja Hirundo PRO ma służyć do znajdywania powracających ptaków.

Założenia:
- więcej filtrów
- możliwość ładowania większej liczby baz danych o różnych formatach, z różnymi strukturami
- użytkownik ma mieć możliwość wskazywania kolumn z których są pobierane konkretne wartości kluczowe i pomiarowe

Ptak (osobnik) powracający - wybieramy warunki na podstawie których określamy kiedy osobnik jest powracający

Ptaki powracające i populacje wybieramy z podzbioru z bazy obserwacji na podstawie zadanych filtrów, wybranych przez użytkownika.

Populacja - wybieramy warunki na podstawie których określamy jakie osobniki należą do populacji. Na podstawie wartości pomiarowych ustalamy wartości statystyczne, np. medianę, modalne, średnie, skośność, etc.

Użytkownik wybiera źródło bazy danych i wybiera kolumny kluczowe, np.

- Obrączkę pobierz z kolumny `RING`
- Datę pobierz z kolumny `DateTime` i wybierz samą datę
- Godzinę pobierz z kolumny `DateTime` i wybierz samą godzinę
- Płeć pobierz z kolumny `SEX`
- Wiek pobierz z kolumny `AGE`, w której są wartości `I`, `J`, `A`.

Użytkownik wybiera kolumny pomiarowe, np.

- Masę pobierz z kolumny `WEIGHT`
- Długość ogona pobierz z kolumny `TAIL`
- Skośność skrzydła pobierz z kolumn `D2`, ... `D8` oraz `Wing` i zastosuj funkcję `Symmetry` 
- Tłuszcz z kolumny `FAT`

Użytkownik ładuje bazę, wybiera z listy kolumny z wartościami odpowiadającymi wartościom kluczowym i pomiarowym.

Możemy zapisywać ustawienia do pliku, aby nie trzeba było za każdym razem wybierać kolumn. Ustawienia są również zapamiętywane i się nie resetują o ile to konieczne, np. jeśli zostały wykonane obliczenia na jednym gatunku, to mogą zostać użyte na następnym gatunku, przy zmianie dat etc.

Użytkownik wybiera kolumny na których ma wykonywać obliczenia dla populacji.

Populacja - wszystkie osobniki spełniające warunki wyboru

Ptak powracający - szczególny rodzaj konkretnego ptaka który został wielokrotnie schwytany w różnych przedziałach czasu + dodatkowe warunki ponownego schwytania.

Populacja - na podstawie ptaka powracającego, warunków ptaka powracającego + rok pierwszego złapania z bazy danych (dla konkretnego gatunku).

Użytkownik wybiera opcję "powtórz dla wszystkich gatunków".

Użytkownik sam wybiera swoje filtry.

Użytkownik może wybrać selekcję po płci i kazać zrobić to samo dla wszystkich płci.

Umawiamy się że stosujemy określenie "Osobnik" (`Specimen`) zamiast "Ptak" (`Bird`), ponieważ program może być używany do innych zwierząt niż ptaki.

Filtr - metoda selekcjonowania obserwacji/osobników, z możliwością łączenia.

Przykłady filtrów:

- od roku X do roku Y, np. od 1960 do 1980
- po sezonie, tj. okresie w każdym roku od daty X do daty Y, np. od 1 sierpnia do 30 września
- gatunek, np. dla `REG.REG`
- płeć, np. dla wszystkich ptaków, dla samców, dla samic, tylko dla nieoznaczonych
- wiek, np. dla `I`, `J`, `A`, lub `I` oraz `J`.
- masa, np. dla `WEIGHT` od 10 do 20 gramów
- czas złapania, np. pomiędzy godziną 9tą a 15stą, albo pomiędzy 23cią a 2gą.

W przypadku, gdy sezon jest ustalany od 1 grudnia do 30 stycznia, to dla osobnika powracającego (np. złapany 10 grudnia 2010) jego okno czasowe jest od 1 grudnia 2010 do 30 stycznia 2011.

Wariant / Iteracja - pojedyncze powtórzenie w pętli

Pętla - obiekt generujący różne warianty / iteracje.

Są kolumny dla których robimy filtry.

Wartości kluczowe - wartości wyróżniające obserwację oraz filtr.

Wartości pomiarowe - wartości, dla których prowadzimy statystykę.

Wartość statystyczna - obliczona na podstawie populacji.

Obserwacja - pojedynczy przypadek złapania/stwierdzenia osobnika, w którym zbierane są jego wartości pomiarowe

Użytkownik wskazuje kolumny/wyrażenia kolumn z bazy danych dla wartości pomiarowych.

Dla każdej z tych wartości wskazujący operacje statystyczne do wykonania na całej populacji, np. mediana, średnia, modalna, odchylenie standardowe, kwartyle.

Druga rzecz dla wartości pomiarowych.

Outlier / obserwacja odstająca

Opcja wykluczania outlierów z populacji.

Metoda wykluczania outlierów:

1. Więcej niż x odchyleń standardowych od średniej
2. Określony % skrajnych wartości

Warunki wyróżniające osobnika powracającego

Zadawanie warunków dla konkretnych analizowanych osobników przeżywających / powracających.

Trzy możliwe kryteria dla przeżywalności.

1. Wskazujemy kolumnę która explicite mówi czy osobnik przeżył
2. Zadajemy warunek ponownej obserwacji w odstępie minimum x dni.
3. Określenie od jakiej minimalnej daty następnej mamy zbierać obserwacje osobników.

Np. załóżmy że mówimy, że zbieramy od minimalnej daty 1 sierpnia. Wtedy obserwacje:

- Pierwsza: 1.09.1960 oraz druga: 1.05.1961 - osobnik jest powracający
- Pierwsza: 1.09.1960 oraz druga: 1.09.1961 - osobnik jest powracający
- Pierwsza: 1.09.1960 oraz druga: 1.05.1962 - osobnik jest powracający
- Pierwsza: 1.09.1960 oraz druga: 1.03.1961 - osobnik nie jest powracający

Umawiamy się że pierwsza obserwacja determinuje wartości osobnika.

Użytkownik zapisuje dane do tabeli w Excelu.

Użytkownik wybiera folder wyjściowy i format pliku wynikowego.

W tabeli wynikowej są wszystkie wartości kluczowe, wartości statystyczne i wartości pomiarowe. Są również liczby określające rozmiary całej populacji.

Użytkownik dostaje również opis wszystkich użytych filtrów i warunków.

Użytkownik może dodawać nowe wartości kluczowe oraz filtry lub pętle do tych warunków.