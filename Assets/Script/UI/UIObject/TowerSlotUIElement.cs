using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TowerSlot))]
public class TowerSlotUIElement : ObjectUIElement, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TowerSlot BoundTowerSlot { get; private set; }
    private Outline _outLine;

    private void Awake()
    {
        BoundTowerSlot = GetComponent<TowerSlot>();

        _outLine ??= GetComponent<Outline>();
        _outLine ??= gameObject.AddComponent<Outline>();

        _outLine.OutlineMode = Outline.Mode.OutlineAll;
        _outLine.OutlineColor = Color.white;
        _outLine.OutlineWidth = 0f;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus.Inst.Publish(new TowerSlotSelectEvent(BoundTowerSlot));
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _outLine.enabled = true;
        DOTween.To(() => _outLine.OutlineWidth,
                    x => _outLine.OutlineWidth = x,
                    5f,
                    0.5f)
                .SetEase(Ease.OutQuad);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.To(() => _outLine.OutlineWidth,
                    x => _outLine.OutlineWidth = x,
                    0f,
                    0.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => _outLine.enabled = false);
    }
}
