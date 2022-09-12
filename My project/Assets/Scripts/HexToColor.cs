using UnityEngine;

public class HexToColor  :MonoBehaviour
{
    private void Awake()
    {
        print("#D771E948");
        Color c = GetColorFromString("#D771E948");
        print(GetStringFromColor(c));
    }

    public static Color GetColorFromString(string hexString)
    {
        float red = HexToFloatNormalized(hexString.Substring(0, 2));
        float green = HexToFloatNormalized(hexString.Substring(2, 2));
        float blue = HexToFloatNormalized(hexString.Substring(4, 2));
        try
        {
            float alpha = HexToFloatNormalized(hexString.Substring(6, 2));
            return new Color(red, green, blue, alpha);
        }
        catch
        {
            return new Color(red, green, blue);
        }
    }

    public static string GetStringFromColor(Color color)
    {
        string red = FloatNormalizedToHex(color.r);
        string green = FloatNormalizedToHex(color.g);
        string blue = FloatNormalizedToHex(color.b);
        string alpha = FloatNormalizedToHex(color.a);
        return red + green + blue + alpha;
    }

    public static string FloatNormalizedToHex(float value)
    {
        return DexToHex(Mathf.RoundToInt(value * 255f));
    }

    public static string DexToHex(int value)
    {
        return value.ToString("X2");
    }

    public static float HexToFloatNormalized(string hex)
    {
        return HexToDec(hex) / 255f;
    }

    public static int HexToDec(string hex)
    {
        int dec = System.Convert.ToInt32(hex, 16);
        return dec;
    }
}
