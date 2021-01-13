using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesDeleter : MonoBehaviour
{
    DecksEditor decksEditor;

    void Awake()
    {
        decksEditor = FindObjectOfType<DecksEditor>();
    }
    public void DeleteMe() {
        if (decksEditor.deleteImage) {
            decksEditor.deleteImage = false;

            ImagesPersistence.DeleteCard(decksEditor.GetCurrentDeckName(), GetComponent<Image>().sprite.name);

            Destroy(gameObject);
        }
    }
}
