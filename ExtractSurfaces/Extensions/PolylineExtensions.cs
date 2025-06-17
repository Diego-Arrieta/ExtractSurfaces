using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.Civil.DatabaseServices;

namespace CivilAPI.Extensions
{
    public static class PolylineExtensions
    {
        public static List<Point2d> GetPoints(this Polyline polyline)
        {
            List<Point2d> points = new List<Point2d>();

            if (polyline != null)
            {
                for (int i = 0; i < polyline.NumberOfVertices; i++)
                {
                    Point2d point = polyline.GetPoint2dAt(i);
                    points.Add(point);
                }
            }

            return points;
        }
    }
}
