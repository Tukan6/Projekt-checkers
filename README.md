# Projekt-checkers
-zacatek deklarace vsech promennych
metoda setupboard-
  - pomoci for smycek postupne do predem vytvoreneho gridu v xamlu nahazim do urcenych mist objekt bud tridy player1 nebo tridy player2
  - bere se to podle toho, jestli jsem zrovna na sude nebo liche pozici
  - ke kazde figurce se prida tlaciko na pohyb
metoda player1_click
  - nejdrive zjistim jestli jsem na tahu
  - pote zjistim radek a sloupek kliknutou figurku
  - pokud uz je nejaky nakliknuty, smazu vytvorene mozne skoky panacka a do metody vlezu zpet s nove nakliknutou figurkou
  - zjistim, zda jsem na kraji plochy, jestli vedle me je pratelska figurka nebo nepratelska figurka. pokud jsem na kraji, zobrazi se mi jen jedna mozna cesta, pokud je vedle me pratelska figurka, nezobrazi se nic, a pokud je vedle me nepratelska figurka za kterou nic neni, musim ji vzit, pokud za ni neco je, nic ze nezobrazi
  - metoda je rozdelena do dvou casti: jedna kdy je figurka na kraji plochy a druha kdy je figurka "uprostred" plochy
  - po vygenerovani policek pro mozny skok figurky se ke kazde vytvori tlacikto, to me nasmeruje na metodu Cango_click ke ktere se dostanu
metoda player1_kingclick
  - opet zjistim zda jsem na tahu
  - zjistim sloupek a radek, a nadeklaruju si true na kazdy smer pohybu
  - potom je kod pro kazdy smer stejny krome prohazovani + a -
  - cele to je ve for smycce ktera se zopakuje 7krat
  - nejdrive zjistim co se nachazi vedle figurky a podle toho se stane to co u normalni figurky
  - kdyz vedle figurky nic neni, bude se vytvaret cesticka dokud neco nepotka, pote si muzeme zvolit ze vsech smeru kam pujdeme i o kolik policek, pokud potka po nejake chvili protivnika, preskoco ho a tam cesticka konci
  - kdyz nakliknu jinou figurku zelene kolecka predstavujici mozny skok se smazou
  - muzu si zase hybat s kym chci
metody player2_click a player2_kingclikc
  - metody jsou uplne stejne jak u predeslych player1 metod, akorat jsou obracene na desce obracene
metoda Cango1_click
  - nejdrive nastavim puvodni pozici figurky v boardarray na typ kde se nic nenachazi
  - pote, podle toho co muzu vzit jestli doprava nebo doleva muzu sebrat figurku
  - projede se cela hraci deska, najde to nepritelsky objekt, ktery se nachazi na urcenem miste, ktere chci vyhodit, a smaze se z desky
  - odecte se z celkoveho poctu jedna figurka, smazou se zelene krouzky ktere ukazuji kam se figurka muze hybat a cely cyklus zacina znovu akorat je na tahu druhy hrac
metody Cango2_click
  - opet stejna jako predchozi akorat otocena pro druheho hrace

  - problemy a nefunkcnosti: z nejakeho duvodu se nechteji mazat figurky kdyz je seberu damou druheho hrace, kod je uplne stejny jako u prvni damy, a jako u sbirani figurek u normalnich malych figurek, kde to funguje
  - dama se obcas bugne kvuli indexum ktere jsou pravdepodobne nekde scitane spatne
