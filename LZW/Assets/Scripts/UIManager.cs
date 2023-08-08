using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject losslessPanel;
    public GameObject lossyPanel;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI gainText;
    public TextMeshProUGUI compressionRateText;
    public TextMeshProUGUI capacityText;
    public TMP_InputField losslessInputField;
    public TMP_InputField lossyInputField;

    private string inputText;

    private CodingManager codingManager;

    private void Awake()
    {
        codingManager = GetComponent<CodingManager>();
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
    }

    public void HideMainPanel()
    {
        mainPanel.SetActive(false);
    }
    
    public void ShowLosslessPanel()
    {
        losslessPanel.SetActive(true);
    }

    public void HideLosslessPanel()
    {
        losslessPanel.SetActive(false);
    }
    
    public void ShowLossyPanel()
    {
        lossyPanel.SetActive(true);
    }

    public void HideLossyPanel()
    {
        lossyPanel.SetActive(false);
    }
    
    public void ShowResultPanel(bool isLossless)
    {
        resultPanel.SetActive(true);
        
        if (isLossless)
        {
            SetResultText(codingManager.EncodeAndDecode(inputText));
        }
        else
        {
            inputText = CodingManager.PrepareForLossyEncoding(inputText);
            SetResultText(codingManager.EncodeAndDecode(inputText));
        }
    }

    public void HideResultPanel()
    {
        resultPanel.SetActive(false);
    }

    public void SetResultText(string value)
    {
        resultText.text = value;
    }

    
    public void SetGainText(bool isLossless)
    {
        if (isLossless)
        {
            gainText.text = "Gain:\n%" + (CodingManager.CalculateGainPercentage(LZW.Encode(losslessInputField.text), DefaultEncoding.Encode(losslessInputField.text))).ToString("F2");
        }
        else
        {
            gainText.text = "Gain:\n%" + (CodingManager.CalculateGainPercentage(LZW.Encode(lossyInputField.text), DefaultEncoding.Encode(lossyInputField.text))).ToString("F2");
        }
        
    }

    public void SetCompressionRateText(bool isLossless)
    {
        if (isLossless)
        {
            compressionRateText.text = "Compression Rate:\n%" + CodingManager.CalculateCompressionRatePercentage(LZW.Encode(losslessInputField.text), DefaultEncoding.Encode(losslessInputField.text)).ToString("F2");
        }
        else
        {
            compressionRateText.text = "Compression Rate:\n%" + CodingManager.CalculateCompressionRatePercentage(LZW.Encode(lossyInputField.text), DefaultEncoding.Encode(lossyInputField.text)).ToString("F2");    
        }
    }

    public void SetChannelCapacityText()
    {
        capacityText.text = "Utilisation of\nChannel Capacity:\n%" + codingManager.CalculateChannelCapacityUtilisationPercentage().ToString("F2");
    }

    public void ReadStringInput(string value)
    {
        inputText = value;
    }
    
    public void ClearInputFieldsAndResultText()
    {
        losslessInputField.text = "";
        lossyInputField.text = "";
        resultText.text = "";
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
