using UnityEngine;

public class GenerateCardPanals : GeneratePanals
{
    [SerializeField] private Material _materialTemp;

    protected override void ChildSettingPanal(CardPanal panal)
    {
        panal.GetComponent<UnityEngine.UI.Image>().material = new Material(_materialTemp);
    }
}
