using UnityEngine;

namespace Crosstales.RTVoice.Demo.Util
{
    /// <summary>Random color changer.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_util_1_1_random_color.html")]
    [RequireComponent(typeof(Renderer))]
    public class RandomColor : MonoBehaviour
    {

        #region Variables

        public Vector2 ChangeInterval = new Vector2(5, 15);

        private float elapsedTime = 0f;
        private float changeTime = 0f;
        private Renderer currentRenderer;
        private Color32 startColor;
        private Color32 endColor;
        private float lerpTime = 0f;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            currentRenderer = GetComponent<Renderer>();

            elapsedTime = changeTime = Random.Range(ChangeInterval.x, ChangeInterval.y);

            startColor = currentRenderer.material.color;
        }

        public void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > changeTime)
            {
                endColor = RTVoice.Util.Helper.HSVToRGB(Random.Range(0.0f, 360f), 1, 1);

                changeTime = Random.Range(ChangeInterval.x, ChangeInterval.y);

                lerpTime = elapsedTime = 0f;
            }

            currentRenderer.material.color = Color.Lerp(startColor, endColor, lerpTime);

            if (lerpTime < 1f)
            {
                lerpTime += Time.deltaTime / (changeTime - 0.2f);
            }
            else
            {
                startColor = currentRenderer.material.color;
            }
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)