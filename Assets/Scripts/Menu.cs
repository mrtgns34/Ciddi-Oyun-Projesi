using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    GameObject playerNamePanel;
    [SerializeField]
    GameObject characterselectPanel;
    [SerializeField]
    GameObject weaponselectPanel;
    [SerializeField]
    GameObject equipmentselectPanel;
    [SerializeField]
    GameObject difficultyselectPanel;

    [Header("Areas")]
    [SerializeField]
    InputField nameArea;


    [Header("Camera Settings")]
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Transform[] camPositions;
    Vector3 cameraFirstPosition;

    int selectedCharacter = 0;
    int selectedWeapon = 0;
    int selectedEquipment = 0;
    int selectedDifficulty = 0;

    [Header("Buttons")]
    [SerializeField]
    Image[] weaponButtons;
    [SerializeField]
    Image[] equipmentButtons;
    [SerializeField]
    Image[] difficultyButtons;

    private void Awake()
    {
        GetPlayerData();
    }
    #region buttons
    public void Continue(int i)
    {
        switch (i)
        {
            case 1:
                PlayerPrefs.SetString("name", nameArea.text);

                playerNamePanel.SetActive(false);
                characterselectPanel.SetActive(true);

                cameraFirstPosition = cameraTransform.position;
                StartCoroutine(MoveToCameraPosition(camPositions[selectedCharacter].position));


                break;
            case 2:

                PlayerPrefs.SetInt("character", selectedCharacter);

                characterselectPanel.SetActive(false);
                weaponselectPanel.SetActive(true);

                StartCoroutine(MoveToCameraPosition(cameraFirstPosition));
                break;
            case 3:
                PlayerPrefs.SetInt("weapon", selectedWeapon);

                weaponselectPanel.SetActive(false);
                equipmentselectPanel.SetActive(true);
                break;
            case 4:
                PlayerPrefs.SetInt("equipment", selectedEquipment);

                equipmentselectPanel.SetActive(false);
                difficultyselectPanel.SetActive(true);
                break;
            case 5:
                PlayerPrefs.SetInt("difficulty", selectedDifficulty);

                difficultyselectPanel.SetActive(false);

                SceneManager.LoadScene(1);
                break;
        }
    }
    public void SelectWeapon(int i)
    {
        selectedWeapon = i;
        for(int j = 0; j < weaponButtons.Length; j++)
        {
            if (j == i)
            {
                weaponButtons[j].color = Color.green;
            }
            else
            {
                weaponButtons[j].color = Color.white;
            }
        }
    }
    public void SelectEquipment(int i)
    {
        selectedEquipment = i;
        for (int j = 0; j < equipmentButtons.Length; j++)
        {
            if (j == i)
            {
                equipmentButtons[j].color = Color.green;
            }
            else
            {
                equipmentButtons[j].color = Color.white;
            }
        }
    }
    public void SelectDifficulty(int i)
    {
        selectedDifficulty = i;
        for (int j = 0; j < difficultyButtons.Length; j++)
        {
            if (j == i)
            {
                difficultyButtons[j].color = Color.green;
            }
            else
            {
                difficultyButtons[j].color = Color.white;
            }
        }
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void SelectCharacterRight()
    {
        if (selectedCharacter < camPositions.Length - 1)
        {
            selectedCharacter++;
        }
        else
        {
            selectedCharacter = 0;
        }
        StartCoroutine(MoveToCameraPosition(camPositions[selectedCharacter].position));
    }
    public void SelectCharacterLeft()
    {
        if (selectedCharacter > 0)
        {
            selectedCharacter--;
        }
        else
        {
            selectedCharacter = camPositions.Length - 1;
        }
        StartCoroutine(MoveToCameraPosition(camPositions[selectedCharacter].position));

    }
    #endregion
    IEnumerator MoveToCameraPosition(Vector3 finalPosition)
    {
        Vector3 startingPos = cameraTransform.position;
        float timer = 0;

        while (timer < 2)
        {
            cameraTransform.position = Vector3.Lerp(startingPos, finalPosition, (timer / 2));
            timer += Time.deltaTime;
            yield return null;
        }
    }
    private void GetPlayerData()
    {
        nameArea.text = PlayerPrefs.GetString("name");
        selectedCharacter = PlayerPrefs.GetInt("character");
        selectedWeapon = PlayerPrefs.GetInt("weapon");
        selectedEquipment = PlayerPrefs.GetInt("equipment");
        selectedDifficulty = PlayerPrefs.GetInt("difficulty");
        weaponButtons[selectedWeapon].color = Color.green;
        equipmentButtons[selectedEquipment].color = Color.green;
        difficultyButtons[selectedDifficulty].color = Color.green;
    }
}
