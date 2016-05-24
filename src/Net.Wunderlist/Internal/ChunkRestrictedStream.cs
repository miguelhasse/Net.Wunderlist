using System.IO;

namespace System.Net.Wunderlist.Internal
{
    internal sealed class ChunkRestrictedStream : Stream
    {
        private Lazy<Stream> innerStream;
        private int offset, length;

        public ChunkRestrictedStream(int offset, int length, Func<Stream> streamFactory)
        {
            this.innerStream = new Lazy<Stream>(() =>
            {
                var stream = streamFactory();
                stream.Seek(offset, SeekOrigin.Begin);
                return stream;
            });
            this.offset = offset;
            this.length = length;
        }

        public override bool CanRead
        {
            get { return innerStream.Value.CanRead; }
        }

        public override bool CanSeek
        {
            get { return innerStream.Value.CanSeek; }
        }

        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public override long Length
        {
            get { return Math.Min(length, innerStream.Value.Length - this.offset); }
        }

        public override long Position
        {
            get { return innerStream.Value.Position - offset; }
            set { innerStream.Value.Position = Math.Min(offset + value, length - 1); }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int currentPos = (int)Position;
            int compensation = Math.Min(length - currentPos + offset + count - 1, 0);
            return (currentPos + offset < length) ? innerStream.Value.Read(buffer, offset, count + compensation) : 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
