using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;

namespace CivilAPI.Extensions
{
    public static class DatabaseExtensions
    {
        public static void Run(this Database database, Action<Transaction> action)
        {
            using (Transaction tr = database.TransactionManager.StartTransaction())
            {
                try
                {
                    action.Invoke(tr);
                    tr.Commit();
                }
                catch
                {
                    tr.Abort();
                    throw new NotImplementedException();
                }
            }
        }

        public static List<TinSurface> GetSurfaces(this Database database, Transaction tr, Boolean forRead)
        {
            OpenMode openMode = forRead ? OpenMode.ForRead : OpenMode.ForWrite;

            CivilDocument civilDocument = CivilDocument.GetCivilDocument(database);
            if (civilDocument == null)
            {
                throw new ArgumentNullException("CivilDocument cannot be null.");
            }

            ObjectIdCollection surfaceIds = civilDocument.GetSurfaceIds();
            if (surfaceIds.Count == 0)
            {
                throw new InvalidOperationException("No surfaces found in the Civil Document.");
            }

            List<TinSurface> surfaces = new List<TinSurface>();
            foreach (ObjectId surfaceId in surfaceIds)
            {
                TinSurface surface = tr.GetObject(surfaceId, openMode) as TinSurface;
                if (surface != null)
                {
                    surfaces.Add(surface);
                }
            }

            return surfaces;
        }


        public static TinSurface GetSurfaceByName(this Database database, Transaction tr, Boolean forRead, string name)
        {
            OpenMode openMode = forRead ? OpenMode.ForRead : OpenMode.ForWrite;

            CivilDocument civilDocument = CivilDocument.GetCivilDocument(database);
            if (civilDocument == null)
            {
                throw new ArgumentNullException("CivilDocument cannot be null.");
            }

            ObjectIdCollection surfaceIds = civilDocument.GetSurfaceIds();
            if (surfaceIds.Count == 0)
            {
                throw new InvalidOperationException("No surfaces found in the Civil Document.");
            }

            TinSurface result = null;

            foreach (ObjectId surfaceId in surfaceIds)
            {
                TinSurface surface = tr.GetObject(surfaceId, openMode) as TinSurface;
                if (surface != null && surface.Name == name)
                {
                    result = surface;
                    break;
                }
            }

            return result; 
        }
    }
}
