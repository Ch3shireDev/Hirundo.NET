# Terminy dziedziny

- **Baza obserwacji** (`Observation database`) - zbiór wszystkich obserwacji. Baza obserwacji jest zapisana w bazie danych, która jest ładowana do programu. Baza danych może być zapisana w różnych formatach - np. plik `.csv`, plik `.xlsx`, baza Access, baza SQL, etc.

- **Obserwacja** (`Observation`) - zaobserwowany osobnik w danym miejscu i czasie. Obserwacja charakteryzuje się zestawem cech, które są zapisane w bazie danych. Obserwacja zawsze jest związana z konkretnym osobnikiem. Obserwacja jest związana z zestawem kolumn w bazie danych, które reprezentują wartości kluczowe oraz wartości pomiarowe.

- **Osobnik** (`Specimen`) - zaobserwowane zwierzę oznaczone obrączką z unikalnym numerem. Osobnik charakteryzuje się zestawem cech, które są zapisane w bazie danych. Osobnik może być zaobserwowany wiele razy w różnych miejscach i czasach.

- **Osobnik powracający** (`Returning specimen`) - osobnik, który został zaobserwowany więcej niż raz, oraz został spełniony konkretny zestaw warunków dla danych obserwacji. Warunki są ustalane przez użytkownika.

- **Populacja** (`Population`) - zbiór wszystkich osobników reprezentowanych przez obserwacje spełniające zadany zestaw warunków. Populację dla danego osobnika powracającego ustala się na podstawie zestawu warunków określanych przez użytkownika - np. populacja musi być tego samego gatunku, płci, musi być zaobserwowana w tym samym miejscu, etc.

- **Wartości kluczowe** (`Key values`) - niezmienne wartości charakteryzujące osobnika lub obserwację - np. numer obrączki, płeć, gatunek, etc. Wartości kluczowe są wyznaczane na podstawie kolumn wybieranych przez użytkownika.

- **Wartości pomiarowe** (`Measurement values`) - zmienne wartości charakteryzujące osobnika lub obserwację - np. wiek, masa, długość, etc. Wartości pomiarowe są wyznaczane na podstawie kolumn wybieranych przez użytkownika.

- **Wartości statystyczne** (`Statistical values`) - wartości liczbowe obliczane na podstawie wartości pomiarowych - np. średnia masa, odchylenie standardowe długości, etc. Wartości statystyczne są wyznaczane na podstawie wartości pomiarowych populacji. Operacje statystyczne są wybierane przez użytkownika.

- **Filtr** (`Filter`) - pojedynczy warunek, który musi być spełniony przez obserwację, aby był wliczony w badany podzbiór obserwacji. Filtry są ustalane przez użytkownika i dotyczą wartości kluczowych - np. wybierane są tylko samice, wybierany jest konkretny gatunek, wybierane są obserwacje z lat 1960-1980, etc.

- **Pętla** (`Loop`) - warunek powtórzenia tych samych obliczeń dla różnych podzbiorów bazy obserwacji. Pętle są ustalane przez użytkownika i dotyczą wartości kluczowych - np. te same operacje są wykonywane dla różnych gatunków, płci, etc. Pętle mogą być zagnieżdżone.

- **Iteracja** (`Iteration`) - zestaw warunków określany przez dane powtórzenie pętli. Np. dla filtru "Tylko samice" oraz pętli "Powtórz dla gatunków: jaskółka dymówka, jaskółka oknówka" wariantami są "Tylko samice, jaskółka dymówka" oraz "Tylko samice, jaskółka oknówka". Jeśli pętli jest więcej niż jedna, pojedynczym wariantem jest kombinacja warunków z każdej pętli.

- **Operacja statystyczna** (`Statistical operation`) - pojedyncza operacja statystyczna wykonywana wartościach pomiarowych populacji. Operacje są ustalane przez użytkownika i dotyczą wartości statystycznych - np. obliczana jest średnia wieku, odchylenie standardowe masy, etc.

- **Sezon** (`Season`) powtarzający się corocznie przedział czasu, w którym obserwuje się osobniki. Sezon jest ustalany przez użytkownika i składa się z zakresu dat - np. od 1 sierpnia do 30 września, od 1 grudnia do 31 stycznia, etc.

- **Okno czasowe** (`Time window`) - wyznaczany na podstawie sezonu i roku obserwacji, np. dla sezonu od 1 sierpnia do 30 września i roku 2018 okno czasowe wynosi od 1 sierpnia 2018 do 30 września 2018. Dla obserwacji z 10 grudnia 2010 i sezonu od 1 grudnia do 31 stycznia okno czasowe wynosi od 1 grudnia 2010 do 31 stycznia 2011. Okna czasowe są istotne dla określania populacji dla danego osobnika powracającego.

- **Obserwacja odstająca** (`Outliner`) - obserwacja która nie spełnia warunków populacji, np. różnią się od średniej o więcej niż trzy odchylenia standardowe, należą do 1% najbardziej skrajnych wartości, etc. Obserwacje odstające są wyznaczane na podstawie wartości statystycznych populacji poprzez operacje wykluczające dla danej wartości statystycznej. Operacje wykluczające są ustalane przez użytkownika.

- **Operacja wykluczająca** (`Excluding operation`) - operacja wykluczająca obserwacje odstające z populacji. Operacje wykluczające są ustalane przez użytkownika i dotyczą wartości statystycznych - np. wykluczanie obserwacji odstających na podstawie odchylenia standardowego, wykluczanie 1% najbardziej skrajnych wartości, etc.

- **Warunki wyróżniające** (`Distinguishing conditions`) - warunki, które muszą być spełnione przez osobnika, aby został uznany za osobnika powracającego. Warunki wyróżniające są ustalane przez użytkownika i dotyczą wartości kluczowych - np. osobnik musi być tego samego gatunku, płci, musi być zaobserwowany w tym samym miejscu, dystans pomiędzy obserwacjami musi być nie mniejszy niż pół roku, obserwacja musi być nie wcześniej niż 1 sierpnia następnego roku, etc.

- **Zbiór warunków** (`Conditions set`) - kompletny zestaw wszystkich warunków, filtrów, kolumn etc. pozwalający na całkowite przetworzenie bazy danych, od załadowania aż do zapisu wyników.

- **Wyniki** (`Results`) - zbiór wyników zawierający listę osobników powracających wraz z wartościami kluczowymi, wartościami pomiarowymi oraz wartościami statystycznymi. Wyniki są zapisywane w pliku `.csv` lub `.xlsx`. W wynikach jest również całkowity zbiór ustawień.
