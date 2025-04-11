using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyReminderTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject difficultyReminder;

    private void Update()
    {
        if (DifficultyManager.IsDifficultySelected) { difficultyReminder.SetActive(false); gameObject.SetActive(false); }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        difficultyReminder.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        difficultyReminder.SetActive(false);
    }
}
