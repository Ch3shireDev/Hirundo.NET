# Zadania / pomysły do dodania

1.  [ ] Dodać klauzulę OR w Powrotach.
2.  [ ] Dodać przetwarzanie konfiguracji na "ludzki" zapis, tj. opis zrozumiały dla użytkownika.
3.  [ ] Guziki "Wybierz plik" oraz "Zapisz dane" to powinien być jeden guzik - wybór pliku automatycznie przelicza dane.
4.  [ ] Dodać możliwość zapisu wyników do pliku .xlsx. Na drugim arkuszu powinna być napisana konfiguracja programu.
5.  [ ] Upewnić się, czy powrót po określonym czasie w Powrotach jest odporny na format daty.
6.  [ ] POPULATION_SIZE w wartościach statystycznych powinno być na podstawie wyłącznie wartości niepustych.
7.  [ ] Wartość średnia i odchylenie standardowe powinny również dodawać różnicę wartości średniej i wartości obserwacji osobnika (jeśli osobnik ma mniejszą wartość niż średnia, to różnica jest ujemna). Różnica powinna być przedstawiona jako realna wartość oraz jako liczba odchyleń standardowych.
8.  [ ] Dla histogramu chcielibyśmy również informację o liczbie osobników mniejszych oraz większych, zakładając że nasz mierzony osobnik jest dokładnie po środku. Np. osobnik powracający miał wartość otłuszczenia 2, 3 osobników z populacji miało 1, 5 osobników miały 2, 3 osobniki miały 3, to wtedy wynik jest: 6 osobników niżej (czyli 6/12, 0.5) oraz 6 osobników wyżej (czyli też 0.5). Chcielibyśmy potem taką listę wartości ile było mniejszych od, i jaka była populacja.
9.  [ ] Dodać dokładne tłumaczenia na temat znaczenia wartości statystycznych.
10. [ ] Dodać przeglądanie dostępnych gatunków w zakładce Osobniki
11. [ ] Dodać abstrakcję ParametersBrowser do zakładki Zapis wyników.
12. [ ] Upewnić się, że dane powrotów dotyczą tylko i wyłącznie pierwszej obserwacji osobnika
13. [ ] Dodać możliwość importowania danych z pliku xlsx. Użytkownik powinien być w stanie wybrać od którego wiersza zaczynają się dane, jakie kolumny zawierają dane, oraz jakie są ich nazwy. Powinien być w stanie również pokazać, gdzie kończą się dane.
14. [ ] Zmienić formę ładowania danych na proste ładowanie pliku i automatyczną decyzję programu, jaki rodzaj loadera wybrać dla tych danych.
15. [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
16. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
17. [ ] DatabaseConditionType powinien spełniać regułę OCP.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.