using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Ű������� �����ؾ��� float / bool / int / trigger (AnimatorControllerParameter)
public class AnimationRecord
{
    //�ִϸ��̼� �̸�
    string name;

    float deger_float;
    int deger_int;
    bool bool_deger;

    AnimatorControllerParameterType type;

    //�ִϸ��̼� ����
    public AnimationRecord(string n, float deger, AnimatorControllerParameterType ty)
    {
        deger_float = deger;
        name = n;
        type = ty;
    }

    public AnimationRecord(string n, int deger, AnimatorControllerParameterType ty)
    {
        deger_int = deger;
        name = n;
        type = ty;
    }

    public AnimationRecord(string n, bool deger, AnimatorControllerParameterType ty)
    {
        bool_deger = deger;
        name = n;
        type = ty;
    }

    #region �� ���
    public string Name_
    {
        get
        {
            return name;
        }
    }

    public float Float_
    {
        get
        {
            return deger_float;
        }
    }

    public int Int_
    {
        get
        {
            return deger_int;
        }
    }

    public bool Bool_
    {
        get
        {
            return bool_deger;
        }
    }

    public AnimatorControllerParameterType Type
    {
        get
        {
            return type;
        }
    }
    #endregion
}
