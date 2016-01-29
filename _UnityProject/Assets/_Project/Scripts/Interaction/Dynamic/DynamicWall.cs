using UnityEngine;
using System.Collections;

public class DynamicWall : MonoBehaviour, IDynamic
{
    public float m_Health = 1;

    public void CalculateEffect(GameObject aDamager, DynamicEffect aDamage)
    {
        switch (aDamage.TypeOfEffect)
        {
            case EffectType.loss:
                if (m_Health > 0)
                {
                    m_Health -= aDamage.Value;
                }
                break;
            case EffectType.gain:
                if (m_Health > 0)
                {
                    m_Health += aDamage.Value;
                }
               
                break;
        }

		if (m_Health > 0)
		{
			return;
		}

		m_Health = 0;
		gameObject.SetActive(false);
    }
}
