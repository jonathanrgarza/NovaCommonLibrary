using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Ncl.Common.Wpf.ViewModels
{
    /// <summary>
    ///     The base class for a view model which support <see cref="INotifyDataErrorInfo" /> and
    ///     uses <see cref="string" /> to represent data errors.
    /// </summary>
    public abstract class DataErrorHandlingViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        /// <inheritdoc />
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return GetErrorDictionaryAsReadOnly();

            return _errors.TryGetValue(propertyName, out List<string> errorList) ? errorList.AsReadOnly() : null;
        }

        /// <inheritdoc />
        public bool HasErrors => _errors.Count > 0;

        /// <inheritdoc />
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        ///     Adds an error message for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorMessage">The error message to add.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorMessage" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void AddError(string propertyName, string errorMessage)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            GuardAgainstInvalidErrorMessage(errorMessage);

            if (_errors.TryGetValue(propertyName, out List<string> propertyErrorList))
            {
                propertyErrorList.Add(errorMessage);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            var newErrorList = new List<string> { errorMessage };
            _errors.Add(propertyName, newErrorList);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Adds error messages for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorMessages">The error messages to add.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorMessages" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void AddErrors(string propertyName, IEnumerable<string> errorMessages)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            if (errorMessages == null)
                throw new ArgumentNullException(nameof(errorMessages));

            if (_errors.TryGetValue(propertyName, out List<string> propertyErrorList))
            {
                propertyErrorList.AddRange(errorMessages);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            var newErrorList = new List<string>(errorMessages);
            _errors.Add(propertyName, newErrorList);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Removes an error message for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorMessage">The error message to remove.</param>
        /// <returns>
        ///     <see langword="true" /> if the error message was removed, otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorMessage" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected bool RemoveError(string propertyName, string errorMessage)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            GuardAgainstInvalidErrorMessage(errorMessage);

            if (!_errors.TryGetValue(propertyName, out List<string> propertyErrorList))
                return false;

            bool removeStatus = propertyErrorList.Remove(errorMessage);

            if (propertyErrorList.Count == 0)
            {
                _errors.Remove(propertyName);
            }

            if (removeStatus)
            {
                RaiseOnErrorChanged(propertyName);
            }

            return removeStatus;
        }

        /// <summary>
        ///     Replaces all the errors for a given property with the new error.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorMessage">The error message to replace with.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorMessage" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void ReplaceErrors(string propertyName, string errorMessage)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            GuardAgainstInvalidErrorMessage(errorMessage);

            if (!_errors.TryGetValue(propertyName, out List<string> propertyErrorList))
            {
                var newErrorList = new List<string> { errorMessage };
                _errors.Add(propertyName, newErrorList);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            propertyErrorList.Clear();
            propertyErrorList.Add(errorMessage);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Replaces all the errors for a given property with the new errors.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorMessages">The error messages to replace with.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorMessages" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void ReplaceErrors(string propertyName, IEnumerable<string> errorMessages)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            if (errorMessages == null)
                throw new ArgumentNullException(nameof(errorMessages));

            if (!_errors.TryGetValue(propertyName, out List<string> propertyErrorList))
            {
                var newErrorList = new List<string>(errorMessages);
                _errors.Add(propertyName, newErrorList);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            propertyErrorList.Clear();
            propertyErrorList.AddRange(errorMessages);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Clears/Removes all the error message for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void ClearErrors(string propertyName)
        {
            GuardAgainstInvalidPropertyName(propertyName);

            if (!_errors.Remove(propertyName))
                return;

            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Clears/Removes all the error message for the view model.
        /// </summary>
        protected void ClearAllErrors()
        {
            if (_errors.Count == 0)
                return;

            _errors.Clear();
            RaiseOnErrorChanged(string.Empty);
        }

        /// <summary>
        ///     Guards against a <see langword="null" /> or empty property name.
        /// </summary>
        /// <param name="propertyName">The property name to check.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName" /> is an empty string.</exception>
        private static void GuardAgainstInvalidPropertyName(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            if (propertyName.Length == 0)
            {
                throw new ArgumentException("propertyName can not be an empty string",
                    nameof(propertyName));
            }
        }

        /// <summary>
        ///     Guards against a <see langword="null" /> error message.
        /// </summary>
        /// <param name="errorMessage">The error message to check.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="errorMessage" /> is <see langword="null" />.
        /// </exception>
        private static void GuardAgainstInvalidErrorMessage(string errorMessage)
        {
            if (errorMessage == null)
                throw new ArgumentNullException(nameof(errorMessage));
        }

        /// <summary>
        ///     Raises the <see cref="ErrorsChanged" /> event for a given property or
        ///     all properties if given <see langword="null" /> or empty string.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        private void RaiseOnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Gets the <see cref="_errors" /> as a readonly dictionary.
        /// </summary>
        /// <returns>
        ///     A readonly version of <see cref="_errors" /> or <see langword="null" /> if the
        ///     dictionary contains no elements.
        /// </returns>
        private ReadOnlyDictionary<string, IReadOnlyList<string>> GetErrorDictionaryAsReadOnly()
        {
            if (_errors.Count == 0)
                return null;

            var newDictionary = new Dictionary<string, IReadOnlyList<string>>(_errors.Count);
            foreach (KeyValuePair<string, List<string>> keyValuePair in _errors)
            {
                newDictionary.Add(keyValuePair.Key, keyValuePair.Value.AsReadOnly());
            }

            return new ReadOnlyDictionary<string, IReadOnlyList<string>>(newDictionary);
        }
    }

    /// <summary>
    ///     The base class for a view model which support <see cref="INotifyDataErrorInfo" /> and
    ///     uses a custom type to represent data errors.
    /// </summary>
    /// <typeparam name="T">The type for the error objects.</typeparam>
    public abstract class DataErrorHandlingViewModelBase<T> : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<T>> _errors = new Dictionary<string, List<T>>();

        /// <inheritdoc />
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return GetErrorDictionaryAsReadOnly();

            return _errors.TryGetValue(propertyName, out List<T> errorList) ? errorList.AsReadOnly() : null;
        }

        /// <inheritdoc />
        public bool HasErrors => _errors.Count > 0;

        /// <inheritdoc />
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        ///     Adds an error for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorObject">The error object to add.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorObject" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void AddError(string propertyName, T errorObject)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            GuardAgainstInvalidErrorObject(errorObject);

            if (_errors.TryGetValue(propertyName, out List<T> propertyErrorList))
            {
                propertyErrorList.Add(errorObject);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            var newErrorList = new List<T> { errorObject };
            _errors.Add(propertyName, newErrorList);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Adds errors for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorObjects">The error objects to add.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorObjects" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void AddErrors(string propertyName, IEnumerable<T> errorObjects)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            if (errorObjects == null)
                throw new ArgumentNullException(nameof(errorObjects));

            if (_errors.TryGetValue(propertyName, out List<T> propertyErrorList))
            {
                propertyErrorList.AddRange(errorObjects);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            var newErrorList = new List<T>(errorObjects);
            _errors.Add(propertyName, newErrorList);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Removes an error for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorObject">The error object to remove.</param>
        /// <returns>
        ///     <see langword="true" /> if the error message was removed, otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorObject" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected bool RemoveError(string propertyName, T errorObject)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            GuardAgainstInvalidErrorObject(errorObject);

            if (!_errors.TryGetValue(propertyName, out List<T> propertyErrorList))
                return false;

            bool removeStatus = propertyErrorList.Remove(errorObject);

            if (propertyErrorList.Count == 0)
            {
                _errors.Remove(propertyName);
            }

            if (removeStatus)
            {
                RaiseOnErrorChanged(propertyName);
            }

            return removeStatus;
        }

        /// <summary>
        ///     Replaces all the errors for a given property with the new error.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorObject">The error object to replace with.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorObject" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void ReplaceErrors(string propertyName, T errorObject)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            GuardAgainstInvalidErrorObject(errorObject);

            if (!_errors.TryGetValue(propertyName, out List<T> propertyErrorList))
            {
                var newErrorList = new List<T> { errorObject };
                _errors.Add(propertyName, newErrorList);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            propertyErrorList.Clear();
            propertyErrorList.Add(errorObject);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Replaces all the errors for a given property with the new errors.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="errorObjects">The error objects to replace with.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> or <paramref name="errorObjects" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void ReplaceErrors(string propertyName, IEnumerable<T> errorObjects)
        {
            GuardAgainstInvalidPropertyName(propertyName);
            if (errorObjects == null)
                throw new ArgumentNullException(nameof(errorObjects));

            if (!_errors.TryGetValue(propertyName, out List<T> propertyErrorList))
            {
                var newErrorList = new List<T>(errorObjects);
                _errors.Add(propertyName, newErrorList);
                RaiseOnErrorChanged(propertyName);
                return;
            }

            propertyErrorList.Clear();
            propertyErrorList.AddRange(errorObjects);
            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Clears/Removes all the errors for a given property.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName" /> is empty.
        /// </exception>
        protected void ClearErrors(string propertyName)
        {
            GuardAgainstInvalidPropertyName(propertyName);

            if (!_errors.Remove(propertyName))
                return;

            RaiseOnErrorChanged(propertyName);
        }

        /// <summary>
        ///     Clears/Removes all the errors for the view model.
        /// </summary>
        protected void ClearAllErrors()
        {
            if (_errors.Count == 0)
                return;

            _errors.Clear();
            RaiseOnErrorChanged(string.Empty);
        }

        /// <summary>
        ///     Guards against a null or empty property name.
        /// </summary>
        /// <param name="propertyName">The property name to check.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName" /> is an empty string.</exception>
        private static void GuardAgainstInvalidPropertyName(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            if (propertyName.Length == 0)
            {
                throw new ArgumentException("propertyName can not be an empty string",
                    nameof(propertyName));
            }
        }

        /// <summary>
        ///     Guards against a null error object.
        /// </summary>
        /// <param name="errorObject">The error object to check.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="errorObject" /> is <see langword="null" />.
        /// </exception>
        private static void GuardAgainstInvalidErrorObject(T errorObject)
        {
            if (errorObject == null)
                throw new ArgumentNullException(nameof(errorObject));
        }

        /// <summary>
        ///     Raises the <see cref="ErrorsChanged" /> event for a given property or
        ///     all properties if given <see langword="null" /> or empty string.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        private void RaiseOnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Gets the <see cref="_errors" /> as a readonly dictionary.
        /// </summary>
        /// <returns>
        ///     A readonly version of <see cref="_errors" /> or <see langword="null" /> if the
        ///     dictionary contains no elements.
        /// </returns>
        private ReadOnlyDictionary<string, IReadOnlyList<T>> GetErrorDictionaryAsReadOnly()
        {
            if (_errors.Count == 0)
                return null;

            var newDictionary = new Dictionary<string, IReadOnlyList<T>>(_errors.Count);
            foreach (KeyValuePair<string, List<T>> keyValuePair in _errors)
            {
                newDictionary.Add(keyValuePair.Key, keyValuePair.Value.AsReadOnly());
            }

            return new ReadOnlyDictionary<string, IReadOnlyList<T>>(newDictionary);
        }
    }
}