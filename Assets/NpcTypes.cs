using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public static class NpcTypes
{
    public  class NPC{
        public  Color color { get; set; }
        public  int spriteNumber { get; set; }
        public  string text1 { get; set; }
        public  string text2 { get; set; }
        public  Item blessing { get; set; }
        public  void GiveBlessing(Unit player) { player.Blessings.Add(blessing); blessingGiven = true; }
        public  string blessingText { get; set; }
        public bool blessingGiven { get; set; }
        public bool itemStolen { get; set; }
    }
    public static Dictionary<string, NPC> friendly = new Dictionary<string, NPC>
    {
        ["fox"] = new NPC
        {
            color = HexToColor("#E79157"),
            spriteNumber = 1,
            text1 = "Verzeihung? Ich sehe, Sie sind keins dieser Monster. Sie kamen urplötzlich vom Nichts und haben meine Heimat zerstört. Sie haben wohl auch kaum etwas, aber darf ich trotzdem um etwas bitten? Ich will auch nicht wählerisch sein.",
            text2 = "Haben Sie vielen Dank, Mensch! Normalerweise wollen Ihresgleichen nur immer.Es tut gut, zu sehen, dass bei Ihnen auch Freundlichkeit herrscht! Falls Sie mal Hilfe brauchen, kommen Sie gerne zu uns.",
            blessing = new Item("foxBless", 1, 0, 0, false),
            blessingText = "Deine Stärke hat sich permanent um 1 erhöht.",
            blessingGiven = false,
            itemStolen = false,
        },
        ["woman"] = new NPC
        {
            color = HexToColor("#BAE757"),
            spriteNumber = 2,
            text1 = "Schon gehört? Ein Krabbenkönig oder so haust umher. Mir eigentlich egal, wenn er nicht den Wald terrorisieren würde. Und dann demolieren dieser Vollidiot noch um mein Hab und Gut. Sorry, aber hast du noch etwas über, oder bin ich in diesem Wald komplett auf mich allein gestellt?",
            text2 = "Sehr schön. Damit kann ich zumindest anfangen, diesen Wald wieder zu reinigen.  Du bist auf dem Weg zum Kloster ? Dann schätze ich mal, kriegst du keinen Besuch von hinten mehr.",
            blessing = new Item("womanBless", 1, 0, 0, false),
            blessingText = "Deine Stärke hat sich permanent um 1 erhöht.",
            blessingGiven = false,
            itemStolen = false,
        },
        ["boi"] = new NPC
        {
            color = HexToColor("#380500"),
            spriteNumber = 0,
            text1 = "Wir, vom Kloster Farsus lernen Frieden durch die Abgabe unseres Eigentums. Wünschst auch du, deinem Frieden einem Stück näher zu kommen, so gib ein Opfer dar.",
            text2 = "Ich sehe, Sie sind auf dem Weg zu etwas Besseren, als das sie vorher mal waren. Umarmen Sie Ihr neues Ich, das sich formt und erleben Sie mehr Sorglosigkeit als zuvor. Friede sei mit Ihnen.",
            blessing = new Item("foxBless", 0, 1, 0, false),
            blessingText = "Du heilst dich ab jetzt mit jedem Angriff.",
            blessingGiven = false,
            itemStolen = false,
        }
    };
    public class Enemy
    {
        public Color color { get; set; }
        public int spriteNumber { get; set; }
        public string text1 { get; set; }
    }

    public static Color HexToColor(string hex)
    {
    ColorUtility.TryParseHtmlString(hex, out var hexColor);
        return hexColor;
    }

}