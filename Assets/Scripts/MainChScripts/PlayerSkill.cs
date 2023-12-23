using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill instance;
    public GameObject SkillUI;

    public int skill1Level;
    public int skill2Level;
    public int skill3Level;
    public int skill4Level;

    public int unusedSkillPoints;

    private void Awake()
    {
        instance = this;
    }
    public void UpgradeSkill1()
    {
        if (unusedSkillPoints >= 1 && skill1Level < 5) { unusedSkillPoints--; skill1Level++; } else Debug.Log("Max LEVEL ");
    }
    public void UpgradeSkill2() 
    {
        if (unusedSkillPoints >= 1 && skill2Level < 5) { unusedSkillPoints--; skill2Level++; } else Debug.Log("Max LEVEL");
    }
    public void UpgradeSkill3() 
    {
        if (unusedSkillPoints >= 1 && skill3Level < 5) { unusedSkillPoints--; skill3Level++; } else Debug.Log("Max LEVEL");
    }
    public void UpgradeSkill4() 
    {
        if (unusedSkillPoints >= 1 && skill4Level < 5) { unusedSkillPoints--; skill4Level++; } else Debug.Log("Max LEVEL");
    }

  
}
