using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("--- CANVASES / SCHERMATE ---")]
    public GameObject canvasAccesso;
    public GameObject canvasRegistrazione;
    public GameObject canvasQuestionario;
    public GameObject canvasMenu;

    [Header("--- ACCESSO (LOGIN) ---")]
    public InputField inputEmailLogin;
    public InputField inputPasswordLogin;
    public Button bottoneAccedi;

    [Header("--- REGISTRAZIONE DATI ---")]
    public InputField inputNome;
    public InputField inputCognome;
    public InputField inputRegEmail;
    public InputField inputRegPassword;
    public InputField inputDataNascita;
    public Button bottoneAvantiQuestionario;

    [Header("--- QUESTIONARIO ALLERGIE ---")]
    public Toggle toggleGlutine;
    public Toggle toggleLattosio;
    public Toggle toggleVegano;

    [Header("--- BOTTONI CIBO (Da disattivare) ---")]
    public Button btnPizza; public Button btnCappuccino; public Button btnPane;
    public Button btnCroassaint; public Button btnPollo; public Button btnPatatine;
    public Button btnSushi; public Button btnKashipan; public Button btnHamburger;
    public Button btnCheeseburger; public Button btnGianburger; public Button btnTorta;
    public Button btnSandwich; public Button btnTofu; public Button btnFocaccia;

    [Header("--- MODELLI 3D ---")]
    public GameObject pizza; public GameObject cappuccino; public GameObject Pane;
    public GameObject Croassaint; public GameObject Pollo; public GameObject Patatineecoca;
    public GameObject Sushi; public GameObject Kashipan; public GameObject Hambuger;
    public GameObject Cheeseburger; public GameObject Gianburger; public GameObject Torta;
    public GameObject Sandwich; public GameObject Tofu; public GameObject Focaccia;

    [Header("--- UI TESTI E AR ---")]
    public Text prezzo;
    public Text testoTitoloPiatto;
    public Text testoIngredienti;
    public Text testoAllergie;
    public GameObject testoTitoloPrincipale;
    public GameObject sfondoMenu;
    public GameObject testoIstruzioniAR;
    public GameObject sfondoNeroIstruzioniAR;

    [Header("--- CARRELLO ---")]
    public GameObject pannelloCarrello;
    public Text testoListaPiatti;
    public Text testoTotale;
    public Text testoOrdineInviato;
    public Text testoPiattoAggiunto;
    public GameObject vediCarrelloButton;
    public GameObject bottoneAggiungiAlCarrello;

    [Header("--- NAVIGATION & DECORAZIONI MENU ---")]
    public GameObject[] tuttiIBottoniMenu;
    public GameObject[] elementiDecorativiMenu;
    public GameObject bottoneIndietro;

    // Variabili private di sistema
    private float totaleOrdine = 0f;
    private string listaOrdine = "";
    private float prezzoAttuale = 0f;
    private Coroutine timerAR;
    private Coroutine timerOrdine;
    private Coroutine timerInviato;

    void Start()
    {
        canvasAccesso.SetActive(true);
        canvasRegistrazione.SetActive(false);
        canvasQuestionario.SetActive(false);
        canvasMenu.SetActive(false);

        if (bottoneAccedi) bottoneAccedi.interactable = false;
        if (bottoneAvantiQuestionario) bottoneAvantiQuestionario.interactable = false;
        if (testoOrdineInviato) testoOrdineInviato.gameObject.SetActive(false);
        if (testoPiattoAggiunto) testoPiattoAggiunto.gameObject.SetActive(false);
        if (pannelloCarrello) pannelloCarrello.SetActive(false);

        SpegniSoloAR();
    }

    // ==========================================
    // VALIDAZIONE INPUT
    // ==========================================

    public void ControllaInputLogin()
    {
        if (bottoneAccedi)
            bottoneAccedi.interactable = (inputEmailLogin.text.Length > 0 && inputPasswordLogin.text.Length > 0);
    }

    public void ControllaInputRegistrazione()
    {
        if (bottoneAvantiQuestionario)
        {
            bool ok = (inputNome.text.Length > 0 && inputCognome.text.Length > 0 &&
                       inputRegEmail.text.Length > 0 && inputRegPassword.text.Length > 0 &&
                       inputDataNascita.text.Length > 0);
            bottoneAvantiQuestionario.interactable = ok;
        }
    }

    // ==========================================
    // NAVIGAZIONE FLUSSO
    // ==========================================

    public void ClickAccediOOspite()
    {
        Handheld.Vibrate();
        canvasAccesso.SetActive(false);
        canvasMenu.SetActive(true);
        ResetAllergeniEBottoni();
        tornaAlMenu();
    }

    public void ClickVaiARegistrazione()
    {
        Handheld.Vibrate();
        canvasAccesso.SetActive(false);
        canvasRegistrazione.SetActive(true);
    }

    public void ClickProcediAQuestionario()
    {
        Handheld.Vibrate();
        canvasRegistrazione.SetActive(false);
        canvasQuestionario.SetActive(true);
    }

    public void CompletaRegistrazione()
    {
        Handheld.Vibrate();
        canvasQuestionario.SetActive(false);
        canvasMenu.SetActive(true);
        ApplicaFiltriAllergie();
        tornaAlMenu();
    }

    public void ApriModificaAllergie()
    {
        Handheld.Vibrate();
        canvasMenu.SetActive(false);
        canvasQuestionario.SetActive(true);
    }

    // ==========================================
    // GESTIONE AR E MENU
    // ==========================================

    private void PreparazionePiatto()
    {
        SpegniSoloAR();

        foreach (GameObject btn in tuttiIBottoniMenu) { if (btn != null) btn.SetActive(false); }
        foreach (GameObject decor in elementiDecorativiMenu) { if (decor != null) decor.SetActive(false); }

        if (sfondoMenu) sfondoMenu.SetActive(false);
        if (testoTitoloPrincipale) testoTitoloPrincipale.SetActive(false);

        if (testoTitoloPiatto) testoTitoloPiatto.gameObject.SetActive(true);
        if (testoIngredienti) testoIngredienti.gameObject.SetActive(true);
        if (testoAllergie) testoAllergie.gameObject.SetActive(true);
        if (prezzo) prezzo.gameObject.SetActive(true);

        if (bottoneIndietro) bottoneIndietro.SetActive(true);
        if (bottoneAggiungiAlCarrello) bottoneAggiungiAlCarrello.SetActive(true);
        if (vediCarrelloButton) vediCarrelloButton.SetActive(true);

        if (testoIstruzioniAR) testoIstruzioniAR.SetActive(true);
        if (sfondoNeroIstruzioniAR) sfondoNeroIstruzioniAR.SetActive(true);

        if (timerAR != null) StopCoroutine(timerAR);
        timerAR = StartCoroutine(NascondiIstruzioni(3f));
    }

    public void tornaAlMenu()
    {
        SpegniSoloAR();

        foreach (GameObject btn in tuttiIBottoniMenu) { if (btn != null) btn.SetActive(true); }
        foreach (GameObject decor in elementiDecorativiMenu) { if (decor != null) decor.SetActive(true); }

        if (sfondoMenu) sfondoMenu.SetActive(true);
        if (testoTitoloPrincipale) testoTitoloPrincipale.SetActive(true);
        if (bottoneIndietro) bottoneIndietro.SetActive(false);

        // Riapplica i filtri allergie ogni volta che si torna al menu,
        // così i bottoni disabilitati restano disabilitati correttamente.
        ApplicaFiltriAllergie();
    }

    private void SpegniSoloAR()
    {
        GameObject[] modelli = { pizza, cappuccino, Pane, Croassaint, Pollo, Patatineecoca, Sushi, Kashipan, Hambuger, Cheeseburger, Gianburger, Torta, Sandwich, Tofu, Focaccia };
        foreach (GameObject m in modelli) if (m != null) m.SetActive(false);

        if (prezzo) prezzo.text = "";
        if (testoTitoloPiatto) testoTitoloPiatto.text = "";
        if (testoIngredienti) testoIngredienti.text = "";
        if (testoAllergie) testoAllergie.text = "";

        if (testoIstruzioniAR) testoIstruzioniAR.SetActive(false);
        if (sfondoNeroIstruzioniAR) sfondoNeroIstruzioniAR.SetActive(false);
        if (bottoneAggiungiAlCarrello) bottoneAggiungiAlCarrello.SetActive(false);
        if (vediCarrelloButton) vediCarrelloButton.SetActive(false);
    }

    // ==========================================
    // CARRELLO E ORDINE
    // ==========================================

    public void aggiungiPiattoAllOrdine()
    {
        Handheld.Vibrate();
        totaleOrdine += prezzoAttuale;
        listaOrdine += "- " + testoTitoloPiatto.text + "\n";
        if (testoPiattoAggiunto)
        {
            testoPiattoAggiunto.text = testoTitoloPiatto.text + " aggiunto!";
            testoPiattoAggiunto.gameObject.SetActive(true);
            if (timerOrdine != null) StopCoroutine(timerOrdine);
            timerOrdine = StartCoroutine(NascondiFeedbackAggiunta(3f));
        }
    }

    public void InviaOrdine()
    {
        if (totaleOrdine <= 0) return;
        Handheld.Vibrate();
        listaOrdine = "";
        totaleOrdine = 0f;
        if (testoListaPiatti) testoListaPiatti.text = "Carrello vuoto";
        if (testoTotale) testoTotale.text = "TOTALE: 0.00 EUR";
        if (testoOrdineInviato)
        {
            testoOrdineInviato.gameObject.SetActive(true);
            if (timerInviato != null) StopCoroutine(timerInviato);
            timerInviato = StartCoroutine(NascondiOrdineInviato(3f));
        }
    }

    public void ApriCarrello()
    {
        if (testoListaPiatti) testoListaPiatti.text = (listaOrdine == "") ? "Carrello vuoto" : listaOrdine;
        if (testoTotale) testoTotale.text = "TOTALE: " + totaleOrdine.ToString("F2") + " EUR";
        if (pannelloCarrello) pannelloCarrello.SetActive(true);
    }

    public void ChiudiCarrello()
    {
        if (pannelloCarrello) pannelloCarrello.SetActive(false);
    }

    // ==========================================
    // FILTRI ALLERGIE (CORRETTI)
    // ==========================================

    private void ApplicaFiltriAllergie()
    {
        // Prima resettiamo tutti i bottoni a interagibile
        ResetAllergeniEBottoni();

        // GLUTINE — piatti con farina, pane o impasti
        // Pizza, Pane, Croissant, Kashipan, Hamburger, Cheeseburger,
        // Giant Burger, Torta, Focaccia, Sandwich
        if (toggleGlutine && toggleGlutine.isOn)
            DisattivaFiltro(new Button[] {
                btnPizza,
                btnPane,
                btnCroassaint,
                btnKashipan,      // FIX: mancava — contiene farina
                btnHamburger,
                btnCheeseburger,
                btnGianburger,
                btnTorta,
                btnFocaccia,
                btnSandwich       // FIX: mancava — pane
            });

        // LATTOSIO — piatti con latte, burro o formaggi
        // Pizza, Cappuccino, Croissant, Kashipan, Cheeseburger,
        // Giant Burger, Torta
        if (toggleLattosio && toggleLattosio.isOn)
            DisattivaFiltro(new Button[] {
                btnPizza,
                btnCappuccino,
                btnCroassaint,
                btnKashipan,      // FIX: mancava — contiene burro
                btnCheeseburger,
                btnGianburger,
                btnTorta
            });

        // VEGANO — piatti con carne, pesce, uova, latticini o derivati animali
        // Pizza (mozzarella), Cappuccino (latte), Croissant (burro+uova),
        // Kashipan (burro), Pollo, Sushi (pesce), Hamburger (manzo+maionese),
        // Cheeseburger (manzo+cheddar), Giant Burger (manzo+bacon+cheddar),
        // Torta (crema+uova), Sandwich (tacchino+bacon+uova)
        if (toggleVegano && toggleVegano.isOn)
            DisattivaFiltro(new Button[] {
                btnPizza,         // Mozzarella
                btnCappuccino,    // Latte
                btnCroassaint,    // Burro, Uova
                btnKashipan,      // FIX: mancava — contiene burro
                btnPollo,         // Carne
                btnSushi,         // Pesce
                btnHamburger,     // Manzo, Maionese
                btnCheeseburger,  // Manzo, Cheddar
                btnGianburger,    // Manzo, Bacon, Cheddar
                btnTorta,         // Crema, Uova
                btnSandwich       // FIX: mancava — Tacchino, Bacon, Uova
            });
    }

    private void DisattivaFiltro(Button[] lista)
    {
        foreach (Button b in lista)
            if (b) b.interactable = false;
    }

    private void ResetAllergeniEBottoni()
    {
        Button[] tutti = {
            btnPizza, btnCappuccino, btnPane, btnCroassaint, btnPollo,
            btnPatatine, btnSushi, btnKashipan, btnHamburger, btnCheeseburger,
            btnGianburger, btnTorta, btnSandwich, btnTofu, btnFocaccia
        };
        foreach (Button b in tutti)
            if (b != null) b.interactable = true;
    }

    // ==========================================
    // COROUTINES / FEEDBACK
    // ==========================================

    IEnumerator NascondiIstruzioni(float s)
    {
        yield return new WaitForSeconds(s);
        if (testoIstruzioniAR) testoIstruzioniAR.SetActive(false);
        if (sfondoNeroIstruzioniAR) sfondoNeroIstruzioniAR.SetActive(false);
    }

    IEnumerator NascondiFeedbackAggiunta(float s)
    {
        yield return new WaitForSeconds(s);
        if (testoPiattoAggiunto) testoPiattoAggiunto.gameObject.SetActive(false);
    }

    IEnumerator NascondiOrdineInviato(float s)
    {
        yield return new WaitForSeconds(s);
        if (testoOrdineInviato) testoOrdineInviato.gameObject.SetActive(false);
        ChiudiCarrello();
    }

    public void VibraTargetTrovato() { Handheld.Vibrate(); }

    // ==========================================
    // APERTURA PIATTI
    // ==========================================

    public void openPizza()
    {
        prezzoAttuale = 8.50f; PreparazionePiatto();
        if (pizza) pizza.SetActive(true);
        prezzo.text = "8.50 EUR";
        testoTitoloPiatto.text = "PIZZA MARGHERITA";
        testoIngredienti.text = "Pomodoro, Mozzarella, Basilico";
        testoAllergie.text = "⚠️ Glutine, Lattosio";
    }

    public void openCappuccino()
    {
        prezzoAttuale = 1.50f; PreparazionePiatto();
        if (cappuccino) cappuccino.SetActive(true);
        prezzo.text = "1.50 EUR";
        testoTitoloPiatto.text = "CAPPUCCINO";
        testoIngredienti.text = "Caffè, Latte";
        testoAllergie.text = "⚠️ Lattosio";
    }

    public void openPane()
    {
        prezzoAttuale = 2.00f; PreparazionePiatto();
        if (Pane) Pane.SetActive(true);
        prezzo.text = "2.00 EUR";
        testoTitoloPiatto.text = "PANE CASERECCIO";
        testoIngredienti.text = "Farina, Acqua, Lievito";
        testoAllergie.text = "⚠️ Glutine";
    }

    public void openCroassaint()
    {
        prezzoAttuale = 1.50f; PreparazionePiatto();
        if (Croassaint) Croassaint.SetActive(true);
        prezzo.text = "1.50 EUR";
        testoTitoloPiatto.text = "CROISSAINT AL BURRO";
        testoIngredienti.text = "Farina, Burro, Uova";
        testoAllergie.text = "⚠️ Glutine, Lattosio";
    }

    public void openPollo()
    {
        prezzoAttuale = 12.00f; PreparazionePiatto();
        if (Pollo) Pollo.SetActive(true);
        prezzo.text = "12.00 EUR";
        testoTitoloPiatto.text = "POLLO ARROSTO";
        testoIngredienti.text = "Pollo ruspante, Rosmarino";
        testoAllergie.text = "";
    }

    public void openPatatineecoca()
    {
        prezzoAttuale = 6.00f; PreparazionePiatto();
        if (Patatineecoca) Patatineecoca.SetActive(true);
        prezzo.text = "6.00 EUR";
        testoTitoloPiatto.text = "PATATINE E COCA COLA";
        testoIngredienti.text = "Patate, Bibita";
        testoAllergie.text = "";
    }

    public void openSushi()
    {
        prezzoAttuale = 15.00f; PreparazionePiatto();
        if (Sushi) Sushi.SetActive(true);
        prezzo.text = "15.00 EUR";
        testoTitoloPiatto.text = "SUSHI MIX";
        testoIngredienti.text = "Riso, Salmone, Soia";
        testoAllergie.text = "";
    }

    public void openKashipan()
    {
        prezzoAttuale = 3.50f; PreparazionePiatto();
        if (Kashipan) Kashipan.SetActive(true);
        prezzo.text = "3.50 EUR";
        testoTitoloPiatto.text = "KASHIPAN";
        testoIngredienti.text = "Pane dolce, Burro";
        testoAllergie.text = "⚠️ Glutine, Lattosio";
    }

    public void openHambuger()
    {
        prezzoAttuale = 9.00f; PreparazionePiatto();
        if (Hambuger) Hambuger.SetActive(true);
        prezzo.text = "9.00 EUR";
        testoTitoloPiatto.text = "HAMBURGER";
        testoIngredienti.text = "Pane, Manzo, Maionese";
        testoAllergie.text = "⚠️ Glutine";
    }

    public void openCheeseburger()
    {
        prezzoAttuale = 10.00f; PreparazionePiatto();
        if (Cheeseburger) Cheeseburger.SetActive(true);
        prezzo.text = "10.00 EUR";
        testoTitoloPiatto.text = "CHEESEBURGER";
        testoIngredienti.text = "Pane, Manzo, Cheddar";
        testoAllergie.text = "⚠️ Glutine, Lattosio";
    }

    public void openGianburger()
    {
        prezzoAttuale = 13.50f; PreparazionePiatto();
        if (Gianburger) Gianburger.SetActive(true);
        prezzo.text = "13.50 EUR";
        testoTitoloPiatto.text = "GIANT BURGER";
        testoIngredienti.text = "Doppio Manzo, Bacon, Cheddar";
        testoAllergie.text = "⚠️ Glutine, Lattosio";
    }

    public void openTorta()
    {
        prezzoAttuale = 5.00f; PreparazionePiatto();
        if (Torta) Torta.SetActive(true);
        prezzo.text = "5.00 EUR";
        testoTitoloPiatto.text = "TORTA";
        testoIngredienti.text = "Pan di Spagna, Crema, Fragole";
        testoAllergie.text = "⚠️ Glutine, Lattosio";
    }

    public void openSandwich()
    {
        prezzoAttuale = 6.50f; PreparazionePiatto();
        if (Sandwich) Sandwich.SetActive(true);
        prezzo.text = "6.50 EUR";
        testoTitoloPiatto.text = "CLUB SANDWICH";
        testoIngredienti.text = "Pane, Tacchino, Bacon, Uova";
        testoAllergie.text = "⚠️ Glutine";
    }

    public void openTofu()
    {
        prezzoAttuale = 7.00f; PreparazionePiatto();
        if (Tofu) Tofu.SetActive(true);
        prezzo.text = "7.00 EUR";
        testoTitoloPiatto.text = "TOFU";
        testoIngredienti.text = "Soia, Salsa Soia";
        testoAllergie.text = "";
    }

    public void openFocaccia()
    {
        prezzoAttuale = 4.00f; PreparazionePiatto();
        if (Focaccia) Focaccia.SetActive(true);
        prezzo.text = "4.00 EUR";
        testoTitoloPiatto.text = "FOCACCIA";
        testoIngredienti.text = "Farina, Olio, Sale";
        testoAllergie.text = "⚠️ Glutine";
    }
}