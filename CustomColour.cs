using EGIS.ShapeFileLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
using The_BEST_US_Swingometer;

public class CustomColour : ICustomRenderSettings
{

    public bool UseCustomTooltips => true;

    private readonly List<Area> areas;
    private readonly Dictionary<string, Color> highlightStates;
    private readonly int fieldIndex;
    private readonly DbfReader dbf;

    public CustomColour(ShapeFile shapeFile, string attributeName,
                                   Dictionary<string, Color> statesToHighlight, List<Area> areas)
    {
        this.areas = areas;
        
        // Case-insensitive dictionary for matching
        highlightStates = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);
        foreach (var kvp in statesToHighlight)
        {
            highlightStates[kvp.Key.Trim()] = kvp.Value;
        }

        dbf = shapeFile.RenderSettings.DbfReader;
        string[] fields = dbf.GetFieldNames();
        fieldIndex = Array.FindIndex(fields, f =>
            string.Equals(f, attributeName, StringComparison.OrdinalIgnoreCase));
        if (fieldIndex == -1)
            throw new ArgumentException($"Field '{attributeName}' not found.");
        this.areas = areas;
    }

    // --- Core logic: check dictionary for a match ---
    public Color GetRecordFillColor(int shapeIndex)
    {
        //MessageBox.Show(UseCustomTooltips.ToString());
        string val = dbf.GetField(shapeIndex, fieldIndex)?.Trim();
        if (val != null && highlightStates.TryGetValue(val, out var color))
        {
            return color; // special colour for this state
        }

        return Color.Gray; // fall back to layer’s default fill
    }

    
    public string GetRecordToolTip(int shapeIndex)
    {
        
        string x = dbf.GetField(shapeIndex, fieldIndex)?.Trim();
        
        //hardcoding areas for maine and nebraska pres
        Area maineOne = areas.Find(areas => areas.name == "Maine 1st");
        Area maineTwo = areas.Find(areas => areas.name == "Maine 2nd");
        
        Area nebraskaOne = areas.Find(areas => areas.name == "Nebraska 1st");
        Area nebraskaTwo = areas.Find(areas => areas.name == "Nebraska 2nd");
        Area nebraskaThree = areas.Find(areas => areas.name == "Nebraska 3rd");

        foreach (Area a in areas)
        {
            //MessageBox.Show(a.name + a.shortName);
            if (a.name == x && (a.type == "Presidential" | a.type == "Senate"))
            {
                if (a.name == "Maine" && a.type == "Presidential")
                {
                    return a.name + "\n" + a.finalResult + "\n" + a.winningMargin.complete + "\n\n" +
                        maineOne.name + "\n" + maineOne.finalResult + "\n" + maineOne.winningMargin.complete + "\n\n" +
                        maineTwo.name + "\n" + maineTwo.finalResult + "\n" + maineTwo.winningMargin.complete;
                }
                else if (a.name == "Nebraska" && a.type == "Presidential")
                {
                    return a.name + "\n" + a.finalResult + "\n" + a.winningMargin.complete + "\n\n" +
                        nebraskaOne.name + "\n" + nebraskaOne.finalResult + "\n" + nebraskaOne.winningMargin.complete + "\n\n" +
                        nebraskaTwo.name + "\n" + nebraskaTwo.finalResult + "\n" + nebraskaTwo.winningMargin.complete + "\n\n" +
                        nebraskaThree.name + "\n" + nebraskaThree.finalResult + "\n" + nebraskaThree.winningMargin.complete;
                }


                return a.name + "\n" + a.finalResult + "\n" + a.winningMargin.complete;
            }
            else if (a.shortName == x && a.type == "House")
            {
                return a.name + "\n" + a.finalResult + "\n" + a.winningMargin.complete;
            }
            else
            {
                continue;
            }
        }
        
        return "shouldn't be here!!";
    }


    // --- Boilerplate stubs (adjust signatures to match your DLL) ---
    public Color GetRecordOutlineColor(int shapeIndex) => Color.Black;
    public Color GetRecordFontColor(int shapeIndex) => Color.Black;
    public bool RenderShape(int shapeIndex) => true;
    public Image GetRecordImageSymbol(int shapeIndex) => null;
    public string GetRecordLabel(int shapeIndex) => null;
    public int GetDirection(int shapeIndex) => 0;

    // Properties
    public bool UseCustomRenderSymbols => false;
    public bool UseCustomRecordLabels => false;
    public bool UseCustomImageSymbols => false;
}


