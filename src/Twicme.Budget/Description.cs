using System;
using System.Collections.Generic;
using System.Globalization;

namespace Twicme.Budget
{
    public sealed class Description : ValueObject<Description>
    {
        public static readonly Description Empty = new Description(string.Empty);
        
        public string Content { get; }
        
        public bool Contains(string phrase) => Content.Contains(phrase, StringComparison.OrdinalIgnoreCase);
        public bool IsEmpty => string.IsNullOrEmpty(Content);
        
        public Description(string content)
        {
            Content = string.IsNullOrEmpty(content) ? string.Empty : content;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Content;
        }
    }
}