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
using Entity = Autodesk.AutoCAD.DatabaseServices.Entity;

namespace CivilAPI.Extensions
{
    public static class EditorExtensions
    {
        public static ObjectId PickObject(this Editor editor, string message = "Select entity: ")
        {
            ObjectId objectId;

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.SingleOnly = true;
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                objectId = editor.PickObject(message);
            }
            else
            {
                objectId = promptSelectionResult.Value[0].ObjectId;
            }
            return objectId;
        }
        public static List<ObjectId> PickObjects(this Editor editor, string message = "Select entities: ")
        {
            List<ObjectId> objectIds = new List<ObjectId>();

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                objectIds = editor.PickObjects(message);
            }
            else
            {
                foreach (SelectedObject selectedObject in promptSelectionResult.Value)
                {
                    objectIds.Add(selectedObject.ObjectId);
                }
            }
            return objectIds;
        }
        public static ObjectId PickObjectOfType(this Editor editor, string dxfTypeName, string message = "Select entity: ")
        {
            ObjectId objectId;

            TypedValue[] typedValue = new TypedValue[1];
            typedValue.SetValue(new TypedValue((int)DxfCode.Start, dxfTypeName), 0);
            SelectionFilter selectionFilter = new SelectionFilter(typedValue);

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.SingleOnly = true;
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions, selectionFilter);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                objectId = editor.PickObjectOfType(dxfTypeName, message);
            }
            else
            {
                objectId = promptSelectionResult.Value[0].ObjectId;
            }
            return objectId;
        }
        public static List<ObjectId> PickObjectsOfType(this Editor editor, string dxfTypeName, string message = "Select entities: ")
        {
            List<ObjectId> objectIds = new List<ObjectId>();

            TypedValue[] typedValue = new TypedValue[1];
            typedValue.SetValue(new TypedValue((int)DxfCode.Start, dxfTypeName), 0);
            SelectionFilter selectionFilter = new SelectionFilter(typedValue);

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions, selectionFilter);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                objectIds = editor.PickObjectsOfType(dxfTypeName, message);
            }
            else
            {
                foreach (SelectedObject selectedObject in promptSelectionResult.Value)
                {
                    objectIds.Add(selectedObject.ObjectId);
                }
            }
            return objectIds;
        }
        public static Entity PickEntity(this Editor editor, Transaction tr, Boolean forRead, string message = "Select entity: ")
        {
            Entity entity = null;
            OpenMode openMode = forRead ? OpenMode.ForRead : OpenMode.ForWrite;

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.SingleOnly = true;
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                entity = editor.PickEntity(tr, forRead, message);
            }
            else
            {
                ObjectId objectId = promptSelectionResult.Value[0].ObjectId;
                entity = tr.GetObject(objectId, openMode) as Entity;
            }
            return entity;
        }
        public static List<Entity> PickEntities(this Editor editor, Transaction tr, Boolean forRead, string message = "Select entities: ")
        {
            List<Entity> entities = new List<Entity>();
            OpenMode openMode = forRead ? OpenMode.ForRead : OpenMode.ForWrite;

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                entities = editor.PickEntities(tr, forRead, message);
            }
            else
            {
                foreach (SelectedObject selectedObject in promptSelectionResult.Value)
                {
                    ObjectId objectId = selectedObject.ObjectId;
                    entities.Add(tr.GetObject(objectId, openMode) as Entity);
                }
            }
            return entities;
        }
        public static Entity PickEntityOfType(this Editor editor, Transaction tr, Boolean forRead, string dxfTypeName, string message = "Select entity: ")
        {
            Entity entity = null;
            OpenMode openMode = forRead ? OpenMode.ForRead : OpenMode.ForWrite;

            TypedValue[] typedValue = new TypedValue[1];
            typedValue.SetValue(new TypedValue((int)DxfCode.Start, dxfTypeName), 0);
            SelectionFilter selectionFilter = new SelectionFilter(typedValue);

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.SingleOnly = true;
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions, selectionFilter);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                entity = editor.PickEntityOfType(tr, forRead, dxfTypeName, message);
            }
            else
            {
                ObjectId objectId = promptSelectionResult.Value[0].ObjectId;
                entity = tr.GetObject(objectId, openMode) as Entity;
            }
            return entity;
        }
        public static List<Entity> PickEntitiesOfType(this Editor editor, Transaction tr, Boolean forRead, string dxfTypeName, string message = "Select entities: ")
        {
            List<Entity> entities = new List<Entity>();
            OpenMode openMode = forRead ? OpenMode.ForRead : OpenMode.ForWrite;

            TypedValue[] typedValue = new TypedValue[1];
            typedValue.SetValue(new TypedValue((int)DxfCode.Start, dxfTypeName), 0);
            SelectionFilter selectionFilter = new SelectionFilter(typedValue);

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.MessageForAdding = message;

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions, selectionFilter);

            if (promptSelectionResult.Status == PromptStatus.Error || promptSelectionResult.Value.Count == 0)
            {
                entities = editor.PickEntitiesOfType(tr, forRead, dxfTypeName, message);
            }
            else
            {
                foreach (SelectedObject selectedObject in promptSelectionResult.Value)
                {
                    ObjectId objectId = selectedObject.ObjectId;
                    entities.Add(tr.GetObject(objectId, openMode) as Entity);
                }
            }
            return entities;
        }
    }
}
