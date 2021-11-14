using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ncl.Common.Wpf.ViewModels;
using Xunit;

namespace Ncl.Common.Wpf.Tests.ViewModels
{
    public class DataErrorHandlingViewModelBaseTests
    {
        private const string PropertyName = "testProperty";
        private const string SecondPropertyName = "testProperty2";
        private const string DefaultErrorMessage = "test error";
        private const string SecondErrorMessage = "test error 2";
        private const string ThirdErrorMessage = "Test error 3";

        [Fact]
        public void GetError_WithPropertyThatHasError_ShouldReturnErrors()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            IEnumerable actual = instance.GetErrors(PropertyName);

            //Assert
            List<object> collection = actual.Cast<object>().ToList();

            Assert.Single(collection);
            Assert.Equal(collection[0], DefaultErrorMessage);
        }

        [Fact]
        public void GetError_WithNullPropertyNameAndErrors_ShouldReturnAllErrors()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            IEnumerable actual = instance.GetErrors(null);

            //Assert
            List<KeyValuePair<string, IReadOnlyList<string>>> collection =
                actual.Cast<KeyValuePair<string, IReadOnlyList<string>>>().ToList();

            Assert.Single(collection);

            string propertyName = collection[0].Key;
            IReadOnlyList<string> propertyErrors = collection[0].Value;

