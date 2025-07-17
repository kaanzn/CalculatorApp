using org.mariuszgromada.math.mxparser;

namespace CalculatorApp.Models;

public static class MathEngine
{
    public static string Evaluate(string expression)
    {
        License.iConfirmNonCommercialUse("kaano");

        Expression e = new Expression(Sanitize(expression));

        if (!e.checkSyntax())
            return "Error";

        double value = e.calculate();

        if (value == -0)
            value = 0;

        if (double.IsNaN(value))
            return "Error";

        else
        {
            return value % 1 == 0
            ? ((int)value).ToString()
            : value.ToString();
        }
    }

    private static string Sanitize(string expression)
    {
        return expression
                .Replace("−", "-")
                .Replace("%", "#")
                .Replace(",", ".")
                .Replace("²", "^2")
                .Replace("³", "^3")
                .Replace("log₁₀", "log10");
    }
}