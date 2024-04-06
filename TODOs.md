# Zadania / pomysły do dodania

1.  [x] W Warunkach Powrotów, w "Czy powrót nastąpił po określonej dacie kolejnego roku", należy ustawić nazwy miesiąca w miejsce numerów.
2.  [ ] Podawać drugą datę, tj. datę ostatniego powrotu.
3.  [ ] POPULATION_SIZE w wartościach statystycznych powinno być na podstawie wyłącznie wartości niepustych.
4.  [ ] Wartość średnia i odchylenie standardowe powinny również dodawać różnicę wartości średniej i wartości obserwacji osobnika (jeśli osobnik ma mniejszą wartość niż średnia, to różnica jest ujemna). Różnica powinna być przedstawiona jako realna wartość oraz jako liczba odchyleń standardowych.
5.  [ ] Dla histogramu chcielibyśmy również informację o liczbie osobników mniejszych oraz większych, zakładając że nasz mierzony osobnik jest dokładnie pośrodku. Np. osobnik powracający miał wartość otłuszczenia 2, 3 osobników z populacji miało 1, 5 osobników miały 2, 3 osobniki miały 3, to wtedy wynik jest: 6 osobników niżej (czyli 6/12, 0.5) oraz 6 osobników wyżej (czyli też 0.5). Chcielibyśmy potem taką listę wartości ile było mniejszych od, i jaka była populacja.
6.  [ ] Dodać dokładne tłumaczenia na temat znaczenia wartości statystycznych.
7.  [ ] Dodać przeglądanie dostępnych gatunków w zakładce Osobniki.
8.  [ ] Dodać abstrakcję ParametersBrowser do zakładki Zapis wyników.
9.  [ ] Upewnić się, że dane powrotów dotyczą tylko i wyłącznie pierwszej obserwacji osobnika
10. [ ] Dodać możliwość importowania danych z pliku xlsx. Użytkownik powinien być w stanie wybrać od którego wiersza zaczynają się dane, jakie kolumny zawierają dane, oraz jakie są ich nazwy. Powinien być w stanie również pokazać, gdzie kończą się dane.
11. [ ] Zmienić formę ładowania danych na proste ładowanie pliku i automatyczną decyzję programu, jaki rodzaj loadera wybrać dla tych danych.
12. [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
13. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
14. [ ] DatabaseConditionType powinien spełniać regułę OCP.
15. [ ] Przyciski "Przetwarzaj dane" oraz "Przerwij" powinny być połączone w jeden przycisk.
16. [ ] Parametry SummaryWritersParameters oraz SummaryWriters powinny być zastąpione przez jedną klasę.
17. [ ] Należy dodać funkcjonalność Explainer do opisu wybieranych parametrów wewnątrz interfejsu graficznego.
18. [ ] Należy stworzyć prostszą abstrakcję dla HirundoApp - zamiast zależności od 8 różnych podserwisów, powinien zależeć od maksymalnie 3.
19. [ ] Należy dodawać automatycznie pierwszą nieużywaną kolumnę jako wartość "Kolumna" w "Źródło" dla Access.
20. [ ] Należy dodać tłumaczenie w zakładkach na czym polega dodawanie nowych wartości.
21. [ ] Dodać informację na temat "zatwierdzania" wartości.
22. [ ] Dodać podpowiadajkę wartości przy porównywaniu w Obserwacjach (i innych zakładkach).
23. [ ] Zmienić szablon Parametrów tak, by móc zmienić dodany parametr manipulując przy nagłówku.
24. [ ] Wytłumaczyć lepiej czym są "Czy dane są w sezonie", bieżący opis jest mało deskryptywny.
25. [ ] Dodać wykresy dynamiki dla danego gatunku liczby schwytanych osobników danego dnia oraz procent otłuszczenia 3 lub więcej każdego dnia (dla wielu lat). W Obserwacje. Na tym samym wykresie. Średnia liczba osobników (po latach) schwytanych danego dnia oraz procent osobników mających otłuszczenie 3 lub więcej.
26. [ ] W "Czy dane są w zbiorze" nowo dodane pole z wartością powinno się focusować po dodaniu aby nie trzeba było dodatkowo klikać.
27. [ ] Zapis pliku powinien być ustawiany na Desktop.
28. [ ] Dodać do Populacji wartość "Czy jest równy", chcemy wyróżnić jedynie osobniki które mają Status równy O.
29. [ ] Upewnić się, że Populacja NIE ZAWIERA W SOBIE powracajacego osobnika.
30. [ ] Dodać przy "Przetwarzanie obliczeń" informację o bieżącym stanie, np. ile z osobników zostało właśnie przetworzonych.
31. [ ] Dodać cache'owanie danych z bazy danych jeśli żadna wartość nie została zmieniona.
32. [ ] Sprawdzić, dlaczego dla ERI.RUB ze starej bazy danych nie możemy znaleźć żadnych powrotów.
33. [ ] Zmienić warunek pętli przy outliers na pojedyncze sprawdzanie. Powtarzamy obliczenia tylko raz, jeśli mieliśmy outlierów.
34. [ ] Przy źródłach danych, jeśli dodajemy drugie źródło, to powinniśmy automatycznie proponować nazwy z comboboxa na podstawie pierwszego źródła danych. Dodatkowo, typy również powinny być zależne od pierwszego źródła.
35. [ ] W zasadzie, przy dodawaniu drugiego źródła danych, weź już od razu dodaj całą listę kolumn, tylko z dodatkowymi nazwami kolumn do uzupełnienia.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.
