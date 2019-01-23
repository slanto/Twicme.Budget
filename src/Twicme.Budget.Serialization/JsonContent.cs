using System.Collections.Generic;

namespace Twicme.Budget.Store
{
    public class JsonContent : ValueObject<JsonContent>
    {
        public string Value { get; }

        public JsonContent(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}