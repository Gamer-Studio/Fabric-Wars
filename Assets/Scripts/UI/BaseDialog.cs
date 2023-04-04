﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace FabricWars.Assets.Scripts.UI
{
    public struct BaseDialogData
    {
        public string title;
        public string description;
        public Action<BaseDialog> onConfirmed, onDismissed;
    }

    public class BaseDialog : Dialog<BaseDialogData>
    {
        [SerializeField] private TextMeshProUGUI titleTMP, descriptionTMP;
        public Action<BaseDialog> onConfirmed, onDismissed;

        public override void Build(BaseDialogData data)
        {
            base.Build(data);
            titleTMP.SetText(data.title);
            descriptionTMP.SetText(data.description);
            onConfirmed = data.onConfirmed;
            onDismissed = data.onDismissed;
        }

        public void OnConfirmed()
        {
            onConfirmed?.Invoke(this);
        }
        public void OnDismissed()
        {
            onDismissed?.Invoke(this);
            Close();
        }
    }
}