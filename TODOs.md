# Zadania / pomysły do dodania

1.  [x] Dodać polskie opisy do warunków dla źródeł danych.
2.  [x] Dodać tytuł i opis operacji w Obserwacjach.
3.  [x] Przenieść RemoveCommand, Remove oraz Remove event do abstrakcji.
4.  [x] Przerzucić Label do klasy LabelsComboBox.
5.  [x] Wewnątrz projektu `Databases` ustandaryzować strukturę do `ParametersBrowser`.
6.  [x] Dodać pole tekstowe w IsEqual w Powrotach.
7.  [ ] Naprawić błąd związany z brakiem zmieniania Prefiksu wartości wynikowej w Histogramie.
8.  [ ] Dodać przeglądanie dostępnych gatunków w zakładce Osobniki
9.  [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
10. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
11. [ ] Dodać operator IsNotEqual dla DatabaseConditionType.
12. [ ] Dodać operatory IsNotEqual, IsGreaterThan, IsLessThan, IsGreaterOrEqual, IsLessOrEqual w Obserwacjach.
13. [ ] Upewnić się, czy powrót po określonym czasie w Powrotach jest odporny na format daty.
14. [ ] Dodać informację wyjaśniającą działanie progu odrzucania w Wartości średniej w Statystykach. 
15. [ ] Dodać abstrakcję ParametersBrowser do zakładki Zapis wyników.
16. [ ] Zmienić nazwy w Plik związane z nazwami "Zapisz", "Wczytaj", etc.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.