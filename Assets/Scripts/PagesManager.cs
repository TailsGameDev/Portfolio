using UnityEngine;

public class PagesManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform contentRectTransform = null;
    [SerializeField]
    private GameObject workPage = null;
    [SerializeField]
    private GameObject[] allPages = null;

    [SerializeField]
    private HeaderButton backButton = null;
    [SerializeField]
    private HeaderButton[] headerButtons = null;

    [SerializeField]
    private GameElement[] allGameElements = null;


    private GameObject currentPage;


    private void Awake()
    {
        // Initialize all header buttons
        for (int h = 0; h < headerButtons.Length; h++)
        {
            headerButtons[h].Initialize(OnHeaderButtonClick);
        }

        // Set current page
        for (int p = 0; p < allPages.Length; p++)
        {
            if (allPages[p].activeSelf)
            {
                SetCurrentPage(allPages[p]);
                break;
            }
        }

        for (int e = 0; e < allGameElements.Length; e++)
        {
            allGameElements[e].Initialize(OnGameElementClick);
        }
    }
    private void OnHeaderButtonClick(HeaderButton clickedButton)
    {
        SetCurrentPage(clickedButton.PageToActivate);

        if (clickedButton != backButton)
        {
            clickedButton.Select();
        }
        else
        {
            // Select the work button, as all back buttons lead to work page
            for (int h = 0; h < headerButtons.Length; h++)
            {
                HeaderButton loopButton = headerButtons[h];
                bool hasFoundWorkButton = (loopButton.PageToActivate == workPage) && (loopButton != backButton);
                if (hasFoundWorkButton)
                {
                    loopButton.Select();
                    break;
                }
            }
        }
    }

    public void OnGameElementClick(GameElement gameElement)
    {
        if (gameElement.PageToOpen != null)
        {
            SetCurrentPage(gameElement.PageToOpen);
        }
    }

    private void SetCurrentPage(GameObject newCurrentPage)
    {
        currentPage = newCurrentPage;

        // Activate current page and deactivate all others
        for (int p = 0; p < allPages.Length; p++)
        {
            GameObject loopPage = allPages[p];
            loopPage.SetActive(newCurrentPage == loopPage);
        }

        // Enable back button for every page but the work page
        backButton.SetGameObjectActive(currentPage != workPage);

        contentRectTransform.anchoredPosition = Vector2.zero;
    }
}
