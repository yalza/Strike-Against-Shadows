using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DATA.Scripts.UI
{
    public class Login : MonoBehaviour , IPointerClickHandler
    {

        public void LoginOnClick()
        {
            Debug.Log("Login");
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            // Lấy tên của object được click
            string objectName = gameObject.name;

            // Hiển thị tên object trong console
            Debug.Log("Object Clicked: " + objectName);
        }
    }
}
