using FabricWars.Assets.Scripts.UI;
using FabricWars.Utils.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class DialogManager : DDOLSingletonMonoBehaviour<DialogManager>
{
    [Header("Preset")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject baseDialogPref, inputDialogPref;

    public void Show(BaseDialogData data)
    {
        BaseDialog baseDialog = Instantiate(baseDialogPref, canvas.transform).GetComponent<BaseDialog>();
        baseDialog.Build(data);
    }

    public void ShowInputDialog(InputDialogData data)
    {
        InputDialog inputDialog = Instantiate(inputDialogPref, canvas.transform).GetComponent<InputDialog>();
        inputDialog.Build(data);
    }
}