using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    public string newLayer; // Hedef Layer (Layer1, Layer2, Layer3 gibi)
    public string newSortingLayer; // Hedef Sorting Layer (Layer1, Layer2, Layer3 gibi)
    public int newOrderInLayer; // Yeni Order in Layer değeri

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(">>> Trigger TETİKLENDİ! Nesne: " + other.gameObject.name);

        if (other.CompareTag("Player")) // Sadece oyuncu etkilesin
        {
            Debug.Log(">>> Oyuncu merdivene girdi. Layer değiştiriliyor...");

            // Yeni Layer'ı ayarla
            int layerIndex = LayerMask.NameToLayer(newLayer);
            if (layerIndex == -1)
            {
                Debug.LogError("HATA: " + newLayer + " isimli Layer bulunamadı!");
            }
            else
            {
                other.gameObject.layer = layerIndex;
                Debug.Log("Yeni Layer: " + newLayer);
            }

            // Sorting Layer ve Order in Layer güncelle
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = newSortingLayer;
                sr.sortingOrder = newOrderInLayer;
                Debug.Log("Yeni Sorting Layer: " + newSortingLayer + " | Order in Layer: " + newOrderInLayer);
            }
            else
            {
                Debug.LogError("HATA: Oyuncunun SpriteRenderer bileşeni bulunamadı!");
            }

            // Çocuk nesnelerin de Sorting Layer'ını değiştir
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in srs)
            {
                sprite.sortingLayerName = newSortingLayer;
                sprite.sortingOrder = newOrderInLayer;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(">>> Karakter merdivenin üstünde: " + other.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(">>> Karakter merdivenden çıktı: " + other.gameObject.name);
    }
}
