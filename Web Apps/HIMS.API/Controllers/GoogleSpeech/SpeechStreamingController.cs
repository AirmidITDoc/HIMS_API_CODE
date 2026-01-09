using Asp.Versioning;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Google.Protobuf;
using HIMS.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace HIMS.API.Controllers.GoogleSpeech
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SpeechStreamingController : BaseController
    {
        [HttpGet]
        [Route("speech")]
        public async Task Get(string lang)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = 400;
                return;
            }

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var credential = GoogleCredential.FromFile("google-keys/google-speech.json").CreateScoped(SpeechClient.DefaultScopes);

            var speechClient = new SpeechClientBuilder
            {
                Credential = credential
            }.Build();

            var streamingCall = speechClient.StreamingRecognize();

            // Initial config
            await streamingCall.WriteAsync(
                new StreamingRecognizeRequest
                {
                    StreamingConfig = new StreamingRecognitionConfig
                    {
                        Config = new RecognitionConfig
                        {
                            Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                            SampleRateHertz = 16000,
                            LanguageCode = lang,
                            EnableAutomaticPunctuation = true,
                            //Model = "latest_long"
                        },
                        InterimResults = true
                    }
                });

            var receiveTask = Task.Run(async () =>
            {
                await foreach (var response in streamingCall.GetResponseStream())
                {
                    foreach (var result in response.Results)
                    {
                        var transcript = result.Alternatives[0].Transcript;
                        var bytes = Encoding.UTF8.GetBytes(transcript);
                        await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            });

            var buffer = new byte[4096];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                await streamingCall.WriteAsync(
                    new StreamingRecognizeRequest
                    {
                        AudioContent = ByteString.CopyFrom(buffer, 0, result.Count)
                    });
            }

            await streamingCall.WriteCompleteAsync();
            await receiveTask;
        }
    }
}
