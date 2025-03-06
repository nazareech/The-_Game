using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // для TMPro
//using UnityEngine.UI; // для звичайного тексту

public class Translator : MonoBehaviour
{

    private static int LanguageID;

    private static List<Translatable_text> listID = new List<Translatable_text>();

    #region Тексти локалізації
    private static string[,] LineText =
    {   
      // 🇺🇦 Українська
      #region UKRAINIAN 
      {
          "Почати гру",       // 0 - Text ID
          "Налаштування",     // 1 - Text ID
          "Вихід",            // 2 - Text ID
          "Мова",             // 3 - Text ID
          "Вимкнути/Вимкнути музику", // 4 - Text ID
          "Музика"   // 5 - Text ID
      },
      #endregion
      
      // 🇵🇱 Польська
      #region POLISH
      {
          "Rozpocznij grę",   // 0 - Text ID
          "Ustawienia",       // 1 - Text ID
          "Wyjście",          // 2 - Text ID
          "Język",            // 3 - Text ID
          "Włącz/Wyłącz muzykę",   // 4 - Text ID
          "Muzyka"   // 5 - Text ID
      },  
      #endregion

      // 🇬🇧 Англійська
      #region ENGLISH
      {
          "Play",         // 0 - Text ID
          "Options",      // 1 - Text ID
          "Quit",         // 2 - Text ID
          "Language",     // 3 - Text ID
          "Toggle Music", // 4 - Text ID
          "Music"  // 5 - Text ID
      },
      #endregion

      // 🇨🇳 Китайська (спрощена)
      #region CHINESE SIMPLIFIED
      {
          "开始游戏",     // 0 - Start Game
          "选项",        // 1 - Options
          "退出",        // 2 - Quit
          "语言",        // 3 - Language
          "切换音乐",     // 4 - Toggle Music
          "音乐"      // 5 - Music
      },
      #endregion

      // 🇪🇸 Іспанська
      #region SPANISH
      {
          "Jugar",                // 0 - Text ID
          "Opciones",             // 1 - Text ID
          "Salir",                // 2 - Text ID
          "Idioma",               // 3 - Text ID
          "Activar/Desactivar música",       // 4 - Text ID
          "Música"  // 5 - Text ID
      },
      #endregion

      // 🇫🇷 Французька
      #region FRENCH
      {
          "Jouer",                // 0 - Text ID
          "Options",              // 1 - Text ID
          "Quitter",              // 2 - Text ID
          "Langue",               // 3 - Text ID
          "Activer/Désactiver la musique",   // 4 - Text ID
          "Musique"  // 5 - Text ID
      },
      #endregion

      // 🇩🇪 Німецька
      #region GERMAN
      {
          "Spielen",          // 0 - Text ID
          "Optionen",         // 1 - Text ID
          "Beenden",          // 2 - Text ID
          "Sprache",          // 3 - Text ID
          "Musik ein/aus", // 4 - Text ID
          "Musik"   // 5 - Text ID
      },
      #endregion

      // 🇮🇹 Італійська
      #region ITALIAN
      {
          "Gioca",                // 0 - Text ID
          "Opzioni",              // 1 - Text ID
          "Esci",                 // 2 - Text ID
          "Lingua",               // 3 - Text ID
          "Attiva/Disattiva musica",     // 4 - Text ID
          "Musica"   // 5 - Text ID
      },
      #endregion
      
      // 🇹🇷 Турецька
      #region TURKISH
      {
          "Oyna",               // 0 - Text ID
          "Ayarlar",            // 1 - Text ID
          "Çıkış",               // 2 - Text ID
          "Dil",                // 3 - Text ID
          "Müzik Aç/Kapat",    // 4 - Text ID
          "Müzik"   // 5 - Text ID
      },
      #endregion
  };
    #endregion

    static public void Select_language(int id)
    {
        LanguageID = id;
        Update_texts();
    }

    static public string Get_text(int textKey)
    {
        return LineText[LanguageID, textKey];
    }

    static public void Add(Translatable_text idtext)
    {
        listID.Add(idtext);
    }

    static public void Delete(Translatable_text idtext)
    {
        listID.Remove(idtext);
    }

    static public void Update_texts()
    {

        if (listID.Count == 0) return;

        for (int i = 0; i < listID.Count; i++)
        {
            listID[i].UIText.text = LineText[LanguageID, listID[i].textID];
            if (PlayerPrefs.GetInt("Language") == 1) 
                listID[i].UIText.font = Resources.Load<TMP_FontAsset>("Назва шрифту UA");
            else if (PlayerPrefs.GetInt("Language") == 2) 
                listID[i].UIText.font = Resources.Load<TMP_FontAsset>("Назва шрифту CH");
            else 
                listID[i].UIText.font = Resources.Load<TMP_FontAsset>("Назва шрифту EN");
        }
    }
};
