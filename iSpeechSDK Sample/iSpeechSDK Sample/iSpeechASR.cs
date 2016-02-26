using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using iSpeech;

namespace iSpeechSDK_Sample
{
    class iSpeechASR : SpeechRecognizerEvent 
    {

        private string _api;
        private bool _production;
        private static string _freeFormAudio = "counting1.wav";
        private static string _listAudio = "bob.wav";

        public iSpeechASR(String api, bool production)
        {
            _api = api;
            _production = production;

        }
        public  void runFreeForm()
        {           

            iSpeechRecognizer iSpeech = new iSpeechRecognizer(_api, _production);
           
            iSpeech.setFreeForm(iSpeechRecognizer.FREEFORM_DICTATION);

           StreamingBuffer streaming =  iSpeech.startStreamingRecognize("audio/x-wav", this);

           uploadFile(_freeFormAudio, streaming);

           try
           {
               SpeechResult result = iSpeech.stopStreaming();
               Console.WriteLine("Text = " + result.Text + " Confidence = " + result.Confidence);
           }
           catch(Exception e)
           {
               Console.WriteLine("error " + e.ToString());
           }

            

           

        }

        public void runList()
        {



            iSpeechRecognizer iSpeech = new iSpeechRecognizer(_api, _production);

            iSpeech.setFreeForm(iSpeechRecognizer.FREEFORM_DISABLED);

            String[] names = new String[] { "jane", "bob", "john" };
            iSpeech.addAlias("NAMES", names);
            iSpeech.addCommand("call %NAMES%");

            StreamingBuffer streaming = iSpeech.startStreamingRecognize("audio/x-wav", this);
            uploadFile(_listAudio, streaming);

            try
            {
                SpeechResult result = iSpeech.stopStreaming();
                Console.WriteLine("Text = " + result.Text + " Confidence = " + result.Confidence);
            }
            catch (Exception e)
            {
                Console.WriteLine("error " + e.ToString());
            }

            
        }
        public void uploadFile(String file,StreamingBuffer streaming)
        {
             FileStream fs = new FileStream(file, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
              int read = 0;
            long fileSize = fs.Length;
            byte[] buffer = new byte[2048];

            while(fileSize > 0)
            {
               read =  br.Read(buffer, 0, 2048);
               streaming.Write(buffer, 0, read);
               fileSize -= read;

            }

        }
        public override void stateChanged(int eventState, int freeFormValue, Exception lastException)
        {
            if (eventState == RECORDING_COMMITTED)
            {
                Console.WriteLine("eventState = Audio file uploaded");

            }
            else if (eventState == RECORDING_CANCELED)
            {
                Console.WriteLine("eventState = Canceled");
            }
            else if (eventState == RECORDING_ERROR)
            {
                Console.WriteLine("eventState = Error");
            }
            
        }

    }
}
