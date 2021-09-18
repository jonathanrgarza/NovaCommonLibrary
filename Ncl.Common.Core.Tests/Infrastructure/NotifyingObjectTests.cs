using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xunit;

namespace Ncl.Common.Core.Infrastructure.Tests
{
    public class NotifyingObjectTests
    {
        [Theory]
        [InlineData("Value", "Value")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void OnPropertyChanged_RaisesEventWithGivenName(string name, string expected)
        {
            //Arrange
            var instance = new NotifyingObjectMock();

            NotifyingObjectMock actualSender = null;
            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) =>
            {
                actualSender = sender as NotifyingObjectMock;
                actual = args;
            };

            //Act
            instance.OnPropertyChangedImpl(name);

            //Assert
            Assert.Equal(instance, actualSender);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual.PropertyName);
        }

        /// <summary>
        ///     Mock implementation of <see cref="NotifyingObject" /> to allow unit testing.
        /// </summary>
        private class NotifyingObjectMock : NotifyingObject
        {
            /// <summary>
            ///     Raises the PropertyChanged event for the given property name.
            ///     The calling member's name will be used as the parameter, by default.
            /// </summary>
            /// <param name="name">
            ///     The property's name.
            ///     Can specify <see cref="string.Empty" /> or null to signal all properties have changed.
            /// </param>
            public void OnPropertyChangedImpl([CallerMemberName] string name = null)
            {
                OnPropertyChanged(name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetImpl<T>(ref T current, T value, [CallerMemberName] string name = null)
            {
                return Set(ref current, value, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetImpl<T>(ref T current, T value, Action onSetAction,
                [CallerMemberName] string name = null)
            {
                return Set(ref current, value, onSetAction, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the <see cref="NotifyingObject.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetAndCallFirstImpl<T>(ref T current, T value, Action onSetAction,
                [CallerMemberName] string name = null)
            {
                return SetAndCallFirst(ref current, value, onSetAction, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetDoubleImpl(ref double current, double value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetDoubleImpl(ref double current, double value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the <see cref="NotifyingObject.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetDoubleAndCallFirstImpl(ref double current, double value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDoubleAndCallFirst(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetFloatImpl(ref float current, float value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetFloatImpl(ref float current, float value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the <see cref="NotifyingObject.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetFloatAndCallFirstImpl(ref float current, float value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloatAndCallFirst(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetDoubleImpl(ref double? current, double? value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetDoubleImpl(ref double? current, double? value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the <see cref="NotifyingObject.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetDoubleAndCallFirstImpl(ref double? current, double? value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDoubleAndCallFirst(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetFloatImpl(ref float? current, float? value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetFloatImpl(ref float? current, float? value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the <see cref="NotifyingObject.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns>True if the field's value changed, otherwise, false.</returns>
            public bool SetFloatAndCallFirstImpl(ref float? current, float? value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloatAndCallFirst(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Determines if the <paramref name="current" /> is considered equal to
            ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
            /// </summary>
            /// <param name="current">The current value.</param>
            /// <param name="value">The other value to compare against.</param>
            /// <param name="decimals">The decimal precision.</param>
            /// <returns>True if the two values are considered equal, otherwise, false.</returns>
            public bool IsDoubleEqualImpl(double current, double value, int decimals)
            {
                return IsDoubleEqual(current, value, decimals);
            }

            /// <summary>
            ///     Determines if the <paramref name="current" /> is considered equal to
            ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
            /// </summary>
            /// <param name="current">The current value.</param>
            /// <param name="value">The other value to compare against.</param>
            /// <param name="decimals">The decimal precision.</param>
            /// <returns>True if the two values are considered equal, otherwise, false.</returns>
            public bool IsFloatEqualImpl(float current, float value, int decimals)
            {
                return IsFloatEqual(current, value, decimals);
            }

            /// <summary>
            ///     Determines if the <paramref name="current" /> is considered equal to
            ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
            /// </summary>
            /// <param name="current">The current value.</param>
            /// <param name="value">The other value to compare against.</param>
            /// <param name="decimals">The decimal precision.</param>
            /// <returns>True if the two values are considered equal, otherwise, false.</returns>
            public bool IsDoubleEqualImpl(double? current, double? value, int decimals)
            {
                return IsDoubleEqual(current, value, decimals);
            }

            /// <summary>
            ///     Determines if the <paramref name="current" /> is considered equal to
            ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
            /// </summary>
            /// <param name="current">The current value.</param>
            /// <param name="value">The other value to compare against.</param>
            /// <param name="decimals">The decimal precision.</param>
            /// <returns>True if the two values are considered equal, otherwise, false.</returns>
            public bool IsFloatEqualImpl(float? current, float? value, int decimals)
            {
                return IsFloatEqual(current, value, decimals);
            }
        }

        #region Normal Set

        [Fact]
        public void Set_SetsValueWithDifferentValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new NotifyingObjectMock();

            //Act
            instance.SetImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            int initialValue = 0;
            var instance = new NotifyingObjectMock();

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            //Act
            int initialValue = 0;
            instance.SetImpl(ref initialValue, 10, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void Set_NoChangeWithSameValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new NotifyingObjectMock();

            //Act
            instance.SetImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            int initialValue = 10;
            var instance = new NotifyingObjectMock();

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_NoEventWithSameValue()
        {
            //Arrange
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            //Act
            int initialValue = 5;
            instance.SetImpl(ref initialValue, 5, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Set1_SetsValueWithDifferentValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            int initialValue = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 0;
            instance.SetImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void Set1_CallActionWithDifferentValue()
        {
            //Arrange
            bool expected = true;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 0;
            instance.SetImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_NoChangeWithSameValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            int initialValue = 10;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_NoEventWithSameValue()
        {
            //Arrange
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 5;
            instance.SetImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Set1_DontCallActionWithSameValue()
        {
            //Arrange
            bool expected = false;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 5;
            instance.SetImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_SetsValueWithDifferentValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            int initialValue = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 0;
            instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetAndCallFirst_CallActionWithDifferentValue()
        {
            //Arrange
            bool expected = true;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 0;
            instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_NoChangeWithSameValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            int initialValue = 10;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_NoEventWithSameValue()
        {
            //Arrange
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 5;
            instance.SetAndCallFirstImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetAndCallFirst_DontCallActionWithSameValue()
        {
            //Arrange
            bool expected = false;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 5;
            instance.SetAndCallFirstImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        #endregion

        #region Double Set

        [Fact]
        public void SetDouble_SetsValueWithDifferentValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0;
            var instance = new NotifyingObjectMock();

            //Act
            instance.SetDoubleImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            double initialValue = 0;
            var instance = new NotifyingObjectMock();

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, 10.5);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            //Act
            double initialValue = 0;
            instance.SetDoubleImpl(ref initialValue, 10.5, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetDouble_NoChangeWithSameValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0;
            var instance = new NotifyingObjectMock();

            //Act
            instance.SetDoubleImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            double initialValue = 10.5;
            var instance = new NotifyingObjectMock();

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, 10.5);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble_NoEventWithSameValue()
        {
            //Arrange
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            //Act
            double initialValue = 5.5;
            instance.SetDoubleImpl(ref initialValue, 5.5, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDouble1_SetsValueWithDifferentValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            double initialValue = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 0;
            instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetDouble1_CallActionWithDifferentValue()
        {
            //Arrange
            bool expected = true;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 0;
            instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_NoChangeWithSameValue()
        {
            //Arrange
            const int expected = 10;
            double actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            double initialValue = 10.5;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_NoEventWithSameValue()
        {
            //Arrange
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDouble1_DontCallActionWithSameValue()
        {
            //Arrange
            bool expected = false;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_SetsValueWithDifferentValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            double initialValue = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 0;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetDoubleAndCallFirst_CallActionWithDifferentValue()
        {
            //Arrange
            bool expected = true;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 0;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_NoChangeWithSameValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            double initialValue = 10.5;
            var instance = new NotifyingObjectMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_NoEventWithSameValue()
        {
            //Arrange
            var instance = new NotifyingObjectMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDoubleAndCallFirst_DontCallActionWithSameValue()
        {
            //Arrange
            bool expected = false;
            var instance = new NotifyingObjectMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        #endregion
    }
}