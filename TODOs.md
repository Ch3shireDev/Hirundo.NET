# Zadania / pomysły do dodania

1. [x] Dodać filtr obserwacji "czy wartość należy do zbioru".
2. [x] Dodać filtr obserwacji "czy wartość nie jest pusta".
3. [x] Dodać filtr powrotów "Wartość jest równa".
4. [x] Dodać filtr powrotów "Wartość należy do zbioru".
5. [x] Dodać pole statystyczne "histogram" (dla fatness).
6. [x] Dodać IsInSeasonCondition do Obserwacji.
7. [x] Dodać nową kategorię danych dodawania parametrów dla obserwacji.
8. [x] Dodać dodawanie parametru "symmetry".
9. [x] Dodać dodawanie parametru "pointedness".
10. [x] Należy dodać możliwość wyboru ścieżki pliku wynikowego w zapisie wyników. 
11. [x] Przy zapisie danych statystycznych do pliku wynikowego należy dodać automatyczne zapisywanie konfiguracji do pliku o tej samej nazwie (z rozszerzeniem `.json`).
12. [ ] Zapis danych statystycznych do CSV powinien pozwalać na dodawanie informacji na temat populacji, pustych wartości oraz wartości odstających.
13. [ ] Operacja statystyczna AVERAGE powinna mieć wartość `ResultValueNamePrefix` zamiast osobnych nazw dla average i sd. Domyślną nazwą dla `ResultValueNamePrefix` powinna być nazwa bieżącej kolumny.
14. [ ] Dodać polskie opisy do warunków dla źródeł danych.
15. [ ] Dodać przeglądanie dostępnych gatunków w zakładce "Osobniki".
16. [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
17. [ ] Dodać przeglądanie dostępnych gatunków w zakładce "Osobniki".
18. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
19. [ ] Sprawdzić, czy w filtrze IsEqual typ wartości jest automatycznie pobierany z typu kolumny.
20. [ ] Przenieść RemoveCommand, Remove oraz Remove event do abstrakcji.
21. [ ] Przerzucić Label do klasy LabelsComboBox.
22. [ ] Wewnątrz projektu `Databases` ustandaryzować strukturę do `ParametersBrowser`.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.