            Assert.Equal(PropertyName, propertyName);
            Assert.Single(propertyErrors);
            Assert.Equal(propertyErrors[0], DefaultErrorMessage);
        }

        [Fact]
        public void GetError_WithEmptyPropertyNameAndErrors_ShouldReturnAllErrors()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            IEnumerable actual = instance.GetErrors(string.Empty);

            //Assert
            List<KeyValuePair<string, IReadOnlyList<string>>> collection =
                actual.Cast<KeyValuePair<string, IReadOnlyList<string>>>().ToList();

            Assert.Single(collection);

            string propertyName = collection[0].Key;
            IReadOnlyList<string> propertyErrors = collection[0].Value;

            Assert.Equal(PropertyName, propertyName);
            Assert.Single(propertyErrors);
            Assert.Equal(propertyErrors[0], DefaultErrorMessage);
        }

        [Fact]
        public void GetError_WithNullPropertyNameAndErrors_ShouldReturnNull()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            IEnumerable actual = instance.GetErrors(null);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetError_WithEmptyPropertyNameAndNoErrors_ShouldReturnNull()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            IEnumerable actual = instance.GetErrors(string.Empty);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void AddError_WithNewError_ShouldAddErrorAndHasErrorsTrue()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Assert
            Assert.True(instance.HasErrors);
        }

        [Fact]
        public void AddError_WithNewError_ShouldAddTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Single(collection);
            Assert.Equal(collection[0], DefaultErrorMessage);
        }

        [Fact]
        public void AddError_WithSecondNewError_ShouldAddTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            instance.AddErrorImpl(PropertyName, SecondErrorMessage);

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Equal(2, collection.Count);
            Assert.Equal(collection[1], SecondErrorMessage);
        }

        [Fact]
        public void AddError_WithValidErrors_ShouldRaiseErrorsChangedEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            void AttachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged += handler;
            }

            void DetachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged -= handler;
            }

            //Act
            void TestCode()
            {
                instance.AddErrorImpl(PropertyName, DefaultErrorMessage);
            }

            //Assert
            Assert.Raises<DataErrorsChangedEventArgs>(AttachErrorsChangedHandler, DetachErrorsChangedHandler, TestCode);
        }

        [Fact]
        public void AddError_WithNullPropertyName_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.AddErrorImpl(null, DefaultErrorMessage);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("propertyName", TestCode);
        }

        [Fact]
        public void AddError_WithEmptyPropertyName_ShouldThrowArgumentException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.AddErrorImpl(string.Empty, DefaultErrorMessage);
            }

            //Assert
            Assert.Throws<ArgumentException>("propertyName", TestCode);
        }

        [Fact]
        public void AddError_WithNullErrorMessage_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.AddErrorImpl(PropertyName, null);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("errorMessage", TestCode);
        }

        [Fact]
        public void AddErrors_WithNewError_ShouldAddErrorAndHasErrorsTrue()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            instance.AddErrorsImpl(PropertyName, new[] { DefaultErrorMessage });

            //Assert
            Assert.True(instance.HasErrors);
        }

        [Fact]
        public void AddErrors_WithNewError_ShouldAddTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            instance.AddErrorsImpl(PropertyName, new[] { DefaultErrorMessage });

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Single(collection);
            Assert.Equal(collection[0], DefaultErrorMessage);
        }

        [Fact]
        public void AddErrors_WithSecondNewError_ShouldAddTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorsImpl(PropertyName, new[] { DefaultErrorMessage });

            //Act
            instance.AddErrorsImpl(PropertyName, new[] { SecondErrorMessage });

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Equal(2, collection.Count);
            Assert.Equal(collection[1], SecondErrorMessage);
        }

        [Fact]
        public void AddErrors_WithValidErrors_ShouldRaiseErrorsChangedEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            void AttachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged += handler;
            }

            void DetachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged -= handler;
            }

            //Act
            void TestCode()
            {
                instance.AddErrorsImpl(PropertyName, new[] { DefaultErrorMessage, SecondErrorMessage });
            }

            //Assert
            Assert.Raises<DataErrorsChangedEventArgs>(AttachErrorsChangedHandler, DetachErrorsChangedHandler, TestCode);
        }

        [Fact]
        public void AddErrors_WithNullPropertyName_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.AddErrorsImpl(null, new[] { SecondErrorMessage });
            }

            //Assert
            Assert.Throws<ArgumentNullException>("propertyName", TestCode);
        }

        [Fact]
        public void AddErrors_WithEmptyPropertyName_ShouldThrowArgumentException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.AddErrorsImpl(string.Empty, new[] { SecondErrorMessage });
            }

            //Assert
            Assert.Throws<ArgumentException>("propertyName", TestCode);
        }

        [Fact]
        public void AddErrors_WithNullErrorEnumerable_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.AddErrorsImpl(PropertyName, null);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("errorMessages", TestCode);
        }

        [Fact]
        public void RemoveError_WithOneErrorAlreadyAdded_ShouldRemoveErrorAndHasErrorsFalse()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            instance.RemoveErrorImpl(PropertyName, DefaultErrorMessage);

            //Assert
            Assert.False(instance.HasErrors);
        }

        [Fact]
        public void RemoveError_WithOneErrorAlreadyAdded_ShouldRemoveTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            instance.RemoveErrorImpl(PropertyName, DefaultErrorMessage);

            //Assert
            IEnumerable actual = instance.GetErrors(PropertyName);

            Assert.Null(actual);
        }

        [Fact]
        public void RemoveError_WithOneErrorAlreadyAdded_ShouldReturnTrue()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            bool actual = instance.RemoveErrorImpl(PropertyName, DefaultErrorMessage);

            Assert.True(actual);
        }

        [Fact]
        public void RemoveError_WithNoErrorsAdded_ShouldReturnFalse()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            bool actual = instance.RemoveErrorImpl(PropertyName, DefaultErrorMessage);

            Assert.False(actual);
        }

        [Fact]
        public void RemoveError_WithTwoErrorAlreadyAdded_ShouldRemoveTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);
            instance.AddErrorImpl(PropertyName, SecondErrorMessage);

            //Act
            instance.RemoveErrorImpl(PropertyName, DefaultErrorMessage);

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Single(collection);
            Assert.Equal(collection[0], SecondErrorMessage);
        }

        [Fact]
        public void RemoveError_WithOneErrorAdded_ShouldRaiseErrorsChangedEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            void AttachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged += handler;
            }

            void DetachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged -= handler;
            }

            //Act
            void TestCode()
            {
                _ = instance.RemoveErrorImpl(PropertyName, DefaultErrorMessage);
            }

            //Assert
            Assert.Raises<DataErrorsChangedEventArgs>(AttachErrorsChangedHandler, DetachErrorsChangedHandler, TestCode);
        }

        [Fact]
        public void RemoveError_WithNoErrorAdded_ShouldNotRaiseEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            bool isInvoked = false;
            instance.ErrorsChanged += (sender, eventArgs) => isInvoked = true;

            //Act
            instance.RemoveErrorImpl(PropertyName, DefaultErrorMessage);

            //Assert
            Assert.False(isInvoked);
        }

        [Fact]
        public void RemoveError_WithNullPropertyName_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.RemoveErrorImpl(null, DefaultErrorMessage);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("propertyName", TestCode);
        }

        [Fact]
        public void RemoveError_WithEmptyPropertyName_ShouldThrowArgumentException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.RemoveErrorImpl(string.Empty, DefaultErrorMessage);
            }

            //Assert
            Assert.Throws<ArgumentException>("propertyName", TestCode);
        }

        [Fact]
        public void RemoveError_WithNullErrorMessage_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.RemoveErrorImpl(PropertyName, null);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("errorMessage", TestCode);
        }

        [Fact]
        public void ReplaceErrors_WithOneErrorAlreadyAdded_ShouldReplaceErrorAndHasErrorsTrue()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            instance.ReplaceErrorsImpl(PropertyName, SecondErrorMessage);

            //Assert
            Assert.True(instance.HasErrors);
        }

        [Fact]
        public void ReplaceErrors_WithNoErrorsAdded_ShouldAddTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            instance.ReplaceErrorsImpl(PropertyName, SecondErrorMessage);

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Single(collection);
            Assert.Equal(collection[0], SecondErrorMessage);
        }

        [Fact]
        public void ReplaceErrors_WithTwoErrorAlreadyAdded_ShouldReplaceAllErrorsWithNewError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);
            instance.AddErrorImpl(PropertyName, SecondErrorMessage);

            //Act
            instance.ReplaceErrorsImpl(PropertyName, ThirdErrorMessage);

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Single(collection);
            Assert.Equal(collection[0], ThirdErrorMessage);
        }

        [Fact]
        public void ReplaceErrors_WithValidError_ShouldRaiseErrorsChangedEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            void AttachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged += handler;
            }

            void DetachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged -= handler;
            }

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(PropertyName, SecondErrorMessage);
            }

            //Assert
            Assert.Raises<DataErrorsChangedEventArgs>(AttachErrorsChangedHandler, DetachErrorsChangedHandler, TestCode);
        }

        [Fact]
        public void ReplaceErrors_WithNullPropertyName_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(null, DefaultErrorMessage);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("propertyName", TestCode);
        }

        [Fact]
        public void ReplaceErrors_WithEmptyPropertyName_ShouldThrowArgumentException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(string.Empty, DefaultErrorMessage);
            }

            //Assert
            Assert.Throws<ArgumentException>("propertyName", TestCode);
        }

        [Fact]
        public void ReplaceErrors_WithNullErrorMessage_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(PropertyName, (string) null);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("errorMessage", TestCode);
        }

        [Fact]
        public void ReplaceErrors1_WithOneErrorAlreadyAdded_ShouldReplaceErrorAndHasErrorsTrue()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            instance.ReplaceErrorsImpl(PropertyName, new[] { SecondErrorMessage, ThirdErrorMessage });

            //Assert
            Assert.True(instance.HasErrors);
        }

        [Fact]
        public void ReplaceErrors1_WithNoErrorsAdded_ShouldAddTheErrors()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            instance.ReplaceErrorsImpl(PropertyName, new[] { SecondErrorMessage, ThirdErrorMessage });

            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Equal(2, collection.Count);
            Assert.Equal(collection[0], SecondErrorMessage);
            Assert.Equal(collection[1], ThirdErrorMessage);
        }

        [Fact]
        public void ReplaceErrors1_WithValidError_ShouldRaiseErrorsChangedEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            void AttachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged += handler;
            }

            void DetachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged -= handler;
            }

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(PropertyName, new[] { SecondErrorMessage, ThirdErrorMessage });
            }

            //Assert
            Assert.Raises<DataErrorsChangedEventArgs>(AttachErrorsChangedHandler, DetachErrorsChangedHandler, TestCode);
        }

        [Fact]
        public void ReplaceErrors1_WithNullPropertyName_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(null, new[] { SecondErrorMessage, ThirdErrorMessage });
            }

            //Assert
            Assert.Throws<ArgumentNullException>("propertyName", TestCode);
        }

        [Fact]
        public void ReplaceErrors1_WithEmptyPropertyName_ShouldThrowArgumentException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(string.Empty, new[] { SecondErrorMessage, ThirdErrorMessage });
            }

            //Assert
            Assert.Throws<ArgumentException>("propertyName", TestCode);
        }

        [Fact]
        public void ReplaceErrors1_WithNullErrorMessages_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ReplaceErrorsImpl(PropertyName, (IEnumerable<string>) null);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("errorMessages", TestCode);
        }

        [Fact]
        public void ClearErrors_WithOneErrorAdded_ShouldClearErrors()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            instance.ClearErrorsImpl(PropertyName);

            //Assert
            IEnumerable actual = instance.GetErrors(PropertyName);

            Assert.Null(actual);
        }

        [Fact]
        public void ClearErrors_WithOneErrorAdded_ShouldRaiseErrorsChangedEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            void AttachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged += handler;
            }

            void DetachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged -= handler;
            }

            //Act
            void TestCode()
            {
                instance.ClearErrorsImpl(PropertyName);
            }

            //Assert
            Assert.Raises<DataErrorsChangedEventArgs>(AttachErrorsChangedHandler, DetachErrorsChangedHandler, TestCode);
        }

        [Fact]
        public void ClearErrors_WithNoErrorAdded_ShouldNotRaiseEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            bool isInvoked = false;
            instance.ErrorsChanged += (sender, eventArgs) => isInvoked = true;

            //Act
            instance.ClearErrorsImpl(PropertyName);

            //Assert
            Assert.False(isInvoked);
        }

        [Fact]
        public void ClearErrors_WithTwoDifferentErrorsAdded_ShouldClearErrorForOneProperty()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);
            instance.AddErrorImpl(SecondPropertyName, SecondErrorMessage);

            //Act
            instance.ClearErrorsImpl(PropertyName);

            //Assert
            IEnumerable actual = instance.GetErrors(PropertyName);

            Assert.Null(actual);

            IEnumerable errors = instance.GetErrors(SecondPropertyName);
            List<object> collection = errors.Cast<object>().ToList();

            Assert.Single(collection);
            Assert.Equal(collection[0], SecondErrorMessage);
        }

        [Fact]
        public void ClearErrors_WithNullPropertyName_ShouldThrowArgumentNullException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ClearErrorsImpl(null);
            }

            //Assert
            Assert.Throws<ArgumentNullException>("propertyName", TestCode);
        }

        [Fact]
        public void ClearErrors_WithEmptyPropertyName_ShouldThrowArgumentException()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            //Act
            void TestCode()
            {
                instance.ClearErrorsImpl(string.Empty);
            }

            //Assert
            Assert.Throws<ArgumentException>("propertyName", TestCode);
        }

        [Fact]
        public void ClearAllErrors_WithOneErrorAdded_ShouldClearErrors()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            //Act
            instance.ClearAllErrorsImpl();

            //Assert
            IEnumerable actual = instance.GetErrors(PropertyName);

            Assert.Null(actual);
        }

        [Fact]
        public void ClearAllErrors_WithOneErrorAdded_ShouldRaiseErrorsChangedEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);

            void AttachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged += handler;
            }

            void DetachErrorsChangedHandler(EventHandler<DataErrorsChangedEventArgs> handler)
            {
                instance.ErrorsChanged -= handler;
            }

            //Act
            void TestCode()
            {
                instance.ClearAllErrorsImpl();
            }

            //Assert
            Assert.Raises<DataErrorsChangedEventArgs>(AttachErrorsChangedHandler, DetachErrorsChangedHandler, TestCode);
        }

        [Fact]
        public void ClearAllErrors_WithNoErrorAdded_ShouldNotRaiseEvent()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();

            bool isInvoked = false;
            instance.ErrorsChanged += (sender, eventArgs) => isInvoked = true;

            //Act
            instance.ClearAllErrorsImpl();

            //Assert
            Assert.False(isInvoked);
        }

        private static DataErrorHandlingViewModelBaseMock GetDefaultInstance()
        {
            return new DataErrorHandlingViewModelBaseMock();
        }

        /// <summary>
        ///     Mock implementation of <see cref="DataErrorHandlingViewModelBase" /> to allow unit testing.
        /// </summary>
        private class DataErrorHandlingViewModelBaseMock : DataErrorHandlingViewModelBase
        {
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
            public void AddErrorImpl(string propertyName, string errorMessage)
            {
                AddError(propertyName, errorMessage);
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
            public void AddErrorsImpl(string propertyName, IEnumerable<string> errorMessages)
            {
                AddErrors(propertyName, errorMessages);
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
            public bool RemoveErrorImpl(string propertyName, string errorMessage)
            {
                return RemoveError(propertyName, errorMessage);
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
            public void ReplaceErrorsImpl(string propertyName, string errorMessage)
            {
                ReplaceErrors(propertyName, errorMessage);
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
            public void ReplaceErrorsImpl(string propertyName, IEnumerable<string> errorMessages)
            {
                ReplaceErrors(propertyName, errorMessages);
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
            public void ClearErrorsImpl(string propertyName)
            {
                ClearErrors(propertyName);
            }

            /// <summary>
            ///     Clears/Removes all the error message for the view model.
            /// </summary>
            public void ClearAllErrorsImpl()
            {
                ClearAllErrors();
            }
        }
    }
}