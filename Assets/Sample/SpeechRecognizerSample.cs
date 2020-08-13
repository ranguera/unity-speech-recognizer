using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpeechRecognizerPlugin
{
    public class SpeechRecognizerSample : MonoBehaviour
    {
        [SerializeField] Text resultText;
        [SerializeField] Text buttonLabel;
        [SerializeField] GameObject loading;
        [SerializeField] string locale = "en-US";
        [SerializeField] Button recordButton;

        string result = null;

        void OnEnable()
        {
            SpeechRecognizer.OnAuthorization += OnAuthorization;
            SpeechRecognizer.OnResult += OnResult;
        }

        void OnDisable()
        {
            SpeechRecognizer.OnAuthorization -= OnAuthorization;
            SpeechRecognizer.OnResult -= OnResult;
        }

        void Start()
        {
            SpeechRecognizer.SetLocale(locale);
            SpeechRecognizer.RequestAuthorization();
        }

        void Update()
        {
            if (SpeechRecognizer.IsRunning)
                loading.transform.Rotate(Vector3.forward, 1f);
        }

        public void OnClickStartButton()
        {
            // Invoked from UI

            if (SpeechRecognizer.IsRunning)
            {
                SpeechRecognizer.Stop();
                buttonLabel.text = "Start Recording";
                recordButton.image.color = new Color(.5f, 1f, .5f);

                if (!string.IsNullOrEmpty(result))
                {
                    resultText.text = result;
                    result = null;
                }
            }
            else
            {
                SpeechRecognizer.Start();
                buttonLabel.text = "Stop Recording";
                recordButton.image.color = new Color(1f, .5f, .5f);
            }
        }

        void OnAuthorization(SpeechRecognizer.AuthorizationStatus status)
        {
            Debug.LogFormat("status : {0}", status);
        }

        void OnResult(string result)
        {
            // this comes from the different thread
            this.result = result;
        }
    }
}

