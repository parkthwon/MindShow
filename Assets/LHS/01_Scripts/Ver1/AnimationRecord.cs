using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//매개변수를 저장해야함 float / bool / int / trigger (AnimatorControllerParameter)
public class AnimationRecord
{
    //애니메이션 이름
    string name;

    float deger_float;
    int deger_int;
    bool bool_deger;

    AnimatorControllerParameterType type;

    //애니메이션 저장
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

    #region 값 얻기
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
