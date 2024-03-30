# Zadania / pomysły do dodania

1.  [x] Guziki "Wybierz plik" oraz "Zapisz dane" to powinien być jeden guzik - wybór pliku automatycznie przelicza dane.
2.  [x] Dodać przetwarzanie konfiguracji na "ludzki" zapis, tj. opis zrozumiały dla użytkownika.
3.  [ ] Dodać możliwość zapisu wyników do pliku .xlsx. Na drugim arkuszu powinna być napisana konfiguracja programu.
4.  [ ] Upewnić się, czy powrót po określonym czasie w Powrotach jest odporny na format daty.
5.  [ ] POPULATION_SIZE w wartościach statystycznych powinno być na podstawie wyłącznie wartości niepustych.
6.  [ ] Wartość średnia i odchylenie standardowe powinny również dodawać różnicę wartości średniej i wartości obserwacji osobnika (jeśli osobnik ma mniejszą wartość niż średnia, to różnica jest ujemna). Różnica powinna być przedstawiona jako realna wartość oraz jako liczba odchyleń standardowych.
7.  [ ] Dla histogramu chcielibyśmy również informację o liczbie osobników mniejszych oraz większych, zakładając że nasz mierzony osobnik jest dokładnie po środku. Np. osobnik powracający miał wartość otłuszczenia 2, 3 osobników z populacji miało 1, 5 osobników miały 2, 3 osobniki miały 3, to wtedy wynik jest: 6 osobników niżej (czyli 6/12, 0.5) oraz 6 osobników wyżej (czyli też 0.5). Chcielibyśmy potem taką listę wartości ile było mniejszych od, i jaka była populacja.
8.  [ ] Dodać dokładne tłumaczenia na temat znaczenia wartości statystycznych.
9.  [ ] Dodać przeglądanie dostępnych gatunków w zakładce Osobniki
10. [ ] Dodać abstrakcję ParametersBrowser do zakładki Zapis wyników.
11. [ ] Upewnić się, że dane powrotów dotyczą tylko i wyłącznie pierwszej obserwacji osobnika
12. [ ] Dodać możliwość importowania danych z pliku xlsx. Użytkownik powinien być w stanie wybrać od którego wiersza zaczynają się dane, jakie kolumny zawierają dane, oraz jakie są ich nazwy. Powinien być w stanie również pokazać, gdzie kończą się dane.
13. [ ] Zmienić formę ładowania danych na proste ładowanie pliku i automatyczną decyzję programu, jaki rodzaj loadera wybrać dla tych danych.
14. [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
15. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
16. [ ] DatabaseConditionType powinien spełniać regułę OCP.
17. [ ] W Warunkach Powrotów, w "Czy powrót nastąpił po określonej dacie kolejnego roku", należy ustawić nazwy miesiąca w miejsce numerów.
18. [ ] Przyciski "Przetwarzaj dane" oraz "Przerwij" powinny być połączone w jeden przycisk.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.