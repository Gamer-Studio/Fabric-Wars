using System;
using TMPro;
using UnityEngine;

namespace FabricWars.Graphics.UI
{
    public struct InputDialogData
    {
        public string title;
        public string description;
        public Action<string> onSubmit;
    }

    public class InputDialog : Dialog<InputDialogData>
    {
        [SerializeField] private TextMeshProUGUI titleTMP, descriptionTMP;
        [SerializeField] private TMP_InputField field;

        public Action<string> onSubmit;

        public override void Build(InputDialogData data)
        {
            base.Build(data);
            titleTMP.SetText(data.title);
            descriptionTMP.SetText(data.description);
            onSubmit = data.onSubmit;
        }

        public void OnSubmitButtonClick()
        {
            onSubmit?.Invoke(field.text);
            
            Close();
        }
    }
}