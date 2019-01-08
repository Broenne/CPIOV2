using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTest
{
    using System.IO;
    using System.Net;
    using System.Speech.AudioFormat;
    using System.Speech.Recognition;

    using Telegram.Bot;
    using Telegram.Bot.Types.Enums;

    public class Program
    {
        private static TelegramBotClient client;

        static void Main(string[] args)
        {


            client = new TelegramBotClient("797920340:AAFWIouhKTBNVrSLPcCy0ieMrDX3nH4Q6CY");
           
            var me = client.GetMeAsync();
            //Console.WriteLine($"{me.Username} started");

            // zum gucken der chat id (man muss vorher was posten)
            // https://api.telegram.org/bot797920340:AAFWIouhKTBNVrSLPcCy0ieMrDX3nH4Q6CY/getUpdates


            var msgAsync =  client.SendTextMessageAsync(768632323, "Hallo Daniel");

            client.OnMessage += Client_OnMessage;

            if (!client.IsReceiving)
            {
                client.StartReceiving();
            }

            while (true)
            {

            };
        }

        private static void Client_OnMessage(object sender, global::Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine("Message " + e.Message.Text);

            //Stream stream = e.Message?.Audio?.FileStream;

            if (e.Message.Type.Equals(MessageType.VoiceMessage))
            {

                var message = e.Message;

                var filePath = Path.Combine(@"C:\apps", message.Voice.FileId + ".ogg");

                using (var file = System.IO.File.OpenWrite(filePath))
                {
                    var tsk = client.GetFileAsync(message.Voice.FileId, file);
                    tsk.Wait();
                    Console.WriteLine($"Find Voice at {filePath}");
                }


                using (FileStream stream = File.Open(filePath, FileMode.Open))
                {
                    //https://stackoverflow.com/questions/17895933/using-system-speech-to-convert-mp3-file-to-text
                    SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
                    Grammar gr = new DictationGrammar();
                    sre.LoadGrammar(gr);
                    sre.SetInputToAudioStream(stream, new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));

                    StringBuilder sb = new StringBuilder();
                    while (true)
                    {
                        try
                        {
                            var recText = sre.Recognize();
                            if (recText == null)
                            {
                                break;
                            }

                            sb.Append(recText.Text);
                        }
                        catch (Exception ex)
                        {
                            //handle exception      
                            //...

                            break;
                        }
                    }

                    Console.WriteLine(sb.ToString());
                }

                //if (stream?.Length > 1)
                //{
                //    //https://stackoverflow.com/questions/17895933/using-system-speech-to-convert-mp3-file-to-text
                //    SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
                //    Grammar gr = new DictationGrammar();
                //    sre.LoadGrammar(gr);
                //    sre.SetInputToAudioStream(stream, new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));

                //    StringBuilder sb = new StringBuilder();
                //    while (true)
                //    {
                //        try
                //        {
                //            var recText = sre.Recognize();
                //            if (recText == null)
                //            {
                //                break;
                //            }

                //            sb.Append(recText.Text);
                //        }
                //        catch (Exception ex)
                //        {
                //            //handle exception      
                //            //...

                //            break;
                //        }
                //    }

                //    Console.WriteLine(sb.ToString());
                //}

            }



        }
    }
}
