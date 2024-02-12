# Zadania / pomysły do dodania

1. [x] Dodać filtr obserwacji "czy wartość należy do zbioru".
2. [x] Dodać filtr obserwacji "czy wartość nie jest pusta".
3. [x] Dodać filtr powrotów "Wartość jest równa".
4. [x] Dodać filtr powrotów "Wartość należy do zbioru".
5. [x] Dodać pole statystyczne "histogram" (dla fatness).
6. [x] Dodać IsInSeasonCondition do Obserwacji.
7. [ ] Dodać nową kategorię danych dodawania parametrów dla obserwacji.
8. [ ] Dodać dodawanie parametru "pointedness".
9. [ ] Dodać dodawanie parametru "symmetry".
10. [ ] Dodać przeglądanie dostępnych gatunków w zakładce "Osobniki".
11. [ ] W polu osobniki dodać informację o ilości osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
12. [ ] Dodać przeglądanie dostępnych gatunków w zakładce "Osobniki".
13. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
14. [ ] Dodać polskie opisy do warunków dla źródeł danych.
15. [ ] Sprawdzić, czy w filtrze IsEqual typ wartości jest automatycznie pobierany z typu kolumny.
16. [ ] Przenieść RemoveCommand, Remove oraz Remove event do abstrakcji.
17. [ ] Zapis danych statystycznych do CSV powinien pozwalać na dodawanie informacji na temat populacji, pustych wartości oraz wartości odstających.
18. [ ] Operacja statystyczna AVERAGE powinna mieć wartość `ResultValueNamePrefix` zamiast osobnych nazw dla average i sd. Domyślną nazwą dla `ResultValueNamePrefix` powinna być nazwa bieżącej kolumny.
19. [ ] Należy dodać możliwość wyboru ścieżki pliku wynikowego w zapisie wyników. 
20. [ ] Przerzucić Label do klasy LabelsComboBox.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.