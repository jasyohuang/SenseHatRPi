using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using SenseHatRPi;
using Newtonsoft.Json;

static class AzureIoTHub
{
    //
    // Note: this connection string is specific to the device "myFirstDevice". To configure other devices,
    // see information on iothub-explorer at http://aka.ms/iothubgetstartedVSCS
    //
    const string deviceConnectionString = "HostName=start.azure-devices.net;DeviceId=myFirstDevice;SharedAccessKey=COjphwqpr6xpuLKoc/JOrty0338x3Cy14jN2ruStTgs=";

    //
    // To monitor messages sent to device "myFirstDevice" use iothub-explorer as follows:
    //    iothub-explorer HostName=start.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=kU67Ap58KDo3UtkU1KL6TjxbBQlDgTyhskCwEr/ELLs= monitor-events "myFirstDevice"
    //

    // Refer to http://aka.ms/azure-iot-hub-vs-cs-wiki for more information on Connected Service for Azure IoT Hub

    public static async Task SendDeviceToCloudMessageAsync(SenseHatDatas data)
    {
        var deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString, TransportType.Amqp);
        var messageInJson = JsonConvert.SerializeObject(data);
        var message = new Message(Encoding.UTF8.GetBytes(messageInJson));

        await deviceClient.SendEventAsync(message);
    }

    public static async Task<string> ReceiveCloudToDeviceMessageAsync()
    {
        var deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString, TransportType.Amqp);

        while (true)
        {
            var receivedMessage = await deviceClient.ReceiveAsync();

            if (receivedMessage != null)
            {
                var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                await deviceClient.CompleteAsync(receivedMessage);
                return messageData;
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
