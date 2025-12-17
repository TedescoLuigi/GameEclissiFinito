using System;

class Program
{
    // Istruzioni iniziali
    static void MostraIstruzioni()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("        ECLISSI GALATTICA: L'ULTIMO RANGER");
        Console.WriteLine("==============================================");
        Console.WriteLine("Anno 2478.\r\n La Terra è stata distrutta da un fenomeno cosmico noto come Eclissi Galattica, una tempesta di materia oscura che corrompe tutto ciò che tocca.\r\n Le ultime navi di evacuazione sono state intercettate da creature generate dall’Eclissi: gli Ombroformi, esseri che assorbono energia vitale.\r\nTu sei un Ranger Stellare, uno dei pochi sopravvissuti.\r\n La tua missione è raggiungere la stazione spaziale ARK-01, l’ultima struttura capace di invertire la tempesta usando un reattore quantico.\r\nSe fallisci → la galassia scompare nel buio eterno.\r\n Se muori → l’Eclissi si espande e ingloba tutto.\r\n");
        Console.WriteLine("- Scegli il tuo personaggio: A-47 (+5 vita), Kael (+1 danno), Nyra (+1 fuga)");
        Console.WriteLine("- Avanza sulla mappa di 20 caselle, ogni casella ha eventi o bonus");
        Console.WriteLine("- Usa oggetti prima dei combattimenti");
        Console.WriteLine("- Obiettivo: arrivare all'ARK-01 senza morire!");
        Console.WriteLine();
        Console.WriteLine("Oggetti possibili:");
        Console.WriteLine("• Nanokit Medico (+10 vita)");
        Console.WriteLine("• Stim di Rigenerazione (+20 vita)");
        Console.WriteLine("• Pistola (+1 danno)");
        Console.WriteLine("• Spada Magnetica (+2 danno)");
        Console.WriteLine("• Motore Sub-Luce Danneggiato (cavalcatura +1 casella)");
        Console.WriteLine("==============================================");
        Console.WriteLine("Premi INVIO per iniziare...");
        Console.ReadLine();
    }

    // Mostra lo status del giocatore: vita, danno, probabilità fuga e zaino
    static void MostraStatus(int numeroCasella, int puntiVita, int danno, int probabilitaFuga, string[] zaino)
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Casella attuale: {numeroCasella}/20");
        Console.WriteLine($"Vita: {puntiVita} | Danno: {danno} | Probabilità fuga: {probabilitaFuga}");
        Console.WriteLine("Zaino:");

        bool zainoVuoto = true;
        for (int i = 0; i < zaino.Length; i++)
        {
            if (zaino[i] != "")
            {
                Console.WriteLine(" - " + zaino[i]);
                zainoVuoto = false;
            }
        }
        if (zainoVuoto)
        {
            Console.WriteLine("(vuoto)");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Premi INVIO per continuare...");
        Console.ReadLine();
    }

    // Oggetto casuale che può essere trovato
    static string TrovaOggettoCasuale(Random rnd)
    {
        string[] oggetti = {
            "Nanokit Medico (+10 vita)",
            "Stim di Rigenerazione (+20 vita)",
            "Pistola (+1 danno)",
            "Spada Magnetica (+2 danno)",
            "Motore Sub-Luce Danneggiato (cavalcatura +1 casella)" };

        int indice = rnd.Next(oggetti.Length); // Sceglie un numero casuale tra 0 e lunghezza dell'array oggetti
        return oggetti[indice]; // Restituisce l'oggetto casuale
    }

    // Aggiungi un oggetto nello zaino
    static void AggiungiOggettoNelloZaino(string[] zaino, string oggetto)
    {
        for (int i = 0; i < zaino.Length; i++)
        {
            if (zaino[i] == "")
            {
                zaino[i] = oggetto;
                Console.WriteLine("Hai aggiunto " + oggetto + " allo zaino!");
                Console.WriteLine("Premi INVIO per continuare...");
                Console.ReadLine();
                return;
            }
        }
        Console.WriteLine("Lo zaino è pieno! Non puoi raccogliere questo oggetto.");
    }

    // Usa un oggetto (es: medikit, pistola, ecc.)
    static void UsaOggetto(string oggetto, ref int puntiVita, ref bool cavalcatura, ref int danno)
    {
        if (oggetto == "Nanokit Medico")
        {
            Console.WriteLine("+10 vita!");
            puntiVita += 10;
        }
        else if (oggetto == "Stim di Rigenerazione")
        {
            Console.WriteLine("+20 vita!");
            puntiVita += 20;
        }
        else if (oggetto == "Pistola")
        {
            Console.WriteLine("+1 danno!");
            danno += 1;
        }
        else if (oggetto == "Spada Magnetica")
        {
            Console.WriteLine("+2 danno!");
            danno += 2;
        }
        else if (oggetto == "Motore Sub-Luce Danneggiato")
        {
            Console.WriteLine("Hai ottenuto una cavalcatura! +1 casella");
            cavalcatura = true;
        }

        Console.WriteLine("Premi INVIO per continuare...");
        Console.ReadLine();
    }

    // Rimuove un oggetto dallo zaino
    static void RimuoviOggettoDalloZaino(string[] zaino, string oggetto)
    {
        for (int i = 0; i < zaino.Length; i++)
        {
            if (zaino[i] == oggetto)
            {
                zaino[i] = "";
                return;
            }
        }
    }

    // Gestisce il combattimento con un nemico
    static void Combattimento(ref int puntiVitaGiocatore, int dannoGiocatore, Random rnd, string nemico, int vitaNemico, int dannoNemico, int probabilitaFuga)
    {
        Console.WriteLine($"Combattimento contro: {nemico} | Vita nemico: {vitaNemico} | Danno: {dannoNemico}");

        Console.WriteLine("Premi INVIO per continuare...");
        Console.ReadLine();

        while (puntiVitaGiocatore > 0 && vitaNemico > 0)
        {
            int fortuna = rnd.Next(1, 7);

            if (fortuna == 1)
            {
                Console.WriteLine("Sei fortunato! Il nemico manca l'attacco.");
            }
            else
            {
                int danno = rnd.Next(1, dannoGiocatore + 1);
                vitaNemico -= danno;
                Console.WriteLine("Infliggi " + danno + " danni!");

                if (vitaNemico > 0)
                {
                    int dannoSubito = rnd.Next(1, dannoNemico + 1);
                    puntiVitaGiocatore -= dannoSubito;
                    Console.WriteLine("Subisci " + dannoSubito + " danni!");
                }
            }

            Console.WriteLine("Premi INVIO per continuare...");
            Console.ReadLine();
        }

        if (puntiVitaGiocatore <= 0)
        {
            Console.WriteLine("Sei stato annientato!!! Riprova sarai più fortunato la prossima volta!");
        }
        else
        {
            Console.WriteLine("Grande, sei riuscito a sconfiggere il nemico!");
        }

        Console.WriteLine("Premi INVIO per continuare...");
        Console.ReadLine();
    }

    // Gestisce l'evento nella casella
    static void EventoCasella(ref int puntiVita, ref int danno, ref int probabilitaFuga, ref bool cavalcatura, string[] zaino, Random rnd, string descrizioneCasella)
    {
        int tipoEvento = rnd.Next(0, 3); // 0=combattimento, 1=oggetto, 2=personaggio

        if (tipoEvento == 0) // combattimento
        {
            Console.WriteLine("Hai incontrato un nemico!");

            for (int i = 0; i < zaino.Length; i++)
            {
                if (zaino[i] != "")
                {
                    Console.WriteLine($"Vuoi usare {zaino[i]} prima del combattimento? (s/n)");
                    string risposta = Console.ReadLine();

                    if (risposta == "s")
                    {
                        UsaOggetto(zaino[i], ref puntiVita, ref cavalcatura, ref danno);
                        zaino[i] = ""; // Rimuove l'oggetto dallo zaino
                    }
                    else if (risposta == "n")
                    {
                        Console.WriteLine("Non utilizzerai l'oggetto.");
                    }
                }

                int vitaNemico = rnd.Next(5, 11); // Vita del nemico tra 5 e 10
                int dannoNemico = rnd.Next(1, 4); // Danno del nemico tra 1 e 3

                Combattimento(ref puntiVita, danno, rnd, "Alieno", vitaNemico, dannoNemico, probabilitaFuga);
            }
        }
        else if (tipoEvento == 1) // oggetto
        {
            string oggetto = TrovaOggettoCasuale(rnd);
            Console.WriteLine("Hai trovato: " + oggetto + " Vuoi raccoglierlo? (s/n)");
            string risposta = Console.ReadLine();

            if (risposta == "s")
            {
                AggiungiOggettoNelloZaino(zaino, oggetto);
            }
        }
        else if (tipoEvento == 2) // personaggio
        {
            int personaggio = rnd.Next(0, 3);

            if (personaggio == 0)
            {
                Console.WriteLine("Incontri un Ranger sopravvissuto che ti offre un bonus!");
                Console.WriteLine("1) Accetta  2) Ignora");
                string scelta = Console.ReadLine();
                if (scelta == "1")
                {
                    Console.WriteLine("Il Ranger ti cura! +10 vita");
                    puntiVita += 10;
                }
                else if (scelta == "2")
                {
                    Console.WriteLine("Ignori il Ranger e prosegui il tuo viaggio");
                }
            }
            else if (personaggio == 1)
            {
                Console.WriteLine("Un mercante interstellare ti offre una scelta:");
                Console.WriteLine("1) Aggiungi bonus   2) Ignora");
                string scelta = Console.ReadLine();
                if (scelta == "1")
                {
                    Console.WriteLine("Il mercante ti offre una pistola!");
                    AggiungiOggettoNelloZaino(zaino, "Pistola");
                }
                else if (scelta == "2")
                {
                    Console.WriteLine("Ignori il mercante e prosegui il tuo viaggio");
                }
            }
            else
            {
                Console.WriteLine("Un impostore ti attacca!");
                int vitaNemico = rnd.Next(5, 8);
                int dannoNemico = rnd.Next(1, 3);
                Combattimento(ref puntiVita, danno, rnd, "Impostore", vitaNemico, dannoNemico, probabilitaFuga);
            }
        }
    }

    // Dado 1-4
    static int TiraDado(Random rnd)
    {
        return rnd.Next(1, 5);
    }

    static void Main()
    {
        MostraIstruzioni();
        Random rnd = new Random();

        // Scelta personaggio
        Console.WriteLine("Scegli il tuo personaggio: 1) A-47 (+5 vita), 2) Kael (+1 danno), 3) Nyra (+1 fuga)");
        string scelta = Console.ReadLine();

        int puntiVita = 50, danno = 5, probabilitaFuga = 2;
        bool cavalcatura = false;

        if (scelta == "1") { puntiVita += 5; }
        else if (scelta == "2") { danno += 1; }
        else if (scelta == "3") { probabilitaFuga += 1; }

        // Zaino
        string[] zaino = new string[10];
        for (int i = 0; i < zaino.Length; i++)
        {
            zaino[i] = "";
        }

        int posizione = 0;// iniziallizzo posizione prima del ciclo whikle   

        // Mappa [20]
        string[] mappa = {
            "Detriti Orbitali - Resti di navi evacuate, pericolo di danni.",
            "Stiva della Falcon-03 - Nave abbandonata, casse e macchinari danneggiati.",
            "Tunnel di Manutenzione - Passaggi stretti e oscuri, l'eco dei passi ti segue.",
            "Settore RAD-2 - Zona ad alta radiazione, rischi di danni e risorse utili.",
            "Laboratorio Abbandonato - Strutture scientifiche rovinate, possibili dati vitali.",
            "Corridoi Meccanici - Labirinto industriale, luci tremolanti e macchine rotte.",
            "Serbatoi Criogenici - Camere ghiacciate, tecnologia obsoleta, possibili risorse.",
            "Hangar Fantasma - Grande hangar vuoto, ricordi di navi scomparse.",
            "Osservatorio Stellare - Vista mozzafiato, ma senza macchine operative.",
            "Giardino Idroponico - Piante artificiali in un ambiente oscuro e minaccioso.",
            "Stanza degli Ologrammi - Proiezioni di un passato ormai perso.",
            "Archivio Quantico - Biblioteca di conoscenza, rischio di corruzione dei dati.",
            "Cunicolo di Ventilazione - Passaggio buio e stretto, facile perdersi.",
            "Nodo Energetico Centrale - Cuore energetico, rischio di cortocircuiti.",
            "Condotti di Rifiuti - Ambienti fetidi, pericoli nascosti ma possibili risorse.",
            "Laboratorio di Xenobiologia - Ricerca su alieni, pericoli e risorse scientifiche.",
            "Ponte di Comando - Sala di controllo deserta, pericolo imminente.",
            "Cubo di Stabilizzazione - Controllo gravitazionale, strumenti dimenticati.",
            "Corridoio dell’Eclissi - Tunnel oscuro, l'energia oscura minaccia ogni passo.",
            "ARK-01 - Ultima speranza per fermare l'Eclissi e salvare la galassia."
        };

        // ciclo 

        while (puntiVita > 0 && posizione < 19 )
        {
            Console.Clear(); //hit cancella la console a ogni giro del ciclo
            Console.WriteLine($"Sei in: {mappa[posizione]}");
            MostraStatus(posizione + 1, puntiVita, danno, probabilitaFuga, zaino); 

           
            Console.WriteLine("Vuoi tirare il dado?...(s/n)");
            string risposta = Console.ReadLine();

            
            if (risposta == "s")
            {
                int tiro = TiraDado(rnd);
                int avanzamento = tiro;

                
                if (cavalcatura)
                {
                    avanzamento += 1;
                    Console.WriteLine("La cavalcatura ti fa avanzare di 1 casella extra!");
                }

             
                posizione += avanzamento;   // Avanza la posizione sulla mappa

               
                if (posizione > 19)//se esce dalla mappa la limita all 'ultima casella
                {
                    posizione = 19;
                }

                Console.WriteLine($"Hai tirato {tiro}. Avanzi di {avanzamento} caselle.");
                Console.WriteLine($"Ora sei in: {mappa[posizione]}");
                Console.WriteLine("Premi INVIO per continuare...");
                Console.ReadLine();

               
                EventoCasella(ref puntiVita, ref danno, ref probabilitaFuga, ref cavalcatura, zaino, rnd, mappa[posizione]);

             
                if (puntiVita <= 0)
                {
                    Console.WriteLine("Sei morto... Game Over.");
                  
                }
            }
            else
            {
               
                Console.WriteLine("Hai scelto di uscire dal gioco.");
                return;
                
            }

          
            if (puntiVita > 0 && posizione >= 19)
            {
                Console.WriteLine("Sei arrivato all'ARK-01! Missione completata!");
               
            }
        }

        Console.WriteLine("Premi INVIO per uscire...");
        Console.ReadLine();

    }

}
