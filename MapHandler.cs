using EGIS.Controls;
using EGIS.ShapeFileLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace The_BEST_US_Swingometer
{
    public class MapHandler
    {
        public EGIS.Controls.SFMap theMap;
        public string filePath;
        public string att;
        public List<Area> areas = new List<Area>();
        public Dictionary<string, Color> areaDic = new Dictionary<string, Color>();
        public ShapeFile shapes = new ShapeFile();

        public MapHandler(EGIS.Controls.SFMap theMap, string filePath, List<Area> areas)
        {
            this.theMap = theMap;
            this.filePath = filePath;
            this.areas = areas;
        }
        public void Colouring()
        {

            foreach (Area x in areas)
            {
                theMap.ClearShapeFiles();

                if (x.type == "Presidential" | x.type == "Senate" | x.type == "Governors")
                {
                    areaDic.Add(x.name, x.realColour);
                    att = "NAME";
                }
                else if (x.type == "House")
                {
                    areaDic.Add(x.shortName, x.realColour);
                    att = "id";
                }
                
            }
            shapes = new ShapeFile(filePath, true);
            theMap.AddShapeFile(shapes, true);

            shapes.RenderSettings.CustomRenderSettings = new CustomColour(shapes, att, areaDic, areas);
            shapes.RenderSettings.UseToolTip = true;
            shapes.RenderSettings.IsSelectable = true;
            theMap.Refresh();
        }
    }
}
