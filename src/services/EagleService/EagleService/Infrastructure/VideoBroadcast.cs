using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleService.Infrastructure
{
    public class VideoBroadcast
    {
        private readonly static ConcurrentQueue<byte[]> _frameQueue = new ConcurrentQueue<byte[]>();
        private readonly static ConcurrentDictionary<string, Action<byte[]>> _subscriber = new ConcurrentDictionary<string, Action<byte[]>>();

        public void Subscribe(string requestId, Action<byte[]> action)
        {
            _subscriber.TryAdd(requestId, action);
        }

        public void Unsubscribe(string requestId)
        {
            _subscriber.TryRemove(requestId, out Action<byte[]> action);

            action = null;
        }

        public void BroadcastFrame()
        {
            if (_frameQueue.TryDequeue(out byte[] frameBytes))
            {
                foreach (var item in _subscriber)
                    item.Value?.Invoke(frameBytes);
            }
        }

        public void ReveiveFrame(byte[] frame)
        {
            _frameQueue.Enqueue(frame);
        }
    }
}
