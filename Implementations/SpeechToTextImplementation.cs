using AVFoundation;
using Foundation;
using GI.Common.XF.Interfaces;
using GI.Common.XF.iOS.Implementation;
using Speech;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechToTextImplementation))]
namespace GI.Common.XF.iOS.Implementation
{
    public class SpeechToTextImplementation : ISpeechToText
    {

        #region Private Variables
        private AVAudioEngine AudioEngine = new AVAudioEngine();
        private SFSpeechRecognizer SpeechRecognizer = new SFSpeechRecognizer();
        private SFSpeechAudioBufferRecognitionRequest LiveSpeechRequest = new SFSpeechAudioBufferRecognitionRequest();
        private SFSpeechRecognitionTask RecognitionTask;
        #endregion

        public async Task SpeechToText()
        {
            // Request user authorization
            SFSpeechRecognizer.RequestAuthorization((SFSpeechRecognizerAuthorizationStatus status) =>
            {
                // Take action based on status
                switch (status)
                {
                    case SFSpeechRecognizerAuthorizationStatus.Authorized:
                        // User has approved speech recognition
                        StartRecording();
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.Denied:
                        // User has declined speech recognition
                        CancelRecording();
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.NotDetermined:
                        // Waiting on approval
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.Restricted:
                        // The device is not permitted
                        break;
                }
            });
        }

        public void StartRecording()
        {
            // Setup audio session
            var node = AudioEngine.InputNode;
            var recordingFormat = node.GetBusOutputFormat(0);
            node.InstallTapOnBus(0, 1024, recordingFormat, (AVAudioPcmBuffer buffer, AVAudioTime when) =>
            {
                // Append buffer to recognition request
                LiveSpeechRequest.Append(buffer);
            });

            // Start recording
            AudioEngine.Prepare();
            NSError error;
            AudioEngine.StartAndReturnError(out error);

            // Did recording start?
            if (error != null)
            {
                // Handle error and return
                return;
            }

            // Start recognition
            RecognitionTask = SpeechRecognizer.GetRecognitionTask(LiveSpeechRequest, (SFSpeechRecognitionResult result, NSError err) =>
            {
                // Was there an error?
                if (err != null)
                {
                    // Handle error
                }
                else
                {
                    // Is this the final translation?
                    if (result.Final)
                    {
                        //AudioPage.AudioText(result.BestTranscription.FormattedString, AudioPage.TextComment);
                    }
                }
            });
        }

        public void StopRecording()
        {
            AudioEngine.Stop();
            LiveSpeechRequest.EndAudio();
        }

        public void CancelRecording()
        {
            AudioEngine.Stop();
            RecognitionTask.Cancel();
        }
    }
}