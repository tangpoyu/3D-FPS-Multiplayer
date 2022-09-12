using System;
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
    private Dictionary<GameObject, Color> bulletImpact_color;

    public Dictionary<Color, GameObject> Color_bulletImpact { get => color_bulletImpact; set => color_bulletImpact = value; }
    public Dictionary<GameObject, Color> BulletImpact_color { get => bulletImpact_color; set => bulletImpact_color = value; }

    private void Awake()
    {
        if (instance == null)
        {
            
            colors = new List<Color>();
            color_bulletImpact = new Dictionary<Color, GameObject>();
            BulletImpact_color = new Dictionary<GameObject, Color>();
            instance = this;

            colors.Add(HexToColor.GetColorFromString("D771E9E6")); // BulletImpact_PINK #D771E9E6
            colors.Add(HexToColor.GetColorFromString("F56000")); // BulletImpact_ORANGE #F56000
            colors.Add(HexToColor.GetColorFromString("F5D500")); // BulletImpact_YELLOW
            colors.Add(HexToColor.GetColorFromString("9B04F5")); // BulletImpact_PURPLE
            colors.Add(HexToColor.GetColorFromString("49F500")); //BulletImpact_GREEN
            colors.Add(HexToColor.GetColorFromString("00F5D4")); //BulletImpact_BLUE

            foreach (var x in colors.Select((value, index) => new { value, index }))
            {
                Color_bulletImpact.Add(x.value, bulletImpactPrefab[x.index]);
                BulletImpact_color.Add(bulletImpactPrefab[x.index], x.value);
            }
        }
    }

 
}
   

