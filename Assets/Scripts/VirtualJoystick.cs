using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 inputDirection { get; set; }

    private Image backgroundImage;
    private Image joystickImage;

    private void Start()
    {
        inputDirection = Vector2.zero;

        backgroundImage = GetComponent<Image>();
        joystickImage = this.transform.GetChild(0).GetComponentInChildren<Image>(); //Bierzemy to z pierwszego dziecka
    }


    public virtual void OnDrag(PointerEventData data)
    {
        Vector2 pos = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, data.position, data.enterEventCamera, out pos)) //jeśli trafiliśmy w środek backgroundImage to możemy iść dalej ze skryptem
        {
            //Wyciągamy proporcje z pozycji
            pos.x = pos.x / backgroundImage.rectTransform.sizeDelta.x;
            pos.y = pos.y / backgroundImage.rectTransform.sizeDelta.y;

            //Przepisujemy info do inputa 
            inputDirection = new Vector2(pos.x * 2f, pos.y * 2f); //razy 2, ponieważ wtedy można dojść do 1.0 bez wychodzenia za obszar
            //Jeśli trzeba to normalizujemy input
            inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

            //Przesuwamy obrazek joysticka
            joystickImage.rectTransform.anchoredPosition = new Vector2(inputDirection.x * backgroundImage.rectTransform.sizeDelta.x / 3, inputDirection.y * backgroundImage.rectTransform.sizeDelta.y / 3);
        }
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        //Wzywamy OnDrag żeby przesuwać to joystick zwykłym przytrzymaniem
        OnDrag(data);
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
        inputDirection = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
    }
}
