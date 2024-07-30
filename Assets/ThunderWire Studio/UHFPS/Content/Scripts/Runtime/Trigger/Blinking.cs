using UnityEngine;
using Newtonsoft.Json.Linq;
using ThunderWire.Attributes;
using UHFPS.Rendering;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;
using Unity.VisualScripting;

namespace UHFPS.Runtime
{
    [InspectorHeader("Blinking Effects Set")]
    public class Blinking : MonoBehaviour
    {
        public VolumeComponentReferecne Reference;
    
        public EyeBlink blink;

        private bool blinking;

        private void Awake()
        {

            blink = (EyeBlink)Reference.GetVolumeComponent();
 
        }

        private void Start()
        {
            StartCoroutine(BlinkEffect());
        }

        private void Update()
        {
            if (blinking)
            {
                blink.Blink.value += Time.deltaTime * 4;
            }
            else
            {
                blink.Blink.value -= Time.deltaTime * 4;
            }
            
        }

        IEnumerator BlinkEffect()
        {
            float random = Random.Range(2, 10);
            yield return new WaitForSeconds(random);
            blinking = true;

            yield return new WaitUntil(() => blink.Blink.value == 1);
            blinking = false;

            StartCoroutine(BlinkEffect());
        }

    }
}