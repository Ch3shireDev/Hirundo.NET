# Zadania / pomysły do dodania

1.  [ ] Upewnić się, że dane powrotów dotyczą tylko i wyłącznie pierwszej obserwacji osobnika.
2.  [ ] Dodać możliwość importowania danych z pliku xlsx. Użytkownik powinien być w stanie wybrać od którego wiersza zaczynają się dane, jakie kolumny zawierają dane, oraz jakie są ich nazwy. Powinien być w stanie również pokazać, gdzie kończą się dane.
3.  [ ] Zmienić formę ładowania danych na proste ładowanie pliku i automatyczną decyzję programu, jaki rodzaj loadera wybrać dla tych danych.
4.  [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
5.  [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
6.  [ ] DatabaseConditionType powinien spełniać regułę OCP.
7.  [ ] Przyciski "Przetwarzaj dane" oraz "Przerwij" powinny być połączone w jeden przycisk.
8.  [ ] Parametry SummaryWritersParameters oraz SummaryWriters powinny być zastąpione przez jedną klasę.
9.  [ ] Należy dodać funkcjonalność Explainer do opisu wybieranych parametrów wewnątrz interfejsu graficznego.
10. [ ] Należy stworzyć prostszą abstrakcję dla HirundoApp - zamiast zależności od 8 różnych podserwisów, powinien zależeć od maksymalnie 3.
11. [ ] Należy dodawać automatycznie pierwszą nieużywaną kolumnę jako wartość "Kolumna" w "Źródło" dla Access.
12. [ ] Należy dodać tłumaczenie w zakładkach na czym polega dodawanie nowych wartości.
13. [ ] Dodać informację na temat "zatwierdzania" wartości.
14. [ ] Dodać podpowiadajkę wartości przy porównywaniu w Obserwacjach (i innych zakładkach).
15. [ ] Zmienić szablon Parametrów tak, by móc zmienić dodany parametr manipulując przy nagłówku.
16. [ ] Wytłumaczyć lepiej czym są "Czy dane są w sezonie", bieżący opis jest mało deskryptywny.
17. [ ] Dodać wykresy dynamiki dla danego gatunku liczby schwytanych osobników danego dnia oraz procent otłuszczenia 3 lub więcej każdego dnia (dla wielu lat). W Obserwacje. Na tym samym wykresie. Średnia liczba osobników (po latach) schwytanych danego dnia oraz procent osobników mających otłuszczenie 3 lub więcej.
18. [ ] W "Czy dane są w zbiorze" nowo dodane pole z wartością powinno się focusować po dodaniu aby nie trzeba było dodatkowo klikać.
19. [ ] Dodać przy "Przetwarzanie obliczeń" informację o bieżącym stanie, np. ile z osobników zostało właśnie przetworzonych.
20. [ ] Dodać cache'owanie danych z bazy danych jeśli żadna wartość nie została zmieniona.
21. [ ] Przy źródłach danych, jeśli dodajemy drugie źródło, to powinniśmy automatycznie proponować nazwy z comboboxa na podstawie pierwszego źródła danych. Dodatkowo, typy również powinny być zależne od pierwszego źródła.
22. [ ] W zasadzie, przy dodawaniu drugiego źródła danych, weź już od razu dodaj całą listę kolumn, tylko z dodatkowymi nazwami kolumn do uzupełnienia.
23. [ ] Usunąć nadmiarowe pola RING, DATE oraz SPECIES z plików wynikowych.
24. [ ] Dodać ostrzeżenie w przypadku braku warunków populacji - statystyki są wtedy liczone dla całego zbioru, co trwa bardzo dużo czasu.
25. [ ] Należy poprawić błąd w którym zmiana nazwy kolumny w istniejącym źródłe danych nie jest odzwierciedlana na wartość konfiguracji.
26. [ ] Pobieranie gatunków powinno odbywać się automatycznie.
27. [ ] Moduł Explain należy zintegrować z operatorami, poprzez dodanie odpowiedniego interfejsu. Obecność interfejsu ma determinować, czy operator jest czy nie jest tłumaczony.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.
