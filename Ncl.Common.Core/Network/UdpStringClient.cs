using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Ncl.Common.Core.Utilities;
using Timer = System.Timers.Timer;

namespace Ncl.Common.Core.Network
{
    /// <summary>
    /// Represents a UDP client that sends and receives string messages.
    /// </summary>
    public class UdpStringClient : IDisposable
    {
        private Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpStringClient"/> class with the specified port.
        /// </summary>
        /// <param name="port">The port number to bind the UDP client to.</param>
        public UdpStringClient(int port)
        {
            UdpClient = new UdpClient(port);
            Port = port;
            RemoteAddress = new IPEndPoint(IPAddress.Any, port);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpStringClient"/> class with the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The IP address to send UDP messages to.</param>
        /// <param name="port">The port number to bind the UDP client to.</param>
        public UdpStringClient(string ipAddress, int port)
        {
            UdpClient = new UdpClient(port);
            Port = port;
            RemoteAddress = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpStringClient"/> class with the specified IP address, port, and remote
        /// port.
        /// </summary>
        /// <param name="ipAddress">The IP address to send UDP messages to.</param>
        /// <param name="port">The port number to bind the UDP client to.</param>
        /// <param name="remotePort">The remote port number to send UDP messages to.</param>
        public UdpStringClient(string ipAddress, int port, int remotePort)
        {
            UdpClient = new UdpClient(port);
            Port = port;
            RemoteAddress = new IPEndPoint(IPAddress.Parse(ipAddress), remotePort);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpStringClient"/> class with the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The IP address to send UDP messages to.</param>
        /// <param name="port">The port number to bind the UDP client to.</param>
        public UdpStringClient(IPAddress ipAddress, int port)
        {
            UdpClient = new UdpClient(port);
            Port = port;
            RemoteAddress = new IPEndPoint(ipAddress, port);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpStringClient"/> class with the specified IP address, port, and remote
        /// port.
        /// </summary>
        /// <param name="ipAddress">The IP address to send UDP messages to.</param>
        /// <param name="port">The port number to bind the UDP client to.</param>
        /// <param name="remotePort">The remote port number to send UDP messages to.</param>
        public UdpStringClient(IPAddress ipAddress, int port, int remotePort)
        {
            UdpClient = new UdpClient(port);
            Port = port;
            RemoteAddress = new IPEndPoint(ipAddress, remotePort);
        }

        /// <summary>
        /// Gets the port number that the UDP client is bound to.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Gets the remote IP endpoint that the UDP client sends/receives messages.
        /// </summary>
        public IPEndPoint RemoteAddress { get; protected set; }

        /// <summary>
        /// Gets or sets the encoding used to convert string messages to bytes and vice versa.
        /// </summary>
        public Encoding Encoding
        {
            get => _encoding;
            set
            {
                Guard.AgainstNullArgument(nameof(value), value);
                if (Equals(_encoding, value))
                    return;
                if (value.IsReadOnly)
                {
                    _encoding = value;
                    return;
                }

                _encoding = (Encoding)value.Clone();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the UDP client is active and connected.
        /// </summary>
        public bool IsActive => !IsDisposed && UdpClient.Client.Connected;

        /// <summary>
        /// Gets the number of bytes available to be read from the UDP client.
        /// </summary>
        /// <exception cref="ObjectDisposedException">The instance is disposed.</exception>
        public int Available
        {
            get
            {
                Guard.AgainstDisposed(IsDisposed);
                return UdpClient.Available;
            }
        }

        /// <summary>
        /// Gets/Sets the disposed state of the instance.
        /// </summary>
        protected bool IsDisposed { get; set; }

        /// <summary>
        /// Gets the underlying <see cref="UdpClient"/> instance used by the <see cref="UdpStringClient"/>.
        /// </summary>
        protected UdpClient UdpClient { get; private set; }

        /// <summary>
        /// Releases all resources used by the <see cref="UdpStringClient"/>.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Sends a string message asynchronously to the remote endpoint.
        /// </summary>
        /// <param name="message">The string message to send.</param>
        /// <returns>A task representing the asynchronous operation. The task result represents the number of bytes sent.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">The instance is disposed.</exception>
        public virtual async Task<int> SendAsync(string message)
        {
            Guard.AgainstNullArgument(nameof(message), message);
            Guard.AgainstDisposed(IsDisposed);

            byte[] messageBytes = Encoding.GetBytes(message);
            return await UdpClient.SendAsync(messageBytes, messageBytes.Length, RemoteAddress).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a string message asynchronously to the remote endpoint with the option to cancel the operation and specify a
        /// timeout.
        /// </summary>
        /// <param name="message">The string message to send.</param>
        /// <param name="token">The cancellation token to cancel the operation.</param>
        /// <param name="timeout">The timeout value in milliseconds. Use <see cref="Timeout.Infinite"/> for no timeout.</param>
        /// <returns>A task representing the asynchronous operation. The task result represents the number of bytes sent.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">The instance is disposed.</exception>
        /// <exception cref="TimeoutException">The operation timed out.</exception>
        /// <exception cref="OperationCanceledException">The operation was canceled.</exception>
        public virtual async Task<int> SendAsync(string message, CancellationToken token,
            int timeout = Timeout.Infinite)
        {
            Guard.AgainstNullArgument(nameof(message), message);
            Guard.AgainstDisposed(IsDisposed);

            byte[] messageBytes = Encoding.GetBytes(message);
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                Timer timer = null;
                if (timeout > 0)
                {
                    timer = new Timer(timeout)
                    {
                        AutoReset = false
                    };
                    timer.Elapsed += OnTimeout;
                }

                cts.Token.Register(CancelUdpOperation);

                try
                {
                    timer?.Start();
                    int bytesSent = await UdpClient.SendAsync(messageBytes, messageBytes.Length, RemoteAddress)
                        .ConfigureAwait(false);
                    timer?.Stop();
                    return bytesSent;
                }
                catch (ObjectDisposedException)
                {
                    if (!cts.IsCancellationRequested)
                        throw new TimeoutException();

                    throw new OperationCanceledException(cts.Token);
                }
            }
        }

        /// <summary>
        /// Receives a string message asynchronously from the UDP client.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result represents the received string message.</returns>
        /// <exception cref="ObjectDisposedException">The instance is disposed.</exception>
        public virtual async Task<string> ReceiveAsync()
        {
            Guard.AgainstDisposed(IsDisposed);

            var result = await UdpClient.ReceiveAsync().ConfigureAwait(false);
            return Encoding.GetString(result.Buffer);
        }

        /// <summary>
        /// Receives a string message asynchronously from the UDP client with the option to cancel the operation and specify a
        /// timeout.
        /// </summary>
        /// <param name="token">The cancellation token to cancel the operation.</param>
        /// <param name="timeout">The timeout value in milliseconds. Use <see cref="Timeout.Infinite"/> for no timeout.</param>
        /// <returns>A task representing the asynchronous operation. The task result represents the received string message.</returns>
        /// <exception cref="ObjectDisposedException">The instance is disposed.</exception>
        /// <exception cref="TimeoutException">The operation timed out.</exception>
        /// <exception cref="OperationCanceledException">The operation was canceled.</exception>
        public virtual async Task<string> ReceiveAsync(CancellationToken token, int timeout = Timeout.Infinite)
        {
            Guard.AgainstDisposed(IsDisposed);

            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                Timer timer = null;
                if (timeout > 0)
                {
                    timer = new Timer(timeout)
                    {
                        AutoReset = false
                    };
                    timer.Elapsed += OnTimeout;
                }

                cts.Token.Register(CancelUdpOperation);

                try
                {
                    timer?.Start();
                    var result = await UdpClient.ReceiveAsync().ConfigureAwait(false);
                    timer?.Stop();
                    return Encoding.GetString(result.Buffer);
                }
                catch (ObjectDisposedException)
                {
                    if (!cts.IsCancellationRequested)
                        throw new TimeoutException();

                    throw new OperationCanceledException(cts.Token);
                }
            }
        }

        /// <summary>
        /// Sends a string message asynchronously to the remote endpoint and receives a response.
        /// </summary>
        /// <param name="message">The string message to send.</param>
        /// <param name="token">The cancellation token to cancel the operation.</param>
        /// <param name="timeout">The timeout value in milliseconds. Use <see cref="Timeout.Infinite"/> for no timeout.</param>
        /// <returns>A task representing the asynchronous operation. The task result represents the received string message.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">The instance is disposed.</exception>
        /// <exception cref="TimeoutException">The operation timed out.</exception>
        /// <exception cref="OperationCanceledException">The operation was canceled.</exception>
        public virtual async Task<string> SendAndReceive(string message, CancellationToken token,
            int timeout = Timeout.Infinite)
        {
            Guard.AgainstNullArgument(nameof(message), message);
            Guard.AgainstDisposed(IsDisposed);

            token.ThrowIfCancellationRequested();

            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                Timer timer = null;
                if (timeout > 0)
                {
                    timer = new Timer(timeout)
                    {
                        AutoReset = false
                    };
                    timer.Elapsed += OnTimeout;
                }

                cts.Token.Register(CancelUdpOperation);

                try
                {
                    byte[] messageBytes = Encoding.GetBytes(message);
                    timer?.Start();
                    await UdpClient.SendAsync(messageBytes, messageBytes.Length, RemoteAddress).ConfigureAwait(false);
                    timer?.Stop();

                    token.ThrowIfCancellationRequested();

                    timer?.Start();
                    var result = await UdpClient.ReceiveAsync().ConfigureAwait(false);
                    timer?.Stop();
                    return Encoding.GetString(result.Buffer);
                }
                catch (ObjectDisposedException)
                {
                    if (!cts.IsCancellationRequested)
                        throw new TimeoutException();

                    throw new OperationCanceledException(cts.Token);
                }
            }
        }

        /// <summary>
        /// Event handler for the timeout event.
        /// </summary>
        /// <param name="sender">The timer instance.</param>
        /// <param name="e">The timer event arguments.</param>
        protected void OnTimeout(object sender, ElapsedEventArgs e)
        {
            (sender as Timer)?.Stop();
            UdpClient.Close();
            UdpClient = new UdpClient(Port);
        }

        /// <summary>
        /// Cancels the UDP operation by closing the UDP client and creating a new instance.
        /// </summary>
        protected void CancelUdpOperation()
        {
            ReconnectClient();
        }

        /// <summary>
        /// Reconnects the UDP client by closing the current client and creating a new instance.
        /// </summary>
        protected void ReconnectClient()
        {
            UdpClient.Close();
            UdpClient = new UdpClient(Port);
        }

        /// <summary>
        /// Reconnects the UDP client by closing the current client and creating a new instance.
        /// </summary>
        /// <param name="port">A new port to use.</param>
        protected void ReconnectClient(int port)
        {
            UdpClient.Close();
            UdpClient = new UdpClient(Port);
            Port = port;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="UdpStringClient"/> and optionally releases the managed
        /// resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether the method is called from the <see cref="Dispose"/> method.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing) UdpClient.Dispose();

            IsDisposed = true;
        }
    }
}