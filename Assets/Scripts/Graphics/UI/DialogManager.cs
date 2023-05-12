using System;
using System.Collections.Generic;
using FabricWars.Utils.Singletons;
using UnityEngine;

namespace FabricWars.Graphics.UI
{
    public class DialogManager : DDOLSingletonMonoBehaviour<DialogManager>
    {
        [Header("Preset")] [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject baseDialogPref;
        [SerializeField] private GameObject inputDialogPref;
        private Dictionary<Type, Dialog> dialogCache = new();

        public void Show(BaseDialogData data)
        {
            if (!dialogCache.TryGetValue(typeof(BaseDialog), out var dialog))
                dialogCache[typeof(BaseDialog)] =
                    Instantiate(baseDialogPref, canvas.transform).GetComponent<BaseDialog>();

            if (dialog is not BaseDialog baseDialog)
                dialogCache[typeof(BaseDialog)] =
                    Instantiate(baseDialogPref, canvas.transform).GetComponent<BaseDialog>();
            else baseDialog.Build(data);
        }


        public void ShowInputDialog(InputDialogData data)
        {
            if (!dialogCache.TryGetValue(typeof(InputDialog), out var dialog) || dialog is not InputDialog inputDialog)
            {
                dialogCache[typeof(InputDialog)] = inputDialog =
                    Instantiate(inputDialogPref, canvas.transform).GetComponent<InputDialog>();
            }

            inputDialog.Build(data);
        }

        public bool IsTypeAlive<T>() where T : Dialog =>
            dialogCache.TryGetValue(typeof(T), out var dialog) && dialog.gameObject.activeSelf;
    }
}