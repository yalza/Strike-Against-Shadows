using UnityEngine;
using UnityEngine.UI;

namespace DATA.Scripts.UI
{
	public class SliderValuePass : MonoBehaviour
	{

		Text _progress;

		// Use this for initialization
		void Start()
		{
			_progress = GetComponent<Text>();
		}

		public void UpdateProgress(float content)
		{
			_progress.text = Mathf.Round(content * 100) + "%";
		}


	}
}
