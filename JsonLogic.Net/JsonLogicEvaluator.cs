using System;
using System.Linq;
using SimpleJson;

namespace JsonLogic.Net
{
    public class JsonLogicEvaluator : IProcessJsonLogic
    {
        private IManageOperators operations;

        public JsonLogicEvaluator(IManageOperators operations)
        {
            this.operations = operations;
        }

        public object Apply(object rule, object data)
        {
            if (rule is null) return null;
            if (rule is JsonArray jsonArray) return jsonArray.Select(r => Apply(r, data)).ToArray();
            if (rule is JsonObject jsonObject)
            {
                var p = jsonObject.First();
                var opName = p.Key;
                var opArgs = p.Value as JsonArray ?? new JsonArray(){p.Value};
                var op = operations.GetOperator(opName);
                return op(this, opArgs, data);
            }
            return AdjustType(rule);
        }

        private object AdjustType(object value)
        {
            return value.IsNumeric() ? Convert.ToDouble(value) : value;
        }
    }
}
