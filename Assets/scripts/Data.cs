using System;
using UnityEngine;

[Serializable]
public class Data
{
    public string InstructionText;
    public string HelperText;
    public Sprite FirstHelperImage;
    public Sprite SecondHelperImage;
    public Sprite ResultImage;

    public Data(string instructionText, string helperText, Sprite firstHelperImage, Sprite secondHelperImage, Sprite resultImage)
    {
        InstructionText = instructionText;
        HelperText = helperText;
        FirstHelperImage = firstHelperImage;
        SecondHelperImage = secondHelperImage;
        ResultImage = resultImage;
    }
}