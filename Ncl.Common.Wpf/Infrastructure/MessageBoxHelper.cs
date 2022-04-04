using System.Windows;

namespace Ncl.Common.Wpf.Infrastructure
{
    /// <summary>
    ///     Helper utility for <see cref="MessageBox" />.
    /// </summary>
    public sealed class MessageBoxHelper : IMessageBoxHelper
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="MessageBoxHelper" />.
        /// </summary>
        public MessageBoxHelper()
        {
            //Intentionally left blank
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="MessageBoxHelper" />.
        /// </summary>
        /// <param name="owner">The initial owner window.</param>
        public MessageBoxHelper(Window? owner)
        {
            Owner = owner;
        }

        /// <inheritdoc />
        public Window? Owner { get; set; }

        /// <inheritdoc />
        public MessageBoxResult Show(
            string messageBoxText,
            string caption = "",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = MessageBoxResult.None,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            Window? owner = Owner;
            if (owner == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window? window,
            string messageBoxText,
            string caption = "",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = MessageBoxResult.None,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            if (window == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(window, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <inheritdoc />
        public MessageBoxResult ShowError(
            string messageBoxText,
            string caption = "Error",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Error,
            MessageBoxResult defaultResult = MessageBoxResult.OK,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            Window? owner = Owner;
            if (owner == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <inheritdoc />
        public MessageBoxResult ShowError(Window? window,
            string messageBoxText,
            string caption = "Error",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Error,
            MessageBoxResult defaultResult = MessageBoxResult.OK,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            if (window == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(window, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <inheritdoc />
        public MessageBoxResult ShowWarning(
            string messageBoxText,
            string caption = "Warning",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Warning,
            MessageBoxResult defaultResult = MessageBoxResult.OK,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            Window? owner = Owner;
            if (owner == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <inheritdoc />
        public MessageBoxResult ShowWarning(Window? window,
            string messageBoxText,
            string caption = "Warning",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Warning,
            MessageBoxResult defaultResult = MessageBoxResult.OK,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            if (window == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(window, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <inheritdoc />
        public MessageBoxResult ShowInfo(
            string messageBoxText,
            string caption = "Information",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Information,
            MessageBoxResult defaultResult = MessageBoxResult.OK,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            Window? owner = Owner;
            if (owner == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <inheritdoc />
        public MessageBoxResult ShowInfo(Window? window,
            string messageBoxText,
            string caption = "Information",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Information,
            MessageBoxResult defaultResult = MessageBoxResult.OK,
            MessageBoxOptions options = MessageBoxOptions.None)
        {
            if (window == null)
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);

            return MessageBox.Show(window, messageBoxText, caption, button, icon, defaultResult, options);
        }
    }
}