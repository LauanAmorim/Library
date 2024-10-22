using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects
{
    public sealed class ISBN : IEquatable<ISBN>
    {
        private ISBN isbn;

        public string Value { get; }
        public IsbnType Type { get; }
        [JsonConstructor]
        public ISBN(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("ISBN is necessary.",nameof(value));
            value = value.Replace("-", "").Replace(" ", "");
            if (value.Length == 10)
            {
                if (!IsValidISBN10(value)) throw new ArgumentException("Invalid ISBN-10.", nameof(value));
                Type = IsbnType.ISBN10;
            }
            else if (value.Length == 13)
            {
                if (!IsValidISBN13(value)) throw new ArgumentException("Invalid ISBN-13.", nameof(value));
                Type = IsbnType.ISBN13;
            }
            else throw new ArgumentException("ISBN deve ter 10 ou 13 caracteres.", nameof(value));
            Value = value;
        }

        public ISBN(ISBN isbn)
        {
            this.isbn = isbn;
        }

        private bool IsValidISBN10(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")) return false;
            return true;
        }
        private bool IsValidISBN13(string isbn)
        {
            if (isbn.Length != 13 || !Regex.IsMatch(isbn, @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")) return false;
            return true;
        }
        public override bool Equals(Object obj)
        {
            return Equals(obj as ISBN);
        }
        public bool Equals(ISBN other)
        {
            return other != null && Value == other.Value && Type == other.Type;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Type);
        }
        public override string ToString()
        {
            return Value;
        }
        public static bool operator ==(ISBN left, ISBN right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(ISBN left, ISBN right)
        {
            return !(left == right);
        }
    }
}
