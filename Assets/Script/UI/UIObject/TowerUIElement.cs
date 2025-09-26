using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Tower))]
public class TowerUIElement : ObjectUIElement, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Tower BoundTower { get; private set; }
    private Outline _outLine;

    private void Awake()
    {
        BoundTower = GetComponent<Tower>();

        _outLine ??= GetComponent<Outline>();
        _outLine ??= gameObject.AddComponent<Outline>();

        _outLine.OutlineMode = Outline.Mode.OutlineAll;
        _outLine.OutlineColor = Color.white;
        _outLine.OutlineWidth = 0f;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus.Inst.Publish(new TowerSelectEvent(BoundTower));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
                .SetEase(Ease.OutQuad);
    }
}
