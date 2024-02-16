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
10. [ ] Należy dodać możliwość wyboru ścieżki pliku wynikowego w zapisie wyników. 
11. [ ] Dodać przeglądanie dostępnych gatunków w zakładce "Osobniki".
12. [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
13. [ ] Dodać przeglądanie dostępnych gatunków w zakładce "Osobniki".
14. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
15. [ ] Dodać polskie opisy do warunków dla źródeł danych.
16. [ ] Sprawdzić, czy w filtrze IsEqual typ wartości jest automatycznie pobierany z typu kolumny.
17. [ ] Przenieść RemoveCommand, Remove oraz Remove event do abstrakcji.
18. [ ] Zapis danych statystycznych do CSV powinien pozwalać na dodawanie informacji na temat populacji, pustych wartości oraz wartości odstających.
19. [ ] Operacja statystyczna AVERAGE powinna mieć wartość `ResultValueNamePrefix` zamiast osobnych nazw dla average i sd. Domyślną nazwą dla `ResultValueNamePrefix` powinna być nazwa bieżącej kolumny.
20. [ ] Przerzucić Label do klasy LabelsComboBox.
21. [ ] Wewnątrz projektu `Databases` ustandaryzować strukturę do `ParametersBrowser`.
22. [ ] Przy zapisie danych statystycznych do pliku wynikowego należy dodać automatyczne zapisywanie konfiguracji do pliku o tej samej nazwie (z rozszerzeniem `.json`).

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.