namespace UShop.Shared.IdGenerator
{
    public class SnowflakeIdGeneratorService:ISnowflakeIdGeneratorService
    {
        private const long Epoch = 1735660800000L; // 2023-01-01 00:00:00 UTC
        private const int WorkerIdBits = 5;
        private const int DatacenterIdBits = 5;
        private const int SequenceBits = 12;

        private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        private const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);
        private const long MaxSequence = -1L ^ (-1L << SequenceBits);

        private readonly long _workerId;
        private readonly long _datacenterId;
        private long _lastTimestamp = -1L;
        private long _sequence = 0L;

        public SnowflakeIdGeneratorService(long workerId, long datacenterId)
        {
            if (workerId < 0 || workerId > MaxWorkerId)
                throw new ArgumentException($"Worker Id must be between 0 and {MaxWorkerId}");
            if (datacenterId < 0 || datacenterId > MaxDatacenterId)
                throw new ArgumentException($"Datacenter Id must be between 0 and {MaxDatacenterId}");

            _workerId = workerId;
            _datacenterId = datacenterId;
        }

        public long GenerateId()
        {
            long timestamp = GetCurrentTimestamp();

            // 如果时间回退，抛出异常
            if (timestamp < _lastTimestamp)
                throw new InvalidOperationException("Clock moved backwards. Refusing to generate ID.");

            if (timestamp == _lastTimestamp)
            {
                // 原子增加序列号
                _sequence = Interlocked.Add(ref _sequence, 1) & MaxSequence;
                if (_sequence == 0)
                    timestamp = WaitForNextTimestamp(_lastTimestamp);
            }
            else
            {
                _sequence = 0;
            }

            Interlocked.Exchange(ref _lastTimestamp, timestamp);

            return ((timestamp - Epoch) << (WorkerIdBits + DatacenterIdBits + SequenceBits)) |
                   (_datacenterId << (WorkerIdBits + SequenceBits)) |
                   (_workerId << SequenceBits) |
                   _sequence;
        }

        private static long GetCurrentTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        private static long WaitForNextTimestamp(long lastTimestamp)
        {
            long timestamp = GetCurrentTimestamp();
            while (timestamp <= lastTimestamp)
                timestamp = GetCurrentTimestamp();
            return timestamp;
        }
    }
}
