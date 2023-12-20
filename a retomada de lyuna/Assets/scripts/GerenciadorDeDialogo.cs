using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GerenciadorDeDialogo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nomeNpc;
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private TextMeshProUGUI botaoContinua;
    [SerializeField]
    private GameObject caixaDialogo;
    private int cont = 0;
    private Dialogo dialogoAtual;

    void Update(){
        AtivarDialogo();
    }

    public void AtivarDialogo()
    {
        if (GameManager.Instance.gameSpeed == 0 && GameManager.Instance.distancia >= 500)
        {
            // Se a caixa de diálogo não estiver inicializada, inicialize-a
            if (caixaDialogo == null)
            {
                // Substitua "SeuPrefabDialogo" pelo nome do prefab ou objeto da caixa de diálogo em seu projeto.
                caixaDialogo = Instantiate(Resources.Load<GameObject>("SeuPrefabDialogo"));
                caixaDialogo.transform.SetParent(transform); // Você pode ajustar isso conforme necessário.
            }

            // Ative a caixa de diálogo
            caixaDialogo.SetActive(true);
        }
    }
    public void Inicializa(Dialogo dialogo)
    {
        cont = 0;
        dialogoAtual = dialogo;
        ProximaFrase();
    }

    public void ProximaFrase()
    {
        if (dialogoAtual == null)
        {
            return;
        }

        if (cont == dialogoAtual.GetFrases().Length)
        {
            caixaDialogo.gameObject.SetActive(false);
            dialogoAtual = null;
            cont = 0;
            return;
        }
        nomeNpc.text = dialogoAtual.GetNomeNpc();
        texto.text = dialogoAtual.GetFrases()[cont].GetFrase();
        botaoContinua.text = dialogoAtual.GetFrases()[cont].GetBotaoContinua();
        caixaDialogo.gameObject.SetActive(true);
        cont++;

    }

}
