using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using iSpeech;



namespace iSpeechSDK_Sample
{
    class iSpeechTTS
    {
        private string _api;
        private bool _production;

        public iSpeechTTS(string api, bool production)
        {
            _api = api;
            _production = production;
        }
        
        public void run(string text)
        {
          iSpeechSynthesis iSpeech=  iSpeechSynthesis.getInstance(_api, _production);

           iSpeech.setVoice("usenglishfemale");
           iSpeech.setOptionalCommands("format", "mp3");
            
           TTSResult result = iSpeech.speak(text);         
            
           byte [] audioData = new byte[result.getAudioFileLength()];

           int read = 0;
           int totalRead = 0;

           while (totalRead < audioData.Length)
           {
               read = result.getStream().Read(audioData, totalRead, audioData.Length - totalRead);
               totalRead += read;
           }


           FileStream fs = new FileStream("audio.wav", FileMode.Create);
           BinaryWriter bw = new BinaryWriter(fs);

           bw.Write(audioData, 0, audioData.Length);           

        }
  
    }
}
