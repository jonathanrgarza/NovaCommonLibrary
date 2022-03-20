using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ncl.Common.Csv.Tests
{
    /// <summary>
    ///     A stream that blocks on read/write calls for a long time.
    /// </summary>
    public class BlockingStream : Stream
    {
        /// <summary>
        ///     The length of the blocking.
        /// </summary>
        public const int DelayLength = 1000 * 60;

        public override bool CanRead { get; } = true;
        public override bool CanSeek { get; } = false;
        public override bool CanWrite { get; } = true;
        public override long Length { get; } = 1;
        public override long Position { get; set; } = 0;

        public override void Flush()
        {
            //Do nothing
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            //Thread.Sleep(DelayLength);
            //return 0;
            throw new Exception("BlockingStream does not expect any read operations. It is for testing async related operations");
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count,
            CancellationToken cancellationToken)
        {
            await Task.Delay(DelayLength, cancellationToken);
            return 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            //Do nothing
            return 0;
        }

        public override void SetLength(long value)
        {
            //Do nothing
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            //Thread.Sleep(DelayLength);
            throw new Exception("BlockingStream does not expect any write operations. It is for testing async related operations");
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Delay(DelayLength, cancellationToken);
        }
    }
}