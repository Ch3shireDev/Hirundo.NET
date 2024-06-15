# Zadania / pomysły do dodania

1.  [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
2.  [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
3.  [ ] DatabaseConditionType powinien spełniać regułę OCP.
4.  [ ] Przyciski "Przetwarzaj dane" oraz "Przerwij" powinny być połączone w jeden przycisk.
5.  [ ] Parametry SummaryWritersParameters oraz SummaryWriters powinny być zastąpione przez jedną klasę.
6.  [ ] Należy dodać funkcjonalność Explainer do opisu wybieranych parametrów wewnątrz interfejsu graficznego.
7.  [ ] Należy stworzyć prostszą abstrakcję dla HirundoApp - zamiast zależności od 8 różnych podserwisów, powinien zależeć od maksymalnie 3.
8.  [ ] Należy dodawać automatycznie pierwszą nieużywaną kolumnę jako wartość "Kolumna" w "Źródło" dla Access.
9.  [ ] Należy dodać tłumaczenie w zakładkach na czym polega dodawanie nowych wartości.
10. [ ] Dodać informację na temat "zatwierdzania" wartości.
11. [ ] Dodać podpowiadajkę wartości przy porównywaniu w Obserwacjach (i innych zakładkach).
12. [ ] Zmienić szablon Parametrów tak, by móc zmienić dodany parametr manipulując przy nagłówku.
13. [ ] Wytłumaczyć lepiej czym są "Czy dane są w sezonie", bieżący opis jest mało deskryptywny.
14. [ ] Dodać wykresy dynamiki dla danego gatunku liczby schwytanych osobników danego dnia oraz procent otłuszczenia 3 lub więcej każdego dnia (dla wielu lat). W Obserwacje. Na tym samym wykresie. Średnia liczba osobników (po latach) schwytanych danego dnia oraz procent osobników mających otłuszczenie 3 lub więcej.
15. [ ] W "Czy dane są w zbiorze" nowo dodane pole z wartością powinno się focusować po dodaniu aby nie trzeba było dodatkowo klikać.
16. [ ] Dodać przy "Przetwarzanie obliczeń" informację o bieżącym stanie, np. ile z osobników zostało właśnie przetworzonych.
17. [ ] Przy źródłach danych, jeśli dodajemy drugie źródło, to powinniśmy automatycznie proponować nazwy z comboboxa na podstawie pierwszego źródła danych. Dodatkowo, typy również powinny być zależne od pierwszego źródła.
18. [ ] W zasadzie, przy dodawaniu drugiego źródła danych, weź już od razu dodaj całą listę kolumn, tylko z dodatkowymi nazwami kolumn do uzupełnienia.
19. [ ] Usunąć nadmiarowe pola RING, DATE oraz SPECIES z plików wynikowych.
20. [ ] Dodać ostrzeżenie w przypadku braku warunków populacji - statystyki są wtedy liczone dla całego zbioru, co trwa bardzo dużo czasu.
21. [ ] Należy poprawić błąd w którym zmiana nazwy kolumny w istniejącym źródłe danych nie jest odzwierciedlana na wartość konfiguracji.
22. [ ] Pobieranie gatunków powinno odbywać się automatycznie.
23. [ ] Zmienić formę ładowania danych na proste ładowanie pliku i automatyczną decyzję programu, jaki rodzaj loadera wybrać dla tych danych.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.
