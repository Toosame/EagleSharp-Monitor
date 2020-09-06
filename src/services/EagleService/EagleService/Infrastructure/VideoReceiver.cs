using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EagleService.Infrastructure
{
    public class VideoReceiver
    {
        private UdpClient udpClient;

        private readonly int _port;

        public delegate void MessageReceived(byte[] frame, IPEndPoint clientIpEndPoint);

        public event MessageReceived OnMessageReceived;

        private readonly ILogger<VideoReceiver> _logger;

        private readonly byte[] _frameEnd;
        private readonly byte[] _frameStart;

        public VideoReceiver(int port, ILogger<VideoReceiver> logger)
        {
            _port = port;
            _logger = logger;
            _frameStart = Encoding.UTF8.GetBytes("--frame\r\nContent-Type: image/png\r\n\r\n");
            _frameEnd = Encoding.UTF8.GetBytes("\r\n\r\n");
        }

        public async Task Start(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    udpClient = new UdpClient(_port);

                    MemoryStream memoryStream = new MemoryStream();
                    await memoryStream.WriteAsync(_frameStart, stoppingToken);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var frameResult = await udpClient.ReceiveAsync();
                        if (frameResult.Buffer.Length == _frameEnd.Length &&
                            frameResult.Buffer[0] == _frameEnd[0] &&
                            frameResult.Buffer[1] == _frameEnd[1] &&
                            frameResult.Buffer[2] == _frameEnd[2] &&
                            frameResult.Buffer[3] == _frameEnd[3])
                        {
                            //向客户端广播帧
                            OnMessageReceived?.Invoke(memoryStream.ToArray(), frameResult.RemoteEndPoint);

                            //释放资源重新接收下一帧
                            memoryStream.Dispose();
                            memoryStream = new MemoryStream();
                            await memoryStream.WriteAsync(_frameStart, stoppingToken);
                        }
                        else
                            await memoryStream.WriteAsync(frameResult.Buffer);
                    }

                    udpClient.Close();
                    udpClient.Dispose();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Video Streaming Exception");
                }
            }
        }
    }
}
