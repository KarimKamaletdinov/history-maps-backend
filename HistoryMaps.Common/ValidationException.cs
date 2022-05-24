namespace HistoryMaps;

public class ValidationException : Exception
{
	public string FieldName { get; }
	public object PassedValue { get; }
	public object ExpectedValue { get; }

	public ValidationException(string fieldName, object passedValue,
		object expectedValue) 
		: base($"Invalid value for {fieldName}: {passedValue}." +
			$" Expected value: {expectedValue}")
	{
		FieldName = fieldName;
		PassedValue = passedValue;
		ExpectedValue = expectedValue;
	}
}
