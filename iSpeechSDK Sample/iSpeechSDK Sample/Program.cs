using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSpeechSDK_Sample
{
    class Program
    {
        private static string api = "developerdemokeydeveloperdemokey";
        private static bool production = true;
        static void Main(string[] args)
        {
            
            iSpeechASR _asr = new iSpeechASR(api,production);
            iSpeechTTS _tts = new iSpeechTTS(api, production);
            Console.WriteLine("Running a TTS \"How are you?\"");
            _tts.run("Hello how are you?");
            Console.WriteLine("TTS Done");
            Console.WriteLine("Running a define list");
            _asr.runList();
            Console.WriteLine("Running free form");
            
            _asr.runFreeForm();
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
