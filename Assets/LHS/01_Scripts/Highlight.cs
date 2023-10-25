using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    //������ ���ϰ� �� ����.
    //�� ���η����� �׵θ��� ���ϰ� ����
    [SerializeField]
    private List<Renderer> renderers;

    //������ ��
    [SerializeField]
    private Color color;

    //Material�� ����
    private List<Material> materials;

    //�� �������� Material ��� ��������
    private void Awake()
    {
        materials = new List<Material>();

        foreach (var renderer in renderers)
        {
            //Randerer -> Materials(������ ������ ���� �� ����)
            //Add -> ��� �ϳ�. AddRange�� ����(�迭, List ��) �߰�
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    //�÷��̾�� Ray�� �� ������ �� ����.
    public void ToggleHighlight(bool val)
    {
        // ������
        if (val)
        {
            foreach (var material in materials)
            {
                //Emission�� �������� �����ϰ� ����.

                //Ȱ��ȭ �� Ű����
                //�� �� ������Ʈ�� Ȱ��ȭ �Ǿ���
                material.EnableKeyword("_EMISSION");
                //���� �����ϱ� ���� 
                material.SetColor("_EmissionColor", color);
                print("�������ϰ� ����");
            }
        }

        //������
        else
        {
            foreach (var material in materials)
            {
                //REMINSE�� ��Ȱ��ȭ�ϸ� �˴ϴ�
                //�ٸ� ������ ���� ������ ������� ������
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
