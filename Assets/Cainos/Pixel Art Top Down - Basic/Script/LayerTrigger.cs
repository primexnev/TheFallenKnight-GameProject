using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    [Header("Layer Geçiş Ayarları")]
    public string layer1 = "Layer 1 - Base";   // En alt katman
    public string layer2 = "Layer 2 - Middle"; // Orta katman
    public string layer3 = "Layer 3 - Top";    // En üst katman
    public int orderLayer1 = 1; // Layer 1 için Order in Layer
    public int orderLayer2 = 2; // Layer 2 için Order in Layer
    public int orderLayer3 = 3; // Layer 3 için Order in Layer

    private Dictionary<string, string> previousSortingLayers = new Dictionary<string, string>();
    private Dictionary<string, int> previousOrderLayers = new Dictionary<string, int>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sadece oyuncu etkilesin
        {
            Debug.Log(">>> Oyuncu merdivene girdi. Layer değiştiriliyor...");

            // Karakterin mevcut Layer'ını kontrol et
            string currentLayer = other.gameObject.layer == LayerMask.NameToLayer(layer1) ? layer1 :
                                  other.gameObject.layer == LayerMask.NameToLayer(layer2) ? layer2 :
                                  layer3; // Varsayılan olarak en üst Layer kabul edilir.

            string newLayer = (currentLayer == layer1) ? layer2 :
                              (currentLayer == layer2) ? layer3 :
                              layer2; // Eğer en üst Layer’daysa geri Layer 2’ye düş

            int newLayerIndex = LayerMask.NameToLayer(newLayer);
            if (newLayerIndex == -1)
            {
                Debug.LogError("HATA: " + newLayer + " isimli Layer bulunamadı!");
                return; // Hata olursa kodu durdur
            }
            
            other.gameObject.layer = newLayerIndex;
            Debug.Log("Yeni Layer: " + newLayer);

            // Sorting Layer ve Order in Layer güncelle
            SpriteRenderer[] spriteRenderers = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                // Eski Sorting Layer ve Order'ı sakla
                if (!previousSortingLayers.ContainsKey(sr.gameObject.name))
                {
                    previousSortingLayers[sr.gameObject.name] = sr.sortingLayerName;
                    previousOrderLayers[sr.gameObject.name] = sr.sortingOrder;
                }

                // Yeni Sorting Layer ve Order'ı ayarla
                sr.sortingOrder = (newLayer == layer1) ? orderLayer1 :
                                  (newLayer == layer2) ? orderLayer2 :
                                  orderLayer3;
            }

            Debug.Log("Layer ve Sorting Order güncellendi: " + newLayer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sadece oyuncu etkilesin
        {
            Debug.Log(">>> Karakter merdivenden çıktı, eski layer geri yükleniyor...");

            // Eski Layer'ı geri yükle
            SpriteRenderer[] spriteRenderers = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                if (previousSortingLayers.ContainsKey(sr.gameObject.name))
                {
                    sr.sortingLayerName = previousSortingLayers[sr.gameObject.name];
                }

                if (previousOrderLayers.ContainsKey(sr.gameObject.name))
                {
                    sr.sortingOrder = previousOrderLayers[sr.gameObject.name];
                }
            }

            Debug.Log("Eski Layer geri yüklendi.");
        }
    }
}
