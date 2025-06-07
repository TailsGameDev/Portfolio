using UnityEngine;

public class GameElement : MonoBehaviour
{
    [SerializeField]
    private string url = null;
    public void OnPointerDown()
    {
        Application.OpenURL(url);
    }
}
