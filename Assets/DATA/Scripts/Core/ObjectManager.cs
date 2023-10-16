using System.Collections;
using UnityEngine;

namespace DATA.Scripts.Core
{
    public class ObjectManager : Singleton<ObjectManager>
    {
        public void StartDelayDeactive(float time, GameObject obj)
        {
            StartCoroutine(IEDelayDeactive(time, obj));
        }
        
        IEnumerator IEDelayDeactive(float time, GameObject obj)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(false);
        }
    }
}