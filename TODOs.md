# Zadania / pomysły do dodania

1.  [x] Dodać polskie opisy do warunków dla źródeł danych.
2.  [x] Dodać tytuł i opis operacji w Obserwacjach.
3.  [x] Przenieść RemoveCommand, Remove oraz Remove event do abstrakcji.
4.  [x] Przerzucić Label do klasy LabelsComboBox.
5.  [x] Wewnątrz projektu `Databases` ustandaryzować strukturę do `ParametersBrowser`.
6.  [x] Dodać pole tekstowe w IsEqual w Powrotach.
7.  [x] Naprawić błąd związany z brakiem zmieniania Prefiksu wartości wynikowej w Histogramie.
8.  [x] Dodać informację wyjaśniającą działanie progu odrzucania w Wartości średniej w Statystykach. 
9.  [x] Zmienić nazwy w Plik związane z nazwami "Zapisz", "Wczytaj", etc.
10. [x] Dodać operator IsNotEqual dla DatabaseConditionType.
11. [x] Dodać możliwość przerywania działania programu (klawisz przerwij).
12. [x] Uzupełnić przerywanie działania programu dla pozostałych operacji.
13. [x] Rozwiązać problem związany z trzymaniem stanu HirundoApp i wykorzystywanych builderów.
14. [x] Dodać operator IsNotEqual w Obserwacjach.
15. [x] Dodać operator IsGreaterThan w Obserwacjach.
16. [ ] Dodać operator IsLessThan w Obserwacjach.
17. [ ] Dodać operator IsGreaterOrEqual w Obserwacjach.
18. [ ] Dodać operator IsLessOrEqual w Obserwacjach.
19. [ ] Dodać przeglądanie dostępnych gatunków w zakładce Osobniki
20. [ ] W polu osobniki dodać informację o liczbie osobników w bazie danych, maksymalnej liczbie obserwacji przypadającej na osobnika.
21. [ ] Dodać przeglądanie rozkładu wartości w Obserwacje.
22. [ ] Upewnić się, czy powrót po określonym czasie w Powrotach jest odporny na format daty.
23. [ ] Dodać abstrakcję ParametersBrowser do zakładki Zapis wyników.
24. [ ] DatabaseConditionType powinien spełniać regułę OCP.

Powyższa lista cech stanowi wymagania dla kolejnej wersji aplikacji. Użytkownik musi być w stanie utworzyć podstawową konfigurację, w której jest w stanie uzyskać dane statystyczne dla powracających osobników. Użytkownik będzie w stanie wybrać interesujące go obserwacje, pola statystyczne, i określić warunek powrotu na podstawie wybranych kolumn. Wynikiem działania programu będzie plik z danymi w formacie `.csv` bądź `.xlsx` w którym przedstawiane są wybrane osobniki z wybranymi kolumnami i wartościami statystycznymi populacji.