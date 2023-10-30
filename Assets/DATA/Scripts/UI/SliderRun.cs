using UnityEngine;
using UnityEngine.UI;

namespace DATA.Scripts.UI
{
	public class SliderRun : MonoBehaviour
	{
		
		Slider _slider;
		public float speed = 0.5f;
		float _time;

		void Start()
		{
			_slider = GetComponent<Slider>();
		}

		void Update()
		{
			_time += Time.deltaTime * speed;
			_slider.value = _time;

			if (_time > 1)
			{
				_time = 0;
			}
		}
	}
}
