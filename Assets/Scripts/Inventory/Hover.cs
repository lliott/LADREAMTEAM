using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //fonctionne pas tant que y a pas un premier clic ?
    private Vector3 _scaleInit;

    void Start()
    {
        _scaleInit = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _scaleInit * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _scaleInit;
    }
}
