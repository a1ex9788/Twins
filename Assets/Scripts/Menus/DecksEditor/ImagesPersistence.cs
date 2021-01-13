using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImagesPersistence
{

    public static string[] GetDecksNames()
    {
        string decksPath = Application.persistentDataPath + "\\Decks";
        if (!Directory.Exists(decksPath)) Directory.CreateDirectory(decksPath);

        string[] aux = Directory.GetDirectories(decksPath);
        string[] res = new string[aux.Length];
        for (int i = 0; i < aux.Length; i++) {
            string[] s = aux[i].Split('\\');
            res[i] = s[s.Length-1];
        }
        return res;
    }

    public static Sprite LoadBackSide(string deckName) {
        WWW wwwImagen = new WWW(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.png");
        if (!File.Exists(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.png"))
            wwwImagen = new WWW(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.jpg");

        //if (!File.Exists(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.png")
        //    && !File.Exists(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.jpg"))
        //    return null;

        return Sprite.Create(wwwImagen.texture, new Rect(0, 0, wwwImagen.texture.width, wwwImagen.texture.height), new Vector2(0.5f, 0.5f));
    }

    public static List<Sprite> LoadCardsImages(string deckName) {
        if (!Directory.Exists(Application.persistentDataPath + "\\Decks\\" + deckName + "\\symbols"))
            Directory.CreateDirectory(Application.persistentDataPath + "\\Decks\\" + deckName + "\\symbols");

        List<Sprite> cards = new List<Sprite>();

        foreach (string image in Directory.GetFiles(Application.persistentDataPath + "\\Decks\\" + deckName + "\\symbols")) {
            WWW wwwImagen = new WWW(image);
            Sprite sprite = Sprite.Create(wwwImagen.texture, new Rect(0, 0, wwwImagen.texture.width, wwwImagen.texture.height), new Vector2(0.5f, 0.5f));
            sprite.name = image.Split('\\')[image.Split('\\').Length-1];
            cards.Add(sprite);
        }

        return cards;
    }

    public static void SaveBackSide(string originPath, string deckName) {
        if (File.Exists(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.png"))
            File.Delete(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.png");
        if (File.Exists(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.jpg"))
            File.Delete(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.jpg");

        SaveImage(originPath, deckName, "backSide");
    }

    public static void SaveCardImage(string originPath, string deckName) {
        SaveImage(originPath, deckName + "\\symbols", originPath.GetHashCode().ToString());
    }

    static void SaveImage(string originPath, string directory, string imageName) {
        string finalDirectory = Application.persistentDataPath + "\\Decks\\" + directory;
        if (!Directory.Exists(finalDirectory)) Directory.CreateDirectory(finalDirectory);
        try
        {
            File.Copy(originPath, finalDirectory + "\\" + imageName + ".png");
        }
        catch (Exception) {
            File.Copy(originPath.Substring(0, originPath.Length-3) + "jpg", finalDirectory + "\\" + imageName + ".jpg");
        }
    }



    public static void DeleteCard(string deckName, string cardName) {
        File.Delete(Application.persistentDataPath + "\\Decks\\" + deckName + "\\symbols\\" + cardName);
        Debug.Log(Application.persistentDataPath + "\\Decks\\" + deckName + "\\symbols\\" + cardName);
    }

    public static void DeleteDeck(string deckName) {
        foreach (string s in Directory.GetFiles(Application.persistentDataPath + "\\Decks\\" + deckName + "\\symbols"))
            File.Delete(s);

        File.Delete(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.png");
        File.Delete(Application.persistentDataPath + "\\Decks\\" + deckName + "\\backSide.jpg");

        foreach (string s in Directory.GetDirectories(Application.persistentDataPath + "\\Decks\\" + deckName))
            Directory.Delete(s);

        Directory.Delete(Application.persistentDataPath + "\\Decks\\" + deckName);
    }

}
