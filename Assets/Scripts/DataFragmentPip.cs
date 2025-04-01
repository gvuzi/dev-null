using UnityEngine;
using UnityEngine.UI;

public class DataFragmentPip : MonoBehaviour
{
    public Image dataFragmentSprite;
    public float animationReduceSpeed = 2;
    private float target = 0; // start of fill amount

    public void Animate() {
        target = 1;
    }

    void Update() {
        dataFragmentSprite.fillAmount = Mathf.MoveTowards(dataFragmentSprite.fillAmount, target, animationReduceSpeed * Time.deltaTime);
    }
}
