using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Autodesk.Civil.Settings;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Entity = Autodesk.AutoCAD.DatabaseServices.Entity;
using CivilAPI.Extensions;

namespace CivilAPI
{
    public class App
    {
        [CommandMethod("ExtractSurfaces")]
        public void ExtractSurfaces()
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Editor editor = document.Editor;

            editor.WriteMessage("Extracting surfaces...\n");

            // Ensure the output directory exists
            string outputDirectory = "D:\\Surfaces";
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            database.Run(tr =>
            {
                TinSurface surface = editor.PickEntityOfType(tr, true, "AECC_TIN_SURFACE", "Select surface: ") as TinSurface;
                editor.WriteMessage($"Surface Name: {surface.Name}\n");

                List<Entity> polylines = editor.PickEntitiesOfType(tr, true, "LWPOLYLINE", "Select polylines: ");
                editor.WriteMessage($"Polylines: {polylines.Count}\n");

                foreach (Polyline polyline in polylines) 
                {
                    List<Point2d> points = polyline.GetPoints();
                    editor.WriteMessage($"Polyline Points: {points.Count}\n");
                    Point2dCollection point2dCol = new Point2dCollection(points.ToArray());
                }

            });

        }
    }
}
