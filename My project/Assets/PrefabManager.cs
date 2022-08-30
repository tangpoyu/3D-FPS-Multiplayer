using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    [SerializeField] GameObject[] bulletImpactPrefab;
    private List<Color> colors;
    private Dictionary<Color, GameObject> color_bulletImpact;

    public Dictionary<Color, GameObject> Color_bulletImpact { get => color_bulletImpact; set => color_bulletImpact = value; }

    private void Awake()
    {
        if (instance == null)
        {
            colors = new List<Color>();
            color_bulletImpact = new Dictionary<Color, GameObject>();
            instance = this;
            colors.Add(new Color(255 / 255f, 0, 91 / 255f, 99 / 255f));
            colors.Add(new Color(255 / 255f, 100 / 255f, 0, 134 / 255f));
            colors.Add(new Color(255 / 255f, 222 / 255f, 0, 132 / 255f));
            colors.Add(new Color(161 / 255f, 4 / 255f, 255 / 255f, 107 / 255f));
            colors.Add(new Color(75 / 255f, 245 / 255f, 0, 128 / 255f));
            colors.Add(new Color(0, 245 / 255f, 212 / 255f, 107 / 255f));

            foreach (var x in colors.Select((value, index) => new { value, index }))
            {
                Color_bulletImpact.Add(x.value, bulletImpactPrefab[x.index]);
            }
        }
    }
}
   

