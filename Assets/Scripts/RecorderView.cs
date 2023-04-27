using TMPro;
using UnityEngine;

namespace ARRecorder
{
    public class RecorderView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI logMessagesText;

        [SerializeField] private ARSessionRecorder arSessionRecorder;


        private void OnEnable()
        {
            if (!arSessionRecorder)
            {
                return;
            }

            arSessionRecorder.OnSendMessage += AddLogMessage_OnSendMessage;
        }

        private void OnDisable()
        {
            if (!arSessionRecorder)
            {
                return;
            }

            arSessionRecorder.OnSendMessage -= AddLogMessage_OnSendMessage;
        }

        private void AddLogMessage_OnSendMessage(string message)
        {
            if (!logMessagesText)
            {
                return;
            }

            logMessagesText.text = message + "\n\n" + logMessagesText.text;
        }

        public  void OnRecordingButtonClicked()
        {
            arSessionRecorder.ToggleRecording();
          
        }

        public void OnPlaybackButtonClicked()
        {
            arSessionRecorder.TogglePlayback();
        }
    }
}