using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThrowableTutorial : TutorialBase
{
    // 2. 조준과 발사
    public override async UniTask ProcessTutorial()
    {
        Debug.Log($"{GetType().Name} Start");
        await UniTask.Delay(5000);
        Debug.Log($"{GetType().Name} End");
    }
}
