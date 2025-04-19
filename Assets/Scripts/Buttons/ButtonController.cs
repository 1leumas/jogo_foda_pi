using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public static ButtonController Instance { get; private set; }

    private Dictionary<string, GameObject> botoes = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Pega todos os botões filhos automaticamente
        botoes.Clear();
        foreach (Transform child in transform)
        {
            botoes[child.gameObject.name] = child.gameObject;
        }

        DeactivateBtn();
    }

    public void DeactivateBtn()
    {
        foreach (var botao in botoes.Values)
        {
            botao.SetActive(false);
        }
    }

    public void ActivateBtn(string nameBtn, object parameter = null)
    {
        DeactivateBtn();

        if (botoes.TryGetValue(nameBtn, out GameObject botaoGO))
        {
            botaoGO.SetActive(true);

            var receiver = botaoGO.GetComponent<IButtonParameter>();
            if (receiver != null)
            {
                receiver.ReceiveParameter(parameter);
            }
        }
        else
        {
            Debug.LogWarning("Botão não encontrado: " + nameBtn);
        }
    }
}
