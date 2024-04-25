using System;

// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     Represents a generic return result with a successful or failed outcome.
    /// </summary>
    /// <typeparam name="TS">The type of the result.</typeparam>
    public class ReturnResult<TS> : IEquatable<ReturnResult<TS>>
    {
        private ReturnResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS}" /> class with a successful result.
        /// </summary>
        /// <param name="result">The result value.</param>
        public ReturnResult(TS result)
        {
            Result = result;
            IsSuccessful = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS}" /> class with a failure.
        /// </summary>
        /// <param name="failure">The failure exception.</param>
        public ReturnResult(Exception failure)
        {
            Failure = failure;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Gets a value indicating whether the return result is successful.
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        ///     Gets the result value.
        /// </summary>
        public TS Result { get; private set; }

        /// <summary>
        ///     Gets the failure exception.
        /// </summary>
        public Exception Failure { get; private set; }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS}" /> object is equal to another object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the current object is equal to the other object; otherwise, <c>false</c>.</returns>
        public bool Equals(ReturnResult<TS> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return IsSuccessful == other.IsSuccessful && Equals(Result, other.Result) && Equals(Failure, other.Failure);
        }

        /// <summary>
        ///     Tries to get the result value.
        /// </summary>
        /// <param name="result">The result value.</param>
        /// <returns><c>true</c> if the return result is successful; otherwise, <c>false</c>.</returns>
        public bool TryGetResult(out TS result)
        {
            result = Result;
            return IsSuccessful;
        }

        /// <summary>
        ///     Tries to get the failure exception.
        /// </summary>
        /// <param name="failure">The failure exception.</param>
        /// <returns><c>true</c> if the return result is a failure; otherwise, <c>false</c>.</returns>
        public bool TryGetFailure(out Exception failure)
        {
            failure = Failure;
            return !IsSuccessful;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS}" /> class with a successful result.
        /// </summary>
        /// <param name="result">The result value.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS}" /> class with a successful result.</returns>
        public static ReturnResult<TS> SuccessResult(TS result)
        {
            var returnResult = new ReturnResult<TS>
            {
                Result = result,
                IsSuccessful = true
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS}" /> class with a failure.
        /// </summary>
        /// <param name="failure">The failure exception.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS}" /> class with a failure.</returns>
        public static ReturnResult<TS> FailureResult(Exception failure)
        {
            var returnResult = new ReturnResult<TS>
            {
                Failure = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsSuccessful.GetHashCode();
                hashCode = (hashCode * 397) ^ (Result != null ? Result.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure != null ? Failure.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as ReturnResult<TS>);
        }
    }

    /// <summary>
    ///     Represents a result that can either be successful or a failure.
    /// </summary>
    /// <typeparam name="TS">The type of the successful result.</typeparam>
    /// <typeparam name="TF">The type of the failure result.</typeparam>
    public class ReturnResult<TS, TF> : IEquatable<ReturnResult<TS, TF>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF}" /> class.
        /// </summary>
        private ReturnResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF}" /> class with a successful result.
        /// </summary>
        /// <param name="result">The successful result.</param>
        public ReturnResult(TS result)
        {
            Result = result;
            IsSuccessful = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF}" /> class with a failure result.
        /// </summary>
        /// <param name="failure">The failure result.</param>
        public ReturnResult(TF failure)
        {
            Failure = failure;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Gets a value indicating whether the result is successful.
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        ///     Gets the successful result.
        /// </summary>
        public TS Result { get; private set; }

        /// <summary>
        ///     Gets the failure result.
        /// </summary>
        public TF Failure { get; private set; }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS, TF}" /> object is equal to another object of the same
        ///     type.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(ReturnResult<TS, TF> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return IsSuccessful == other.IsSuccessful && Equals(Result, other.Result) && Equals(Failure, other.Failure);
        }

        /// <summary>
        ///     Tries to get the successful result.
        /// </summary>
        /// <param name="result">
        ///     When this method returns, contains the successful result if the result is successful; otherwise,
        ///     the default value.
        /// </param>
        /// <returns>true if the result is successful; otherwise, false.</returns>
        public bool TryGetResult(out TS result)
        {
            result = Result;
            return IsSuccessful;
        }

        /// <summary>
        ///     Tries to get the failure result.
        /// </summary>
        /// <param name="failure">
        ///     When this method returns, contains the failure result if the result is a failure; otherwise, the
        ///     default value.
        /// </param>
        /// <returns>true if the result is a failure; otherwise, false.</returns>
        public bool TryGetFailure(out TF failure)
        {
            failure = Failure;
            return !IsSuccessful;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF}" /> class with a successful result.
        /// </summary>
        /// <param name="result">The successful result.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF}" /> class with a successful result.</returns>
        public static ReturnResult<TS, TF> SuccessResult(TS result)
        {
            var returnResult = new ReturnResult<TS, TF>
            {
                Result = result,
                IsSuccessful = true
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF}" /> class with a failure result.
        /// </summary>
        /// <param name="failure">The failure result.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF}" /> class with a failure result.</returns>
        public static ReturnResult<TS, TF> FailureResult(TF failure)
        {
            var returnResult = new ReturnResult<TS, TF>
            {
                Failure = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Returns the hash code for this <see cref="ReturnResult{TS, TF}" /> object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsSuccessful.GetHashCode();
                hashCode = (hashCode * 397) ^ (Result != null ? Result.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure != null ? Failure.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current <see cref="ReturnResult{TS, TF}" /> object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ReturnResult<TS, TF>);
        }
    }

    /// <summary>
    ///     Represents a generic return result with two possible failure types.
    /// </summary>
    /// <typeparam name="TS">The type of the successful result.</typeparam>
    /// <typeparam name="TF1">The type of the first failure.</typeparam>
    /// <typeparam name="TF2">The type of the second failure.</typeparam>
    public class ReturnResult<TS, TF1, TF2> : IEquatable<ReturnResult<TS, TF1, TF2>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2}" /> class.
        /// </summary>
        private ReturnResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2}" /> class with a successful result.
        /// </summary>
        /// <param name="result">The successful result.</param>
        public ReturnResult(TS result)
        {
            Result = result;
            IsSuccessful = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2}" /> class with the first failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        public ReturnResult(TF1 failure)
        {
            Failure1 = failure;
            FailureNumber = 1;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2}" /> class with the second failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        public ReturnResult(TF2 failure)
        {
            Failure2 = failure;
            FailureNumber = 2;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Gets the number indicating the type of failure.
        /// </summary>
        public int FailureNumber { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the result is successful.
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        ///     Gets the successful result.
        /// </summary>
        public TS Result { get; private set; }

        /// <summary>
        ///     Gets the first failure.
        /// </summary>
        public TF1 Failure1 { get; private set; }

        /// <summary>
        ///     Gets the second failure.
        /// </summary>
        public TF2 Failure2 { get; private set; }

        /// <summary>
        ///     Determines whether this instance is equal to another <see cref="ReturnResult{TS, TF1, TF2}" /> instance.
        /// </summary>
        /// <param name="other">The other <see cref="ReturnResult{TS, TF1, TF2}" /> instance to compare.</param>
        /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(ReturnResult<TS, TF1, TF2> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return IsSuccessful == other.IsSuccessful && Equals(Result, other.Result) &&
                   Equals(Failure1, other.Failure1) && Equals(Failure2, other.Failure2) &&
                   FailureNumber == other.FailureNumber;
        }

        /// <summary>
        ///     Tries to get the successful result.
        /// </summary>
        /// <param name="result">The successful result if available.</param>
        /// <returns><c>true</c> if the result is successful; otherwise, <c>false</c>.</returns>
        public bool TryGetResult(out TS result)
        {
            result = Result;
            return IsSuccessful;
        }

        /// <summary>
        ///     Tries to get the failure details.
        /// </summary>
        /// <param name="failureNumber">The number indicating the type of failure.</param>
        /// <param name="failure1">The first failure if available.</param>
        /// <param name="failure2">The second failure if available.</param>
        /// <returns><c>true</c> if the result is a failure; otherwise, <c>false</c>.</returns>
        public bool TryGetFailure(out int failureNumber, out TF1 failure1, out TF2 failure2)
        {
            failureNumber = FailureNumber;
            failure1 = Failure1;
            failure2 = Failure2;
            return !IsSuccessful;
        }

        /// <summary>
        ///     Creates a new <see cref="ReturnResult{TS, TF1, TF2}" /> instance with a successful result.
        /// </summary>
        /// <param name="result">The successful result.</param>
        /// <returns>A new <see cref="ReturnResult{TS, TF1, TF2}" /> instance with a successful result.</returns>
        public static ReturnResult<TS, TF1, TF2> SuccessResult(TS result)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2>
            {
                Result = result,
                IsSuccessful = true
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new <see cref="ReturnResult{TS, TF1, TF2}" /> instance with the first failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        /// <returns>A new <see cref="ReturnResult{TS, TF1, TF2}" /> instance with the first failure.</returns>
        public static ReturnResult<TS, TF1, TF2> Failure1Result(TF1 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2>
            {
                FailureNumber = 1,
                Failure1 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new <see cref="ReturnResult{TS, TF1, TF2}" /> instance with the second failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        /// <returns>A new <see cref="ReturnResult{TS, TF1, TF2}" /> instance with the second failure.</returns>
        public static ReturnResult<TS, TF1, TF2> Failure2Result(TF2 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2>
            {
                FailureNumber = 2,
                Failure2 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsSuccessful.GetHashCode();
                hashCode = (hashCode * 397) ^ (Result != null ? Result.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure1 != null ? Failure1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure2 != null ? Failure2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ FailureNumber;
                return hashCode;
            }
        }

        /// <summary>
        ///     Determines whether this instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><c>true</c> if the object is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ReturnResult<TS, TF1, TF2>);
        }
    }

    /// <summary>
    ///     Represents a generic return result with three possible failure types.
    /// </summary>
    /// <typeparam name="TS">The type of the success result.</typeparam>
    /// <typeparam name="TF1">The type of the first failure.</typeparam>
    /// <typeparam name="TF2">The type of the second failure.</typeparam>
    /// <typeparam name="TF3">The type of the third failure.</typeparam>
    public class ReturnResult<TS, TF1, TF2, TF3> : IEquatable<ReturnResult<TS, TF1, TF2, TF3>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class.
        /// </summary>
        private ReturnResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with a success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        public ReturnResult(TS result)
        {
            Result = result;
            IsSuccessful = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the first failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        public ReturnResult(TF1 failure)
        {
            Failure1 = failure;
            FailureNumber = 1;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the second failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        public ReturnResult(TF2 failure)
        {
            Failure2 = failure;
            FailureNumber = 2;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the third failure.
        /// </summary>
        /// <param name="failure">The third failure.</param>
        public ReturnResult(TF3 failure)
        {
            Failure3 = failure;
            FailureNumber = 3;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Gets the number of the failure.
        /// </summary>
        public int FailureNumber { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the return result is successful.
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        ///     Gets the success result.
        /// </summary>
        public TS Result { get; private set; }

        /// <summary>
        ///     Gets the first failure.
        /// </summary>
        public TF1 Failure1 { get; private set; }

        /// <summary>
        ///     Gets the second failure.
        /// </summary>
        public TF2 Failure2 { get; private set; }

        /// <summary>
        ///     Gets the third failure.
        /// </summary>
        public TF3 Failure3 { get; private set; }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> object is equal to another object of
        ///     the same type.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        ///     <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>
        ///     .
        /// </returns>
        public bool Equals(ReturnResult<TS, TF1, TF2, TF3> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return IsSuccessful == other.IsSuccessful && Equals(Result, other.Result) &&
                   Equals(Failure1, other.Failure1) && Equals(Failure2, other.Failure2) &&
                   Equals(Failure3, other.Failure3) && FailureNumber == other.FailureNumber;
        }

        /// <summary>
        ///     Tries to get the success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        /// <returns><c>true</c> if the return result is successful; otherwise, <c>false</c>.</returns>
        public bool TryGetResult(out TS result)
        {
            result = Result;
            return IsSuccessful;
        }

        /// <summary>
        ///     Tries to get the failure details.
        /// </summary>
        /// <param name="failureNumber">The number of the failure.</param>
        /// <param name="failure1">The first failure.</param>
        /// <param name="failure2">The second failure.</param>
        /// <param name="failure3">The third failure.</param>
        /// <returns><c>true</c> if the return result is a failure; otherwise, <c>false</c>.</returns>
        public bool TryGetFailure(out int failureNumber, out TF1 failure1, out TF2 failure2, out TF3 failure3)
        {
            failureNumber = FailureNumber;
            failure1 = Failure1;
            failure2 = Failure2;
            failure3 = Failure3;
            return !IsSuccessful;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with a success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with a success result.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3> SuccessResult(TS result)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3>
            {
                Result = result,
                IsSuccessful = true
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the first failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the first failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3> Failure1Result(TF1 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3>
            {
                FailureNumber = 1,
                Failure1 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the second failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the second failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3> Failure2Result(TF2 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3>
            {
                FailureNumber = 2,
                Failure2 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the third failure.
        /// </summary>
        /// <param name="failure">The third failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> class with the third failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3> Failure3Result(TF3 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3>
            {
                FailureNumber = 3,
                Failure3 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Returns the hash code for the current <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsSuccessful.GetHashCode();
                hashCode = (hashCode * 397) ^ (Result != null ? Result.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure1 != null ? Failure1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure2 != null ? Failure2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure3 != null ? Failure3.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ FailureNumber;
                return hashCode;
            }
        }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS, TF1, TF2, TF3}" /> object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ReturnResult<TS, TF1, TF2, TF3>);
        }
    }

    /// <summary>
    ///     Represents a generic return result with four possible failure types.
    /// </summary>
    /// <typeparam name="TS">The type of the success result.</typeparam>
    /// <typeparam name="TF1">The type of the first failure.</typeparam>
    /// <typeparam name="TF2">The type of the second failure.</typeparam>
    /// <typeparam name="TF3">The type of the third failure.</typeparam>
    /// <typeparam name="TF4">The type of the fourth failure.</typeparam>
    public class ReturnResult<TS, TF1, TF2, TF3, TF4> : IEquatable<ReturnResult<TS, TF1, TF2, TF3, TF4>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class.
        /// </summary>
        private ReturnResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with a success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        public ReturnResult(TS result)
        {
            Result = result;
            IsSuccessful = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the first failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        public ReturnResult(TF1 failure)
        {
            Failure1 = failure;
            FailureNumber = 1;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the second
        ///     failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        public ReturnResult(TF2 failure)
        {
            Failure2 = failure;
            FailureNumber = 2;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the third failure.
        /// </summary>
        /// <param name="failure">The third failure.</param>
        public ReturnResult(TF3 failure)
        {
            Failure3 = failure;
            FailureNumber = 3;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the fourth
        ///     failure.
        /// </summary>
        /// <param name="failure">The fourth failure.</param>
        public ReturnResult(TF4 failure)
        {
            Failure4 = failure;
            FailureNumber = 4;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Gets the number of the failure.
        /// </summary>
        public int FailureNumber { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the return result is successful.
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        ///     Gets the success result.
        /// </summary>
        public TS Result { get; private set; }

        /// <summary>
        ///     Gets the first failure.
        /// </summary>
        public TF1 Failure1 { get; private set; }

        /// <summary>
        ///     Gets the second failure.
        /// </summary>
        public TF2 Failure2 { get; private set; }

        /// <summary>
        ///     Gets the third failure.
        /// </summary>
        public TF3 Failure3 { get; private set; }

        /// <summary>
        ///     Gets the fourth failure.
        /// </summary>
        public TF4 Failure4 { get; private set; }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> object is equal to another
        ///     object of the same type.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        ///     <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>
        ///     .
        /// </returns>
        public bool Equals(ReturnResult<TS, TF1, TF2, TF3, TF4> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return IsSuccessful == other.IsSuccessful && Equals(Result, other.Result) &&
                   Equals(Failure1, other.Failure1) && Equals(Failure2, other.Failure2) &&
                   Equals(Failure3, other.Failure3) && Equals(Failure4, other.Failure4) &&
                   FailureNumber == other.FailureNumber;
        }

        /// <summary>
        ///     Tries to get the success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        /// <returns><c>true</c> if the return result is successful; otherwise, <c>false</c>.</returns>
        public bool TryGetResult(out TS result)
        {
            result = Result;
            return IsSuccessful;
        }

        /// <summary>
        ///     Tries to get the failure details.
        /// </summary>
        /// <param name="failureNumber">The number of the failure.</param>
        /// <param name="failure1">The first failure.</param>
        /// <param name="failure2">The second failure.</param>
        /// <param name="failure3">The third failure.</param>
        /// <param name="failure4">The fourth failure.</param>
        /// <returns><c>true</c> if the return result is a failure; otherwise, <c>false</c>.</returns>
        public bool TryGetFailure(out int failureNumber, out TF1 failure1, out TF2 failure2, out TF3 failure3,
            out TF4 failure4)
        {
            failureNumber = FailureNumber;
            failure1 = Failure1;
            failure2 = Failure2;
            failure3 = Failure3;
            failure4 = Failure4;
            return !IsSuccessful;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with a success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with a success result.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4> SuccessResult(TS result)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4>
            {
                Result = result,
                IsSuccessful = true
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the first failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the first failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4> Failure1Result(TF1 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4>
            {
                FailureNumber = 1,
                Failure1 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the second failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the second failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4> Failure2Result(TF2 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4>
            {
                FailureNumber = 2,
                Failure2 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the third failure.
        /// </summary>
        /// <param name="failure">The third failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the third failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4> Failure3Result(TF3 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4>
            {
                FailureNumber = 3,
                Failure3 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the fourth failure.
        /// </summary>
        /// <param name="failure">The fourth failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> class with the fourth failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4> Failure4Result(TF4 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4>
            {
                FailureNumber = 4,
                Failure4 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Returns the hash code for the current <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsSuccessful.GetHashCode();
                hashCode = (hashCode * 397) ^ (Result != null ? Result.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure1 != null ? Failure1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure2 != null ? Failure2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure3 != null ? Failure3.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure4 != null ? Failure4.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ FailureNumber;
                return hashCode;
            }
        }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4}" /> object is equal to another
        ///     object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ReturnResult<TS, TF1, TF2, TF3, TF4>);
        }
    }

    /// <summary>
    ///     Represents a generic return result with five possible failure types.
    /// </summary>
    /// <typeparam name="TS">The type of the success result.</typeparam>
    /// <typeparam name="TF1">The type of the first failure.</typeparam>
    /// <typeparam name="TF2">The type of the second failure.</typeparam>
    /// <typeparam name="TF3">The type of the third failure.</typeparam>
    /// <typeparam name="TF4">The type of the fourth failure.</typeparam>
    /// <typeparam name="TF5">The type of the fifth failure.</typeparam>
    public class ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> : IEquatable<ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class.
        /// </summary>
        private ReturnResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with a success
        ///     result.
        /// </summary>
        /// <param name="result">The success result.</param>
        public ReturnResult(TS result)
        {
            Result = result;
            IsSuccessful = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the first
        ///     failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        public ReturnResult(TF1 failure)
        {
            Failure1 = failure;
            FailureNumber = 1;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the second
        ///     failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        public ReturnResult(TF2 failure)
        {
            Failure2 = failure;
            FailureNumber = 2;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the third
        ///     failure.
        /// </summary>
        /// <param name="failure">The third failure.</param>
        public ReturnResult(TF3 failure)
        {
            Failure3 = failure;
            FailureNumber = 3;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the fourth
        ///     failure.
        /// </summary>
        /// <param name="failure">The fourth failure.</param>
        public ReturnResult(TF4 failure)
        {
            Failure4 = failure;
            FailureNumber = 4;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the fifth
        ///     failure.
        /// </summary>
        /// <param name="failure">The fifth failure.</param>
        public ReturnResult(TF5 failure)
        {
            Failure5 = failure;
            FailureNumber = 5;
            IsSuccessful = false;
        }

        /// <summary>
        ///     Gets the number of the failure.
        /// </summary>
        public int FailureNumber { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the return result is successful.
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        ///     Gets the success result.
        /// </summary>
        public TS Result { get; private set; }

        /// <summary>
        ///     Gets the first failure.
        /// </summary>
        public TF1 Failure1 { get; private set; }

        /// <summary>
        ///     Gets the second failure.
        /// </summary>
        public TF2 Failure2 { get; private set; }

        /// <summary>
        ///     Gets the third failure.
        /// </summary>
        public TF3 Failure3 { get; private set; }

        /// <summary>
        ///     Gets the fourth failure.
        /// </summary>
        public TF4 Failure4 { get; private set; }

        /// <summary>
        ///     Gets the fifth failure.
        /// </summary>
        public TF5 Failure5 { get; private set; }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> object is equal to another
        ///     object of the same type.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        ///     <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>
        ///     .
        /// </returns>
        public bool Equals(ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return IsSuccessful == other.IsSuccessful && Equals(Result, other.Result) &&
                   Equals(Failure1, other.Failure1) && Equals(Failure2, other.Failure2) &&
                   Equals(Failure3, other.Failure3) && Equals(Failure4, other.Failure4) &&
                   Equals(Failure5, other.Failure5) && FailureNumber == other.FailureNumber;
        }

        /// <summary>
        ///     Tries to get the success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        /// <returns><c>true</c> if the return result is successful; otherwise, <c>false</c>.</returns>
        public bool TryGetResult(out TS result)
        {
            result = Result;
            return IsSuccessful;
        }

        /// <summary>
        ///     Tries to get the failure details.
        /// </summary>
        /// <param name="failureNumber">The number of the failure.</param>
        /// <param name="failure1">The first failure.</param>
        /// <param name="failure2">The second failure.</param>
        /// <param name="failure3">The third failure.</param>
        /// <param name="failure4">The fourth failure.</param>
        /// <param name="failure5">The fifth failure.</param>
        /// <returns><c>true</c> if the return result is a failure; otherwise, <c>false</c>.</returns>
        public bool TryGetFailure(out int failureNumber, out TF1 failure1, out TF2 failure2, out TF3 failure3,
            out TF4 failure4, out TF5 failure5)
        {
            failureNumber = FailureNumber;
            failure1 = Failure1;
            failure2 = Failure2;
            failure3 = Failure3;
            failure4 = Failure4;
            failure5 = Failure5;
            return !IsSuccessful;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with a success result.
        /// </summary>
        /// <param name="result">The success result.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with a success result.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> SuccessResult(TS result)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>
            {
                Result = result,
                IsSuccessful = true
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the first
        ///     failure.
        /// </summary>
        /// <param name="failure">The first failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the first failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> Failure1Result(TF1 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>
            {
                FailureNumber = 1,
                Failure1 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the second
        ///     failure.
        /// </summary>
        /// <param name="failure">The second failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the second failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> Failure2Result(TF2 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>
            {
                FailureNumber = 2,
                Failure2 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the third
        ///     failure.
        /// </summary>
        /// <param name="failure">The third failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the third failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> Failure3Result(TF3 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>
            {
                FailureNumber = 3,
                Failure3 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the fourth
        ///     failure.
        /// </summary>
        /// <param name="failure">The fourth failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the fourth failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> Failure4Result(TF4 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>
            {
                FailureNumber = 4,
                Failure4 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the fifth
        ///     failure.
        /// </summary>
        /// <param name="failure">The fifth failure.</param>
        /// <returns>A new instance of the <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> class with the fifth failure.</returns>
        public static ReturnResult<TS, TF1, TF2, TF3, TF4, TF5> Failure5Result(TF5 failure)
        {
            var returnResult = new ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>
            {
                FailureNumber = 5,
                Failure5 = failure,
                IsSuccessful = false
            };
            return returnResult;
        }

        /// <summary>
        ///     Returns the hash code for the current <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsSuccessful.GetHashCode();
                hashCode = (hashCode * 397) ^ (Result != null ? Result.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure1 != null ? Failure1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure2 != null ? Failure2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure3 != null ? Failure3.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure4 != null ? Failure4.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Failure5 != null ? Failure5.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ FailureNumber;
                return hashCode;
            }
        }

        /// <summary>
        ///     Determines whether the current <see cref="ReturnResult{TS, TF1, TF2, TF3, TF4, TF5}" /> object is equal to another
        ///     object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ReturnResult<TS, TF1, TF2, TF3, TF4, TF5>);
        }
    }
}