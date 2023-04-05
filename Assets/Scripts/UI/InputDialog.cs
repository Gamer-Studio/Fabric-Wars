using System;
using TMPro;
using UnityEngine;

namespace FabricWars.Assets.Scripts.UI
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
        public Action<string> onSubmit;
        private string _currentInputValid = "";

        public override void Build(InputDialogData data)
        {
            base.Build(data);
            titleTMP.SetText(data.title);
            descriptionTMP.SetText(data.description);
            onSubmit = data.onSubmit;
        }

        public void OnInputChange(string value) => _currentInputValid = value;
        public void OnSubmitButtonClick() => onSubmit?.Invoke(_currentInputValid);
    }
}