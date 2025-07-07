using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engel : MonoBehaviour
{
    [Tooltip("Otomatik olarak tüm child'lardaki KenarRenk scriptlerini bulur.")]
    public List<KenarRenk> kenarParcalari;

    private void Reset()
    {
        kenarParcalari = new List<KenarRenk>(GetComponentsInChildren<KenarRenk>());
    }

    public List<Color> GetRenkler()
    {
        List<Color> renkler = new List<Color>();
        if (kenarParcalari == null || kenarParcalari.Count == 0)
        {
            kenarParcalari = new List<KenarRenk>(GetComponentsInChildren<KenarRenk>());
        }

        foreach (var kenar in kenarParcalari)
        {
            if (kenar != null && !renkler.Contains(kenar.renk))
            {
                renkler.Add(kenar.renk);
            }
        }

        return renkler;
    }
}