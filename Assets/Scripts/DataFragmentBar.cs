using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataFragmentBar : MonoBehaviour
{
    public Player player;
    public List<DataFragmentPip> pips = new List<DataFragmentPip>();
    private int lastFragment = 0;

    void Update()
    {
        for(int i = 0; i < pips.Count; i++) {
            if(i < player.dataFragmentsCollected) {
                pips[i].Animate();
            }
        }
    }
}
